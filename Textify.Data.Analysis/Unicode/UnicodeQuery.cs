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

namespace Textify.Data.Analysis.Unicode
{
    /// <summary>
    /// Unicode querying tools
    /// </summary>
    public static class UnicodeQuery
    {
        /// <summary>
        /// Queries the character
        /// </summary>
        /// <param name="character">Character</param>
        public static Char QueryChar(char character) =>
            QueryChar(Convert.ToInt32(character), UnicodeQueryType.Full);

        /// <summary>
        /// Queries the character
        /// </summary>
        /// <param name="charNum">Character number</param>
        public static Char QueryChar(int charNum) =>
            QueryChar(charNum, UnicodeQueryType.Full);

        /// <summary>
        /// Queries the character
        /// </summary>
        /// <param name="character">Character</param>
        /// <param name="type">Database type to query</param>
        public static Char QueryChar(char character, UnicodeQueryType type) =>
            QueryChar(Convert.ToInt32(character), type);

        /// <summary>
        /// Queries the character
        /// </summary>
        /// <param name="charNum">Character number</param>
        /// <param name="type">Database type to query</param>
        public static Char QueryChar(int charNum, UnicodeQueryType type) =>
            UnicodeQueryHandler.Serialize(charNum, type);
    }
}
