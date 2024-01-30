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
using Textify.Data;
using Textify.Words;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    internal class WordGet : IFixture
    {
        public string FixtureID => "WordGet";
        public void RunFixture()
        {
            // Get 10 random words
            DataInitializer.Initialize(DataType.Words);
            for (int i = 0; i < 10; i++)
            {
                string word = WordManager.GetRandomWord();
                Console.WriteLine($"Word {i + 1}: {word}");
            }
        }
    }
}
