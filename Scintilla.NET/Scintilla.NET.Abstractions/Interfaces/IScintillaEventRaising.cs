#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Collections;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.EventArguments;

namespace Scintilla.NET.Abstractions.Interfaces;

/// <summary>
/// The events of the Scintilla API.
/// </summary>
/// <typeparam name="TMarkers">The type of the markers collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TStyles">The type of the styles collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TIndicators">The type of the indicators collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TLines">The type of the lines collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TMargins">The type of the margins collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TSelections">The type of the selections collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TEventArgs">The type of the Scintilla notification event handler <see cref="EventArgs"/> descendant implementation.</typeparam>
/// <typeparam name="TMarker">The type of the item in the <typeparamref name="TMarkers"/> collection.</typeparam>
/// <typeparam name="TStyle">The type of the item in the <typeparamref name="TStyles"/> collection.</typeparam>
/// <typeparam name="TIndicator">The type of the item in the <typeparamref name="TIndicators"/> collection.</typeparam>
/// <typeparam name="TLine">The type of the item in the <typeparamref name="TLines"/> collection.</typeparam>
/// <typeparam name="TMargin">The type of the item in the <typeparamref name="TMargin"/> collection.</typeparam>
/// <typeparam name="TSelection">The type of the item in the <typeparamref name="TSelections"/> collection.</typeparam>
/// <typeparam name="TBitmap">The type of the bitmap used in the platform.</typeparam>
/// <typeparam name="TColor">The type of the color used in the platform.</typeparam>
/// <typeparam name="TKeys">The type of the keys enumeration used by the platform.</typeparam>
/// <typeparam name="TAutoCSelectionEventArgs">The type of the automatic code completion related event arguments.</typeparam>
/// <typeparam name="TBeforeModificationEventArgs">The type of the event arguments used in events before the Scintilla text is about to be changed.</typeparam>
/// <typeparam name="TModificationEventArgs">The type of the event arguments used in events before the Scintilla text was changed.</typeparam>
/// <typeparam name="TChangeAnnotationEventArgs">The type of the annotation change event arguments.</typeparam>
/// <typeparam name="TCharAddedEventArgs">The type of the character added event arguments.</typeparam>
/// <typeparam name="TDoubleClickEventArgs">The type of the double click event arguments.</typeparam>
/// <typeparam name="TDwellEventArgs">The type of the dwell start and dwell end event arguments.</typeparam>
/// <typeparam name="TCallTipClickEventArgs">The type of the t call tip click event arguments.</typeparam>
/// <typeparam name="THotspotClickEventArgs">The type of the t hotspot click event arguments.</typeparam>
/// <typeparam name="TIndicatorClickEventArgs">The type of the t indicator click event arguments.</typeparam>
/// <typeparam name="TIndicatorReleaseEventArgs">The type of the t indicator release event arguments.</typeparam>
/// <typeparam name="TInsertCheckEventArgs">The type of the t insert check event arguments.</typeparam>
/// <typeparam name="TMarginClickEventArgs">The type of the t margin click event arguments.</typeparam>
/// <typeparam name="TNeedShownEventArgs">The type of the t need shown event arguments.</typeparam>
/// <typeparam name="TStyleNeededEventArgs">The type of the t style needed event arguments.</typeparam>
/// <typeparam name="TUpdateUiEventArgs">The type of the t update UI event arguments.</typeparam>
public interface IScintillaEventRaising<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, in TEventArgs,
    TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys, in TAutoCSelectionEventArgs, 
    in TBeforeModificationEventArgs, in TModificationEventArgs, in TChangeAnnotationEventArgs, in TCharAddedEventArgs, 
    in TDoubleClickEventArgs, in TDwellEventArgs, in TCallTipClickEventArgs, in THotspotClickEventArgs, 
    in TIndicatorClickEventArgs, in TIndicatorReleaseEventArgs, in TInsertCheckEventArgs, in TMarginClickEventArgs, 
    in TNeedShownEventArgs, in TStyleNeededEventArgs, in TUpdateUiEventArgs>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections,
        TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :
    IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker
        , TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections,
        TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections :
    SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TEventArgs : EventArgs
    where TMarker : MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap : class
    where TColor : struct
    where TKeys : Enum
    where TAutoCSelectionEventArgs : AutoCSelectionEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins,
        TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBeforeModificationEventArgs : BeforeModificationEventArgsBase<TMarkers, TStyles, TIndicators, TLines,
        TMargins, TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TModificationEventArgs : ModificationEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins,
        TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TChangeAnnotationEventArgs : ChangeAnnotationEventArgsBase
    where TCharAddedEventArgs : CharAddedEventArgsBase
    where TDoubleClickEventArgs : DoubleClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections
        , TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TDwellEventArgs : DwellEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TCallTipClickEventArgs : CallTipClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins,
        TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where THotspotClickEventArgs : HotspotClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins,
        TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TIndicatorClickEventArgs : IndicatorClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins,
        TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TIndicatorReleaseEventArgs : IndicatorReleaseEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins,
        TSelections, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TInsertCheckEventArgs : InsertCheckEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections
        , TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMarginClickEventArgs : MarginClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections
        , TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TNeedShownEventArgs : NeedShownEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections,
        TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyleNeededEventArgs : StyleNeededEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections
        , TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TUpdateUiEventArgs : UpdateUIEventArgsBase
{
    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.AutoCCancelled" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnAutoCCancelled(TEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.AutoCCharDeleted" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnAutoCCharDeleted(TEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.AutoCCompleted" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TAutoCSelectionEventArgs"/> that contains the event data.</param>
    void OnAutoCCompleted(TAutoCSelectionEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.AutoCSelection" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TAutoCSelectionEventArgs"/> that contains the event data.</param>
    void OnAutoCSelection(TAutoCSelectionEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.BeforeDelete" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TBeforeModificationEventArgs"/> that contains the event data.</param>
    void OnBeforeDelete(TBeforeModificationEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.BeforeInsert" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TBeforeModificationEventArgs"/> that contains the event data.</param>
    void OnBeforeInsert(TBeforeModificationEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.ChangeAnnotation" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TChangeAnnotationEventArgs"/> that contains the event data.</param>
    void OnChangeAnnotation(TChangeAnnotationEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.CharAdded" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TCharAddedEventArgs"/> that contains the event data.</param>
    void OnCharAdded(TCharAddedEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.Delete" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TModificationEventArgs"/> that contains the event data.</param>
    void OnDelete(TModificationEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.DoubleClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TDoubleClickEventArgs"/> that contains the event data.</param>
    void OnDoubleClick(TDoubleClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.DwellEnd" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TDwellEventArgs"/> that contains the event data.</param>
    void OnDwellEnd(TDwellEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.DwellStart" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TDwellEventArgs"/> that contains the event data.</param>
    void OnDwellStart(TDwellEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.CallTipClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TCallTipClickEventArgs"/> that contains the event data.</param>
    void OnCallTipClick(TCallTipClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.HotspotClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="THotspotClickEventArgs"/> that contains the event data.</param>
    void OnHotspotClick(THotspotClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.HotspotDoubleClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="THotspotClickEventArgs"/> that contains the event data.</param>
    void OnHotspotDoubleClick(THotspotClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.HotspotReleaseClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="THotspotClickEventArgs"/> that contains the event data.</param>
    void OnHotspotReleaseClick(THotspotClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.IndicatorClick" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TIndicatorClickEventArgs"/> that contains the event data.</param>
    void OnIndicatorClick(TIndicatorClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.IndicatorRelease" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TIndicatorReleaseEventArgs"/> that contains the event data.</param>
    void OnIndicatorRelease(TIndicatorReleaseEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.Insert" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TModificationEventArgs"/> that contains the event data.</param>
    void OnInsert(TModificationEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.InsertCheck" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TInsertCheckEventArgs"/> that contains the event data.</param>
    void OnInsertCheck(TInsertCheckEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.MarginClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TMarginClickEventArgs"/> that contains the event data.</param>
    void OnMarginClick(TMarginClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.MarginRightClick" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TMarginClickEventArgs"/> that contains the event data.</param>
    void OnMarginRightClick(TMarginClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.ModifyAttempt" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnModifyAttempt(TEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.NeedShown" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TNeedShownEventArgs"/> that contains the event data.</param>
    void OnNeedShown(TNeedShownEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.Painted" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnPainted(TEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.SavePointLeft" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnSavePointLeft(TEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.SavePointReached" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnSavePointReached(TEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.StyleNeeded" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TStyleNeededEventArgs"/> that contains the event data.</param>
    void OnStyleNeeded(TStyleNeededEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.UpdateUi" /> event.
    /// </summary>
    /// <param name="e">An <typeparamref name="TUpdateUiEventArgs"/> that contains the event data.</param>
    void OnUpdateUI(TUpdateUiEventArgs e);

    /// <summary>
    /// Raises the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.ZoomChanged" /> event.
    /// </summary>
    /// <param name="e">A <typeparamref name="TEventArgs"/> that contains the event data.</param>
    void OnZoomChanged(TEventArgs e);
}