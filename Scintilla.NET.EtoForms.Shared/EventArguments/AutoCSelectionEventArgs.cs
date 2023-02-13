﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.EtoForms.Shared.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.AutoCSelection" /> event.
/// </summary>
public class AutoCSelectionEventArgs : AutoCSelectionEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoCSelectionEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document of the word being completed.</param>
    /// <param name="text">A pointer to the selected auto-completion text.</param>
    /// <param name="ch">The character that caused the completion.</param>
    /// <param name="listCompletionMethod">A value indicating the way in which the completion occurred.</param>
    public AutoCSelectionEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, 
        nint text, 
        int ch, 
        ListCompletionMethod listCompletionMethod) : base(scintilla, lineCollectionGeneral, bytePosition, text, ch, listCompletionMethod)
    {
    }
}