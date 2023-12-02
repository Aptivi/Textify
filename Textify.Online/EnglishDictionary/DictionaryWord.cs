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

using Newtonsoft.Json;

namespace Textify.Online.EnglishDictionary
{
    /// <summary>
    /// A dictionary word
    /// </summary>
    public partial class DictionaryWord
    {
        /// <summary>
        /// The definition class
        /// </summary>
        public partial class DefinitionType
        {
            /// <summary>
            /// Word definition
            /// </summary>
            [JsonProperty("definition")]
            public string Definition { get; set; }

            /// <summary>
            /// List of synonyms based on the definition
            /// </summary>
            [JsonProperty("synonyms")]
            public string[] Synonyms { get; set; }

            /// <summary>
            /// List of antonyms based on the definition
            /// </summary>
            [JsonProperty("antonyms")]
            public string[] Antonyms { get; set; }

            /// <summary>
            /// Example in sentence
            /// </summary>
            [JsonProperty("example")]
            public string Example { get; set; }
        }

        /// <summary>
        /// The license class
        /// </summary>
        public partial class License
        {
            /// <summary>
            /// License name
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// License URL
            /// </summary>
            [JsonProperty("url")]
            public string Url { get; set; }
        }

        /// <summary>
        /// Word meaning class
        /// </summary>
        public partial class Meaning
        {
            /// <summary>
            /// Part of speech, usually noun, verb, adjective, adverb, interjection, etc.
            /// </summary>
            [JsonProperty("partOfSpeech")]
            public string PartOfSpeech { get; set; }

            /// <summary>
            /// List of word definitions. Words usually come with one or more definitions.
            /// </summary>
            [JsonProperty("definitions")]
            public DefinitionType[] Definitions { get; set; }

            /// <summary>
            /// List of synonyms based on the word meaning
            /// </summary>
            [JsonProperty("synonyms")]
            public string[] Synonyms { get; set; }

            /// <summary>
            /// List of antonyms based on the word meaning
            /// </summary>
            [JsonProperty("antonyms")]
            public string[] Antonyms { get; set; }
        }

        /// <summary>
        /// Phonetic class
        /// </summary>
        public partial class Phonetic
        {
            /// <summary>
            /// Phonetic representation of the word
            /// </summary>
            [JsonProperty("text")]
            public string Text { get; set; }

            /// <summary>
            /// Link to the pronounciation, usually in MP3 format. Use NAudio (Windows) to play it.
            /// </summary>
            [JsonProperty("audio")]
            public string Audio { get; set; }

            /// <summary>
            /// From where did we get the audio from?
            /// </summary>
            [JsonProperty("sourceUrl")]
            public string SourceUrl { get; set; }

            /// <summary>
            /// License information for the source
            /// </summary>
            [JsonProperty("license")]
            public License License { get; set; }
        }

        /// <summary>
        /// The actual word
        /// </summary>
        [JsonProperty("word")]
        public string Word { get; set; }

        /// <summary>
        /// The base phonetic representation of the word
        /// </summary>
        [JsonProperty("phonetic")]
        public string PhoneticWord { get; set; }

        /// <summary>
        /// The alternative phonetic representations
        /// </summary>
        [JsonProperty("phonetics")]
        public Phonetic[] Phonetics { get; set; }

        /// <summary>
        /// Word meanings
        /// </summary>
        [JsonProperty("meanings")]
        public Meaning[] Meanings { get; set; }

        /// <summary>
        /// License information
        /// </summary>
        [JsonProperty("license")]
        public License LicenseInfo { get; set; }

        /// <summary>
        /// List of where we got the word information from
        /// </summary>
        [JsonProperty("sourceUrls")]
        public string[] SourceUrls { get; set; }
    }
}
