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
            if (string.IsNullOrWhiteSpace(sentence))
                return [];

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
                string source = sentence.GetEnclosedWordFromIndex(match.Index);
                int sourceIndex = sentence.GetIndexOfEnclosedWordFromIndex(match.Index);
                source = source.Length < 2 ? match.Value : source;
                var info = new ProfanityOccurrenceInfo(searchType, match.Value, source, sentence, match.Index, sourceIndex);
                occurrences.Add(info);
            }
            return [.. occurrences];
        }

        /// <summary>
        /// Filter profane words in the sentence
        /// </summary>
        /// <param name="sentence">A sentence which may contain profanity</param>
        /// <param name="searchType">Specifies how to analyze the sentence</param>
        /// <returns>A filtered sentence that contain censored profanity with asterisks</returns>
        public static string FilterProfanities(string sentence, ProfanitySearchType searchType = ProfanitySearchType.Shallow)
        {
            // Get the profanities and check to see if it contains profanity or not
            var profanities = GetProfanities(sentence, searchType);
            if (profanities is null || profanities.Length == 0)
                return sentence;

            // Now, filter them!
            return FilterProfanities(profanities);
        }

        private static string FilterProfanities(ProfanityOccurrenceInfo[] profanities)
        {
            if (profanities is null || profanities.Length == 0)
                return "";

            // Get the profanity properties and use them to filter swear words. We need to sort by decreasing index
            // for safety by getting from the last profanity occurrence to the first one.
            StringBuilder sentenceBuilder = new(profanities[0].SourceSentence);
            for (int i = profanities.Length - 1; i >= 0; i--)
            {
                var profanity = profanities[i];
                int sourceIndex = profanity.SourceIndex;
                int sourceLength = profanity.SourceWord.Length;
                string replacement = new('*', sourceLength);
                sentenceBuilder.Remove(sourceIndex, sourceLength);
                sentenceBuilder.Insert(sourceIndex, replacement);
            }

            // Return the resulting string
            return sentenceBuilder.ToString();
        }

        private static void InitializeMatchers()
        {
            if (thoroughProfanityMatcher is not null && shallowProfanityMatcher is not null && partialProfanityMatcher is not null)
                return;

            // Get the profanities and make three matchers: thorough, partial, and shallow.
            string[] profanities = WordManager.GetWords(WordDataType.BadWords);
            string thoroughPattern = string.Join("|", profanities.Select(word => string.Join(@"\s*", word.ToCharArray().Select((ch) => $"{ch}".Escape()))));
            string partialPattern = string.Join(@"|", profanities.Select(word => string.Join("", word.ToCharArray().Select((ch) => $"{ch}".Escape()))));
            string shallowPattern = string.Join(@"\b|\b", profanities.Select(word => string.Join("", word.ToCharArray().Select((ch) => $"{ch}".Escape()))));
            thoroughProfanityMatcher = new Regex(
                @"\b(" + thoroughPattern + @")\b", RegexOptions.IgnoreCase);
            partialProfanityMatcher = new Regex(
                @"(" + partialPattern + @")", RegexOptions.IgnoreCase);
            shallowProfanityMatcher = new Regex(
                @"(\b" + shallowPattern + @"\b)", RegexOptions.IgnoreCase);
        }
    }
}
