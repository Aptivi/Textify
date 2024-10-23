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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textify.CharArrayGen
{
    [Generator]
    public class ArrayGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            string header =
                $$"""
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
                
                // <auto-generated/>

                namespace Textify.General
                {
                    public static partial class CharManager
                    {
                
                """;
            string footer =
                $$"""
                    }
                }
                """;
            var builder = new StringBuilder(header);

            // Populate the ultimate array for all characters
            builder.Append(PopulateArray(CharArrayTools.GetAllChars(), "unicodeChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllAsciiChars(), "unicodeAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLettersAndNumbers(), "letterOrDigitChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLettersAndNumbers(false), "letterOrDigitAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLetters(), "letterChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLetters(false), "letterAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllNumbers(), "numberChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllNumbers(false), "numberAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllDigitChars(), "digitChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllDigitChars(false), "digitAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllControlChars(), "controlChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllRealControlChars(), "realControlChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllSurrogateChars(), "surrogateChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllHighSurrogateChars(), "highSurrogateChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLowSurrogateChars(), "lowSurrogateChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLowerChars(), "lowerChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllLowerChars(false), "lowerAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllUpperChars(), "upperChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllUpperChars(false), "upperAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllPunctuationChars(), "punctuationChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllPunctuationChars(false), "punctuationAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllSeparatorChars(), "separatorChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllSeparatorChars(false), "separatorAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllSymbolChars(), "symbolChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllSymbolChars(false), "symbolAsciiChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllWhitespaceChars(), "whitespaceChars"));
            builder.Append(PopulateArray(CharArrayTools.GetAllWhitespaceChars(false), "whitespaceAsciiChars"));

            // End the file
            builder.AppendLine(footer);
            context.RegisterPostInitializationOutput(ctx =>
            {
                ctx.AddSource("CharManager.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
            });
        }

        private static string PopulateArray(char[] array, string fieldName)
        {
            string arrayHeader =
                $$"""
                            
                """;
            string arrayFooter =
                $$"""
                        ];

                
                """;
            string asciiCharsHeader =
                $$"""
                        private static readonly char[] {{fieldName}} =
                        [
                
                """;
            var builder = new StringBuilder(asciiCharsHeader);

            // Populate the characters
            bool isLast = false;
            for (int i = 0; i < array.Length; i++)
            {
                int factor = i % 8;
                isLast = factor == 7;
                char c = array[i];

                // Add the header if required
                if (factor == 0)
                    builder.Append(arrayHeader);

                // Add the character literal
                builder.Append($"'\\u{(int)c:X4}', ");

                // Add the new line if required
                if (isLast)
                    builder.AppendLine("");
            }
            if (!isLast)
                builder.AppendLine("");
            builder.Append(arrayFooter);
            return builder.ToString();
        }
    }
}