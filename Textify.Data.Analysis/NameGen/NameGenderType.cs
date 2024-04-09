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

namespace Textify.Data.Analysis.NameGen
{
    /// <summary>
    /// Name gender type
    /// </summary>
    public enum NameGenderType
    {
        /// <summary>
        /// Uses the list of both male and female names
        /// </summary>
        Unified,
        /// <summary>
        /// Uses the list of female names
        /// </summary>
        Female,
        /// <summary>
        /// Uses the list of male names
        /// </summary>
        Male,
        /// <summary>
        /// Uses the list of implicit female names
        /// </summary>
        FemaleImplicit,
        /// <summary>
        /// Uses the list of implicit male names
        /// </summary>
        MaleImplicit,
        /// <summary>
        /// Uses the list of unified natural names
        /// </summary>
        Natural,
    }
}
