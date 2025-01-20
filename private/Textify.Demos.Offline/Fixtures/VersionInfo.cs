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

using System;
using Terminaux.Colors.Data;
using Terminaux.Reader;
using Terminaux.Writer.ConsoleWriters;
using Textify.Versioning;

namespace Textify.Demos.Offline.Fixtures
{
    public static class VersionInfo
    {
        public static void Test()
        {
            // Prompt for a version
            string ver = TermReader.Read("Enter a version (0.0.0, 0.0.0-...): ");

            // Query it
            var verInstance = SemVer.Parse(ver);
            if (verInstance is null)
                return;
            TextWriterColor.Write($"  Version (input): ", false);
            TextWriterColor.WriteColor(ver, ConsoleColors.Yellow);
            TextWriterColor.Write($"  Version (gen'd): ", false);
            TextWriterColor.WriteColor($"{verInstance}", ConsoleColors.Yellow);
            TextWriterColor.Write($"  Major: ", false);
            TextWriterColor.WriteColor($"{verInstance.MajorVersion}", ConsoleColors.Yellow);
            TextWriterColor.Write($"  Minor: ", false);
            TextWriterColor.WriteColor($"{verInstance.MinorVersion}", ConsoleColors.Yellow);
            TextWriterColor.Write($"  Revision: ", false);
            TextWriterColor.WriteColor($"{verInstance.RevisionVersion}", ConsoleColors.Yellow);
            TextWriterColor.Write($"  Patch: ", false);
            TextWriterColor.WriteColor($"{verInstance.PatchVersion}", ConsoleColors.Yellow);
            TextWriterColor.Write($"  Special: ", false);
            TextWriterColor.WriteColor(verInstance.SpecialVersion, ConsoleColors.Yellow);
            TextWriterColor.Write($"  Build: ", false);
            TextWriterColor.WriteColor(verInstance.BuildMetadata, ConsoleColors.Yellow);
            TextWriterColor.Write($"  PR Info: ", false);
            TextWriterColor.WriteColor(verInstance.PreReleaseInfo, ConsoleColors.Yellow);
        }
    }
}
