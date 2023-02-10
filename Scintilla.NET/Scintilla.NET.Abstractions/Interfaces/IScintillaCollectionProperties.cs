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
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Abstractions.Interfaces;

/// <summary>
/// Properties for Scintilla API with without generic type members.
/// </summary>
public interface IScintillaCollectionProperties<out TMarkers, out TStyles, out TIndicators, out TLines, out TMargins, out TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TImage, TColor>
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
    /// <summary>
    /// Gets a collection of objects for working with indicators.
    /// </summary>
    /// <returns>A collection of <typeparamref name="TIndicator"/> objects.</returns>
    public TIndicators Indicators { get; }

    /// <summary>
    /// Gets a collection representing lines of text in the <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>A collection of text lines.</returns>
    public TLines Lines { get; }

    /// <summary>
    /// Gets a collection representing margins in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>A collection of margins.</returns>
    public TMargins Margins { get; }

    /// <summary>
    /// Gets a collection representing markers in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>A collection of markers.</returns>
    public TMarkers Markers { get; }

    /// <summary>
    /// Gets a collection representing multiple selections in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>A collection of selections.</returns>
    public TSelections Selections { get; }

    /// <summary>
    /// Gets a collection representing style definitions in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>A collection of style definitions.</returns>
    public TStyles Styles { get; }
}