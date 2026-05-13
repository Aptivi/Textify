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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Textify.Data.Cowsay
{
    /// <summary>
    /// Default bubble blower class
    /// </summary>
    public class DefaultBubbleBlower : IBubbleBlower
    {
        /// <inheritdoc/>
        public string GetBubble(string phrase, int maxCols, bool isThoughtBubble)
        {
            string bubble;

            if (phrase.Length > maxCols || phrase.Contains(Environment.NewLine))
                bubble = GenerateMultiLineBubble(phrase, maxCols, isThoughtBubble);
            else
                bubble = GenerateSingleLineBubble(phrase, isThoughtBubble);

            return bubble;
        }

        private static string GenerateSingleLineBubble(string phrase, bool isThoughtBubble)
        {
            string bubble;
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine(" " + string.Empty.PadRight(phrase.Length + 2, '_') + " ");
            stringBuilder.AppendLine((isThoughtBubble ? "( " : "< ") + phrase + (isThoughtBubble ? " )" : " >"));
            stringBuilder.AppendLine(" " + string.Empty.PadRight(phrase.Length + 2, '-') + " ");

            bubble = stringBuilder.ToString();
            return bubble;
        }

        private string GenerateMultiLineBubble(string phrase, int maxCols, bool isThoughtBubble)
        {
            StringBuilder stringBuilder = new();

            string[] linesWithoutDecoration = GetWrappedLines(phrase, maxCols);

            int maxWidth = linesWithoutDecoration.Max(l => l.Length);

            stringBuilder.AppendLine(" " + string.Empty.PadRight(maxWidth + 2, '_'));

            for (int i = 0; i < linesWithoutDecoration.Length; i++)
            {
                stringBuilder.AppendLine(
                    GetPrefix(i + 1, linesWithoutDecoration.Length, isThoughtBubble) +
                    " " +
                    linesWithoutDecoration[i].PadRight(maxWidth + 1) +
                    GetSuffix(i + 1, linesWithoutDecoration.Length, isThoughtBubble));
            }

            stringBuilder.AppendLine(" " + string.Empty.PadRight(maxWidth + 2, '-') + " ");

            return stringBuilder.ToString();
        }

        private string[] GetWrappedLines(string phrase, int maxCols)
        {
            int lineNumber = 0;
            string[] phrases = phrase.Split([Environment.NewLine], StringSplitOptions.None).ToArray();
            List<string> wrappedLines = [];
            foreach (var inputLine in phrases)
            {
                string[] words = inputLine.Split([" "], StringSplitOptions.None);
                string currentLine = string.Empty;
                foreach (var word in words)
                {
                    if (currentLine.Length + " ".Length + word.Length <= maxCols)
                        currentLine += (currentLine.Length > 0 ? " " : string.Empty) + word;
                    else
                    {
                        lineNumber++;
                        wrappedLines.Add(currentLine);
                        currentLine = word;
                    }
                }
                wrappedLines.Add(currentLine);
            }

            return wrappedLines.ToArray();
        }

        private string GetPrefix(int lineNumber, int lines, bool isThoughtBubble)
        {
            string prefix = "|";

            if (isThoughtBubble)
                prefix = "(";
            else if (lineNumber == 1)
                prefix = "/";
            else if (lineNumber == lines)
                prefix = "\\";

            return prefix;
        }

        private string GetSuffix(int lineNumber, int lines, bool thoughtBubble)
        {
            string suffix = "|";

            if (thoughtBubble)
                suffix = ")";
            else if (lineNumber == 1)
                suffix = "\\";
            else if (lineNumber == lines)
                suffix = "/";

            return suffix;
        }
    }
}
