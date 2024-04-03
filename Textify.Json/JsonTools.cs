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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Textify.Json
{
    /// <summary>
    /// JSON tools
    /// </summary>
    public static class JsonTools
    {
        /// <summary>
        /// Beautifies the JSON text.
        /// </summary>
        /// <param name="JsonText">Contents of a minified JSON.</param>
        /// <returns>Beautified JSON</returns>
        public static string BeautifyJsonText(string JsonText)
        {
            // Make an instance of JToken with this text
            var JsonToken = JToken.Parse(JsonText);

            // Beautify JSON
            string BeautifiedJson = JsonConvert.SerializeObject(JsonToken, Formatting.Indented);
            return BeautifiedJson;
        }

        /// <summary>
        /// Minifies the JSON text.
        /// </summary>
        /// <param name="JsonText">Contents of a beautified JSON.</param>
        /// <returns>Minified JSON</returns>
        public static string MinifyJsonText(string JsonText)
        {
            // Make an instance of JToken with this text
            var JsonToken = JToken.Parse(JsonText);

            // Minify JSON
            string MinifiedJson = JsonConvert.SerializeObject(JsonToken);
            return MinifiedJson;
        }
    }
}
