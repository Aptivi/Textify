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

namespace Textify.Data
{
    /// <summary>
    /// Data type
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Initializes the zip files containing names and surnames
        /// </summary>
        Names = 1,
        /// <summary>
        /// Initializes the zip files containing Unicode data
        /// </summary>
        Unicode = 2,
        /// <summary>
        /// Initializes the zip files containing word lists
        /// </summary>
        Words = 4,
    }
}
