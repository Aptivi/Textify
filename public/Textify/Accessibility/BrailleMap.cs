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

namespace Textify.Accessibility
{
    internal static class BrailleMap
    {
        internal static (string, char, BrailleDots)[] brailleMap =
        [
            ("⠀", ' ', BrailleDots.None),
            ("⠁", 'a', BrailleDots.First),
            ("⠃", 'b', BrailleDots.First | BrailleDots.Second),
            ("⠉", 'c', BrailleDots.First | BrailleDots.Fourth),
            ("⠙", 'd', BrailleDots.First | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠑", 'e', BrailleDots.First | BrailleDots.Fifth),
            ("⠋", 'f', BrailleDots.First | BrailleDots.Second | BrailleDots.Fourth),
            ("⠛", 'g', BrailleDots.First | BrailleDots.Second | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠓", 'h', BrailleDots.First | BrailleDots.Second | BrailleDots.Fifth),
            ("⠊", 'i', BrailleDots.Second | BrailleDots.Fourth),
            ("⠚", 'j', BrailleDots.Second | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠅", 'k', BrailleDots.First | BrailleDots.Third),
            ("⠇", 'l', BrailleDots.First | BrailleDots.Second | BrailleDots.Third),
            ("⠍", 'm', BrailleDots.First | BrailleDots.Third | BrailleDots.Fourth),
            ("⠝", 'n', BrailleDots.First | BrailleDots.Third | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠕", 'o', BrailleDots.First | BrailleDots.Third | BrailleDots.Fifth),
            ("⠏", 'p', BrailleDots.First | BrailleDots.Second | BrailleDots.Third | BrailleDots.Fourth),
            ("⠟", 'q', BrailleDots.First | BrailleDots.Second | BrailleDots.Third | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠗", 'r', BrailleDots.First | BrailleDots.Second | BrailleDots.Third | BrailleDots.Fifth),
            ("⠎", 's', BrailleDots.Second | BrailleDots.Third | BrailleDots.Fourth),
            ("⠞", 't', BrailleDots.Second | BrailleDots.Third | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠥", 'u', BrailleDots.First | BrailleDots.Third | BrailleDots.Sixth),
            ("⠧", 'v', BrailleDots.First | BrailleDots.Second | BrailleDots.Third | BrailleDots.Sixth),
            ("⠺", 'w', BrailleDots.Second | BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Sixth),
            ("⠭", 'x', BrailleDots.First | BrailleDots.Third | BrailleDots.Fourth | BrailleDots.Sixth),
            ("⠽", 'y', BrailleDots.First | BrailleDots.Third | BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Sixth),
            ("⠵", 'z', BrailleDots.First | BrailleDots.Third | BrailleDots.Fifth | BrailleDots.Sixth),
            ("⠼", '#', BrailleDots.Third | BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Sixth),
            ("⠁", '1', BrailleDots.First),
            ("⠃", '2', BrailleDots.First | BrailleDots.Second),
            ("⠉", '3', BrailleDots.First | BrailleDots.Fourth),
            ("⠙", '4', BrailleDots.First | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠑", '5', BrailleDots.First | BrailleDots.Fifth),
            ("⠋", '6', BrailleDots.First | BrailleDots.Second | BrailleDots.Fourth),
            ("⠛", '7', BrailleDots.First | BrailleDots.Second | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠓", '8', BrailleDots.First | BrailleDots.Second | BrailleDots.Fifth),
            ("⠊", '9', BrailleDots.Second | BrailleDots.Fourth),
            ("⠚", '0', BrailleDots.Second | BrailleDots.Fourth | BrailleDots.Fifth),
            ("⠂", ',', BrailleDots.Second),
            ("⠆", ';', BrailleDots.Second | BrailleDots.Third),
            ("⠒", ':', BrailleDots.Second | BrailleDots.Fifth),
            ("⠲", '.', BrailleDots.Second | BrailleDots.Fifth | BrailleDots.Sixth),
            ("⠦", '?', BrailleDots.Second | BrailleDots.Third | BrailleDots.Sixth),
            ("⠖", '!', BrailleDots.Second | BrailleDots.Third | BrailleDots.Fifth),
            ("⠄", '\'', BrailleDots.Third),
            ("⠄⠦", '‘', BrailleDots.Third | BrailleDots.Second | BrailleDots.Third | BrailleDots.Sixth),
            ("⠄⠴", '’', BrailleDots.Third | BrailleDots.Second | BrailleDots.Third | BrailleDots.Fifth),
            ("⠘⠦", '“', BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Second | BrailleDots.Third | BrailleDots.Sixth),
            ("⠘⠴", '”', BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Second | BrailleDots.Third | BrailleDots.Fifth),
            ("⠐", '(', BrailleDots.Fourth),
            ("⠐", ')', BrailleDots.Third | BrailleDots.Fourth),
            ("⠸⠌", '/', BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Sixth | BrailleDots.Third | BrailleDots.Fourth),
            ("⠸⠡", '\\', BrailleDots.Fourth | BrailleDots.Fifth | BrailleDots.Sixth | BrailleDots.First | BrailleDots.Sixth),
            ("⠤", '‐', BrailleDots.Third | BrailleDots.Sixth),
            ("⠠⠤", '–', BrailleDots.Sixth | BrailleDots.Third | BrailleDots.Sixth),
            ("⠐⠠⠤", '—', BrailleDots.Fifth | BrailleDots.Sixth | BrailleDots.Third | BrailleDots.Sixth),
        ];
    }
}
