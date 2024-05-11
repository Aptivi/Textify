// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Textify.Figlet.Utilities;

namespace Textify.Figlet;

/// <summary>
/// Collection of bundled fonts, ready for use.
/// </summary>
/// <remarks>
/// Fonts are lazily loaded upon property access. Only the fonts you use will be loaded.
/// </remarks>
public static class FigletFonts
{
    private static readonly StringPool _stringPool = new();
    internal static readonly ConcurrentDictionary<string, FigletFont> _fontByName = new(StringComparer.Ordinal);
    internal static readonly string[] _builtinFonts = new[]
    {
        "1row",
        "3-d",
        "3d_diagonal",
        "3x5",
        "4max",
        "5lineoblique",
        "acrobatic",
        "alligator",
        "alligator2",
        "alligator3",
        "alpha",
        "alphabet",
        "amc3line",
        "amc3liv1",
        "amcaaa01",
        "amcneko",
        "amcrazo2",
        "amcrazor",
        "amcslash",
        "amcslder",
        "amcthin",
        "amctubes",
        "amcun1",
        "arrows",
        "ascii_new_roman",
        "avatar",
        "B1FF",
        "banner",
        "banner3",
        "banner3-D",
        "banner4",
        "barbwire",
        "basic",
        "bear",
        "bell",
        "benjamin",
        "big",
        "bigchief",
        "bigfig",
        "binary",
        "block",
        "blocks",
        "bolger",
        "braced",
        "bright",
        "broadway",
        "broadway_kb",
        "bubble",
        "bulbhead",
        "calgphy2",
        "caligraphy",
        "cards",
        "catwalk",
        "chiseled",
        "chunky",
        "coinstak",
        "cola",
        "colossal",
        "computer",
        "contessa",
        "contrast",
        "cosmic",
        "cosmike",
        "crawford",
        "crazy",
        "cricket",
        "cursive",
        "cyberlarge",
        "cybermedium",
        "cybersmall",
        "cygnet",
        "DANC4",
        "dancingfont",
        "decimal",
        "defleppard",
        "diamond",
        "dietcola",
        "digital",
        "doh",
        "doom",
        "dosrebel",
        "dotmatrix",
        "double",
        "doubleshorts",
        "drpepper",
        "dwhistled",
        "eftichess",
        "eftifont",
        "eftipiti",
        "eftirobot",
        "eftitalic",
        "eftiwall",
        "eftiwater",
        "epic",
        "fender",
        "filter",
        "fire_font-k",
        "fire_font-s",
        "flipped",
        "flowerpower",
        "fourtops",
        "fraktur",
        "funface",
        "funfaces",
        "fuzzy",
        "georgi16",
        "Georgia11",
        "ghost",
        "ghoulish",
        "glenyn",
        "goofy",
        "gothic",
        "graceful",
        "gradient",
        "graffiti",
        "greek",
        "heart_left",
        "heart_right",
        "henry3d",
        "hex",
        "hieroglyphs",
        "hollywood",
        "horizontalleft",
        "horizontalright",
        "ICL-1900",
        "impossible",
        "invita",
        "isometric1",
        "isometric2",
        "isometric3",
        "isometric4",
        "italic",
        "ivrit",
        "jacky",
        "jazmine",
        "jerusalem",
        "katakana",
        "kban",
        "keyboard",
        "knob",
        "konto",
        "kontoslant",
        "larry3d",
        "lcd",
        "lean",
        "letters",
        "lildevil",
        "lineblocks",
        "linux",
        "lockergnome",
        "madrid",
        "marquee",
        "maxfour",
        "merlin1",
        "merlin2",
        "mike",
        "mini",
        "mirror",
        "mnemonic",
        "modular",
        "morse",
        "morse2",
        "moscow",
        "mshebrew210",
        "muzzle",
        "nancyj",
        "nancyj-fancy",
        "nancyj-improved",
        "nancyj-underlined",
        "nipples",
        "nscript",
        "ntgreek",
        "nvscript",
        "o8",
        "octal",
        "ogre",
        "oldbanner",
        "os2",
        "pawp",
        "peaks",
        "peaksslant",
        "pebbles",
        "pepper",
        "poison",
        "puffy",
        "puzzle",
        "pyramid",
        "rammstein",
        "rectangles",
        "red_phoenix",
        "relief",
        "relief2",
        "rev",
        "reverse",
        "roman",
        "rot13",
        "rotated",
        "rounded",
        "rowancap",
        "rozzo",
        "runic",
        "runyc",
        "santaclara",
        "sblood",
        "script",
        "slscript",
        "serifcap",
        "shadow",
        "shimrod",
        "short",
        "slant",
        "slide",
        "small",
        "smallcaps",
        "smisome1",
        "smkeyboard",
        "smpoison",
        "smscript",
        "smshadow",
        "smslant",
        "smtengwar",
        "soft",
        "speed",
        "spliff",
        "s-relief",
        "stacey",
        "stampate",
        "stampatello",
        "standard",
        "starstrips",
        "starwars",
        "stellar",
        "stforek",
        "stop",
        "straight",
        "sub-zero",
        "swampland",
        "swan",
        "sweet",
        "tanja",
        "tengwar",
        "term",
        "test1",
        "thick",
        "thin",
        "threepoint",
        "ticks",
        "ticksslant",
        "tiles",
        "tinker-toy",
        "tombstone",
        "train",
        "trek",
        "tsalagi",
        "tubular",
        "twisted",
        "twopoint",
        "univers",
        "usaflag",
        "varsity",
        "wavy",
        "weird",
        "wetletter",
        "whimsy",
        "wow",
    };

    /// <summary>
    /// Attempts to load the font with specified name.
    /// </summary>
    /// <param name="name">the name of the font. Case-sensitive.</param>
    /// <returns>The font if found, otherwise, throws.</returns>
    public static FigletFont GetByName(string name)
    {
        return _fontByName.GetOrAdd(name, FontFactory);

        static FigletFont FontFactory(string name)
        {
            if (!_builtinFonts.Contains(name))
                throw new FigletException("Built-in font not implemented. Try using the FigletFontParser class to parse custom Figlet fonts.");
            var font = ParseEmbeddedFont(name);
            return font;
        }
    }

    /// <summary>
    /// Attempts to load the font with specified name.
    /// </summary>
    /// <param name="name">the name of the font. Case-sensitive.</param>
    /// <returns>The font if found, otherwise <see langword="null"/>.</returns>
    public static FigletFont TryGetByName(string name)
    {
        // Check to see if we have the cached version
        if (_fontByName.TryGetValue(name, out var font))
            return font;

        // Try to parse the font, and then add it if found
        font = ParseEmbeddedFont(name);
        if (font is not null)
            _fontByName.TryAdd(name, font);

        // Return the parsed font.
        return font;
    }

    private static FigletFont ParseEmbeddedFont(string name)
    {
        using var stream = typeof(FigletFonts).GetTypeInfo().Assembly.GetManifestResourceStream($"Textify.Figlet.Fonts.{name}.flf");
        if (stream is null)
            return null;
        return FigletFontParser.Parse(stream, name, _stringPool);
    }
}
