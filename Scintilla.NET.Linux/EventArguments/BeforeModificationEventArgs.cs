﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Linux.Collections;
using Color = Gdk.Color;
using Selection = Scintilla.NET.Linux.Collections.Selection;
using Style = Scintilla.NET.Linux.Collections.Style;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.BeforeInsert" /> and <see cref="Scintilla.BeforeDelete" /> events.
/// </summary>
public class BeforeModificationEventArgs : BeforeModificationEventArgsBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BeforeModificationEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="source">The source of the modification.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text is being modified.</param>
    /// <param name="byteLength">The length in bytes of the text being modified.</param>
    /// <param name="text">A pointer to the text being inserted.</param>
    public BeforeModificationEventArgs(
        IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection,
            SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image,
            Color> scintilla, ModificationSource source, int bytePosition, int byteLength, nint text) : base(
        scintilla, source, bytePosition, byteLength, text)
    {
    }
}