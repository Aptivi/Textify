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

using System.Linq;
using Textify.Tools;

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Kaomoji management class
    /// </summary>
    public static partial class KaomojiManager
    {
        /// <summary>
        /// Gets all kaomoji subcategories
        /// </summary>
        /// <param name="category">Kaomoji category</param>
        /// <returns>An array of kaomoji subcategories</returns>
        public static KaomojiSubcategory[] GetKaomojiSubcategories(KaomojiCategory category)
        {
            if (!kaomojis.TryGetValue(category, out var subcategory))
                throw new TextifyException("Category doesn't exist");
            return [.. subcategory.Keys];
        }

        /// <summary>
        /// Gets all kaomoji sequences
        /// </summary>
        /// <param name="category">Kaomoji category</param>
        /// <param name="subcategory">Kaomoji subcategory</param>
        /// <returns>An array of kaomoji sequences</returns>
        public static string[] GetKaomojis(KaomojiCategory category, KaomojiSubcategory subcategory)
        {
            var subcategories = GetKaomojiSubcategories(category);
            if (!subcategories.Contains(subcategory))
                throw new TextifyException($"There is no such subcategory for {category}");
            return kaomojis[category][subcategory];
        }

        /// <summary>
        /// Gets a kaomoji sequence
        /// </summary>
        /// <param name="category">Kaomoji category</param>
        /// <param name="subcategory">Kaomoji subcategory</param>
        /// <param name="sequenceIdx">Sequence index</param>
        /// <returns>Kaomoji sequence</returns>
        public static string GetKaomoji(KaomojiCategory category, KaomojiSubcategory subcategory, int sequenceIdx)
        {
            var sequences = GetKaomojis(category, subcategory);
            return sequences[sequenceIdx];
        }
    }
}
