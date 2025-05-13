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

using Terminaux.Inputs.TestFixtures;
using Terminaux.Inputs.TestFixtures.Tools;
using Textify.Demos.Offline.Fixtures;

namespace Textify.Demos.Offline
{
    public class Entry
    {
        private static readonly Fixture[] fixtures =
        [
            new FixtureUnconditional(nameof(AnalyzeSpaces), "Analyzes the spaces", AnalyzeSpaces.Test),
            new FixtureUnconditional(nameof(FigletPrint), "Prints the figlet text", FigletPrint.Test),
            new FixtureUnconditional(nameof(FigletPrintWrap), "Prints the wrapped figlet text", FigletPrintWrap.Test),
            new FixtureUnconditional(nameof(NameGenerator), "Generates 10 names from all types", NameGenerator.Test),
            new FixtureUnconditional(nameof(QueryUnicode), "Queries a Unicode character and gets info", QueryUnicode.Test),
            new FixtureUnconditional(nameof(RepairSpaces), "Repairs the spaces", RepairSpaces.Test),
            new FixtureUnconditional(nameof(VersionInfo), "Parses SemVer", VersionInfo.Test),
            new FixtureUnconditional(nameof(VersionInfoRev), "Parses SemVer with revision part support", VersionInfoRev.Test),
            new FixtureUnconditional(nameof(WordGet), "Gets 10 words from both the common and the normal word lists", WordGet.Test),
        ];

        static void Main()
        {
            FixtureSelector.OpenFixtureSelector(fixtures);
        }
    }
}
