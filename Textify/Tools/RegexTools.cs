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

using System;
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

        /// <summary>
        /// Parses the regular expression pattern after validating it
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <returns>Regular expression instance of a valid regular expression pattern</returns>
        public static Regex ParseRegex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (!IsValidRegex(pattern))
                throw new ArgumentException($"Regular expression pattern is invalid. [{pattern}]");
            return new Regex(pattern);
        }

        /// <summary>
        /// Parses the regular expression pattern after validating it
        /// </summary>
        /// <param name="pattern">Specified pattern</param>
        /// <param name="result">[<see langword="out"/>] Resultant regular expression instance</param>
        /// <returns>True if valid; false otherwise</returns>
        public static bool TryParseRegex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern, out Regex? result)
        {
            try
            {
                result = ParseRegex(pattern);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
