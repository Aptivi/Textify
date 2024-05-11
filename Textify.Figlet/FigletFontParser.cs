// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Textify.Figlet.Utilities;

namespace Textify.Figlet;

/// <summary>
/// Parses FIGlet font files.
/// </summary>
public static class FigletFontParser
{
    private const int SM_SMUSH = 128;
    private const int SM_KERN = 64;
    private const int SM_FULLWIDTH = 0;

    private static readonly Regex _firstLinePattern = new(
        @"^flf2                         # signature
          a                             # always 'a'
          (?<hardblank>.)               # any single character
          \s(?<height>\d+)              # the number of rows, shared across all characters
          \s(?<baseline>\d+)            # the number of rows from the top of the char to the baseline (excludes descenders)
          \s(\d+)                       # the maximum width of character data in the file, including endmarks
          \s(?<layoutold>-?\d+)         # layout settings (old format)
          \s(?<commentlinecount>\d+)    # number of comment lines after first line, before first character
          (\s(?<direction>\d+))?        # print direction (0 is left-to-right, 1 is right-to-left)
          (\s(?<layoutnew>\d+))?        # layout settings (new format)
          (\s(\d+))?                    # number of code-tagged (non-required) characters in the font, equal to total number of characters minus 102
          (\s|$)",
        RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

    /// <summary>
    /// Parses a FIGlet font description stream, and returns a usable <see cref="FigletFont"/>.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="pool">An optional string pool for merging identical string references. If null, creates a new pool</param>
    /// <param name="name">Figlet font name.</param>
    /// <param name="encoding">Encoding to use while parsing font file</param>
    /// <returns>The font described by the stream.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="FigletException">The stream contained an error and could not be parsed.</exception>
    public static FigletFont Parse(Stream stream, string name = "unknown", StringPool pool = null, Encoding encoding = null)
    {
        // Validate before continuing
        var (reader, match) = ValidateFontStream(stream, encoding);

        // Get some of the values
        var hardBlank = match.Groups["hardblank"].Value[0];
        var height = int.Parse(match.Groups["height"].Value);
        var baseline = int.Parse(match.Groups["baseline"].Value);
        var layoutOld = int.Parse(match.Groups["layoutold"].Value);
        var commentLineCount = int.Parse(match.Groups["commentlinecount"].Value);
        var layoutNewMatch = match.Groups["layoutnew"];
        var layoutNew = layoutNewMatch.Success
            ? int.Parse(layoutNewMatch.Value)
            : UpgradeLayout();

        int UpgradeLayout()
        {
            if (layoutOld == 0)
                return SM_KERN;
            if (layoutOld < 0)
                return SM_FULLWIDTH;
            return layoutOld & 0x1F | SM_SMUSH;
        }

        // Determine the direction
        var dirMatch = match.Groups["direction"];
        var direction = dirMatch.Success
            ? (FigletTextDirection)int.Parse(dirMatch.Value)
            : FigletTextDirection.LeftToRight;

        // Get all the comment lines
        var comments = new List<string>();
        for (var i = 0; i < commentLineCount; i++)
            comments.Add(reader.ReadLine());

        // Create a string pool if not specified
        pool ??= new StringPool();

        /*
        Characters 0-31 are control characters.

        Characters 32-126 appear in order:

        32 (blank/space) 64 @             96  `
        33 !             65 A             97  a
        34 "             66 B             98  b
        35 #             67 C             99  c
        36 $             68 D             100 d
        37 %             69 E             101 e
        38 &             70 F             102 f
        39 '             71 G             103 g
        40 (             72 H             104 h
        41 )             73 I             105 i
        42 *             74 J             106 j
        43 +             75 K             107 k
        44 ,             76 L             108 l
        45 -             77 M             109 m
        46 .             78 N             110 n
        47 /             79 O             111 o
        48 0             80 P             112 p
        49 1             81 Q             113 q
        50 2             82 R             114 r
        51 3             83 S             115 s
        52 4             84 T             116 t
        53 5             85 U             117 u
        54 6             86 V             118 v
        55 7             87 W             119 w
        56 8             88 X             120 x
        57 9             89 Y             121 y
        58 :             90 Z             122 z
        59 ;             91 [             123 {
        60 <             92 \             124 |
        61 =             93 ]             125 }
        62 >             94 ^             126 ~
        63 ?             95 _

        Then codes:

        196 Ä
        214 Ö
        220 Ü
        228 ä
        246 ö
        252 ü
        223 ß

        If some of these characters are not desired, empty characters may be used, having endmarks flush with the margin.

        After the required characters come "code tagged characters" in range -2147483648 to +2147483647, excluding -1. The assumed mapping is to ASCII/Latin-1/Unicode.

        A zero character is treated as the character to be used whenever a required character is missing. If no zero character is available, nothing will be printed.
        */

        FigletCharacter ReadCharacter()
        {
            var lines = new Line[height];

            static byte CountSolSpaces(string s)
            {
                // Start of line spaces
                byte count = 0;
                for (; count < s.Length && s[count] == ' '; count++)
                { }
                return count;
            }

            static byte CountEolSpaces(string s)
            {
                // End of line spaces
                byte count = 0;
                for (var i = s.Length - 1; i > 0 && s[i] == ' '; i--, count++)
                { }
                return count;
            }

            for (var i = 0; i < height; i++)
            {
                // Get a line
                var line = reader.ReadLine() ??
                    throw new FigletException("Unexpected EOF in Font file.");

                // Trim the end mark and make a new line instance holding info about the line
                var endmark = line[line.Length - 1];
                line = line.TrimEnd(endmark);
                lines[i] = new Line(pool.Pool(line), CountSolSpaces(line), CountEolSpaces(line));
            }

            return new FigletCharacter(lines);
        }

        var requiredCharacters = new FigletCharacter[256];

        for (var i = 32; i < 127; i++)
            requiredCharacters[i] = ReadCharacter();

        requiredCharacters[196] = ReadCharacter();
        requiredCharacters[214] = ReadCharacter();
        requiredCharacters[220] = ReadCharacter();
        requiredCharacters[228] = ReadCharacter();
        requiredCharacters[246] = ReadCharacter();
        requiredCharacters[252] = ReadCharacter();
        requiredCharacters[223] = ReadCharacter();

        // support code-tagged characters
        var sparseCharacters = new Dictionary<int, FigletCharacter>();

        while (true)
        {
            // Get a line
            var line = reader.ReadLine();

            // We reached the end of stream
            if (line == null)
                break;

            // Ignore empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Try to parse the line
            if (!ParseUtil.TryParse(line, out var code))
                throw new FigletException($"Unsupported code-tagged character code string \"{line}\".");

            // Read the required and sparse characters
            if (code >= 0 && code < 256)
                requiredCharacters[code] = ReadCharacter();
            else
                sparseCharacters[code] = ReadCharacter();
        }

        // Return a new instance of the font
        return new FigletFont(requiredCharacters, sparseCharacters, hardBlank, height, baseline, direction, layoutNew, name, comments.ToArray());
    }

    /// <summary>
    /// Extracts the comments from the font stream
    /// </summary>
    /// <param name="stream">Stream that contains FLF information</param>
    /// <param name="encoding">Encoding to use while parsing font file</param>
    /// <returns>Read only list of comments</returns>
    public static IReadOnlyList<string> ExtractComments(Stream stream, Encoding encoding = null)
    {
        // Validate before continuing
        var (reader, match) = ValidateFontStream(stream, encoding);

        // Get some of the values
        var commentLineCount = int.Parse(match.Groups["commentlinecount"].Value);

        // Get all comment lines
        List<string> comments = [];
        for (int i = 0; i < commentLineCount; i++)
        {
            string commentLine = reader.ReadLine();
            comments.Add(commentLine);
        }
        return comments;
    }

    /// <summary>
    /// Checks to see if the provided stream is a valid font stream
    /// </summary>
    /// <param name="stream">Stream that contains FLF information</param>
    /// <param name="encoding">Encoding to use while parsing font file</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FigletException"></exception>
    public static (StreamReader reader, Match match) ValidateFontStream(Stream stream, Encoding encoding = null)
    {
        // Check the stream before opening it
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        // Open the stream reader with appropriate encoding
        StreamReader reader;
        if (encoding == null)
            reader = new StreamReader(stream);
        else
            reader = new StreamReader(stream, encoding);
        stream.Seek(0, SeekOrigin.Begin);

        // Read the first line and parse it
        var firstLine = reader.ReadLine() ??
            throw new FigletException("Font file is empty.");
        var match = _firstLinePattern.Match(firstLine);
        if (!match.Success)
            throw new FigletException("Font file has invalid first line.");
        return (reader, match);
    }

    /// <summary>
    /// Tries to validate the font stream
    /// </summary>
    /// <param name="stream">Stream that contains FLF information</param>
    /// <param name="encoding">Encoding to use while parsing font file</param>
    /// <returns>True with an instance of the reader and the match if found; otherwise, false with two null values.</returns>
    public static (bool result, StreamReader reader, Match match) TryValidateFontStream(Stream stream, Encoding encoding = null)
    {
        try
        {
            var (reader, match) = ValidateFontStream(stream, encoding);
            return (true, reader, match);
        }
        catch
        {
            return (false, null, null);
        }
    }
}
