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

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class StreamExtensionsTests
    {
        [TestMethod()]
        public async Task ConvertToStringAsync_reads_stream_content()
        {
            // Arrange
            const string expectedContent = "Hello, World!";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Act
            var result = await stream.ConvertToStringAsync();

            // Assert
            result.ShouldBe(expectedContent);
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_with_leaveOpen_true_keeps_stream_open()
        {
            // Arrange
            const string expectedContent = "Test content";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Act
            var result = await stream.ConvertToStringAsync(leaveOpen: true);

            // Assert
            result.ShouldBe(expectedContent);
            stream.CanRead.ShouldBeTrue(); // Stream should still be open
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_with_leaveOpen_false_closes_stream()
        {
            // Arrange
            const string expectedContent = "Test content";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Act
            var result = await stream.ConvertToStringAsync(leaveOpen: false);

            // Assert
            result.ShouldBe(expectedContent);
            stream.CanRead.ShouldBeFalse(); // Stream should be closed
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_resets_stream_position_before_reading()
        {
            // Arrange
            const string expectedContent = "Reset position test";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Move stream position to middle
            stream.Seek(5, SeekOrigin.Begin);

            // Act
            var result = await stream.ConvertToStringAsync();

            // Assert
            result.ShouldBe(expectedContent); // Should read from beginning despite initial position
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_handles_empty_stream()
        {
            // Arrange
            using var stream = new MemoryStream();

            // Act
            var result = await stream.ConvertToStringAsync();

            // Assert
            result.ShouldBe(string.Empty);
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_handles_unicode_content()
        {
            // Arrange
            const string expectedContent = "Hello 世界 🐮";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Act
            var result = await stream.ConvertToStringAsync();

            // Assert
            result.ShouldBe(expectedContent);
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_handles_multiline_content()
        {
            // Arrange
            const string expectedContent = "Line 1\nLine 2\r\nLine 3";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Act
            var result = await stream.ConvertToStringAsync();

            // Assert
            result.ShouldBe(expectedContent);
        }

        [TestMethod()]
        public async Task ConvertToStringAsync_can_be_called_multiple_times_with_leaveOpen()
        {
            // Arrange
            const string expectedContent = "Reusable content";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedContent));

            // Act
            var result1 = await stream.ConvertToStringAsync(leaveOpen: true);
            var result2 = await stream.ConvertToStringAsync(leaveOpen: true);

            // Assert
            result1.ShouldBe(expectedContent);
            result2.ShouldBe(expectedContent);
        }
    }
}
