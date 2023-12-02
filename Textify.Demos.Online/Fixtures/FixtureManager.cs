﻿
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
using System.Linq;
using Textify.Demos.Online.Fixtures.Cases;

namespace Textify.Demos.Online.Fixtures
{
    internal static class FixtureManager
    {
        internal static IFixture[] fixtures =
        [
            // Dictionary
            new Dictionary(),

            // MailAddress
            new Addresstigation(),

            // NameGen
            new NameGen(),

            // Words
            new WordGet(),
        ];

        internal static IFixture GetFixtureFromName(string name)
        {
            if (DoesFixtureExist(name))
            {
                var detectedFixtures = fixtures.Where((fixture) => fixture.FixtureID == name).ToArray();
                return detectedFixtures[0];
            }
            else
                throw new Exception(
                    "Fixture doesn't exist. Available fixtures:\n" +
                    "  - " + string.Join("\n  - ", GetFixtureNames())
                );
        }

        internal static bool DoesFixtureExist(string name)
        {
            var detectedFixtures = fixtures.Where((fixture) => fixture.FixtureID == name);
            return detectedFixtures.Any();
        }

        internal static string[] GetFixtureNames() =>
            fixtures.Select((fixture) => fixture.FixtureID).ToArray();
    }
}
