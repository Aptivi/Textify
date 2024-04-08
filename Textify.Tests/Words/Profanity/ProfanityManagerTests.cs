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
using Textify.Words;
using Textify.Words.Profanity;

namespace Textify.Tests.Words.Profanity
{
    [TestClass]
    public class ProfanityManagerTests
    {
        [TestMethod]
        [DataRow("Who gives a shit? Get off your fucking high horse!", true)]
        [DataRow("Who gives a s h i t? Get off your f u c k ing high horse!", true)]
        [DataRow("Who gives? Get off your high horse! You s u c k   b a l l s!", false)]
        [DataRow("Who gives a ----? Get off your ----ing high horse!", false)]
        public void TestAnalyzeSentenceShallow(string sentence, bool expected)
        {
            var profanities = ProfanityManager.GetProfanities(sentence);
            bool result = profanities.Length > 0;
            result.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("Who gives a shit? Get off your fucking high horse!", true)]
        [DataRow("Who gives a s h i t? Get off your f u c k ing high horse!", true)]
        [DataRow("Who gives? Get off your high horse! You s u c k   b a l l s!", true)]
        [DataRow("Who gives a ----? Get off your ----ing high horse!", false)]
        public void TestAnalyzeSentenceThorough(string sentence, bool expected)
        {
            var profanities = ProfanityManager.GetProfanities(sentence, ProfanitySearchType.Thorough);
            bool result = profanities.Length > 0;
            result.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("Who gives a shit?", true)]
        [DataRow("Who givesashit?", false)]
        [DataRow("Who lives in Scunthorpe?", false)]
        [DataRow("Who are these jackasses from Scunthorpe?", true)]
        public void TestAnalyzeScunthorpeSentenceShallow(string sentence, bool expected)
        {
            var profanities = ProfanityManager.GetProfanities(sentence);
            bool result = profanities.Length > 0;
            result.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("Who gives a shit?", true)]
        [DataRow("Who givesashit?", false)]
        [DataRow("Who lives in Scunthorpe?", false)]
        [DataRow("Who are these jackasses from Scunthorpe?", true)]
        public void TestAnalyzeScunthorpeSentenceThorough(string sentence, bool expected)
        {
            var profanities = ProfanityManager.GetProfanities(sentence, ProfanitySearchType.Thorough);
            bool result = profanities.Length > 0;
            result.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("Who gives a shit?", true)]
        [DataRow("Who givesashit?", true)]
        [DataRow("Who lives in Scunthorpe?", true)]
        [DataRow("Who are these jackasses from Scunthorpe?", true)]
        [DataRow("Who are these j a c k a s s e s from Scunthorpe?", true)]
        public void TestAnalyzeScunthorpeSentencePartial(string sentence, bool expected)
        {
            var profanities = ProfanityManager.GetProfanities(sentence, ProfanitySearchType.Partial);
            bool result = profanities.Length > 0;
            result.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("Who gives a shit?", true)]
        [DataRow("Who givesashit?", true)]
        [DataRow("Who lives in Scunthorpe?", false)]
        [DataRow("Who are these jackasses from Scunthorpe?", true)]
        [DataRow("Who are these j a c k a s s e s from Scunthorpe?", false)]
        public void TestAnalyzeScunthorpeSentencePartialMitigated(string sentence, bool expected)
        {
            var profanities = ProfanityManager.GetProfanities(sentence, ProfanitySearchType.Mitigated);
            bool result = profanities.Length > 0;
            result.ShouldBe(expected);
        }
    }
}
