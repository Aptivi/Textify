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
using System.Collections.Generic;
using System.Text;

namespace Textify.Data.Unicode.UBidi
{
    internal static class BidiBracketMap
    {
        private readonly static Dictionary<char, (char, char)> bracketType = new()
        {
            { '\u0028', ('\u0029', 'o') },
            { '\u0029', ('\u0028', 'c') },
            { '\u005B', ('\u005D', 'o') },
            { '\u005D', ('\u005B', 'c') },
            { '\u007B', ('\u007D', 'o') },
            { '\u007D', ('\u007B', 'c') },
            { '\u0F3A', ('\u0F3B', 'o') },
            { '\u0F3B', ('\u0F3A', 'c') },
            { '\u0F3C', ('\u0F3D', 'o') },
            { '\u0F3D', ('\u0F3C', 'c') },
            { '\u169B', ('\u169C', 'o') },
            { '\u169C', ('\u169B', 'c') },
            { '\u2045', ('\u2046', 'o') },
            { '\u2046', ('\u2045', 'c') },
            { '\u207D', ('\u207E', 'o') },
            { '\u207E', ('\u207D', 'c') },
            { '\u208D', ('\u208E', 'o') },
            { '\u208E', ('\u208D', 'c') },
            { '\u2308', ('\u2309', 'o') },
            { '\u2309', ('\u2308', 'c') },
            { '\u230A', ('\u230B', 'o') },
            { '\u230B', ('\u230A', 'c') },
            { '\u2329', ('\u232A', 'o') },
            { '\u232A', ('\u2329', 'c') },
            { '\u2768', ('\u2769', 'o') },
            { '\u2769', ('\u2768', 'c') },
            { '\u276A', ('\u276B', 'o') },
            { '\u276B', ('\u276A', 'c') },
            { '\u276C', ('\u276D', 'o') },
            { '\u276D', ('\u276C', 'c') },
            { '\u276E', ('\u276F', 'o') },
            { '\u276F', ('\u276E', 'c') },
            { '\u2770', ('\u2771', 'o') },
            { '\u2771', ('\u2770', 'c') },
            { '\u2772', ('\u2773', 'o') },
            { '\u2773', ('\u2772', 'c') },
            { '\u2774', ('\u2775', 'o') },
            { '\u2775', ('\u2774', 'c') },
            { '\u27C5', ('\u27C6', 'o') },
            { '\u27C6', ('\u27C5', 'c') },
            { '\u27E6', ('\u27E7', 'o') },
            { '\u27E7', ('\u27E6', 'c') },
            { '\u27E8', ('\u27E9', 'o') },
            { '\u27E9', ('\u27E8', 'c') },
            { '\u27EA', ('\u27EB', 'o') },
            { '\u27EB', ('\u27EA', 'c') },
            { '\u27EC', ('\u27ED', 'o') },
            { '\u27ED', ('\u27EC', 'c') },
            { '\u27EE', ('\u27EF', 'o') },
            { '\u27EF', ('\u27EE', 'c') },
            { '\u2983', ('\u2984', 'o') },
            { '\u2984', ('\u2983', 'c') },
            { '\u2985', ('\u2986', 'o') },
            { '\u2986', ('\u2985', 'c') },
            { '\u2987', ('\u2988', 'o') },
            { '\u2988', ('\u2987', 'c') },
            { '\u2989', ('\u298A', 'o') },
            { '\u298A', ('\u2989', 'c') },
            { '\u298B', ('\u298C', 'o') },
            { '\u298C', ('\u298B', 'c') },
            { '\u298D', ('\u2990', 'o') },
            { '\u298E', ('\u298F', 'c') },
            { '\u298F', ('\u298E', 'o') },
            { '\u2990', ('\u298D', 'c') },
            { '\u2991', ('\u2992', 'o') },
            { '\u2992', ('\u2991', 'c') },
            { '\u2993', ('\u2994', 'o') },
            { '\u2994', ('\u2993', 'c') },
            { '\u2995', ('\u2996', 'o') },
            { '\u2996', ('\u2995', 'c') },
            { '\u2997', ('\u2998', 'o') },
            { '\u2998', ('\u2997', 'c') },
            { '\u29D8', ('\u29D9', 'o') },
            { '\u29D9', ('\u29D8', 'c') },
            { '\u29DA', ('\u29DB', 'o') },
            { '\u29DB', ('\u29DA', 'c') },
            { '\u29FC', ('\u29FD', 'o') },
            { '\u29FD', ('\u29FC', 'c') },
            { '\u2E22', ('\u2E23', 'o') },
            { '\u2E23', ('\u2E22', 'c') },
            { '\u2E24', ('\u2E25', 'o') },
            { '\u2E25', ('\u2E24', 'c') },
            { '\u2E26', ('\u2E27', 'o') },
            { '\u2E27', ('\u2E26', 'c') },
            { '\u2E28', ('\u2E29', 'o') },
            { '\u2E29', ('\u2E28', 'c') },
            { '\u2E55', ('\u2E56', 'o') },
            { '\u2E56', ('\u2E55', 'c') },
            { '\u2E57', ('\u2E58', 'o') },
            { '\u2E58', ('\u2E57', 'c') },
            { '\u2E59', ('\u2E5A', 'o') },
            { '\u2E5A', ('\u2E59', 'c') },
            { '\u2E5B', ('\u2E5C', 'o') },
            { '\u2E5C', ('\u2E5B', 'c') },
            { '\u3008', ('\u3009', 'o') },
            { '\u3009', ('\u3008', 'c') },
            { '\u300A', ('\u300B', 'o') },
            { '\u300B', ('\u300A', 'c') },
            { '\u300C', ('\u300D', 'o') },
            { '\u300D', ('\u300C', 'c') },
            { '\u300E', ('\u300F', 'o') },
            { '\u300F', ('\u300E', 'c') },
            { '\u3010', ('\u3011', 'o') },
            { '\u3011', ('\u3010', 'c') },
            { '\u3014', ('\u3015', 'o') },
            { '\u3015', ('\u3014', 'c') },
            { '\u3016', ('\u3017', 'o') },
            { '\u3017', ('\u3016', 'c') },
            { '\u3018', ('\u3019', 'o') },
            { '\u3019', ('\u3018', 'c') },
            { '\u301A', ('\u301B', 'o') },
            { '\u301B', ('\u301A', 'c') },
            { '\uFE59', ('\uFE5A', 'o') },
            { '\uFE5A', ('\uFE59', 'c') },
            { '\uFE5B', ('\uFE5C', 'o') },
            { '\uFE5C', ('\uFE5B', 'c') },
            { '\uFE5D', ('\uFE5E', 'o') },
            { '\uFE5E', ('\uFE5D', 'c') },
            { '\uFF08', ('\uFF09', 'o') },
            { '\uFF09', ('\uFF08', 'c') },
            { '\uFF3B', ('\uFF3D', 'o') },
            { '\uFF3D', ('\uFF3B', 'c') },
            { '\uFF5B', ('\uFF5D', 'o') },
            { '\uFF5D', ('\uFF5B', 'c') },
            { '\uFF5F', ('\uFF60', 'o') },
            { '\uFF60', ('\uFF5F', 'c') },
            { '\uFF62', ('\uFF63', 'o') },
            { '\uFF63', ('\uFF62', 'c') },
        };

        internal static bool IsBracket(char c, out (char reverse, char type) result) =>
            bracketType.TryGetValue(c, out result);

        internal static char GetBracket(char c) =>
            bracketType.TryGetValue(c, out var result) ? result.Item1 : '\0';
    }
}
