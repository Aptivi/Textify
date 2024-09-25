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

using System.IO;
using System.Linq;

namespace Textify.Data.Tools
{
    internal static class DataStreamTools
    {
        private static readonly string[] resourceNames = typeof(DataStreamTools).Assembly.GetManifestResourceNames();

        internal static bool HasResource(string type, string extension = "zip")
        {
            // Check the resource
            var asm = typeof(DataStreamTools).Assembly;
            string resourceName = $"{asm.GetName().Name}.{type}.{extension}";
            return resourceNames.Contains(resourceName);
        }

        internal static Stream GetStreamFrom(string type, string extension = "zip")
        {
            // Check the resource
            var asm = typeof(DataStreamTools).Assembly;
            string resourceName = $"{asm.GetName().Name}.{type}.{extension}";
            if (!HasResource(type, extension))
                throw new InvalidDataException($"Resource type {type} with extension {extension} not found [{resourceName}]");

            // Return the stream
            return asm.GetManifestResourceStream(resourceName);
        }

        internal static byte[] GetDataFrom(string type, string extension = "zip")
        {
            // Now, use the stream to read the resource and return it
            using var stream = GetStreamFrom(type, extension);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            return bytes;
        }
    }
}
