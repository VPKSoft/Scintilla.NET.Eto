﻿using ScintillaNet.Abstractions.EventArguments;

namespace ScintillaNet.EtoForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEtoFormsEvents.CharAdded" /> event.
/// </summary>
public class CharAddedEventArgs : CharAddedEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CharAddedEventArgs" /> class.
    /// </summary>
    /// <param name="ch">The character added.</param>
    public CharAddedEventArgs(int ch) : base(ch)
    {
    }
}