using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// An immutable collection of indicators in a <see cref="Scintilla" /> control.
/// </summary>
public class IndicatorCollection: IndicatorCollectionBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>, IEnumerable<Indicator>
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Gets an <see cref="Indicator" /> object at the specified index.
    /// </summary>
    /// <param name="index">The indicator index.</param>
    /// <returns>An object representing the indicator at the specified <paramref name="index" />.</returns>
    /// <remarks>
    /// Indicators 0 through 7 are used by lexers.
    /// Indicators 32 through 35 are used for IME.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Indicator this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Indicator(scintilla, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public IndicatorCollection(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla) : base(scintilla)
    {
    }
}