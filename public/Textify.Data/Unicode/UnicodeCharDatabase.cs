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

using System.Xml.Serialization;

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Character
    /// </summary>
    [XmlRoot(ElementName = "char")]
    public class UnicodeCharInfo
    {
        /// <summary>
        /// Name aliases
        /// </summary>
        [XmlElement(ElementName = "namealias")]
        public Namealias[] Namealias { get; set; } = [];

        /// <summary>
        /// Codepage number
        /// </summary>
        [XmlAttribute(AttributeName = "cp")]
        public string Cp { get; set; } = "";

        /// <summary>
        /// The version of Unicode in which a code point was assigned to an abstract character, or made a surrogate or non-character.
        /// </summary>
        [XmlAttribute(AttributeName = "age")]
        public double Age { get; set; }

        /// <summary>
        /// General category
        /// </summary>
        [XmlAttribute(AttributeName = "gc")]
        public string Gc { get; set; } = "";

        /// <summary>
        /// Combining class
        /// </summary>
        [XmlAttribute(AttributeName = "ccc")]
        public int Ccc { get; set; }

        /// <summary>
        /// Decomposition type
        /// </summary>
        [XmlAttribute(AttributeName = "dt")]
        public string Dt { get; set; } = "";

        /// <summary>
        /// Decomposition mapping
        /// </summary>
        [XmlAttribute(AttributeName = "dm")]
        public string Dm { get; set; } = "";

        /// <summary>
        /// Jamo Short Name
        /// </summary>
        [XmlAttribute(AttributeName = "JSN")]
        public string Jsn { get; set; } = "";

        /// <summary>
        /// The code point of a character whose glyph is typically a mirrored image of the glyph for the current character.
        /// </summary>
        [XmlAttribute(AttributeName = "bmg")]
        public string Bmg { get; set; } = "";

        /// <summary>
        /// ISO 10646 comment
        /// </summary>
        [XmlAttribute(AttributeName = "isc")]
        public string Isc { get; set; } = "";

        /// <summary>
        /// Numeric type
        /// </summary>
        [XmlAttribute(AttributeName = "nt")]
        public string Nt { get; set; } = "";

        /// <summary>
        /// Numeric value
        /// </summary>
        [XmlAttribute(AttributeName = "nv")]
        public string Nv { get; set; } = "";

        /// <summary>
        /// Bidirectionality
        /// </summary>
        [XmlAttribute(AttributeName = "bc")]
        public string Bc { get; set; } = "";

        /// <summary>
        /// Bidi paired bracket type
        /// </summary>
        [XmlAttribute(AttributeName = "bpt")]
        public string Bpt { get; set; } = "";

        /// <summary>
        /// Bidi paired bracket
        /// </summary>
        [XmlAttribute(AttributeName = "bpb")]
        public string Bpb { get; set; } = "";

        /// <summary>
        /// Mirrored (Bidi)
        /// </summary>
        [XmlAttribute(AttributeName = "Bidi_M")]
        public string BidiM { get; set; } = "";

        /// <summary>
        /// Simple case mapping
        /// </summary>
        [XmlAttribute(AttributeName = "suc")]
        public string Suc { get; set; } = "";

        /// <summary>
        /// Simple case mapping
        /// </summary>
        [XmlAttribute(AttributeName = "slc")]
        public string Slc { get; set; } = "";

        /// <summary>
        /// Simple case mapping
        /// </summary>
        [XmlAttribute(AttributeName = "stc")]
        public string Stc { get; set; } = "";

        /// <summary>
        /// Case mapping
        /// </summary>
        [XmlAttribute(AttributeName = "uc")]
        public string Uc { get; set; } = "";

        /// <summary>
        /// Case mapping
        /// </summary>
        [XmlAttribute(AttributeName = "lc")]
        public string Lc { get; set; } = "";

        /// <summary>
        /// Case mapping
        /// </summary>
        [XmlAttribute(AttributeName = "tc")]
        public string Tc { get; set; } = "";

        /// <summary>
        /// Simple case folding
        /// </summary>
        [XmlAttribute(AttributeName = "scf")]
        public string Scf { get; set; } = "";

        /// <summary>
        /// Case folding
        /// </summary>
        [XmlAttribute(AttributeName = "cf")]
        public string Cf { get; set; } = "";

        /// <summary>
        /// Joining class
        /// </summary>
        [XmlAttribute(AttributeName = "jt")]
        public string Jt { get; set; } = "";

        /// <summary>
        /// Joining group
        /// </summary>
        [XmlAttribute(AttributeName = "jg")]
        public string Jg { get; set; } = "";

        /// <summary>
        /// East Asian width
        /// </summary>
        [XmlAttribute(AttributeName = "ea")]
        public string Ea { get; set; } = "";

        /// <summary>
        /// Line break
        /// </summary>
        [XmlAttribute(AttributeName = "lb")]
        public string Lb { get; set; } = "";

        /// <summary>
        /// Script
        /// </summary>
        [XmlAttribute(AttributeName = "sc")]
        public string Sc { get; set; } = "";

        /// <summary>
        /// Script extension
        /// </summary>
        [XmlAttribute(AttributeName = "scx")]
        public string Scx { get; set; } = "";

        /// <summary>
        /// Dash
        /// </summary>
        [XmlAttribute(AttributeName = "Dash")]
        public string Dash { get; set; } = "";

        /// <summary>
        /// White space
        /// </summary>
        [XmlAttribute(AttributeName = "WSpace")]
        public string WSpace { get; set; } = "";

        /// <summary>
        /// Hyphen
        /// </summary>
        [XmlAttribute(AttributeName = "Hyphen")]
        public string Hyphen { get; set; } = "";

        /// <summary>
        /// Question mark
        /// </summary>
        [XmlAttribute(AttributeName = "QMark")]
        public string QMark { get; set; } = "";

        /// <summary>
        /// Radical
        /// </summary>
        [XmlAttribute(AttributeName = "Radical")]
        public string Radical { get; set; } = "";

        /// <summary>
        /// Ideographic
        /// </summary>
        [XmlAttribute(AttributeName = "Ideo")]
        public string Ideo { get; set; } = "";

        /// <summary>
        /// Unified Ideographic
        /// </summary>
        [XmlAttribute(AttributeName = "UIdeo")]
        public string UIdeo { get; set; } = "";

        /// <summary>
        /// IDS Binary Operator
        /// </summary>
        [XmlAttribute(AttributeName = "IDSB")]
        public string IDSB { get; set; } = "";

        /// <summary>
        /// IDS Trinary Operator
        /// </summary>
        [XmlAttribute(AttributeName = "IDST")]
        public string IDST { get; set; } = "";

        /// <summary>
        /// Hangul Syllable Type
        /// </summary>
        [XmlAttribute(AttributeName = "hst")]
        public string Hst { get; set; } = "";

        /// <summary>
        /// Default Ignorable Code Point
        /// </summary>
        [XmlAttribute(AttributeName = "DI")]
        public string DI { get; set; } = "";

        /// <summary>
        /// Other Default Ignorable Code Point
        /// </summary>
        [XmlAttribute(AttributeName = "ODI")]
        public string ODI { get; set; } = "";

        /// <summary>
        /// Alphabetic
        /// </summary>
        [XmlAttribute(AttributeName = "Alpha")]
        public string Alpha { get; set; } = "";

        /// <summary>
        /// Other Alphabetic
        /// </summary>
        [XmlAttribute(AttributeName = "OAlpha")]
        public string OAlpha { get; set; } = "";

        /// <summary>
        /// Uppercase
        /// </summary>
        [XmlAttribute(AttributeName = "Upper")]
        public string Upper { get; set; } = "";

        /// <summary>
        /// Other Uppercase
        /// </summary>
        [XmlAttribute(AttributeName = "OUpper")]
        public string OUpper { get; set; } = "";

        /// <summary>
        /// Lowercase
        /// </summary>
        [XmlAttribute(AttributeName = "Lower")]
        public string Lower { get; set; } = "";

        /// <summary>
        /// Other Lowercase
        /// </summary>
        [XmlAttribute(AttributeName = "OLower")]
        public string OLower { get; set; } = "";

        /// <summary>
        /// Mathematic
        /// </summary>
        [XmlAttribute(AttributeName = "Math")]
        public string Math { get; set; } = "";

        /// <summary>
        /// Other Mathematic
        /// </summary>
        [XmlAttribute(AttributeName = "OMath")]
        public string OMath { get; set; } = "";

        /// <summary>
        /// Hex digit
        /// </summary>
        [XmlAttribute(AttributeName = "Hex")]
        public string Hex { get; set; } = "";

        /// <summary>
        /// ASCII Hex digit
        /// </summary>
        [XmlAttribute(AttributeName = "AHex")]
        public string AHex { get; set; } = "";

        /// <summary>
        /// Non-character code point
        /// </summary>
        [XmlAttribute(AttributeName = "NChar")]
        public string NChar { get; set; } = "";

        /// <summary>
        /// Variation Selector
        /// </summary>
        [XmlAttribute(AttributeName = "VS")]
        public string VS { get; set; } = "";

        /// <summary>
        /// Bidi Control
        /// </summary>
        [XmlAttribute(AttributeName = "Bidi_C")]
        public string BidiC { get; set; } = "";

        /// <summary>
        /// Join Control
        /// </summary>
        [XmlAttribute(AttributeName = "Join_C")]
        public string JoinC { get; set; } = "";

        /// <summary>
        /// Grapheme Base
        /// </summary>
        [XmlAttribute(AttributeName = "Gr_Base")]
        public string GrBase { get; set; } = "";

        /// <summary>
        /// Grapheme Extended
        /// </summary>
        [XmlAttribute(AttributeName = "Gr_Ext")]
        public string GrExt { get; set; } = "";

        /// <summary>
        /// Other Grapheme Extended
        /// </summary>
        [XmlAttribute(AttributeName = "OGr_Ext")]
        public string OGrExt { get; set; } = "";

        /// <summary>
        /// Grapheme Link
        /// </summary>
        [XmlAttribute(AttributeName = "Gr_Link")]
        public string GrLink { get; set; } = "";

        /// <summary>
        /// Sentence Terminal
        /// </summary>
        [XmlAttribute(AttributeName = "STerm")]
        public string STerm { get; set; } = "";

        /// <summary>
        /// Extender
        /// </summary>
        [XmlAttribute(AttributeName = "Ext")]
        public string Ext { get; set; } = "";

        /// <summary>
        /// Terminal
        /// </summary>
        [XmlAttribute(AttributeName = "Term")]
        public string Term { get; set; } = "";

        /// <summary>
        /// Diacritic
        /// </summary>
        [XmlAttribute(AttributeName = "Dia")]
        public string Dia { get; set; } = "";

        /// <summary>
        /// Deprecated
        /// </summary>
        [XmlAttribute(AttributeName = "Dep")]
        public string Dep { get; set; } = "";

        /// <summary>
        /// ID Start
        /// </summary>
        [XmlAttribute(AttributeName = "IDS")]
        public string IDS { get; set; } = "";

        /// <summary>
        /// Other ID Start
        /// </summary>
        [XmlAttribute(AttributeName = "OIDS")]
        public string OIDS { get; set; } = "";

        /// <summary>
        /// XID Start
        /// </summary>
        [XmlAttribute(AttributeName = "XIDS")]
        public string XIDS { get; set; } = "";

        /// <summary>
        /// ID Continue
        /// </summary>
        [XmlAttribute(AttributeName = "IDC")]
        public string IDC { get; set; } = "";

        /// <summary>
        /// Other ID Continue
        /// </summary>
        [XmlAttribute(AttributeName = "OIDC")]
        public string OIDC { get; set; } = "";

        /// <summary>
        /// XID Continue
        /// </summary>
        [XmlAttribute(AttributeName = "XIDC")]
        public string XIDC { get; set; } = "";

        /// <summary>
        /// Sofrt Dotted
        /// </summary>
        [XmlAttribute(AttributeName = "SD")]
        public string SD { get; set; } = "";

        /// <summary>
        /// Logical Order Exception
        /// </summary>
        [XmlAttribute(AttributeName = "LOE")]
        public string LOE { get; set; } = "";

        /// <summary>
        /// Pattern Whitespace
        /// </summary>
        [XmlAttribute(AttributeName = "Pat_WS")]
        public string PatWS { get; set; } = "";

        /// <summary>
        /// Pattern Syntax
        /// </summary>
        [XmlAttribute(AttributeName = "Pat_Syn")]
        public string PatSyn { get; set; } = "";

        /// <summary>
        /// Grapheme Cluster Break
        /// </summary>
        [XmlAttribute(AttributeName = "GCB")]
        public string GCB { get; set; } = "";

        /// <summary>
        /// Word Break
        /// </summary>
        [XmlAttribute(AttributeName = "WB")]
        public string WB { get; set; } = "";

        /// <summary>
        /// Sentence Break
        /// </summary>
        [XmlAttribute(AttributeName = "SB")]
        public string SB { get; set; } = "";

        /// <summary>
        /// Composition Exclusion
        /// </summary>
        [XmlAttribute(AttributeName = "CE")]
        public string CE { get; set; } = "";

        /// <summary>
        /// Full Composition Exclusion
        /// </summary>
        [XmlAttribute(AttributeName = "Comp_Ex")]
        public string CompEx { get; set; } = "";

        /// <summary>
        /// NFC Quick Check
        /// </summary>
        [XmlAttribute(AttributeName = "NFC_QC")]
        public string NFCQC { get; set; } = "";

        /// <summary>
        /// NFD Quick Check
        /// </summary>
        [XmlAttribute(AttributeName = "NFD_QC")]
        public string NFDQC { get; set; } = "";

        /// <summary>
        /// NFKC Quick Check
        /// </summary>
        [XmlAttribute(AttributeName = "NFKC_QC")]
        public string NFKCQC { get; set; } = "";

        /// <summary>
        /// NFKD Quick Check
        /// </summary>
        [XmlAttribute(AttributeName = "NFKD_QC")]
        public string NFKDQC { get; set; } = "";

        /// <summary>
        /// Expands on NFC
        /// </summary>
        [XmlAttribute(AttributeName = "XO_NFC")]
        public string XONFC { get; set; } = "";

        /// <summary>
        /// Expands on NFD
        /// </summary>
        [XmlAttribute(AttributeName = "XO_NFD")]
        public string XONFD { get; set; } = "";

        /// <summary>
        /// Expands on NFKC
        /// </summary>
        [XmlAttribute(AttributeName = "XO_NFKC")]
        public string XONFKC { get; set; } = "";

        /// <summary>
        /// Expands on NFKD
        /// </summary>
        [XmlAttribute(AttributeName = "XO_NFKD")]
        public string XONFKD { get; set; } = "";

        /// <summary>
        /// FC NFKC Closure
        /// </summary>
        [XmlAttribute(AttributeName = "FC_NFKC")]
        public string FCNFKC { get; set; } = "";

        /// <summary>
        /// Case Ignorable
        /// </summary>
        [XmlAttribute(AttributeName = "CI")]
        public string CI { get; set; } = "";

        /// <summary>
        /// Cased
        /// </summary>
        [XmlAttribute(AttributeName = "Cased")]
        public string Cased { get; set; } = "";

        /// <summary>
        /// Changes when Casefolded
        /// </summary>
        [XmlAttribute(AttributeName = "CWCF")]
        public string CWCF { get; set; } = "";

        /// <summary>
        /// Changes when Casemapped
        /// </summary>
        [XmlAttribute(AttributeName = "CWCM")]
        public string CWCM { get; set; } = "";

        /// <summary>
        /// Changes when NFKC Casefolded
        /// </summary>
        [XmlAttribute(AttributeName = "CWKCF")]
        public string CWKCF { get; set; } = "";

        /// <summary>
        /// Changes when Lowercased
        /// </summary>
        [XmlAttribute(AttributeName = "CWL")]
        public string CWL { get; set; } = "";

        /// <summary>
        /// Changes when Titlecased
        /// </summary>
        [XmlAttribute(AttributeName = "CWT")]
        public string CWT { get; set; } = "";

        /// <summary>
        /// Changes when Uppercased
        /// </summary>
        [XmlAttribute(AttributeName = "CWU")]
        public string CWU { get; set; } = "";

        /// <summary>
        /// NFKC Casefold
        /// </summary>
        [XmlAttribute(AttributeName = "NFKC_CF")]
        public string NFKCCF { get; set; } = "";

        /// <summary>
        /// Indic Syllabic Category
        /// </summary>
        [XmlAttribute(AttributeName = "InSC")]
        public string InSC { get; set; } = "";

        /// <summary>
        /// Indic Positional Category
        /// </summary>
        [XmlAttribute(AttributeName = "InPC")]
        public string InPC { get; set; } = "";

        /// <summary>
        /// Prepended Concatenation Mark
        /// </summary>
        [XmlAttribute(AttributeName = "PCM")]
        public string PCM { get; set; } = "";

        /// <summary>
        /// Vertical Orientation
        /// </summary>
        [XmlAttribute(AttributeName = "vo")]
        public string Vo { get; set; } = "";

        /// <summary>
        /// Regional Indicator
        /// </summary>
        [XmlAttribute(AttributeName = "RI")]
        public string RI { get; set; } = "";

        /// <summary>
        /// Block
        /// </summary>
        [XmlAttribute(AttributeName = "blk")]
        public string Blk { get; set; } = "";

        /// <summary>
        /// The name this character had in version 1.0 of the Unicode standard
        /// </summary>
        [XmlAttribute(AttributeName = "na1")]
        public string Na1 { get; set; } = "";

        /// <summary>
        /// The name given by the current version of the Unicode standard
        /// </summary>
        [XmlAttribute(AttributeName = "na")]
        public string Na { get; set; } = "";

        /// <summary>
        /// Is the character an Emoji?
        /// </summary>
        [XmlAttribute(AttributeName = "Emoji")]
        public string Emoji { get; set; } = "";

        /// <summary>
        /// Emoji Pres?
        /// </summary>
        [XmlAttribute(AttributeName = "EPres")]
        public string EPres { get; set; } = "";

        /// <summary>
        /// Emoji Mod?
        /// </summary>
        [XmlAttribute(AttributeName = "EMod")]
        public string EMod { get; set; } = "";

        /// <summary>
        /// Emoji Base?
        /// </summary>
        [XmlAttribute(AttributeName = "EBase")]
        public string EBase { get; set; } = "";

        /// <summary>
        /// Emoji Comp?
        /// </summary>
        [XmlAttribute(AttributeName = "EComp")]
        public string EComp { get; set; } = "";

        /// <summary>
        /// Ext Pict?
        /// </summary>
        [XmlAttribute(AttributeName = "ExtPict")]
        public string ExtPict { get; set; } = "";

        /// <summary>
        /// NFKC_SCF?
        /// </summary>
        [XmlAttribute(AttributeName = "NFKC_SCF")]
        public string NFKCSCF { get; set; } = "";

        /// <summary>
        /// ID_Compat_Math_Start
        /// </summary>
        [XmlAttribute(AttributeName = "ID_Compat_Math_Start")]
        public string IdCompatMathStart { get; set; } = "";

        /// <summary>
        /// ID_Compat_Math_Continue
        /// </summary>
        [XmlAttribute(AttributeName = "ID_Compat_Math_Continue")]
        public string IdCompatMathContinue { get; set; } = "";

        /// <summary>
        /// IDSU?
        /// </summary>
        [XmlAttribute(AttributeName = "IDSU")]
        public string IDSU { get; set; } = "";

        /// <summary>
        /// InCB?
        /// </summary>
        [XmlAttribute(AttributeName = "InCB")]
        public string InCB { get; set; } = "";

        /// <summary>
        /// MCM?
        /// </summary>
        [XmlAttribute(AttributeName = "MCM")]
        public string MCM { get; set; } = "";

        // Unihan info

        /// <summary>
        /// Compatibility variant
        /// </summary>
        [XmlAttribute(AttributeName = "kCompatibilityVariant")]
        public string KCompatibilityVariant { get; set; } = "";

        /// <summary>
        /// RS Unicode
        /// </summary>
        [XmlAttribute(AttributeName = "kRSUnicode")]
        public string KRSUnicode { get; set; } = "";

        /// <summary>
        /// IRD G Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_GSource")]
        public string KIRGGSource { get; set; } = "";

        /// <summary>
        /// IRD T Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_TSource")]
        public string KIRGTSource { get; set; } = "";

        /// <summary>
        /// IRD J Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_JSource")]
        public string KIRGJSource { get; set; } = "";

        /// <summary>
        /// IRD K Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_KSource")]
        public string KIRGKSource { get; set; } = "";

        /// <summary>
        /// IRD KP Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_KPSource")]
        public string KIRGKPSource { get; set; } = "";

        /// <summary>
        /// IRD V Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_VSource")]
        public string KIRGVSource { get; set; } = "";

        /// <summary>
        /// IRD H Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_HSource")]
        public string KIRGHSource { get; set; } = "";

        /// <summary>
        /// IRD U Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_USource")]
        public string KIRGUSource { get; set; } = "";

        /// <summary>
        /// IRD M Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_MSource")]
        public string KIRGMSource { get; set; } = "";

        /// <summary>
        /// IRD UK Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_UKSource")]
        public string KIRGUKSource { get; set; } = "";

        /// <summary>
        /// IRD S Source
        /// </summary>
        [XmlAttribute(AttributeName = "kIRG_SSource")]
        public string KIRGSSource { get; set; } = "";

        /// <summary>
        /// KPS1
        /// </summary>
        [XmlAttribute(AttributeName = "kKPS1")]
        public string KKPS1 { get; set; } = "";

        /// <summary>
        /// Definition
        /// </summary>
        [XmlAttribute(AttributeName = "kDefinition")]
        public string KDefinition { get; set; } = "";

        /// <summary>
        /// Han Yu
        /// </summary>
        [XmlAttribute(AttributeName = "kHanYu")]
        public string KHanYu { get; set; } = "";

        /// <summary>
        /// Mandarin
        /// </summary>
        [XmlAttribute(AttributeName = "kMandarin")]
        public string KMandarin { get; set; } = "";

        /// <summary>
        /// CihaiT
        /// </summary>
        [XmlAttribute(AttributeName = "kCihaiT")]
        public string KCihaiT { get; set; } = "";

        /// <summary>
        /// SBGY
        /// </summary>
        [XmlAttribute(AttributeName = "kSBGY")]
        public string KSBGY { get; set; } = "";

        /// <summary>
        /// Cangjie
        /// </summary>
        [XmlAttribute(AttributeName = "kCangjie")]
        public string KCangjie { get; set; } = "";

        /// <summary>
        /// Kang Xi
        /// </summary>
        [XmlAttribute(AttributeName = "kKangXi")]
        public string KKangXi { get; set; } = "";

        /// <summary>
        /// Hanyu Pinyin
        /// </summary>
        [XmlAttribute(AttributeName = "kHanyuPinyin")]
        public string KHanyuPinyin { get; set; } = "";

        /// <summary>
        /// IRG Hanyu Da Zidian
        /// </summary>
        [XmlAttribute(AttributeName = "kIRGHanyuDaZidian")]
        public double KIRGHanyuDaZidian { get; set; }

        /// <summary>
        /// IRG Kang Xi
        /// </summary>
        [XmlAttribute(AttributeName = "kIRGKangXi")]
        public double KIRGKangXi { get; set; }

        /// <summary>
        /// Morohashi
        /// </summary>
        [XmlAttribute(AttributeName = "kMorohashi")]
        public string KMorohashi { get; set; } = "";

        /// <summary>
        /// Total strokes
        /// </summary>
        [XmlAttribute(AttributeName = "kTotalStrokes")]
        public string KTotalStrokes { get; set; } = "";

        /// <summary>
        /// Japanese
        /// </summary>
        [XmlAttribute(AttributeName = "kJapanese")]
        public string KJapanese { get; set; } = "";

        /// <summary>
        /// Moji Joho
        /// </summary>
        [XmlAttribute(AttributeName = "kMojiJoho")]
        public string KMojiJoho { get; set; } = "";

        /// <summary>
        /// Fanqie
        /// </summary>
        [XmlAttribute(AttributeName = "kFanqie")]
        public string KFanqie { get; set; } = "";

        /// <summary>
        /// Strange
        /// </summary>
        [XmlAttribute(AttributeName = "kStrange")]
        public string KStrange { get; set; } = "";

        /// <summary>
        /// RS Adobe Japan 1.6
        /// </summary>
        [XmlAttribute(AttributeName = "kRSAdobe_Japan1_6")]
        public string KRSAdobeJapan16 { get; set; } = "";

        /// <summary>
        /// Cantonese
        /// </summary>
        [XmlAttribute(AttributeName = "kCantonese")]
        public string KCantonese { get; set; } = "";

        /// <summary>
        /// Phonetic
        /// </summary>
        [XmlAttribute(AttributeName = "kPhonetic")]
        public string KPhonetic { get; set; } = "";
    }

    /// <summary>
    /// Name alias
    /// </summary>
    [XmlRoot(ElementName = "name-alias")]
    public class Namealias
    {
        /// <summary>
        /// Alias name
        /// </summary>
        [XmlAttribute(AttributeName = "alias")]
        public string Alias { get; set; } = "";

        /// <summary>
        /// Alias type
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; } = "";
    }
}
