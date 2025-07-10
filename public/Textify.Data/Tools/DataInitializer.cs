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
using System.IO;
using Textify.Data.Language;

namespace Textify.Data.Tools
{
    internal static class DataInitializer
    {
        internal static Stream GetStreamFrom(DataType types, string extension = "zip")
        {
            // Enumerate through all data types
            DataType[] enumTypes = (DataType[])Enum.GetValues(typeof(DataType));
            foreach (DataType enumType in enumTypes)
            {
                if (types.HasFlag(enumType))
                {
                    string name = GetResourceName(enumType);
                    return DataStreamTools.GetStreamFrom(name, extension);
                }
            }
            throw new NotImplementedException(LanguageTools.GetLocalized("TEXTIFY_DATA_EXCEPTION_NOSTREAM"));
        }

        private static string GetResourceName(DataType types) =>
            types switch
            {
                DataType.Names => "FirstNames",
                DataType.NamesFemale => "FirstNames_Female",
                DataType.NamesFemaleImplicit => "FirstNames_Female_Implicit",
                DataType.NamesMale => "FirstNames_Male",
                DataType.NamesMaleImplicit => "FirstNames_Male_Implicit",
                DataType.NamesNatural => "FirstNames_Natural",
                DataType.Surnames => "Surnames",
                DataType.Unicode => "ucd.all.flat",
                DataType.UnicodeNoUnihan => "ucd.nounihan.flat",
                DataType.UnicodeUnihan => "ucd.unihan.flat",
                DataType.UnicodeIndex => "ucd.all.flat.index",
                DataType.UnicodeNoUnihanIndex => "ucd.nounihan.flat.index",
                DataType.UnicodeUnihanIndex => "ucd.unihan.flat.index",
                DataType.Words => "words-clean-alpha",
                DataType.WordsFull => "words-clean",
                DataType.WordsDirty => "words_alpha",
                DataType.WordsDirtyFull => "words",
                DataType.WordsJustDirty => "bad-words",
                DataType.CommonWords => "words-common-clean",
                DataType.CommonWordsDirty => "words-common",
                _ => throw new NotImplementedException(LanguageTools.GetLocalized("TEXTIFY_DATA_EXCEPTION_NORESNAME")),
            };
    }
}
