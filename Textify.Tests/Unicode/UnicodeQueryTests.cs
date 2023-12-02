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

using Shouldly;
using Textify.Unicode;

namespace Textify.Tests.Unicode
{
    internal class UnicodeQueryTests
    {
        [Test]
        [TestCase('A', "LATIN CAPITAL LETTER A")]
        [TestCase('P', "LATIN CAPITAL LETTER P")]
        [TestCase('T', "LATIN CAPITAL LETTER T")]
        [TestCase('V', "LATIN CAPITAL LETTER V")]
        [TestCase('a', "LATIN SMALL LETTER A")]
        [TestCase('p', "LATIN SMALL LETTER P")]
        [TestCase('t', "LATIN SMALL LETTER T")]
        [TestCase('v', "LATIN SMALL LETTER V")]
        public void QueryUnicodeCharacter(char character, string expectedUnicodeName)
        {
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            charInstance.Na.ShouldBe(expectedUnicodeName);
        }

        [Test]
        [TestCase('\r', "CARRIAGE RETURN (CR)")]
        [TestCase('\n', "LINE FEED (LF)")]
        [TestCase('\t', "CHARACTER TABULATION")]
        [TestCase('\b', "BACKSPACE")]
        public void QueryUnicodeControlCharacter(char character, string expectedUnicodeName)
        {
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            charInstance.Na1.ShouldBe(expectedUnicodeName);
        }
    }
}
