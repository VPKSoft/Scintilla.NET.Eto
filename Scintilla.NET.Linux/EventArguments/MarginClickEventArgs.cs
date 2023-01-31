using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Linux.Collections;
using Color = Gdk.Color;
using Key = Gdk.Key;
using Selection = Scintilla.NET.Linux.Collections.Selection;
using Style = Scintilla.NET.Linux.Collections.Style;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.MarginClick" /> event.
/// </summary>
public class MarginClickEventArgs : MarginClickEventArgsBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color, Key>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MarginClickEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the margin click.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where the line adjacent to the clicked margin starts.</param>
    /// <param name="margin">The zero-based index of the clicked margin.</param>
    public MarginClickEventArgs(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla, Key modifiers, int bytePosition, int margin) : base(scintilla, modifiers, bytePosition, margin)
    {
    }
}