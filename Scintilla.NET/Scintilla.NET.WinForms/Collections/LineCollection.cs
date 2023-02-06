using System.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;
using Scintilla.NET.Abstractions.Structs;
using Scintilla.NET.Abstractions.UtilityClasses;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.WinForms.Collections;

/// <summary>
/// An immutable collection of lines of text in a <see cref="Scintilla" /> control.
/// </summary>
public class LineCollection : LineCollectionBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color>
{
    #region Methods    
    /// <summary>
    /// A method to be added as event subscription to <see cref="E:Scintilla.NET.Abstractions.Interfaces.IScintillaEvents`32.SCNotification" /> event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The <see cref="T:Scintilla.NET.Abstractions.Interfaces.ISCNotificationEventArgs" /> instance containing the event data.</param>
    public override void ScNotificationCallback(object sender, ISCNotificationEventArgs e)
    {
        var scn = e.SCNotification;
        switch (scn.nmhdr.code)
        {
            case SCN_MODIFIED:
                ScnModified(scn);
                break;
        }
    }
    #endregion Methods

    #region Properties
    /// <summary>
    /// Gets the <see cref="Line" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="Line" /> to get.</param>
    /// <returns>The <see cref="Line" /> at the specified index.</returns>
    public override Line this[int index]
    {
        get
        {
            index = Helpers.Clamp(index, 0, Count - 1);
            return new Line(ScintillaApi, index);
        }
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LineCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public LineCollection(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color> scintilla) : base(scintilla)
    {
        perLineData = new GapBuffer<PerLine>
        {
            new() { Start = 0 },
            new() { Start = 0 }, // Terminal
        };
    }

    #endregion Constructors
}