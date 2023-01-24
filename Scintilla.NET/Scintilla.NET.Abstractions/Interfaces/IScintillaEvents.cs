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
using System.ComponentModel;
using System.Net.Mime;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
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
/// <typeparam name="TScNotificationEventArgs">The type of the TSC notification event arguments.</typeparam>
public interface IScintillaEvents<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
    TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys,
    TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs,
    TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs,
    TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs,
    TStyleNeededEventArgs, TUpdateUiEventArgs, TScNotificationEventArgs>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs,
        TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs,
        TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :
    IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker
        , TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs,
        TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections :
    SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TEventArgs : EventArgs
    where TMarker : MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle,
        TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap : class
    where TColor : struct
    where TKeys : Enum
    where TAutoCSelectionEventArgs : AutoCSelectionEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBeforeModificationEventArgs: BeforeModificationEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TChangeAnnotationEventArgs: ChangeAnnotationEventArgsBase
    where TCharAddedEventArgs: CharAddedEventArgsBase
    where TDoubleClickEventArgs: DoubleClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TDwellEventArgs: DwellEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TCallTipClickEventArgs: CallTipClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where THotspotClickEventArgs: HotspotClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TIndicatorClickEventArgs: IndicatorClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TIndicatorReleaseEventArgs: IndicatorReleaseEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TInsertCheckEventArgs: InsertCheckEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMarginClickEventArgs: MarginClickEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor, TKeys>
    where TNeedShownEventArgs: NeedShownEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyleNeededEventArgs: StyleNeededEventArgsBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker,
        TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TUpdateUiEventArgs: UpdateUIEventArgsBase
    where TScNotificationEventArgs: SCNotificationEventArgsBase
{
    /// <summary>
    /// Occurs when an auto-completion list is cancelled.
    /// </summary>
    event EventHandler<EventArgs> AutoCCancelled;

    /// <summary>
    /// Occurs when the user deletes a character while an auto-completion list is active.
    /// </summary>
    event EventHandler<EventArgs> AutoCCharDeleted;

    /// <summary>
    /// Occurs after autocompleted text is inserted.
    /// </summary>
    event EventHandler<TAutoCSelectionEventArgs> AutoCCompleted;

    /// <summary>
    /// Occurs when a user has selected an item in an auto-completion list.
    /// </summary>
    /// <remarks>Automatic insertion can be cancelled by calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.AutoCCancel" /> from the event handler.</remarks>
    event EventHandler<TAutoCSelectionEventArgs> AutoCSelection;

    /// <summary>
    /// Occurs when text is about to be deleted.
    /// </summary>
    event EventHandler<TBeforeModificationEventArgs> BeforeDelete;

    /// <summary>
    /// Occurs when text is about to be inserted.
    /// </summary>
    event EventHandler<TBeforeModificationEventArgs> BeforeInsert;

    /// <summary>
    /// Occurs when an annotation has changed.
    /// </summary>
    event EventHandler<TChangeAnnotationEventArgs> ChangeAnnotation;

    /// <summary>
    /// Occurs when the user enters a text character.
    /// </summary>
    event EventHandler<TCharAddedEventArgs> CharAdded;

    /// <summary>
    /// Occurs when text has been deleted from the document.
    /// </summary>
    event EventHandler<TBeforeModificationEventArgs> Delete;

    /// <summary>
    /// Occurs when the <see cref="Scintilla" /> control is double-clicked.
    /// </summary>
    event EventHandler<TDoubleClickEventArgs> DoubleClick;

    /// <summary>
    /// Occurs when the mouse moves or another activity such as a key press ends a <see cref="DwellStart" /> event.
    /// </summary>
    event EventHandler<TDwellEventArgs> DwellEnd;

    /// <summary>
    /// Occurs when the mouse clicked over a call tip displayed by the <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.CallTipShow" /> method.
    /// </summary>
    event EventHandler<TCallTipClickEventArgs> CallTipClick;

    /// <summary>
    /// Occurs when the mouse is kept in one position (hovers) for the <see cref="IScintillaProperties{TColor}.MouseDwellTime" />.
    /// </summary>
    event EventHandler<TDwellEventArgs> DwellStart;

    /// <summary>
    /// Occurs when the user clicks on text that is in a style with the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Hotspot" /> property set.
    /// </summary>
    event EventHandler<THotspotClickEventArgs> HotspotClick;

    /// <summary>
    /// Occurs when the user double clicks on text that is in a style with the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Hotspot" /> property set.
    /// </summary>
    event EventHandler<THotspotClickEventArgs>
        HotspotDoubleClick;

    /// <summary>
    /// Occurs when the user releases a click on text that is in a style with the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Hotspot" /> property set.
    /// </summary>
    event EventHandler<THotspotClickEventArgs> HotspotReleaseClick;

    /// <summary>
    /// Occurs when the user clicks on text that has an indicator.
    /// </summary>
    event EventHandler<TIndicatorClickEventArgs> IndicatorClick;

    /// <summary>
    /// Occurs when the user releases a click on text that has an indicator.
    /// </summary>
    event EventHandler<TIndicatorReleaseEventArgs> IndicatorRelease;

    /// <summary>
    /// Occurs when text has been inserted into the document.
    /// </summary>
    event EventHandler<TBeforeModificationEventArgs> Insert;

    /// <summary>
    /// Occurs when text is about to be inserted. The inserted text can be changed.
    /// </summary>
    event EventHandler<TInsertCheckEventArgs> InsertCheck;

    /// <summary>
    /// Occurs when the mouse was clicked inside a margin that was marked as sensitive.
    /// </summary>
    /// <remarks>The <see cref="MarginBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Sensitive" /> property must be set for a margin to raise this event.</remarks>
    event EventHandler<TMarginClickEventArgs> MarginClick;

    /// <summary>
    /// Occurs when the mouse was right-clicked inside a margin that was marked as sensitive.
    /// </summary>
    /// <remarks>The <see cref="MarginBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Sensitive" /> property and <see cref="MediaTypeNames.Text" /> must be set for a margin to raise this event.</remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.UsePopup(Scintilla.NET.Abstractions.Enumerations.PopupMode)" />
    event EventHandler<TMarginClickEventArgs> MarginRightClick;

    /// <summary>
    /// Occurs when a user attempts to change text while the document is in read-only mode.
    /// </summary>
    /// <seealso cref="IScintillaProperties{TColor}.ReadOnly" />
    event EventHandler<EventArgs> ModifyAttempt;

    /// <summary>
    /// Occurs when the control determines hidden text needs to be shown.
    /// </summary>
    /// <remarks>An example of when this event might be raised is if the end of line of a contracted fold point is deleted.</remarks>
    event EventHandler<TNeedShownEventArgs> NeedShown;

    /// <summary>
    /// Occurs on native Scintilla API signal.
    /// </summary>
    // ReSharper disable once InconsistentNaming, API event.
    event EventHandler<TScNotificationEventArgs> SCNotification;

    /// <summary>
    /// Occurs when painting has just been done.
    /// </summary>
    event EventHandler<EventArgs> Painted;

    /// <summary>
    /// Occurs when the document becomes 'dirty'.
    /// </summary>
    /// <remarks>The document 'dirty' state can be checked with the <see cref="IScintillaProperties{TColor}.Modified" /> property and reset by calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetSavePoint" />.</remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetSavePoint" />
    /// <seealso cref="SavePointReached" />
    event EventHandler<EventArgs> SavePointLeft;

    /// <summary>
    /// Occurs when the document 'dirty' flag is reset.
    /// </summary>
    /// <remarks>The document 'dirty' state can be reset by calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetSavePoint" /> or undoing an action that modified the document.</remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetSavePoint" />
    /// <seealso cref="SavePointLeft" />
    event EventHandler<EventArgs> SavePointReached;

    /// <summary>
    /// Occurs when the control is about to display or print text and requires styling.
    /// </summary>
    /// <remarks>
    /// This event is only raised when <see cref="Lexer" /> is set to <see cref="Container" />.
    /// The last position styled correctly can be determined by calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.GetEndStyled" />.
    /// </remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.GetEndStyled" />
    event EventHandler<TStyleNeededEventArgs> StyleNeeded;

    /// <summary>
    /// Occurs when the control UI is updated as a result of changes to text (including styling),
    /// selection, and/or scroll positions.
    /// </summary>
    event EventHandler<TUpdateUiEventArgs> UpdateUi;

    /// <summary>
    /// Occurs when the user zooms the display using the keyboard or the <see cref="IScintillaProperties{TColor}.Zoom" /> property is changed.
    /// </summary>
    event EventHandler<EventArgs> ZoomChanged;
}