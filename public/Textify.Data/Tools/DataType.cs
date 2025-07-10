//
// Textify  Copyright (C) 2023-2025  Aptivi
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

namespace Textify.Data.Tools
{
    /// <summary>
    /// Data type
    /// </summary>
    internal enum DataType
    {
        /// <summary>
        /// Initializes the zip files containing names
        /// </summary>
        Names = 1,
        /// <summary>
        /// Initializes the zip files containing female names
        /// </summary>
        NamesFemale = 2,
        /// <summary>
        /// Initializes the zip files containing male names
        /// </summary>
        NamesMale = 4,
        /// <summary>
        /// Initializes the zip files containing implicit female names
        /// </summary>
        NamesFemaleImplicit = 8,
        /// <summary>
        /// Initializes the zip files containing implicit male names
        /// </summary>
        NamesMaleImplicit = 16,
        /// <summary>
        /// Initializes the zip files containing natural names
        /// </summary>
        NamesNatural = 32,
        /// <summary>
        /// Initializes the zip files containing surnames
        /// </summary>
        Surnames = 64,
        /// <summary>
        /// Initializes the zip files containing Unicode data without Unihan data
        /// </summary>
        UnicodeNoUnihan = 128,
        /// <summary>
        /// Initializes the zip files containing Unicode data with just Unihan data
        /// </summary>
        UnicodeUnihan = 256,
        /// <summary>
        /// Initializes the zip files containing Unicode data
        /// </summary>
        Unicode = 512,
        /// <summary>
        /// Initializes the zip files containing word lists
        /// </summary>
        Words = 1024,
        /// <summary>
        /// Initializes the zip files containing word lists, including alphanumeric characters
        /// </summary>
        WordsFull = 2048,
        /// <summary>
        /// Initializes the zip files containing word lists, including offensive words (18+)
        /// </summary>
        WordsDirty = 4096,
        /// <summary>
        /// Initializes the zip files containing word lists, including offensive words (18+) and alphanumeric characters
        /// </summary>
        WordsDirtyFull = 8192,
        /// <summary>
        /// Initializes the zip files containing just the offensive words (18+) for bad word filtering
        /// </summary>
        WordsJustDirty = 16384,
        /// <summary>
        /// Initializes the zip files containing common words list
        /// </summary>
        CommonWords = 32768,
        /// <summary>
        /// Initializes the zip files containing common words list, including the offensive words (18+)
        /// </summary>
        CommonWordsDirty = 65536,
        /// <summary>
        /// Initializes the JSON file containing char indexes without Unihan data
        /// </summary>
        UnicodeNoUnihanIndex = 131072,
        /// <summary>
        /// Initializes the JSON file containing char indexes with just Unihan data
        /// </summary>
        UnicodeUnihanIndex = 262144,
        /// <summary>
        /// Initializes the JSON file containing char indexes
        /// </summary>
        UnicodeIndex = 524288,
    }
}
