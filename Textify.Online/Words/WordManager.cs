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
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Textify.Online.Words
{
    /// <summary>
    /// The word management class
    /// </summary>
    public static class WordManager
    {
        private static readonly List<string> Words = [];
        private static readonly HttpClient httpClient = new();
        private static readonly Random rng = new();

        /// <summary>
        /// Initializes the words. Does nothing if already downloaded.
        /// </summary>
        public static void InitializeWords()
        {
            // Download the words
            if (Words.Count == 0)
            {
                var response = httpClient.GetAsync("https://cdn.jsdelivr.net/gh/dwyl/english-words/words_alpha.txt").Result;
                var contentStream = new MemoryStream();
                response.Content.ReadAsStreamAsync().Result.CopyTo(contentStream);
                contentStream.Seek(0L, SeekOrigin.Begin);
                Words.AddRange(new StreamReader(contentStream).ReadToEnd().SplitNewLines().ToList());
            }
        }

        /// <summary>
        /// Initializes the words. Does nothing if already downloaded.
        /// </summary>
        public static async Task InitializeWordsAsync()
        {
            // Download the words
            if (Words.Count == 0)
            {
                var response = await httpClient.GetAsync("https://cdn.jsdelivr.net/gh/dwyl/english-words/words_alpha.txt");
                var contentStream = new MemoryStream();
                var stream = await response.Content.ReadAsStreamAsync();
                stream.CopyTo(contentStream);
                contentStream.Seek(0L, SeekOrigin.Begin);
                Words.AddRange(new StreamReader(contentStream).ReadToEnd().SplitNewLines().ToList());
            }
        }

        /// <summary>
        /// Gets all words
        /// </summary>
        public static string[] GetWords()
        {
            InitializeWords();
            return [.. Words];
        }

        /// <summary>
        /// Gets a random word
        /// </summary>
        /// <returns>A random word</returns>
        public static string GetRandomWord()
        {
            InitializeWords();
            return Words[rng.Next(Words.Count)];
        }

        /// <summary>
        /// Gets a random word conditionally
        /// </summary>
        /// <param name="wordMaxLength">The maximum length of the word</param>
        /// <param name="wordStartsWith">The word starts with...</param>
        /// <param name="wordEndsWith">The word ends with...</param>
        /// <param name="wordExactLength">The exact length of the word</param>
        /// <returns>A random word</returns>
        public static string GetRandomWordConditional(int wordMaxLength, string wordStartsWith, string wordEndsWith, int wordExactLength)
        {
            // Get an initial word
            string word = GetRandomWord();
            bool lengthCheck = wordMaxLength > 0;
            bool startsCheck = !string.IsNullOrWhiteSpace(wordStartsWith);
            bool endsCheck = !string.IsNullOrWhiteSpace(wordEndsWith);
            bool exactLengthCheck = wordExactLength > 0;

            // Loop until all the conditions that need to be checked are satisfied
            while (!((lengthCheck && word.Length <= wordMaxLength || !lengthCheck) &&
                     (startsCheck && word.StartsWith(wordStartsWith) || !startsCheck) &&
                     (endsCheck && word.EndsWith(wordEndsWith) || !endsCheck) &&
                     (exactLengthCheck && word.Length == wordExactLength || !exactLengthCheck)))
                word = GetRandomWord();

            // Get a word that satisfies all the conditions
            return word;
        }

        /// <summary>
        /// Gets all words
        /// </summary>
        public static async Task<string[]> GetWordsAsync()
        {
            await InitializeWordsAsync();
            return [.. Words];
        }

        /// <summary>
        /// Gets a random word
        /// </summary>
        /// <returns>A random word</returns>
        public static async Task<string> GetRandomWordAsync()
        {
            await InitializeWordsAsync();
            return Words[rng.Next(Words.Count)];
        }

        /// <summary>
        /// Gets a random word conditionally
        /// </summary>
        /// <param name="wordMaxLength">The maximum length of the word</param>
        /// <param name="wordStartsWith">The word starts with...</param>
        /// <param name="wordEndsWith">The word ends with...</param>
        /// <param name="wordExactLength">The exact length of the word</param>
        /// <returns>A random word</returns>
        public static async Task<string> GetRandomWordConditionalAsync(int wordMaxLength, string wordStartsWith, string wordEndsWith, int wordExactLength)
        {
            // Get an initial word
            string word = await GetRandomWordAsync();
            bool lengthCheck = wordMaxLength > 0;
            bool startsCheck = !string.IsNullOrWhiteSpace(wordStartsWith);
            bool endsCheck = !string.IsNullOrWhiteSpace(wordEndsWith);
            bool exactLengthCheck = wordExactLength > 0;

            // Loop until all the conditions that need to be checked are satisfied
            while (!((lengthCheck && word.Length <= wordMaxLength || !lengthCheck) &&
                     (startsCheck && word.StartsWith(wordStartsWith) || !startsCheck) &&
                     (endsCheck && word.EndsWith(wordEndsWith) || !endsCheck) &&
                     (exactLengthCheck && word.Length == wordExactLength || !exactLengthCheck)))
                word = GetRandomWord();

            // Get a word that satisfies all the conditions
            return word;
        }

        private static string[] SplitNewLines(this string Str) =>
            Str.Replace(Convert.ToChar(13).ToString(), "")
               .Split(Convert.ToChar(10));
    }
}
