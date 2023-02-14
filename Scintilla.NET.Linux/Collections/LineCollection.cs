using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments;
using ScintillaNet.Abstractions.Structs;
using ScintillaNet.Abstractions.UtilityClasses;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Linux.Collections;

/// <summary>
/// An immutable collection of lines of text in a <see cref="Scintilla" /> control.
/// </summary>
public class LineCollection : LineCollectionBase<Line>
{
    /// <summary>
    /// Gets the style collection general members.
    /// </summary>
    /// <value>The style collection  general members.</value>
    private IScintillaStyleCollectionGeneral StyleCollectionGeneral { get; }

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    private IScintillaMarkerCollectionGeneral MarkerCollectionGeneral { get; }

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
            return new Line(ScintillaApi, StyleCollectionGeneral, this, MarkerCollectionGeneral, index);
        }
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LineCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    /// <param name="styleCollectionGeneral">A reference to Scintilla's style collection.</param>
    /// <param name="markerCollectionGeneral">A reference to Scintilla's marker collection.</param>
    public LineCollection(
        IScintillaApi scintilla,
        IScintillaStyleCollectionGeneral styleCollectionGeneral, 
        IScintillaMarkerCollectionGeneral markerCollectionGeneral
        ) : base(scintilla)
    {
        StyleCollectionGeneral = styleCollectionGeneral;
        MarkerCollectionGeneral = markerCollectionGeneral;
        PerLineData = new GapBuffer<PerLine>
        {
            new() { Start = 0, },
            new() { Start = 0, }, // Terminal
        };
    }

    #endregion Constructors
}