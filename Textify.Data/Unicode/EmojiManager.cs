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
using System.Linq;
using Textify.General;

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Emoji management tools
    /// </summary>
    public static partial class EmojiManager
    {
        /// <summary>
        /// Gets a list of emoticons
        /// </summary>
        public static Emoji[] GetEmojis() =>
            [.. emojis.Values];

        /// <summary>
        /// Gets an emoji instance from the name
        /// </summary>
        /// <param name="name">Name of emoji (case insensitive)</param>
        /// <returns>An emoji instance</returns>
        public static Emoji[] GetEmojisFromName(string name) =>
            GetEmojis().Where((emoji) => emoji.Name.EqualsNoCase(name)).ToArray();

        /// <summary>
        /// Gets an emoji instance from the emoji
        /// </summary>
        /// <param name="sequence">Emoji representation</param>
        /// <returns>An emoji instance</returns>
        public static Emoji GetEmojiFromEmoji(string sequence) =>
            GetEmojis().First((emoji) => emoji.Sequence == sequence);

        /// <summary>
        /// Gets an emoji instance from the name
        /// </summary>
        /// <param name="name">Name of emoji (enumeration of <see cref="EmojiEnum"/>)</param>
        /// <returns>An emoji instance</returns>
        public static Emoji GetEmojiFromEnum(EmojiEnum name)
        {
            if (!emojis.TryGetValue(name, out Emoji emoji))
                throw new ArgumentException($"Can't find emoji from enum [{(int)name}, {name}]");
            return emoji;
        }
    }
}
