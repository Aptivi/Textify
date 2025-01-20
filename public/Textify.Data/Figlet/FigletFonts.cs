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
using System.Collections.Concurrent;
using System.IO;
using Textify.Data.Figlet.Utilities;
using Textify.Data.Figlet.Utilities.Lines;
using Textify.Data.Tools;

namespace Textify.Data.Figlet
{
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
                var font = ParseEmbeddedFont(name) ??
                    throw new FigletException("Built-in font not implemented. Try using the FigletFontParser class to parse custom Figlet fonts.");
                return font;
            }
        }

        /// <summary>
        /// Attempts to load the font with specified name.
        /// </summary>
        /// <param name="name">the name of the font. Case-sensitive.</param>
        /// <returns>The font if found, otherwise <see langword="null"/>.</returns>
        public static FigletFont? TryGetByName(string name)
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

        private static FigletFont? ParseEmbeddedFont(string name)
        {
            Stream resultantStream;
            if (DataStreamTools.HasResource(name, "flf"))
                resultantStream = DataStreamTools.GetStreamFrom(name, "flf");
            else if (DataStreamTools.HasResource(name, "FLF"))
                resultantStream = DataStreamTools.GetStreamFrom(name, "FLF");
            else
                return null;
            return FigletFontParser.Parse(resultantStream, name, _stringPool);
        }
    }
}
