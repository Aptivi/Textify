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

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Textify.Versioning
{
    /// <summary>
    /// Class containing semantic versioning information
    /// </summary>
    public class SemVer : IEquatable<SemVer>
    {
        private readonly int major = 0;
        private readonly int minor = 0;
        private readonly int patch = 0;
        private readonly int rev = 0;
        private readonly string preReleaseInfo = "";
        private readonly string buildMetadata = "";
        private static readonly Regex revValidator = new(@"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");
        private static readonly Regex normalValidator = new(@"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");

        /// <summary>
        /// Gets the major version part of the version
        /// </summary>
        public int MajorVersion =>
            major;

        /// <summary>
        /// Gets the minor version part of the version
        /// </summary>
        public int MinorVersion =>
            minor;

        /// <summary>
        /// Gets the patch version part of the version
        /// </summary>
        public int PatchVersion =>
            patch;

        /// <summary>
        /// Gets the revision version part of the version
        /// </summary>
        public int RevisionVersion =>
            rev;

        /// <summary>
        /// Gets the pre-release information part of the version
        /// </summary>
        public string PreReleaseInfo =>
            preReleaseInfo;

        /// <summary>
        /// Gets the build metadata part of the version
        /// </summary>
        public string BuildMetadata =>
            buildMetadata;

        /// <summary>
        /// Gets the special part of the version (<see cref="PreReleaseInfo"/> and <see cref="BuildMetadata"/>)
        /// </summary>
        public string SpecialVersion =>
            $"{PreReleaseInfo}{(!string.IsNullOrEmpty(BuildMetadata) ? $"+{BuildMetadata}" : "")}";

        /// <summary>
        /// Makes a new instance of this class
        /// </summary>
        /// <param name="major">Major version</param>
        /// <param name="minor">Minor version</param>
        /// <param name="patch">Patch version</param>
        /// <param name="preReleaseInfo">Info about the pre-release version</param>
        /// <param name="buildMetadata">Info about the build metadata</param>
        protected SemVer(int major, int minor, int patch, string preReleaseInfo, string buildMetadata) :
            this(major, minor, patch, 0, preReleaseInfo, buildMetadata)
        { }

        /// <summary>
        /// Makes a new instance of this class
        /// </summary>
        /// <param name="major">Major version</param>
        /// <param name="minor">Minor version</param>
        /// <param name="patch">Patch version</param>
        /// <param name="rev">Revision version</param>
        /// <param name="preReleaseInfo">Info about the pre-release version</param>
        /// <param name="buildMetadata">Info about the build metadata</param>
        protected SemVer(int major, int minor, int patch, int rev, string preReleaseInfo, string buildMetadata)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
            this.rev = rev;
            this.preReleaseInfo = preReleaseInfo;
            this.buildMetadata = buildMetadata;
        }

        /// <summary>
        /// Parses the semantic version string (revision or not revision)
        /// </summary>
        /// <param name="value">Value that contains a SemVer 2.0 compliant string</param>
        /// <returns>A <see cref="SemVer"/> class instance containing version information.</returns>
        /// <exception cref="SemVerException"></exception>
        public static SemVer? Parse(string value)
        {
            if (HasRevision(value))
                return ParseWithRev(value);
            return ParseWithoutRev(value);
        }

        /// <summary>
        /// Parses the semantic version string (no revision only)
        /// </summary>
        /// <param name="value">Value that contains a SemVer 2.0 compliant string</param>
        /// <returns>A <see cref="SemVer"/> class instance containing version information.</returns>
        /// <exception cref="SemVerException"></exception>
        public static SemVer? ParseWithoutRev(string value)
        {
            // Verify that the semantic versioning string is a valid SemVer string
            MatchCollection matches = normalValidator.Matches(value);
            if (matches.Count == 0)
                throw new SemVerException($"This version [{value}] is not a valid SemVer string.");

            // Now, iterate through the matches to find the version parts
            foreach (Match match in matches)
            {
                GroupCollection matchGroups = match.Groups;
                if (!int.TryParse(matchGroups[1].Value, out int major))
                    throw new SemVerException($"Invalid major version part");
                if (!int.TryParse(matchGroups[2].Value, out int minor))
                    throw new SemVerException($"Invalid minor version part");
                if (!int.TryParse(matchGroups[3].Value, out int patch))
                    throw new SemVerException($"Invalid patch version part");
                string preReleaseInfo = matchGroups[4].Value;
                string buildMetadata = matchGroups[5].Value;
                return new SemVer(major, minor, patch, preReleaseInfo, buildMetadata);
            }
            return null;
        }

        /// <summary>
        /// Parses the semantic version string (revision only)
        /// </summary>
        /// <param name="value">Value that contains a SemVer 2.0 compliant string</param>
        /// <returns>A <see cref="SemVer"/> class instance containing version information.</returns>
        /// <exception cref="SemVerException"></exception>
        public static SemVer? ParseWithRev(string value)
        {
            // Verify that the semantic versioning string is a valid SemVer string
            MatchCollection matches = revValidator.Matches(value);
            if (matches.Count == 0)
                throw new SemVerException($"This version [{value}] is not a valid SemVer string.");

            // Now, iterate through the matches to find the version parts
            foreach (Match match in matches)
            {
                GroupCollection matchGroups = match.Groups;
                if (!int.TryParse(matchGroups[1].Value, out int major))
                    throw new SemVerException($"Invalid major version part");
                if (!int.TryParse(matchGroups[2].Value, out int minor))
                    throw new SemVerException($"Invalid minor version part");
                if (!int.TryParse(matchGroups[3].Value, out int patch))
                    throw new SemVerException($"Invalid patch version part");
                if (!int.TryParse(matchGroups[4].Value, out int rev))
                    throw new SemVerException($"Invalid revision version part");
                string preReleaseInfo = matchGroups[5].Value;
                string buildMetadata = matchGroups[6].Value;
                return new SemVer(major, minor, patch, rev, preReleaseInfo, buildMetadata);
            }
            return null;
        }

        /// <summary>
        /// Converts the version instance to the string representation (a SemVer 2.0 compliant string)
        /// </summary>
        public override string ToString()
        {
            string result;
            if (rev > 0)
                result =
                    $"{MajorVersion}.{MinorVersion}.{PatchVersion}.{RevisionVersion}" +
                    (!string.IsNullOrWhiteSpace(PreReleaseInfo) ? $"-{PreReleaseInfo}" : "") +
                    (!string.IsNullOrWhiteSpace(BuildMetadata) ? $"+{BuildMetadata}" : "");
            else
                result =
                    $"{MajorVersion}.{MinorVersion}.{PatchVersion}" +
                    (!string.IsNullOrWhiteSpace(PreReleaseInfo) ? $"-{PreReleaseInfo}" : "") +
                    (!string.IsNullOrWhiteSpace(BuildMetadata) ? $"+{BuildMetadata}" : "");
            return result;
        }

        /// <summary>
        /// Compares between the two semantic version instances
        /// </summary>
        /// <param name="other">The other semantic version instance to compare this instance with</param>
        /// <returns>
        /// 0 if both versions are equal. 1 if this instance is newer than the other instance. -1 if this instance
        /// is older than the other instance. See Remarks for more info.
        /// </returns>
        /// <remarks>
        /// Normally, what is returned according to the Returns section is normal. However, there are some special
        /// conditions. If the other instance is null, this instance is assumed to be the newer version. If the first
        /// three version parts (major, minor, patch) differ, this function returns immediately with the comparison
        /// results. However, if these equal, it goes on to comparing the special version part (pre-release info and
        /// build metadata info) and returns the following:
        /// <br></br>
        /// <br></br>
        /// * 0 if both the special versions are empty<br></br>
        /// * 1 if this special version is empty<br></br>
        /// * -1 if the other special version is empty
        /// <br></br>
        /// <br></br>
        /// Otherwise, ordinal comparison is performed.
        /// </remarks>
        public int CompareTo(SemVer other)
        {
            if (other is null)
                return 1;

            // First, get the normal version parts and compare both versions
            Version normalVersion = new(MajorVersion, MinorVersion, PatchVersion, RevisionVersion);
            Version normalVersionOther = new(other.MajorVersion, other.MinorVersion, other.PatchVersion, other.RevisionVersion);
            int stageOneCompare = normalVersion.CompareTo(normalVersionOther);
            if (stageOneCompare != 0)
                return stageOneCompare;

            // Same version, but could have different release info
            bool specialVersionEmpty = string.IsNullOrEmpty(SpecialVersion);
            bool specialVersionEmptyOther = string.IsNullOrEmpty(other.SpecialVersion);
            if (specialVersionEmpty && specialVersionEmptyOther)
                return 0;
            if (specialVersionEmpty)
                return 1;
            if (specialVersionEmptyOther)
                return -1;
            return string.CompareOrdinal(SpecialVersion, other.SpecialVersion);
        }

        /// <summary>
        /// Checks to see if the version string has the SemVer revision part or not
        /// </summary>
        /// <param name="version">Version to parse</param>
        /// <returns>True if the version contains the revision part (the fourth part of the version), or false if otherwise.</returns>
        /// <exception cref="SemVerException"></exception>
        public static bool HasRevision(string version)
        {
            bool revValid, normalValid;
            MatchCollection matches = normalValidator.Matches(version);
            MatchCollection matchesRev = revValidator.Matches(version);
            normalValid = matches.Count > 0;
            revValid = matchesRev.Count > 0;
            if (!revValid && !normalValid)
                throw new SemVerException($"This version [{version}] is not a valid SemVer string.");
            return revValid;
        }

        /// <summary>
        /// Checks to see whether all the version elements are equal to these elements in the other instance. Simply, checks
        /// to see whether this <see cref="SemVer"/> instance equals the <paramref name="other"/> <see cref="SemVer"/> instance.
        /// </summary>
        /// <param name="other">A <see cref="SemVer"/> instance to compare this <see cref="SemVer"/> instance with</param>
        /// <returns>True if both instances have equal values. False otherwise.</returns>
        public bool Equals(SemVer other) =>
            CompareTo(other) == 0;

        /// <summary>
        /// Checks to see whether this <see cref="SemVer"/> instance contains a version older than the <paramref name="other"/> <see cref="SemVer"/> instance.
        /// </summary>
        /// <param name="other">A <see cref="SemVer"/> instance to compare this <see cref="SemVer"/> instance with</param>
        /// <returns>True if the <paramref name="other"/> <see cref="SemVer"/> instance contains a version that is newer than this instance. False otherwise.</returns>
        public bool IsOlderThan(SemVer other) =>
            CompareTo(other) < 0;

        /// <summary>
        /// Checks to see whether this <see cref="SemVer"/> instance contains a version newer than the <paramref name="other"/> <see cref="SemVer"/> instance.
        /// </summary>
        /// <param name="other">A <see cref="SemVer"/> instance to compare this <see cref="SemVer"/> instance with</param>
        /// <returns>True if the <paramref name="other"/> <see cref="SemVer"/> instance contains a version that is older than this instance. False otherwise.</returns>
        public bool IsNewerThan(SemVer other)
            => CompareTo(other) > 0;

        /// <inheritdoc/>
        public override bool Equals(object o) =>
            base.Equals(o);

        /// <inheritdoc/>
        public static bool operator ==(SemVer a, SemVer b)
            => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(SemVer a, SemVer b)
            => !a.Equals(b);

        /// <inheritdoc/>
        public static bool operator <(SemVer a, SemVer b)
            => a.IsOlderThan(b);

        /// <inheritdoc/>
        public static bool operator >(SemVer a, SemVer b)
            => a.IsNewerThan(b);

        /// <inheritdoc/>
        public static bool operator <=(SemVer a, SemVer b)
            => a.IsOlderThan(b) || a.Equals(b);

        /// <inheritdoc/>
        public static bool operator >=(SemVer a, SemVer b)
            => a.IsNewerThan(b) || a.Equals(b);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -1685347008;
            hashCode = hashCode * -1521134295 + MajorVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + MinorVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + PatchVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + RevisionVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PreReleaseInfo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BuildMetadata);
            return hashCode;
        }
    }
}
