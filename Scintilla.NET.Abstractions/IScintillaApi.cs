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

using System.Collections;
using System.Text;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;

namespace Scintilla.NET.Abstractions;

/// <summary>
/// An interface for interacting with the Scintilla API
/// </summary>
public interface IScintillaApi : IHasEncoding
{
    /// <summary>
    /// The platform-depended implementation of the <see cref="DirectMessage(int, IntPtr, IntPtr)"/> method.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    /// <remarks>The WParam of the call is set to <see cref="IntPtr.Zero"/>.</remarks>
    /// <remarks>The lParam of the call is set to <see cref="IntPtr.Zero"/>.</remarks>
    IntPtr DirectMessage(int message);
            
    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    /// <remarks>The lParam of the call is set to <see cref="IntPtr.Zero"/>.</remarks>
    IntPtr DirectMessage(int message, IntPtr wParam);

    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    IntPtr DirectMessage(int message, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="scintillaPointer">The Scintilla control pointer.</param>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    IntPtr DirectMessage(IntPtr scintillaPointer, int message, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Removes the specified marker from all lines.
    /// </summary>
    /// <param name="marker">The zero-based index to remove from all lines, or -1 to remove all markers from all lines.</param>
    void MarkerDeleteAll(int marker);

    /// <summary>
    /// Gets the length of the text in the control.
    /// </summary>
    /// <returns>The number of characters in the document.</returns>
    public int TextLength { get; }

    /// <summary>
    /// Gets a range of text from the document.
    /// </summary>
    /// <param name="position">The zero-based starting character position of the range to get.</param>
    /// <param name="length">The number of characters to get.</param>
    /// <returns>A string representing the text range.</returns>
    string GetTextRange(int position, int length);

    /// <summary>
    /// Performs the specified fold action on the entire document.
    /// </summary>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    /// <remarks>When using <see cref="FoldAction.Toggle" /> the first fold header in the document is examined to decide whether to expand or contract.</remarks>
    void FoldAll(FoldAction action);
}

/// <summary>
/// Interface IScintillaApi
/// Implements the <see cref="Scintilla.NET.Abstractions.IScintillaApi" />
/// Implements the <see cref="IScintillaNotificationEvent{TEventArgs}" />
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
/// <seealso cref="Scintilla.NET.Abstractions.IScintillaApi" />
/// <seealso cref="IScintillaNotificationEvent{TEventArgs}" />
public interface IScintillaApi<
    out TMarkers, 
    out TStyles,
    out TIndicators, 
    out TLines, 
    out TMargins, 
    out TSelections, 
    TEventArgs, 
    TMarker,
    TStyle, 
    TIndicator,
    TLine,
    TMargin,
    TSelection,
    TBitmap,
    TColor> : IScintillaApi, IScintillaNotificationEvent<TEventArgs>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections : SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TEventArgs : EventArgs
    where TMarker: MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap: class
    where TColor: struct
{
    /// <summary>
    /// Gets a collection representing markers in a <see cref="IScintillaApi{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> control.
    /// </summary>
    /// <returns>A collection of markers.</returns>
    TMarkers Markers { get; }

    /// <summary>
    /// Gets a collection representing style definitions in a <see cref="IScintillaApi{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> control.
    /// </summary>
    /// <returns>A collection of style definitions.</returns>
    TStyles Styles { get; }

    /// <summary>
    /// Gets a collection of objects for working with indicators.
    /// </summary>
    /// <returns>A collection of the indicator objects.</returns>
    TIndicators Indicators { get; }

    /// <summary>
    /// Gets a collection representing lines of text in the <see cref="IScintillaApi{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> control interface.
    /// </summary>
    /// <returns>A collection of text lines.</returns>
    TLines Lines { get; }
        
    /// <summary>
    /// Gets a collection representing margins in a <see cref="IScintillaApi{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> control interface.
    /// </summary>
    /// <returns>A collection of margins.</returns>
    TMargins Margins { get; }

    /// <summary>
    /// Gets a collection representing multiple selections in a <see cref="IScintillaApi{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> control interface.
    /// </summary>
    /// <returns>A collection of selections.</returns>
    TSelections Selections { get; }
}