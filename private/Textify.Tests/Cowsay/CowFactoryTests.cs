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
using Shouldly;
using NSubstitute;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class CowFactoryTests
    {
        [TestMethod()]
        public async Task Create_from_provider_returns_expected_cow()
        {
            var provider = Substitute.For<ICowFormatProvider>();
            provider
                .GetCowFormatAsync("default")
                .Returns("abc$eyedef");

            var bubbleBlower = Substitute.For<IBubbleBlower>();
            var factory = new DefaultCattleFarmer(provider, bubbleBlower);

            var cow = await factory.RearCowAsync("default");

            cow.Format.ShouldBe("abc$eyedef");
        }

        [TestMethod()]
        public async Task Create_from_stream_returns_expected_cow()
        {
            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("$the_cow = <<EOC;\r\nHello$eye$eye\r\nEOC"));
            var bubbleBlower = Substitute.For<IBubbleBlower>();
            var factory = new DefaultCattleFarmer(null, bubbleBlower);

            var cow = await factory.RearCowFromFileStreamAsync(memoryStream);

            cow.Format.ShouldBe("Hello$eye$eye");
        }

        [TestMethod()]
        public async Task Create_using_defaults_returns_expected_cow()
        {
            var cow = await DefaultCattleFarmer.RearCowWithDefaults("default");

            cow.Format.ShouldBe(File.ReadAllText(Path.Combine("Cowsay/ExpectedOutputCows", "default_cleaned.txt")));
        }
    }
}
