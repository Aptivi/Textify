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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Data.Unicode;

namespace Textify.Tests.Unicode
{
    [TestClass]
    public class UnicodeQueryTests
    {
        [TestMethod]
        [DataRow('A', "LATIN CAPITAL LETTER A")]
        [DataRow('P', "LATIN CAPITAL LETTER P")]
        [DataRow('T', "LATIN CAPITAL LETTER T")]
        [DataRow('V', "LATIN CAPITAL LETTER V")]
        [DataRow('a', "LATIN SMALL LETTER A")]
        [DataRow('p', "LATIN SMALL LETTER P")]
        [DataRow('t', "LATIN SMALL LETTER T")]
        [DataRow('v', "LATIN SMALL LETTER V")]
        public void QueryUnicodeCharacter(char character, string expectedUnicodeName)
        {
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            charInstance.Na.ShouldBe(expectedUnicodeName);
        }

        [TestMethod]
        [DataRow('\r', "CARRIAGE RETURN (CR)")]
        [DataRow('\n', "LINE FEED (LF)")]
        [DataRow('\t', "CHARACTER TABULATION")]
        [DataRow('\b', "BACKSPACE")]
        public void QueryUnicodeControlCharacter(char character, string expectedUnicodeName)
        {
            var charInstance = UnicodeQuery.QueryChar(character, UnicodeQueryType.Simple);
            charInstance.Na1.ShouldBe(expectedUnicodeName);
        }

        [TestMethod]
        [DataRow("😀", "grinning face")]
        [DataRow("☺️", "smiling face")]
        [DataRow("🤵‍♂️", "man in tuxedo")]
        public void QueryEmojiFromEmoji(string emoji, string expectedName)
        {
            var emojiInstance = EmojiManager.GetEmojiFromEmoji(emoji);
            emojiInstance.Name.ShouldBe(expectedName);
        }

        [TestMethod]
        [DataRow("grinning face", "grinning face", "😀")]
        [DataRow("smiling face", "smiling face", "☺️")]
        [DataRow("man in tuxedo", "man in tuxedo", "🤵‍♂️")]
        public void QueryEmojisFromEmojiName(string emoji, string expectedName, string expectedSequence)
        {
            var emojiInstance = EmojiManager.GetEmojisFromName(emoji)[0];
            emojiInstance.Name.ShouldBe(expectedName);
            emojiInstance.Sequence.ShouldBe(expectedSequence);
        }

        [TestMethod]
        [DataRow(EmojiEnum.GrinningFace, "grinning face", "😀")]
        [DataRow(EmojiEnum.SmilingFace, "smiling face", "☺️")]
        [DataRow(EmojiEnum.ManInTuxedo, "man in tuxedo", "🤵‍♂️")]
        public void QueryEmojisFromEmojiEnum(EmojiEnum emoji, string expectedName, string expectedSequence)
        {
            var emojiInstance = EmojiManager.GetEmojiFromEnum(emoji);
            emojiInstance.Name.ShouldBe(expectedName);
            emojiInstance.Sequence.ShouldBe(expectedSequence);
        }

        [TestMethod]
        [DynamicData(nameof(EmojiData.AllEmojisFromEmojis), typeof(EmojiData))]
        public void QueryEmojiFromEmojis(string emoji, string expectedName)
        {
            var emojiInstance = EmojiManager.GetEmojiFromEmoji(emoji);
            emojiInstance.Name.ShouldBe(expectedName);
        }

        [TestMethod]
        [DynamicData(nameof(EmojiData.AllEmojisFromEmojiNames), typeof(EmojiData))]
        public void QueryEmojisFromEmojiNames(string emoji, string expectedName, string expectedSequence)
        {
            var emojiInstances = EmojiManager.GetEmojisFromName(emoji);
            bool found = false;
            foreach (var instance in emojiInstances)
            {
                if (instance.Name == expectedName && instance.Sequence == expectedSequence)
                {
                    found = true;
                    break;
                }
            }
            found.ShouldBeTrue();
        }

        [TestMethod]
        [DynamicData(nameof(EmojiData.AllEmojisFromEmojiEnums), typeof(EmojiData))]
        public void QueryEmojiFromEmojiEnums(EmojiEnum emoji, string expectedName, string expectedSequence)
        {
            var emojiInstance = EmojiManager.GetEmojiFromEnum(emoji);
            emojiInstance.Name.ShouldBe(expectedName);
            emojiInstance.Sequence.ShouldBe(expectedSequence);
        }

        [TestMethod]
        [DataRow(KaomojiCategory.Positive, KaomojiSubcategory.Joy, 0, "(* ^ ω ^)")]
        [DataRow(KaomojiCategory.Negative, KaomojiSubcategory.Dissatisfaction, 1, "(；⌣̀_⌣́)")]
        [DataRow(KaomojiCategory.Neutral, KaomojiSubcategory.Doubt, 2, "(￢ ￢)")]
        public void QueryKaomojiFromKaomojiEnums(KaomojiCategory category, KaomojiSubcategory subcategory, int sequenceIdx, string expectedSequence)
        {
            string sequence = KaomojiManager.GetKaomoji(category, subcategory, sequenceIdx);
            sequence.ShouldBe(expectedSequence);
        }
    }
}
