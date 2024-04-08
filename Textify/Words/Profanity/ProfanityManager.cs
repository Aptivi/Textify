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
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
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
        private static Regex partialProfanityMatcher;
        private static Regex shallowProfanityMatcher;

        /// <summary>
        /// Gets a list of profane words in a sentence
        /// </summary>
        /// <param name="sentence">A sentence which may contain profanity</param>
        /// <param name="searchType">Specifies how to analyze the sentence</param>
        /// <returns>An array of profanity occurrence info</returns>
        public static ProfanityOccurrenceInfo[] GetProfanities(string sentence, ProfanitySearchType searchType = ProfanitySearchType.Shallow)
        {
            // Initialize the profanity matchers
            InitializeMatchers();

            // Now, try to match profanity
            var matcher =
                searchType == ProfanitySearchType.Thorough ? thoroughProfanityMatcher :
                searchType == ProfanitySearchType.Partial ? partialProfanityMatcher :
                searchType == ProfanitySearchType.Mitigated ? partialProfanityMatcher :
                shallowProfanityMatcher;
            var matches = matcher.Matches(sentence);
            List<ProfanityOccurrenceInfo> occurrences = [];
            foreach (Match match in matches)
            {
                if (searchType == ProfanitySearchType.Mitigated)
                {
                    // Load the list of known words
                    var knownWords = WordManager.GetWords(WordDataType.WordsFull);

                    // Get a word from these two values and compare it to the list of known words
                    string word = sentence.GetEnclosedWordFromIndex(match.Index);
                    if (knownWords.Any((knownWord) => word.Equals(knownWord, StringComparison.OrdinalIgnoreCase)))
                        continue;
                }
                var info = new ProfanityOccurrenceInfo(searchType, match.Value, match.Value);
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
            string partialPattern = string.Join(@"|", profanities.Select(word => string.Join("", word.ToCharArray().Select((ch) => $"{ch}".ReplaceAllRange(unescaped, escaped)))));
            string shallowPattern = string.Join(@"\b|\b", profanities.Select(word => string.Join("", word.ToCharArray().Select((ch) => $"{ch}".ReplaceAllRange(unescaped, escaped)))));
            thoroughProfanityMatcher = new Regex(
                @"\b(" + thoroughPattern + @")\b", RegexOptions.IgnoreCase);
            partialProfanityMatcher = new Regex(
                @"(" + partialPattern + @")", RegexOptions.IgnoreCase);
            shallowProfanityMatcher = new Regex(
                @"(\b" + shallowPattern + @"\b)", RegexOptions.IgnoreCase);
        }
    }
}