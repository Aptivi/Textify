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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace Textify.Tools
{
    /// <summary>
    /// JSON tools
    /// </summary>
    public static class JsonTools
    {
        /// <summary>
        /// Beautifies the JSON text contained in the file.
        /// </summary>
        /// <param name="JsonFile">Path to JSON file.</param>
        /// <returns>Beautified JSON</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string BeautifyJson(string JsonFile)
        {
            // Try to beautify JSON
            string JsonFileContents = File.ReadAllText(JsonFile);
            return BeautifyJsonText(JsonFileContents);
        }

        /// <summary>
        /// Minifies the JSON text contained in the file.
        /// </summary>
        /// <param name="JsonFile">Path to JSON file.</param>
        /// <returns>Minified JSON</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string MinifyJson(string JsonFile)
        {
            // Try to minify JSON
            string JsonFileContents = File.ReadAllText(JsonFile);
            return MinifyJsonText(JsonFileContents);
        }

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

        /// <summary>
        /// Finds the JSON object differences between the two JSON object tokens
        /// </summary>
        /// <param name="sourceObj">Source object token</param>
        /// <param name="targetObj">Target object token</param>
        /// <returns>A JSON object containing differences for objects</returns>
        public static JObject FindDifferences(JToken sourceObj, JToken targetObj)
        {
            var diff = new JObject();
            if (!JToken.DeepEquals(targetObj, sourceObj))
            {
                // The objects are not equal.
                switch (targetObj.Type)
                {
                    case JTokenType.Object:
                        // Added keys
                        var addedKeys = ((JObject)targetObj).Properties().Select(c => c.Name).Except(((JObject)sourceObj).Properties().Select(c => c.Name));
                        foreach (var k in addedKeys)
                        {
                            diff[$"+{k}"] = new JObject
                            {
                                ["+"] = targetObj[k]?.Path
                            };
                        }

                        // Removed keys
                        var removedKeys = ((JObject)sourceObj).Properties().Select(c => c.Name).Except(((JObject)targetObj).Properties().Select(c => c.Name));
                        foreach (var k in removedKeys)
                        {
                            diff[$"-{k}"] = new JObject
                            {
                                ["-"] = sourceObj[k]?.Path
                            };
                        }

                        // Changed keys
                        var changedKeys = ((JObject)targetObj).Properties().Where(c => !JToken.DeepEquals(c.Value, sourceObj[c.Name])).Select(c => c.Name);
                        foreach (var k in changedKeys)
                        {
                            diff[$"*{k}"] = new JObject
                            {
                                ["*"] = new JObject
                                {
                                    ["source"] = sourceObj[k],
                                    ["target"] = targetObj[k],
                                }
                            };
                        }
                        break;
                    case JTokenType.Array:
                        // Additions and subtractions
                        diff["+"] = new JArray(((JArray)targetObj).Except(sourceObj));
                        diff["-"] = new JArray(((JArray)sourceObj).Except(targetObj));
                        break;
                    default:
                        diff["+"] = targetObj;
                        diff["-"] = sourceObj;
                        break;
                }
            }
            return diff;
        }
    }
}
