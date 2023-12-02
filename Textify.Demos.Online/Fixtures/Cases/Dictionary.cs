
// Terminaux  Copyright (C) 2023  Aptivi
// 
// This file is part of Terminaux
// 
// Terminaux is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Terminaux is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using Textify.Online.EnglishDictionary;

namespace Textify.Demos.Online.Fixtures.Cases
{
    internal class Dictionary : IFixture
    {
        public string FixtureID => "Dictionary";
        public void RunFixture()
        {
            // Prompt for a word
            Console.Write("Enter a word: ");
            string input = Console.ReadLine();

            // Analyze them
            var result = DictionaryManager.GetWordInfo(input);
            foreach (var word in result)
            {
                // General info
                Console.WriteLine($"  Word: {word.Word}");
                Console.WriteLine($"  Phonetic: {word.PhoneticWord}");

                // Meanings
                foreach (var meaning in word.Meanings)
                {
                    // Part of Speech
                    Console.WriteLine($"    Part of Speech: {meaning.PartOfSpeech}");

                    // Definitions
                    foreach (var definition in meaning.Definitions)
                    {
                        // A definition and an example
                        Console.WriteLine($"      Definition: {definition.Definition}");
                        Console.WriteLine($"      Example in Sentence: {definition.Example}");

                        // Synonyms and Antonyms
                        Console.WriteLine($"      Def. Synonyms: {string.Join(", ", definition.Synonyms)}");
                        Console.WriteLine($"      Def. Antonyms: {string.Join(", ", definition.Antonyms)}");
                    }

                    // Synonyms and Antonyms
                    Console.WriteLine($"    Base Synonyms: {string.Join(", ", meaning.Synonyms)}");
                    Console.WriteLine($"    Base Antonyms: {string.Join(", ", meaning.Antonyms)}");
                }

                // Sources
                foreach (var source in word.SourceUrls)
                    Console.WriteLine($"    Source: {source}");

                // License
                var license = word.LicenseInfo;
                Console.WriteLine($"  License: {license.Name} [{license.Url}]");
            }
        }
    }
}
