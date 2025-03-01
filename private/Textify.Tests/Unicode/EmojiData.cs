﻿//
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

using System.Collections.Generic;
using System.Linq;
using Textify.Data.Unicode;

namespace Textify.Tests.Unicode
{
    public static class EmojiData
    {
        public static IEnumerable<object[]> AllEmojisFromEmojis =>
            EmojiManager.GetEmojis().Select((emoji) => new object[] { emoji.Sequence, emoji.Name }).ToArray();

        public static IEnumerable<object[]> AllEmojisFromEmojiNames =>
            EmojiManager.GetEmojis().Select((emoji) => new object[] { emoji.Name, emoji.Name, emoji.Sequence }).ToArray();

        public static IEnumerable<object[]> AllEmojisFromEmojiEnums =>
            EmojiManager.GetEmojis().Select((emoji) => new object[] { emoji.Enum, emoji.Name, emoji.Sequence }).ToArray();
    }
}
