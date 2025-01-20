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

namespace Textify.Accessibility
{
    /// <summary>
    /// Braille dot enumeration
    /// </summary>
    public enum BrailleDots
    {
        /// <summary>
        /// No dot. (⠀)
        /// </summary>
        None = 0,
        /// <summary>
        /// First dot, located in the first column, first row. (⠁)
        /// </summary>
        First = 1,
        /// <summary>
        /// Second dot, located in the first column, second row. (⠂)
        /// </summary>
        Second = 2,
        /// <summary>
        /// Third dot, located in the first column, third row. (⠄)
        /// </summary>
        Third = 4,
        /// <summary>
        /// Fourth dot, located in the second column, first row. (⠈)
        /// </summary>
        Fourth = 8,
        /// <summary>
        /// Fifth dot, located in the second column, second row. (⠐)
        /// </summary>
        Fifth = 16,
        /// <summary>
        /// Sixth dot, located in the second column, third row. (⠠)
        /// </summary>
        Sixth = 32,
    }
}
