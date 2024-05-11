// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.


// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;

namespace Textify.Figlet;

/// <summary>
/// Type for exceptions raised by Figlet.
/// </summary>
public sealed class FigletException : Exception
{
    /// <summary>
    /// Constructs a new Figlet exception.
    /// </summary>
    /// <param name="message">A message explaining the exception.</param>
    public FigletException(string message) : base(message)
    { }
}
