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
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;
using Textify.Tools;

namespace Textify.Unicode
{
    internal static class UnicodeQueryHandler
    {
        static Stream cachedXmlStream = null;
        static Ucd cachedUcd = null;

        internal static void UnpackUnicodeDataToStream(UnicodeQueryType type)
        {
            // If we've cached, just bail
            if (cachedXmlStream == null)
            {
                // Select XML file based on type
                var unicodeData = Array.Empty<byte>();
                var xmlFile = "";
                switch (type)
                {
                    case UnicodeQueryType.Simple:
                        unicodeData = DataTools.GetDataFrom("ucd_nounihan_flat");
                        xmlFile = "ucd.nounihan.flat.xml";
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
                cachedXmlStream = archive.GetEntry(xmlFile).Open();
            }
        }

        internal static Char Serialize(int charNum)
        {
            if (cachedUcd is null)
            {
                var serializer = new XmlSerializer(typeof(Ucd), "http://www.unicode.org/ns/2003/ucd/1.0");
                using var reader = XmlReader.Create(cachedXmlStream);
                cachedUcd = (Ucd)serializer.Deserialize(reader);
            }

            return cachedUcd.Repertoire.Char[charNum];
        }
    }
}
