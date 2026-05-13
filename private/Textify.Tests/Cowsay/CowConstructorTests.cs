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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class CowConstructorTests
    {
        [TestMethod()]
        public void Constructor_throws_ArgumentNullException_for_null_cowFormat()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();

            // Act & Assert
            var exception = Should.Throw<ArgumentNullException>(() => new Cow(null, bubbleBlower));
            exception.ParamName.ShouldBe("cowFormat");
        }

        [TestMethod()]
        public void Constructor_throws_ArgumentNullException_for_null_bubbleGenerator()
        {
            // Arrange
            const string cowFormat = "test format";

            // Act & Assert
            var exception = Should.Throw<ArgumentNullException>(() => new Cow(cowFormat, null));
            exception.ParamName.ShouldBe("bubbleGenerator");
        }

        [TestMethod()]
        public void Constructor_accepts_empty_cowFormat()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            const string emptyFormat = "";

            // Act
            var cow = new Cow(emptyFormat, bubbleBlower);

            // Assert
            cow.ShouldNotBeNull();
            cow.Format.ShouldBe(emptyFormat);
        }

        [TestMethod()]
        public void Format_property_returns_constructor_cowFormat()
        {
            // Arrange
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            const string expectedFormat = "custom $eyes format $thoughts";

            // Act
            var cow = new Cow(expectedFormat, bubbleBlower);

            // Assert
            cow.Format.ShouldBe(expectedFormat);
        }
    }
}
