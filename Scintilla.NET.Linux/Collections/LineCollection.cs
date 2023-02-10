using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;
using Scintilla.NET.Abstractions.Structs;
using Scintilla.NET.Abstractions.UtilityClasses;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// An immutable collection of lines of text in a <see cref="Scintilla" /> control.
/// </summary>
public class LineCollection : LineCollectionBase<Line>
{
    private readonly IScintillaStyleCollectionGeneral styleCollectionGeneral;
    private readonly IScintillaLineCollectionGeneral lineCollectionGeneral;
    private readonly IScintillaMarkerCollectionGeneral markerCollectionGeneral;

    #region Methods    
    /// <inheritdoc />
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
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Line(ScintillaApi, styleCollectionGeneral, lineCollectionGeneral, markerCollectionGeneral, index);
        }
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LineCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    /// <param name="styleCollectionGeneral">A reference to Scintilla's style collection.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="markerCollectionGeneral">A reference to Scintilla's marker collection.</param>
    public LineCollection(
        IScintillaApi scintilla,
        IScintillaStyleCollectionGeneral styleCollectionGeneral, 
        IScintillaLineCollectionGeneral lineCollectionGeneral, 
        IScintillaMarkerCollectionGeneral markerCollectionGeneral
        ) : base(scintilla)
    {
        this.styleCollectionGeneral = styleCollectionGeneral;
        this.lineCollectionGeneral = lineCollectionGeneral;
        this.markerCollectionGeneral = markerCollectionGeneral;
        PerLineData = new GapBuffer<PerLine>
        {
            new() { Start = 0, },
            new() { Start = 0, }, // Terminal
        };
    }

    #endregion Constructors
}