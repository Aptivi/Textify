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
using System;
using Textify.Data.Figlet;

namespace Textify.Tests.Figlet
{
    [TestClass]
    public class FigletFontTest
    {
        [TestMethod]
        [DataRow("standard", "Hello, World!", null,
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ")]
        [DataRow("threepoint", "Hello, World!", null,
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.")]
        [DataRow("ogre", "Hello, World!", null,
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ")]
        [DataRow("rectangles", "Hello, World!", null,
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ")]
        [DataRow("slant", "H.W", null,
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ")]
        [DataRow("impossible", "Figlet", null,
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
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/           ")]
        [DataRow("graffiti", "Hello, World!", null,
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/")]
        public void RenderByFontInstance(string fontName, string s, int? smushOverride = null, params string[] expected)
        {
            var font = FigletFonts.GetByName(fontName);
            var output = font.Render(s, smushOverride);
            var actual = output.Split([Environment.NewLine], StringSplitOptions.None);
            ProcessActual(actual, expected);
        }

        [TestMethod]
        [DataRow("standard", "Hello, World!",
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ")]
        [DataRow("threepoint", "Hello, World!",
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.")]
        [DataRow("ogre", "Hello, World!",
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ")]
        [DataRow("rectangles", "Hello, World!",
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ")]
        [DataRow("slant", "H.W",
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ")]
        [DataRow("impossible", "Figlet",
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
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/           ")]
        [DataRow("graffiti", "Hello, World!",
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/")]
        public void RenderByFigletTools(string fontName, string s, params string[] expected)
        {
            var output = FigletTools.RenderFiglet(s, fontName);
            var actual = output.Split('\n');
            ProcessActual(actual, expected);
        }

        private void ProcessActual(string[] actual, string[] expected)
        {
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
