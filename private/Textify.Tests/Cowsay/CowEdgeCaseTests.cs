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

using Textify.Data.Cowsay;
using NSubstitute;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class CowEdgeCaseTests
    {
        [TestMethod()]
        public void Speak_pads_short_eyes_to_two_characters()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("$eyes", bubbleBlower);

            // Act
            var result = cow.Speak("test", cowEyes: "x", cowTongue: "  ");

            // Assert
            result.ShouldContain("x "); // Should be padded to 2 chars
        }

        [TestMethod()]
        public void Speak_pads_short_tongue_to_two_characters()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("$tongue", bubbleBlower);

            // Act
            var result = cow.Speak("test", cowEyes: "oo", cowTongue: "U");

            // Assert
            result.ShouldContain("U "); // Should be padded to 2 chars
        }

        [TestMethod()]
        public void Speak_handles_empty_eyes()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("$eyes", bubbleBlower);

            // Act
            var result = cow.Speak("test", cowEyes: "", cowTongue: "  ");

            // Assert
            result.ShouldContain("  "); // Should be padded to 2 spaces
        }

        [TestMethod()]
        public void Speak_handles_empty_tongue()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("$tongue", bubbleBlower);

            // Act
            var result = cow.Speak("test", cowEyes: "oo", cowTongue: "");

            // Assert
            result.ShouldContain("  "); // Should be padded to 2 spaces
        }

        [TestMethod()]
        public void Speak_truncates_long_eyes_to_first_and_last_character()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("$eye $eye", bubbleBlower);

            // Act
            var result = cow.Speak("test", cowEyes: "ABCDEFG", cowTongue: "  ");

            // Assert
            result.ShouldContain("A ");
            result.ShouldContain(" G");
        }

        [TestMethod()]
        public void Think_sets_thoughts_to_o()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), true).Returns("bubble");
            var cow = new Cow("$thoughts", bubbleBlower);

            // Act
            var result = cow.Think("test");

            // Assert
            result.ShouldContain("o");
        }

        [TestMethod()]
        public void Speak_sets_thoughts_to_backslash()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), false).Returns("bubble");
            var cow = new Cow("$thoughts", bubbleBlower);

            // Act
            var result = cow.Speak("test");

            // Assert
            result.ShouldContain(@"\");
        }

        [TestMethod()]
        public void Cow_handles_format_without_placeholders()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("just a plain cow", bubbleBlower);

            // Act
            var result = cow.Speak("test");

            // Assert
            result.ShouldBe("bubblejust a plain cow");
        }

        [TestMethod()]
        public void Cow_preserves_format_with_unknown_placeholders()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            bubbleBlower.GetBubble(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<bool>()).Returns("bubble");
            var cow = new Cow("$unknown $placeholder", bubbleBlower);

            // Act
            var result = cow.Speak("test");

            // Assert
            result.ShouldBe("bubble$unknown $placeholder");
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(int.MaxValue)]
        public void Speak_passes_maxCols_to_bubble_generator(int maxCols)
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            var cow = new Cow("test", bubbleBlower);

            // Act
            cow.Speak("test", maxCols: maxCols);

            // Assert
            bubbleBlower.Received(1).GetBubble("test", maxCols, false);
        }
    }
}
