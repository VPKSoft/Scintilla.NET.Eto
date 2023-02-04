using System.Collections;
using System.Text;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// A style definition in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
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
    /// Copies the current style to another style.
    /// </summary>
    /// <param name="destination">The <see cref="StyleBase" /> to which the current style should be copied.</param>
    public void CopyTo(
        IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator,
            TLine, TMargin, TSelection, TBitmap, TColor>? destination);
    #endregion Methods

    #region Properties
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine,
        TMargin, TSelection, TBitmap, TColor> ScintillaApi { get; }

    /// <summary>
    /// Gets or sets the background color of the style.
    /// </summary>
    /// <returns>A Color object representing the style background color. The default is White.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    TColor BackColor { get; set; }

    /// <summary>
    /// Gets or sets whether the style font is bold.
    /// </summary>
    /// <returns>true if bold; otherwise, false. The default is false.</returns>
    /// <remarks>Setting this property affects the <see cref="Weight" /> property.</remarks>
    bool Bold { get; set; }

    /// <summary>
    /// Gets or sets the casing used to display the styled text.
    /// </summary>
    /// <returns>One of the <see cref="StyleCase" /> enum values. The default is <see cref="StyleCase.Mixed" />.</returns>
    /// <remarks>This does not affect how text is stored, only displayed.</remarks>
    StyleCase Case { get; set; }

    /// <summary>
    /// Gets or sets whether the remainder of the line is filled with the <see cref="BackColor" />
    /// when this style is used on the last character of a line.
    /// </summary>
    /// <returns>true to fill the line; otherwise, false. The default is false.</returns>
    bool FillLine { get; set; }

    /// <summary>
    /// Gets or sets the style font name.
    /// </summary>
    /// <returns>The style font name. The default is Verdana.</returns>
    /// <remarks>Scintilla caches fonts by name so font names and casing should be consistent.</remarks>
    string Font { get; set; }

    /// <summary>
    /// Gets or sets the foreground color of the style.
    /// </summary>
    /// <returns>A Color object representing the style foreground color. The default is Black.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    TColor ForeColor { get; set; }

    /// <summary>
    /// Gets or sets whether hovering the mouse over the style text exhibits hyperlink behavior.
    /// </summary>
    /// <returns>true to use hyperlink behavior; otherwise, false. The default is false.</returns>
    bool Hotspot { get; set; }

    /// <summary>
    /// Gets the zero-based style definition index.
    /// </summary>
    /// <returns>The style definition index within the <see cref="IScintillaStyleCollection{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />.</returns>
    int Index { get; }

    /// <summary>
    /// Gets or sets whether the style font is italic.
    /// </summary>
    /// <returns>true if italic; otherwise, false. The default is false.</returns>
    bool Italic { get; set; }

    /// <summary>
    /// Gets or sets the size of the style font in points.
    /// </summary>
    /// <returns>The size of the style font as a whole number of points. The default is 8.</returns>
    int Size { get; set; }

    /// <summary>
    /// Gets or sets the size of the style font in fractional points.
    /// </summary>
    /// <returns>The size of the style font in fractional number of points. The default is 8.</returns>
    float SizeF { get; set; }

    /// <summary>
    /// Gets or sets whether the style is underlined.
    /// </summary>
    /// <returns>true if underlined; otherwise, false. The default is false.</returns>
    bool Underline { get; set; }

    /// <summary>
    /// Gets or sets whether the style text is visible.
    /// </summary>
    /// <returns>true to display the style text; otherwise, false. The default is true.</returns>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the style font weight.
    /// </summary>
    /// <returns>The font weight. The default is 400.</returns>
    /// <remarks>Setting this property affects the <see cref="Bold" /> property.</remarks>
    int Weight { get; set; }
    #endregion Properties
}