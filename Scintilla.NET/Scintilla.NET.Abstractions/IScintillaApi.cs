#region License
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
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.Collections;

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

    /// <summary>
    /// Initializes the Scintilla document.
    /// </summary>
    /// <param name="eolMode">The eol mode.</param>
    /// <param name="useTabs">if set to <c>true</c> use tabs instead of spaces.</param>
    /// <param name="tabWidth">Width of the tab.</param>
    /// <param name="indentWidth">Width of the indent.</param>
    void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0);
}

/// <summary>
/// Interface IScintillaApi
/// Implements the <see cref="Scintilla.NET.Abstractions.IScintillaApi" />
/// </summary>
/// <typeparam name="TMarkers">The type of the markers collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TStyles">The type of the styles collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TIndicators">The type of the indicators collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TLines">The type of the lines collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TMargins">The type of the margins collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TSelections">The type of the selections collection of the Scintilla control implementation.</typeparam>
/// <typeparam name="TMarker">The type of the item in the <typeparamref name="TMarkers"/> collection.</typeparam>
/// <typeparam name="TStyle">The type of the item in the <typeparamref name="TStyles"/> collection.</typeparam>
/// <typeparam name="TIndicator">The type of the item in the <typeparamref name="TIndicators"/> collection.</typeparam>
/// <typeparam name="TLine">The type of the item in the <typeparamref name="TLines"/> collection.</typeparam>
/// <typeparam name="TMargin">The type of the item in the <typeparamref name="TMargin"/> collection.</typeparam>
/// <typeparam name="TSelection">The type of the item in the <typeparamref name="TSelections"/> collection.</typeparam>
/// <typeparam name="TImage">The type of the image used in the platform.</typeparam>
/// <typeparam name="TColor">The type of the color used in the platform.</typeparam>
/// <seealso cref="Scintilla.NET.Abstractions.IScintillaApi" />
public interface IScintillaApi<
    out TMarkers, 
    out TStyles,
    out TIndicators, 
    out TLines, 
    out TMargins, 
    out TSelections,
    TMarker,
    TStyle, 
    TIndicator,
    TLine,
    TMargin,
    TSelection,
    TImage,
    TColor> : IScintillaApi, IScintillaCollectionProperties<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TImage, TColor>
    where TMarkers : IScintillaMarkerCollection<TMarker, TImage, TColor>, IEnumerable
    where TStyles : IScintillaStyleCollection<TStyle, TColor>, IEnumerable
    where TIndicators :IScintillaIndicatorCollection<TIndicator, TColor>, IEnumerable
    where TLines : IScintillaLineCollection<TLine>, IEnumerable
    where TMargins : IScintillaMarginCollection<TMargin, TColor>, IEnumerable
    where TSelections : IScintillaSelectionCollection<TSelection>, IEnumerable
    where TMarker: IScintillaMarker<TImage, TColor>
    where TStyle : IScintillaStyle<TColor>
    where TIndicator : IScintillaIndicator<TColor>
    where TLine : IScintillaLine
    where TMargin : IScintillaMargin<TColor>
    where TSelection : IScintillaSelection
    where TImage: class
    where TColor: struct
{
}