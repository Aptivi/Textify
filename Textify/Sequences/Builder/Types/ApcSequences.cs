﻿
// Terminaux  Copyright (C) 2023  Aptivi
// 
// This file is part of Terminaux
// 
// Terminaux is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Terminaux is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Text.RegularExpressions;

namespace Textify.Sequences.Builder.Types
{
    /// <summary>
    /// List of APC sequences and their regular expressions
    /// </summary>
    public static class ApcSequences
    {
        private static readonly Regex apcApplicationProgramCommandSequenceRegex =
            new(@"(\x9f|\x1b_).+\x9c", RegexOptions.Compiled);

        /// <summary>
        /// [APC Pt ST] Regular expression for application program command
        /// </summary>
        public static Regex ApcApplicationProgramCommandSequenceRegex =>
            apcApplicationProgramCommandSequenceRegex;

        /// <summary>
        /// [APC Pt ST] Generates an escape sequence that can be used for the console
        /// </summary>
        public static string GenerateApcApplicationProgramCommand(string proprietaryCommands)
        {
            string result = $"{VtSequenceBasicChars.EscapeChar}_{proprietaryCommands}{VtSequenceBasicChars.StChar}";
            var regexParser = ApcApplicationProgramCommandSequenceRegex;
            if (!regexParser.IsMatch(result))
                throw new TextifyException("We have failed to generate a working VT sequence. Make sure that you've specified values correctly.");
            return result;
        }
    }
}
