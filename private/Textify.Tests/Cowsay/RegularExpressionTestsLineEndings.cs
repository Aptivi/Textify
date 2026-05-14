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

// Originally found in https://github.com/rawsonm88/Cowsay

using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public partial class RegularExpressionTestsLineEndings
    {
        [TestMethod]
        [DataRow("$e\nye", "@@@", "$e@@@ye")]
        [DataRow("$eye\r\notherthi\rngs", "[[[", "$eye[[[otherthi[[[ngs")]
        [DataRow("other$eye\nthings", "...", "other$eye...things")]
        [DataRow("other\r\n$eye\r\n", "\n", "other\n$eye\n")]
        public void Replace_all_new_line_chars(string input, string replacement, string expectedOutput)
        {
            string output = RegularExpressions.LineEndings.Replace(input, replacement);

            output.ShouldBe(expectedOutput);
        }

        [TestMethod]
        [DataRow("", "@@@")]
        [DataRow("otherthings", "[[[")]
        [DataRow("other$things", "...")]
        [DataRow("$other", ";;;")]
        public void Return_original_text_if_no_new_lines(string input, string replacement)
        {
            string output = RegularExpressions.LineEndings.Replace(input, replacement);

            output.ShouldBe(input);
        }
    }
}
