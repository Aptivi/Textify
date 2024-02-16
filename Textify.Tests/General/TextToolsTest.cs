﻿//
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
using System.Linq;
using Textify.General;

namespace Textify.Tests.General
{

    [TestClass]
    public class TextToolsTest
    {

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

    }

}
