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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using Textify.Figlet.Utilities.Lines;

namespace Textify.Figlet.Tests
{
    [TestClass]
    public class FigletFontVerificationTest
    {
        public static IEnumerable<object[]> TestFigletFonts
        {
            get
            {
                List<object[]> arguments = [];
                foreach (string font in FigletTools.figletFonts)
                    arguments.Add([font]);
                return arguments;
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestFigletFonts))]
        public void VerifyFont(string fontName)
        {
            FigletFont? font = null;
            Should.NotThrow(() => font = FigletFonts.GetByName(fontName));
            font.ShouldNotBeNull();
            font.Name.ShouldBe(fontName);
        }

        [TestMethod]
        [DynamicData(nameof(TestFigletFonts))]
        public void VerifyFontRender(string fontName)
        {
            FigletFont? font = null;
            Should.NotThrow(() => font = FigletFonts.GetByName(fontName));
            font.ShouldNotBeNull();
            font.Name.ShouldBe(fontName);
            string rendered = "";
            Should.NotThrow(() => rendered = font.Render("tT3!"));
            Console.WriteLine(rendered);
            rendered.ShouldNotBeNullOrEmpty();
        }
    }
}
