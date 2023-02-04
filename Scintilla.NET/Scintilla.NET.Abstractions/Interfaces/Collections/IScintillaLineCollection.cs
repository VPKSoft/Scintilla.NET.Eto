using System.Collections;
using Scintilla.NET.Abstractions.Collections;
using static Scintilla.NET.Abstractions.Classes.ScintillaApiStructs;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// An immutable collection of lines of text in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> : IEnumerable<TLine>
    where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap: class
    where TColor: struct
{
    #region Methods
    /// <summary>
    /// Adjust the number of CHARACTERS in a line.
    /// </summary>
    void AdjustLineLength(int index, int delta);

    /// <summary>
    /// Converts a BYTE offset to a CHARACTER offset.
    /// </summary>
    int ByteToCharPosition(int pos);

    /// <summary>
    /// Returns the number of CHARACTERS in a line.
    /// </summary>
    int CharLineLength(int index);

    /// <summary>
    /// Returns the CHARACTER offset where the line begins.
    /// </summary>
    int CharPositionFromLine(int index);

    /// <summary>
    /// Gets the byte position from the specified character position.
    /// </summary>
    /// <param name="pos">The character position within the document.</param>
    /// <returns>The byte position of the specified character position.</returns>
    int CharToBytePosition(int pos);

    /// <summary>
    /// Deletes the specified line characters specified by the line index.
    /// </summary>
    /// <param name="index">The line index.</param>
    void DeletePerLine(int index);

    /// <summary>
    /// Gets the number of CHARACTERS int a BYTE range.
    /// </summary>
    int GetCharCount(int pos, int length);

    /// <summary>
    /// Gets a value indicating whether a line specified by its index contains multi-byte character(s).
    /// </summary>
    /// <param name="index">The line index.</param>
    /// <returns><c>true</c> if the line specified by its index contains multi-byte character(s), <c>false</c> otherwise.</returns>
    bool LineContainsMultiByteChar(int index);

    /// <summary>
    /// Returns the line index containing the CHARACTER position.
    /// </summary>
    int LineFromCharPosition(int pos);

    /// <summary>
    /// Tracks a new line with the given CHARACTER length.
    /// </summary>
    void InsertPerLine(int index, int length = 0);

    /// <summary>
    /// Moves the step.
    /// </summary>
    /// <param name="line">The line.</param>
    void MoveStep(int line);

    /// <summary>
    /// Rebuilds the line data.
    /// </summary>
    void RebuildLineData();

    /// <summary>
    /// A method to be added as event subscription to <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.SCNotification"/> event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The <see cref="ISCNotificationEventArgs"/> instance containing the event data.</param>
    void ScNotificationCallback(object sender, ISCNotificationEventArgs e);

    /// <summary>
    /// Further handling of the Scintilla notification event.
    /// </summary>
    /// <param name="scn">The Scintilla notification data.</param>
    void ScnModified(SCNotification scn);

    /// <summary>
    /// Tracks the delete text.
    /// </summary>
    /// <param name="scn">The Scintilla notification data.</param>
    void TrackDeleteText(SCNotification scn);

    /// <summary>
    /// Tracks the insert text.
    /// </summary>
    /// <param name="scn">The Scintilla notification data.</param>
    void TrackInsertText(SCNotification scn);
    #endregion Methods

    #region Properties
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine,
        TMargin, TSelection, TBitmap, TColor> ScintillaApi { get; }

    /// <summary>
    /// Gets a value indicating whether all the document lines are visible (not hidden).
    /// </summary>
    /// <returns>true if all the lines are visible; otherwise, false.</returns>
    bool AllLinesVisible { get; }

    /// <summary>
    /// Gets the number of lines.
    /// </summary>
    /// <returns>The number of lines in the <see cref="LineCollectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" />.</returns>
    int Count { get; }

    /// <summary>
    /// Gets the number of CHARACTERS in the document.
    /// </summary>
    int TextLength { get; }

    /// <summary>
    /// Gets the <see cref="LineBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="LineBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> to get.</param>
    /// <returns>The <see cref="LineBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> at the specified index.</returns>
    TLine this[int index] { get; }

    #endregion Properties
}