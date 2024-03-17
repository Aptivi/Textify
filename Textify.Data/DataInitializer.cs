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
        /// <summary>
        /// Initializes all the needed data
        /// </summary>
        public static void Initialize() =>
            Initialize(DataType.All);

        /// <summary>
        /// Initializes all the needed data
        /// </summary>
        /// <param name="types">Types to initialize</param>
        public static void Initialize(DataType types)
        {
            // Some variables for needed data
            bool needsNames = types.HasFlag(DataType.Names);
            bool needsFemaleNames = types.HasFlag(DataType.NamesFemale);
            bool needsMaleNames = types.HasFlag(DataType.NamesMale);
            bool needsFemaleImplicitNames = types.HasFlag(DataType.NamesFemaleImplicit);
            bool needsMaleImplicitNames = types.HasFlag(DataType.NamesMaleImplicit);
            bool needsNaturalNames = types.HasFlag(DataType.NamesNatural);
            bool needsUnicode = types.HasFlag(DataType.Unicode);
            bool needsUnicodeNoUnihan = types.HasFlag(DataType.UnicodeNoUnihan);
            bool needsWords = types.HasFlag(DataType.Words);
            bool needsSurnames = needsNames || needsFemaleNames || needsMaleNames || needsFemaleImplicitNames || needsMaleImplicitNames || needsNaturalNames;

            // Go through all the types
            if (needsNames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.FirstNames)))
                    DataTools.dataStreams.Add(nameof(NamesData.FirstNames), NamesData.FirstNames);
            }
            if (needsFemaleNames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.FirstNames_Female)))
                    DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Female), NamesData.FirstNames_Female);
            }
            if (needsMaleNames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.FirstNames_Male)))
                    DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Male), NamesData.FirstNames_Male);
            }
            if (needsFemaleImplicitNames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.FirstNames_Female_Implicit)))
                    DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Female_Implicit), NamesData.FirstNames_Female_Implicit);
            }
            if (needsMaleImplicitNames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.FirstNames_Male_Implicit)))
                    DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Male_Implicit), NamesData.FirstNames_Male_Implicit);
            }
            if (needsNaturalNames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.FirstNames_Natural)))
                    DataTools.dataStreams.Add(nameof(NamesData.FirstNames_Natural), NamesData.FirstNames_Natural);
            }
            if (needsSurnames)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(NamesData.Surnames)))
                    DataTools.dataStreams.Add(nameof(NamesData.Surnames), NamesData.Surnames);
            }
            if (needsUnicode)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(UnicodeData.ucd_all_flat)))
                    DataTools.dataStreams.Add(nameof(UnicodeData.ucd_all_flat), UnicodeData.ucd_all_flat);
            }
            if (needsUnicodeNoUnihan)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(UnicodeData.ucd_nounihan_flat)))
                    DataTools.dataStreams.Add(nameof(UnicodeData.ucd_nounihan_flat), UnicodeData.ucd_nounihan_flat);
            }
            if (needsWords)
            {
                if (!DataTools.dataStreams.ContainsKey(nameof(WordsData.words_alpha)))
                    DataTools.dataStreams.Add(nameof(WordsData.words_alpha), WordsData.words_alpha);
            }
        }
    }
}
