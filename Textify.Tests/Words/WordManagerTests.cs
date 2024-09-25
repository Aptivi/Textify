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
using System.Collections.Generic;
using System.Linq;
using Textify.Data.Words;

namespace Textify.Tests.Words
{
    [TestClass]
    public class WordManagerTests
    {
        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void TestKnownBadWordsNotOnCleanList(bool useFull)
        {
            var cleanWords = WordManager.GetWords(useFull ? WordDataType.WordsFull : WordDataType.Words);
            var badWords = WordManager.GetWords(WordDataType.BadWords);
            List<string> falsePositives = [];
            cleanWords.ShouldNotBeNull();
            cleanWords.ShouldNotBeEmpty();
            foreach (var word in cleanWords)
            {
                if (badWords.Contains(word))
                    falsePositives.Add(word);
            }
            falsePositives.ShouldBeEmpty();
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void TestKnownBadWordsOnDirtyList(bool useFull)
        {
            var words = WordManager.GetWords(useFull ? WordDataType.WordsDirtyFull : WordDataType.WordsDirty);
            var badWords = WordManager.GetWords(WordDataType.BadWords);
            List<string> falsePositives = [];
            words.ShouldNotBeNull();
            words.ShouldNotBeEmpty();
            foreach (var word in words)
            {
                if (badWords.Contains(word))
                    falsePositives.Add(word);
            }
            falsePositives.ShouldNotBeEmpty();
            falsePositives.ShouldContain("jackassification");
            falsePositives.ShouldContain("bullshit");
        }
    }
}
