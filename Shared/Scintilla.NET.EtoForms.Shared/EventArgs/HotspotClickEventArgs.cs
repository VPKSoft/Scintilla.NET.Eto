using Eto.Drawing;
using Eto.Forms;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.EtoForms.Shared.Collections;

namespace Scintilla.NET.EtoForms.Shared.EventArgs;

/// <summary>
/// Provides data for the <see cref="Scintilla.HotspotClick" />, <see cref="Scintilla.HotspotDoubleClick" />,
/// and <see cref="Scintilla.HotspotReleaseClick" /> events.
/// </summary>
public class HotspotClickEventArgs : HotspotClickEventArgsBase<MarkerCollection, StyleCollection, IndicatorCollection, Collections.LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color, Keys>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HotspotClickEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
    public HotspotClickEventArgs(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, Collections.LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color> scintilla, Keys modifiers, int bytePosition) : base(scintilla, modifiers, bytePosition)
    {
    }
}