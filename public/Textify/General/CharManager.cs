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

namespace Textify.General
{
    /// <summary>
    /// Character querying and management module
    /// </summary>
    public static partial class CharManager
    {
        /// <summary>
        /// New line constant
        /// </summary>
        public static string NewLine =>
            Environment.NewLine;

        /// <summary>
        /// Gets all the letters and the numbers (ASCII).
        /// </summary>
        public static char[] GetAllAsciiChars() =>
            unicodeAsciiChars;

        /// <summary>
        /// Gets all the letters and the numbers (Unicode).
        /// </summary>
        public static char[] GetAllChars() =>
            unicodeChars;

        /// <summary>
        /// Gets all the letters and the numbers.
        /// </summary>
        public static char[] GetAllLettersAndNumbers(bool unicode = true) =>
            unicode ? letterOrDigitChars : letterOrDigitAsciiChars;

        /// <summary>
        /// Gets all the letters.
        /// </summary>
        public static char[] GetAllLetters(bool unicode = true) =>
            unicode ? letterChars : letterAsciiChars;

        /// <summary>
        /// Gets all the numbers.
        /// </summary>
        public static char[] GetAllNumbers(bool unicode = true) =>
            unicode ? numberChars : numberAsciiChars;

        /// <summary>
        /// Gets all the characters that represents a digit.
        /// </summary>
        public static char[] GetAllDigitChars(bool unicode = true) =>
            unicode ? digitChars : digitAsciiChars;

        /// <summary>
        /// Gets all the control characters.
        /// </summary>
        public static char[] GetAllControlChars() =>
            controlChars;

        /// <summary>
        /// Gets all the real control characters for binary file comparison.
        /// </summary>
        public static char[] GetAllRealControlChars() =>
            realControlChars;

        /// <summary>
        /// Gets all the characters that represents a surrogate character.
        /// </summary>
        public static char[] GetAllSurrogateChars() =>
            surrogateChars;

        /// <summary>
        /// Gets all the characters that represents a high surrogate character.
        /// </summary>
        public static char[] GetAllHighSurrogateChars() =>
            highSurrogateChars;

        /// <summary>
        /// Gets all the characters that represents a low surrogate character.
        /// </summary>
        public static char[] GetAllLowSurrogateChars() =>
            lowSurrogateChars;

        /// <summary>
        /// Gets all the characters that represents a lowercase character.
        /// </summary>
        public static char[] GetAllLowerChars(bool unicode = true) =>
            unicode ? lowerChars : lowerAsciiChars;

        /// <summary>
        /// Gets all the characters that represents a uppercase character.
        /// </summary>
        public static char[] GetAllUpperChars(bool unicode = true) =>
            unicode ? upperChars : upperAsciiChars;

        /// <summary>
        /// Gets all the characters that represents a punctuation.
        /// </summary>
        public static char[] GetAllPunctuationChars(bool unicode = true) =>
            unicode ? punctuationChars : punctuationAsciiChars;

        /// <summary>
        /// Gets all the characters that represents a separator.
        /// </summary>
        public static char[] GetAllSeparatorChars(bool unicode = true) =>
            unicode ? separatorChars : separatorAsciiChars;

        /// <summary>
        /// Gets all the characters that represents a separator.
        /// </summary>
        public static char[] GetAllSymbolChars(bool unicode = true) =>
            unicode ? symbolChars : symbolAsciiChars;

        /// <summary>
        /// Gets all the characters that represents a white space.
        /// </summary>
        public static char[] GetAllWhitespaceChars(bool unicode = true) =>
            unicode ? whitespaceChars : whitespaceAsciiChars;

        /// <summary>
        /// A simplification for casting to return the ESC character
        /// </summary>
        /// <returns>ESC</returns>
        public static char GetEsc() =>
            '\x001b';

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
