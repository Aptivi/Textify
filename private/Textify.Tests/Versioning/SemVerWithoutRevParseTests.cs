//
// Textify  Copyright (C) 2023-2025  Aptivi
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
using Textify.Versioning;

namespace Textify.Tests.Versioning
{
    [TestClass]
    public class SemVerWithoutRevParseTests
    {
        [TestMethod]
        [DataRow("1.0.0", new object[] { 1, 0, 0, "", "" })]
        [DataRow("1.0.0-alpha1", new object[] { 1, 0, 0, "alpha1", "" })]
        [DataRow("1.0.0+234F234D", new object[] { 1, 0, 0, "", "234F234D" })]
        [DataRow("1.0.0-alpha1+234F234D", new object[] { 1, 0, 0, "alpha1", "234F234D" })]
        [DataRow("0.1.0", new object[] { 0, 1, 0, "", "" })]
        [DataRow("0.1.0-alpha1", new object[] { 0, 1, 0, "alpha1", "" })]
        [DataRow("0.1.0+234F234D", new object[] { 0, 1, 0, "", "234F234D" })]
        [DataRow("0.1.0-alpha1+234F234D", new object[] { 0, 1, 0, "alpha1", "234F234D" })]
        [DataRow("0.0.1", new object[] { 0, 0, 1, "", "" })]
        [DataRow("0.0.1-alpha1", new object[] { 0, 0, 1, "alpha1", "" })]
        [DataRow("0.0.1+234F234D", new object[] { 0, 0, 1, "", "234F234D" })]
        [DataRow("0.0.1-alpha1+234F234D", new object[] { 0, 0, 1, "alpha1", "234F234D" })]
        public void TestSemVer(string version, object[] elements)
        {
            SemVer? semVer = SemVer.ParseWithoutRev(version);
            if (semVer is null)
                Assert.Fail();
            object[] actual =
            [
                semVer.MajorVersion,
                semVer.MinorVersion,
                semVer.PatchVersion,
                semVer.PreReleaseInfo,
                semVer.BuildMetadata
            ];
            actual.ShouldBe(elements);
        }

        [TestMethod]
        [DataRow("1.0.0", "1.0.0", true)]
        [DataRow("1.0.0-alpha1", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0-alpha1", false)]
        [DataRow("1.0.0-alpha1", "1.0.0-alpha1", true)]
        [DataRow("1.0.0+234F234D", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0+234F234D", false)]
        [DataRow("1.0.0+234F234D", "1.0.0+234F234D", true)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0-alpha1+234F234D", false)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", true)]
        public void TestSemVerEquality(string version, string otherVersion, bool expected)
        {
            SemVer? semVer = SemVer.ParseWithoutRev(version);
            SemVer? semVer2 = SemVer.ParseWithoutRev(otherVersion);
            if (semVer is null || semVer2 is null)
                Assert.Fail();
            (semVer == semVer2).ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("0.9.0", "1.0.0", true)]
        [DataRow("0.9.0-alpha1", "1.0.0", true)]
        [DataRow("1.1.0", "1.0.0", false)]
        [DataRow("1.1.0-alpha1", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0", false)]
        [DataRow("1.0.0-alpha1", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0-alpha1", false)]
        [DataRow("1.0.0-alpha1", "1.0.0-alpha1", false)]
        [DataRow("1.0.0+234F234D", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0+234F234D", false)]
        [DataRow("1.0.0+234F234D", "1.0.0+234F234D", false)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0-alpha1+234F234D", false)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", false)]
        public void TestSemVerIsOlderThan(string version, string otherVersion, bool expected)
        {
            SemVer? semVer = SemVer.ParseWithoutRev(version);
            SemVer? semVer2 = SemVer.ParseWithoutRev(otherVersion);
            if (semVer is null || semVer2 is null)
                Assert.Fail();
            (semVer < semVer2).ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("0.9.0", "1.0.0", true)]
        [DataRow("0.9.0-alpha1", "1.0.0", true)]
        [DataRow("1.1.0", "1.0.0", false)]
        [DataRow("1.1.0-alpha1", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0", true)]
        [DataRow("1.0.0-alpha1", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0-alpha1", false)]
        [DataRow("1.0.0-alpha1", "1.0.0-alpha1", true)]
        [DataRow("1.0.0+234F234D", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0+234F234D", false)]
        [DataRow("1.0.0+234F234D", "1.0.0+234F234D", true)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0-alpha1+234F234D", false)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", true)]
        public void TestSemVerIsOlderOrEqualTo(string version, string otherVersion, bool expected)
        {
            SemVer? semVer = SemVer.ParseWithoutRev(version);
            SemVer? semVer2 = SemVer.ParseWithoutRev(otherVersion);
            if (semVer is null || semVer2 is null)
                Assert.Fail();
            (semVer <= semVer2).ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("0.9.0", "1.0.0", false)]
        [DataRow("0.9.0-alpha1", "1.0.0", false)]
        [DataRow("1.1.0", "1.0.0", true)]
        [DataRow("1.1.0-alpha1", "1.0.0", true)]
        [DataRow("1.0.0-alpha1", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0-alpha1", true)]
        [DataRow("1.0.0-alpha1", "1.0.0-alpha1", false)]
        [DataRow("1.0.0+234F234D", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0+234F234D", true)]
        [DataRow("1.0.0+234F234D", "1.0.0+234F234D", false)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0-alpha1+234F234D", true)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", false)]
        public void TestSemVerIsNewerThan(string version, string otherVersion, bool expected)
        {
            SemVer? semVer = SemVer.ParseWithoutRev(version);
            SemVer? semVer2 = SemVer.ParseWithoutRev(otherVersion);
            if (semVer is null || semVer2 is null)
                Assert.Fail();
            (semVer > semVer2).ShouldBe(expected);
        }

        [TestMethod]
        [DataRow("0.9.0", "1.0.0", false)]
        [DataRow("0.9.0-alpha1", "1.0.0", false)]
        [DataRow("1.1.0", "1.0.0", true)]
        [DataRow("1.1.0-alpha1", "1.0.0", true)]
        [DataRow("1.0.0", "1.0.0", true)]
        [DataRow("1.0.0-alpha1", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0-alpha1", true)]
        [DataRow("1.0.0-alpha1", "1.0.0-alpha1", true)]
        [DataRow("1.0.0+234F234D", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0+234F234D", true)]
        [DataRow("1.0.0+234F234D", "1.0.0+234F234D", true)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0", false)]
        [DataRow("1.0.0", "1.0.0-alpha1+234F234D", true)]
        [DataRow("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", true)]
        public void TestSemVerIsNewerOrEqualTo(string version, string otherVersion, bool expected)
        {
            SemVer? semVer = SemVer.ParseWithoutRev(version);
            SemVer? semVer2 = SemVer.ParseWithoutRev(otherVersion);
            if (semVer is null || semVer2 is null)
                Assert.Fail();
            (semVer >= semVer2).ShouldBe(expected);
        }
    }
}
