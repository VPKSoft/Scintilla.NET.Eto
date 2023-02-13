﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.BeforeInsert" /> and <see cref="WinForms.Scintilla.BeforeDelete" /> events.
/// </summary>
public class BeforeModificationEventArgs : BeforeModificationEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BeforeModificationEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="source">The source of the modification.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text is being modified.</param>
    /// <param name="byteLength">The length in bytes of the text being modified.</param>
    /// <param name="text">A pointer to the text being inserted.</param>
    public BeforeModificationEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        ModificationSource source, 
        int bytePosition, 
        int byteLength, 
        nint text) : base(scintilla, lineCollectionGeneral, source, bytePosition, byteLength, text)
    {
    }
}