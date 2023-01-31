﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// Represents a selection when there are multiple active selections in a <see cref="Scintilla" /> control.
/// </summary>
public class Selection : SelectionBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Selection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this selection.</param>
    /// <param name="index">The index of this selection within the <see cref="SelectionCollection" /> that created it.</param>
    public Selection(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla, int index) : base(scintilla, index)
    {
    }
}