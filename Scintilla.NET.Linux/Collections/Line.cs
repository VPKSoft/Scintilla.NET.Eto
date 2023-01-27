﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// Represents a line of text in a <see cref="Scintilla" /> control.
/// </summary>
public class Line : LineBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Line" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this line.</param>
    /// <param name="index">The index of this line within the <see cref="LineCollection" /> that created it.</param>
    public Line(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}