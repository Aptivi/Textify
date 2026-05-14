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

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.IO;
using System.Linq;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class EmbeddedCowProviderTests
    {
        [TestMethod()]
        public async Task Non_existent_cow_throw_FileNotFoundException()
        {
            var provider = new EmbeddedCowFormatProvider();

            var exception = await Should.ThrowAsync<FileNotFoundException>(
                async () => await provider.GetCowFormatAsync("no-a-real-cow"));
        }

        [TestMethod()]
        public async Task Real_cow_returns_cow_format_without_escaping()
        {
            var provider = new EmbeddedCowFormatProvider();

            var format = await provider.GetCowFormatAsync("default");

            format.ShouldBe(File.ReadAllText(Path.Combine("Cowsay/ExpectedOutputCows", "default_cleaned.txt")));
        }

        [TestMethod()]
        public async Task GetAvailableCowsAsync_returns_all_embedded_cows()
        {
            var provider = new EmbeddedCowFormatProvider();

            var cows = await provider.GetAvailableCowsAsync();

            cows.ShouldNotBeNull();
            cows.ShouldNotBeEmpty();
            cows.ShouldContain("default");
            cows.ShouldContain("tux");
            cows.ShouldContain("stegosaurus");
            cows.ShouldContain("dragon");
            cows.ShouldContain("vader");
            cows.Count.ShouldBeGreaterThan(50);
        }

        [TestMethod()]
        public async Task GetAvailableCowsAsync_returns_unique_cow_names()
        {
            var provider = new EmbeddedCowFormatProvider();

            var cows = await provider.GetAvailableCowsAsync();
            var cowList = cows.ToList();

            cowList.ShouldBeUnique();
        }

        [TestMethod()]
        public async Task GetAvailableCowsAsync_returns_cow_names_without_extension()
        {
            var provider = new EmbeddedCowFormatProvider();

            var cows = await provider.GetAvailableCowsAsync();

            cows.ShouldAllBe(cow => !cow.EndsWith(".cow"));
            cows.ShouldAllBe(cow => !cow.Contains("Textify.Data"));
        }
    }
}
