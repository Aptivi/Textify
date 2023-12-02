
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
using Textify.Online.NameGen;

namespace Textify.Demos.Online.Fixtures.Cases
{
    internal class NameGen : IFixture
    {
        public string FixtureID => "NameGen";
        public void RunFixture()
        {
            string[] names = NameGenerator.GenerateNames(10);
            string[] firstNames = NameGenerator.GenerateFirstNames(10);
            string[] surnames = NameGenerator.GenerateLastNames(10);

            Console.WriteLine($"10 names: {string.Join(", ", names)}");
            Console.WriteLine($"10 first names: {string.Join(", ", firstNames)}");
            Console.WriteLine($"10 last names: {string.Join(", ", surnames)}");
        }
    }
}
