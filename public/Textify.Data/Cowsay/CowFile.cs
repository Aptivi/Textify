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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Textify.Data.Cowsay
{
    internal class CowFile
    {
        private string _fileContents = "";
        private bool _stringLoaded = false;
        private readonly Stream? _fileStream;
        private readonly SemaphoreSlim _streamLock = new(1);

        internal async Task<string> GetCowFormatAsync()
        {
            if (!_stringLoaded)
            {
                await _streamLock.WaitAsync().ConfigureAwait(false);

                try
                {
                    if (!_stringLoaded)
                    {
                        if (_fileStream is null)
                            throw new ArgumentNullException(nameof(_fileStream));
                        _fileContents = await _fileStream.ConvertToStringAsync(leaveOpen: true).ConfigureAwait(false);
                        _stringLoaded = true;
                    }
                }
                finally
                {
                    _streamLock.Release();
                }
            }

            string cowFormat = ExtractCow(_fileContents);
            return cowFormat;
        }

        private string ExtractCow(string cowString)
        {
            var match = RegularExpressions.Cow.Match(cowString);

            if (!match.Groups["cow"].Success)
                throw new ArgumentException("Failed to extract cow from cow file.", nameof(cowString));

            var cow = match.Groups["cow"].Value;

            cow = RegularExpressions.LineEndings.Replace(cow, Environment.NewLine)
                .Replace("\\\\", "\\");

            return cow;
        }

        private enum LoadMode
        {
            Stream,
            String
        }

        internal CowFile(Stream fileStream)
        {
            _fileStream = fileStream;
        }

        internal CowFile(string fileContents)
        {
            _fileContents = fileContents;
            _stringLoaded = true;
        }
    }
}
