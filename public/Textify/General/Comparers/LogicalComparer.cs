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

using System.Collections.Generic;

namespace Textify.General.Comparers
{
    /// <summary>
    /// Logical text comparer
    /// </summary>
    public class LogicalComparer : IComparer<string>
    {
        /// <summary>
        /// Compares between two strings in a logical way
        /// </summary>
        /// <param name="x">First string to compare</param>
        /// <param name="y">Second string to compare</param>
        /// <returns>Check <see cref="TextTools.CompareLogical(string, string)"/></returns>
        public int Compare(string x, string y) =>
            TextTools.CompareLogical(x, y);
    }
}
