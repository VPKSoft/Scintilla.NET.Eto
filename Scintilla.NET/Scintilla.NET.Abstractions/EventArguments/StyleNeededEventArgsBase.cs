﻿using System.Collections;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.EventArguments.Base;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.StyleNeeded" /> event.
/// </summary>
public abstract class StyleNeededEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> 
    : ScintillaEventArgs<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>,
        IStyleNeededEventArgs<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
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
    private readonly int bytePosition;
    private int? position;

    /// <summary>
    /// Gets the document position where styling should end. The <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.GetEndStyled" /> method
    /// indicates the last position styled correctly and the starting place for where styling should begin.
    /// </summary>
    /// <returns>The zero-based position within the document to perform styling up to.</returns>
    public virtual int Position
    {
        get
        {
            position ??= ScintillaApi.Lines.ByteToCharPosition(bytePosition);

            return (int)position;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleNeededEventArgsBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="bytePosition">The zero-based byte position within the document to stop styling.</param>
    protected StyleNeededEventArgsBase(
        IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle,
            TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> scintilla, int bytePosition) : base(scintilla)
    {
        this.bytePosition = bytePosition;
    }
}