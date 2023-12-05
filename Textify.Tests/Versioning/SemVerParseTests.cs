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

using Textify.Versioning;

namespace Textify.Tests.Versioning
{
    public class SemVerParseTests
    {
        [Test]
        [TestCase("1.0.0", ExpectedResult = new object[] { 1, 0, 0, "", "" })]
        [TestCase("1.0.0-alpha1", ExpectedResult = new object[] { 1, 0, 0, "alpha1", "" })]
        [TestCase("1.0.0+234F234D", ExpectedResult = new object[] { 1, 0, 0, "", "234F234D" })]
        [TestCase("1.0.0-alpha1+234F234D", ExpectedResult = new object[] { 1, 0, 0, "alpha1", "234F234D" })]
        [TestCase("0.1.0", ExpectedResult = new object[] { 0, 1, 0, "", "" })]
        [TestCase("0.1.0-alpha1", ExpectedResult = new object[] { 0, 1, 0, "alpha1", "" })]
        [TestCase("0.1.0+234F234D", ExpectedResult = new object[] { 0, 1, 0, "", "234F234D" })]
        [TestCase("0.1.0-alpha1+234F234D", ExpectedResult = new object[] { 0, 1, 0, "alpha1", "234F234D" })]
        [TestCase("0.0.1", ExpectedResult = new object[] { 0, 0, 1, "", "" })]
        [TestCase("0.0.1-alpha1", ExpectedResult = new object[] { 0, 0, 1, "alpha1", "" })]
        [TestCase("0.0.1+234F234D", ExpectedResult = new object[] { 0, 0, 1, "", "234F234D" })]
        [TestCase("0.0.1-alpha1+234F234D", ExpectedResult = new object[] { 0, 0, 1, "alpha1", "234F234D" })]
        public object[] TestSemVer(string version)
        {
            SemVer semVer = SemVer.Parse(version);
            return
            [
                semVer.MajorVersion,
                semVer.MinorVersion,
                semVer.PatchVersion,
                semVer.PreReleaseInfo,
                semVer.BuildMetadata
            ];
        }

        [Test]
        [TestCase("1.0.0", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1", ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", ExpectedResult = true)]
        public bool TestSemVerEquality(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer == semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0", ExpectedResult = true)]
        [TestCase("0.9.0-alpha1", "1.0.0", ExpectedResult = true)]
        [TestCase("1.1.0", "1.0.0", ExpectedResult = false)]
        [TestCase("1.1.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1", ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", ExpectedResult = false)]
        public bool TestSemVerIsOlderThan(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer < semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0", ExpectedResult = true)]
        [TestCase("0.9.0-alpha1", "1.0.0", ExpectedResult = true)]
        [TestCase("1.1.0", "1.0.0", ExpectedResult = false)]
        [TestCase("1.1.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1", ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", ExpectedResult = true)]
        public bool TestSemVerIsOlderOrEqualTo(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer <= semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0", ExpectedResult = false)]
        [TestCase("0.9.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.1.0", "1.0.0", ExpectedResult = true)]
        [TestCase("1.1.0-alpha1", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1", ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D", ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", ExpectedResult = false)]
        public bool TestSemVerIsNewerThan(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer > semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0", ExpectedResult = false)]
        [TestCase("0.9.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.1.0", "1.0.0", ExpectedResult = true)]
        [TestCase("1.1.0-alpha1", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1", ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0", ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D", ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D", ExpectedResult = true)]
        public bool TestSemVerIsNewerOrEqualTo(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer >= semVer2;
        }
    }
}
