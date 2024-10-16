//
// Textify  Copyright (C) 2023-2024  Aptivi
//
// This file is part of Textify
//
// Textify is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Textify is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Textify.General.Comparers;
using Textify.General.Data;
using Textify.SpaceManager;
using Textify.Tools;

namespace Textify.General
{
    /// <summary>
    /// Tools for text manipulation
    /// </summary>
    public static class TextTools
    {
        private static readonly string[] escaped = [@"\\", @"\*", @"\+", @"\?", @"\|", @"\{", @"\[", @"\(", @"\)", @"\^", @"\$", @"\.", @"\#", @"\ ", @"\-", @"\""", @"\'", @"\`", @"\!"];
        private static readonly string[] unescaped = [@"\", @"*", @"+", @"?", @"|", @"{", @"[", @"(", @")", @"^", @"$", @".", @"#", @" ", @"-", @"""", @"'", @"`", @"!"];
        private static readonly Dictionary<int, (int, CharWidthType)> cachedWidths = [];

        /// <summary>
        /// Whether to use two cells for unassigned characters or only one cell
        /// </summary>
        public static bool UseTwoCellsForUnassignedChars { get; set; }
        /// <summary>
        /// Whether to use two cells for ambiguous characters or only one cell
        /// </summary>
        public static bool UseTwoCellsForAmbiguousChars { get; set; }
        /// <summary>
        /// Whether to use two cells for private characters or only one cell
        /// </summary>
        public static bool UseTwoCellsForPrivateChars { get; set; }

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula
        /// </summary>
        /// <param name="target">Target string</param>
        public static string[] SplitEncloseDoubleQuotes(this string target) =>
            target.SplitEncloseDoubleQuotesNoRelease()
                .Select((s) => s.ReleaseDoubleQuotes())
                .ToArray();

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="partialQuoteSplitChars">Split characters for partial quotes</param>
        public static string[] SplitEncloseDoubleQuotes(this string target, char[]? partialQuoteSplitChars = null) =>
            target.SplitEncloseDoubleQuotesNoRelease(partialQuoteSplitChars)
                .Select((s) => s.ReleaseDoubleQuotes())
                .ToArray();

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="match">Character to match</param>
        /// <param name="partialQuoteSplitChars">Split characters for partial quotes</param>
        public static string[] SplitEncloseDoubleQuotes(this string target, char match = ' ', char[]? partialQuoteSplitChars = null) =>
            target.SplitEncloseDoubleQuotesNoRelease(match, partialQuoteSplitChars)
                .Select((s) => s.ReleaseDoubleQuotes())
                .ToArray();

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="condition">Condition for matching</param>
        /// <param name="partialQuoteSplitChars">Split characters for partial quotes</param>
        public static string[] SplitEncloseDoubleQuotes(this string target, Func<char, bool> condition, char[]? partialQuoteSplitChars = null) =>
            target.SplitEncloseDoubleQuotesNoRelease(condition, partialQuoteSplitChars)
                .Select((s) => s.ReleaseDoubleQuotes())
                .ToArray();

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula without releasing double quotes
        /// </summary>
        /// <param name="target">Target string</param>
        public static string[] SplitEncloseDoubleQuotesNoRelease(this string target) =>
            SplitEncloseDoubleQuotesNoRelease(target, char.IsWhiteSpace, CharManager.GetAllWhitespaceChars());

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula without releasing double quotes
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="partialQuoteSplitChars">Split characters for partial quotes</param>
        public static string[] SplitEncloseDoubleQuotesNoRelease(this string target, char[]? partialQuoteSplitChars = null) =>
            SplitEncloseDoubleQuotesNoRelease(target, char.IsWhiteSpace, partialQuoteSplitChars ?? CharManager.GetAllWhitespaceChars());

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by a specific character using regular expression formula without releasing double quotes
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="match">Character to match</param>
        /// <param name="partialQuoteSplitChars">Split characters for partial quotes</param>
        public static string[] SplitEncloseDoubleQuotesNoRelease(this string target, char match = ' ', char[]? partialQuoteSplitChars = null) =>
            SplitEncloseDoubleQuotesNoRelease(target, match.Equals, partialQuoteSplitChars ?? CharManager.GetAllWhitespaceChars());

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by a condition descriptor using regular expression formula without releasing double quotes
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="condition">Condition for matching</param>
        /// <param name="partialQuoteSplitChars">Split characters for partial quotes</param>
        public static string[] SplitEncloseDoubleQuotesNoRelease(this string target, Func<char, bool> condition, char[]? partialQuoteSplitChars = null)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            // Build the split matches
            List<string> matchesStr = [];
            bool inEscape = false;
            bool inQuote = false;
            char quoteChar = default;
            StringBuilder builder = new();
            for (int i = 0; i < target.Length; i++)
            {
                // Add a character
                char character = target[i];
                if (condition(character) && !inEscape && !inQuote)
                {
                    // Ignore all empty strings
                    if (builder.Length > 0)
                    {
                        matchesStr.Add(builder.ToString());
                        builder.Clear();
                    }
                }
                else
                    builder.Append(character);

                // Deal with the quotes
                if ((character == '\"' || character == '\'' || character == '`') && !inEscape)
                {
                    if (!inQuote)
                    {
                        quoteChar = character;
                        inQuote = true;
                    }
                    else if (character == quoteChar)
                    {
                        quoteChar = default;
                        inQuote = false;
                    }
                }

                // Deal with the escapes
                if (character == '\\')
                    inEscape = true;
                else if (inEscape)
                    inEscape = false;
            }

            // Add the final portion, but check the quotes
            if (builder.Length > 0)
            {
                string final = builder.ToString();
                if (inQuote)
                {
                    // Now, split this portion normally with spaces, but check for at least three quotes
                    int finalQuoteIdx = final.LastIndexOf(quoteChar);
                    string firstPart = final.Substring(0, finalQuoteIdx);
                    string secondPart = final.Substring(finalQuoteIdx);
                    var splitFinal = secondPart.Split(partialQuoteSplitChars ?? CharManager.GetAllWhitespaceChars());
                    splitFinal[0] = firstPart + splitFinal[0];
                    matchesStr.AddRange(splitFinal);
                }
                else
                    matchesStr.Add(final);
            }
            return [.. matchesStr];
        }

        /// <summary>
        /// Releases a string from double quotations
        /// </summary>
        /// <param name="target">Target string</param>
        /// <returns>A string that doesn't contain double quotation marks at the start and at the end of the string</returns>
        public static string ReleaseDoubleQuotes(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            string ReleasedString = target;
            if (target.StartsWith("\"") && target.EndsWith("\"") && target != "\"" ||
                target.StartsWith("'") && target.EndsWith("'") && target != "'" ||
                target.StartsWith("`") && target.EndsWith("`") && target != "`")
            {
                ReleasedString = ReleasedString.Remove(0, 1);
                ReleasedString = ReleasedString.Remove(ReleasedString.Length - 1);
            }
            return ReleasedString;
        }

        /// <summary>
        /// Gets the enclosed double quotes type from the string
        /// </summary>
        /// <param name="target">Target string to query</param>
        /// <returns><see cref="EnclosedDoubleQuotesType"/> containing information about the current string enclosure</returns>
        public static EnclosedDoubleQuotesType GetEnclosedDoubleQuotesType(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            var type = EnclosedDoubleQuotesType.None;
            if (target.StartsWith("\"") && target.EndsWith("\"") && target != "\"")
                type = EnclosedDoubleQuotesType.DoubleQuotes;
            else if (target.StartsWith("'") && target.EndsWith("'") && target != "'")
                type = EnclosedDoubleQuotesType.SingleQuotes;
            else if (target.StartsWith("`") && target.EndsWith("`") && target != "`")
                type = EnclosedDoubleQuotesType.Backticks;
            return type;
        }

        /// <summary>
        /// Makes a string array with new line as delimiter
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="emptyStrings">Whether to include empty strings in the output or not</param>
        /// <returns>List of words that are separated by the new lines</returns>
        public static string[] SplitNewLines(this string target, bool emptyStrings = true)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            var result = target.UnixifyNewLines().Split(Convert.ToChar(10));
            if (!emptyStrings)
                result = result.Where((str) => !string.IsNullOrEmpty(str)).ToArray();
            return result;
        }

        /// <summary>
        /// Makes a string array with new line as delimiter (the old way)
        /// </summary>
        /// <param name="target">Target string</param>
        /// <returns>List of words that are separated by the new lines</returns>
        [Obsolete("This doesn't properly split Mac OS 9 newlines.")]
        public static string[] SplitNewLinesOld(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            return target
                .Replace(Convert.ToChar(13).ToString(), "")
                .Split(Convert.ToChar(10));
        }

        /// <summary>
        /// Converts any existing new lines (Windows and Mac OS 9) to Unix format (Line Feeds) for universal parsing and for general use
        /// </summary>
        /// <param name="target">Target string</param>
        /// <returns>A string that contains new lines converted to the Unix format</returns>
        /// <exception cref="TextifyException"></exception>
        public static string UnixifyNewLines(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            // Convert Windows (CR+LF) and Mac OS 9 (CR) to Unix (LF)
            return target
                .Replace($"{Convert.ToChar(13)}{Convert.ToChar(10)}", $"{Convert.ToChar(10)}")  // CRLF
                .Replace($"{Convert.ToChar(13)}", $"{Convert.ToChar(10)}")                      // CR
                .Replace($"{Convert.ToChar(11)}", $"{Convert.ToChar(10)}")                      // VT
                .Replace($"{Convert.ToChar(12)}", $"{Convert.ToChar(10)}")                      // FF
                .Replace($"{Convert.ToChar(133)}", $"{Convert.ToChar(10)}")                     // NEL
                .Replace($"{Convert.ToChar(0x2028)}", $"{Convert.ToChar(10)}")                  // LS
                .Replace($"{Convert.ToChar(0x2029)}", $"{Convert.ToChar(10)}")                  // PS
            ;
        }

        /// <summary>
        /// Checks to see if the string starts with any of the values
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="values">Values</param>
        /// <returns>True if the string starts with any of the values specified in the array. Otherwise, false.</returns>
        public static bool StartsWithAnyOf(this string target, string[] values)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            bool started = false;
            foreach (string value in values)
                if (target.StartsWith(value))
                    started = true;
            return started;
        }

        /// <summary>
        /// Checks to see if the string starts with all of the values
        /// </summary>
        /// <param name="source">Target string</param>
        /// <param name="values">Values</param>
        public static bool StartsWithAllOf(this string source, string[] values)
        {
            if (source is null)
                throw new TextifyException("The target may not be null");

            List<string> done = [];
            foreach (string Value in values)
            {
                if (source.StartsWith(Value))
                    done.Add(Value);
            }
            return done.SequenceEqual(values);
        }

        /// <summary>
        /// Checks to see if the string ends with any of the values
        /// </summary>
        /// <param name="source">Target string</param>
        /// <param name="values">Values</param>
        public static bool EndsWithAnyOf(this string source, string[] values)
        {
            if (source is null)
                throw new TextifyException("The target may not be null");
            var Started = default(bool);
            foreach (string Value in values)
            {
                if (source.EndsWith(Value))
                    Started = true;
            }
            return Started;
        }

        /// <summary>
        /// Checks to see if the string ends with all of the values
        /// </summary>
        /// <param name="source">Target string</param>
        /// <param name="values">Values</param>
        public static bool EndsWithAllOf(this string source, string[] values)
        {
            if (source is null)
                throw new TextifyException("The target may not be null");

            List<string> done = [];
            foreach (string Value in values)
            {
                if (source.EndsWith(Value))
                    done.Add(Value);
            }
            return done.SequenceEqual(values);
        }

        /// <summary>
        /// Checks to see if the string contains any of the target strings.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="targets">Target strings</param>
        /// <returns>True if one of them is found; otherwise, false.</returns>
        public static bool ContainsAnyOf(this string source, string[] targets)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");

            foreach (string target in targets)
                if (source.Contains(target))
                    return true;
            return false;
        }

        /// <summary>
        /// Checks to see if the string contains all of the target strings.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="targets">Target strings</param>
        /// <returns>True if all of them are found; else, false.</returns>
        public static bool ContainsAllOf(this string source, string[] targets)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");

            List<string> done = [];
            foreach (string Target in targets)
            {
                if (source.Contains(Target))
                    done.Add(Target);
            }
            return done.SequenceEqual(targets);
        }

        /// <summary>
        /// Checks to see if the string starts with any of the values
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="values">Values</param>
        /// <returns>True if the string starts with any of the values specified in the array. Otherwise, false.</returns>
        public static bool StartsWithAnyOf(this string target, char[] values)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            bool started = false;
            foreach (char value in values)
                if (target.StartsWith($"{value}"))
                    started = true;
            return started;
        }

        /// <summary>
        /// Checks to see if the string starts with all of the values
        /// </summary>
        /// <param name="source">Target string</param>
        /// <param name="values">Values</param>
        public static bool StartsWithAllOf(this string source, char[] values)
        {
            if (source is null)
                throw new TextifyException("The target may not be null");

            List<char> done = [];
            foreach (char Value in values)
            {
                if (source.StartsWith($"{Value}"))
                    done.Add(Value);
            }
            return done.SequenceEqual(values);
        }

        /// <summary>
        /// Checks to see if the string ends with any of the values
        /// </summary>
        /// <param name="source">Target string</param>
        /// <param name="values">Values</param>
        public static bool EndsWithAnyOf(this string source, char[] values)
        {
            if (source is null)
                throw new TextifyException("The target may not be null");
            var Started = default(bool);
            foreach (char Value in values)
            {
                if (source.EndsWith($"{Value}"))
                    Started = true;
            }
            return Started;
        }

        /// <summary>
        /// Checks to see if the string ends with all of the values
        /// </summary>
        /// <param name="source">Target string</param>
        /// <param name="values">Values</param>
        public static bool EndsWithAllOf(this string source, char[] values)
        {
            if (source is null)
                throw new TextifyException("The target may not be null");

            List<char> done = [];
            foreach (char Value in values)
            {
                if (source.EndsWith($"{Value}"))
                    done.Add(Value);
            }
            return done.SequenceEqual(values);
        }

        /// <summary>
        /// Checks to see if the string contains any of the target strings.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="targets">Target strings</param>
        /// <returns>True if one of them is found; otherwise, false.</returns>
        public static bool ContainsAnyOf(this string source, char[] targets)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");

            foreach (char target in targets)
                if (source.Contains(target))
                    return true;
            return false;
        }

        /// <summary>
        /// Checks to see if the string contains all of the target strings.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="targets">Target strings</param>
        /// <returns>True if all of them are found; else, false.</returns>
        public static bool ContainsAllOf(this string source, char[] targets)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");

            List<char> done = [];
            foreach (char Target in targets)
            {
                if (source.Contains(Target))
                    done.Add(Target);
            }
            return done.SequenceEqual(targets);
        }

        /// <summary>
        /// Replaces all the instances of strings with a string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Strings to be replaced</param>
        /// <param name="toReplace">String to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReplaceAll(this string target, string[] toBeReplaced, string toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced strings may not be null");

            foreach (string ReplaceTarget in toBeReplaced)
                target = target.Replace(ReplaceTarget, toReplace);
            return target;
        }

        /// <summary>
        /// Replaces all the instances of strings with a string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Strings to be replaced</param>
        /// <param name="toReplace">Character to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReplaceAll(this string target, string[] toBeReplaced, char toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced strings may not be null");

            foreach (string ReplaceTarget in toBeReplaced)
                target = target.Replace(ReplaceTarget, $"{toReplace}");
            return target;
        }

        /// <summary>
        /// Replaces all the instances of strings with a string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Characters to be replaced</param>
        /// <param name="toReplace">String to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReplaceAll(this string target, char[] toBeReplaced, string toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced strings may not be null");

            foreach (char ReplaceTarget in toBeReplaced)
                target = target.Replace($"{ReplaceTarget}", toReplace);
            return target;
        }

        /// <summary>
        /// Replaces all the instances of strings with a string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Characters to be replaced</param>
        /// <param name="toReplace">Character to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReplaceAll(this string target, char[] toBeReplaced, char toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced strings may not be null");

            foreach (char ReplaceTarget in toBeReplaced)
                target = target.Replace(ReplaceTarget, toReplace);
            return target;
        }

        /// <summary>
        /// Replaces all the instances of strings with a string assigned to each entry
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Strings to be replaced</param>
        /// <param name="toReplace">Strings to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string ReplaceAllRange(this string target, string[] toBeReplaced, string[] toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced strings may not be null");
            if (toReplace is null || toReplace.Length == 0)
                throw new TextifyException("Array of to be replacement strings may not be null");
            if (toBeReplaced.Length != toReplace.Length)
                throw new TextifyException("Array length of which strings to be replaced doesn't equal the array length of which strings to replace.");

            for (int i = 0, loopTo = toBeReplaced.Length - 1; i <= loopTo; i++)
                target = target.Replace(toBeReplaced[i], toReplace[i]);
            return target;
        }

        /// <summary>
        /// Replaces all the instances of strings with a character assigned to each entry
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Strings to be replaced</param>
        /// <param name="toReplace">Characters to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string ReplaceAllRange(this string target, string[] toBeReplaced, char[] toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced strings may not be null");
            if (toReplace is null || toReplace.Length == 0)
                throw new TextifyException("Array of to be replacement characters may not be null");
            if (toBeReplaced.Length != toReplace.Length)
                throw new TextifyException("Array length of which strings to be replaced doesn't equal the array length of which characters to replace.");

            for (int i = 0, loopTo = toBeReplaced.Length - 1; i <= loopTo; i++)
                target = target.Replace(toBeReplaced[i], $"{toReplace[i]}");
            return target;
        }

        /// <summary>
        /// Replaces all the instances of characters with a string assigned to each entry
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Characters to be replaced</param>
        /// <param name="toReplace">Strings to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string ReplaceAllRange(this string target, char[] toBeReplaced, string[] toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced characters may not be null");
            if (toReplace is null || toReplace.Length == 0)
                throw new TextifyException("Array of to be replacement strings may not be null");
            if (toBeReplaced.Length != toReplace.Length)
                throw new TextifyException("Array length of which characters to be replaced doesn't equal the array length of which strings to replace.");

            for (int i = 0, loopTo = toBeReplaced.Length - 1; i <= loopTo; i++)
                target = target.Replace($"{toBeReplaced[i]}", toReplace[i]);
            return target;
        }

        /// <summary>
        /// Replaces all the instances of characters with a character assigned to each entry
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="toBeReplaced">Characters to be replaced</param>
        /// <param name="toReplace">Characters to replace with</param>
        /// <returns>Modified string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string ReplaceAllRange(this string target, char[] toBeReplaced, char[] toReplace)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (toBeReplaced is null || toBeReplaced.Length == 0)
                throw new TextifyException("Array of to be replaced characters may not be null");
            if (toReplace is null || toReplace.Length == 0)
                throw new TextifyException("Array of to be replacement characters may not be null");
            if (toBeReplaced.Length != toReplace.Length)
                throw new TextifyException("Array length of which characters to be replaced doesn't equal the array length of which strings to replace.");

            for (int i = 0, loopTo = toBeReplaced.Length - 1; i <= loopTo; i++)
                target = target.Replace(toBeReplaced[i], toReplace[i]);
            return target;
        }

        /// <summary>
        /// Replaces last occurrence of a text in source string with the replacement
        /// </summary>
        /// <param name="source">A string which has the specified text to replace</param>
        /// <param name="searchText">A string to be replaced</param>
        /// <param name="replace">A string to replace</param>
        /// <returns>String that has its last occurrence of text replaced</returns>
        public static string ReplaceLastOccurrence(this string source, string searchText, string replace)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");
            if (searchText is null)
                throw new TextifyException("The search text may not be null");

            int position = source.LastIndexOf(searchText);
            if (position == -1)
                return source;
            string result = source.Remove(position, searchText.Length).Insert(position, replace);
            return result;
        }

        /// <summary>
        /// Replaces last occurrence of a text in source string with the replacement
        /// </summary>
        /// <param name="source">A string which has the specified text to replace</param>
        /// <param name="searchText">A string to be replaced</param>
        /// <param name="replace">A character to replace</param>
        /// <returns>String that has its last occurrence of text replaced</returns>
        public static string ReplaceLastOccurrence(this string source, string searchText, char replace)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");
            if (searchText is null)
                throw new TextifyException("The search text may not be null");

            int position = source.LastIndexOf(searchText);
            if (position == -1)
                return source;
            string result = source.Remove(position, searchText.Length).Insert(position, $"{replace}");
            return result;
        }

        /// <summary>
        /// Replaces last occurrence of a text in source string with the replacement
        /// </summary>
        /// <param name="source">A string which has the specified text to replace</param>
        /// <param name="searchText">A character to be replaced</param>
        /// <param name="replace">A string to replace</param>
        /// <returns>String that has its last occurrence of text replaced</returns>
        public static string ReplaceLastOccurrence(this string source, char searchText, string replace)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");

            int position = source.LastIndexOf(searchText);
            if (position == -1)
                return source;
            string result = source.Remove(position, 1).Insert(position, replace);
            return result;
        }

        /// <summary>
        /// Replaces last occurrence of a text in source string with the replacement
        /// </summary>
        /// <param name="source">A string which has the specified text to replace</param>
        /// <param name="searchText">A character to be replaced</param>
        /// <param name="replace">A character to replace</param>
        /// <returns>String that has its last occurrence of text replaced</returns>
        public static string ReplaceLastOccurrence(this string source, char searchText, char replace)
        {
            if (source is null)
                throw new TextifyException("The source may not be null");

            int position = source.LastIndexOf(searchText);
            if (position == -1)
                return source;
            string result = source.Remove(position, 1).Insert(position, $"{replace}");
            return result;
        }

        /// <summary>
        /// Get all indexes of a value in string
        /// </summary>
        /// <param name="target">Source string</param>
        /// <param name="value">A value</param>
        /// <returns>Indexes of strings</returns>
        public static IEnumerable<int> AllIndexesOf(this string target, string value)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (string.IsNullOrEmpty(value))
                throw new TextifyException("Empty string value specified");

            int index = 0;
            while (true)
            {
                index = target.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
                index += value.Length;
            }
        }

        /// <summary>
        /// Get all indexes of a value in string
        /// </summary>
        /// <param name="target">Source string</param>
        /// <param name="value">A value</param>
        /// <returns>Indexes of strings</returns>
        public static IEnumerable<int> AllIndexesOf(this string target, char value)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            int index = 0;
            while (true)
            {
                index = target.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
                index++;
            }
        }

        /// <summary>
        /// Formats the string
        /// </summary>
        /// <param name="Format">The string to format</param>
        /// <param name="Vars">The variables used</param>
        /// <returns>A formatted string if successful, or the unformatted one if failed.</returns>
        public static string FormatString(this string Format, params object?[]? Vars)
        {
            if (Format is null)
                throw new TextifyException("The target format may not be null");
            if (Vars is null)
                throw new TextifyException("The target vars may not be null");

            string FormattedString = Format;
            try
            {
                if (Vars.Length > 0)
                    FormattedString = string.Format(Format, Vars);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to format string: {ex.Message}", nameof(FormatString));
                Debug.WriteLine(ex.StackTrace, nameof(FormatString));
            }
            return FormattedString;
        }

        /// <summary>
        /// Is the string numeric?
        /// </summary>
        /// <param name="Expression">The expression</param>
        public static bool IsStringNumeric(this string Expression)
        {
            if (string.IsNullOrWhiteSpace(Expression))
                throw new TextifyException("The target expression may not be null");

            return double.TryParse(Expression, out double _);
        }

        /// <summary>
		/// Adds a prefix from the text
		/// </summary>
        /// <param name="text">Target text</param>
        /// <param name="prefix">Prefix (that is, a string that marks the beginning of a string)</param>
        /// <param name="check">Verify the prefix before inserting</param>
        /// <returns>Modified string that contains a prefix before the string.</returns>
        /// <remarks>
        /// If the string contains a prefix that you've already put, and you're attempting to add the same prefix
        /// with checking turned on, the result will be the same as when you've passed in the target text.
        /// </remarks>
		public static string AddPrefix(this string text, string prefix, bool check = true)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            var builder = new StringBuilder(text);
            if ((check && !text.VerifyPrefix(prefix)) || !check)
                builder.Insert(0, prefix);
            return builder.ToString();
        }

        /// <summary>
        /// Adds a suffix from the text
        /// </summary>
        /// <param name="text">Target text</param>
        /// <param name="suffix">Suffix (that is, a string that marks the end of a string)</param>
        /// <param name="check">Verify the suffix before inserting</param>
        /// <returns>Modified string that contains a suffix after the string.</returns>
        /// <remarks>
        /// If the string contains a suffix that you've already put, and you're attempting to add the same suffix
        /// with checking turned on, the result will be the same as when you've passed in the target text.
        /// </remarks>
        public static string AddSuffix(this string text, string suffix, bool check = true)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            var builder = new StringBuilder(text);
            if ((check && !text.VerifySuffix(suffix)) || !check)
                builder.Append(suffix);
            return builder.ToString();
        }

        /// <summary>
		/// Removes a prefix from the text
		/// </summary>
        /// <param name="text">Target text</param>
        /// <param name="prefix">Prefix (that is, a string that marks the beginning of a string)</param>
        /// <returns>Modified string that doesn't contain a prefix before the string.</returns>
        /// <remarks>
        /// If the string doesn't contain a prefix that you want to remove, the end result will be unchanged.
        /// </remarks>
		public static string RemovePrefix(this string text, string prefix)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            if (text.StartsWith(prefix))
                return text.Substring(prefix.Length).Trim();
            return text;
        }

        /// <summary>
        /// Removes a suffix from the text
        /// </summary>
        /// <param name="text">Target text</param>
        /// <param name="suffix">Suffix (that is, a string that marks the end of a string)</param>
        /// <returns>Modified string that doesn't contain a suffix after the string.</returns>
        /// <remarks>
        /// If the string doesn't contain a suffix that you want to remove, the end result will be unchanged.
        /// </remarks>
        public static string RemoveSuffix(this string text, string suffix)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            if (text.EndsWith(suffix))
                return text.Substring(0, text.Length - suffix.Length);
            return text;
        }

        /// <summary>
		/// Verifies a prefix from the text
		/// </summary>
        /// <param name="text">Target text</param>
        /// <param name="prefix">Prefix (that is, a string that marks the beginning of a string)</param>
        /// <param name="comparison">String comparison rules for case sensitivity and for culture settings</param>
        /// <returns>True if the prefix is detected; false otherwise.</returns>
        /// <remarks>
        /// This function is just a wrapper for <see cref="string.StartsWith(string, StringComparison)"/>
        /// </remarks>
		public static bool VerifyPrefix(this string text, string prefix, StringComparison comparison = StringComparison.CurrentCulture)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            return text.StartsWith(prefix, comparison);
        }

        /// <summary>
        /// Verifies a suffix from the text
        /// </summary>
        /// <param name="text">Target text</param>
        /// <param name="suffix">Suffix (that is, a string that marks the end of a string)</param>
        /// <param name="comparison">String comparison rules for case sensitivity and for culture settings</param>
        /// <returns>True if the suffix is detected; false otherwise.</returns>
        /// <remarks>
        /// This function is just a wrapper for <see cref="string.EndsWith(string, StringComparison)"/>
        /// </remarks>
        public static bool VerifySuffix(this string text, string suffix, StringComparison comparison = StringComparison.CurrentCulture)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            return text.EndsWith(suffix, comparison);
        }

        /// <summary>
        /// Gets a BASE64-encoded string from the text
        /// </summary>
        /// <param name="text">Text to encode to BASE64</param>
        /// <returns>A BASE64-encoded string from the text</returns>
        public static string GetBase64Encoded(this string text)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            byte[] textBytes = Encoding.Default.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        /// <summary>
        /// Gets a BASE64-decoded string from the BASE64-encoded string
        /// </summary>
        /// <param name="text">Text to encode to BASE64</param>
        /// <returns>A BASE64-encoded string from the text</returns>
        public static string GetBase64Decoded(this string text)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            byte[] textBytes = Convert.FromBase64String(text);
            return Encoding.Default.GetString(textBytes);
        }

        /// <summary>
        /// Shifts the letters in a string
        /// </summary>
        /// <param name="text">Text to shift</param>
        /// <param name="shift">How many times to shift</param>
        /// <returns>A string containing shifted letters</returns>
        public static string ShiftLetters(this string text, int shift)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            // Get the character array
            char[] chars = text.ToCharArray();

            // Now, add each character with shift threshold, taking overflows into account
            while (shift < -255)
                shift += 255;
            while (shift > 255)
                shift -= 255;
            for (int i = 0; i < chars.Length; i++)
            {
                // Get a character and its integer value
                char character = chars[i];
                int charInt = character;

                // Add by shift threshold
                charInt += shift;
                while (charInt < -255)
                    charInt += 255;
                while (charInt > 255)
                    charInt -= 255;

                // Now, convert the final result to the character
                character = (char)charInt;
                chars[i] = character;
            }

            return string.Join("", chars);
        }

        /// <summary>
        /// Gets the wrapped sentences for text wrapping for console (with character wrapping, without VT sequence support)
        /// </summary>
        /// <param name="text">Text to be wrapped</param>
        /// <param name="maximumLength">Maximum length of text before wrapping</param>
        /// <remarks>If you want to be able to use the VT-sequence-enabled version, you must use Terminaux 3.0 or later.</remarks>
        public static string[] GetWrappedSentences(this string text, int maximumLength) =>
            GetWrappedSentences(text, maximumLength, 0);

        /// <summary>
        /// Gets the wrapped sentences for text wrapping for console (with character wrapping, without VT sequence support)
        /// </summary>
        /// <param name="text">Text to be wrapped</param>
        /// <param name="maximumLength">Maximum length of text before wrapping</param>
        /// <param name="indentLength">Indentation length</param>
        /// <remarks>If you want to be able to use the VT-sequence-enabled version, you must use Terminaux 3.0 or later.</remarks>
        public static string[] GetWrappedSentences(this string text, int maximumLength, int indentLength)
        {
            if (string.IsNullOrEmpty(text))
                return [""];

            // Split the paragraph into sentences that have the length of maximum characters that can be printed in various terminal
            // sizes.
            var IncompleteSentences = new List<string>();
            var IncompleteSentenceBuilder = new StringBuilder();

            // Make the text look like it came from Linux
            text = text.Replace(Convert.ToString(Convert.ToChar(13)), "");

            // Convert tabs to four spaces
            text = text.Replace("\t", "    ");

            // This indent length count tells us how many spaces are used for indenting the paragraph. This is only set for
            // the first time and will be reverted back to zero after the incomplete sentence is formed.
            foreach (string splitText in text.SplitNewLines())
            {
                int compensate = 0;
                if (splitText.Length == 0)
                    IncompleteSentences.Add(splitText);
                for (int i = 0; i < splitText.Length; i++)
                {
                    // Check the character to see if we're at the VT sequence
                    bool explicitNewLine = splitText[splitText.Length - 1] == '\n';
                    char ParagraphChar = splitText[i];
                    bool isNewLine = splitText[i] == '\n';

                    // Append the character into the incomplete sentence builder.
                    if (!isNewLine)
                        IncompleteSentenceBuilder.Append(ParagraphChar.ToString());

                    // Also, compensate the \0 characters
                    if (splitText[i] == '\0')
                        compensate++;

                    // Check to see if we're at the maximum character number or at the new line
                    if (IncompleteSentenceBuilder.Length == maximumLength - indentLength + compensate |
                        i == splitText.Length - 1 |
                        isNewLine)
                    {
                        // We're at the character number of maximum character. Add the sentence to the list for "wrapping" in columns.
                        IncompleteSentences.Add(IncompleteSentenceBuilder.ToString());
                        if (explicitNewLine)
                            IncompleteSentences.Add("");

                        // Clean everything up
                        IncompleteSentenceBuilder.Clear();
                        indentLength = 0;
                        compensate = 0;
                    }
                }
            }

            return [.. IncompleteSentences];
        }

        /// <summary>
        /// Gets the wrapped sentences for text wrapping for console (with word wrapping, without VT sequence support)
        /// </summary>
        /// <param name="text">Text to be wrapped</param>
        /// <param name="maximumLength">Maximum length of text before wrapping</param>
        /// <remarks>If you want to be able to use the VT-sequence-enabled version, you must use Terminaux 3.0 or later.</remarks>
        public static string[] GetWrappedSentencesByWords(this string text, int maximumLength) =>
            GetWrappedSentencesByWords(text, maximumLength, 0);

        /// <summary>
        /// Gets the wrapped sentences for text wrapping for console (with word wrapping, without VT sequence support)
        /// </summary>
        /// <param name="text">Text to be wrapped</param>
        /// <param name="maximumLength">Maximum length of text before wrapping</param>
        /// <param name="indentLength">Indentation length</param>
        /// <remarks>If you want to be able to use the VT-sequence-enabled version, you must use Terminaux 3.0 or later.</remarks>
        public static string[] GetWrappedSentencesByWords(this string text, int maximumLength, int indentLength)
        {
            if (string.IsNullOrEmpty(text))
                return [""];

            // Split the paragraph into sentences that have the length of maximum characters that can be printed in various terminal
            // sizes.
            var IncompleteSentences = new List<string>();
            var IncompleteSentenceBuilder = new StringBuilder();

            // Make the text look like it came from Linux
            text = text.Replace(Convert.ToString(Convert.ToChar(13)), "");

            // Convert tabs to four spaces
            text = text.Replace("\t", "    ");

            // This indent length count tells us how many spaces are used for indenting the paragraph. This is only set for
            // the first time and will be reverted back to zero after the incomplete sentence is formed.
            var lines = text.SplitNewLines();
            foreach (string splitText in lines)
            {
                int compensate = 0;
                if (splitText.Length == 0)
                {
                    IncompleteSentences.Add(splitText);
                    continue;
                }

                // Split the text by spaces
                var words = splitText.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];

                    // Compensate the \0 characters
                    for (int c = 0; c < word.Length; c++)
                        if (splitText[c] == '\0')
                            compensate++;

                    // Append the word into the incomplete sentence builder.
                    int finalMaximum = maximumLength - indentLength + compensate;
                    if (word.Length >= finalMaximum)
                    {
                        var charSplit = GetWrappedSentences(word, maximumLength, indentLength);
                        for (int splitIdx = 0; splitIdx < charSplit.Length - 1; splitIdx++)
                        {
                            string charSplitText = charSplit[splitIdx];

                            // We need to add character splits, except the last one.
                            if (IncompleteSentenceBuilder.Length > 0)
                                IncompleteSentences.Add(IncompleteSentenceBuilder.ToString());
                            IncompleteSentences.Add(charSplitText);

                            // Clean everything up
                            IncompleteSentenceBuilder.Clear();
                            indentLength = 0;
                            compensate = 0;
                        }

                        // Process the character split last text
                        string charSplitLastText = charSplit[charSplit.Length - 1];
                        word = charSplitLastText;
                        finalMaximum = maximumLength - indentLength + compensate;
                        IncompleteSentenceBuilder.Append(charSplitLastText);
                    }
                    else if (IncompleteSentenceBuilder.Length + word.Length < finalMaximum)
                        IncompleteSentenceBuilder.Append(word);

                    // Check to see if we're at the maximum length
                    int nextWord = i + 1 >= words.Length ? 1 : words[i + 1].Length + 1;
                    if (IncompleteSentenceBuilder.Length + nextWord >= finalMaximum)
                    {
                        // We're at the character number of maximum character. Add the sentence to the list for "wrapping" in columns.
                        IncompleteSentences.Add(IncompleteSentenceBuilder.ToString());

                        // Clean everything up
                        IncompleteSentenceBuilder.Clear();
                        indentLength = 0;
                        compensate = 0;
                    }
                    else
                        IncompleteSentenceBuilder.Append(IncompleteSentenceBuilder.Length + nextWord >= finalMaximum || i + 1 >= words.Length ? "" : " ");
                }
                if (IncompleteSentenceBuilder.Length > 0)
                    IncompleteSentences.Add(IncompleteSentenceBuilder.ToString());
                IncompleteSentenceBuilder.Clear();
            }

            return [.. IncompleteSentences];
        }

        /// <summary>
        /// Truncates the string if the string is larger than the threshold, otherwise, returns an unchanged string (without VT sequence support)
        /// </summary>
        /// <param name="target">Source string to truncate</param>
        /// <param name="threshold">Max number of string characters</param>
        /// <returns>Truncated string</returns>
        /// <remarks>If you want to be able to use the VT-sequence-enabled version, you must use Terminaux 3.0 or later.</remarks>
        public static string TruncateString(this string target, int threshold)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (threshold < 0)
                threshold = 0;
            if (threshold == 0)
                return "";

            // Try to truncate string. If the string length is bigger than the threshold, it'll be truncated to the length of
            // the threshold, putting three dots next to it. We don't use ellipsis marks here because we might use this
            // function with the console.
            if (target.Length > threshold)
                return target.Substring(0, threshold) + "...";
            else
                return target;
        }

        /// <summary>
        /// Reverses the order of characters in a string
        /// </summary>
        /// <param name="target">Target string</param>
        public static string Reverse(this string target)
        {
            if (string.IsNullOrEmpty(target))
                return "";

            // Now, get lines, because we may have been provided multi-line strings
            StringBuilder builder = new();
            var lines = target.SplitNewLines();
            for (int l = 0; l < lines.Length; l++)
            {
                string line = lines[l];
                for (int lc = line.Length - 1; lc >= 0; lc--)
                {
                    char c = line[lc];
                    builder.Append(c);
                }
                if (l + 1 < lines.Length)
                    builder.AppendLine();
            }

            // Return the result
            return builder.ToString();
        }

        /// <summary>
        /// Makes the first character of the string uppercase
        /// </summary>
        /// <param name="target">The target string</param>
        /// <returns>A string that starts with the capital letter</returns>
        public static string UpperFirst(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            char[] chars = target.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return string.Join("", chars);
        }

        /// <summary>
        /// Makes the first character of the string lowercase
        /// </summary>
        /// <param name="target">The target string</param>
        /// <returns>A string that starts with the small letter</returns>
        public static string LowerFirst(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            char[] chars = target.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return string.Join("", chars);
        }

        /// <summary>
        /// Title cases the string
        /// </summary>
        /// <param name="target">String to convert to title case</param>
        /// <returns>The string that has title case</returns>
        public static string ToTitleCase(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            string[] exclusions = ["of", "the", "a", "an", "in", "on", "to", "from"];

            // Split the string to words and make them the titlecase
            string[] words = target.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                // If the word isn't in the exclusions list, uppercase the first character
                if (!exclusions.Contains(word))
                    words[i] = word.UpperFirst();
            }

            // Form a final string
            return string.Join(" ", words);
        }

        /// <summary>
        /// Gets enclosed word from index that represents a start of a substring
        /// </summary>
        /// <param name="target">Target string that contains a substring</param>
        /// <param name="sourceIdx">Target string index that usually starts a substring</param>
        /// <param name="includeSymbols">Whether to include symbols, such as punctuation, in the search or not</param>
        /// <returns>The enclosed word from the specified index</returns>
        public static string GetEnclosedWordFromIndex(this string target, int sourceIdx, bool includeSymbols = false)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            // Calculate the distance between two word spaces
            int distance = 0;
            int idx = GetIndexOfEnclosedWordFromIndex(target, sourceIdx, includeSymbols);
            if (idx < 0)
                return "";
            for (int distanceIdx = idx; distanceIdx < target.Length; distanceIdx++)
            {
                var character = target[distanceIdx];
                if ((!includeSymbols && !char.IsLetterOrDigit(character)) || (includeSymbols && char.IsWhiteSpace(character)))
                    break;
                distance++;
            }

            // Now, get a word from these two values.
            string word = target.Substring(idx, distance);
            return word;
        }

        /// <summary>
        /// Gets a string containing escaped characters
        /// </summary>
        /// <param name="target">Target string to escape characters</param>
        /// <returns>A string containing escaped characters</returns>
        public static string Escape(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            // Escape characters now.
            return target.ReplaceAllRange(unescaped, escaped);
        }

        /// <summary>
        /// Gets a string containing unescaped characters
        /// </summary>
        /// <param name="target">Target string to unescape characters</param>
        /// <returns>A string containing unescaped characters</returns>
        public static string Unescape(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            // Unescape characters now.
            return target.ReplaceAllRange(escaped, unescaped);
        }

        /// <summary>
        /// Gets the index of the enclosed word from index that represents a start of a substring
        /// </summary>
        /// <param name="target">Target string that contains a substring</param>
        /// <param name="sourceIdx">Target string index that usually starts a substring</param>
        /// <param name="includeSymbols">Whether to include symbols, such as punctuation, in the search or not</param>
        /// <returns>The index of the enclosed enclosed word from the specified index, or -1 if the string is empty</returns>
        public static int GetIndexOfEnclosedWordFromIndex(this string target, int sourceIdx, bool includeSymbols = false)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (string.IsNullOrEmpty(target))
                return -1;

            // Calculate the word begin index
            int idx;
            for (idx = sourceIdx; idx >= 0; idx--)
            {
                var character = target[idx];
                if ((!includeSymbols && !char.IsLetterOrDigit(character)) || (includeSymbols && char.IsWhiteSpace(character)))
                {
                    idx++;
                    break;
                }
            }
            if (idx < 0)
                idx = 0;
            return idx;
        }

        /// <summary>
        /// Gets the letter repetition pattern (LRP) that determines how many times the program needs to step "<paramref name="steps"/>" times on the string until it reaches the end of the string.
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="steps">Number of steps</param>
        public static int GetLetterRepetitionPattern(this string target, int steps)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (steps <= 0)
                throw new TextifyException("Can't get the letter repetition pattern with zero or negative number of steps.");
            if (target.Length == 0)
                return 0;

            // Some variables
            int length = target.Length;
            int lastPosition = 0;
            int repeatTimes = 0;

            // The LRP formula requires a loop
            while (true)
            {
                // Loop through the number of steps with the last position
                int step = 0;
                for (int i = lastPosition; i <= steps + lastPosition; i++)
                {
                    step = i;

                    // Check to see if the number of steps exceeds the string length
                    while (step > length)
                        step -= length;
                }

                // Now, update the last position and increment the repeat times by one
                lastPosition = step;
                repeatTimes += 1;

                // If we're at the end of the string, return the repeat times
                if (lastPosition == length)
                    return repeatTimes;
            }
        }

        /// <summary>
        /// Gets the letter repetition pattern (LRP) table from the length of the string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="twice">Whether to add the LRP of the string using the number of steps up to twice the string length or not</param>
        public static ReadOnlyDictionary<int, int> GetLetterRepetitionPatternTable(this string target, bool twice = false) =>
            GetLetterRepetitionPatternTable(target, twice ? 2 : 1);

        /// <summary>
        /// Gets the letter repetition pattern (LRP) table from the length of the string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="iterations">Number of iterations</param>
        public static ReadOnlyDictionary<int, int> GetLetterRepetitionPatternTable(this string target, int iterations)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (target.Length == 0 || iterations <= 0)
                return new(new Dictionary<int, int>());

            // Now, iterate through the target length
            Dictionary<int, int> lrpTable = [];
            int targetLength = target.Length * iterations;
            for (int i = 1; i <= targetLength; i++)
            {
                int lrp = target.GetLetterRepetitionPattern(i);
                lrpTable.Add(i, lrp);
            }

            // Return the resulting dictionary
            return new(lrpTable);
        }

        /// <summary>
        /// Gets the list of repeated letters, including those that have occurred only once
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="removeSingle">Whether to remove all characters that only contain a single occurrence or not</param>
        public static ReadOnlyDictionary<char, int> GetListOfRepeatedLetters(this string target, bool removeSingle = false)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");
            if (target.Length == 0)
                return new(new Dictionary<char, int>());

            // Add repeated letters to the list
            var letters = new Dictionary<char, int>();
            foreach (char chr in target)
            {
                if (letters.ContainsKey(chr))
                    letters[chr]++;
                else
                    letters.Add(chr, 1);
            }

            // Remove a letter that only occurred once
            if (removeSingle)
            {
                for (int i = letters.Count - 1; i >= 0; i--)
                {
                    char character = letters.Keys.ElementAt(i);
                    int repetitions = letters[character];
                    if (repetitions == 1)
                        letters.Remove(character);
                }
            }

            // Return the resulting dictionary
            return new(letters);
        }

        /// <summary>
        /// Logically compares the two strings ignoring the cases while treating digits (localized or not) as numbers and text as strings.
        /// </summary>
        /// <param name="source">Source text to compare</param>
        /// <param name="compare">Comparison text</param>
        /// <returns>1 if left, -1 if right, 0 if equal</returns>
        public static int CompareLogical(string source, string compare)
        {
            string s1 = source;
            string s2 = compare;
            if (s1 == null || s2 == null)
                return 0;

            // Initialize markers
            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                // Get characters
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];
                char[] space1 = new char[len1];
                char[] space2 = new char[len2];
                int loc1 = 0;
                int loc2 = 0;

                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                        ch1 = s1[marker1];
                    else
                        break;
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                        ch2 = s2[marker2];
                    else
                        break;
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                string str1 = new(space1);
                string str2 = new(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                    result = str1.CompareTo(str2);

                if (result != 0)
                    return result;
            }
            return len1 - len2;
        }

        /// <summary>
        /// Reads a string until the null character is seen with the specified offset
        /// </summary>
        /// <param name="source">Source text to parse</param>
        /// <param name="offset">Zero-based offset (from where do we start reading?)</param>
        /// <returns>A read string</returns>
        public static string ReadNullTerminatedString(this string source, int offset)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(source))
                return "";
            if (offset < 0 || offset >= source.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "Offset is out of range");
            if (source[offset] == '\0')
                return "";

            // Check to see if there is a null terminator
            source = source.Substring(offset);
            int nullIdx = source.IndexOf('\0');
            if (nullIdx == -1)
                return source;

            // Now, read until the first null terminator found
            return source.Substring(0, nullIdx);
        }

        /// <summary>
        /// Gets the character width
        /// </summary>
        /// <param name="c">A character number (codepoint) to parse</param>
        /// <returns>Either 0 for non-printing characters, 1 for half-width characters, or 2 for full-width characters</returns>
        /// <exception cref="TextifyException"></exception>
        public static int GetCharWidth(int c)
        {
            // Check the value
            if (c < 0 || c > 0x10FFFF)
                throw new TextifyException($"Invalid character number {c}.");

            // Check the cached width
            if (cachedWidths.ContainsKey(c))
                return cachedWidths[c].Item1;

            // Use the character cell table defined in a separate code class to be able to determine the width
            int width = 1;
            CharWidthType widthType = (CharWidthType)(-1);
            bool cacheable = true;
            foreach ((var range, int cells, CharWidthType type) in CharWidths.ranges)
            {
                // Check for each range if we have a Unicode character that falls under one of the characters that take
                // up either no cells or more than one cell.
                foreach ((int first, int last) in range)
                {
                    if (c >= first && c <= last)
                    {
                        widthType = type;
                        width = cells;
                        break;
                    }
                }
                if (width == 1)
                    continue;

                // Now, check the value of the width
                switch (width)
                {
                    case -1:
                        // Unassigned character. This way, we need to let users select how to handle it by giving them a property
                        // that allows them to set either one (default) or two cells to be taken.
                        width = UseTwoCellsForUnassignedChars ? 2 : 1;
                        cacheable = false;
                        break;
                    case -2:
                        // Ambiguous character. See above.
                        width = UseTwoCellsForAmbiguousChars ? 2 : 1;
                        cacheable = false;
                        break;
                    case -3:
                        // Private character. See above.
                        width = UseTwoCellsForPrivateChars ? 2 : 1;
                        cacheable = false;
                        break;
                }
                break;
            }

            // Add to cache if we can
            if (cacheable)
            {
                // To ensure that we don't have any race condition, lock here and check.
                lock (cachedWidths)
                {
                    if (!cachedWidths.ContainsKey(c))
                        cachedWidths.Add(c, (width, widthType));
                }
            }
            return width;
        }

        /// <summary>
        /// Gets the character width type
        /// </summary>
        /// <param name="c">A character number (codepoint) to parse</param>
        /// <returns>Character width type</returns>
        /// <exception cref="TextifyException"></exception>
        public static CharWidthType GetCharWidthType(int c)
        {
            // Check the value
            if (c < 0 || c > 0x10FFFF)
                throw new TextifyException($"Invalid character number {c}.");

            // Check the cached width
            if (cachedWidths.ContainsKey(c))
                return cachedWidths[c].Item2;

            // Use the character cell table defined in a separate code class to be able to determine the width type
            int width = 1;
            CharWidthType widthType = (CharWidthType)(-1);
            foreach ((var range, int cells, CharWidthType type) in CharWidths.ranges)
            {
                // Check for each range if we have a Unicode character that falls under one of the characters that take
                // up either no cells or more than one cell.
                foreach ((int first, int last) in range)
                {
                    if (c >= first && c <= last)
                    {
                        widthType = type;
                        width = cells;
                        break;
                    }
                }
                if (width == 1)
                    continue;
                break;
            }

            // Add to cache if we can
            bool cacheable = width >= 0;
            if (cacheable)
            {
                // To ensure that we don't have any race condition, lock here and check.
                lock (cachedWidths)
                {
                    if (!cachedWidths.ContainsKey(c))
                        cachedWidths.Add(c, (width, widthType));
                }
            }
            return widthType;
        }

        /// <summary>
        /// Checks whether the strings equal (case-insensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <param name="comparison">Comparison type (must be case-insensitive)</param>
        /// <returns>True if both strings equal; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EqualsNoCase(this string source, string target, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (comparison != StringComparison.CurrentCultureIgnoreCase &&
                comparison != StringComparison.InvariantCultureIgnoreCase &&
                comparison != StringComparison.OrdinalIgnoreCase)
                throw new ArgumentException($"Comparison {comparison} not valid for case-insensitive string comparison");
            return string.Equals(source, target, comparison);
        }

        /// <summary>
        /// Checks whether the strings equal (case-sensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <param name="comparison">Comparison type (must be case-sensitive)</param>
        /// <returns>True if both strings equal; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EqualsCase(this string source, string target, StringComparison comparison = StringComparison.Ordinal)
        {
            if (comparison != StringComparison.CurrentCulture &&
                comparison != StringComparison.InvariantCulture &&
                comparison != StringComparison.Ordinal)
                throw new ArgumentException($"Comparison {comparison} not valid for case-sensitive string comparison");
            return string.Equals(source, target, comparison);
        }

        /// <summary>
        /// Checks whether the string starts with the specific target string (case-insensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <param name="comparison">Comparison type (must be case-insensitive)</param>
        /// <returns>True if the source string starts with the target substring; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool StartsWithNoCase(this string source, string target, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (comparison != StringComparison.CurrentCultureIgnoreCase &&
                comparison != StringComparison.InvariantCultureIgnoreCase &&
                comparison != StringComparison.OrdinalIgnoreCase)
                throw new ArgumentException($"Comparison {comparison} not valid for case-insensitive string comparison");
            return source.StartsWith(target, comparison);
        }

        /// <summary>
        /// Checks whether the string starts with the specific target string (case-sensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <param name="comparison">Comparison type (must be case-sensitive)</param>
        /// <returns>True if the source string starts with the target substring; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool StartsWithCase(this string source, string target, StringComparison comparison = StringComparison.Ordinal)
        {
            if (comparison != StringComparison.CurrentCulture &&
                comparison != StringComparison.InvariantCulture &&
                comparison != StringComparison.Ordinal)
                throw new ArgumentException($"Comparison {comparison} not valid for case-sensitive string comparison");
            return source.StartsWith(target, comparison);
        }

        /// <summary>
        /// Checks whether the string ends with the specific target string (case-insensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <param name="comparison">Comparison type (must be case-insensitive)</param>
        /// <returns>True if the source string ends with the target substring; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EndsWithNoCase(this string source, string target, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (comparison != StringComparison.CurrentCultureIgnoreCase &&
                comparison != StringComparison.InvariantCultureIgnoreCase &&
                comparison != StringComparison.OrdinalIgnoreCase)
                throw new ArgumentException($"Comparison {comparison} not valid for case-insensitive string comparison");
            return source.EndsWith(target, comparison);
        }

        /// <summary>
        /// Checks whether the string ends with the specific target string (case-sensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <param name="comparison">Comparison type (must be case-sensitive)</param>
        /// <returns>True if the source string ends with the target substring; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EndsWithCase(this string source, string target, StringComparison comparison = StringComparison.Ordinal)
        {
            if (comparison != StringComparison.CurrentCulture &&
                comparison != StringComparison.InvariantCulture &&
                comparison != StringComparison.Ordinal)
                throw new ArgumentException($"Comparison {comparison} not valid for case-sensitive string comparison");
            return source.EndsWith(target, comparison);
        }

        /// <summary>
        /// Checks whether the string contains a specific target string (case-insensitive)
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="target">Target string to compare</param>
        /// <returns>True if the source string contains a target substring; false otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool ContainsWithNoCase(this string source, string target) =>
            source.ToLower().Contains(target.ToLower());

        /// <summary>
        /// Orders the string logically (alphanumerically) in an ascending order
        /// </summary>
        /// <param name="source">Source array of which it contains a string</param>
        /// <returns>An array of logically sorted strings in an ascending order</returns>
        public static string[] OrderLogically(this string[] source) =>
            [.. OrderLogically(source as IEnumerable<string>)];

        /// <summary>
        /// Orders the string logically (alphanumerically) in a descending order
        /// </summary>
        /// <param name="source">Source array of which it contains a string</param>
        /// <returns>An array of logically sorted strings in a descending order</returns>
        public static string[] OrderDescendLogically(this string[] source) =>
            [.. OrderDescendLogically(source as IEnumerable<string>)];

        /// <summary>
        /// Orders the string logically (alphanumerically) in an ascending order
        /// </summary>
        /// <param name="source">Source array of which it contains a string</param>
        /// <returns>An array of logically sorted strings in an ascending order</returns>
        public static IEnumerable<string> OrderLogically(this IEnumerable<string> source) =>
            source.OrderBy(x => x, new LogicalComparer()).AsEnumerable();

        /// <summary>
        /// Orders the string logically (alphanumerically) in a descending order
        /// </summary>
        /// <param name="source">Source array of which it contains a string</param>
        /// <returns>An array of logically sorted strings in a descending order</returns>
        public static IEnumerable<string> OrderDescendLogically(this IEnumerable<string> source) =>
            source.OrderByDescending(x => x, new LogicalComparer()).AsEnumerable();

        /// <summary>
        /// Checks to see if this string is a palindrome (madam) or not (alarm) and that it can form a mirror or not. This is a case-insensitive operation.
        /// </summary>
        /// <param name="target">Target string to process</param>
        /// <param name="caseSensitive">Whether to process in case sensitive manner</param>
        /// <returns>True if this string is a palindrome; false otherwise. Empty strings are not palindromes.</returns>
        public static bool IsPalindrome(this string target, bool caseSensitive = false)
        {
            if (string.IsNullOrEmpty(target))
                return false;

            // Lowercase the string to make comparison case insensitive
            if (!caseSensitive)
                target = target.ToLower();

            // Get the start and the end index
            int leftMarker = 0;
            int rightMarker = target.Length - 1;
            while (leftMarker < rightMarker)
            {
                char leftChar = target[leftMarker];
                char rightChar = target[rightMarker];

                // Check for palindrome
                if (leftChar != rightChar)
                    return false;

                leftMarker++;
                rightMarker--;
            }

            // It's a palindrome
            return true;
        }

        /// <summary>
        /// Replaces a single character in a string with a new character
        /// </summary>
        /// <param name="source">Source string to process</param>
        /// <param name="idx">Index of a character in which replacement is done</param>
        /// <param name="replacement">Replacement character</param>
        /// <returns>A modified string</returns>
        /// <exception cref="Exception"></exception>
        public static string ReplaceChar(this string source, int idx, char replacement)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(source))
                return "";
            if (idx < 0)
                throw new Exception($"Specified index [{idx}] may not be less than zero");
            if (idx >= source.Length)
                throw new Exception($"Specified index [{idx}] may not be larger than the source string length [{source.Length}]");

            // Replace the character in a specified index
            char[] chars = source.ToCharArray();
            chars[idx] = replacement;
            return new(chars);
        }
    }
}
