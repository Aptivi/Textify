//
// Terminaux  Copyright (C) 2023-2024  Aptivi
//
// This file is part of Terminaux
//
// Terminaux is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Terminaux is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Textify.General;

namespace Textify.Tools
{
    /// <summary>
    /// Regular expression tools
    /// </summary>
    public static class RegexTools
    {
        private static string[] escaped = [@"\", @"*", @"+", @"?", @"|", @"{", @"[", @"(", @")", @"^", @"$", @".", @"#", @" ", @"-", @"""", @"'", @"`"];
        private static string[] unescaped = [@"\\", @"\*", @"\+", @"\?", @"\|", @"\{", @"\[", @"\(", @"\)", @"\^", @"\$", @"\.", @"\#", @"\ ", @"\-", @"\""", @"\'", @"\`"];

        /// <summary>
        /// Determines whether the specified regular expression pattern is valid or not
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool IsValidRegex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern) =>
            IsValidRegex(new Regex(pattern));

        /// <summary>
        /// Determines whether the specified regular expression pattern is valid or not
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool IsValidRegex(Regex pattern)
        {
            try
            {
                pattern.Match(string.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Parses the regular expression pattern after validating it
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <returns>Regular expression instance of a valid regular expression pattern</returns>
        public static Regex ParseRegex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (!IsValidRegex(pattern))
                throw new ArgumentException($"Regular expression pattern is invalid. [{pattern}]");
            return new Regex(pattern);
        }

        /// <summary>
        /// Parses the regular expression pattern after validating it
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <param name="result">[<see langword="out"/>] Resultant regular expression instance</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool TryParseRegex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern, out Regex? result)
        {
            try
            {
                result = ParseRegex(pattern);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Checks to see whether the pattern matches
        /// </summary>
        /// <param name="text">The text to be matched</param>
        /// <param name="pattern">Regular expression pattern for matching</param>
        /// <returns>True if there are matches. Otherwise, false</returns>
        public static bool IsMatch(string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (!IsValidRegex(pattern))
                return false;

            return new Regex(pattern).IsMatch(text);
        }

        /// <summary>
        /// Matches the pattern with the text given
        /// </summary>
        /// <param name="text">The text to be matched</param>
        /// <param name="pattern">Regular expression pattern for matching</param>
        /// <returns>A <see cref="System.Text.RegularExpressions.Match"/> that contains information about the current match</returns>
        public static Match Match(string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (!IsValidRegex(pattern))
                throw new ArgumentException("Invalid regular expression syntax.");

            return new Regex(pattern).Match(text);
        }

        /// <summary>
        /// Matches the pattern with the text given
        /// </summary>
        /// <param name="text">The text to be matched</param>
        /// <param name="pattern">Regular expression pattern for matching</param>
        /// <returns>Collection of <see cref="System.Text.RegularExpressions.Match"/>es that contains information about the current match</returns>
        public static MatchCollection Matches(string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (!IsValidRegex(pattern))
                throw new ArgumentException("Invalid regular expression syntax.");

            return new Regex(pattern).Matches(text);
        }

        /// <summary>
        /// Filters the string from the substrings matched by the given pattern
        /// </summary>
        /// <param name="text">The text to be processed</param>
        /// <param name="pattern">Regular expression pattern for replacing</param>
        /// <returns>Filtered text</returns>
        public static string Filter(string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern) =>
            Filter(text, pattern, "");

        /// <summary>
        /// Filters the string from the substrings matched by the given pattern
        /// </summary>
        /// <param name="text">The text to be processed</param>
        /// <param name="pattern">Regular expression pattern for replacing</param>
        /// <param name="replaceWith">Replaces the matched substrings with the specified text</param>
        /// <returns>Filtered text</returns>
        public static string Filter(string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, string replaceWith)
        {
            if (!IsValidRegex(pattern))
                throw new ArgumentException("Invalid regular expression syntax.");

            return new Regex(pattern).Replace(text, replaceWith);
        }

        /// <summary>
        /// Splits the string using the matched substrings as the delimiters
        /// </summary>
        /// <param name="text">The text to be split</param>
        /// <param name="pattern">Regular expression pattern for splitting</param>
        /// <returns>Array of strings</returns>
        public static string[] Split(string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (!IsValidRegex(pattern))
                throw new ArgumentException("Invalid regular expression syntax.");

            return new Regex(pattern).Split(text);
        }

        /// <summary>
        /// Escapes the invalid characters from the string
        /// </summary>
        /// <param name="text">The text containing invalid characters to escape</param>
        /// <returns>Escaped string</returns>
        public static string Escape(string text) =>
            text.ReplaceAllRange(escaped, unescaped);

        /// <summary>
        /// Unescapes the escaped characters from the string
        /// </summary>
        /// <param name="text">The text containing escaped characters to unescape</param>
        /// <returns>Unescaped string</returns>
        public static string Unescape(string text) =>
            text.ReplaceAllRange(unescaped, escaped);
    }
}
