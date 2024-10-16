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

namespace Textify.Data.Unicode
{
    /// <summary>
    /// Emoji information
    /// </summary>
    public class Emoji
    {
        private readonly string name = "";
        private readonly string sequence = "";
        private readonly EmojiEnum emojiEnum = (EmojiEnum)(-1);
        private readonly EmojiStatus status = EmojiStatus.Component;

        /// <summary>
        /// Emoji name
        /// </summary>
        public string Name =>
            name;

        /// <summary>
        /// A sequence of characters that represent an emoji
        /// </summary>
        public string Sequence =>
            sequence;

        /// <summary>
        /// Emoji name enumeration
        /// </summary>
        public EmojiEnum Enum =>
            emojiEnum;

        /// <summary>
        /// Emoji status
        /// </summary>
        public EmojiStatus Status =>
            status;

        internal Emoji(string name, string sequence, EmojiEnum emojiEnum, EmojiStatus status)
        {
            this.name = name;
            this.sequence = sequence;
            this.emojiEnum = emojiEnum;
            this.status = status;
        }
    }
}
