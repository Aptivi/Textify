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

using System;

namespace Textify.Versioning
{
    /// <summary>
    /// Exception for semantic versioning
    /// </summary>
    public class SemVerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SemVerException"/>
        /// </summary>
        public SemVerException()
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="SemVerException"/>
        /// </summary>
        public SemVerException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="SemVerException"/>
        /// </summary>
        public SemVerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
