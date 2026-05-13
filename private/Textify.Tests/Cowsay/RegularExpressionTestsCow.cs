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
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class RegularExpressionTestsCow
    {
        [TestMethod]
        [DataRow("default.cow", "default.txt")]
        [DataRow("bill-the-cat.cow", "bill-the-cat.txt")]
        public void Cow_capture_group_only_contains_ascii_art(string cowFileName, string expectedOutputFileName)
        {
            var cowFile = File.ReadAllText(Path.Combine("Cowsay/TestCows", cowFileName));
            var match = RegularExpressions.Cow.Match(cowFile);

            var expectedOutput = File.ReadAllText(Path.Combine("Cowsay/ExpectedOutputCows", expectedOutputFileName));
            match.Groups["cow"].Value.ShouldBe(expectedOutput);
        }
    }
}
