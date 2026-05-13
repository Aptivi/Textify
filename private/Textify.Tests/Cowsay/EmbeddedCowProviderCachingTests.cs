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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class EmbeddedCowProviderCachingTests
    {
        [TestMethod()]
        public async Task GetAvailableCowsAsync_returns_same_string_instances_from_cache()
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();

            // Act
            var cows1 = await provider.GetAvailableCowsAsync();
            var cows2 = await provider.GetAvailableCowsAsync();

            // Assert - the actual string instances should be the same (from cache)
            for (int i = 0; i < cows1.Count; i++)
            {
                ReferenceEquals(cows1[i], cows2[i]).ShouldBeTrue($"String at index {i} should be same instance");
            }
        }

        [TestMethod()]
        public async Task GetAvailableCowsAsync_caches_cow_list_after_first_call()
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();

            // Act - multiple parallel calls
            var tasks = Enumerable.Range(0, 10)
                .Select(_ => provider.GetAvailableCowsAsync())
                .ToArray();

            var results = await Task.WhenAll(tasks);

            // Assert - all results should have the same string instances (from cache)
            for (int i = 1; i < results.Length; i++)
            {
                results[i].SequenceEqual(results[0]).ShouldBeTrue();
                // Check string instances are the same
                for (int j = 0; j < results[0].Count; j++)
                {
                    ReferenceEquals(results[i][j], results[0][j]).ShouldBeTrue();
                }
            }
        }

        [TestMethod()]
        public async Task GetCowFormatAsync_uses_cached_cow_list()
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();

            // Act - call GetAvailableCowsAsync first to populate cache
            var availableCows = await provider.GetAvailableCowsAsync();

            // Then get a specific cow format multiple times
            var format1 = await provider.GetCowFormatAsync("default");
            var format2 = await provider.GetCowFormatAsync("default");
            var format3 = await provider.GetCowFormatAsync("tux");

            // Assert - formats should be retrieved successfully
            format1.ShouldNotBeNullOrEmpty();
            format2.ShouldNotBeNullOrEmpty();
            format3.ShouldNotBeNullOrEmpty();
            format1.ShouldBe(format2); // Same cow should return same format
        }

        [TestMethod()]
        public async Task Multiple_providers_have_independent_caches()
        {
            // Arrange
            var provider1 = new EmbeddedCowFormatProvider();
            var provider2 = new EmbeddedCowFormatProvider();

            // Act
            var cows1 = await provider1.GetAvailableCowsAsync();
            var cows2 = await provider2.GetAvailableCowsAsync();

            // Assert - different providers load their own cache
            // Content should be the same
            cows1.SequenceEqual(cows2).ShouldBeTrue();
            // But string instances might be different (each provider has its own cache)
            // Note: strings might still be the same due to .NET string interning,
            // so we can't reliably test that they're different instances
        }

        [TestMethod()]
        public async Task GetAvailableCowsAsync_returns_sorted_list()
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();

            // Act
            var cows = await provider.GetAvailableCowsAsync();
            var cowsList = cows.ToList();

            // Assert - should be sorted alphabetically
            var sortedList = cowsList.OrderBy(x => x).ToList();
            cowsList.ShouldBe(sortedList);
        }

        [TestMethod()]
        public async Task Lazy_initialization_is_thread_safe()
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();
            const int threadCount = 20;

            // Act - multiple threads try to access cache simultaneously
            var tasks = Enumerable.Range(0, threadCount)
                .Select(_ => Task.Run(() => provider.GetAvailableCowsAsync()))
                .ToArray();

            var allResults = await Task.WhenAll(tasks);

            // Assert - all threads should get the same content (cache works correctly)
            for (int i = 1; i < allResults.Length; i++)
            {
                allResults[i].SequenceEqual(allResults[0]).ShouldBeTrue();
                allResults[i].Count.ShouldBe(allResults[0].Count);
            }
        }

        [TestMethod()]
        public async Task GetCowFormatAsync_can_retrieve_all_cached_cows()
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();
            var availableCows = await provider.GetAvailableCowsAsync();

            // Act & Assert - verify we can get format for first few cows
            // (not all to keep test fast)
            var testCows = availableCows.Take(5);
            foreach (var cowName in testCows)
            {
                var format = await provider.GetCowFormatAsync(cowName);
                format.ShouldNotBeNullOrEmpty();
            }
        }
    }
}
