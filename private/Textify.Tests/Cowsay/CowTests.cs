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

// Originally found in https://github.com/rawsonm88/Cowsay

using Textify.Data.Cowsay;
using Shouldly;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class CowTests
    {
        [TestMethod]
        [DataRow("$eye$tongue$thoughts$eye", @"<A bubble>ACD\B")]
        [DataRow("$eyes$tongue$thoughts", @"<A bubble>ABCD\")]
        public void Calling_speak_replaces_the_placeholders_and_includes_the_speech_bubble(string format, string expectedOutput)
        {
            var bubbleGenerator = Substitute.For<IBubbleBlower>();

            bubbleGenerator
                .GetBubble("Hello world", 10)
                .Returns("<A bubble>");

            var cow = new Cow(cowFormat: format, bubbleGenerator);

            var output = cow.Speak("Hello world", cowEyes: "AB", cowTongue: "CD", maxCols: 10);

            output.ShouldBe(expectedOutput);
        }

        [TestMethod]
        [DataRow("$eye$tongue$thoughts$eye", @"<A bubble>ACDoB")]
        [DataRow("$eyes$tongue$thoughts", @"<A bubble>ABCDo")]
        public void Calling_think_replaces_the_placeholders_and_includes_the_thought_bubble(string format, string expectedOutput)
        {
            var bubbleGenerator = Substitute.For<IBubbleBlower>();

            bubbleGenerator
                .GetBubble("Hello world", 10, isThought: true)
                .Returns("<A bubble>");

            var cow = new Cow(cowFormat: format, bubbleGenerator);

            var output = cow.Think("Hello world", cowEyes: "AB", cowTongue: "CD", maxCols: 10);

            output.ShouldBe(expectedOutput);
        }
    }
}
