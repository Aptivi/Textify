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

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;
using Textify.Tools;

namespace Textify.Unicode
{
    internal static class UnicodeQueryHandler
    {
        static readonly Dictionary<UnicodeQueryType, (Stream, Ucd)> cachedQueries = [];

        internal static Stream UnpackUnicodeDataToStream(UnicodeQueryType type)
        {
            // If we've cached, just bail
            if (cachedQueries.ContainsKey(type))
                return cachedQueries[type].Item1;

            // Select XML file based on type
            var unicodeData = Array.Empty<byte>();
            var xmlFile = "";
            switch (type)
            {
                case UnicodeQueryType.Simple:
                    unicodeData = DataTools.GetDataFrom("ucd_nounihan_flat");
                    xmlFile = "ucd.nounihan.flat.xml";
                    break;
                case UnicodeQueryType.Unihan:
                    unicodeData = DataTools.GetDataFrom("ucd_unihan_flat");
                    xmlFile = "ucd.unihan.flat.xml";
                    break;
                case UnicodeQueryType.Full:
                    unicodeData = DataTools.GetDataFrom("ucd_all_flat");
                    xmlFile = "ucd.all.flat.xml";
                    break;
            }

            // Unpack the ZIP to stream
            var archiveByte = new MemoryStream(unicodeData);
            var archive = new ZipArchive(archiveByte, ZipArchiveMode.Read);

            // Open the XML to stream
            return archive.GetEntry(xmlFile).Open();
        }

        internal static Char Serialize(int charNum, UnicodeQueryType type)
        {
            var stream = UnpackUnicodeDataToStream(type);
            Repertoire repertoire = null;

            // Get the repertoire
            if (cachedQueries.ContainsKey(type))
                repertoire = cachedQueries[type].Item2.Repertoire;
            else
            {
                var serializer = new XmlSerializer(typeof(Ucd), "http://www.unicode.org/ns/2003/ucd/1.0");
                using var reader = XmlReader.Create(stream);
                var ucd = (Ucd)serializer.Deserialize(reader);
                repertoire = ucd.Repertoire;
            }

            // Now, get the Char instance by char number
            if (charNum > repertoire.Char.Length)
                throw new TextifyException($"Char number {charNum} exceeds {repertoire.Char.Length} available characters.");
            return repertoire.Char[charNum];
        }
    }
}
