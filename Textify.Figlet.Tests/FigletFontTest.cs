// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Textify.Figlet.Utilities;

namespace Textify.Figlet.Tests;

[TestClass]
public class FigletFontTest
{
    [TestMethod]
    public void RenderByFontInstance()
    {
        Test(FigletFonts.TryGetByName("standard"), "Hello, World!", null,
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        Test(FigletFonts.TryGetByName("threepoint"), "Hello, World!", null,
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.");

        Test(FigletFonts.TryGetByName("ogre"), "Hello, World!", null,
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        Test(FigletFonts.TryGetByName("rectangles"), "Hello, World!", null,
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        Test(FigletFonts.TryGetByName("slant"), "Hello, World!", null,
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        Test(FigletFonts.TryGetByName("slant"), "H.W", null,
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ");

        Test(FigletFonts.TryGetByName("impossible"), "Figlet", null,
            @"         _        _          _              _             _          _       ",
            @"        /\ \     /\ \       /\ \           _\ \          /\ \       /\ \     ",
            @"       /  \ \    \ \ \     /  \ \         /\__ \        /  \ \      \_\ \    ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /_ \_\      / /\ \ \     /\__ \   ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/     / / /\ \_\   / /_ \ \  ",
            @"    / /_/_ \/_// / /    / / / ______   / / /         / /_/_ \/_/  / / /\ \ \ ",
            @"   / /____/\  / / /    / / / /\_____\ / / /         / /____/\    / / /  \/_/ ",
            @"  / /\____\/ / / /    / / /  \/____ // / / ____    / /\____\/   / / /        ",
            @" / / /   ___/ / /__  / / /_____/ / // /_/_/ ___/\ / / /______  / / /         ",
            @"/ / /   /\__\/_/___\/ / /______\/ //_______/\__\// / /_______\/_/ /          ",
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/           ");

        Test(FigletFonts.TryGetByName("graffiti"), "Hello, World!", null,
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/");

        void Test(FigletFont font, string s, int? smushOverride = null, params string[] expected)
        {
            var output = font.Render(s, smushOverride);
            var actual = output.Split([Environment.NewLine], StringSplitOptions.None);
            actual.Length.ShouldBe(expected.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                if (expected[i] == actual[i])
                    continue;
                if (expected[i].Length != actual[i].Length)
                    Assert.Fail($"Mismatched lengths row {i}. Expecting '{expected[i].Length}' but got '{actual[i].Length}'.");

                for (var x = 0; x < expected[i].Length; x++)
                {
                    if (expected[i][x] != actual[i][x])
                        Assert.Fail($"Mismatch at row {i} col {x}. Expecting '{expected[i][x]}' but got '{actual[i][x]}'.");
                }
            }
        }
    }

    [TestMethod]
    public void RenderByFigletTools()
    {
        TestFont(FigletFonts.TryGetByName("standard"), "Hello, World!",
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        TestFont(FigletFonts.TryGetByName("threepoint"), "Hello, World!",
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.");

        TestFont(FigletFonts.TryGetByName("ogre"), "Hello, World!",
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        TestFont(FigletFonts.TryGetByName("rectangles"), "Hello, World!",
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        TestFont(FigletFonts.TryGetByName("slant"), "Hello, World!",
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        TestFont(FigletFonts.TryGetByName("slant"), "H.W",
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ");

        TestFont(FigletFonts.TryGetByName("impossible"), "Figlet",
            @"         _        _          _              _             _          _       ",
            @"        /\ \     /\ \       /\ \           _\ \          /\ \       /\ \     ",
            @"       /  \ \    \ \ \     /  \ \         /\__ \        /  \ \      \_\ \    ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /_ \_\      / /\ \ \     /\__ \   ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/     / / /\ \_\   / /_ \ \  ",
            @"    / /_/_ \/_// / /    / / / ______   / / /         / /_/_ \/_/  / / /\ \ \ ",
            @"   / /____/\  / / /    / / / /\_____\ / / /         / /____/\    / / /  \/_/ ",
            @"  / /\____\/ / / /    / / /  \/____ // / / ____    / /\____\/   / / /        ",
            @" / / /   ___/ / /__  / / /_____/ / // /_/_/ ___/\ / / /______  / / /         ",
            @"/ / /   /\__\/_/___\/ / /______\/ //_______/\__\// / /_______\/_/ /          ",
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/           ");

        TestFont(FigletFonts.TryGetByName("graffiti"), "Hello, World!",
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/");

        Test("standard", "Hello, World!",
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        Test("threepoint", "Hello, World!",
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.");

        Test("ogre", "Hello, World!",
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        Test("rectangles", "Hello, World!",
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        Test("slant", "Hello, World!",
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        Test("slant", "H.W",
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ");

        Test("impossible", "Figlet",
            @"         _        _          _              _             _          _       ",
            @"        /\ \     /\ \       /\ \           _\ \          /\ \       /\ \     ",
            @"       /  \ \    \ \ \     /  \ \         /\__ \        /  \ \      \_\ \    ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /_ \_\      / /\ \ \     /\__ \   ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/     / / /\ \_\   / /_ \ \  ",
            @"    / /_/_ \/_// / /    / / / ______   / / /         / /_/_ \/_/  / / /\ \ \ ",
            @"   / /____/\  / / /    / / / /\_____\ / / /         / /____/\    / / /  \/_/ ",
            @"  / /\____\/ / / /    / / /  \/____ // / / ____    / /\____\/   / / /        ",
            @" / / /   ___/ / /__  / / /_____/ / // /_/_/ ___/\ / / /______  / / /         ",
            @"/ / /   /\__\/_/___\/ / /______\/ //_______/\__\// / /_______\/_/ /          ",
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/           ");

        Test("graffiti", "Hello, World!",
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/");

        void TestFont(FigletFont font, string s, params string[] expected) =>
            Test(font.Name, s, expected);

        void Test(string fontName, string s, params string[] expected)
        {
            var output = FigletTools.RenderFiglet(s, fontName);
            var actual = output.Split('\n');
            actual.Length.ShouldBe(expected.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                if (expected[i] == actual[i])
                    continue;
                if (expected[i].Length != actual[i].Length)
                    Assert.Fail($"Mismatched lengths row {i}. Expecting '{expected[i].Length}' but got '{actual[i].Length}'.");

                for (var x = 0; x < expected[i].Length; x++)
                {
                    if (expected[i][x] != actual[i][x])
                        Assert.Fail($"Mismatch at row {i} col {x}. Expecting '{expected[i][x]}' but got '{actual[i][x]}'.");
                }
            }
        }
    }
}
