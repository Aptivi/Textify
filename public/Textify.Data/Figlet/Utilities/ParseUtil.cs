﻿//
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

namespace Textify.Data.Figlet.Utilities
{
    internal static class ParseUtil
    {
        public static bool TryParse(string s, out int i)
        {
            const int starting = 0;
            const int negative = 1;
            const int leadingZero = 2;
            const int dec = 3;
            const int oct = 4;
            const int startingHex = 5;
            const int hex = 6;

            var state = starting;
            var value = 0;
            var isNegative = false;

            for (var idx = 0; idx < s.Length; idx++)
            {
                var c = s[idx];

                switch (state)
                {
                    case starting:
                    case negative:
                        {
                            switch (c)
                            {
                                case '0':
                                    state = leadingZero;
                                    break;
                                case '-':
                                    if (state == starting)
                                    {
                                        isNegative = true;
                                        state = negative;
                                        break;
                                    }
                                    i = default;
                                    return false;
                                default:
                                    if (char.IsDigit(c))
                                    {
                                        value = c - '0';
                                        state = dec;
                                        break;
                                    }
                                    if (state == starting && char.IsWhiteSpace(c))
                                        break;
                                    i = default;
                                    return false;
                            }
                            break;
                        }
                    case leadingZero:
                        {
                            switch (c)
                            {
                                case 'x':
                                case 'X':
                                case 'h':
                                    state = startingHex;
                                    break;
                                case ' ':
                                    i = 0;
                                    return true;
                                default:
                                    if (char.IsDigit(c))
                                    {
                                        value = c - '0';
                                        state = oct;
                                        break;
                                    }
                                    i = default;
                                    return false;
                            }
                            break;
                        }
                    case dec:
                        {
                            if (char.IsDigit(c))
                            {
                                value *= 10;
                                value += c - '0';
                                continue;
                            }
                            if (char.IsWhiteSpace(c))
                            {
                                i = isNegative ? -value : value;
                                return true;
                            }
                            i = default;
                            return false;
                        }
                    case oct:
                        {
                            var v = c - '0';
                            if (v >= 0 && v < 8)
                            {
                                value *= 8;
                                value += v;
                                continue;
                            }
                            if (char.IsWhiteSpace(c))
                            {
                                i = isNegative ? -value : value;
                                return true;
                            }
                            i = default;
                            return false;
                        }
                    case hex:
                    case startingHex:
                        {
                            if (c >= '0' && c <= '9')
                            {
                                state = hex;
                                var v = c - '0';
                                value *= 16;
                                value += v;
                                continue;
                            }
                            var cl = char.ToLower(c);
                            if (cl >= 'a' && c <= 'f')
                            {
                                state = hex;
                                var v = cl - 'a' + 10;
                                value *= 16;
                                value += v;
                                continue;
                            }
                            if (state == hex && char.IsWhiteSpace(c))
                            {
                                i = isNegative ? -value : value;
                                return true;
                            }
                            i = default;
                            return false;
                        }
                }
            }

            switch (state)
            {
                case dec:
                case oct:
                case hex:
                    i = isNegative ? -value : value;
                    return true;
                case leadingZero:
                    i = 0;
                    return true;
                default:
                    i = 0;
                    return false;
            }
        }
    }
}
