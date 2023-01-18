using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.EtoForms.Shared.Collections;

namespace Scintilla.NET.EtoForms.Shared.EventArgs;

/// <summary>
/// Provides data for the <see cref="Scintilla.StyleNeeded" /> event.
/// </summary>
public class StyleNeededEventArgs : StyleNeededEventArgsBase<MarkerCollection, StyleCollection, IndicatorCollection, Collections.LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleNeededEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="bytePosition">The zero-based byte position within the document to stop styling.</param>
    public StyleNeededEventArgs(
        IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, Collections.LineCollection, MarginCollection,
            SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap,
            Color> scintilla, int bytePosition) : base(scintilla, bytePosition)
    {
    }
}