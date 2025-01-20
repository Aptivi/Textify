//
// Terminaux  Copyright (C) 2023-2025  Aptivi
//
// This file is part of Terminaux
//
// Terminaux is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Terminaux is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Textify.General.Structures.Helpers;

namespace Textify.General.Structures
{
    /// <summary>
    /// Wide string representation that represents an array of 4 wide characters
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct WideString : IEquatable<WideString>, IComparable<WideString>, IEnumerable<WideChar>
    {
        internal WideChar[] chars = [];
        private readonly WideStringEnumerator enumerator = new();

        /// <summary>
        /// Length of this wide string
        /// </summary>
        public readonly int Length =>
            chars.Sum((wc) => wc.GetLength());

        /// <summary>
        /// Converts this wide string instance to a character array
        /// </summary>
        /// <returns></returns>
        public readonly WideChar[] ToCharArray() =>
            [.. chars];

        /// <summary>
        /// Returns a string instance of this wide string
        /// </summary>
        /// <returns>Wide string as a string</returns>
        public override readonly string ToString() =>
            $"{string.Join("", chars)}";

        /// <inheritdoc/>
        public override readonly bool Equals(object obj)
        {
            if (obj is WideString WideString)
                return Equals(WideString);
            return base.Equals(obj);
        }

        /// <summary>
        /// Checks to see if this instance of the <see cref="WideString"/> instance is equal to another instance of the <see cref="WideString"/> instance
        /// </summary>
        /// <param name="other">Another instance of the <see cref="WideString"/> instance to compare with this <see cref="WideString"/> instance</param>
        /// <returns>True if both the <see cref="WideString"/> instances match; otherwise, false.</returns>
        public readonly bool Equals(WideString other)
            => Equals(this, other);

        /// <summary>
        /// Checks to see if the first instance of the <see cref="WideString"/> instance is equal to another instance of the <see cref="WideString"/> instance
        /// </summary>
        /// <param name="other">Another instance of the <see cref="WideString"/> instance to compare with another</param>
        /// <param name="other2">Another instance of the <see cref="WideString"/> instance to compare with another</param>
        /// <returns>True if both the <see cref="WideString"/> instances match; otherwise, false.</returns>
        public readonly bool Equals(WideString other, WideString other2)
        {
            // Check all the properties
            return
                other.ToString() == other2.ToString()
            ;
        }

        /// <inheritdoc/>
        public static bool operator ==(WideString a, WideString b)
            => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(WideString a, WideString b)
            => !a.Equals(b);

        /// <inheritdoc/>
        public static WideString operator +(WideString a, WideString b)
        {
            string stringA = a.ToString();
            string stringB = b.ToString();
            return (WideString)(stringA + stringB);
        }

        /// <inheritdoc/>
        public override readonly int GetHashCode()
        {
            return 1023364794 + EqualityComparer<WideChar[]>.Default.GetHashCode(chars);
        }

        /// <inheritdoc/>
        public readonly int CompareTo(WideString other) =>
            ToString().CompareTo(other.ToString());

        /// <inheritdoc/>
        public readonly IEnumerator<WideChar> GetEnumerator() =>
            enumerator;

        readonly IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <summary>
        /// Explicit operator for <see cref="WideString"/>
        /// </summary>
        /// <param name="source">Source string that contains either one (low) or two (low/high) strings</param>
        public static explicit operator WideString(string source) =>
            new(source);

        /// <summary>
        /// Implicit operator for <see cref="string"/>
        /// </summary>
        /// <param name="source">Source wide string</param>
        public static implicit operator string(WideString source) =>
            source.ToString();

        /// <summary>
        /// Indexer for a wide character
        /// </summary>
        /// <param name="index">Index starting from zero according to <see cref="Length"/></param>
        /// <returns>A wide character corresponding to the specified index</returns>
        public readonly WideChar this[int index]
        {
            get => chars[index];
            set => chars[index] = value;
        }

        /// <summary>
        /// Makes a new wide string instance
        /// </summary>
        /// <param name="source">Source string</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public WideString(string source)
        {
            if (string.IsNullOrEmpty(source))
                source = "";

            // Get hi/lo values and install them
            chars = TextTools.GetWideChars(source);
            enumerator.characters = chars;
        }
    }
}
