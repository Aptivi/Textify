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

using Terminaux.Colors.Data;
using Terminaux.Inputs;
using Terminaux.Writer.ConsoleWriters;
using Textify.Data.Unicode;

namespace Textify.Demos.Offline.Fixtures
{
    public static class QueryUnicode
    {
        public static void Test()
        {
            // Prompt for a character
            TextWriterColor.Write("Enter a character: ", false);
            char character = Input.ReadKey(true).KeyChar;
            TextWriterRaw.Write();

            // Query it
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            TextWriterColor.Write($"Na (current): ", false);
            TextWriterColor.WriteColor(charInstance.Na, ConsoleColors.Yellow);
            TextWriterColor.Write($"Na1 (Unicode v1): ", false);
            TextWriterColor.WriteColor(charInstance.Na1, ConsoleColors.Yellow);
        }
    }
}
