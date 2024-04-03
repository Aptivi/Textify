//
// Nitrocid KS  Copyright (C) 2018-2023  Aptivi
//
// This file is part of Nitrocid KS
//
// Nitrocid KS is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Nitrocid KS is distributed in the hope that it will be useful,
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

namespace Textify.Tests.General
{

    [TestClass]
    public class CharManagerTests
    {

        /// <summary>
        /// Tests getting all letters and numbers
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllChars()
        {
            var chars = CharManager.GetAllChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldContain('1');
            chars.ShouldContain('\u0662');
        }

        /// <summary>
        /// Tests getting all ASCII letters and numbers
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllCharsAscii()
        {
            var chars = CharManager.GetAllAsciiChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldContain('1');
            chars.ShouldNotContain('\u0662');
        }
        
        /// <summary>
        /// Tests getting all letters and numbers
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLettersAndNumbers()
        {
            var chars = CharManager.GetAllLettersAndNumbers();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldContain('1');
        }

        /// <summary>
        /// Tests getting all letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLetters()
        {
            var chars = CharManager.GetAllLetters();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldNotContain('1');
            chars.ShouldContain('\u019f');
        }

        /// <summary>
        /// Tests getting all numbers
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllNumbers()
        {
            var chars = CharManager.GetAllNumbers();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldNotContain('a');
            chars.ShouldContain('1');
            chars.ShouldContain('\u0662');
        }

        /// <summary>
        /// Tests getting all control characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllControlChars()
        {
            var chars = CharManager.GetAllControlChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('\x1b');
        }

        /// <summary>
        /// Tests getting all real control characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllRealControlChars()
        {
            var chars = CharManager.GetAllRealControlChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('\a');
        }

        /// <summary>
        /// Tests getting all digit characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllDigitChars()
        {
            var chars = CharManager.GetAllDigitChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldNotContain('a');
            chars.ShouldContain('1');
            chars.ShouldContain('\u0662');
        }

        /// <summary>
        /// Tests getting all surrogate characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllSurrogateChars()
        {
            var chars = CharManager.GetAllSurrogateChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('\ud800');
            chars.ShouldContain('\udc00');
        }

        /// <summary>
        /// Tests getting all high surrogate characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllHighSurrogateChars()
        {
            var chars = CharManager.GetAllHighSurrogateChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('\ud800');
            chars.ShouldNotContain('\udc00');
        }

        /// <summary>
        /// Tests getting all low surrogate characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLowSurrogateChars()
        {
            var chars = CharManager.GetAllLowSurrogateChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldNotContain('\ud800');
            chars.ShouldContain('\udc00');
        }

        /// <summary>
        /// Tests getting all lowercase letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLowerChars()
        {
            var chars = CharManager.GetAllLowerChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldNotContain('A');
            chars.ShouldContain('\u014d');
        }

        /// <summary>
        /// Tests getting all uppercase letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllUpperChars()
        {
            var chars = CharManager.GetAllUpperChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('A');
            chars.ShouldNotContain('a');
            chars.ShouldContain('\u014c');
        }

        /// <summary>
        /// Tests getting all punctuation letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllPunctuationChars()
        {
            var chars = CharManager.GetAllPunctuationChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain(',');
            chars.ShouldNotContain('a');
            chars.ShouldContain('\u060a');
        }

        /// <summary>
        /// Tests getting all separator letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllSeparatorChars()
        {
            var chars = CharManager.GetAllSeparatorChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain(' ');
            chars.ShouldNotContain('\t');
            chars.ShouldContain('\u1680');
        }

        /// <summary>
        /// Tests getting all symbol letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllSymbolChars()
        {
            var chars = CharManager.GetAllSymbolChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('+');
            chars.ShouldNotContain('a');
            chars.ShouldContain('\u02c2');
        }

        /// <summary>
        /// Tests getting all whitespace letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllWhitespaceChars()
        {
            var chars = CharManager.GetAllWhitespaceChars();
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain(' ');
            chars.ShouldContain('\t');
            chars.ShouldContain('\u1680');
        }
        
        /// <summary>
        /// Tests getting all letters and numbers
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLettersAndNumbersAscii()
        {
            var chars = CharManager.GetAllLettersAndNumbers(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldContain('1');
            chars.ShouldNotContain('\u0662');
        }

        /// <summary>
        /// Tests getting all letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLettersAscii()
        {
            var chars = CharManager.GetAllLetters(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldNotContain('1');
            chars.ShouldNotContain('\u019f');
        }

        /// <summary>
        /// Tests getting all numbers
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllNumbersAscii()
        {
            var chars = CharManager.GetAllNumbers(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldNotContain('a');
            chars.ShouldContain('1');
            chars.ShouldNotContain('\u0662');
        }

        /// <summary>
        /// Tests getting all digit characters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllDigitCharsAscii()
        {
            var chars = CharManager.GetAllDigitChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldNotContain('a');
            chars.ShouldContain('1');
            chars.ShouldNotContain('\u0662');
        }

        /// <summary>
        /// Tests getting all lowercase letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllLowerCharsAscii()
        {
            var chars = CharManager.GetAllLowerChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('a');
            chars.ShouldNotContain('A');
            chars.ShouldNotContain('\u014d');
        }

        /// <summary>
        /// Tests getting all uppercase letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllUpperCharsAscii()
        {
            var chars = CharManager.GetAllUpperChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('A');
            chars.ShouldNotContain('a');
            chars.ShouldNotContain('\u014c');
        }

        /// <summary>
        /// Tests getting all punctuation letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllPunctuationCharsAscii()
        {
            var chars = CharManager.GetAllPunctuationChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain(',');
            chars.ShouldNotContain('a');
            chars.ShouldNotContain('\u060a');
        }

        /// <summary>
        /// Tests getting all separator letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllSeparatorCharsAscii()
        {
            var chars = CharManager.GetAllSeparatorChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain(' ');
            chars.ShouldNotContain('\t');
            chars.ShouldNotContain('\u1680');
        }

        /// <summary>
        /// Tests getting all symbol letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllSymbolCharsAscii()
        {
            var chars = CharManager.GetAllSymbolChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain('+');
            chars.ShouldNotContain('a');
            chars.ShouldNotContain('\u02c2');
        }

        /// <summary>
        /// Tests getting all whitespace letters
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetAllWhitespaceCharsAscii()
        {
            var chars = CharManager.GetAllWhitespaceChars(false);
            chars.ShouldNotBeNull();
            chars.ShouldNotBeEmpty();
            chars.ShouldContain(' ');
            chars.ShouldContain('\t');
            chars.ShouldNotContain('\u1680');
        }

        /// <summary>
        /// Tests getting escape character
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetEsc()
        {
            var esc = CharManager.GetEsc();
            esc.ShouldBe('\x1b');
        }

        /// <summary>
        /// Tests checking to see if the letter is a real control character
        /// </summary>
        [TestMethod]
        [DataRow('\x00', false)]
        [DataRow('\x07', true)]
        [DataRow('\x08', false)]
        [DataRow('\x0a', false)]
        [DataRow('\x0d', false)]
        [DataRow('\x20', false)]
        [DataRow('a', false)]
        [DataRow('1', false)]
        [Description("Querying")]
        public void TestIsControlChar(char possiblyControlChar, bool expected)
        {
            bool actual = CharManager.IsControlChar(possiblyControlChar);
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests checking to see if the letter is a real control character
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestIsControlCharDynamic()
        {
            for (char ch = (char)0; ch < (char)30; ch++)
            {
                bool expected = ch > (char)0 && ch < (char)8 || ch > (char)13 && ch < (char)26;
                bool actual = CharManager.IsControlChar(ch);
                actual.ShouldBe(expected);
            }
        }
    }

}
