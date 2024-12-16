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

namespace Textify.Data.Words
{
    /// <summary>
    /// Word data type to select what kind of words do we need to fetch
    /// </summary>
    public enum WordDataType
    {
        /// <summary>
        /// Word list
        /// </summary>
        Words,
        /// <summary>
        /// Word list, including alphanumeric characters
        /// </summary>
        WordsFull,
        /// <summary>
        /// Word list, including offensive words (18+)
        /// </summary>
        WordsDirty,
        /// <summary>
        /// Word list, including offensive words (18+) and alphanumeric characters
        /// </summary>
        WordsDirtyFull,
        /// <summary>
        /// Offensive words list (18+) for bad word filtering
        /// </summary>
        BadWords,
        /// <summary>
        /// Common word list
        /// </summary>
        CommonWords,
        /// <summary>
        /// Common word list, including offensive words (18+)
        /// </summary>
        CommonWordsDirty,
    }
}
