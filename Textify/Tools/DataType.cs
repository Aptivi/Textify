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

namespace Textify.Tools
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
    }
}
