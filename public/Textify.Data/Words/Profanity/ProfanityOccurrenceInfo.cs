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

namespace Textify.Data.Words.Profanity
{
    /// <summary>
    /// Profanity occurrence info class
    /// </summary>
    public class ProfanityOccurrenceInfo
    {
        internal ProfanitySearchType searchType = ProfanitySearchType.Shallow;

        /// <summary>
        /// The profane word in which the profanity manager has matched successfully
        /// </summary>
        public string ProfaneWord { get; private set; }
        /// <summary>
        /// The source word containing either a profane word or being a profane word
        /// </summary>
        public string SourceWord { get; private set; }
        /// <summary>
        /// The source sentence in which the profanity manager has analyzed
        /// </summary>
        public string SourceSentence { get; private set; }
        /// <summary>
        /// Index of the profane word in which the profanity manager has matched successfully
        /// </summary>
        public int ProfaneIndex { get; private set; }
        /// <summary>
        /// Index of the source word containing either a profane word or being a profane word
        /// </summary>
        public int SourceIndex { get; private set; }

        internal ProfanityOccurrenceInfo(ProfanitySearchType searchType, string profaneWord, string sourceWord, string sourceSentence, int profaneIndex, int sourceIndex)
        {
            this.searchType = searchType;
            ProfaneWord = profaneWord;
            SourceWord = sourceWord;
            SourceSentence = sourceSentence;
            ProfaneIndex = profaneIndex;
            SourceIndex = sourceIndex;
        }
    }
}
