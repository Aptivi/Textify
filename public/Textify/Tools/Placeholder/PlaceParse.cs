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
using System.Collections.Generic;
using System.Linq;
using Textify.General;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Textify.Tools.Placeholder
{
    /// <summary>
    /// Placeholder parsing module
    /// </summary>
    public static class PlaceParse
    {
        private readonly static List<PlaceInfo> customPlaceholders = [];
        private readonly static List<PlaceInfo> placeholders =
        [
            new PlaceInfo("system", (_) => Environment.OSVersion.ToString()),
            new PlaceInfo("newline", (_) => CharManager.NewLine),
        ];

        /// <summary>
        /// Probes the placeholders found in string
        /// </summary>
        /// <param name="text">Specified string</param>
        /// <param name="ThrowIfFailure">Throw if the placeholder parsing fails</param>
        /// <returns>A string that has the parsed placeholders</returns>
        public static string ProbePlaces(string text, bool ThrowIfFailure = false)
        {
            try
            {
                // Parse the text for the following placeholders:
                var placeMatches = RegexTools.Matches(text, /* lang=regex */ @"\<.*\>");

                // Get all the placeholder matches and replace them as appropriate
                foreach (Match placeMatch in placeMatches)
                {
                    // Get the argument (if any)
                    string place = placeMatch.Value;
                    string arg = "";
                    if (place.Contains(':'))
                        arg = place.Substring(place.IndexOf(':') + 1, place.Length - 1);
                    string placeNoArg = place.Replace($":{arg}>", ">");

                    // Fetch a placeholder
                    try
                    {
                        // Execute the action
                        var action = GetPlaceholderAction(placeNoArg);
                        string result = action(arg);
                        text = text.Replace(place, result);
                    }
                    catch (Exception ex)
                    {
                        // Leave the text and the placeholder alone in this case.
                        Debug.WriteLine("Leaving placeholder alone because of failure: {0}", args: [ex.Message]);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ThrowIfFailure)
                    throw new TextifyException("Error trying to parse placeholders. {0}".FormatString(ex.Message));
            }
            return text;
        }

        /// <summary>
        /// Registers a custom placeholder
        /// </summary>
        /// <param name="placeholder">Custom placeholder to register (may be without the &lt; and the &gt; marks)</param>
        /// <param name="placeholderAction">Action associated with the placeholder</param>
        public static void RegisterCustomPlaceholder(string placeholder, Func<string, string> placeholderAction)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(placeholder))
                throw new TextifyException("Placeholder may not be null");
            if (placeholderAction is null)
                throw new TextifyException("Placeholder action may not be null");

            // Try to register
            if (!IsPlaceholderRegistered($"<{placeholder}>"))
            {
                var place = new PlaceInfo(placeholder, placeholderAction);
                customPlaceholders.Add(place);
            }
        }

        /// <summary>
        /// Unregisters a custom placeholder
        /// </summary>
        /// <param name="placeholder">Custom placeholder to unregister (should be with the &lt; and the &gt; marks)</param>
        public static void UnregisterCustomPlaceholder(string placeholder)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(placeholder))
                throw new TextifyException("Placeholder may not be null");
            if (!IsPlaceholderRegistered(placeholder))
                throw new TextifyException("Placeholder is not registered");
            if (!placeholder.StartsWith("<") || !placeholder.EndsWith(">"))
                throw new TextifyException("Placeholder must satisfy this format" + ": <place>");

            // Try to unregister
            var place = GetPlaceholder(placeholder);
            customPlaceholders.Remove(place);
        }

        /// <summary>
        /// Checks to see whether the placeholder is built in
        /// </summary>
        /// <param name="placeholder">Placeholder to query (should be with the &lt; and the &gt; marks)</param>
        /// <returns>True if the placeholder is in the list of built-in placeholders</returns>
        public static bool IsPlaceholderBuiltin(string placeholder)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(placeholder))
                throw new TextifyException("Placeholder may not be null");
            if (!placeholder.StartsWith("<") || !placeholder.EndsWith(">"))
                throw new TextifyException("Placeholder must satisfy this format" + ": <place>");

            // Check to see if we have this placeholder
            string placeNoArg = StripPlaceholderArgs(placeholder);
            return placeholders.Any((pi) => $"<{pi.Placeholder}>" == placeNoArg);
        }

        /// <summary>
        /// Checks to see whether the placeholder is registered
        /// </summary>
        /// <param name="placeholder">Placeholder to query (should be with the &lt; and the &gt; marks)</param>
        /// <returns>True if the placeholder is in the list of placeholders</returns>
        public static bool IsPlaceholderRegistered(string placeholder)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(placeholder))
                throw new TextifyException("Placeholder may not be null");
            if (!placeholder.StartsWith("<") || !placeholder.EndsWith(">"))
                throw new TextifyException("Placeholder must satisfy this format" + ": <place>");

            // Check to see if we have this placeholder
            string placeNoArg = StripPlaceholderArgs(placeholder);
            return
                IsPlaceholderBuiltin(placeholder) ||
                customPlaceholders.Any((pi) => $"<{pi.Placeholder}>" == placeNoArg);
        }

        /// <summary>
        /// Gets a placeholder from the placeholder name
        /// </summary>
        /// <param name="placeholder">Placeholder to query (should be with the &lt; and the &gt; marks)</param>
        public static PlaceInfo GetPlaceholder(string placeholder)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(placeholder))
                throw new TextifyException("Placeholder may not be null");
            if (!IsPlaceholderRegistered(placeholder))
                throw new TextifyException("Placeholder is not registered");
            if (!placeholder.StartsWith("<") || !placeholder.EndsWith(">"))
                throw new TextifyException("Placeholder must satisfy this format" + ": <place>");

            // Try to get a placeholder
            string placeNoArg = StripPlaceholderArgs(placeholder);
            return
                IsPlaceholderBuiltin(placeholder) ?
                placeholders.First((pi) => $"<{pi.Placeholder}>" == placeNoArg) :
                customPlaceholders.First((pi) => $"<{pi.Placeholder}>" == placeNoArg);
        }

        /// <summary>
        /// Gets a placeholder action from the placeholder name
        /// </summary>
        /// <param name="placeholder">Placeholder to query (should be with the &lt; and the &gt; marks)</param>
        public static Func<string, string> GetPlaceholderAction(string placeholder)
        {
            // Sanity checks
            if (string.IsNullOrEmpty(placeholder))
                throw new TextifyException("Placeholder may not be null");
            if (!IsPlaceholderRegistered(placeholder))
                throw new TextifyException("Placeholder is not registered");
            if (!placeholder.StartsWith("<") || !placeholder.EndsWith(">"))
                throw new TextifyException("Placeholder must satisfy this format" + ": <place>");

            // Try to get a placeholder action
            string placeNoArg = StripPlaceholderArgs(placeholder);
            return GetPlaceholder(placeNoArg).PlaceholderAction;
        }

        /// <summary>
        /// Strips the placeholder with arguments
        /// </summary>
        /// <param name="placeholder">Placeholder with arguments (should be with the &lt; and the &gt; marks)</param>
        /// <returns>Stripped placeholder</returns>
        public static string StripPlaceholderArgs(string placeholder)
        {
            string place = placeholder;
            string arg = "";
            if (place.Contains(':'))
                arg = place.Substring(place.IndexOf(':') + 1, place.Length - 1);
            string placeNoArg = place.Replace($":{arg}>", ">");
            return placeNoArg;
        }
    }
}
