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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Data.Figlet.Utilities;

namespace Textify.Tests.Figlet
{
    public class StringPoolTest
    {
        [TestMethod]
        public void PoolsReferences()
        {
            var pool = new StringPool();

            var s1 = "s";
            var s2 = "S".ToLower();

            Assert.AreNotSame(s1, s2);
            s2.ShouldBe(s1);

            Assert.AreSame(s1, pool.Pool(s1));
            Assert.AreSame(s1, pool.Pool(s1));
            Assert.AreSame(s1, pool.Pool(s2));
            Assert.AreSame(s1, pool.Pool(s2));
        }
    }
}
