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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Accessibility;
using Textify.General;

namespace Textify.Tests.Accessibility
{
    [TestClass]
    public class BrailleBuilderTests
    {
        [TestMethod]
        [DataRow(null, "")]
        [DataRow("", "")]
        [DataRow("Hello world!", "⠓⠑⠇⠇⠕⠀⠺⠕⠗⠇⠙⠖")]
        [DataRow("940 centimeters, or 940cm", "⠼⠊⠙⠚⠀⠉⠑⠝⠞⠊⠍⠑⠞⠑⠗⠎⠂⠀⠕⠗⠀⠼⠊⠙⠚⠆⠉⠍")]
        [DataRow(" 940 centimeters, or 940cm", "⠀⠼⠊⠙⠚⠀⠉⠑⠝⠞⠊⠍⠑⠞⠑⠗⠎⠂⠀⠕⠗⠀⠼⠊⠙⠚⠆⠉⠍")]
        [DataRow("In 940 centimeters, or 940cm", "⠊⠝⠀⠼⠊⠙⠚⠀⠉⠑⠝⠞⠊⠍⠑⠞⠑⠗⠎⠂⠀⠕⠗⠀⠼⠊⠙⠚⠆⠉⠍")]
        [DataRow("940", "⠼⠊⠙⠚")]
        [DataRow("940cm", "⠼⠊⠙⠚⠆⠉⠍")]
        [DataRow("940\ncm", "⠼⠊⠙⠚\n⠉⠍")]
        public void TestBuildBraille(string target, string expected)
        {
            string braille = BrailleBuilder.ToBraille(target);
            braille.UnixifyNewLines().ShouldBe(expected);
        }
    }
}
