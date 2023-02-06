using System.Collections;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Collections;

/// <summary>
/// Represents a selection when there are multiple active selections in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> : 
    IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections : SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMarker: MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap: class
    where TColor: struct
{
    /// <inheritdoc />
    public IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> ScintillaApi
    {
        get;
    }

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

            return ScintillaApi.Lines.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = ScintillaApi.Lines.CharToBytePosition(value);
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

            return ScintillaApi.Lines.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = ScintillaApi.Lines.CharToBytePosition(value);
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

            return ScintillaApi.Lines.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = ScintillaApi.Lines.CharToBytePosition(value);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNEND, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets the selection index.
    /// </summary>
    /// <returns>The zero-based selection index within the <see cref="SelectionCollectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> that created it.</returns>
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

            return ScintillaApi.Lines.ByteToCharPosition(pos);
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.TextLength);
            value = ScintillaApi.Lines.CharToBytePosition(value);
            ScintillaApi.DirectMessage(SCI_SETSELECTIONNSTART, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this selection.</param>
    /// <param name="index">The index of this selection within the <see cref="SelectionCollectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> that created it.</param>
    protected SelectionBase(IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> scintilla, int index)
    {
        this.ScintillaApi = scintilla;
        Index = index;
    }
}