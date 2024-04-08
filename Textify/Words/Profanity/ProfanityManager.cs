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

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text.RegularExpressions;
using Textify.General;

namespace Textify.Words.Profanity
{
    /// <summary>
    /// Profanity management class
    /// </summary>
    public static class ProfanityManager
    {
        private static Regex thoroughProfanityMatcher;
        private static Regex shallowProfanityMatcher;

        /// <summary>
        /// Gets a list of profane words in a sentence
        /// </summary>
        /// <param name="sentence">A sentence which may contain profanity</param>
        /// <param name="thorough">Whether to analyze the sentence thoroughly or not</param>
        /// <returns>An array of profanity occurrence info</returns>
        public static ProfanityOccurrenceInfo[] GetProfanities(string sentence, bool thorough = false)
        {
            // Initialize the profanity matchers
            InitializeMatchers();

            // Now, try to match profanity
            var matcher = thorough ? thoroughProfanityMatcher : shallowProfanityMatcher;
            var matches = matcher.Matches(sentence);
            List<ProfanityOccurrenceInfo> occurrences = [];
            foreach (Match match in matches)
            {
                var info = new ProfanityOccurrenceInfo(thorough, match.Groups[0].Value, match.Value);
                occurrences.Add(info);
            }
            return [.. occurrences];
        }

        private static void InitializeMatchers()
        {
            if (thoroughProfanityMatcher is not null && shallowProfanityMatcher is not null)
                return;

            // Get the profanities and make two matchers: thorough and shallow.
            string[] profanities = WordManager.GetWords(WordDataType.BadWords);
            string[] escaped = [@"\\", @"\*", @"\+", @"\?", @"\|", @"\{", @"\[", @"\(", @"\)", @"\^", @"\$", @"\.", @"\#", @"\ ", @"\-", @"\""", @"\'", @"\`", @"\!"];
            string[] unescaped = [@"\", @"*", @"+", @"?", @"|", @"{", @"[", @"(", @")", @"^", @"$", @".", @"#", @" ", @"-", @"""", @"'", @"`", @"!"];
            string thoroughPattern = string.Join("|", profanities.Select(word => string.Join(@"\s*", word.ToCharArray().Select((ch) => $"{ch}".ReplaceAllRange(unescaped, escaped)))));
            string shallowPattern = string.Join(@"\b|\b", profanities.Select(word => string.Join("", word.ToCharArray().Select((ch) => $"{ch}".ReplaceAllRange(unescaped, escaped)))));
            thoroughProfanityMatcher = new Regex(
                @"\b(" + thoroughPattern + @")\b", RegexOptions.IgnoreCase);
            shallowProfanityMatcher = new Regex(
                @"(\b" + shallowPattern + @"\b)", RegexOptions.IgnoreCase);
        }
    }
}
