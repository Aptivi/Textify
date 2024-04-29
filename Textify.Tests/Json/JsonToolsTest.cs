﻿//
// Nitrocid KS  Copyright (C) 2018-2024  Aptivi
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

using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Json;

namespace Textify.Tests.Json
{

    [TestClass]
    public class JsonToolsTest
    {

        private static readonly string demoJson =
            """
            {
              "LyricsPath": "C:/Users/MyUser/Music/"
            }
            """;
        private static readonly string minifiedJson =
            """
            {"LyricsPath":"C:/Users/MyUser/Music/"}
            """;

        /// <summary>
        /// Tests beautifying the JSON text
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestBeautifyJsonText()
        {
            string Beautified = JsonTools.BeautifyJsonText(minifiedJson);
            Beautified.ShouldNotBeEmpty();
            Beautified.ShouldBe(demoJson);
        }

        /// <summary>
        /// Tests beautifying the JSON text (already done)
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestBeautifyJsonTextAlreadyDone()
        {
            string Beautified = JsonTools.BeautifyJsonText(demoJson);
            Beautified.ShouldNotBeEmpty();
            Beautified.ShouldBe(demoJson);
        }

        /// <summary>
        /// Tests minifying the JSON text
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestMinifyJsonText()
        {
            string Minified = JsonTools.MinifyJsonText(demoJson);
            Minified.ShouldNotBeEmpty();
            Minified.ShouldBe(minifiedJson);
        }

        /// <summary>
        /// Tests minifying the JSON text (already done)
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestMinifyJsonTextAlreadyDone()
        {
            string Minified = JsonTools.MinifyJsonText(minifiedJson);
            Minified.ShouldNotBeEmpty();
            Minified.ShouldBe(minifiedJson);
        }

        private static readonly JToken compareObjectSourceJson =
            JToken.Parse(
                """
                {
                  "LyricsPath": "C:/Users/MyUser/Music/",
                  "DebugPath": "C:/Users/MyUser/Debug/",
                  "DebugPath2": "C:/Users/MyUser/Debug2/",
                  "DebugPath3": "C:/Users/MyUser/Debug3/",
                  "DebugPath4": "C:/Users/MyUser/Debug4/",
                }
                """
            );
        private static readonly JToken compareObjectTargetJson =
            JToken.Parse(
                """
                {
                  "LyricsPath": "C:/Users/MyUser/Music/",
                  "DebugPath": "C:/Users/MyUser/Debug2/",
                  "DebugPath2": "C:/Users/MyUser/Debug/",
                  "DebugPath3": "C:/Users/MyUser/Debug3/",
                  "DebugPath5": "C:/Users/MyUser/Debug4/",
                }
                """
            );

        /// <summary>
        /// Tests finding a difference between two different objects
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestFindDifferenceObjectDifferent()
        {
            var diffObject = JsonTools.FindDifferences(compareObjectSourceJson, compareObjectTargetJson);
            diffObject.ShouldNotBeNull();
            diffObject.Count.ShouldBe(5);
            diffObject["+DebugPath5"].ShouldNotBeNull();
            diffObject["-DebugPath4"].ShouldNotBeNull();
            diffObject["*DebugPath"].ShouldNotBeNull();
            diffObject["*DebugPath2"].ShouldNotBeNull();
            diffObject["*DebugPath5"].ShouldNotBeNull();
            ((JProperty)diffObject["+DebugPath5"].First).Name.ShouldBe("+");
            ((JProperty)diffObject["-DebugPath4"].First).Name.ShouldBe("-");
            ((JProperty)diffObject["*DebugPath"].First).Name.ShouldBe("*");
            ((JProperty)diffObject["*DebugPath2"].First).Name.ShouldBe("*");
            ((JProperty)diffObject["*DebugPath5"].First).Name.ShouldBe("*");
        }

        /// <summary>
        /// Tests finding a difference between two identical objects
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestFindDifferenceObjectIdentical()
        {
            var diffObject = JsonTools.FindDifferences(compareObjectSourceJson, compareObjectSourceJson);
            diffObject.ShouldNotBeNull();
            diffObject.Count.ShouldBe(0);
        }

        private static readonly JToken compareArraySourceJson =
            JToken.Parse(
                """
                [
                  "C:/Users/MyUser/Music/",
                  "C:/Users/MyUser/Debug/",
                  "C:/Users/MyUser/Debug2/",
                  "C:/Users/MyUser/Debug3/",
                  "C:/Users/MyUser/Debug4/",
                ]
                """
            );
        private static readonly JToken compareArrayTargetJson =
            JToken.Parse(
                """
                [
                  "C:/Users/MyUser/Music/",
                  "C:/Users/MyUser/Debug/",
                  "C:/Users/MyUser/Debug2/",
                ]
                """
            );

        /// <summary>
        /// Tests finding a difference between two different arrays
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestFindDifferenceArrayDifferent()
        {
            var diffArray = JsonTools.FindDifferences(compareArraySourceJson, compareArrayTargetJson);
            diffArray.ShouldNotBeNull();
            diffArray.Count.ShouldBe(2);
            diffArray["+"].ShouldNotBeNull();
            diffArray["-"].ShouldNotBeNull();
            ((JArray)diffArray["+"]).Count.ShouldBe(0);
            ((JArray)diffArray["-"]).Count.ShouldBe(2);
        }

        /// <summary>
        /// Tests finding a difference between two identical arrays
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestFindDifferenceArrayIdentical()
        {
            var diffArray = JsonTools.FindDifferences(compareArraySourceJson, compareArraySourceJson);
            diffArray.ShouldNotBeNull();
            diffArray.Count.ShouldBe(0);
        }

        private static readonly JToken compareOtherSourceJson =
            """
            test
            """;
        private static readonly JToken compareOtherTargetJson =
            """
            test2
            """;

        /// <summary>
        /// Tests finding a difference between two different other objects
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestFindDifferenceOtherDifferent()
        {
            var diffOther = JsonTools.FindDifferences(compareOtherSourceJson, compareOtherTargetJson);
            diffOther.ShouldNotBeNull();
            diffOther.Count.ShouldBe(2);
            diffOther["+"].ShouldNotBeNull();
            diffOther["-"].ShouldNotBeNull();
            diffOther["+"].ShouldBe("test2");
            diffOther["-"].ShouldBe("test");
        }

        /// <summary>
        /// Tests finding a difference between two identical other objects
        /// </summary>
        [TestMethod]
        [Description("Action")]
        public void TestFindDifferenceOtherIdentical()
        {
            var diffOther = JsonTools.FindDifferences(compareOtherSourceJson, compareOtherSourceJson);
            diffOther.ShouldNotBeNull();
            diffOther.Count.ShouldBe(0);
        }

    }

}
