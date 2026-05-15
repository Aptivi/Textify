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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Textify.General;

namespace Textify.Data.Cowsay
{
    /// <summary>
    /// Embedded cow format provider
    /// </summary>
    public class EmbeddedCowFormatProvider : ICowFormatProvider
    {
        private readonly Lazy<IReadOnlyList<(string Name, string FullPath)>> _cachedCows;
        private const string ResourcePrefix = "Textify.Data.";

        /// <inheritdoc/>
        public async Task<string> GetCowFormatAsync(string cowName)
        {
            var cows = _cachedCows.Value;
            var (Name, FullPath) = cows.FirstOrDefault(c => c.Name == cowName);

            if (FullPath == null)
                throw new FileNotFoundException($"{cowName}.cow embedded file not found");

            string cowFileContents;
            var assembly = typeof(DefaultCattleFarmer).Assembly;
            using (var stream = assembly.GetManifestResourceStream(FullPath))
                cowFileContents = await stream.ConvertToStringAsync(leaveOpen: false).ConfigureAwait(false);

            var cowFile = new CowFile(cowFileContents);
            return await cowFile.GetCowFormatAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<string>> GetAvailableCowsAsync()
        {
            var cows = _cachedCows.Value;
            var names = cows.Select(c => c.Name).ToList().AsReadOnly();
            return Task.FromResult<IReadOnlyList<string>>(names);
        }

        internal static IReadOnlyList<(string Name, string FullPath)> LoadCows()
        {
            var assembly = typeof(DefaultCattleFarmer).Assembly;
            return assembly.GetManifestResourceNames()
                .Where(rn => rn.VerifyPrefix(ResourcePrefix) && rn.VerifySuffix(".cow"))
                .Select(rn => (Name: rn.RemovePrefix(ResourcePrefix).RemoveSuffix(".cow"), FullPath: rn))
                .OrderBy(c => c.Name)
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Embedded cow format provider constructor
        /// </summary>
        public EmbeddedCowFormatProvider()
        {
            _cachedCows = new Lazy<IReadOnlyList<(string Name, string FullPath)>>(
                LoadCows,
                LazyThreadSafetyMode.PublicationOnly);
        }
    }
}
