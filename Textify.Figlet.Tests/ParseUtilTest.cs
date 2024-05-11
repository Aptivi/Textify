// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Figlet.Utilities;

namespace Textify.Figlet.Tests;

public class ParseUtilTest
{
    [TestMethod]
    public void Parse()
    {
        void Test(string s, int expected)
        {
            ParseUtil.TryParse(s, out var actual).ShouldBeTrue();
            actual.ShouldBe(expected);
        }

        void TestFails(string s) =>
            ParseUtil.TryParse(s, out var _).ShouldBeFalse();

        Test("1234", 1234);
        Test("1234 ", 1234);
        Test("1234  ", 1234);
        Test("0X4D2", 1234);
        Test("0h4D2", 1234);
        Test("0x4d2", 1234);
        Test("0x4D2  ", 1234);
        Test("02322", 1234);
        Test("02322  ", 1234);
        Test("002322  ", 1234);
        Test("0002322  ", 1234);

        Test("-1234", -1234);
        Test("-1234 ", -1234);
        Test("-1234  ", -1234);
        Test("-0X4D2", -1234);
        Test("-0h4D2", -1234);
        Test("-0x4d2", -1234);
        Test("-0x4D2  ", -1234);
        Test("-02322", -1234);
        Test("-02322  ", -1234);
        Test("-002322  ", -1234);
        Test("-0002322  ", -1234);

        Test(" 1234", 1234);
        Test(" 1234 ", 1234);
        Test("  1234  ", 1234);
        Test(" 0X4D2", 1234);
        Test(" 0h4D2", 1234);
        Test(" 0x4d2", 1234);
        Test(" 0x4D2  ", 1234);
        Test(" 02322", 1234);
        Test(" 02322  ", 1234);
        Test(" 002322  ", 1234);
        Test(" 0002322  ", 1234);

        Test("0", 0);
        Test("00", 0);
        Test("000", 0);
        Test("0x0", 0);
        Test(" 0 ", 0);
        Test(" 00 ", 0);
        Test(" 000 ", 0);
        Test(" 0x0 ", 0);

        TestFails("Hello");
        TestFails("0Hello");
        TestFails("0xx1234");
        TestFails("04D2");
        TestFails("4D2");
        TestFails("098LKJ");
        TestFails("0x");
        TestFails("0x ");
        TestFails(" 0x ");
        TestFails("- 123");
        TestFails("--123");
    }
}
