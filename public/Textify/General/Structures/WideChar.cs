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

using System;
using System.Diagnostics;
using Textify.Language;
using Textify.Tools;

namespace Textify.General.Structures
{
    /// <summary>
    /// Wide character representation that represents 4 bytes for a 32-bit character
    /// </summary>
    [Serializable]
    [DebuggerDisplay("'{ToString(),nq}' [Hi: {(int)high}, Lo: {(int)low}]")]
    public struct WideChar : IEquatable<WideChar>, IComparable<WideChar>
    {
        internal char high = '\0';
        internal char low = '\0';

        /// <summary>
        /// Gets the character length
        /// </summary>
        /// <returns>2 if this character is represented with four bytes. Otherwise, 1.</returns>
        public readonly int GetLength() =>
            high != '\0' ? 2 : 1;

        /// <summary>
        /// Gets the character code
        /// </summary>
        /// <returns>Character code that represents four bytes of a wide character in an integral value</returns>
        public readonly long GetCharCode() =>
            high <= '\0' ? low : 0x10000 + ((low - 0xd800) << 10) + (high - 0xdc00);

        /// <summary>
        /// Checks to see if this is a valid UTF-32 character
        /// </summary>
        /// <returns>True if this is a valid character; false otherwise.</returns>
        public readonly bool IsValidChar() =>
            high <= '\0' || IsValidSurrogate();

        /// <summary>
        /// Checks to see whether this wide character is a valid surrogate
        /// </summary>
        /// <returns>True if this is a valid surrogate pair; false otherwise. False for characters that don't need four bytes.</returns>
        public readonly bool IsValidSurrogate() =>
            char.IsSurrogatePair(low, high);

        /// <summary>
        /// Returns a string instance of this wide character
        /// </summary>
        /// <returns>Wide character as a string</returns>
        public override readonly string ToString() =>
            $"{low}{(high != '\0' ? high : "")}";

        /// <summary>
        /// Parses a string containing a 32-bit character
        /// </summary>
        /// <param name="representation">A string that contains a 32-bit character</param>
        /// <returns>An instance of <see cref="WideChar"/></returns>
        public static WideChar Parse(string representation) =>
            new(representation, true);

        /// <summary>
        /// Parses a character code that represents a 32-bit character
        /// </summary>
        /// <param name="code">Character code that represents a 32-bit character</param>
        /// <returns>An instance of <see cref="WideChar"/></returns>
        public static WideChar Parse(long code) =>
            new(code, true);

        /// <summary>
        /// Parses two characters that represent a 32-bit character
        /// </summary>
        /// <param name="hi">Second two bytes of a wide character</param>
        /// <param name="lo">First two bytes of a wide character</param>
        /// <returns>An instance of <see cref="WideChar"/></returns>
        public static WideChar Parse(char hi, char lo) =>
            new(hi, lo, true);

        /// <summary>
        /// Parses a string containing a 32-bit character
        /// </summary>
        /// <param name="representation">A string that contains a 32-bit character</param>
        /// <param name="output">Output of an instance of <see cref="WideChar"/></param>
        /// <returns>True if parsed successfully. Otherwise, false.</returns>
        public static bool TryParse(string representation, out WideChar? output)
        {
            try
            {
                output = new(representation, true);
            }
            catch
            {
                output = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Parses a character code that represents a 32-bit character
        /// </summary>
        /// <param name="code">Character code that represents a 32-bit character</param>
        /// <param name="output">Output of an instance of <see cref="WideChar"/></param>
        /// <returns>True if parsed successfully. Otherwise, false.</returns>
        public static bool TryParse(long code, out WideChar? output)
        {
            try
            {
                output = new(code, true);
            }
            catch
            {
                output = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Parses two characters that represent a 32-bit character
        /// </summary>
        /// <param name="hi">Second two bytes of a wide character</param>
        /// <param name="lo">First two bytes of a wide character</param>
        /// <param name="output">Output of an instance of <see cref="WideChar"/></param>
        /// <returns>True if parsed successfully. Otherwise, false.</returns>
        public static bool TryParse(char hi, char lo, out WideChar? output)
        {
            try
            {
                output = new(hi, lo, true);
            }
            catch
            {
                output = null;
                return false;
            }
            return true;
        }

        /// <inheritdoc/>
        public override readonly bool Equals(object obj)
        {
            if (obj is WideChar WideChar)
                return Equals(WideChar);
            return base.Equals(obj);
        }

        /// <summary>
        /// Checks to see if this instance of the <see cref="WideChar"/> instance is equal to another instance of the <see cref="WideChar"/> instance
        /// </summary>
        /// <param name="other">Another instance of the <see cref="WideChar"/> instance to compare with this <see cref="WideChar"/> instance</param>
        /// <returns>True if both the <see cref="WideChar"/> instances match; otherwise, false.</returns>
        public readonly bool Equals(WideChar other)
            => Equals(this, other);

        /// <summary>
        /// Checks to see if the first instance of the <see cref="WideChar"/> instance is equal to another instance of the <see cref="WideChar"/> instance
        /// </summary>
        /// <param name="other">Another instance of the <see cref="WideChar"/> instance to compare with another</param>
        /// <param name="other2">Another instance of the <see cref="WideChar"/> instance to compare with another</param>
        /// <returns>True if both the <see cref="WideChar"/> instances match; otherwise, false.</returns>
        public readonly bool Equals(WideChar other, WideChar other2)
        {
            // Check all the properties
            return
                other.high == other2.high &&
                other.low == other2.low
            ;
        }

        /// <inheritdoc/>
        public static bool operator ==(WideChar a, WideChar b)
            => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(WideChar a, WideChar b)
            => !a.Equals(b);

        /// <inheritdoc/>
        public static bool operator <(WideChar a, WideChar b)
        {
            long codeA = a.GetCharCode();
            long codeB = b.GetCharCode();
            return codeA < codeB;
        }

        /// <inheritdoc/>
        public static bool operator >(WideChar a, WideChar b)
        {
            long codeA = a.GetCharCode();
            long codeB = b.GetCharCode();
            return codeA > codeB;
        }

        /// <inheritdoc/>
        public static bool operator <=(WideChar a, WideChar b)
        {
            long codeA = a.GetCharCode();
            long codeB = b.GetCharCode();
            return codeA <= codeB;
        }

        /// <inheritdoc/>
        public static bool operator >=(WideChar a, WideChar b)
        {
            long codeA = a.GetCharCode();
            long codeB = b.GetCharCode();
            return codeA >= codeB;
        }

        /// <inheritdoc/>
        public static WideChar operator +(WideChar a, WideChar b)
        {
            long codeA = a.GetCharCode();
            long codeB = b.GetCharCode();
            return (WideChar)(codeA + codeB);
        }

        /// <inheritdoc/>
        public static WideChar operator -(WideChar a, WideChar b)
        {
            long codeA = a.GetCharCode();
            long codeB = b.GetCharCode();
            return (WideChar)(codeA - codeB);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -1466551034;
            hashCode = hashCode * -1521134295 + high.GetHashCode();
            hashCode = hashCode * -1521134295 + low.GetHashCode();
            return hashCode;
        }

        /// <inheritdoc/>
        public readonly int CompareTo(WideChar other)
        {
            long codeA = GetCharCode();
            long codeB = other.GetCharCode();
            if (codeA == codeB)
                return 0;
            else if (codeA > codeB)
                return -1;
            else if (codeA < codeB)
                return 1;
            return 0;
        }

        /// <summary>
        /// Explicit operator for <see cref="WideChar"/>
        /// </summary>
        /// <param name="source">Source string that contains either one (low) or two (low/high) characters</param>
        public static explicit operator WideChar(string source) =>
            new(source);

        /// <summary>
        /// Explicit operator for <see cref="WideChar"/>
        /// </summary>
        /// <param name="charCode">Character code</param>
        public static explicit operator WideChar(long charCode) =>
            new(charCode);

        /// <summary>
        /// Implicit operator for <see cref="string"/>
        /// </summary>
        /// <param name="source">Source wide character</param>
        public static implicit operator string(WideChar source) =>
            source.ToString();

        /// <summary>
        /// Implicit operator for hi/lo values
        /// </summary>
        /// <param name="source">Source wide character</param>
        public static implicit operator (char high, char low)(WideChar source) =>
            (source.high, source.low);

        /// <summary>
        /// Implicit operator for char code
        /// </summary>
        /// <param name="source">Source wide character</param>
        public static implicit operator long(WideChar source) =>
            source.GetCharCode();

        /// <summary>
        /// Makes a new wide character instance
        /// </summary>
        /// <param name="source">Source string that contains either one (low) or two (low/high) characters</param>
        /// <param name="check">Checks for validity</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public WideChar(string source, bool check = true)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(LanguageTools.GetLocalized("TEXTIFY_GENERAL_STRUCTURES_WCHAR_EXCEPTION_NOSTRING"));
            if (source.Length > 2)
                throw new ArgumentException(LanguageTools.GetLocalized("TEXTIFY_GENERAL_STRUCTURES_WCHAR_EXCEPTION_TWOCHARSMAX"));

            // Get hi/lo values and install them
            high = source.Length == 2 ? source[1] : '\0';
            low = source[0];

            // Check for validity
            if (check && !IsValidChar())
                throw new TextifyException(LanguageTools.GetLocalized("TEXTIFY_GENERAL_STRUCTURES_WCHAR_EXCEPTION_INVALIDCHAR").FormatString((int)high, (int)low));
        }

        /// <summary>
        /// Makes a new wide character instance
        /// </summary>
        /// <param name="charCode">Character code that represents a wide character</param>
        /// <param name="check">Checks for validity</param>
        public WideChar(long charCode, bool check = true)
        {
            if (charCode >= 0x10000)
            {
                low = (char)(0xd800 + ((charCode - 0x10000) >> 10));
                high = (char)(0xdc00 + ((charCode - 0x10000) & 0x3ff));
            }
            else
                low = (char)(charCode & 65535);

            // Check for validity
            if (check && !IsValidChar())
                throw new TextifyException(LanguageTools.GetLocalized("TEXTIFY_GENERAL_STRUCTURES_WCHAR_EXCEPTION_INVALIDCHAR").FormatString((int)high, (int)low));
        }

        /// <summary>
        /// Makes a new wide character instance
        /// </summary>
        /// <param name="hi">Second two bytes of a wide character</param>
        /// <param name="lo">First two bytes of a wide character</param>
        /// <param name="check">Checks for validity</param>
        public WideChar(char hi, char lo, bool check = true)
        {
            high = hi;
            low = lo;

            // Check for validity
            if (check && !IsValidChar())
                throw new TextifyException(LanguageTools.GetLocalized("TEXTIFY_GENERAL_STRUCTURES_WCHAR_EXCEPTION_INVALIDCHAR").FormatString((int)high, (int)low));
        }
    }
}
