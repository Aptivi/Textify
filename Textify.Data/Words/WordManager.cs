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
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Textify.Data.Tools;
using Textify.General;
using Textify.Tools;

namespace Textify.Data.Words
{
    /// <summary>
    /// The word management class
    /// </summary>
    public static class WordManager
    {
        private static readonly Dictionary<WordDataType, MemoryStream> words = [];
        private static readonly Random rng = new();

        /// <summary>
        /// Gets all words
        /// </summary>
        /// <param name="type">Specifies the word data type</param>
        public static string[] GetWords(WordDataType type = WordDataType.Words) =>
            GetWordsAsync(type).ConfigureAwait(true).GetAwaiter().GetResult();

        /// <summary>
        /// Gets a random word
        /// </summary>
        /// <param name="type">Specifies the word data type</param>
        /// <returns>A random word</returns>
        public static string GetRandomWord(WordDataType type = WordDataType.Words)
        {
            var words = GetWords(type);
            return words[rng.Next(words.Length)];
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
            List<string> wordList = [];
            if (!words.TryGetValue(type, out MemoryStream stream))
            {
                stream = await GetWordListAsync(type);
                words.Add(type, stream);
            }

            // Now, get all the words
            stream.Seek(0, SeekOrigin.Begin);
            var dataReader = new StreamReader(stream);
            while (!dataReader.EndOfStream)
            {
                // Get a line
                string line = dataReader.ReadLine();

                // TODO: Move condition processing here
                // Add this processed line
                wordList.Add(line);
            }
            return [.. wordList];
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

        private static async Task<MemoryStream> GetWordListAsync(WordDataType type)
        {
            (DataType dataType, string resourceName) =
                type == WordDataType.Words ? (DataType.Words, "words-clean-alpha") :
                type == WordDataType.WordsFull ? (DataType.WordsFull, "words-clean") :
                type == WordDataType.WordsDirty ? (DataType.WordsDirty, "words_alpha") :
                type == WordDataType.WordsDirtyFull ? (DataType.WordsDirtyFull, "words") :
                type == WordDataType.BadWords ? (DataType.WordsJustDirty, "bad-words") :
                type == WordDataType.CommonWords ? (DataType.CommonWords, "words-common-clean") :
                type == WordDataType.CommonWordsDirty ? (DataType.CommonWordsDirty, "words-common") :
                throw new TextifyException("Invalid word data type");
            var contentStream = DataInitializer.GetStreamFrom(dataType);
            var archive = new ZipArchive(contentStream, ZipArchiveMode.Read);

            // Open the XML to stream
            var content = archive.GetEntry(resourceName + ".txt").Open();
            var extracted = new MemoryStream();
            await content.CopyToAsync(extracted);
            extracted.Seek(0, SeekOrigin.Begin);
            return extracted;
        }
    }
}
