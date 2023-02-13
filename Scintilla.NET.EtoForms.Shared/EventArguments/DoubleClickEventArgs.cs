﻿using Eto.Forms;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.DoubleClick" /> event.
/// </summary>
public class DoubleClickEventArgs : DoubleClickEventArgsBase<Keys>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleClickEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the double click.</param>
    /// <param name="bytePosition">The zero-based byte position of the double clicked text.</param>
    /// <param name="line">The zero-based line index of the double clicked text.</param>
    public DoubleClickEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        Keys modifiers, 
        int bytePosition, 
        int line) : base(scintilla, lineCollectionGeneral, modifiers, bytePosition,
        line)
    {
    }
}