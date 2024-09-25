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

using System.Collections.Generic;
using Textify.Tools;

namespace Textify.Data.Tools
{
    internal static class DataTools
    {
        internal static Dictionary<string, byte[]> dataStreams = [];

        internal static byte[] GetDataFrom(string dataName)
        {
            // Check the data if it exists
            if (!dataStreams.TryGetValue(dataName, out byte[] data))
                throw new TextifyException($"Data {dataName} is not initialized yet. Ensure that you've called Initialize() from DataInitializer and try again.");

            // Now, get the data
            return data;
        }
    }
}
