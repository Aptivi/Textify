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

using System;

namespace Textify.Tools
{
    /// <summary>
    /// Textify error class
    /// </summary>
    public class TextifyException : Exception
    {
        /// <summary>
        /// Reports a Textify error
        /// </summary>
        public TextifyException()
        { }

        /// <summary>
        /// Reports a Textify error
        /// </summary>
        /// <param name="message">Message to use with reporting an error</param>
        public TextifyException(string message) :
            base(message)
        { }

        /// <summary>
        /// Reports a Textify error
        /// </summary>
        /// <param name="message">Message to use with reporting an error</param>
        /// <param name="innerException">Inner exception to use with reporting an error</param>
        public TextifyException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
