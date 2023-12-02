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
        internal static readonly Dictionary<string, byte[]> badSpaces = new()
        {
            { "CHARACTER TABULATION",           Encoding.UTF8.GetBytes("\u0009") },
            { "NON-BREAKING SPACE",             Encoding.UTF8.GetBytes("\u00a0") },
            { "OGHAM SPACE MARK",               Encoding.UTF8.GetBytes("\u1680") },
            { "EN QUAD",                        Encoding.UTF8.GetBytes("\u2000") },
            { "EM QUAD",                        Encoding.UTF8.GetBytes("\u2001") },
            { "EN SPACE",                       Encoding.UTF8.GetBytes("\u2002") },
            { "EM SPACE",                       Encoding.UTF8.GetBytes("\u2003") },
            { "THREE-PER-EM SPACE",             Encoding.UTF8.GetBytes("\u2004") },
            { "FOUR-PER-EM SPACE",              Encoding.UTF8.GetBytes("\u2005") },
            { "SIX-PER-EM SPACE",               Encoding.UTF8.GetBytes("\u2006") },
            { "FIGURE SPACE",                   Encoding.UTF8.GetBytes("\u2007") },
            { "PUNCTUATION SPACE",              Encoding.UTF8.GetBytes("\u2008") },
            { "THIN SPACE",                     Encoding.UTF8.GetBytes("\u2009") },
            { "HAIR SPACE",                     Encoding.UTF8.GetBytes("\u200A") },
            { "NARROW NON-BREAKING SPACE",      Encoding.UTF8.GetBytes("\u202F") },
            { "MEDIUM MATHEMATICAL SPACE",      Encoding.UTF8.GetBytes("\u205F") },
            { "IDEOGRAPHIC SPACE",              Encoding.UTF8.GetBytes("\u3000") },
            { "MONGOLIAN VOWEL SEPARATOR",      Encoding.UTF8.GetBytes("\u180E") },
            { "ZERO WIDTH SPACE",               Encoding.UTF8.GetBytes("\u200B") },
            { "ZERO WIDTH NON-JOINER",          Encoding.UTF8.GetBytes("\u200C") },
            { "ZERO WIDTH JOINER",              Encoding.UTF8.GetBytes("\u200D") },
            { "WORD JOINER",                    Encoding.UTF8.GetBytes("\u2060") },
            { "ZERO WIDTH NON-BREAKING SPACE",  Encoding.UTF8.GetBytes("\uFEFF") },
        };
    }
}
