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
using System.Text;
using Terminaux.Colors.Data;
using Terminaux.Reader;
using Terminaux.Writer.ConsoleWriters;
using Textify.SpaceManager.Analysis;
using Textify.SpaceManager.Conversion;

namespace Textify.Demos.Offline.Fixtures
{
    public static class RepairSpaces
    {
        public static void Test()
        {
            char nbsp = '\u00a0';
            string text = $"Textify{nbsp}Test";

            // Prompt for spaces
            string input = TermReader.Read("Enter a text containing non-breaking spaces: ");
            text = string.IsNullOrEmpty(input) ? text : input;

            // Analyze them
            var result = SpaceAnalysisTools.AnalyzeSpaces(text);
            TextWriterColor.Write($"Amount of false spaces: {result.FalseSpaces.Length}");
            foreach (var line in result.FalseSpaces)
                TextWriterColor.WriteColor($"  - There is a false space with char id {(int)line.Item1} - {line.Item2}", ConsoleColors.Red);
            if (result.FalseSpaces.Length == 0)
                TextWriterColor.WriteColor("  - Your text is clean!", ConsoleColors.Lime);
            else
            {
                // Fix them
                TextWriterColor.Write("\nFixing your text...");
                byte[] convertedBytes = SpaceConversionTools.ConvertSpaces(result);
                string converted = Encoding.UTF8.GetString(convertedBytes);
                TextWriterColor.WriteColor("Fixed! Here's your new text:", ConsoleColors.Lime);
                TextWriterColor.WriteColor($"  - {converted}\n", ConsoleColors.Yellow);

                // Verify the fixed text
                var verified = SpaceAnalysisTools.AnalyzeSpaces(converted);
                TextWriterColor.Write($"Amount of false spaces: {verified.FalseSpaces.Length}");
                foreach (var line in verified.FalseSpaces)
                    TextWriterColor.WriteColor($"  - There is a false space with char id {(int)line.Item1} - {line.Item2}", ConsoleColors.Red);
                if (verified.FalseSpaces.Length == 0)
                    TextWriterColor.WriteColor("  - Your text is clean!", ConsoleColors.Lime);
                else
                    TextWriterColor.WriteColor("  - Your text is still not clean.", ConsoleColors.Red);
            }
        }
    }
}
