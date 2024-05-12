//
// Terminaux  Copyright (C) 2023-2024  Aptivi
//
// This file is part of Terminaux
//
// Terminaux is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Terminaux is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Textify.Tools
{
    /// <summary>
    /// Regular expression tools
    /// </summary>
    public static class RegexTools
    {
        /// <summary>
        /// Determines whether the specified regular expression pattern is valid or not
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool IsValidRegex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern) =>
            IsValidRegex(new Regex(pattern));

        /// <summary>
        /// Determines whether the specified regular expression pattern is valid or not
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool IsValidRegex(Regex pattern)
        {
            try
            {
                pattern.Match(string.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
