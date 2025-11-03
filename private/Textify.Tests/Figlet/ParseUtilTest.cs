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
using Textify.Data.Figlet.Utilities;

namespace Textify.Tests.Figlet
{
    [TestClass]
    public class ParseUtilTest
    {
        [TestMethod]
        [DataRow("1234", 1234)]
        [DataRow("1234 ", 1234)]
        [DataRow("1234  ", 1234)]
        [DataRow("0X4D2", 1234)]
        [DataRow("0h4D2", 1234)]
        [DataRow("0x4d2", 1234)]
        [DataRow("0x4D2  ", 1234)]
        [DataRow("02322", 1234)]
        [DataRow("02322  ", 1234)]
        [DataRow("002322  ", 1234)]
        [DataRow("0002322  ", 1234)]
        [DataRow("-1234", -1234)]
        [DataRow("-1234 ", -1234)]
        [DataRow("-1234  ", -1234)]
        [DataRow("-0X4D2", -1234)]
        [DataRow("-0h4D2", -1234)]
        [DataRow("-0x4d2", -1234)]
        [DataRow("-0x4D2  ", -1234)]
        [DataRow("-02322", -1234)]
        [DataRow("-02322  ", -1234)]
        [DataRow("-002322  ", -1234)]
        [DataRow("-0002322  ", -1234)]
        [DataRow(" 1234", 1234)]
        [DataRow(" 1234 ", 1234)]
        [DataRow("  1234  ", 1234)]
        [DataRow(" 0X4D2", 1234)]
        [DataRow(" 0h4D2", 1234)]
        [DataRow(" 0x4d2", 1234)]
        [DataRow(" 0x4D2  ", 1234)]
        [DataRow(" 02322", 1234)]
        [DataRow(" 02322  ", 1234)]
        [DataRow(" 002322  ", 1234)]
        [DataRow(" 0002322  ", 1234)]
        [DataRow("0", 0)]
        [DataRow("00", 0)]
        [DataRow("000", 0)]
        [DataRow("0x0", 0)]
        [DataRow(" 0 ", 0)]
        [DataRow(" 00 ", 0)]
        [DataRow(" 000 ", 0)]
        [DataRow(" 0x0 ", 0)]
        public void TestParseValid(string s, int expected)
        {
            ParseUtil.TryParse(s, out var actual).ShouldBeTrue();
            actual.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("Hello")]
        [DataRow("0Hello")]
        [DataRow("0xx1234")]
        [DataRow("04D2")]
        [DataRow("4D2")]
        [DataRow("098LKJ")]
        [DataRow("0x")]
        [DataRow("0x ")]
        [DataRow(" 0x ")]
        [DataRow("- 123")]
        [DataRow("--123")]
        public void TestParseInalid(string s) =>
            ParseUtil.TryParse(s, out var _).ShouldBeFalse();
    }
}
