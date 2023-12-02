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
using System.Text;
using Textify.SpaceManager;
using Textify.SpaceManager.Analysis;
using Textify.SpaceManager.Conversion;

namespace Textify.Tests.SpaceManager
{
    public class ConversionTests
    {
        [Test]
        public void TestConvertSpacesNormal()
        {
            //                  v~~~~ This is a normal space
            string text = "Hello world!";
            byte[] expectedBytes = Encoding.UTF8.GetBytes(text);
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBe(expectedBytes);
            result.Length.ShouldBe(expectedBytes.Length);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpace()
        {
            //                  v~~~~ This is a non-breaking space
            string text = "Hello world!";
            byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello world!");
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBe(expectedBytes);
            result.Length.ShouldBe(expectedBytes.Length);
            result.ShouldNotContain((byte)'\u00a0');
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpaces()
        {
            //                  v~~~v~~~~~~~v~~v~~~v~~~~ These are the non-breaking spaces
            string text = "Hello and welcome to the world!";
            byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello and welcome to the world!");
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBe(expectedBytes);
            result.Length.ShouldBe(expectedBytes.Length);
            result.ShouldNotContain((byte)'\u00a0');
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceExplicit()
        {
            //                  vvvvvv~~~~ This is a non-breaking space
            string text = "Hello\u00a0world!";
            byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello world!");
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBe(expectedBytes);
            result.Length.ShouldBe(expectedBytes.Length);
            result.ShouldNotContain((byte)'\u00a0');
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesExplicit()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u00a0welcome\u00a0to\u00a0the\u00a0world!";
            byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello and welcome to the world!");
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBe(expectedBytes);
            result.Length.ShouldBe(expectedBytes.Length);
            result.ShouldNotContain((byte)'\u00a0');
        }

        [Test]
        public void TestConvertSpacesWithBadSpacesExplicit()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~~ This is a bad space
                string text = $"Hello{whiteSpace}world!";
                byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello world!");
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
                result.ShouldNotBeNull();
                result.ShouldNotBeEmpty();
                result.ShouldBe(expectedBytes);
                result.Length.ShouldBe(expectedBytes.Length);
                result.ShouldNotContain((byte)whiteSpace);
            }
        }

        [Test]
        public void TestConvertSpacesWithMultipleBadSpacesExplicit()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~~~~vvvvvvvvvvvv~~vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~ These are bad spaces
                string text = $"Hello{whiteSpace}and{whiteSpace}welcome{whiteSpace}to{whiteSpace}the{whiteSpace}world!";
                byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello and welcome to the world!");
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
                result.ShouldNotBeNull();
                result.ShouldNotBeEmpty();
                result.ShouldBe(expectedBytes);
                result.Length.ShouldBe(expectedBytes.Length);
                result.ShouldNotContain((byte)whiteSpace);
            }
        }

        [Test]
        public void TestConvertSpacesMultipleDifferentSpacesExplicit()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u200Bwelcome\u2008to\u200Bthe\u00a0world!";
            byte[] expectedBytes = Encoding.UTF8.GetBytes("Hello and welcome to the world!");
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            byte[] result = SpaceConversionTools.ConvertSpaces(analysisResult);
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBe(expectedBytes);
            result.Length.ShouldBe(expectedBytes.Length);
            result.ShouldNotContain((byte)'\u00a0');
            result.ShouldNotBeOneOf(Spaces.badSpaces["ZERO WIDTH SPACE"]);
            result.ShouldNotBeOneOf(Spaces.badSpaces["PUNCTUATION SPACE"]);
        }

        [Test]
        public void TestConvertSpacesNormalToText()
        {
            //                  v~~~~ This is a normal space
            string text = "Hello world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceToText()
        {
            //                  v~~~~ This is a non-breaking space
            string text = "Hello world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesToText()
        {
            //                  v~~~v~~~~~~~v~~v~~~v~~~~ These are the non-breaking spaces
            string text = "Hello and welcome to the world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceExplicitToText()
        {
            //                  vvvvvv~~~~ This is a non-breaking space
            string text = "Hello\u00a0world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesExplicitToText()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u00a0welcome\u00a0to\u00a0the\u00a0world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesWithBadSpacesExplicitToText()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~~ This is a bad space
                string text = $"Hello{whiteSpace}world!";
                string expectedResult = "Hello world!";
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
                result.ShouldNotBeNullOrEmpty();
                result.ShouldBe(expectedResult);
            }
        }

        [Test]
        public void TestConvertSpacesWithMultipleBadSpacesExplicitToText()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~~~~vvvvvvvvvvvv~~vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~ These are bad spaces
                string text = $"Hello{whiteSpace}and{whiteSpace}welcome{whiteSpace}to{whiteSpace}the{whiteSpace}world!";
                string expectedResult = "Hello and welcome to the world!";
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
                result.ShouldNotBeNullOrEmpty();
                result.ShouldBe(expectedResult);
            }
        }

        [Test]
        public void TestConvertSpacesMultipleDifferentSpacesExplicitToText()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u200Bwelcome\u2008to\u200Bthe\u00a0world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            string result = SpaceConversionTools.ConvertSpacesToString(analysisResult);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNormalToStream()
        {
            //                  v~~~~ This is a normal space
            string text = "Hello world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            var stream = new MemoryStream();
            SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
            byte[] array = new byte[stream.Length];
            stream.ShouldNotBeNull();
            stream.Length.ShouldBe(expectedResult.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(array, 0, array.Length);
            string result = Encoding.UTF8.GetString(array);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceToStream()
        {
            //                  v~~~~ This is a non-breaking space
            string text = "Hello world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            var stream = new MemoryStream();
            SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
            byte[] array = new byte[stream.Length];
            stream.ShouldNotBeNull();
            stream.Length.ShouldBe(expectedResult.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(array, 0, array.Length);
            string result = Encoding.UTF8.GetString(array);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesToStream()
        {
            //                  v~~~v~~~~~~~v~~v~~~v~~~~ These are the non-breaking spaces
            string text = "Hello and welcome to the world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            var stream = new MemoryStream();
            SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
            byte[] array = new byte[stream.Length];
            stream.ShouldNotBeNull();
            stream.Length.ShouldBe(expectedResult.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(array, 0, array.Length);
            string result = Encoding.UTF8.GetString(array);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceExplicitToStream()
        {
            //                  vvvvvv~~~~ This is a non-breaking space
            string text = "Hello\u00a0world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            var stream = new MemoryStream();
            SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
            byte[] array = new byte[stream.Length];
            stream.ShouldNotBeNull();
            stream.Length.ShouldBe(expectedResult.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(array, 0, array.Length);
            string result = Encoding.UTF8.GetString(array);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesExplicitToStream()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u00a0welcome\u00a0to\u00a0the\u00a0world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            var stream = new MemoryStream();
            SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
            byte[] array = new byte[stream.Length];
            stream.ShouldNotBeNull();
            stream.Length.ShouldBe(expectedResult.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(array, 0, array.Length);
            string result = Encoding.UTF8.GetString(array);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesWithBadSpacesExplicitToStream()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~~ This is a bad space
                string text = $"Hello{whiteSpace}world!";
                string expectedResult = "Hello world!";
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                var stream = new MemoryStream();
                SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
                byte[] array = new byte[stream.Length];
                stream.ShouldNotBeNull();
                stream.Length.ShouldBe(expectedResult.Length);
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(array, 0, array.Length);
                string result = Encoding.UTF8.GetString(array);
                result.ShouldNotBeNullOrEmpty();
                result.ShouldBe(expectedResult);
            }
        }

        [Test]
        public void TestConvertSpacesWithMultipleBadSpacesExplicitToStream()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~~~~vvvvvvvvvvvv~~vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~ These are bad spaces
                string text = $"Hello{whiteSpace}and{whiteSpace}welcome{whiteSpace}to{whiteSpace}the{whiteSpace}world!";
                string expectedResult = "Hello and welcome to the world!";
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                var stream = new MemoryStream();
                SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
                byte[] array = new byte[stream.Length];
                stream.ShouldNotBeNull();
                stream.Length.ShouldBe(expectedResult.Length);
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(array, 0, array.Length);
                string result = Encoding.UTF8.GetString(array);
                result.ShouldNotBeNullOrEmpty();
                result.ShouldBe(expectedResult);
            }
        }

        [Test]
        public void TestConvertSpacesMultipleDifferentSpacesExplicitToStream()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u200Bwelcome\u2008to\u200Bthe\u00a0world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            var stream = new MemoryStream();
            SpaceConversionTools.ConvertSpacesTo(analysisResult, stream);
            byte[] array = new byte[stream.Length];
            stream.ShouldNotBeNull();
            stream.Length.ShouldBe(expectedResult.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(array, 0, array.Length);
            string result = Encoding.UTF8.GetString(array);
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNormalToFile()
        {
            //                  v~~~~ This is a normal space
            string text = "Hello world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
            string result = File.ReadAllText("file.txt");
            File.Delete("file.txt");
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceToFile()
        {
            //                  v~~~~ This is a non-breaking space
            string text = "Hello world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
            string result = File.ReadAllText("file.txt");
            File.Delete("file.txt");
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesToFile()
        {
            //                  v~~~v~~~~~~~v~~v~~~v~~~~ These are the non-breaking spaces
            string text = "Hello and welcome to the world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
            string result = File.ReadAllText("file.txt");
            File.Delete("file.txt");
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesNonBreakingSpaceExplicitToFile()
        {
            //                  vvvvvv~~~~ This is a non-breaking space
            string text = "Hello\u00a0world!";
            string expectedResult = "Hello world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
            string result = File.ReadAllText("file.txt");
            File.Delete("file.txt");
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesMultipleNonBreakingSpacesExplicitToFile()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are the non-breaking spaces
            string text = "Hello\u00a0and\u00a0welcome\u00a0to\u00a0the\u00a0world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
            string result = File.ReadAllText("file.txt");
            File.Delete("file.txt");
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void TestConvertSpacesWithBadSpacesExplicitToFile()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~~ This is a bad space
                string text = $"Hello{whiteSpace}world!";
                string expectedResult = "Hello world!";
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
                string result = File.ReadAllText("file.txt");
                File.Delete("file.txt");
                result.ShouldNotBeNullOrEmpty();
                result.ShouldBe(expectedResult);
            }
        }

        [Test]
        public void TestConvertSpacesWithMultipleBadSpacesExplicitToFile()
        {
            foreach (var badSpace in Spaces.badSpaces)
            {
                char whiteSpace = Encoding.UTF8.GetString(badSpace.Value)[0];

                //                   vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~~~~vvvvvvvvvvvv~~vvvvvvvvvvvv~~~vvvvvvvvvvvv~~~~ These are bad spaces
                string text = $"Hello{whiteSpace}and{whiteSpace}welcome{whiteSpace}to{whiteSpace}the{whiteSpace}world!";
                string expectedResult = "Hello and welcome to the world!";
                var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
                SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
                string result = File.ReadAllText("file.txt");
                File.Delete("file.txt");
                result.ShouldNotBeNullOrEmpty();
                result.ShouldBe(expectedResult);
            }
        }

        [Test]
        public void TestConvertSpacesMultipleDifferentSpacesExplicitToFile()
        {
            //                  vvvvvv~~~vvvvvv~~~~~~~vvvvvv~~vvvvvv~~~vvvvvv~~~~ These are bad spaces
            string text = "Hello\u00a0and\u200Bwelcome\u2008to\u200Bthe\u00a0world!";
            string expectedResult = "Hello and welcome to the world!";
            var analysisResult = SpaceAnalysisTools.AnalyzeSpaces(text);
            SpaceConversionTools.ConvertSpacesTo(analysisResult, "file.txt");
            string result = File.ReadAllText("file.txt");
            File.Delete("file.txt");
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }
    }
}
