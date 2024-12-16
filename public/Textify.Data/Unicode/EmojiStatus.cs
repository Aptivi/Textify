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

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Emoji status enumeration
    /// </summary>
    public enum EmojiStatus
    {
        /// <summary>
        /// Emoji doesn't have a status
        /// </summary>
        None,
        /// <summary>
        /// Emoji is a component (excluding Regional_Indicators, ASCII, and non-Emoji.)
        /// </summary>
        Component,
        /// <summary>
        /// Emoji is fully qualified
        /// </summary>
        FullyQualified,
        /// <summary>
        /// Emoji is minimally qualified
        /// </summary>
        MinimalQualified,
        /// <summary>
        /// Emoji is not qualified
        /// </summary>
        NotQualified,
    }
}
