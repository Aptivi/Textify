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

using System.Collections;
using System.Collections.Generic;

namespace Textify.General.Structures.Helpers
{
    internal class WideStringEnumerator : IEnumerator<WideChar>
    {
        internal WideChar[] characters = [];
        private int index = -1;

        public WideChar Current =>
            characters[index];

        object IEnumerator.Current =>
            Current;

        public void Dispose()
        {
            characters = [];
            index = -1;
        }

        public bool MoveNext()
        {
            if (index + 1 == characters.Length)
                return false;
            index++;
            return true;
        }

        public void Reset() =>
            index = -1;
    }
}
