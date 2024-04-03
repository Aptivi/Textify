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
using Textify.Versioning;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    public class VersionInfo : IFixture
    {
        public string FixtureID => "VersionInfo";
        public void RunFixture()
        {
            // Prompt for a version
            Console.Write("Enter a version (0.0.0, 0.0.0-...): ");
            string ver = Console.ReadLine();
            Console.WriteLine();

            // Query it
            var verInstance = SemVer.Parse(ver);
            Console.WriteLine($"Version (input): {ver}");
            Console.WriteLine($"Version (gen'd): {verInstance}");
            Console.WriteLine($"Major: {verInstance.MajorVersion}");
            Console.WriteLine($"Minor: {verInstance.MinorVersion}");
            Console.WriteLine($"Revision: {verInstance.RevisionVersion}");
            Console.WriteLine($"Patch: {verInstance.PatchVersion}");
            Console.WriteLine($"Special: {verInstance.SpecialVersion}");
            Console.WriteLine($"  Build: {verInstance.BuildMetadata}");
            Console.WriteLine($"  PR Info: {verInstance.PreReleaseInfo}");
        }
    }
}
