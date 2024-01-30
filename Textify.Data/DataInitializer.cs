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

using Textify.Data.DataRes;
using Textify.Tools;

namespace Textify.Data
{
    /// <summary>
    /// Data initialization class
    /// </summary>
    public static class DataInitializer
    {
        internal static bool initialized = false;

        /// <summary>
        /// Initializes all the needed data
        /// </summary>
        public static void Initialize() =>
            Initialize(DataType.Names | DataType.Unicode | DataType.Words);

        /// <summary>
        /// Initializes all the needed data
        /// </summary>
        /// <param name="types">Types to initialize</param>
        public static void Initialize(DataType types)
        {
            // If already initialized, bail.
            if (initialized)
                return;
            initialized = true;

            // Some variables for needed data
            bool needsNames = types.HasFlag(DataType.Names);
            bool needsUnicode = types.HasFlag(DataType.Unicode);
            bool needsWords = types.HasFlag(DataType.Words);

            // Go through all the types
            if (needsNames)
            {
                DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Female), NamesData.FirstNames_Female);
                DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Male), NamesData.FirstNames_Male);
                DataTools.dataStreams.Add(nameof(NamesData.FirstNames), NamesData.FirstNames);
                DataTools.dataStreams.Add(nameof(NamesData.Surnames), NamesData.Surnames);
            }
            if (needsUnicode)
            {
                DataTools.dataStreams.Add(nameof(UnicodeData.ucd_nounihan_flat), UnicodeData.ucd_nounihan_flat);
                DataTools.dataStreams.Add(nameof(UnicodeData.ucd_all_flat), UnicodeData.ucd_all_flat);
            }
            if (needsWords)
                DataTools.dataStreams.Add(nameof(WordsData.words_alpha), WordsData.words_alpha);
        }
    }
}
