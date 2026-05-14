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

// Originally found in https://github.com/rawsonm88/Cowsay

using System;
using System.IO;
using System.Threading.Tasks;

namespace Textify.Data.Cowsay
{
    /// <summary>
    /// Default cattle farmer
    /// </summary>
    public class DefaultCattleFarmer : ICattleFarmer
    {
        private readonly ICowFormatProvider? _cowFormatProvider;
        private readonly IBubbleBlower? _bubbleBlower;

        /// <summary>
        /// Retrieves the format string from <see cref="ICowFormatProvider"/> and returns a new ICow instance.
        /// </summary>
        /// <param name="cowName">The cow format name.</param>
        /// <returns>A new ICow instance with the format string specified.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the provider can't find the requested cow format.</exception>
        public static async Task<ICow> RearCowWithDefaults(string cowName)
        {
            var cowFormatProvider = new EmbeddedCowFormatProvider();
            var bubbleBlower = new DefaultBubbleBlower();

            var cowFormat = await cowFormatProvider.GetCowFormatAsync(cowName);

            return new Cow(cowFormat, bubbleBlower);
        }

        /// <inheritdoc/>
        public async Task<ICow> RearCowAsync(string cowName)
        {
            string cowFormat = _cowFormatProvider is not null ? await _cowFormatProvider.GetCowFormatAsync(cowName).ConfigureAwait(false) : "";

            return new Cow(cowFormat, _bubbleBlower);
        }

        /// <inheritdoc/>
        public async Task<ICow> RearCowFromFileStreamAsync(Stream cowStream)
        {
            var cowFile = new CowFile(await cowStream.ConvertToStringAsync(leaveOpen: true).ConfigureAwait(false));
            return new Cow(await cowFile.GetCowFormatAsync().ConfigureAwait(false), _bubbleBlower);
        }

        /// <summary>
        /// Makes a new instance of the default cattle farmer
        /// </summary>
        /// <param name="cowFormatProvider">Cow format provider interface</param>
        /// <param name="bubbleBlower">Bubble blower interface</param>
        public DefaultCattleFarmer(ICowFormatProvider? cowFormatProvider, IBubbleBlower? bubbleBlower)
        {
            _cowFormatProvider = cowFormatProvider;
            _bubbleBlower = bubbleBlower;
        }
    }
}
