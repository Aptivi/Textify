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

using Textify.Data.Tools;

namespace Textify.Data.Analysis.Tools
{
    /// <summary>
    /// Data initialization class
    /// </summary>
    internal static class DataInitializer
    {
        /// <summary>
        /// Initializes all the needed data
        /// </summary>
        /// <param name="types">Types to initialize</param>
        internal static void Initialize(DataType types)
        {
            // Some variables for needed data
            bool needsNames = types.HasFlag(DataType.Names);
            bool needsFemaleNames = types.HasFlag(DataType.NamesFemale);
            bool needsMaleNames = types.HasFlag(DataType.NamesMale);
            bool needsFemaleImplicitNames = types.HasFlag(DataType.NamesFemaleImplicit);
            bool needsMaleImplicitNames = types.HasFlag(DataType.NamesMaleImplicit);
            bool needsNaturalNames = types.HasFlag(DataType.NamesNatural);
            bool needsSurnames = types.HasFlag(DataType.Surnames);
            bool needsUnicode = types.HasFlag(DataType.Unicode);
            bool needsUnicodeNoUnihan = types.HasFlag(DataType.UnicodeNoUnihan);
            bool needsUnicodeUnihan = types.HasFlag(DataType.UnicodeUnihan);
            bool needsWords = types.HasFlag(DataType.Words);
            bool needsWordsFull = types.HasFlag(DataType.WordsFull);
            bool needsWordsDirty = types.HasFlag(DataType.WordsDirty);
            bool needsWordsDirtyFull = types.HasFlag(DataType.WordsDirtyFull);
            bool needsWordsJustDirty = types.HasFlag(DataType.WordsJustDirty);
            bool needsCommonWords = types.HasFlag(DataType.CommonWords);
            bool needsCommonWordsDirty = types.HasFlag(DataType.CommonWordsDirty);

            // Go through all the types
            if (needsNames)
            {
                if (!DataTools.dataStreams.ContainsKey("FirstNames"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("FirstNames");
                    DataTools.dataStreams.Add("FirstNames", bytes);
                }
            }
            if (needsFemaleNames)
            {
                if (!DataTools.dataStreams.ContainsKey("FirstNames_Female"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("FirstNames_Female");
                    DataTools.dataStreams.Add("FirstNames_Female", bytes);
                }
            }
            if (needsMaleNames)
            {
                if (!DataTools.dataStreams.ContainsKey("FirstNames_Male"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("FirstNames_Male");
                    DataTools.dataStreams.Add("FirstNames_Male", bytes);
                }
            }
            if (needsFemaleImplicitNames)
            {
                if (!DataTools.dataStreams.ContainsKey("FirstNames_Female_Implicit"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("FirstNames_Female_Implicit");
                    DataTools.dataStreams.Add("FirstNames_Female_Implicit", bytes);
                }
            }
            if (needsMaleImplicitNames)
            {
                if (!DataTools.dataStreams.ContainsKey("FirstNames_Male_Implicit"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("FirstNames_Male_Implicit");
                    DataTools.dataStreams.Add("FirstNames_Male_Implicit", bytes);
                }
            }
            if (needsNaturalNames)
            {
                if (!DataTools.dataStreams.ContainsKey("FirstNames_Natural"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("FirstNames_Natural");
                    DataTools.dataStreams.Add("FirstNames_Natural", bytes);
                }
            }
            if (needsSurnames)
            {
                if (!DataTools.dataStreams.ContainsKey("Surnames"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("Surnames");
                    DataTools.dataStreams.Add("Surnames", bytes);
                }
            }
            if (needsUnicode)
            {
                if (!DataTools.dataStreams.ContainsKey("ucd.all.flat"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("ucd.all.flat");
                    DataTools.dataStreams.Add("ucd.all.flat", bytes);
                }
            }
            if (needsUnicodeNoUnihan)
            {
                if (!DataTools.dataStreams.ContainsKey("ucd.nounihan.flat"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("ucd.nounihan.flat");
                    DataTools.dataStreams.Add("ucd.nounihan.flat", bytes);
                }
            }
            if (needsUnicodeUnihan)
            {
                if (!DataTools.dataStreams.ContainsKey("ucd.unihan.flat"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("ucd.unihan.flat");
                    DataTools.dataStreams.Add("ucd.unihan.flat", bytes);
                }
            }
            if (needsWords)
            {
                if (!DataTools.dataStreams.ContainsKey("words-clean-alpha"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("words-clean-alpha");
                    DataTools.dataStreams.Add("words-clean-alpha", bytes);
                }
            }
            if (needsWordsFull)
            {
                if (!DataTools.dataStreams.ContainsKey("words-clean"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("words-clean");
                    DataTools.dataStreams.Add("words-clean", bytes);
                }
            }
            if (needsWordsDirty)
            {
                if (!DataTools.dataStreams.ContainsKey("words_alpha"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("words_alpha");
                    DataTools.dataStreams.Add("words_alpha", bytes);
                }
            }
            if (needsWordsDirtyFull)
            {
                if (!DataTools.dataStreams.ContainsKey("words"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("words");
                    DataTools.dataStreams.Add("words", bytes);
                }
            }
            if (needsWordsJustDirty)
            {
                if (!DataTools.dataStreams.ContainsKey("bad-words"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("bad-words");
                    DataTools.dataStreams.Add("bad-words", bytes);
                }
            }
            if (needsCommonWords)
            {
                if (!DataTools.dataStreams.ContainsKey("words-common-clean"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("words-common-clean");
                    DataTools.dataStreams.Add("words-common-clean", bytes);
                }
            }
            if (needsCommonWordsDirty)
            {
                if (!DataTools.dataStreams.ContainsKey("words-common"))
                {
                    byte[] bytes = DataStreamTools.GetDataFrom("words-common");
                    DataTools.dataStreams.Add("words-common", bytes);
                }
            }
        }
    }
}
