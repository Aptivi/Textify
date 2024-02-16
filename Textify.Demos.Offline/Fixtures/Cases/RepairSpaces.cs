
// Terminaux  Copyright (C) 2023  Aptivi
// 
// This file is part of Terminaux
// 
// Terminaux is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Terminaux is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Text;
using Textify.SpaceManager.Analysis;
using Textify.SpaceManager.Conversion;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    public class RepairSpaces : IFixture
    {
        public string FixtureID => "RepairSpaces";
        public void RunFixture()
        {
            char nbsp = '\u00a0';
            string text = $"Textify{nbsp}Test";

            // Prompt for spaces
            Console.Write("Enter a text containing non-breaking spaces: ");
            string input = Console.ReadLine();
            text = string.IsNullOrEmpty(input) ? text : input;

            // Analyze them
            var result = SpaceAnalysisTools.AnalyzeSpaces(text);
            Console.WriteLine($"Amount of false spaces: {result.FalseSpaces.Length}");
            foreach (var line in result.FalseSpaces)
                Console.WriteLine($"  - There is a false space with char id {(int)line.Item1} - {line.Item2}");
            if (result.FalseSpaces.Length == 0)
                Console.WriteLine("  - Your text is clean!");
            else
            {
                // Fix them
                Console.WriteLine("\nFixing your text...");
                byte[] convertedBytes = SpaceConversionTools.ConvertSpaces(result);
                string converted = Encoding.UTF8.GetString(convertedBytes);
                Console.WriteLine("Fixed! Here's your new text:");
                Console.WriteLine($"  - {converted}\n");

                // Verify the fixed text
                var verified = SpaceAnalysisTools.AnalyzeSpaces(converted);
                Console.WriteLine($"Amount of false spaces: {verified.FalseSpaces.Length}");
                foreach (var line in verified.FalseSpaces)
                    Console.WriteLine($"  - There is a false space with char id {(int)line.Item1} - {line.Item2}");
                if (verified.FalseSpaces.Length == 0)
                    Console.WriteLine("  - Your text is clean!");
                else
                    Console.WriteLine("  - Your text is still not clean.");
            }
        }
    }
}
