using System.ComponentModel;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// An immutable collection of margins in a <see cref="Scintilla" /> control.
/// </summary>
public class MarginCollection : MarginCollectionBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{
    /// <summary>
    /// Gets a <see cref="Margin" /> object at the specified index.
    /// </summary>
    /// <param name="index">The margin index.</param>
    /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
    /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Margin this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Margin(scintilla, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarginCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public MarginCollection(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla) : base(scintilla)
    {
    }
}