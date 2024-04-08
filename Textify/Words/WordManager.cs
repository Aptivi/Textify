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
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Textify.General;
using Textify.Tools;

namespace Textify.Words
{
    /// <summary>
    /// The word management class
    /// </summary>
    public static class WordManager
    {
        private static readonly Dictionary<WordDataType, string[]> words = [];
        private static readonly Random rng = new();

        /// <summary>
        /// Gets all words
        /// </summary>
        /// <param name="type">Specifies the word data type</param>
        public static string[] GetWords(WordDataType type = WordDataType.Words)
        {
            if (!words.ContainsKey(type))
            {
                var content = GetWordListAsync(type).Result;
                words.Add(type, content);
            }
            return words[type];
        }

        /// <summary>
        /// Gets a random word
        /// </summary>
        /// <param name="type">Specifies the word data type</param>
        /// <returns>A random word</returns>
        public static string GetRandomWord(WordDataType type = WordDataType.Words) =>
            GetWords(type)[rng.Next(words.Count)];

        /// <summary>
        /// Gets a random word conditionally
        /// </summary>
        /// <param name="wordMaxLength">The maximum length of the word</param>
        /// <param name="wordStartsWith">The word starts with...</param>
        /// <param name="wordEndsWith">The word ends with...</param>
        /// <param name="wordExactLength">The exact length of the word</param>
        /// <param name="type">Specifies the word data type</param>
        /// <returns>A random word</returns>
        public static string GetRandomWordConditional(int wordMaxLength, string wordStartsWith, string wordEndsWith, int wordExactLength, WordDataType type = WordDataType.Words)
        {
            // Get an initial word
            string word = GetRandomWord(type);
            bool lengthCheck = wordMaxLength > 0;
            bool startsCheck = !string.IsNullOrWhiteSpace(wordStartsWith);
            bool endsCheck = !string.IsNullOrWhiteSpace(wordEndsWith);
            bool exactLengthCheck = wordExactLength > 0;

            // Loop until all the conditions that need to be checked are satisfied
            while (!((lengthCheck && word.Length <= wordMaxLength || !lengthCheck) &&
                     (startsCheck && word.StartsWith(wordStartsWith) || !startsCheck) &&
                     (endsCheck && word.EndsWith(wordEndsWith) || !endsCheck) &&
                     (exactLengthCheck && word.Length == wordExactLength || !exactLengthCheck)))
                word = GetRandomWord(type);

            // Get a word that satisfies all the conditions
            return word;
        }

        /// <summary>
        /// Gets all words
        /// </summary>
        public static async Task<string[]> GetWordsAsync(WordDataType type = WordDataType.Words)
        {
            if (!words.ContainsKey(type))
            {
                var content = await GetWordListAsync(type);
                words.Add(type, content);
            }
            return words[type];
        }

        /// <summary>
        /// Gets a random word
        /// </summary>
        /// <returns>A random word</returns>
        public static async Task<string> GetRandomWordAsync(WordDataType type = WordDataType.Words)
        {
            var wordList = await GetWordsAsync(type);
            return wordList[rng.Next(words.Count)];
        }

        /// <summary>
        /// Gets a random word conditionally
        /// </summary>
        /// <param name="wordMaxLength">The maximum length of the word</param>
        /// <param name="wordStartsWith">The word starts with...</param>
        /// <param name="wordEndsWith">The word ends with...</param>
        /// <param name="wordExactLength">The exact length of the word</param>
        /// <param name="type">Specifies the word data type</param>
        /// <returns>A random word</returns>
        public static async Task<string> GetRandomWordConditionalAsync(int wordMaxLength, string wordStartsWith, string wordEndsWith, int wordExactLength, WordDataType type = WordDataType.Words)
        {
            // Get an initial word
            string word = await GetRandomWordAsync(type);
            bool lengthCheck = wordMaxLength > 0;
            bool startsCheck = !string.IsNullOrWhiteSpace(wordStartsWith);
            bool endsCheck = !string.IsNullOrWhiteSpace(wordEndsWith);
            bool exactLengthCheck = wordExactLength > 0;

            // Loop until all the conditions that need to be checked are satisfied
            while (!((lengthCheck && word.Length <= wordMaxLength || !lengthCheck) &&
                     (startsCheck && word.StartsWith(wordStartsWith) || !startsCheck) &&
                     (endsCheck && word.EndsWith(wordEndsWith) || !endsCheck) &&
                     (exactLengthCheck && word.Length == wordExactLength || !exactLengthCheck)))
                word = await GetRandomWordAsync(type);

            // Get a word that satisfies all the conditions
            return word;
        }

        private static async Task<string[]> GetWordListAsync(WordDataType type)
        {
            (DataType dataType, string resourceName, string fileName) =
                type == WordDataType.Words ? (DataType.Words, "words_clean_alpha", "words-clean-alpha.txt") :
                type == WordDataType.WordsFull ? (DataType.WordsFull, "words_clean", "words-clean.txt") :
                type == WordDataType.WordsDirty ? (DataType.WordsDirty, "words_alpha", "words_alpha.txt") :
                type == WordDataType.WordsDirtyFull ? (DataType.WordsDirtyFull, "words", "words.txt") :
                type == WordDataType.BadWords ? (DataType.WordsJustDirty, "bad_words", "bad-words.txt") :
                throw new TextifyException();
            DataInitializer.Initialize(dataType);
            var contentStream = new MemoryStream(DataTools.GetDataFrom(resourceName));
            var archive = new ZipArchive(contentStream, ZipArchiveMode.Read);

            // Open the XML to stream
            var content = archive.GetEntry(fileName).Open();
            var read = await new StreamReader(content).ReadToEndAsync();
            return read.SplitNewLines(false);
        }
    }
}
