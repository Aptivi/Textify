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

using Textify.General;

namespace Textify.Tools
{
    //
    // ========== CODE ASSISTED BY Claude Sonnet 5 Medium WITH HUMAN REVIEW
    //
    /// <summary>
    /// Grapheme cluster tools
    /// </summary>
    public static class GraphemeCluster
    {
        private const char Zwj = '\u200D';
        private const char Vs15 = '\uFE0E';
        private const char Vs16 = '\uFE0F';
        private const char KeycapCombiner = '\u20E3';
        private const int ModifierRangeStart = 0x1F3FB;
        private const int ModifierRangeEnd = 0x1F3FF;
        private const int RegionalIndicatorStart = 0x1F1E6;
        private const int RegionalIndicatorEnd = 0x1F1FF;
        private const int TagStart = 0xE0020;
        private const int TagEnd = 0xE007E;
        private const int TagCancel = 0xE007F;

        /// <summary>
        /// Gets the length of the sequence from a specified position with forwards read
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <param name="pos">Position to start processing from</param>
        /// <returns>Total length of the sequence</returns>
        public static int GetLength(string text, int pos)
        {
            if (pos >= text.Length)
                return 0;

            var seg = ReadSegment(text, pos);
            int idx = pos + seg.Length;
            while (idx < text.Length && text[idx] == Zwj && idx + 1 < text.Length)
            {
                idx++;
                idx += ReadSegment(text, idx).Length;
            }
            return idx - pos;
        }

        /// <summary>
        /// Gets the grapheme cluster width
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <param name="clusterStart">Position to start processing from</param>
        /// <returns>Total width of the sequence</returns>
        public static int GetClusterWidth(string text, int clusterStart)
        {
            if (clusterStart < 0 || clusterStart >= text.Length)
                return 0;
            return ReadSegment(text, clusterStart).Width;
        }

        /// <summary>
        /// Gets the length of the sequence from a specified position with backwards read
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <param name="pos">Position to start processing from</param>
        /// <returns>Total length of the sequence</returns>
        public static int GetLengthBackward(string text, int pos)
        {
            if (pos <= 0)
                return 0;
            int start = GetStart(text, pos - 1);
            return pos - start;
        }

        /// <summary>
        /// Gets the start position of the grapheme cluster
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <param name="index">Index of text to start processing from</param>
        /// <returns>Starting position</returns>
        public static int GetStart(string text, int index)
        {
            if (index <= 0)
                return 0;

            int pos = 0;
            while (pos < text.Length)
            {
                int len = GetLength(text, pos);
                if (len <= 0)
                    len = 1;

                if (index < pos + len)
                    return pos;

                pos += len;
            }
            return pos;
        }
        
        private static int GetRegionalIndicatorRunStart(string text, int pos)
        {
            int start = pos;
            while (start - 2 >= 0 && IsRegionalIndicatorPair(text, start - 2))
                start -= 2;
            return start;
        }

        private static int GetRegionalIndicatorClusterStart(string text, int posInRun)
        {
            int runStart = GetRegionalIndicatorRunStart(text, posInRun);
            int k = (posInRun - runStart) / 2;
            int pairIndex = k / 2;
            return runStart + pairIndex * 4;
        }

        private static int GetRegionalIndicatorClusterLength(string text, int clusterStart) =>
            (clusterStart + 2 < text.Length && IsRegionalIndicatorPair(text, clusterStart + 2)) ? 4 : 2;

        private static bool IsPairInRange(string text, int idx, int lo, int hi) =>
            idx + 1 < text.Length && char.IsSurrogatePair(text[idx], text[idx + 1]) &&
            char.ConvertToUtf32(text[idx], text[idx + 1]) is int cp && cp >= lo && cp <= hi;

        private static bool IsModifierPair(string text, int idx) =>
            IsPairInRange(text, idx, ModifierRangeStart, ModifierRangeEnd);

        private static bool IsRegionalIndicatorPair(string text, int idx) =>
            IsPairInRange(text, idx, RegionalIndicatorStart, RegionalIndicatorEnd);

        private static bool IsTagCharPair(string text, int idx) =>
            IsPairInRange(text, idx, TagStart, TagEnd);

        private static bool IsTagCancelPair(string text, int idx) =>
            IsPairInRange(text, idx, TagCancel, TagCancel);

        private static SegmentInfo ReadSegment(string text, int pos)
        {
            if (pos >= text.Length)
                return new SegmentInfo(0, 0);

            // Flags: pair from the start of the run, always double-width as a whole unit,
            // regardless of whatever width a lone regional indicator codepoint might report.
            if (IsRegionalIndicatorPair(text, pos))
            {
                int clusterStart = GetRegionalIndicatorClusterStart(text, pos);
                if (clusterStart == pos)
                    return new SegmentInfo(GetRegionalIndicatorClusterLength(text, clusterStart), 2);

                // Not pair-aligned (shouldn't normally happen with a proper cluster start) -
                // treat as a lone RI codepoint instead of guessing.
                int cp = char.ConvertToUtf32(text[pos], text[pos + 1]);
                return new SegmentInfo(2, TextTools.GetCharWidth(cp));
            }

            bool isSurrogatePair = pos + 1 < text.Length && char.IsSurrogatePair(text[pos], text[pos + 1]);
            int baseLen = isSurrogatePair ? 2 : 1;
            int baseCodePoint = isSurrogatePair ? char.ConvertToUtf32(text[pos], text[pos + 1]) : text[pos];
            int idx = pos + baseLen;
            int width = TextTools.GetCharWidth(baseCodePoint);

            // Skin-tone modifier: zero-width addition, doesn't change the base's width
            if (IsModifierPair(text, idx))
                idx += 2;

            // Tag sequence: zero-width addition regardless of what GetCharWidth would
            // (likely incorrectly) report for these astral, invisible codepoints
            if (IsTagCharPair(text, idx))
            {
                while (IsTagCharPair(text, idx))
                    idx += 2;
                if (IsTagCancelPair(text, idx))
                    idx += 2;
            }

            // Variation selectors / keycap combiner can override the base's default width
            if (idx < text.Length && text[idx] == Vs16)
            {
                width = 2; // emoji presentation forces full width
                idx++;
                if (idx < text.Length && text[idx] == KeycapCombiner)
                    idx++;
            }
            else if (idx < text.Length && text[idx] == Vs15)
            {
                width = 1; // text presentation forces narrow width
                idx++;
            }
            else if (idx < text.Length && text[idx] == KeycapCombiner)
            {
                width = 2; // keycap without explicit VS16
                idx++;
            }

            return new SegmentInfo(idx - pos, width);
        }
    }
    //
    // ========== CODE ASSISTED BY Claude Sonnet 5 Medium WITH HUMAN REVIEW
    //
}
