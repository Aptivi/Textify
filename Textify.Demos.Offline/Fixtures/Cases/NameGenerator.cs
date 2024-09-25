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
using Textify.Data.NameGen;
using NameGen = Textify.Data.NameGen.NameGenerator;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    public class NameGenerator : IFixture
    {
        public string FixtureID => "NameGenerator";
        public void RunFixture()
        {
            string[] names = NameGen.GenerateNames(10);
            string[] firstNames = NameGen.GenerateFirstNames(10);
            string[] firstFemaleNames = NameGen.GenerateFirstNames(10, NameGenderType.Female);
            string[] firstMaleNames = NameGen.GenerateFirstNames(10, NameGenderType.Male);
            string[] firstFemaleImplicitNames = NameGen.GenerateFirstNames(10, NameGenderType.FemaleImplicit);
            string[] firstMaleImplicitNames = NameGen.GenerateFirstNames(10, NameGenderType.MaleImplicit);
            string[] firstNaturalNames = NameGen.GenerateFirstNames(10, NameGenderType.Natural);
            string[] surnames = NameGen.GenerateLastNames(10);
            string[] allNames = NameGen.GetAllFirstNames();
            string[] allSurnames = NameGen.GetAllLastNames();

            Console.WriteLine($"10 names: {string.Join(", ", names)}");
            Console.WriteLine($"10 first names: {string.Join(", ", firstNames)}");
            Console.WriteLine($"10 first female names: {string.Join(", ", firstFemaleNames)}");
            Console.WriteLine($"10 first male names: {string.Join(", ", firstMaleNames)}");
            Console.WriteLine($"10 first implicit female names: {string.Join(", ", firstFemaleImplicitNames)}");
            Console.WriteLine($"10 first implicit male names: {string.Join(", ", firstMaleImplicitNames)}");
            Console.WriteLine($"10 first natural names: {string.Join(", ", firstNaturalNames)}");
            Console.WriteLine($"10 last names: {string.Join(", ", surnames)}");
            Console.WriteLine();
            Console.WriteLine($"...out of {allNames.Length} names, {allSurnames.Length} surnames");
        }
    }
}
