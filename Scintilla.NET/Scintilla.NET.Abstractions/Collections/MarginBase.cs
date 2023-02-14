using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// Represents a margin displayed on the left edge of a <see cref="Scintilla" /> control.
/// </summary>
public abstract class MarginBase<TColor> : IScintillaMargin<TColor>
    where TColor : struct
{
    #region Properties

    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }


    /// <summary>
    /// Gets or sets the background color of the margin when the <see cref="Type" /> property is set to <see cref="MarginType.Color" />.
    /// </summary>
    /// <returns>A Color object representing the margin background color. The default is Black.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    public abstract TColor BackColor { get; set; }

    /// <summary>
    /// Gets or sets the mouse cursor style when over the margin.
    /// </summary>
    /// <returns>One of the <see cref="MarginCursor" /> enumeration values. The default is <see cref="MarginCursor.Arrow" />.</returns>
    public virtual MarginCursor Cursor
    {
        get => (MarginCursor)ScintillaApi.DirectMessage(SCI_GETMARGINCURSORN, new IntPtr(Index));
        set
        {
            var cursor = (int)value;
            ScintillaApi.DirectMessage(SCI_SETMARGINCURSORN, new IntPtr(Index), new IntPtr(cursor));
        }
    }

    /// <summary>
    /// Gets the zero-based margin index this object represents.
    /// </summary>
    /// <returns>The margin index within the <see cref="IScintillaCollectionProperties{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TImage,TColor}.Margins" />.</returns>
    public virtual int Index { get; private set; }

    /// <summary>
    /// Gets or sets whether the margin is sensitive to mouse clicks.
    /// </summary>
    /// <returns>true if the margin is sensitive to mouse clicks; otherwise, false. The default is false.</returns>
    public virtual bool Sensitive
    {
        get => ScintillaApi.DirectMessage(SCI_GETMARGINSENSITIVEN, new IntPtr(Index)) != IntPtr.Zero;
        set
        {
            var sensitive = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(SCI_SETMARGINSENSITIVEN, new IntPtr(Index), sensitive);
        }
    }

    /// <summary>
    /// Gets or sets the margin type.
    /// </summary>
    /// <returns>One of the <see cref="MarginType" /> enumeration values. The default is <see cref="MarginType.Symbol" />.</returns>
    public virtual MarginType Type
    {
        get => (MarginType)ScintillaApi.DirectMessage(SCI_GETMARGINTYPEN, new IntPtr(Index));
        set
        {
            var type = (int)value;
            ScintillaApi.DirectMessage(SCI_SETMARGINTYPEN, new IntPtr(Index), new IntPtr(type));
        }
    }

    /// <summary>
    /// Gets or sets the width in pixels of the margin.
    /// </summary>
    /// <returns>The width of the margin measured in pixels.</returns>
    /// <remarks>Scintilla assigns various default widths.</remarks>
    public virtual int Width
    {
        get => ScintillaApi.DirectMessage(SCI_GETMARGINWIDTHN, new IntPtr(Index)).ToInt32();
        set
        {
            value = HelpersGeneral.ClampMin(value, 0);
            ScintillaApi.DirectMessage(SCI_SETMARGINWIDTHN, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets a mask indicating which markers this margin can display.
    /// </summary>
    /// <returns>
    /// An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="IScintillaMargin{TColor}" /> indexes.
    /// The default is 0x1FFFFFF, which is every marker except folder markers (i.e. 0 through 24).
    /// </returns>
    /// <remarks>
    /// For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).
    /// <see cref="MarkerConstants.MaskFolders" /> is a useful constant for working with just folder margin indexes.
    /// </remarks>
    public virtual uint Mask
    {
        get => unchecked((uint)ScintillaApi.DirectMessage(SCI_GETMARGINMASKN, new IntPtr(Index)).ToInt32());
        set
        {
            var mask = unchecked((int)value);
            ScintillaApi.DirectMessage(SCI_SETMARGINMASKN, new IntPtr(Index), new IntPtr(mask));
        }
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MarginBase{TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this margin.</param>
    /// <param name="index">The index of this margin within the <see cref="IScintillaMarginCollection{TMargin,TColor}" /> that created it.</param>
    protected MarginBase(IScintillaApi scintilla, int index)
    {
        this.ScintillaApi = scintilla;
        Index = index;
    }

    #endregion Constructors
}