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
using Textify.Data.Words;

namespace Textify.Demos.Offline.Fixtures
{
    public static class WordGet
    {
        public static void Test()
        {
            // Get 10 random words
            for (int i = 0; i < 10; i++)
            {
                string word = WordManager.GetRandomWord();
                TextWriterColor.Write($"Word {i + 1}: ", false);
                TextWriterColor.WriteColor(word, ConsoleColors.Yellow);
            }

            // Get 10 random common words
            for (int i = 0; i < 10; i++)
            {
                string word = WordManager.GetRandomWord(WordDataType.CommonWords);
                TextWriterColor.Write($"Common {i + 1}: ", false);
                TextWriterColor.WriteColor(word, ConsoleColors.Yellow);
            }
        }
    }
}
