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
using Textify.Figlet;

namespace Textify.Demos.Offline.Fixtures.Cases
{
    public class FigletPrint : IFixture
    {
        public string FixtureID => "FigletPrint";
        public void RunFixture()
        {
            string figlet = FigletTools.RenderFiglet("Hello!", "ANSISH_1");
            Console.WriteLine(figlet);
        }
    }
}
