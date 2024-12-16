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

using System;
using System.Collections.Generic;

namespace Textify.Data.Figlet.Utilities
{
    /// <summary>
    /// An object pool for merging references to identical strings.
    /// </summary>
    /// <remarks>
    /// Unlike string interning the pool may be released and garbage collected.
    /// </remarks>
    public sealed class StringPool
    {
        private readonly Dictionary<string, string> _pool = new(StringComparer.Ordinal);

        /// <summary>
        /// Returns a reference to a string equal to <paramref name="s"/> from the pool.
        /// If no such string exists within the pool, it is added, and <paramref name="s"/> is returned.
        /// </summary>
        /// <param name="s">The string to pool.</param>
        /// <returns>A reference to the pooled string.</returns>
        public string Pool(string s)
        {
            lock (_pool)
            {
                if (_pool.TryGetValue(s, out var pooled))
                    return pooled;

                _pool[s] = s;
                return s;
            }
        }
    }
}
