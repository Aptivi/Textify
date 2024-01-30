
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
using Textify.Data;
using Textify.Unicode;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    internal class QueryUnicode : IFixture
    {
        public string FixtureID => "QueryUnicode";
        public void RunFixture()
        {
            // Prompt for a character
            Console.Write("Enter a character: ");
            char character = Console.ReadKey(true).KeyChar;
            Console.WriteLine();

            // Query it
            DataInitializer.Initialize(DataType.Unicode);
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            Console.WriteLine($"Na (current): {charInstance.Na}");
            Console.WriteLine($"Na1 (Unicode v1): {charInstance.Na1}");
        }
    }
}
