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
using System.Runtime.InteropServices;

namespace Textify.Data.Unicode.Icu
{
    internal static unsafe class NativeInteropBidi
    {
        internal struct UBidi
        { }

        internal enum BidiDirection
        {
            UBIDI_LTR,
            UBIDI_RTL,
            UBIDI_MIXED,
            UBIDI_NEUTRAL
        }

        internal enum BidiLevel
        {
            UBIDI_DEFAULT_LTR = 0xfe,
            UBIDI_DEFAULT_RTL = 0xff,
            UBIDI_MAX_EXPLICIT_LEVEL = 125,
            UBIDI_LEVEL_OVERRIDE = 0x80,
        }

        /// <summary>
        /// U_CAPI UBiDi * U_EXPORT2 ubidi_open(void);
        /// </summary>
        internal delegate UBidi* ubidi_open();

        /// <summary>
        /// U_CAPI void U_EXPORT2 ubidi_close(UBiDi *pBiDi);
        /// </summary>
        internal delegate void ubidi_close(UBidi* bidi);

        /// <summary>
        /// U_CAPI void U_EXPORT2 ubidi_setPara(UBiDi *pBiDi, const UChar *text, int32_t length, UBiDiLevel paraLevel, UBiDiLevel *embeddingLevels, UErrorCode *pErrorCode);
        /// </summary>
        internal delegate void ubidi_setPara(UBidi* bidi, IntPtr text, int length, BidiLevel level, IntPtr embedLevels, out int errorCode);

        /// <summary>
        /// U_CAPI void U_EXPORT2 ubidi_getVisualMap(UBiDi *pBiDi, int32_t *indexMap, UErrorCode *pErrorCode);
        /// </summary>
        internal delegate void ubidi_getVisualMap(UBidi* bidi, [Out] int[] map, out int errorCode);
    }
}
