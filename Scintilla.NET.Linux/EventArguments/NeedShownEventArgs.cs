using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Linux.Collections;
using Color = Gdk.Color;
using Selection = Scintilla.NET.Linux.Collections.Selection;
using Style = Scintilla.NET.Linux.Collections.Style;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.NeedShown" /> event.
/// </summary>
public class NeedShownEventArgs : NeedShownEventArgsBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NeedShownEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text needs to be shown.</param>
    /// <param name="byteLength">The length in bytes of the text that needs to be shown.</param>
    public NeedShownEventArgs(
        IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection,
            SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image,
            Color> scintilla, int bytePosition, int byteLength) : base(scintilla, bytePosition, byteLength)
    {
    }
}