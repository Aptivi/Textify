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
using Textify.Data.Unicode;

namespace Textify.Tests.Unicode
{
    [TestClass]
    public class UnicodeToolsTests
    {
        /// <summary>
        /// Tests reversing the right-to-left characters in a string (for terminal printing)
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [DataRow("\u200b", "\u200b")]
        [DataRow("Hello!", "Hello!")]
        [DataRow("H\u200bello!", "H\u200bello!")]

        // Chinese and Korean should not be reversed.
        [DataRow("你好！", "你好！")]
        [DataRow("\u200b你好！", "\u200b你好！")]
        [DataRow("你好!", "你好!")]
        [DataRow("\u200b你好!", "\u200b你好!")]
        [DataRow("Terminaux는 최고입니다!", "Terminaux는 최고입니다!")]
        [DataRow("\u200bTerminaux는 최고입니다!", "\u200bTerminaux는 최고입니다!")]

        // Arabic should be reversed, preserving the order of English characters.
        [DataRow("Terminaux رائع!", "Terminaux عئار!")]
        [DataRow("Terminaux رائع! Terminaux رائع!", "Terminaux عئار! Terminaux عئار!")]
        [DataRow("\u200bTerminaux رائع!", "\u200bTerminaux عئار!")]
        [DataRow("\u200bTerminaux رائع! Terminaux رائع!", "\u200bTerminaux عئار! Terminaux عئار!")]

        // Arabic with formatters. The "Aldammatun (وٌ)" should not be affected.
        [DataRow("Terminaux رائعٌ!", "Terminaux ٌعئار!")]
        [DataRow("Terminaux رائعٌ! Terminaux رائعٌ!", "Terminaux ٌعئار! Terminaux ٌعئار!")]
        [DataRow("\u200bTerminaux رائعٌ!", "\u200bTerminaux ٌعئار!")]
        [DataRow("\u200bTerminaux رائعٌ! Terminaux رائعٌ!", "\u200bTerminaux ٌعئار! Terminaux ٌعئار!")]

        // Emoji should be unaffected.
        [DataRow("😀", "😀")]
        [Description("Querying")]
        public void TestReverseRtl(string sentence, string expectedSentence)
        {
            string actualSentence = UnicodeTools.ReverseRtl(sentence);
            actualSentence.ShouldBe(expectedSentence);
        }
    }
}
