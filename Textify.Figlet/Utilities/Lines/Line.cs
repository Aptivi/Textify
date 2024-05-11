//
// Textify  Copyright (C) 2023-2024  Aptivi
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

namespace Textify.Figlet.Utilities.Lines
{
    internal readonly struct Line
    {
        public string Content { get; }
        public byte SpaceBefore { get; }
        public byte SpaceAfter { get; }

        public char FrontChar =>
            Content.Length == SpaceBefore ? ' ' : Content[SpaceBefore];
        public char BackChar =>
            Content.Length == SpaceAfter ? ' ' : Content[Content.Length - SpaceAfter - 1];

        public Line(string content, byte spaceBefore, byte spaceAfter)
        {
            Content = content;
            SpaceBefore = spaceBefore;
            SpaceAfter = spaceAfter;
        }
    }
}
