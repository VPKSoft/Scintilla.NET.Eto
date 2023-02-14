using ScintillaNet.Abstractions.Interfaces.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// Represents a selection when there are multiple active selections in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class SelectionBase : IScintillaSelection
{
    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }


    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    protected IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets or sets the anchor position of the selection.
    /// </summary>
    /// <returns>The zero-based document position of the selection anchor.</returns>
    public virtual int Anchor
    {
        get
        {
            var pos = ScintillaApi.DirectMessage(SCI_GETSELECTIONNANCHOR, new IntPtr(Index)).ToInt32();
            if (pos <= 0)
            {
                return pos;
            }

            return LineCollectionGeneral.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = LineCollectionGeneral.CharToBytePosition(value);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNANCHOR, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets the amount of anchor virtual space.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the selection anchor.</returns>
    public virtual int AnchorVirtualSpace
    {
        get => ScintillaApi.DirectMessage(SCI_GETSELECTIONNANCHORVIRTUALSPACE, new IntPtr(Index)).ToInt32();
        set
        {
            value = HelpersGeneral.ClampMin(value, 0);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNANCHORVIRTUALSPACE, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets the caret position of the selection.
    /// </summary>
    /// <returns>The zero-based document position of the selection caret.</returns>
    public virtual int Caret
    {
        get
        {
            var pos = ScintillaApi.DirectMessage(SCI_GETSELECTIONNCARET, new IntPtr(Index)).ToInt32();
            if (pos <= 0)
            {
                return pos;
            }

            return LineCollectionGeneral.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = LineCollectionGeneral.CharToBytePosition(value);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNCARET, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets the amount of caret virtual space.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the selection caret.</returns>
    public virtual int CaretVirtualSpace
    {
        get => ScintillaApi.DirectMessage(SCI_GETSELECTIONNCARETVIRTUALSPACE, new IntPtr(Index)).ToInt32();
        set
        {
            value = HelpersGeneral.ClampMin(value, 0);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNCARETVIRTUALSPACE, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets the end position of the selection.
    /// </summary>
    /// <returns>The zero-based document position where the selection ends.</returns>
    public virtual int End
    {
        get
        {
            var pos = ScintillaApi.DirectMessage(SCI_GETSELECTIONNEND, new IntPtr(Index)).ToInt32();
            if (pos <= 0)
            {
                return pos;
            }

            return LineCollectionGeneral.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = LineCollectionGeneral.CharToBytePosition(value);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNEND, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets the selection index.
    /// </summary>
    /// <returns>The zero-based selection index within the <see cref="SelectionCollectionBase{TSelection}" /> that created it.</returns>
    public int Index { get; private set; }

    /// <summary>
    /// Gets or sets the start position of the selection.
    /// </summary>
    /// <returns>The zero-based document position where the selection starts.</returns>
    public virtual int Start
    {
        get
        {
            var pos = ScintillaApi.DirectMessage(SCI_GETSELECTIONNSTART, new IntPtr(Index)).ToInt32();
            if (pos <= 0)
            {
                return pos;
            }

            return LineCollectionGeneral.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = LineCollectionGeneral.CharToBytePosition(value);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNSTART, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this selection.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="index">The index of this selection within the <see cref="SelectionCollectionBase{TSelection}" /> that created it.</param>
    protected SelectionBase(IScintillaApi scintilla, IScintillaLineCollectionGeneral lineCollectionGeneral, int index)
    {
        ScintillaApi = scintilla;
        LineCollectionGeneral = lineCollectionGeneral;
        Index = index;
    }
}