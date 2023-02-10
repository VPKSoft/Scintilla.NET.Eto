using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// Represents a margin marker in a <see cref="Scintilla" /> control.
/// </summary>
public interface IScintillaMarker<in TImage, in TColor>
    where TImage : class
    where TColor : struct
{
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Sets the marker symbol to a custom image.
    /// </summary>
    /// <param name="image">The Bitmap to use as a marker symbol.</param>
    /// <remarks>Calling this method will also update the <see cref="Symbol" /> property to <see cref="MarkerSymbol.RgbaImage" />.</remarks>
    void DefineRgbaImage(TImage image);

    /// <summary>
    /// Removes this marker from all lines.
    /// </summary>
    void DeleteAll();

    /// <summary>
    /// Sets the foreground alpha transparency for markers that are drawn in the content area.
    /// </summary>
    /// <param name="alpha">The alpha transparency ranging from 0 (completely transparent) to 255 (no transparency).</param>
    /// <remarks>See the remarks on the <see cref="SetBackColor" /> method for a full explanation of when a marker can be drawn in the content area.</remarks>
    /// <seealso cref="SetBackColor" />
    void SetAlpha(int alpha);

    /// <summary>
    /// Sets the background color of the marker.
    /// </summary>
    /// <param name="color">The <see cref="IScintillaMargin{TColor}" /> background Color. The default is White.</param>
    /// <remarks>
    /// The background color of the whole line will be drawn in the <paramref name="color" /> specified when the marker is not visible
    /// because it is hidden by a <see cref="IScintillaMargin{TColor}.Mask" /> or the <see cref="MarginBase{TColor}.Width" /> is zero.
    /// </remarks>
    /// <seealso cref="SetAlpha" />
    void SetBackColor(TColor color);

    /// <summary>
    /// Sets the foreground color of the marker.
    /// </summary>
    /// <param name="color">The <see cref="IScintillaMargin{TColor}" /> foreground Color. The default is Black.</param>
    void SetForeColor(TColor color);

    /// <summary>
    /// Gets the zero-based marker index this object represents.
    /// </summary>
    /// <returns>The marker index within the <see cref="MarkerCollectionBase{TMarker, TImage, TColor}" />.</returns>
    public int Index { get; }

    /// <summary>
    /// Gets or sets the marker symbol.
    /// </summary>
    /// <returns>
    /// One of the <see cref="MarkerSymbol" /> enumeration values.
    /// The default is <see cref="MarkerSymbol.Circle" />.
    /// </returns>
    MarkerSymbol Symbol { get; set; }
}