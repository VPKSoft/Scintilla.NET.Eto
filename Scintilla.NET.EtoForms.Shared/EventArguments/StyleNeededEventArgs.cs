﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.StyleNeeded" /> event.
/// </summary>
public class StyleNeededEventArgs : StyleNeededEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleNeededEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document to stop styling.</param>
    public StyleNeededEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition) : base(scintilla, lineCollectionGeneral, bytePosition)
    {
    }
}