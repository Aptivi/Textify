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

using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class RegularExpressionTestsEye
    {
        [TestMethod]
        [DataRow("$eye", "@@@", "@@@")]
        [DataRow("$eyeotherthings", "[[[", "[[[otherthings")]
        [DataRow("other$eyethings", "...", "other...things")]
        [DataRow("other$eye", ";;;", "other;;;")]
        public void Replace_eye_placeholder(string input, string replacement, string expectedOutput)
        {
            string output = RegularExpressions.Eye.Replace(input, replacement);

            output.ShouldBe(expectedOutput);
        }

        [TestMethod]
        [DataRow("", "@@@")]
        [DataRow("otherthings", "[[[")]
        [DataRow("other$things", "...")]
        [DataRow("$other", ";;;")]
        public void Return_original_text_if_no_eye_placeholder(string input, string replacement)
        {
            string output = RegularExpressions.Eye.Replace(input, replacement);

            output.ShouldBe(input);
        }
    }
}
