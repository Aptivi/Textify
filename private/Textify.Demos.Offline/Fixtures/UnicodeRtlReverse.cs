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

using Terminaux.Base.Extensions;
using Terminaux.Writer.ConsoleWriters;
using Textify.Data.Unicode;

namespace Textify.Demos.Offline.Fixtures
{
    public static class UnicodeRtlReverse
    {
        public static void Test()
        {
            // Let the emulator reverse the text
            ConsoleMisc.TerminalReversesRtlText = true;

            // Print the normal phrase
            string normalPhrase = "السلام عليكم! Terminaux رائعٌ!";
            TextWriterColor.Write(normalPhrase);

            // Now, reverse the RTL characters
            string reversedPhrase = UnicodeTools.ReverseRtl(normalPhrase);
            TextWriterColor.Write(reversedPhrase);

            // Restore original settings
            ConsoleMisc.TerminalReversesRtlText = false;
        }
    }
}
