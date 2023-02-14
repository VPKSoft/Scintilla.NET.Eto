﻿#region License
/*
MIT License

Copyright(c) 2023 Petteri Kautonen

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

using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments;
using static ScintillaNet.Abstractions.Classes.ScintillaApiStructs;

namespace ScintillaNet.Abstractions.Interfaces.Collections;

/// <summary>
/// An interface for the <see cref="IScintillaLineCollection{TLine}"/> methods and properties.
/// </summary>
public interface IScintillaLineCollectionGeneral
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
    /// A method to be added as event subscription to <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.SCNotification"/> event.
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
    #endregion

    #region Propertiees
    /// <summary>
    /// Gets a value indicating whether all the document lines are visible (not hidden).
    /// </summary>
    /// <returns>true if all the lines are visible; otherwise, false.</returns>
    bool AllLinesVisible { get; }

    /// <summary>
    /// Gets the number of lines.
    /// </summary>
    /// <returns>The number of lines in the <see cref="LineCollectionBase{TLine}" />.</returns>
    int Count { get; }

    /// <summary>
    /// Gets the number of CHARACTERS in the document.
    /// </summary>
    int TextLength { get; }
    #endregion
}
 