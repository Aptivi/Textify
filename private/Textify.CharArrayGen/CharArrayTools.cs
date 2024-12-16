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
using System.Linq;

namespace Textify.CharArrayGen
{
    internal static class CharArrayTools
    {
        private static readonly char[] unicodeChars = Enumerable.Range(0, Convert.ToInt32(char.MaxValue) + 1).Select(Convert.ToChar).ToArray();

        /// <summary>
        /// Gets all the letters and the numbers (ASCII).
        /// </summary>
        public static char[] GetAllAsciiChars() =>
            unicodeChars.Take(byte.MaxValue + 1).ToArray();

        /// <summary>
        /// Gets all the letters and the numbers (Unicode).
        /// </summary>
        public static char[] GetAllChars() =>
            unicodeChars;

        /// <summary>
        /// Gets all the letters and the numbers.
        /// </summary>
        public static char[] GetAllLettersAndNumbers(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsLetterOrDigit).ToArray();

        /// <summary>
        /// Gets all the letters.
        /// </summary>
        public static char[] GetAllLetters(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsLetter).ToArray();

        /// <summary>
        /// Gets all the numbers.
        /// </summary>
        public static char[] GetAllNumbers(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsNumber).ToArray();

        /// <summary>
        /// Gets all the control characters.
        /// </summary>
        public static char[] GetAllControlChars() =>
            GetAllChars().Where(char.IsControl).ToArray();

        /// <summary>
        /// Gets all the real control characters for binary file comparison.
        /// </summary>
        public static char[] GetAllRealControlChars() =>
            GetAllChars().Where(IsControlChar).ToArray();

        /// <summary>
        /// Gets all the characters that represents a digit.
        /// </summary>
        public static char[] GetAllDigitChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsDigit).ToArray();

        /// <summary>
        /// Gets all the characters that represents a surrogate character.
        /// </summary>
        public static char[] GetAllSurrogateChars() =>
            GetAllChars().Where(char.IsSurrogate).ToArray();

        /// <summary>
        /// Gets all the characters that represents a high surrogate character.
        /// </summary>
        public static char[] GetAllHighSurrogateChars() =>
            GetAllChars().Where(char.IsHighSurrogate).ToArray();

        /// <summary>
        /// Gets all the characters that represents a low surrogate character.
        /// </summary>
        public static char[] GetAllLowSurrogateChars() =>
            GetAllChars().Where(char.IsLowSurrogate).ToArray();

        /// <summary>
        /// Gets all the characters that represents a lowercase character.
        /// </summary>
        public static char[] GetAllLowerChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsLower).ToArray();

        /// <summary>
        /// Gets all the characters that represents a uppercase character.
        /// </summary>
        public static char[] GetAllUpperChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsUpper).ToArray();

        /// <summary>
        /// Gets all the characters that represents a punctuation.
        /// </summary>
        public static char[] GetAllPunctuationChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsPunctuation).ToArray();

        /// <summary>
        /// Gets all the characters that represents a separator.
        /// </summary>
        public static char[] GetAllSeparatorChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsSeparator).ToArray();

        /// <summary>
        /// Gets all the characters that represents a separator.
        /// </summary>
        public static char[] GetAllSymbolChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsSymbol).ToArray();

        /// <summary>
        /// Gets all the characters that represents a white space.
        /// </summary>
        public static char[] GetAllWhitespaceChars(bool unicode = true) =>
            (unicode ? GetAllChars() : GetAllAsciiChars()).Where(char.IsWhiteSpace).ToArray();

        /// <summary>
        /// Is the character a real control character
        /// </summary>
        /// <param name="ch">Character to query</param>
        public static bool IsControlChar(char ch) =>
            // If the character is greater than the NULL character and less than the BACKSPACE character, or
            // if the character is greater than the CARRIAGE RETURN character and less than the SUBSTITUTE character,
            // it's a real control character.
            (ch > (char)0 && ch < (char)8) || (ch > (char)13 && ch < (char)26);
    }
}
