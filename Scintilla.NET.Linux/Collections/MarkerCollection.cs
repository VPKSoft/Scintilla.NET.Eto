﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// An immutable collection of markers in a <see cref="Scintilla" /> control.
/// </summary>
public class MarkerCollection : MarkerCollectionBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{
    /// <summary>
    /// Gets a <see cref="Marker" /> object at the specified index.
    /// </summary>
    /// <param name="index">The marker index.</param>
    /// <returns>An object representing the marker at the specified <paramref name="index" />.</returns>
    /// <remarks>Markers 25 through 31 are used by Scintilla for folding.</remarks>
    protected override Marker this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Marker(scintilla, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkerCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public MarkerCollection(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla) : base(scintilla)
    {

    }
}