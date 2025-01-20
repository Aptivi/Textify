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

using System.Text;
using Textify.General;

namespace Textify.Accessibility
{
    /// <summary>
    /// Braille building tools
    /// </summary>
    public static class BrailleBuilder
    {
        /// <summary>
        /// Converts the sentence to Braille
        /// </summary>
        /// <param name="sentence">Sentence to convert</param>
        /// <returns>A Braille representation of the string</returns>
        public static string ToBraille(string sentence)
        {
            // Check the sentence first to avoid processing empty strings
            if (string.IsNullOrEmpty(sentence))
                return "";

            var lines = sentence.SplitNewLines();
            var builder = new StringBuilder();
            bool needsNumIndicator = false;
            bool needsLetterIndicator = false;
            for (int i = 0; i < lines.Length; i++)
            {
                // Get a line to convert
                string line = lines[i].ToLower();
                bool firstTime = true;
                string lastLetter = "";

                // Now, convert each character to Braille characters
                for (int idx = 0; idx < line.Length; idx++)
                {
                    char ch = line[idx];
                    bool isNum = char.IsNumber(ch);
                    bool isSpace = char.IsWhiteSpace(ch);
                    string result = "⠀";

                    // Use the braille map to check to see if we can find a character
                    foreach (var map in BrailleMap.brailleMap)
                    {
                        string braille = map.Item1;
                        char character = map.Item2;
                        if (ch == character)
                        {
                            result = braille;
                            break;
                        }
                    }

                    // Set the appropriate indicator
                    if (!firstTime)
                    {
                        needsNumIndicator = isNum && lastLetter == "letter";
                        needsLetterIndicator = !isNum && lastLetter == "number";
                    }

                    // Add an indicator as appropriate
                    if (needsNumIndicator || (firstTime && isNum))
                        result = "⠼" + result;
                    else if (needsLetterIndicator && !firstTime && !isSpace)
                        result = "⠆" + result;

                    // Apped the result and set the flags
                    builder.Append(result);
                    lastLetter = isNum ? "number" : "letter";
                    firstTime = false;
                }

                // Add a new line to the builder if we're not at the end and advance to the next line
                if (i != lines.Length - 1)
                    builder.AppendLine();
            }

            // Return the resulting Braille string
            return builder.ToString();
        }
    }
}
