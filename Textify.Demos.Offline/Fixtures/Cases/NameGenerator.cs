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
using Textify.Data;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    public class NameGenerator : IFixture
    {
        public string FixtureID => "NameGenerator";
        public void RunFixture()
        {
            DataInitializer.Initialize(DataType.Names | DataType.NamesFemale | DataType.NamesMale | DataType.NamesFemaleImplicit | DataType.NamesMaleImplicit | DataType.NamesNatural);
            string[] names = NameGen.NameGenerator.GenerateNames(10);
            string[] firstNames = NameGen.NameGenerator.GenerateFirstNames(10);
            string[] firstFemaleNames = NameGen.NameGenerator.GenerateFirstNames(10, NameGen.NameGenderType.Female);
            string[] firstMaleNames = NameGen.NameGenerator.GenerateFirstNames(10, NameGen.NameGenderType.Male);
            string[] firstFemaleImplicitNames = NameGen.NameGenerator.GenerateFirstNames(10, NameGen.NameGenderType.FemaleImplicit);
            string[] firstMaleImplicitNames = NameGen.NameGenerator.GenerateFirstNames(10, NameGen.NameGenderType.MaleImplicit);
            string[] firstNaturalNames = NameGen.NameGenerator.GenerateFirstNames(10, NameGen.NameGenderType.Natural);
            string[] surnames = NameGen.NameGenerator.GenerateLastNames(10);

            Console.WriteLine($"10 names: {string.Join(", ", names)}");
            Console.WriteLine($"10 first names: {string.Join(", ", firstNames)}");
            Console.WriteLine($"10 first female names: {string.Join(", ", firstFemaleNames)}");
            Console.WriteLine($"10 first male names: {string.Join(", ", firstMaleNames)}");
            Console.WriteLine($"10 first implicit female names: {string.Join(", ", firstFemaleImplicitNames)}");
            Console.WriteLine($"10 first implicit male names: {string.Join(", ", firstMaleImplicitNames)}");
            Console.WriteLine($"10 first natural names: {string.Join(", ", firstNaturalNames)}");
            Console.WriteLine($"10 last names: {string.Join(", ", surnames)}");
        }
    }
}
