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
using Terminaux.Colors.Data;
using Terminaux.Writer.ConsoleWriters;
using Textify.Data.NameGen;
using NameGen = Textify.Data.NameGen.NameGenerator;

namespace Textify.Demos.Offline.Fixtures
{
    public static class NameGenerator
    {
        public static void Test()
        {
            var names = NameGen.GenerateNames(10);
            var firstNames = NameGen.GenerateFirstNames(10);
            var firstFemaleNames = NameGen.GenerateFirstNames(10, NameGenderType.Female);
            var firstMaleNames = NameGen.GenerateFirstNames(10, NameGenderType.Male);
            var firstFemaleImplicitNames = NameGen.GenerateFirstNames(10, NameGenderType.FemaleImplicit);
            var firstMaleImplicitNames = NameGen.GenerateFirstNames(10, NameGenderType.MaleImplicit);
            var firstNaturalNames = NameGen.GenerateFirstNames(10, NameGenderType.Natural);
            var surnames = NameGen.GenerateLastNames(10);
            var allNames = NameGen.GetAllFirstNames();
            var allSurnames = NameGen.GetAllLastNames();

            TextWriterColor.Write("10 names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", names), ConsoleColors.Yellow);
            TextWriterColor.Write("10 first names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", firstNames), ConsoleColors.Yellow);
            TextWriterColor.Write("10 first female names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", firstFemaleNames), ConsoleColors.Yellow);
            TextWriterColor.Write("10 first male names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", firstMaleNames), ConsoleColors.Yellow);
            TextWriterColor.Write("10 first implicit female names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", firstFemaleImplicitNames), ConsoleColors.Yellow);
            TextWriterColor.Write("10 first implicit male names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", firstMaleImplicitNames), ConsoleColors.Yellow);
            TextWriterColor.Write("10 first natural names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", firstNaturalNames), ConsoleColors.Yellow);
            TextWriterColor.Write("10 last names: ", false);
            TextWriterColor.WriteColor(string.Join(", ", surnames), ConsoleColors.Yellow);
            TextWriterRaw.Write();
            TextWriterColor.WriteColor($"...out of {allNames.Length} names, {allSurnames.Length} surnames", ConsoleColors.Lime);
        }
    }
}
