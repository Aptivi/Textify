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
using System;
using System.Text.RegularExpressions;
using Textify.Tools;

namespace Textify.Tests.General
{
    [TestClass]
    public class RegexToolsTest
    {
        [TestMethod]
        [DataRow(@"^[\w.-]+$", true)]
        [DataRow(@"", true)]
        [DataRow("^[\\w.-\"]+$", false)]
        [Description("Action")]
        public void TestIsValidRegex(string pattern, bool expected)
        {
            bool actual = RegexTools.IsValidRegex(pattern);
            actual.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("twitch", @"^[\w.-]+$", true)]
        [DataRow("twi?tch", @"^[\w.-]+$", false)]
        [DataRow("twitch", "", true)]
        [DataRow("", @"^[\w.-]+$", false)]
        [DataRow("", "", true)]
        [Description("Action")]
        public void TestIsMatch(string text, string pattern, bool expected)
        {
            bool actual = RegexTools.IsMatch(text, pattern);
            actual.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("twitch", @"^[\w.-]+$")]
        [DataRow("twi?tch", @"^[\w.-]+$")]
        [DataRow("twitch", "")]
        [DataRow("", @"^[\w.-]+$")]
        [DataRow("", "")]
        [Description("Action")]
        public void TestMatch(string text, string pattern)
        {
            Match? match = default;
            Should.NotThrow(() => match = RegexTools.Match(text, pattern));
        }

        [TestMethod]
        [DataRow("twitch", "^[\\w.-\"]+$")]
        [DataRow("twi?tch", "^[\\w.-\"]+$")]
        [DataRow("", "^[\\w.-\"]+$")]
        [Description("Action")]
        public void TestMatchInvalid(string text, string pattern)
        {
            Match? match = default;
            Should.Throw(() => match = RegexTools.Match(text, pattern), typeof(ArgumentException));
        }

        [TestMethod]
        [DataRow("twitch", @"^[\w.-]+$")]
        [DataRow("twi?tch", @"^[\w.-]+$")]
        [DataRow("twitch", "")]
        [DataRow("", @"^[\w.-]+$")]
        [DataRow("", "")]
        [Description("Action")]
        public void TestMatches(string text, string pattern)
        {
            MatchCollection? match = default;
            Should.NotThrow(() => match = RegexTools.Matches(text, pattern));
        }

        [TestMethod]
        [DataRow("twitch", "^[\\w.-\"]+$")]
        [DataRow("twi?tch", "^[\\w.-\"]+$")]
        [DataRow("", "^[\\w.-\"]+$")]
        [Description("Action")]
        public void TestMatchesInvalid(string text, string pattern)
        {
            MatchCollection? match = default;
            Should.Throw(() => match = RegexTools.Matches(text, pattern), typeof(ArgumentException));
        }

        [TestMethod]
        [DataRow("twitch", @"^[\w.-]+$", "")]
        [DataRow("twi?tch", @"^[\w.-]+$", "twi?tch")]
        [DataRow("twitch", "", "twitch")]
        [DataRow("", @"^[\w.-]+$", "")]
        [DataRow("", "", "")]
        [Description("Action")]
        public void TestFilter(string text, string pattern, string expected)
        {
            string? filtered = default;
            Should.NotThrow(() => filtered = RegexTools.Filter(text, pattern));
            filtered.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("twitch", "^[\\w.-\"]+$")]
        [DataRow("twi?tch", "^[\\w.-\"]+$")]
        [DataRow("", "^[\\w.-\"]+$")]
        [Description("Action")]
        public void TestFilterInvalid(string text, string pattern)
        {
            string? filtered = default;
            Should.Throw(() => filtered = RegexTools.Filter(text, pattern), typeof(ArgumentException));
        }

        [TestMethod]
        [DataRow("twitch", @"^[\w.-]+$", "switch", "switch")]
        [DataRow("twi?tch", @"^[\w.-]+$", "switch", "twi?tch")]
        [DataRow("twitch", "", "switch", "switchtswitchwswitchiswitchtswitchcswitchhswitch")]
        [DataRow("", @"^[\w.-]+$", "switch", "")]
        [DataRow("", "", "switch", "switch")]
        [Description("Action")]
        public void TestFilter(string text, string pattern, string replaceWith, string expected)
        {
            string? filtered = default;
            Should.NotThrow(() => filtered = RegexTools.Filter(text, pattern, replaceWith));
            filtered.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("twitch", "^[\\w.-\"]+$", "switch")]
        [DataRow("twi?tch", "^[\\w.-\"]+$", "switch")]
        [DataRow("", "^[\\w.-\"]+$", "switch")]
        [Description("Action")]
        public void TestFilterInvalid(string text, string pattern, string replaceWith)
        {
            string? filtered = default;
            Should.Throw(() => filtered = RegexTools.Filter(text, pattern, replaceWith), typeof(ArgumentException));
        }

        [TestMethod]
        [DataRow("twitch-switch", @"[st]", new string[] { "", "wi", "ch-", "wi", "ch" })]
        [DataRow("twi?tch-switch", @"[st]", new string[] { "", "wi?", "ch-", "wi", "ch" })]
        [DataRow("twitch", "", new string[] { "", "t", "w", "i", "t", "c", "h", "" })]
        [DataRow("", @"[st]", new string[] { "" })]
        [DataRow("", "", new string[] { "", "" })]
        [Description("Action")]
        public void TestSplit(string text, string pattern, string[] expected)
        {
            string[]? split = default;
            Should.NotThrow(() => split = RegexTools.Split(text, pattern));
            split.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("twitch", "^[\\w.-\"]+$")]
        [DataRow("twi?tch", "^[\\w.-\"]+$")]
        [DataRow("", "^[\\w.-\"]+$")]
        [Description("Action")]
        public void TestSplitInvalid(string text, string pattern)
        {
            string[]? split = default;
            Should.Throw(() => split = RegexTools.Split(text, pattern), typeof(ArgumentException));
        }

        [TestMethod]
        [DataRow(@"Twi\tch", @"Twi\\tch")]
        [DataRow(@"Twi*tch", @"Twi\*tch")]
        [DataRow(@"Twi+tch", @"Twi\+tch")]
        [DataRow(@"Twi?tch", @"Twi\?tch")]
        [DataRow(@"Twi|tch", @"Twi\|tch")]
        [DataRow(@"Twi(tch", @"Twi\(tch")]
        [DataRow(@"Twi{tch", @"Twi\{tch")]
        [DataRow(@"Twi[tch", @"Twi\[tch")]
        [DataRow(@"Twi)tch", @"Twi\)tch")]
        [DataRow(@"Twi^tch", @"Twi\^tch")]
        [DataRow(@"Twi$tch", @"Twi\$tch")]
        [DataRow(@"Twi.tch", @"Twi\.tch")]
        [DataRow(@"Twi#tch", @"Twi\#tch")]
        [DataRow(@"Twi tch", @"Twi\ tch")]
        [Description("Action")]
        public void TestEscape(string text, string expected)
        {
            string? final = default;
            Should.NotThrow(() => final = RegexTools.Escape(text));
            final.ShouldBe(expected);
        }

        [TestMethod]
        [DataRow(@"Twi\\tch", @"Twi\tch")]
        [DataRow(@"Twi\*tch", @"Twi*tch")]
        [DataRow(@"Twi\+tch", @"Twi+tch")]
        [DataRow(@"Twi\?tch", @"Twi?tch")]
        [DataRow(@"Twi\|tch", @"Twi|tch")]
        [DataRow(@"Twi\(tch", @"Twi(tch")]
        [DataRow(@"Twi\{tch", @"Twi{tch")]
        [DataRow(@"Twi\[tch", @"Twi[tch")]
        [DataRow(@"Twi\)tch", @"Twi)tch")]
        [DataRow(@"Twi\^tch", @"Twi^tch")]
        [DataRow(@"Twi\$tch", @"Twi$tch")]
        [DataRow(@"Twi\.tch", @"Twi.tch")]
        [DataRow(@"Twi\#tch", @"Twi#tch")]
        [DataRow(@"Twi\ tch", @"Twi tch")]
        [Description("Action")]
        public void TestUnescape(string text, string expected)
        {
            string? final = default;
            Should.NotThrow(() => final = RegexTools.Unescape(text));
            final.ShouldBe(expected);
        }
    }
}
