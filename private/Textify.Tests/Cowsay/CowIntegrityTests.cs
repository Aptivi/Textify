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
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textify.Data.Cowsay;

#pragma warning disable MSTESTEXP
namespace Textify.Tests.Cowsay
{
    [TestClass]
    public class CowIntegrityTests
    {
        private static readonly IEnumerable<(string name, string fullPath)> cowsList = EmbeddedCowFormatProvider.LoadCows();

        [TestMethod()]
        [DynamicData(nameof(cowsList))]
        public async Task TestAllCows(string Name, string FullPath)
        {
            // Arrange
            var provider = new EmbeddedCowFormatProvider();

            // Get a specific cow
            var format = await provider.GetCowFormatAsync(Name);
            TestContext.Current?.WriteLine("{0} - {1}\n\n{2}", Name, FullPath, format);
        }
    }
}
#pragma warning restore MSTESTEXP
