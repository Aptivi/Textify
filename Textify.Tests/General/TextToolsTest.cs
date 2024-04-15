﻿//
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Textify.General;

namespace Textify.Tests.General
{

    [TestClass]
    public class TextToolsTest
    {
        public static IEnumerable<object[]> LrpData
        {
            get
            {
                return
                [
                    ["", TestTools.GetDictionaryFrom([])],
                    ["H", TestTools.GetDictionaryFrom(new() { { 1, 1 } })],
                    ["He", TestTools.GetDictionaryFrom(new() { { 1, 2 }, { 2, 1 } })],
                    ["Hel", TestTools.GetDictionaryFrom(new() { { 1, 3 }, { 2, 3 }, { 3, 1 } })],
                    ["Hell", TestTools.GetDictionaryFrom(new() { { 1, 4 }, { 2, 2 }, { 3, 4 }, { 4, 1 } })],
                    ["Hello", TestTools.GetDictionaryFrom(new() { { 1, 5 }, { 2, 5 }, { 3, 5 }, { 4, 5 }, { 5, 1 } })],
                    ["Hello!", TestTools.GetDictionaryFrom(new() { { 1, 6 }, { 2, 3 }, { 3, 2 }, { 4, 3 }, { 5, 6 }, { 6, 1 } })],
                ];
            }
        }
        
        public static IEnumerable<object[]> LrpDataTwice
        {
            get
            {
                return
                [
                    ["", TestTools.GetDictionaryFrom([])],
                    ["H", TestTools.GetDictionaryFrom(new() { { 1, 1 }, { 2, 1 } })],
                    ["He", TestTools.GetDictionaryFrom(new() { { 1, 2 }, { 2, 1 }, { 3, 2 }, { 4, 1 } })],
                    ["Hel", TestTools.GetDictionaryFrom(new() { { 1, 3 }, { 2, 3 }, { 3, 1 }, { 4, 3 }, { 5, 3 }, { 6, 1 } })],
                    ["Hell", TestTools.GetDictionaryFrom(new() { { 1, 4 }, { 2, 2 }, { 3, 4 }, { 4, 1 }, { 5, 4 }, { 6, 2 }, { 7, 4 }, { 8, 1 } })],
                    ["Hello", TestTools.GetDictionaryFrom(new() { { 1, 5 }, { 2, 5 }, { 3, 5 }, { 4, 5 }, { 5, 1 }, { 6, 5 }, { 7, 5 }, { 8, 5 }, { 9, 5 }, { 10, 1 } })],
                    ["Hello!", TestTools.GetDictionaryFrom(new() { { 1, 6 }, { 2, 3 }, { 3, 2 }, { 4, 3 }, { 5, 6 }, { 6, 1 }, { 7, 6 }, { 8, 3 }, { 9, 2 }, { 10, 3 }, { 11, 6 }, { 12, 1 } })],
                ];
            }
        }

        /// <summary>
        /// Tests replacing last occurrence of a string
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestReplaceLastOccurrence()
        {
            string expected = "Nitrocid is awesome and its features are great!";
            string Source = "Nitrocid is awesome and is great!";
            string Target = "is";
            Source = Source.ReplaceLastOccurrence(Target, "its features are");
            Source.ShouldBe(expected);
        }

        /// <summary>
        /// Tests replacing all specified occurrences of strings with a single string
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestReplaceAll()
        {
            string ExpectedString = "Please test Nitrocid. This sub is a unit test.";
            string TargetString = "Please <replace> Nitrocid. This sub is a unit <replace2>.";
            TargetString = TargetString.ReplaceAll(["<replace>", "<replace2>"], "test");
            TargetString.ShouldBe(ExpectedString);
        }

        /// <summary>
        /// Tests replacing all specified occurrences of strings with multiple strings
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestReplaceAllRange()
        {
            string ExpectedString = "Please test the integrity of Nitrocid. This sub is a unit test.";
            string TargetString = "Please <replace> Nitrocid. This sub is a unit <replace2>.";
            TargetString = TargetString.ReplaceAllRange(["<replace>", "<replace2>"], ["test the integrity of", "test"]);
            TargetString.ShouldBe(ExpectedString);
        }

        /// <summary>
        /// Tests string formatting
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestFormatString()
        {
            string Expected = "Nitrocid KS 0.0.1 first launched 2/22/2018.";
            string Actual = "Nitrocid KS 0.0.1 first launched {0}/{1}/{2}.";
            int Day = 22;
            int Year = 2018;
            int Month = 2;
            Actual = TextTools.FormatString(Actual, Month, Day, Year);
            Actual.ShouldBe(Expected);
        }

        /// <summary>
        /// Tests string formatting with reference to null
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestFormatStringNullReference()
        {
            string Expected = "Nothing is ";
            string Actual = "{0} is {1}";
            Actual = TextTools.FormatString(Actual, "Nothing", null);
            Actual.ShouldBe(Expected);
        }

        /// <summary>
        /// Tests removing the prefix
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestRemovePrefix()
        {
            string expected = "Hello";
            string TargetString = "strHello";
            TargetString.RemovePrefix("str").ShouldBe(expected);
        }

        /// <summary>
        /// Tests removing the suffix
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestRemoveSuffix()
        {
            string expected = "Hello";
            string TargetString = "Hello_guy";
            TargetString.RemoveSuffix("_guy").ShouldBe(expected);
        }

        /// <summary>
        /// Tests releasing a string from double quotes
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [DataRow("\"Hi!\"", "Hi!")]
        [DataRow("'Hi!'", "Hi!")]
        [DataRow("`Hi!`", "Hi!")]
        [DataRow("Hi!", "Hi!")]
        [DataRow("\"Hi! as in \"Hello!\"\"", "Hi! as in \"Hello!\"")]
        [DataRow("\"Hi! as in \"Hello!\"", "Hi! as in \"Hello!")]
        [DataRow("'Hi! as in \"Hello!\"'", "Hi! as in \"Hello!\"")]
        [DataRow("'Hi! as in \"Hello!'", "Hi! as in \"Hello!")]
        [DataRow("`Hi! as in \"Hello!\"`", "Hi! as in \"Hello!\"")]
        [DataRow("`Hi! as in \"Hello!`", "Hi! as in \"Hello!")]
        [DataRow("\"Hi! as in 'Hello!'\"", "Hi! as in 'Hello!'")]
        [DataRow("\"Hi! as in 'Hello!\"", "Hi! as in 'Hello!")]
        [DataRow("'Hi! as in 'Hello!''", "Hi! as in 'Hello!'")]
        [DataRow("'Hi! as in 'Hello!'", "Hi! as in 'Hello!")]
        [DataRow("`Hi! as in 'Hello!'`", "Hi! as in 'Hello!'")]
        [DataRow("`Hi! as in 'Hello!`", "Hi! as in 'Hello!")]
        [DataRow("\"Hi! as in `Hello!`\"", "Hi! as in `Hello!`")]
        [DataRow("\"Hi! as in `Hello!\"", "Hi! as in `Hello!")]
        [DataRow("'Hi! as in `Hello!`'", "Hi! as in `Hello!`")]
        [DataRow("'Hi! as in `Hello!'", "Hi! as in `Hello!")]
        [DataRow("`Hi! as in `Hello!``", "Hi! as in `Hello!`")]
        [DataRow("`Hi! as in `Hello!`", "Hi! as in `Hello!")]
        [DataRow("\"Hi! as in \"Hello!`\"", "Hi! as in \"Hello!`")]
        [DataRow("'Hi! as in \"Hello!`'", "Hi! as in \"Hello!`")]
        [DataRow("`Hi! as in \"Hello!``", "Hi! as in \"Hello!`")]
        [DataRow("\"Hi! as in 'Hello!`\"", "Hi! as in 'Hello!`")]
        [DataRow("'Hi! as in 'Hello!`'", "Hi! as in 'Hello!`")]
        [DataRow("`Hi! as in 'Hello!``", "Hi! as in 'Hello!`")]
        [DataRow("\"Hi! as in `Hello!'\"", "Hi! as in `Hello!'")]
        [DataRow("'Hi! as in `Hello!''", "Hi! as in `Hello!'")]
        [DataRow("`Hi! as in `Hello!'`", "Hi! as in `Hello!'")]
        [DataRow("Hi! as in \"Hello!\"", "Hi! as in \"Hello!\"")]
        [DataRow("Hi! as in \"Hello!", "Hi! as in \"Hello!")]
        [DataRow("Hi! as in 'Hello!'", "Hi! as in 'Hello!'")]
        [DataRow("Hi! as in 'Hello!", "Hi! as in 'Hello!")]
        [DataRow("Hi! as in `Hello!`", "Hi! as in `Hello!`")]
        [DataRow("Hi! as in `Hello!", "Hi! as in `Hello!")]
        [DataRow("\"\"\"", "\"")]
        [DataRow("\"'\"", "'")]
        [DataRow("\"`\"", "`")]
        [DataRow("'\"'", "\"")]
        [DataRow("'''", "'")]
        [DataRow("'`'", "`")]
        [DataRow("`\"`", "\"")]
        [DataRow("`'`", "'")]
        [DataRow("```", "`")]
        [DataRow("\"", "\"")]
        [DataRow("'", "'")]
        [DataRow("`", "`")]
        [DataRow("\"\"\"\"", "\"\"")]
        [DataRow("\"''\"", "''")]
        [DataRow("\"``\"", "``")]
        [DataRow("'\"\"'", "\"\"")]
        [DataRow("''''", "''")]
        [DataRow("'``'", "``")]
        [DataRow("`\"\"`", "\"\"")]
        [DataRow("`''`", "''")]
        [DataRow("````", "``")]
        [DataRow("\"\"", "")]
        [DataRow("''", "")]
        [DataRow("``", "")]
        [DataRow("\"\"`\"", "\"`")]
        [DataRow("\"'`\"", "'`")]
        [DataRow("\"`'\"", "`'")]
        [DataRow("'\"`'", "\"`")]
        [DataRow("''`'", "'`")]
        [DataRow("'`''", "`'")]
        [DataRow("`\"'`", "\"'")]
        [DataRow("`'``", "'`")]
        [DataRow("``'`", "`'")]
        [DataRow("\"'", "\"'")]
        [DataRow("'`", "'`")]
        [DataRow("`'", "`'")]
        [Description("Querying")]
        public void TestReleaseDoubleQuotes(string TargetString, string expected)
        {
            TargetString.ReleaseDoubleQuotes().ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting the enclosed double quotes type from the string
        /// </summary>
        [TestMethod]
        [DataRow("", EnclosedDoubleQuotesType.None)]
        [DataRow("\"Hi!\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi!'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi!`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("Hi!", EnclosedDoubleQuotesType.None)]
        [DataRow("\"Hi! as in \"Hello!\"\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"Hi! as in \"Hello!\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi! as in \"Hello!\"'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'Hi! as in \"Hello!'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi! as in \"Hello!\"`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("`Hi! as in \"Hello!`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"Hi! as in 'Hello!'\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"Hi! as in 'Hello!\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi! as in 'Hello!''", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'Hi! as in 'Hello!'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi! as in 'Hello!'`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("`Hi! as in 'Hello!`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"Hi! as in `Hello!`\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"Hi! as in `Hello!\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi! as in `Hello!`'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'Hi! as in `Hello!'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi! as in `Hello!``", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("`Hi! as in `Hello!`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"Hi! as in \"Hello!`\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi! as in \"Hello!`'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi! as in \"Hello!``", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"Hi! as in 'Hello!`\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi! as in 'Hello!`'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi! as in 'Hello!``", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"Hi! as in `Hello!'\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'Hi! as in `Hello!''", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`Hi! as in `Hello!'`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("Hi! as in \"Hello!\"", EnclosedDoubleQuotesType.None)]
        [DataRow("Hi! as in \"Hello!", EnclosedDoubleQuotesType.None)]
        [DataRow("Hi! as in 'Hello!'", EnclosedDoubleQuotesType.None)]
        [DataRow("Hi! as in 'Hello!", EnclosedDoubleQuotesType.None)]
        [DataRow("Hi! as in `Hello!`", EnclosedDoubleQuotesType.None)]
        [DataRow("Hi! as in `Hello!", EnclosedDoubleQuotesType.None)]
        [DataRow("\"\"\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"'\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"`\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'\"'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'''", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'`'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`\"`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("`'`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("```", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"", EnclosedDoubleQuotesType.None)]
        [DataRow("'", EnclosedDoubleQuotesType.None)]
        [DataRow("`", EnclosedDoubleQuotesType.None)]
        [DataRow("\"\"\"\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"''\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"``\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'\"\"'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("''''", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'``'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`\"\"`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("`''`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("````", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("''", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("``", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"\"`\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"'`\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("\"`'\"", EnclosedDoubleQuotesType.DoubleQuotes)]
        [DataRow("'\"`'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("''`'", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("'`''", EnclosedDoubleQuotesType.SingleQuotes)]
        [DataRow("`\"'`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("`'``", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("``'`", EnclosedDoubleQuotesType.Backticks)]
        [DataRow("\"'", EnclosedDoubleQuotesType.None)]
        [DataRow("'`", EnclosedDoubleQuotesType.None)]
        [DataRow("`'", EnclosedDoubleQuotesType.None)]
        [Description("Querying")]
        public void TestGetEnclosedDoubleQuotesType(string TargetString, EnclosedDoubleQuotesType expected)
        {
            TargetString.GetEnclosedDoubleQuotesType().ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting all indexes of a character
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestAllIndexesOf()
        {
            int expected = 3;
            string Source = "Nitrocid is awesome and is great!";
            string Target = "a";
            int Indexes = Source.AllIndexesOf(Target).Count();
            Indexes.ShouldBe(expected);
        }

        /// <summary>
        /// Tests checking if the string contains any of the target strings.
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestContainsAnyOf()
        {
            string TargetString = "Hello, Nitrocid users!";
            TargetString.ContainsAnyOf(["Nitrocid", "users"]).ShouldBeTrue();
            TargetString.ContainsAnyOf(["Awesome", "developers"]).ShouldBeFalse();
        }

        /// <summary>
        /// Tests checking if the string contains all of the target strings.
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestContainsAllOf()
        {
            string TargetString = "Hello, Extensification users!";
            TargetString.ContainsAllOf(["Extensification", "users"]).ShouldBeTrue();
            TargetString.ContainsAllOf(["Awesome", "developers"]).ShouldBeFalse();
        }

        /// <summary>
        /// Tests checking to see if the string starts with any of the values
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestStartsWithAnyOf()
        {
            string TargetString = "dotnet-hostfxr-3.1 dotnet-hostfxr-5.0 runtime-3.1 runtime-5.0 sdk-3.1 sdk-5.0";
            TargetString.StartsWithAnyOf(["dotnet", "sdk"]).ShouldBeTrue();
        }

        /// <summary>
        /// Tests checking to see if the string starts with all of the values
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestStartsWithAllOf()
        {
            string TargetString = "dotnet-hostfxr-3.1 dotnet-hostfxr-5.0 runtime-3.1 runtime-5.0 sdk-3.1 sdk-5.0";
            TargetString.StartsWithAllOf(["dotnet", "dotnet-hostfxr"]).ShouldBeTrue();
        }

        /// <summary>
        /// Tests checking to see if the string ends with any of the values
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestEndsWithAnyOf()
        {
            string TargetString = "dotnet-hostfxr-3.1 dotnet-hostfxr-5.0 runtime-3.1 runtime-5.0 sdk-3.1 sdk-5.0";
            TargetString.EndsWithAnyOf(["5.0", "3.1"]).ShouldBeTrue();
        }

        /// <summary>
        /// Tests checking to see if the string ends with all of the values
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestEndsWithAllOf()
        {
            string TargetString = "dotnet-hostfxr-3.1 dotnet-hostfxr-5.0 runtime-3.1 runtime-5.0 sdk-3.1 sdk-5.0";
            TargetString.EndsWithAllOf(["5.0", "sdk-5.0"]).ShouldBeTrue();
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesCrLf()
        {
            string TargetString = "First line\r\nSecond line\r\nThird line";
            var TargetArray = TargetString.SplitNewLines();
            TargetArray.Length.ShouldBe(3);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesLf()
        {
            string TargetString = "First line\nSecond line\nThird line";
            var TargetArray = TargetString.SplitNewLines();
            TargetArray.Length.ShouldBe(3);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCr)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesCr()
        {
            string TargetString = "First line\rSecond line\rThird line";
            var TargetArray = TargetString.SplitNewLines();
            TargetArray.Length.ShouldBe(3);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf + vbCr)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesCrLfCr()
        {
            string TargetString = "First line\r\n\rSecond line\r\n\rThird line";
            var TargetArray = TargetString.SplitNewLines();
            TargetArray.Length.ShouldBe(5);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf + vbLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesCrLfLf()
        {
            string TargetString = "First line\r\n\nSecond line\r\n\nThird line";
            var TargetArray = TargetString.SplitNewLines();
            TargetArray.Length.ShouldBe(5);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf + vbCrLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesCrLfCrLf()
        {
            string TargetString = "First line\r\n\r\nSecond line\r\n\r\nThird line";
            var TargetArray = TargetString.SplitNewLines();
            TargetArray.Length.ShouldBe(5);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesOldCrLf()
        {
            string TargetString = "First line\r\nSecond line\r\nThird line";
            var TargetArray = TargetString.SplitNewLinesOld();
            TargetArray.Length.ShouldBe(3);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesOldLf()
        {
            string TargetString = "First line\nSecond line\nThird line";
            var TargetArray = TargetString.SplitNewLinesOld();
            TargetArray.Length.ShouldBe(3);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCr)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesOldCr()
        {
            string TargetString = "First line\rSecond line\rThird line";
            var TargetArray = TargetString.SplitNewLinesOld();
            TargetArray.Length.ShouldNotBe(3);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf + vbCr)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesOldCrLfCr()
        {
            string TargetString = "First line\r\n\rSecond line\r\n\rThird line";
            var TargetArray = TargetString.SplitNewLinesOld();
            TargetArray.Length.ShouldNotBe(5);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf + vbLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesOldCrLfLf()
        {
            string TargetString = "First line\r\n\nSecond line\r\n\nThird line";
            var TargetArray = TargetString.SplitNewLinesOld();
            TargetArray.Length.ShouldBe(5);
        }

        /// <summary>
        /// Tests splitting a string with new lines (vbCrLf + vbCrLf)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitNewLinesOldCrLfCrLf()
        {
            string TargetString = "First line\r\n\r\nSecond line\r\n\r\nThird line";
            var TargetArray = TargetString.SplitNewLinesOld();
            TargetArray.Length.ShouldBe(5);
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotes()
        {
            string TargetString = "First \"Second Third\" Fourth";
            var TargetArray = TargetString.SplitEncloseDoubleQuotes();
            TargetArray.Length.ShouldBe(3);
            TargetArray[1].ShouldBe("Second Third");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed without releasing them
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesNoRelease()
        {
            string TargetString = "First \"Second Third\" Fourth";
            var TargetArray = TargetString.SplitEncloseDoubleQuotesNoRelease();
            TargetArray.Length.ShouldBe(3);
            TargetArray[1].ShouldBe("\"Second Third\"");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed (quotes inside quotes)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesInQuote()
        {
            string TargetString = "First \"Second \\\"quote quote\\\" Third\" Fourth";
            var TargetArray = TargetString.SplitEncloseDoubleQuotes();
            TargetArray.Length.ShouldBe(3);
            TargetArray[1].ShouldBe("Second \\\"quote quote\\\" Third");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed without releasing them (quotes inside quotes)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesNoReleaseInQuote()
        {
            string TargetString = "First \"Second \\\"quote quote\\\" Third\" Fourth";
            var TargetArray = TargetString.SplitEncloseDoubleQuotesNoRelease();
            TargetArray.Length.ShouldBe(3);
            TargetArray[1].ShouldBe("\"Second \\\"quote quote\\\" Third\"");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed (edge case regarding escaped quotes alone)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesEdgeCase()
        {
            string TargetString = @"wrap ""echo \\\""hello\\\""""";
            var TargetArray = TargetString.SplitEncloseDoubleQuotes();
            TargetArray.Length.ShouldBe(2);
            TargetArray[1].ShouldBe(@"echo \\\""hello\\\""");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed without releasing them (edge case regarding escaped quotes alone)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesNoReleaseEdgeCase()
        {
            string TargetString = @"wrap ""echo \\\""hello\\\""""";
            var TargetArray = TargetString.SplitEncloseDoubleQuotesNoRelease();
            TargetArray.Length.ShouldBe(2);
            TargetArray[1].ShouldBe(@"""echo \\\""hello\\\""""");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed (edge case regarding single escape with space)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesEdgeCase2()
        {
            string TargetString = "test\"\0t est";
            var TargetArray = TargetString.SplitEncloseDoubleQuotes();
            TargetArray.Length.ShouldBe(2);
            TargetArray[1].ShouldBe("\0t est");
        }

        /// <summary>
        /// Tests splitting a string with double quotes enclosed (edge case regarding two or more spaces that used to be ignored)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestSplitEncloseDoubleQuotesEdgeCase3()
        {
            string TargetString = "test  est     \"Textify Terminaux\"";
            var TargetArray = TargetString.SplitEncloseDoubleQuotes();
            TargetArray.Length.ShouldBe(3);
            TargetArray[1].ShouldBe("est");
            TargetArray[2].ShouldBe("Textify Terminaux");
        }

        /// <summary>
        /// Tests checking to see if the string is numeric
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestIsStringNumeric()
        {
            string TargetString = "1";
            TextTools.IsStringNumeric(TargetString).ShouldBeTrue();
        }

        /// <summary>
        /// [Counterexample] Tests checking to see if the string is numeric
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestIsStringNumericCounterexample()
        {
            string TargetString = "a";
            TextTools.IsStringNumeric(TargetString).ShouldBeFalse();
        }

        /// <summary>
        /// Gets a BASE64-encoded string
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [DataRow("Hello", "SGVsbG8=")]
        [DataRow("Nitrocid KS", "Tml0cm9jaWQgS1M=")]
        [DataRow("Test text", "VGVzdCB0ZXh0")]
        [DataRow("123456789", "MTIzNDU2Nzg5")]
        [DataRow("Test with :[]:", "VGVzdCB3aXRoIDpbXTo=")]
        [DataRow("Test with this long text", "VGVzdCB3aXRoIHRoaXMgbG9uZyB0ZXh0")]
        [DataRow("Test with this even longer text", "VGVzdCB3aXRoIHRoaXMgZXZlbiBsb25nZXIgdGV4dA==")]
        [Description("Querying")]
        public void TestGetBase64Encoded(string text, string expectedEncoded)
        {
            string actualEncoded = text.GetBase64Encoded();
            actualEncoded.ShouldBe(expectedEncoded);
        }

        /// <summary>
        /// Gets a BASE64-decoded string
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [DataRow("SGVsbG8=", "Hello")]
        [DataRow("Tml0cm9jaWQgS1M=", "Nitrocid KS")]
        [DataRow("VGVzdCB0ZXh0", "Test text")]
        [DataRow("MTIzNDU2Nzg5", "123456789")]
        [DataRow("VGVzdCB3aXRoIDpbXTo=", "Test with :[]:")]
        [DataRow("VGVzdCB3aXRoIHRoaXMgbG9uZyB0ZXh0", "Test with this long text")]
        [DataRow("VGVzdCB3aXRoIHRoaXMgZXZlbiBsb25nZXIgdGV4dA==", "Test with this even longer text")]
        [Description("Querying")]
        public void TestGetBase64Decoded(string text, string expectedDecoded)
        {
            string actualDecoded = text.GetBase64Decoded();
            actualDecoded.ShouldBe(expectedDecoded);
        }

        /// <summary>
        /// Shifts the letters in a string
        /// </summary>
        [TestMethod]
        [DataRow("", -255, "")]
        [DataRow("Hello", -1, "Gdkkn")]
        [DataRow("Hello", 1, "Ifmmp")]
        [DataRow("Hello", -256, "Gdkkn")]
        [DataRow("Hello", 256, "Ifmmp")]
        [DataRow("Hello", -2, "Fcjjm")]
        [DataRow("Hello", 2, "Jgnnq")]
        [DataRow("Hello", -257, "Fcjjm")]
        [DataRow("Hello", 257, "Jgnnq")]
        [Description("Querying")]
        public void TestShiftLetters(string text, int shift, string expectedShifted)
        {
            string actualShifted = text.ShiftLetters(shift);
            actualShifted.ShouldBe(expectedShifted);
        }

        /// <summary>
        /// Tests getting wrapped sentences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetWrappedSentences()
        {
            var sentences = TextTools.GetWrappedSentences("Nitrocid", 4);
            sentences.ShouldNotBeNull();
            sentences.ShouldNotBeEmpty();
            sentences.Length.ShouldBe(2);
            sentences[0].ShouldBe("Nitr");
            sentences[1].ShouldBe("ocid");
        }

        /// <summary>
        /// Tests getting wrapped sentences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetWrappedSentencesIndented()
        {
            var sentences = TextTools.GetWrappedSentences("Nitrocid", 4, 2);
            sentences.ShouldNotBeNull();
            sentences.ShouldNotBeEmpty();
            sentences.Length.ShouldBe(3);
            sentences[0].ShouldBe("Ni");
            sentences[1].ShouldBe("troc");
            sentences[2].ShouldBe("id");
        }

        /// <summary>
        /// Tests getting wrapped sentences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetWrappedSentencesByWords()
        {
            var sentences = TextTools.GetWrappedSentencesByWords("Nitrocid KS kernel sim", 4);
            sentences.ShouldNotBeNull();
            sentences.ShouldNotBeEmpty();
            sentences.Length.ShouldBe(6);
            sentences[0].ShouldBe("Nitr");
            sentences[1].ShouldBe("ocid");
            sentences[2].ShouldBe("KS");
            sentences[3].ShouldBe("kern");
            sentences[4].ShouldBe("el");
            sentences[5].ShouldBe("sim");
        }

        /// <summary>
        /// Tests getting wrapped sentences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetWrappedSentencesByWordsIndented()
        {
            var sentences = TextTools.GetWrappedSentencesByWords("Nitrocid KS kernel sim", 4, 2);
            sentences.ShouldNotBeNull();
            sentences.ShouldNotBeEmpty();
            sentences.Length.ShouldBe(7);
            sentences[0].ShouldBe("Ni");
            sentences[1].ShouldBe("troc");
            sentences[2].ShouldBe("id");
            sentences[3].ShouldBe("KS");
            sentences[4].ShouldBe("kern");
            sentences[5].ShouldBe("el");
            sentences[6].ShouldBe("sim");
        }

        /// <summary>
        /// Tests getting wrapped sentences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetWrappedSentencesByWordsEdgeCase()
        {
            var sentences = TextTools.GetWrappedSentencesByWords("-------------------------------------------------------------------\r\n\r\nTest text\n    \n\n  Test text 2.", 30);
            sentences.ShouldNotBeNull();
            sentences.ShouldNotBeEmpty();
            sentences.Length.ShouldBe(8);
        }

        /// <summary>
        /// Tests truncating...
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestTruncate()
        {
            string expected = "Nitrocid is awesome ...";
            string Source = "Nitrocid is awesome and is great!";
            int Target = 20;
            Source = Source.TruncateString(Target);
            Source.ShouldBe(expected);
        }

        /// <summary>
        /// Tests reversing the order of characters (single line)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestReverseSingleLine()
        {
            string expected = "desreveR";
            string Source = "Reversed";
            Source = Source.Reverse();
            Source.ShouldBe(expected);
        }

        /// <summary>
        /// Tests reversing the order of characters (multiple lines)
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestReverseMultipleLines()
        {
            string expected = $"desreveR{Environment.NewLine}elpitlum{Environment.NewLine}enil";
            string Source = $"Reversed{Environment.NewLine}multiple{Environment.NewLine}line";
            Source = Source.Reverse();
            Source.ShouldBe(expected);
        }

        /// <summary>
        /// Tests making the first character of the string uppercase
        /// </summary>
        [TestMethod]
        public void TestUpperFirst()
        {
            string TargetString = "hello";
            TargetString.UpperFirst().ShouldBe("Hello");
        }

        /// <summary>
        /// Tests making the first character of the string lowercase
        /// </summary>
        [TestMethod]
        public void TestLowerFirst()
        {
            string TargetString = "Hello";
            TargetString.LowerFirst().ShouldBe("hello");
        }

        /// <summary>
        /// Tests making the string have the title case
        /// </summary>
        [TestMethod]
        public void TestToTitleCase()
        {
            string TargetString = "Reconnecting your network to the work connection...";
            TargetString.ToTitleCase().ShouldBe("Reconnecting Your Network to the Work Connection...");
        }

        /// <summary>
        /// Tests getting enclosed word from index that represents a start of a substring (without symbols)
        /// </summary>
        [TestMethod]
        [DataRow("", 0, "")]
        [DataRow("Hello world!", 2, "Hello")]
        [DataRow("Hello world!", 8, "world")]
        [Description("Querying")]
        public void TestGetEnclosedWordFromIndex(string text, int idx, string expected)
        {
            string actual = text.GetEnclosedWordFromIndex(idx);
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting enclosed word from index that represents a start of a substring (with symbols)
        /// </summary>
        [TestMethod]
        [DataRow("", 0, "")]
        [DataRow("Hello world!", 2, "Hello")]
        [DataRow("Hello world!", 8, "world!")]
        [Description("Querying")]
        public void TestGetEnclosedWordFromIndexSymbols(string text, int idx, string expected)
        {
            string actual = text.GetEnclosedWordFromIndex(idx, true);
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests escaping characters
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [DataRow("Hello world!", @"Hello\ world\!")]
        [DataRow("Helloworld", "Helloworld")]
        [Description("Querying")]
        public void TestEscape(string text, string expected)
        {
            string actual = text.Escape();
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests unescaping characters
        /// </summary>
        [TestMethod]
        [DataRow("", "")]
        [DataRow(@"Hello\ world!", "Hello world!")]
        [DataRow("Helloworld!", "Helloworld!")]
        [Description("Querying")]
        public void TestUnescape(string text, string expected)
        {
            string actual = text.Unescape();
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting the index of the enclosed word from index that represents a start of a substring (without symbols)
        /// </summary>
        [TestMethod]
        [DataRow("", 0, -1)]
        [DataRow("!Hello world!", 2, 1)]
        [DataRow("Hello world!", 8, 6)]
        [Description("Querying")]
        public void TestGetIndexOfEnclosedWordFromIndex(string text, int idx, int expected)
        {
            int actual = text.GetIndexOfEnclosedWordFromIndex(idx);
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting the index of the enclosed word from index that represents a start of a substring (with symbols)
        /// </summary>
        [TestMethod]
        [DataRow("", 0, -1)]
        [DataRow("!Hello world!", 2, 0)]
        [DataRow("Hello world!", 8, 6)]
        [Description("Querying")]
        public void TestGetIndexOfEnclosedWordFromIndexSymbols(string text, int idx, int expected)
        {
            int actual = text.GetIndexOfEnclosedWordFromIndex(idx, true);
            actual.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting LRP from string
        /// </summary>
        [TestMethod]
        [DataRow("", 1, 0)]
        [DataRow("", 2, 0)]
        [DataRow("", 3, 0)]
        [DataRow("", 10, 0)]
        [DataRow("H", 1, 1)]
        [DataRow("H", 2, 1)]
        [DataRow("He", 1, 2)]
        [DataRow("He", 2, 1)]
        [DataRow("He", 3, 2)]
        [DataRow("He", 4, 1)]
        [DataRow("Hel", 1, 3)]
        [DataRow("Hel", 2, 3)]
        [DataRow("Hel", 3, 1)]
        [DataRow("Hel", 4, 3)]
        [DataRow("Hel", 5, 3)]
        [DataRow("Hel", 6, 1)]
        [DataRow("Hell", 1, 4)]
        [DataRow("Hell", 2, 2)]
        [DataRow("Hell", 3, 4)]
        [DataRow("Hell", 4, 1)]
        [DataRow("Hell", 5, 4)]
        [DataRow("Hell", 6, 2)]
        [DataRow("Hell", 7, 4)]
        [DataRow("Hell", 8, 1)]
        [DataRow("Hello", 1, 5)]
        [DataRow("Hello", 2, 5)]
        [DataRow("Hello", 3, 5)]
        [DataRow("Hello", 4, 5)]
        [DataRow("Hello", 5, 1)]
        [DataRow("Hello", 6, 5)]
        [DataRow("Hello", 7, 5)]
        [DataRow("Hello", 8, 5)]
        [DataRow("Hello", 9, 5)]
        [DataRow("Hello", 10, 1)]
        [DataRow("Hello!", 1, 6)]
        [DataRow("Hello!", 2, 3)]
        [DataRow("Hello!", 3, 2)]
        [DataRow("Hello!", 4, 3)]
        [DataRow("Hello!", 5, 6)]
        [DataRow("Hello!", 6, 1)]
        [DataRow("Hello!", 7, 6)]
        [DataRow("Hello!", 8, 3)]
        [DataRow("Hello!", 9, 2)]
        [DataRow("Hello!", 10, 3)]
        [DataRow("Hello!", 11, 6)]
        [DataRow("Hello!", 12, 1)]
        [Description("Querying")]
        public void TestLRP(string target, int lrpSteps, int expected)
        {
            int lrp = target.GetLetterRepetitionPattern(lrpSteps);
            lrp.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting LRP table from string
        /// </summary>
        [TestMethod]
        [DynamicData(nameof(LrpData))]
        [Description("Querying")]
        public void TestLRPTable(string target, ReadOnlyDictionary<int, int> expected)
        {
            var lrp = target.GetLetterRepetitionPatternTable();
            lrp.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting LRP table from string (twice)
        /// </summary>
        [TestMethod]
        [DynamicData(nameof(LrpDataTwice))]
        [Description("Querying")]
        public void TestLRPTableTwice(string target, ReadOnlyDictionary<int, int> expected)
        {
            var lrp = target.GetLetterRepetitionPatternTable(true);
            lrp.ShouldBe(expected);
        }

        /// <summary>
        /// Tests getting list of repeated letters without wiping the single letter occurrences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetListOfRepeatedLettersNoWipe()
        {
            var repeated = "Hello!".GetListOfRepeatedLetters();
            repeated.ShouldContainKey('H');
            repeated['H'].ShouldBe(1);
            repeated.ShouldContainKey('e');
            repeated['e'].ShouldBe(1);
            repeated.ShouldContainKey('l');
            repeated['l'].ShouldBe(2);
            repeated.ShouldContainKey('o');
            repeated['o'].ShouldBe(1);
            repeated.ShouldContainKey('!');
            repeated['!'].ShouldBe(1);
        }

        /// <summary>
        /// Tests getting list of repeated letters without wiping the single letter occurrences
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetListOfRepeatedLettersWipe()
        {
            var repeated = "Hello!".GetListOfRepeatedLetters(true);
            repeated.ShouldNotContainKey('H');
            repeated.ShouldNotContainKey('e');
            repeated.ShouldContainKey('l');
            repeated['l'].ShouldBe(2);
            repeated.ShouldNotContainKey('o');
            repeated.ShouldNotContainKey('!');
        }
    }

}
