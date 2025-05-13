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
using System.Runtime.InteropServices;
using System.Text;
using Textify.Data.Unicode.Icu;
using Textify.Tools;

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Unicode tools
    /// </summary>
    public static class UnicodeTools
    {
        /// <summary>
        /// Sets the libicu path
        /// </summary>
        public static string IcuLibPath { get; set; } = "";
        
        /// <summary>
        /// Sets the libicudata path
        /// </summary>
        public static string IcuDataLibPath { get; set; } = "";

        /// <summary>
        /// Reverses the RTL character order in a text
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <returns>A string that is obtained from a visual map</returns>
        /// <exception cref="TextifyException"></exception>
        public static string ReverseRtl(string text)
        {
            // Check to see if libicuuc is initialized
            if (!NativeLoader.loaded)
            {
                if (string.IsNullOrEmpty(IcuLibPath) || string.IsNullOrEmpty(IcuDataLibPath))
                    NativeLoader.InitializeLibrary();
                else
                    NativeLoader.InitializeLibrary(IcuLibPath, IcuDataLibPath);
            }

            // Now, open the ubidi instance
            unsafe
            {
                var bidiDelegate = NativeLoader.GetDelegate<NativeInteropBidi.ubidi_open>(NativeLoader.libManagerIcu, nameof(NativeInteropBidi.ubidi_open));
                var bidi = bidiDelegate.Invoke();

                // The instance is now open, so we need to pin the bytes in which we're going to pass to the
                // ubidi_getVisualMap() function.
                try
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(text);
                    var bidiHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

                    // We now have the handle. Pass it to ubidi_getVisualMap().
                    try
                    {
                        // First, we need to pass the text
                        var bidiSetParaDelegate = NativeLoader.GetDelegate<NativeInteropBidi.ubidi_setPara>(NativeLoader.libManagerIcu, nameof(NativeInteropBidi.ubidi_setPara));
                        bidiSetParaDelegate.Invoke(bidi, bidiHandle.AddrOfPinnedObject(), text.Length, 0, IntPtr.Zero, out int error);
                        if (error != 0)
                            throw new TextifyException("Failed to process request to pass the text");

                        // Then, get the visual map that describes indexes
                        int[] visualMap = new int[text.Length];
                        var bidiGetVisualMap = NativeLoader.GetDelegate<NativeInteropBidi.ubidi_getVisualMap>(NativeLoader.libManagerIcu, nameof(NativeInteropBidi.ubidi_getVisualMap));
                        bidiGetVisualMap.Invoke(bidi, visualMap, out error);
                        if (error != 0)
                            throw new TextifyException("Failed to get visual map");

                        // Finally, convert the map to a usable text
                        char[] result = new char[text.Length];
                        for (int i = 0; i < text.Length; i++)
                            result[i] = text[visualMap[i]];

                        return new(result);
                    }
                    finally
                    {
                        bidiHandle.Free();
                    }
                }
                finally
                {
                    var bidiCloseDelegate = NativeLoader.GetDelegate<NativeInteropBidi.ubidi_close>(NativeLoader.libManagerIcu, nameof(NativeInteropBidi.ubidi_close));
                    bidiCloseDelegate.Invoke(bidi);
                }
            }
        }
    }
}
