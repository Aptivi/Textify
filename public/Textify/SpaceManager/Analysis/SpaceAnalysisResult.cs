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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Textify.SpaceManager.Analysis
{
    /// <summary>
    /// A class for the space analysis result
    /// </summary>
    public class SpaceAnalysisResult
    {
        /// <summary>
        /// Gets the stream from the analysis
        /// </summary>
        public Stream ResultingStream { get; }
        /// <summary>
        /// Gets the array of false spaces with their indexes in the text
        /// </summary>
        public (char, string)[] FalseSpaces { get; }

        internal SpaceAnalysisResult(Stream stream)
        {
            int readByte = 0;
            long bytesRead = 0;
            List<(char, string)> falseSpaces = [];

            // Seek through the entire stream
            stream.Seek(0, SeekOrigin.Begin);
            while (readByte != -1)
            {
                // Read a byte
                readByte = stream.ReadByte();
                if (readByte == -1)
                    break;

                // Process it and verify the whitespace
                char c = (char)readByte;
                byte b = (byte)readByte;
                var badSpaceList = Spaces.badSpaces.Where((kvp) => kvp.Value[0] == b);
                if (badSpaceList.Any())
                {
                    // Seek until we find exactly a character that we want
                    int charLen = 0;
                    int charIdx = 1;
                    while (badSpaceList.Count() > 1)
                    {
                        charIdx++;
                        readByte = stream.ReadByte();
                        badSpaceList = badSpaceList.Where((kvp) => kvp.Value.Length >= charIdx && kvp.Value[charIdx - 1] == (byte)readByte);
                    }
                    if (badSpaceList.Any())
                    {
                        var kvp = badSpaceList.Single();
                        c = Encoding.UTF8.GetString(kvp.Value)[0];
                        charLen = Encoding.UTF8.GetByteCount($"{c}") - (charIdx - 1);
                        for (int i = 1; i < charLen; i++)
                            readByte = stream.ReadByte();
                    }
                }
                if (!SpaceAnalysisTools.IsTrueSpaceOrChar(c))
                {
                    var falseSpace = (c, Spaces.badSpaces.Where((kvp) => Encoding.UTF8.GetBytes($"{c}").SequenceEqual(kvp.Value)).ToArray()[0].Key);
                    if (!falseSpaces.Contains(falseSpace))
                        falseSpaces.Add(falseSpace);
                }
                bytesRead++;
            }

            // Install the values
            ResultingStream = stream;
            FalseSpaces = [.. falseSpaces];
        }
    }
}
