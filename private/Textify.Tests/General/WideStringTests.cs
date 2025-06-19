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
using System.Collections.Generic;
using Textify.General;
using Textify.General.Structures;

namespace Textify.Tests.General
{

    [TestClass]
    public class WideStringTests
    {

        /// <summary>
        /// Tests getting a WideString from the string and converting it back to the string
        /// </summary>
        [TestMethod]
        [DataRow("\0test\0")]
        [DataRow("Atest")]
        [DataRow("\u200btest")]
        [DataRow("\U000F200btest")]
        [DataRow("😀test")]
        [DataRow("🩷test")]
        [DataRow("")]
        [Description("Querying")]
        public void TestWideStringString(string representation)
        {
            WideString wideString = (WideString)representation;
            string str = wideString.ToString();
            str.ShouldBe(representation);
        }

        /// <summary>
        /// Tests getting a WideString from the string, converting this result to WideString, and comparing between them
        /// </summary>
        [TestMethod]
        [DataRow("\0test\0")]
        [DataRow("Atest")]
        [DataRow("\u200btest")]
        [DataRow("\U000F200btest")]
        [DataRow("😀test")]
        [DataRow("🩷test")]
        [DataRow("")]
        [Description("Querying")]
        public void TestWideStringComparison(string representation)
        {
            WideString wideString = (WideString)representation;
            string str = wideString.ToString();
            WideString wideString2 = (WideString)str;
            wideString2.ShouldBe(wideString);
        }

        /// <summary>
        /// Tests getting a WideString length
        /// </summary>
        [TestMethod]
        [DataRow("\0test", 5)]
        [DataRow("Atest", 5)]
        [DataRow("\u200btest", 5)]
        [DataRow("\U000F200btest", 6)]
        [DataRow("😀test", 6)]
        [DataRow("🩷test", 6)]
        [DataRow("", 0)]
        [Description("Querying")]
        public void TestWideStringLength(string representation, int expected)
        {
            WideString wideString = (WideString)representation;
            int length = wideString.Length;
            length.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideString comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0test", false)]
        [DataRow("Atest", false)]
        [DataRow("\u200btest", false)]
        [DataRow("\U000F200btest", true)]
        [DataRow("😀test", false)]
        [DataRow("🩷test", false)]
        [DataRow("", false)]
        [Description("Querying")]
        public void TestWideStringEquals(string representation, bool expected)
        {
            WideString wideString = (WideString)representation;
            bool isLess = wideString == (WideString)"\U000F200btest";
            isLess.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideString comparison
        /// </summary>
        [TestMethod]
        [DataRow("\0test", true)]
        [DataRow("Atest", true)]
        [DataRow("\u200btest", true)]
        [DataRow("\U000F200btest", false)]
        [DataRow("😀test", true)]
        [DataRow("🩷test", true)]
        [DataRow("", true)]
        [Description("Querying")]
        public void TestWideStringNotEquals(string representation, bool expected)
        {
            WideString wideString = (WideString)representation;
            bool isLess = wideString != (WideString)"\U000F200btest";
            isLess.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideString concatenation
        /// </summary>
        [TestMethod]
        [DataRow("\0", "\0test")]
        [DataRow("A", "Atest")]
        [DataRow("\u200b", "\u200btest")]
        [DataRow("\U000F200b", "\U000F200btest")]
        [Description("Querying")]
        public void TestWideStringAddition(string representation, string expected)
        {
            WideString wideString = (WideString)representation;
            WideString wideString1 = wideString + (WideString)"test";
            string wideStr = wideString1.ToString();
            wideStr.ShouldBe(expected);
        }

        /// <summary>
        /// Tests the WideString addition (add by 1)
        /// </summary>
        [TestMethod]
        [DataRow("\0", "\0test")]
        [DataRow("A", "Atest")]
        [DataRow("\u200b", "\u200btest")]
        [DataRow("\U000F200b", "\U000F200btest")]
        [Description("Querying")]
        public void TestWideStringAdditionAlt(string representation, string expected)
        {
            WideString wideString = (WideString)representation;
            WideString wideString1 = (WideString)(wideString + "test");
            string wideStr = wideString1.ToString();
            wideStr.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting a list of wide characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideStringGetWideChars()
        {
            string target = "Hello! 😀";
            WideChar[] wideChars = target.GetWideChars();
            wideChars.Length.ShouldBe(8);
        }

        /// <summary>
        /// Tests getting a list of wide characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideStringGetWideCharsFor()
        {
            WideString target = (WideString)"Hello! 😀";
            var wideChars = new WideChar[8];
            for (int i = 0; i < 8; i++)
            {
                var targetChar = target[i];
                wideChars[i] = targetChar;
            }
            wideChars.Length.ShouldBe(8);
        }

        /// <summary>
        /// Tests getting a list of wide characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestWideStringGetWideCharsForEach()
        {
            WideString target = (WideString)"Hello! 😀";
            var wideChars = new List<WideChar>();
            foreach (var character in target)
                wideChars.Add(character);
            wideChars.Count.ShouldBe(8);
        }

    }
}
