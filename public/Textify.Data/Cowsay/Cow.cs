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

using System;
using System.Text;
using System.Linq;

namespace Textify.Data.Cowsay
{
    /// <summary>
    /// Cow class instance
    /// </summary>
    public class Cow : ICow
    {
        private readonly string _cowFormat;
        private readonly IBubbleBlower _bubbleGenerator;

        /// <inheritdoc/>
        public string Format =>
            _cowFormat;

        /// <inheritdoc/>
        public string Speak(string phrase, string cowEyes = "oo", string cowTongue = "  ", int maxCols = 40) =>
            Act(phrase, cowEyes, cowTongue, maxCols, isThought: false);

        /// <inheritdoc/>
        public string Think(string phrase, string cowEyes = "oo", string cowTongue = "  ", int maxCols = 40) =>
            Act(phrase, cowEyes, cowTongue, maxCols, isThought: true);

        private string Act(string phrase, string cowEyes = "oo", string cowTongue = "  ", int maxCols = 40, bool isThought = false)
        {
            cowTongue = cowTongue.PadRight(2);
            cowEyes = cowEyes.PadRight(2);

            string bubble = _bubbleGenerator.GetBubble(phrase, maxCols, isThought);

            var cowBuilder = new StringBuilder(_cowFormat);
            cowBuilder.Replace("$eyes", cowEyes);
            cowBuilder.Replace("$tongue", cowTongue);
            cowBuilder.Replace("$thoughts", isThought ? "o": @"\");

            string cow = cowBuilder.ToString();
            cow = RegularExpressions.Eye.Replace(cow, cowEyes.First().ToString(), 1);
            cow = RegularExpressions.Eye.Replace(cow, cowEyes.Last().ToString(), 1);

            return bubble + cow;
        }

        /// <summary>
        /// Makes a new cow instance
        /// </summary>
        /// <param name="cowFormat">A string instance that shows you an ASCII art of a cow speaking</param>
        /// <param name="bubbleGenerator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Cow(string? cowFormat, IBubbleBlower? bubbleGenerator)
        {
            _cowFormat = cowFormat ?? throw new ArgumentNullException(nameof(cowFormat));
            _bubbleGenerator = bubbleGenerator ?? throw new ArgumentNullException(nameof(bubbleGenerator));
        }
    }
}
