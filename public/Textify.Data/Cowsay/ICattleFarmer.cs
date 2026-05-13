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

using System.IO;
using System.Threading.Tasks;

namespace Textify.Data.Cowsay
{
    /// <summary>
    /// Cattle farmer interface
    /// </summary>
    public interface ICattleFarmer
    {
        /// <summary>
        /// Retrieves the format string from <see cref="ICowFormatProvider"/> and returns a new ICow instance.
        /// </summary>
        /// <param name="cowName">The cow format name.</param>
        /// <returns>A new ICow instance with the format string specified.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the provider can't find the requested cow format.</exception>
        Task<ICow> RearCowAsync(string cowName = "default");

        /// <summary>
        /// Retrieves the format string from the .cow file provided in the stream.
        /// </summary>
        /// <param name="cowStream">The .cow file format stream.</param>
        /// <returns>A new ICow instance with the format string from the stream.</returns>
        Task<ICow> RearCowFromFileStreamAsync(Stream cowStream);
    }
}
