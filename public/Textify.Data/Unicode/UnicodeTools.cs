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

using Textify.Data.Unicode.UBidi;
using Textify.Tools;

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Unicode tools
    /// </summary>
    public static class UnicodeTools
    {
        /// <summary>
        /// Reverses the RTL character order in a text
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <returns>A string that is obtained from a visual map</returns>
        /// <exception cref="TextifyException"></exception>
        public static string ReverseRtl(string text) =>
            Bidi.LogicalToVisual(text);
    }
}
