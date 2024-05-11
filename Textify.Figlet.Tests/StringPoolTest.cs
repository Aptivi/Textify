// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Textify.Figlet.Utilities;

namespace Textify.Figlet.Tests;

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
