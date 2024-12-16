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
using System.Text;
using Textify.SpaceManager;
using Textify.SpaceManager.Analysis;

namespace Textify.Tests.SpaceManager
{
    [TestClass]
    public class AnalysisTests
    {
        [TestMethod]
        public void TestAnalyzeSpacesNormal()
        {
            //                  v~~~~ This is a normal space
            string text = "Hello world!";
            int length = Encoding.UTF8.GetByteCount(text);
            var result = SpaceAnalysisTools.AnalyzeSpaces(text);
            result.ShouldNotBeNull();
            result.ResultingStream.ShouldNotBeNull();
            result.ResultingStream.Length.ShouldBe(length);
            result.FalseSpaces.ShouldBeEmpty();
        }

        [TestMethod]
        public void TestAnalyzeSpacesNonBreakingSpaceExplicit()
        {
            //                  vvvvvv~~~~ This is a non-breaking space
            string text = "Hello\u00a0world!";
            int length = Encoding.UTF8.GetByteCount(text);
            var result = SpaceAnalysisTools.AnalyzeSpaces(text);
            result.ShouldNotBeNull();
            result.ResultingStream.ShouldNotBeNull();
            result.ResultingStream.Length.ShouldBe(length);
            result.FalseSpaces.ShouldNotBeEmpty();
            result.FalseSpaces[0].Item1.ShouldBe('\u00a0');
            result.FalseSpaces[0].Item2.ShouldBe("NON-BREAKING SPACE");
        }

        [TestMethod]
        public void TestAnalyzeSpacesMultipleNonBreakingSpacesExplicit()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u00a0welcome\u00a0to\u00a0the\u00a0world!";
            int length = Encoding.UTF8.GetByteCount(text);
            var result = SpaceAnalysisTools.AnalyzeSpaces(text);
            result.ShouldNotBeNull();
            result.ResultingStream.ShouldNotBeNull();
            result.ResultingStream.Length.ShouldBe(length);
            result.FalseSpaces.ShouldNotBeEmpty();
            result.FalseSpaces.Length.ShouldBe(1);
            foreach (var space in result.FalseSpaces)
            {
                space.Item1.ShouldBe('\u00a0');
                space.Item2.ShouldBe("NON-BREAKING SPACE");
            }
        }

        [TestMethod]
        public void TestAnalyzeSpacesWithBadSpacesExplicit()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                string spaceName = badSpace.Key;
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~~ This is a bad space
                string text = $"Hello{whiteSpace}world!";
                int length = Encoding.UTF8.GetByteCount(text);
                var result = SpaceAnalysisTools.AnalyzeSpaces(text);
                result.ShouldNotBeNull();
                result.ResultingStream.ShouldNotBeNull();
                result.ResultingStream.Length.ShouldBe(length);
                result.FalseSpaces.ShouldNotBeEmpty();
                result.FalseSpaces[0].Item1.ShouldBe(whiteSpace);
                result.FalseSpaces[0].Item2.ShouldBe(spaceName);
            }
        }

        [TestMethod]
        public void TestAnalyzeSpacesWithMultipleBadSpacesExplicit()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                string spaceName = badSpace.Key;
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~~~~vvvvvvvvvvvv~~vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~ These are bad spaces
                string text = $"Hello{whiteSpace}and{whiteSpace}welcome{whiteSpace}to{whiteSpace}the{whiteSpace}world!";
                int length = Encoding.UTF8.GetByteCount(text);
                var result = SpaceAnalysisTools.AnalyzeSpaces(text);
                result.ShouldNotBeNull();
                result.ResultingStream.ShouldNotBeNull();
                result.ResultingStream.Length.ShouldBe(length);
                result.FalseSpaces.ShouldNotBeEmpty();
                result.FalseSpaces.Length.ShouldBe(1);
                foreach (var space in result.FalseSpaces)
                {
                    space.Item1.ShouldBe(whiteSpace);
                    space.Item2.ShouldBe(spaceName);
                }
            }
        }

        [TestMethod]
        public void TestAnalyzeSpacesMultipleDifferentSpacesExplicit()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the bad spaces
            string text = "Hello\u00a0and\u200Bwelcome\u2008to\u200Bthe\u00a0world!";
            int length = Encoding.UTF8.GetByteCount(text);
            var result = SpaceAnalysisTools.AnalyzeSpaces(text);
            result.ShouldNotBeNull();
            result.ResultingStream.ShouldNotBeNull();
            result.ResultingStream.Length.ShouldBe(length);
            result.FalseSpaces.ShouldNotBeEmpty();
            result.FalseSpaces.Length.ShouldBe(3);
            result.FalseSpaces[0].Item1 = '\u00a0';
            result.FalseSpaces[1].Item1 = '\u200b';
            result.FalseSpaces[2].Item1 = '\u2008';
            result.FalseSpaces[0].Item2 = "NON-BREAKING SPACE";
            result.FalseSpaces[1].Item2 = "ZERO WIDTH SPACE";
            result.FalseSpaces[2].Item2 = "PUNCTUATION SPACE";
        }
    }
}
