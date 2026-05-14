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

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.IO;
using Textify.Data.Cowsay;
using Textify.Tests.Cowsay.Stubs;

namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class CowFileTests
    {
        [TestMethod()]
        public async Task Successfully_loads_cow_from_string()
        {
            var cowFile = new CowFile(File.ReadAllText(Path.Combine("Cowsay/TestCows", "default.cow")));

            var format = await cowFile.GetCowFormatAsync();

            format.ShouldBe(File.ReadAllText(Path.Combine("Cowsay/ExpectedOutputCows", "default_cleaned.txt")));
        }

        [TestMethod()]
        public async Task Successfully_loads_cow_from_stream()
        {
            string format;

            using (var stream = File.OpenRead(Path.Combine("Cowsay/TestCows", "default.cow")))
            {
                var cowFile = new CowFile(stream);
                format = await cowFile.GetCowFormatAsync();
            }

            format.ShouldBe(File.ReadAllText(Path.Combine("Cowsay/ExpectedOutputCows", "default_cleaned.txt")));
        }

        [TestMethod()]
        public async Task Throws_ArgumentException_if_cow_cant_be_parsed()
        {
            var cowFile = new CowFile("not a cow");

            await Should.ThrowAsync<ArgumentException>(
                async () => await cowFile.GetCowFormatAsync());
        }

        [TestMethod()]
        public async Task Multiple_threads_accessing_cowfile_reads_stream_once()
        {
            string expectedFormat = File.ReadAllText(Path.Combine("Cowsay/ExpectedOutputCows", "default_cleaned.txt"));

            using (var stream = File.OpenRead(Path.Combine("Cowsay/TestCows", "default.cow")))
            using (var slowStream = new SlowStream(stream))
            {
                var cowFile = new CowFile(slowStream);
                Task<string>[] tasks = new Task<string>[10];

                for (int i = 0; i < 10; i++)
                {
                    tasks[i] = Task.Run(async () => await cowFile.GetCowFormatAsync());
                }

                await Task.Delay(100);

                slowStream.ThreadsReadingCount.ShouldBe(1);

                await Task.WhenAll(tasks);

                tasks.ShouldAllBe(f => f.Result == expectedFormat);
            }
        }
    }
}
