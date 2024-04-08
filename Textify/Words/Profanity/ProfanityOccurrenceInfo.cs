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

namespace Textify.Words.Profanity
{
    /// <summary>
    /// Profanity occurrence info class
    /// </summary>
    public class ProfanityOccurrenceInfo
    {
        internal bool isThorough = false;

        /// <summary>
        /// The profane word in which the profanity manager matched successfully
        /// </summary>
        public string ProfaneWord { get; private set; }
        /// <summary>
        /// The source word containing either a profane word or being a profane word
        /// </summary>
        public string SourceWord { get; private set; }

        internal ProfanityOccurrenceInfo(bool isThorough, string profaneWord, string sourceWord)
        {
            this.isThorough = isThorough;
            ProfaneWord = profaneWord;
            SourceWord = sourceWord;
        }
    }
}
