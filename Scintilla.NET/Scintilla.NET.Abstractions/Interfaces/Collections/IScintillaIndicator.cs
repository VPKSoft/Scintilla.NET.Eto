using System.Collections;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// Represents an indicator in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> 
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
    /// Given a document position which is filled with this indicator, will return the document position
    /// where the use of this indicator ends.
    /// </summary>
    /// <param name="position">A zero-based document position using this indicator.</param>
    /// <returns>The zero-based document position where the use of this indicator ends.</returns>
    /// <remarks>
    /// Specifying a <paramref name="position" /> which is not filled with this indicator will cause this method
    /// to return the end position of the range where this indicator is not in use (the negative space). If this
    /// indicator is not in use anywhere within the document the return value will be 0.
    /// </remarks>
    int End(int position);

    /// <summary>
    /// Given a document position which is filled with this indicator, will return the document position
    /// where the use of this indicator starts.
    /// </summary>
    /// <param name="position">A zero-based document position using this indicator.</param>
    /// <returns>The zero-based document position where the use of this indicator starts.</returns>
    /// <remarks>
    /// Specifying a <paramref name="position" /> which is not filled with this indicator will cause this method
    /// to return the start position of the range where this indicator is not in use (the negative space). If this
    /// indicator is not in use anywhere within the document the return value will be 0.
    /// </remarks>
    int Start(int position);

    /// <summary>
    /// Returns the user-defined value for the indicator at the specified position.
    /// </summary>
    /// <param name="position">The zero-based document position to get the indicator value for.</param>
    /// <returns>The user-defined value at the specified <paramref name="position" />.</returns>
    int ValueAt(int position);
    #endregion Methods

    #region Properties
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine,
        TMargin, TSelection, TBitmap, TColor> ScintillaApi { get; }

    /// <summary>
    /// Gets or sets the alpha transparency of the indicator.
    /// </summary>
    /// <returns>
    /// The alpha transparency ranging from 0 (completely transparent)
    /// to 255 (no transparency). The default is 30.
    /// </returns>
    int Alpha { get; set; }

    /// <summary>
    /// Gets or sets the indicator flags.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="IndicatorFlags" /> enumeration.
    /// The default is <see cref="IndicatorFlags.None" />.
    /// </returns>
    IndicatorFlags Flags { get; set; }

    /// <summary>
    /// Gets or sets the color used to draw an indicator.
    /// </summary>
    /// <returns>The Color used to draw an indicator. The default varies.</returns>
    /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
    /// <seealso cref="HoverForeColor" />
    TColor ForeColor { get; set; }

    /// <summary>
    /// Gets or sets the color used to draw an indicator when the mouse or caret is over an indicator.
    /// </summary>
    /// <returns>
    /// The Color used to draw an indicator.
    /// By default, the hover style is equal to the regular <see cref="ForeColor" />.
    /// </returns>
    /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
    /// <seealso cref="ForeColor" />
    TColor HoverForeColor { get; set; }

    /// <summary>
    /// Gets or sets the indicator style used when the mouse or caret is over an indicator.
    /// </summary>
    /// <returns>
    /// One of the <see cref="IndicatorStyle" /> enumeration values.
    /// By default, the hover style is equal to the regular <see cref="Style" />.
    /// </returns>
    /// <remarks>Changing the <see cref="Style" /> property will reset the <see cref="HoverStyle" />.</remarks>
    /// <seealso cref="Style" />
    IndicatorStyle HoverStyle { get; set; }

    /// <summary>
    /// Gets the zero-based indicator index this object represents.
    /// </summary>
    /// <returns>The indicator definition index within the <see cref="IndicatorCollectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" />.</returns>
    int Index { get; }

    /// <summary>
    /// Gets or sets the alpha transparency of the indicator outline.
    /// </summary>
    /// <returns>
    /// The alpha transparency ranging from 0 (completely transparent)
    /// to 255 (no transparency). The default is 50.
    /// </returns>
    int OutlineAlpha { get; set; }

    /// <summary>
    /// Gets or sets the indicator style.
    /// </summary>
    /// <returns>One of the <see cref="IndicatorStyle" /> enumeration values. The default varies.</returns>
    /// <remarks>Changing the <see cref="Style" /> property will reset the <see cref="HoverStyle" />.</remarks>
    /// <seealso cref="HoverStyle" />
    IndicatorStyle Style { get; set; }

    /// <summary>
    /// Gets or sets whether indicators are drawn under or over text.
    /// </summary>
    /// <returns>true to draw the indicator under text; otherwise, false. The default is false.</returns>
    /// <remarks>Drawing indicators under text requires <see cref="Phases.One" /> or <see cref="Phases.Multiple" /> drawing.</remarks>
    bool Under { get; set; }
    #endregion Properties
}