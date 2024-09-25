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
using System.Linq;
using System.Xml;
using Textify.Data.Tools;
using Textify.Tools;

namespace Textify.Data.Unicode
{
    internal static class UnicodeQueryHandler
    {
        static readonly Dictionary<UnicodeQueryType, List<(int, UnicodeCharInfo)>> cachedQueries = [];

        internal static Stream UnpackUnicodeDataToStream(UnicodeQueryType type)
        {
            // Select XML file based on type
            var unicodeData = Array.Empty<byte>();
            var xmlFile = "";
            switch (type)
            {
                case UnicodeQueryType.Simple:
                    DataInitializer.Initialize(DataType.UnicodeNoUnihan);
                    unicodeData = DataTools.GetDataFrom("ucd.nounihan.flat");
                    xmlFile = "ucd.nounihan.flat.xml";
                    break;
                case UnicodeQueryType.Unihan:
                    DataInitializer.Initialize(DataType.UnicodeUnihan);
                    unicodeData = DataTools.GetDataFrom("ucd.unihan.flat");
                    xmlFile = "ucd.unihan.flat.xml";
                    break;
                case UnicodeQueryType.Full:
                    DataInitializer.Initialize(DataType.Unicode);
                    unicodeData = DataTools.GetDataFrom("ucd.all.flat");
                    xmlFile = "ucd.all.flat.xml";
                    break;
            }

            // Unpack the ZIP to stream
            var archiveByte = new MemoryStream(unicodeData);
            var archive = new ZipArchive(archiveByte, ZipArchiveMode.Read);

            // Open the XML to stream
            return archive.GetEntry(xmlFile).Open();
        }

        internal static UnicodeCharInfo Serialize(int charNum, UnicodeQueryType type)
        {
            var stream = UnpackUnicodeDataToStream(type);
            UnicodeCharInfo charInfo;

            // Generate the char info if needed
            if (cachedQueries.ContainsKey(type) && cachedQueries[type].Any((tuple) => tuple.Item1 == charNum))
            {
                var infoList = cachedQueries[type];
                foreach (var infoTuple in infoList)
                    if (infoTuple.Item1 == charNum)
                        return infoTuple.Item2;
                throw new TextifyException($"There is no character info for this number {charNum}, {type}.");
            }
            else
            {
                using var reader = XmlReader.Create(stream);
                if (!reader.SkipToUcd())
                    throw new TextifyException($"There is no UCD root element {charNum}, {type}.");
                reader.MoveToContent();
                if (!reader.SkipToNum(charNum))
                    throw new TextifyException($"There is no character info for this number {charNum}, {type}.");
                charInfo = ProcessInfo(reader);
                if (cachedQueries.ContainsKey(type))
                    cachedQueries[type].Add((charNum, charInfo));
                else
                    cachedQueries.Add(type, [(charNum, charInfo)]);
            }

            // Return the final result
            return charInfo;
        }

        private static bool SkipToUcd(this XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "ucd")
                    return true;
                reader.Skip();
            }
            return false;
        }

        private static bool SkipToNum(this XmlReader reader, int charNum)
        {
            while (reader.ReadToFollowing("char"))
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "char" && reader.HasAttributes && reader.GetAttribute("cp") == $"{charNum:X4}")
                    return true;
                reader.Skip();
            }
            return false;
        }

        private static UnicodeCharInfo ProcessInfo(XmlReader reader)
        {
            List<Namealias> nameAliases = [];
            var charInfo = new UnicodeCharInfo()
            {
                // General
                Cp = reader.GetAttribute("cp"),

                // Non-unihan
                Age = double.TryParse(reader.GetAttribute("age"), out double age) ? age : 0.0,
                Na = reader.GetAttribute("na"),
                Jsn = reader.GetAttribute("JSN"),
                Gc = reader.GetAttribute("gc"),
                Ccc = int.TryParse(reader.GetAttribute("ccc"), out int ccc) ? ccc : 0,
                Dt = reader.GetAttribute("dt"),
                Dm = reader.GetAttribute("dm"),
                Nt = reader.GetAttribute("nt"),
                Nv = reader.GetAttribute("nv"),
                Bc = reader.GetAttribute("bc"),
                Bpt = reader.GetAttribute("bpt"),
                Bpb = reader.GetAttribute("bpb"),
                BidiM = reader.GetAttribute("Bidi_M"),
                Bmg = reader.GetAttribute("bmg"),
                Suc = reader.GetAttribute("suc"),
                Slc = reader.GetAttribute("slc"),
                Stc = reader.GetAttribute("stc"),
                Uc = reader.GetAttribute("uc"),
                Lc = reader.GetAttribute("lc"),
                Tc = reader.GetAttribute("tc"),
                Scf = reader.GetAttribute("scf"),
                Cf = reader.GetAttribute("cf"),
                Jt = reader.GetAttribute("jt"),
                Jg = reader.GetAttribute("jg"),
                Ea = reader.GetAttribute("ea"),
                Lb = reader.GetAttribute("lb"),
                Sc = reader.GetAttribute("sc"),
                Scx = reader.GetAttribute("scx"),
                Dash = reader.GetAttribute("Dash"),
                WSpace = reader.GetAttribute("WSpace"),
                Hyphen = reader.GetAttribute("Hyphen"),
                QMark = reader.GetAttribute("QMark"),
                Radical = reader.GetAttribute("Radical"),
                Ideo = reader.GetAttribute("Ideo"),
                UIdeo = reader.GetAttribute("UIdeo"),
                IDSB = reader.GetAttribute("IDSB"),
                IDST = reader.GetAttribute("IDST"),
                Hst = reader.GetAttribute("hst"),
                DI = reader.GetAttribute("DI"),
                ODI = reader.GetAttribute("ODI"),
                Alpha = reader.GetAttribute("Alpha"),
                OAlpha = reader.GetAttribute("OAlpha"),
                Upper = reader.GetAttribute("Upper"),
                OUpper = reader.GetAttribute("OUpper"),
                Lower = reader.GetAttribute("Lower"),
                OLower = reader.GetAttribute("OLower"),
                Math = reader.GetAttribute("Math"),
                OMath = reader.GetAttribute("OMath"),
                Hex = reader.GetAttribute("Hex"),
                AHex = reader.GetAttribute("AHex"),
                NChar = reader.GetAttribute("NChar"),
                VS = reader.GetAttribute("VS"),
                BidiC = reader.GetAttribute("Bidi_C"),
                JoinC = reader.GetAttribute("Join_C"),
                GrBase = reader.GetAttribute("Gr_Base"),
                GrExt = reader.GetAttribute("Gr_Ext"),
                OGrExt = reader.GetAttribute("OGr_Ext"),
                GrLink = reader.GetAttribute("Gr_Link"),
                STerm = reader.GetAttribute("STerm"),
                Ext = reader.GetAttribute("Ext"),
                Term = reader.GetAttribute("Term"),
                Dia = reader.GetAttribute("Dia"),
                Dep = reader.GetAttribute("Dep"),
                IDS = reader.GetAttribute("IDS"),
                OIDS = reader.GetAttribute("OIDS"),
                XIDS = reader.GetAttribute("XIDS"),
                IDC = reader.GetAttribute("IDC"),
                OIDC = reader.GetAttribute("OIDC"),
                XIDC = reader.GetAttribute("XIDC"),
                SD = reader.GetAttribute("SD"),
                LOE = reader.GetAttribute("LOE"),
                PatWS = reader.GetAttribute("Pat_WS"),
                PatSyn = reader.GetAttribute("Pat_Syn"),
                GCB = reader.GetAttribute("GCB"),
                WB = reader.GetAttribute("WB"),
                SB = reader.GetAttribute("SB"),
                CE = reader.GetAttribute("CE"),
                CompEx = reader.GetAttribute("Comp_Ex"),
                NFCQC = reader.GetAttribute("NFC_QC"),
                NFDQC = reader.GetAttribute("NFD_QC"),
                NFKCQC = reader.GetAttribute("NFKC_QC"),
                NFKDQC = reader.GetAttribute("NFKD_QC"),
                XONFC = reader.GetAttribute("XO_NFC"),
                XONFD = reader.GetAttribute("XO_NFD"),
                XONFKC = reader.GetAttribute("XO_NFKC"),
                XONFKD = reader.GetAttribute("XO_NFKD"),
                FCNFKC = reader.GetAttribute("FC_NFKC"),
                CI = reader.GetAttribute("CI"),
                Cased = reader.GetAttribute("Cased"),
                CWCF = reader.GetAttribute("CWCF"),
                CWCM = reader.GetAttribute("CWCM"),
                CWKCF = reader.GetAttribute("CWKCF"),
                CWL = reader.GetAttribute("CWL"),
                CWT = reader.GetAttribute("CWT"),
                CWU = reader.GetAttribute("CWU"),
                NFKCCF = reader.GetAttribute("NFKC_CF"),
                InSC = reader.GetAttribute("InSC"),
                InPC = reader.GetAttribute("InPC"),
                PCM = reader.GetAttribute("PCM"),
                Vo = reader.GetAttribute("vo"),
                RI = reader.GetAttribute("RI"),
                Blk = reader.GetAttribute("blk"),
                Isc = reader.GetAttribute("isc"),
                Na1 = reader.GetAttribute("na1"),
                Emoji = reader.GetAttribute("Emoji"),
                EPres = reader.GetAttribute("EPres"),
                EMod = reader.GetAttribute("EMod"),
                EBase = reader.GetAttribute("EBase"),
                EComp = reader.GetAttribute("EComp"),
                ExtPict = reader.GetAttribute("ExtPict"),
                NFKCSCF = reader.GetAttribute("NFKC_SCF"),
                IdCompatMathStart = reader.GetAttribute("ID_Compat_Math_Start"),
                IdCompatMathContinue = reader.GetAttribute("ID_Compat_Math_Continue"),
                IDSU = reader.GetAttribute("IDSU"),
                InCB = reader.GetAttribute("InCB"),
                MCM = reader.GetAttribute("MCM"),

                // Unihan
                KCompatibilityVariant = reader.GetAttribute("kCompatibilityVariant"),
                KRSUnicode = reader.GetAttribute("kRSUnicode"),
                KIRGGSource = reader.GetAttribute("kIRG_GSource"),
                KIRGTSource = reader.GetAttribute("kIRG_TSource"),
                KIRGJSource = reader.GetAttribute("kIRG_JSource"),
                KIRGKSource = reader.GetAttribute("kIRG_KSource"),
                KIRGKPSource = reader.GetAttribute("kIRG_KPSource"),
                KIRGVSource = reader.GetAttribute("kIRG_VSource"),
                KIRGHSource = reader.GetAttribute("kIRG_HSource"),
                KIRGUSource = reader.GetAttribute("kIRG_USource"),
                KIRGMSource = reader.GetAttribute("kIRG_MSource"),
                KIRGUKSource = reader.GetAttribute("kIRG_UKSource"),
                KIRGSSource = reader.GetAttribute("kIRG_SSource"),
                KDefinition = reader.GetAttribute("kDefinition"),
                KMandarin = reader.GetAttribute("kMandarin"),
                KCihaiT = reader.GetAttribute("kCihaiT"),
                KSBGY = reader.GetAttribute("kSBGY"),
                KCangjie = reader.GetAttribute("kCangjie"),
                KKangXi = reader.GetAttribute("kKangXi"),
                KHanyuPinyin = reader.GetAttribute("kHanyuPinyin"),
                KIRGHanyuDaZidian = double.TryParse(reader.GetAttribute("kIRGHanyuDaZidian"), out double kIRGHanyuDaZidian) ? kIRGHanyuDaZidian : 0.0,
                KIRGKangXi = double.TryParse(reader.GetAttribute("kIRGKangXi"), out double kIRGKangXi) ? kIRGKangXi : 0.0,
                KMorohashi = reader.GetAttribute("kMorohashi"),
                KTotalStrokes = reader.GetAttribute("kTotalStrokes"),
                KJapanese = reader.GetAttribute("kJapanese"),
                KMojiJoho = reader.GetAttribute("kMojiJoho"),
                KFanqie = reader.GetAttribute("kFanqie"),
                KStrange = reader.GetAttribute("kStrange"),
                KRSAdobeJapan16 = reader.GetAttribute("kRSAdobe_Japan1_6"),
                KCantonese = reader.GetAttribute("kCantonese"),
                KPhonetic = reader.GetAttribute("kPhonetic"),
            };
            var nameAliasTree = reader.ReadSubtree();
            while (nameAliasTree.ReadToFollowing("name-alias"))
            {
                var nameAlias = new Namealias()
                {
                    Alias = nameAliasTree.GetAttribute("alias"),
                    Type = nameAliasTree.GetAttribute("type"),
                };
                nameAliases.Add(nameAlias);
            }
            charInfo.Namealias = [.. nameAliases];
            return charInfo;
        }
    }
}
