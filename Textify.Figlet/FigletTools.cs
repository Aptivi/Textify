// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.Collections.Generic;
using Textify.Figlet.Utilities;
using Textify.General;

namespace Textify.Figlet
{
    /// <summary>
    /// Figlet tools
    /// </summary>
    public static class FigletTools
    {
        private readonly static Dictionary<string, string> cachedFiglets = [];

        /// <summary>
        /// Gets the figlet lines
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static string[] GetFigletLines(string Text, FigletFont FigletFont)
        {
            Text = FigletFont.Render(Text);
            return Text.SplitNewLines();
        }

        /// <summary>
        /// Gets the figlet text height
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static int GetFigletHeight(string Text, FigletFont FigletFont) =>
            GetFigletLines(Text, FigletFont).Length;

        /// <summary>
        /// Gets the figlet text width
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static int GetFigletWidth(string Text, FigletFont FigletFont) =>
            GetFigletLines(Text, FigletFont)[0].Length;

        /// <summary>
        /// Gets the figlet fonts
        /// </summary>
        /// <returns>List of supported Figlet fonts</returns>
        public static Dictionary<string, FigletFont> GetFigletFonts()
        {
            Dictionary<string, FigletFont> fonts = [];

            // Populate through all the built-in fonts
            foreach (string fontName in FigletFonts._builtinFonts)
            {
                var font = FigletFonts.TryGetByName(fontName);
                if (font is not null)
                    fonts.Add(fontName, font);
            }
            return fonts;
        }

        /// <summary>
        /// Gets the figlet font from font name
        /// </summary>
        /// <param name="FontName">Font name. Consult <see cref="GetFigletFonts()"/> for more info.</param>
        /// <returns>Figlet font instance of your font, or Small if not found</returns>
        public static FigletFont GetFigletFont(string FontName)
        {
            var figletFonts = GetFigletFonts();
            if (figletFonts.ContainsKey(FontName))
                return figletFonts[FontName];
            else
                return figletFonts["small"];
        }

        /// <summary>
        /// Renders the figlet font
        /// </summary>
        /// <param name="Text">Text to render</param>
        /// <param name="figletFontName">Figlet font name to render. Consult <see cref="GetFigletFonts()"/> for more info.</param>
        /// <param name="Vars">Variables to use when formatting the string</param>
        public static string RenderFiglet(string Text, string figletFontName, params object[] Vars)
        {
            var FigletFont = GetFigletFont(figletFontName);
            return RenderFiglet(Text, FigletFont, Vars);
        }

        /// <summary>
        /// Renders the figlet font
        /// </summary>
        /// <param name="Text">Text to render</param>
        /// <param name="FigletFont">Figlet font instance to render. Consult <see cref="GetFigletFonts()"/> for more info.</param>
        /// <param name="Vars">Variables to use when formatting the string</param>
        public static string RenderFiglet(string Text, FigletFont FigletFont, params object[] Vars)
        {
            // Get the figlet font name.
            string figletFontName = FigletFont.Name;
            if (string.IsNullOrEmpty(figletFontName))
                return "";

            // Now, render the figlet and add to the cache
            string cachedFigletKey = $"[{cachedFiglets.Count} - {figletFontName}] {Text}";
            string cachedFigletKeyToAdd = $"[{cachedFiglets.Count + 1} - {figletFontName}] {Text}";
            if (cachedFiglets.ContainsKey(cachedFigletKey))
                return cachedFiglets[cachedFigletKey];
            else
            {
                // Format string as needed
                if (!(Vars.Length == 0))
                    Text = TextTools.FormatString(Text, Vars);

                // Write the font
                Text = string.Join("\n", GetFigletLines(Text, FigletFont));
                cachedFiglets.Add(cachedFigletKeyToAdd, Text);
                return Text;
            }
        }
    }
}
