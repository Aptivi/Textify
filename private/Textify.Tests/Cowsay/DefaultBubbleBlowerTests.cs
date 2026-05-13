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

using Shouldly;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class DefaultBubbleBlowerTests
    {
        [TestMethod()]
        public void Single_line_bubble_generated_correctly()
        {
            var bubbleGenerator = new DefaultBubbleBlower();

            var bubble = bubbleGenerator.GetBubble("Hello", 40, isThoughtBubble: false);

            bubble.ShouldBe($" _______ {Environment.NewLine}< Hello >{Environment.NewLine} ------- {Environment.NewLine}");
        }

        [TestMethod()]
        public void Multi_line_with_single_phrase_bubble_generated_correctly()
        {
            var bubbleGenerator = new DefaultBubbleBlower();

            var bubble = bubbleGenerator.GetBubble("Hello world, this is a bubble", 7, isThoughtBubble: false);

            bubble.ShouldBe($" _________{Environment.NewLine}/ Hello   \\{Environment.NewLine}| world,  |{Environment.NewLine}| this is |{Environment.NewLine}| a       |{Environment.NewLine}\\ bubble  /{Environment.NewLine} --------- {Environment.NewLine}");
        }

        [TestMethod()]
        public void Multi_line_with_multiple_phrases_bubble_generated_correctly()
        {
            var bubbleGenerator = new DefaultBubbleBlower();

            var bubble = bubbleGenerator.GetBubble($"Hello world{Environment.NewLine}This{Environment.NewLine}Cow{Environment.NewLine}This should be multiple lines", 11, isThoughtBubble: false);

            bubble.ShouldBe($" _____________{Environment.NewLine}/ Hello world \\{Environment.NewLine}| This        |{Environment.NewLine}| Cow         |{Environment.NewLine}| This should |{Environment.NewLine}| be multiple |{Environment.NewLine}\\ lines       /{Environment.NewLine} ------------- {Environment.NewLine}");
        }

        [TestMethod()]
        public void Multi_line_thought_bubble_generated_correctly()
        {
            var bubbleGenerator = new DefaultBubbleBlower();

            var bubble = bubbleGenerator.GetBubble("Hello world, this is a bubble", 7, isThoughtBubble: true);

            bubble.ShouldBe($" _________{Environment.NewLine}( Hello   ){Environment.NewLine}( world,  ){Environment.NewLine}( this is ){Environment.NewLine}( a       ){Environment.NewLine}( bubble  ){Environment.NewLine} --------- {Environment.NewLine}");
        }

        [TestMethod()]
        public void Single_line_thought_bubble_generated_correctly()
        {
            var bubbleGenerator = new DefaultBubbleBlower();

            var bubble = bubbleGenerator.GetBubble("Hello", 40, isThoughtBubble: true);

            bubble.ShouldBe($" _______ {Environment.NewLine}( Hello ){Environment.NewLine} ------- {Environment.NewLine}");
        }
    }
}
