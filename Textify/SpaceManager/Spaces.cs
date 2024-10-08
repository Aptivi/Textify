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

using System.Collections.Generic;
using System.Text;

namespace Textify.SpaceManager
{
    internal static class Spaces
    {
        internal static readonly char[] badSpaceChars =
        [
            '\u0009', '\u00a0', '\u1680', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004',
            '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F',
            '\u3000', '\u180E', '\u200B', '\u200C', '\u200D', '\u2060', '\uFEFF',
        ];
        
        internal static readonly char[] allSpaceChars =
        [
            ' ', .. badSpaceChars
        ];

        internal static readonly Dictionary<string, byte[]> badSpaces = new()
        {
            { "CHARACTER TABULATION",           Encoding.UTF8.GetBytes($"{badSpaceChars[0]}") },
            { "NON-BREAKING SPACE",             Encoding.UTF8.GetBytes($"{badSpaceChars[1]}") },
            { "OGHAM SPACE MARK",               Encoding.UTF8.GetBytes($"{badSpaceChars[2]}") },
            { "EN QUAD",                        Encoding.UTF8.GetBytes($"{badSpaceChars[3]}") },
            { "EM QUAD",                        Encoding.UTF8.GetBytes($"{badSpaceChars[4]}") },
            { "EN SPACE",                       Encoding.UTF8.GetBytes($"{badSpaceChars[5]}") },
            { "EM SPACE",                       Encoding.UTF8.GetBytes($"{badSpaceChars[6]}") },
            { "THREE-PER-EM SPACE",             Encoding.UTF8.GetBytes($"{badSpaceChars[7]}") },
            { "FOUR-PER-EM SPACE",              Encoding.UTF8.GetBytes($"{badSpaceChars[8]}") },
            { "SIX-PER-EM SPACE",               Encoding.UTF8.GetBytes($"{badSpaceChars[9]}") },
            { "FIGURE SPACE",                   Encoding.UTF8.GetBytes($"{badSpaceChars[10]}") },
            { "PUNCTUATION SPACE",              Encoding.UTF8.GetBytes($"{badSpaceChars[11]}") },
            { "THIN SPACE",                     Encoding.UTF8.GetBytes($"{badSpaceChars[12]}") },
            { "HAIR SPACE",                     Encoding.UTF8.GetBytes($"{badSpaceChars[13]}") },
            { "NARROW NON-BREAKING SPACE",      Encoding.UTF8.GetBytes($"{badSpaceChars[14]}") },
            { "MEDIUM MATHEMATICAL SPACE",      Encoding.UTF8.GetBytes($"{badSpaceChars[15]}") },
            { "IDEOGRAPHIC SPACE",              Encoding.UTF8.GetBytes($"{badSpaceChars[16]}") },
            { "MONGOLIAN VOWEL SEPARATOR",      Encoding.UTF8.GetBytes($"{badSpaceChars[17]}") },
            { "ZERO WIDTH SPACE",               Encoding.UTF8.GetBytes($"{badSpaceChars[18]}") },
            { "ZERO WIDTH NON-JOINER",          Encoding.UTF8.GetBytes($"{badSpaceChars[19]}") },
            { "ZERO WIDTH JOINER",              Encoding.UTF8.GetBytes($"{badSpaceChars[20]}") },
            { "WORD JOINER",                    Encoding.UTF8.GetBytes($"{badSpaceChars[21]}") },
            { "ZERO WIDTH NON-BREAKING SPACE",  Encoding.UTF8.GetBytes($"{badSpaceChars[22]}") },
        };
    }
}
