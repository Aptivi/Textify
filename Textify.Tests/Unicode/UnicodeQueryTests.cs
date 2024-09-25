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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Data.Unicode;

namespace Textify.Tests.Unicode
{
    [TestClass]
    public class UnicodeQueryTests
    {
        [TestMethod]
        [DataRow('A', "LATIN CAPITAL LETTER A")]
        [DataRow('P', "LATIN CAPITAL LETTER P")]
        [DataRow('T', "LATIN CAPITAL LETTER T")]
        [DataRow('V', "LATIN CAPITAL LETTER V")]
        [DataRow('a', "LATIN SMALL LETTER A")]
        [DataRow('p', "LATIN SMALL LETTER P")]
        [DataRow('t', "LATIN SMALL LETTER T")]
        [DataRow('v', "LATIN SMALL LETTER V")]
        public void QueryUnicodeCharacter(char character, string expectedUnicodeName)
        {
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            charInstance.Na.ShouldBe(expectedUnicodeName);
        }

        [TestMethod]
        [DataRow('\r', "CARRIAGE RETURN (CR)")]
        [DataRow('\n', "LINE FEED (LF)")]
        [DataRow('\t', "CHARACTER TABULATION")]
        [DataRow('\b', "BACKSPACE")]
        public void QueryUnicodeControlCharacter(char character, string expectedUnicodeName)
        {
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            charInstance.Na1.ShouldBe(expectedUnicodeName);
        }
    }
}
