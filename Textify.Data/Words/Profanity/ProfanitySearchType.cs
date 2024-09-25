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

namespace Textify.Data.Words.Profanity
{
    /// <summary>
    /// Profanity search type
    /// </summary>
    public enum ProfanitySearchType
    {
        /// <summary>
        /// Shallow searching. May not find swearing embedded in two or more words and/or separated by whitespace.
        /// </summary>
        Shallow,
        /// <summary>
        /// Thorough searching. May not find swearing embedded in two or more words.
        /// </summary>
        Thorough,
        /// <summary>
        /// Partial searching. May cause legitimate words to be found, such as Scunthorpe.
        /// </summary>
        Partial,
        /// <summary>
        /// Mitigated partial searching. May not find swearing that has its characters separated by whitespace.
        /// </summary>
        Mitigated,
    }
}
