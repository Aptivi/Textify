﻿//
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Textify.Tools;

namespace Textify.General
{
    /// <summary>
    /// Tools for text manipulation
    /// </summary>
    public static class TextTools
    {
        private static readonly string regexMatchEnclosedStrings = /* lang=regex */
            @"(""(.+?)(?<![^\\]\\)"")|('(.+?)(?<![^\\]\\)')|(`(.+?)(?<![^\\]\\)`)|(?:[^\\\s]|\\.)+|\S+";
        private static readonly string[] escaped = [@"\\", @"\*", @"\+", @"\?", @"\|", @"\{", @"\[", @"\(", @"\)", @"\^", @"\$", @"\.", @"\#", @"\ ", @"\-", @"\""", @"\'", @"\`", @"\!"];
        private static readonly string[] unescaped = [@"\", @"*", @"+", @"?", @"|", @"{", @"[", @"(", @")", @"^", @"$", @".", @"#", @" ", @"-", @"""", @"'", @"`", @"!"];

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula
        /// </summary>
        /// <param name="target">Target string</param>
        public static string[] SplitEncloseDoubleQuotes(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            var matches = Regex.Matches(target, regexMatchEnclosedStrings);
            var matchList = new List<Match>();
            foreach (Match match in matches)
                matchList.Add(match);
            return matchList
                .Select((m) => m.Value.ReleaseDoubleQuotes())
                .ToArray();
        }

        /// <summary>
        /// Splits the string enclosed in double quotes delimited by spaces using regular expression formula without releasing double quotes
        /// </summary>
        /// <param name="target">Target string</param>
        public static string[] SplitEncloseDoubleQuotesNoRelease(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            var matches = Regex.Matches(target, regexMatchEnclosedStrings);
            var matchList = new List<Match>();
            foreach (Match match in matches)
                matchList.Add(match);
            return matchList
                .Select((m) => m.Value)
                .ToArray();
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

            var result = target
                .Replace($"{Convert.ToChar(13)}{Convert.ToChar(10)}", $"{Convert.ToChar(10)}")
                .Replace($"{Convert.ToChar(13)}", $"{Convert.ToChar(10)}")
                .Split(Convert.ToChar(10));
            if (!emptyStrings)
                result = result.Where((str) => !string.IsNullOrEmpty(str)).ToArray();
            return result;
        }

        /// <summary>
        /// Makes a string array with new line as delimiter (the old way)
        /// </summary>
        /// <param name="target">Target string</param>
        /// <returns>List of words that are separated by the new lines</returns>
        public static string[] SplitNewLinesOld(this string target)
        {
            if (target is null)
                throw new TextifyException("The target may not be null");

            return target
                .Replace(Convert.ToChar(13).ToString(), "")
                .Split(Convert.ToChar(10));
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
        /// Formats the string
        /// </summary>
        /// <param name="Format">The string to format</param>
        /// <param name="Vars">The variables used</param>
        /// <returns>A formatted string if successful, or the unformatted one if failed.</returns>
        public static string FormatString(this string Format, params object[] Vars)
        {
            if (Format is null)
                throw new TextifyException("The target format may not be null");

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
		/// Removes a prefix from the text
		/// </summary>
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
        public static string RemoveSuffix(this string text, string suffix)
        {
            if (text is null)
                throw new TextifyException("The target may not be null");

            if (text.EndsWith(suffix))
                return text.Substring(0, text.Length - suffix.Length);
            return text;
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
            int distance = 0, idx;
            for (idx = sourceIdx; idx > 0; idx--)
            {
                var character = target[idx];
                if ((!includeSymbols && !char.IsLetterOrDigit(character)) || (includeSymbols && char.IsWhiteSpace(character)))
                {
                    idx++;
                    break;
                }
            }
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
    }
}
