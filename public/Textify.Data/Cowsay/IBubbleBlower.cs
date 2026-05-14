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

namespace Textify.Data.Cowsay
{
    /// <summary>
    /// Bubble blower interface
    /// </summary>
    public interface IBubbleBlower
    {
        /// <summary>
        /// Generates the speech/thought bubble.
        /// </summary>
        /// <param name="phrase">The phrase to include inside the bubble.</param>
        /// <param name="maxCols">The maximum columns of text before wrapping.</param>
        /// <param name="isThought">True, if this should be a thought bubble.</param>
        /// <returns>The ASCII speech/thought bubble.</returns>
        string GetBubble(string phrase, int maxCols, bool isThought = false);
    }
}
