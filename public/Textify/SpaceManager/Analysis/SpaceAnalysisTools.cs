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
using System.Linq;
using System.Text;

namespace Textify.SpaceManager.Analysis
{
    /// <summary>
    /// Space analysis tools
    /// </summary>
    public static class SpaceAnalysisTools
    {
        /// <summary>
        /// Analyzes spaces from the given text
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>Analysis results</returns>
        public static SpaceAnalysisResult AnalyzeSpaces(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            // Make a stream to analyze it
            var textStream = new MemoryStream();
            var textBytes = Encoding.UTF8.GetBytes(text);
            textStream.Write(textBytes, 0, textBytes.Length);
            return AnalyzeSpacesFrom(textStream);
        }

        /// <summary>
        /// Analyzes spaces from the given path to file
        /// </summary>
        /// <param name="pathToFile">File to analyze</param>
        /// <returns>Analysis results</returns>
        public static SpaceAnalysisResult AnalyzeSpacesFrom(string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile))
                throw new ArgumentNullException(nameof(pathToFile));
            if (!File.Exists(pathToFile))
                throw new FileNotFoundException("File not found to analyze space from.", pathToFile);

            // Open the file, analyze it, then close it.
            var fileReader = File.OpenRead(pathToFile);
            var result = AnalyzeSpacesFrom(fileReader);
            fileReader.Close();
            return result;
        }

        /// <summary>
        /// Analyzes spaces from the given stream
        /// </summary>
        /// <param name="stream">Stream to analyze</param>
        /// <returns>Analysis results</returns>
        public static SpaceAnalysisResult AnalyzeSpacesFrom(Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead)
                throw new InvalidOperationException("Stream can't read.");
            if (!stream.CanSeek)
                throw new InvalidOperationException("Stream can't seek.");
            return new SpaceAnalysisResult(stream);
        }

        internal static bool IsTrueSpaceOrChar(char c) =>
            !Spaces.badSpaces.Where((kvp) => !Encoding.UTF8.GetBytes($"{c}").Except(kvp.Value).Any()).Any();
    }
}
