﻿//
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
using Textify.General;
using Textify.General.Structures;

namespace Textify.Tests.General
{

    [TestClass]
    public class WideCharTests
    {

        /// <summary>
        /// Tests getting a WideChar from the string and converting it back to the string
        /// </summary>
        [TestMethod]
        [DataRow("\0")]
        [DataRow("A")]
        [DataRow("\u200b")]
        [DataRow("\U000F200b")]
        [DataRow("😀")]
        [DataRow("🩷")]
        [Description("Querying")]
        public void TestWideCharString(string representation)
        {
            WideChar wideChar = (WideChar)representation;
            string str = wideChar.ToString();
            str.ShouldBe(representation);
        }

        /// <summary>
        /// Tests getting a WideChar from the string, converting this result to WideChar, and comparing between them
        /// </summary>
        [TestMethod]
        [DataRow("\0")]
        [DataRow("A")]
        [DataRow("\u200b")]
        [DataRow("\U000F200b")]
        [DataRow("😀")]
        [DataRow("🩷")]
        [Description("Querying")]
        public void TestWideCharComparison(string representation)
        {
            WideChar wideChar = (WideChar)representation;
            string str = wideChar.ToString();
            WideChar wideChar2 = (WideChar)str;
            wideChar2.ShouldBe(wideChar);
        }

        /// <summary>
        /// Tests getting a WideChar from the string, getting its integral code, and parsing it
        /// </summary>
        [TestMethod]
        [DataRow("\0")]
        [DataRow("A")]
        [DataRow("\u200b")]
        [DataRow("\U000F200b")]
        [DataRow("😀")]
        [DataRow("🩷")]
        [Description("Querying")]
        public void TestWideCharIntCode(string representation)
        {
            WideChar wideChar = (WideChar)representation;
            long code = wideChar;
            WideChar wideChar2 = (WideChar)code;
            wideChar2.ShouldBe(wideChar);
        }

        /// <summary>
        /// Tests getting a WideChar from the string, getting its integral code, and parsing it
        /// </summary>
        [TestMethod]
        [DataRow("\0", 1)]
        [DataRow("A", 1)]
        [DataRow("\u200b", 1)]
        [DataRow("\U000F200b", 2)]
        [DataRow("😀", 2)]
        [DataRow("🩷", 2)]
        [Description("Querying")]
        public void TestWideCharLength(string representation, int expected)
        {
            WideChar wideChar = (WideChar)representation;
            int length = wideChar.GetLength();
            length.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0", false)]
        [DataRow("A", true)]
        [DataRow("\u200b", true)]
        [DataRow("\U000F200b", true)]
        [DataRow("😀", true)]
        [DataRow("🩷", true)]
        [Description("Querying")]
        public void TestWideCharGreaterThan(string representation, bool expected)
        {
            WideChar wideChar = (WideChar)representation;
            bool isGreater = wideChar > (WideChar)0;
            isGreater.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0", true)]
        [DataRow("A", true)]
        [DataRow("\u200b", true)]
        [DataRow("\U000F200b", true)]
        [DataRow("😀", true)]
        [DataRow("🩷", true)]
        [Description("Querying")]
        public void TestWideCharGreaterThanOrEqual(string representation, bool expected)
        {
            WideChar wideChar = (WideChar)representation;
            bool isGreater = wideChar >= (WideChar)0;
            isGreater.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0", false)]
        [DataRow("A", false)]
        [DataRow("\u200b", false)]
        [DataRow("\U000F200b", false)]
        [DataRow("😀", false)]
        [DataRow("🩷", false)]
        [Description("Querying")]
        public void TestWideCharLessThan(string representation, bool expected)
        {
            WideChar wideChar = (WideChar)representation;
            bool isLess = wideChar < (WideChar)0;
            isLess.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0", true)]
        [DataRow("A", false)]
        [DataRow("\u200b", false)]
        [DataRow("\U000F200b", false)]
        [DataRow("😀", false)]
        [DataRow("🩷", false)]
        [Description("Querying")]
        public void TestWideCharLessThanOrEqual(string representation, bool expected)
        {
            WideChar wideChar = (WideChar)representation;
            bool isLess = wideChar <= (WideChar)0;
            isLess.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0", false)]
        [DataRow("A", true)]
        [DataRow("\u200b", false)]
        [DataRow("\U000F200b", false)]
        [DataRow("😀", false)]
        [DataRow("🩷", false)]
        [Description("Querying")]
        public void TestWideCharEquals(string representation, bool expected)
        {
            WideChar wideChar = (WideChar)representation;
            bool isLess = wideChar == (WideChar)"A";
            isLess.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0", true)]
        [DataRow("A", false)]
        [DataRow("\u200b", true)]
        [DataRow("\U000F200b", true)]
        [DataRow("😀", true)]
        [DataRow("🩷", true)]
        [Description("Querying")]
        public void TestWideCharNotEquals(string representation, bool expected)
        {
            WideChar wideChar = (WideChar)representation;
            bool isLess = wideChar != (WideChar)"A";
            isLess.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar addition (add by 1)
        /// </summary>
        [TestMethod]
        [DataRow("\0", "\u0001")]
        [DataRow("A", "B")]
        [DataRow("\u200b", "\u200c")]
        [DataRow("\U000F200b", "\udb88\udc0c")]
        [Description("Querying")]
        public void TestWideCharAddition(string representation, string expected)
        {
            WideChar wideChar = (WideChar)representation;
            WideChar wideChar1 = wideChar + (WideChar)1;
            string wideStr = wideChar1.ToString();
            wideStr.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar subtraction (subtract by 1)
        /// </summary>
        [TestMethod]
        [DataRow("\u0001", "\0")]
        [DataRow("B", "A")]
        [DataRow("\u200c", "\u200b")]
        [DataRow("\U000F200c", "\udb88\udc0b")]
        [Description("Querying")]
        public void TestWideCharSubtraction(string representation, string expected)
        {
            WideChar wideChar = (WideChar)representation;
            WideChar wideChar1 = wideChar - (WideChar)1;
            string wideStr = wideChar1.ToString();
            wideStr.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar addition (add by 1)
        /// </summary>
        [TestMethod]
        [DataRow("\0", "\u0001")]
        [DataRow("A", "B")]
        [DataRow("\u200b", "\u200c")]
        [DataRow("\U000F200b", "\udb88\udc0c")]
        [Description("Querying")]
        public void TestWideCharAdditionAlt(string representation, string expected)
        {
            WideChar wideChar = (WideChar)representation;
            WideChar wideChar1 = (WideChar)(wideChar + 1);
            string wideStr = wideChar1.ToString();
            wideStr.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideChar subtraction (subtract by 1)
        /// </summary>
        [TestMethod]
        [DataRow("\u0001", "\0")]
        [DataRow("B", "A")]
        [DataRow("\u200c", "\u200b")]
        [DataRow("\U000F200c", "\udb88\udc0b")]
        [Description("Querying")]
        public void TestWideCharSubtractionAlt(string representation, string expected)
        {
            WideChar wideChar = (WideChar)representation;
            WideChar wideChar1 = (WideChar)(wideChar - 1);
            string wideStr = wideChar1.ToString();
            wideStr.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting a list of wide characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharGetWideChars()
        {
            string target = "Hello! 😀";
            WideChar[] wideChars = target.GetWideChars();
            wideChars.Length.ShouldBe(8);
        }

        /// <summary>
        /// Tests parsing a wide character
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharParse()
        {
            string target = "😀";
            WideChar wideChar = WideChar.Parse(target);
            string actual = wideChar.ToString();
            actual.ShouldBe(target);
        }

        /// <summary>
        /// Tests parsing a wide character with character code
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharParseWithCode()
        {
            string target = "😀";
            WideChar wideChar = WideChar.Parse(128512);
            string actual = wideChar.ToString();
            actual.ShouldBe(target);
        }

        /// <summary>
        /// Tests parsing a wide character with two characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharParseWithChars()
        {
            string target = "😀";
            WideChar wideChar = WideChar.Parse('\ude00', '\ud83d');
            string actual = wideChar.ToString();
            actual.ShouldBe(target);
        }

        /// <summary>
        /// Tests parsing a wide character
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharTryParse()
        {
            string target = "😀";
            bool result = WideChar.TryParse(target, out WideChar? wideChar);
            string actual = wideChar?.ToString() ?? "";
            result.ShouldBeTrue();
            actual.ShouldBe(target);
        }

        /// <summary>
        /// Tests parsing a wide character with character code
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharTryParseWithCode()
        {
            string target = "😀";
            bool result = WideChar.TryParse(128512, out WideChar? wideChar);
            string actual = wideChar?.ToString() ?? "";
            result.ShouldBeTrue();
            actual.ShouldBe(target);
        }

        /// <summary>
        /// Tests parsing a wide character with two characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideCharTryParseWithChars()
        {
            string target = "😀";
            bool result = WideChar.TryParse('\ude00', '\ud83d', out WideChar? wideChar);
            string actual = wideChar?.ToString() ?? "";
            result.ShouldBeTrue();
            actual.ShouldBe(target);
        }

    }
}
