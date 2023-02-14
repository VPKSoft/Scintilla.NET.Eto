using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// Represents a margin marker in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class MarkerBase<TImage, TColor> : IScintillaMarker<TImage, TColor>
    where TColor : struct
    where TImage : class
{
    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Sets the marker symbol to a custom image.
    /// </summary>
    /// <param name="image">The Bitmap to use as a marker symbol.</param>
    /// <remarks>Calling this method will also update the <see cref="Symbol" /> property to <see cref="MarkerSymbol.RgbaImage" />.</remarks>
    public abstract void DefineRgbaImage(TImage image);

    /// <summary>
    /// Removes this marker from all lines.
    /// </summary>
    public void DeleteAll()
    {
        ScintillaApi.MarkerDeleteAll(Index);
    }

    /// <summary>
    /// Sets the foreground alpha transparency for markers that are drawn in the content area.
    /// </summary>
    /// <param name="alpha">The alpha transparency ranging from 0 (completely transparent) to 255 (no transparency).</param>
    /// <remarks>See the remarks on the <see cref="SetBackColor" /> method for a full explanation of when a marker can be drawn in the content area.</remarks>
    /// <seealso cref="SetBackColor" />
    public virtual void SetAlpha(int alpha)
    {
        alpha = HelpersGeneral.Clamp(alpha, 0, 255);
        ScintillaApi.DirectMessage(SCI_MARKERSETALPHA, new IntPtr(Index), new IntPtr(alpha));
    }

    /// <summary>
    /// Sets the background color of the marker.
    /// </summary>
    /// <param name="color">The <see cref="MarkerBase{TImage, TColor}" /> background Color. The default is White.</param>
    /// <remarks>
    /// The background color of the whole line will be drawn in the <paramref name="color" /> specified when the marker is not visible
    /// because it is hidden by a <see cref="MarginBase{TColor}.Width" /> is zero.
    /// </remarks>
    /// <seealso cref="SetAlpha" />
    public abstract void SetBackColor(TColor color);

    /// <summary>
    /// Sets the foreground color of the marker.
    /// </summary>
    /// <param name="color">The <see cref="MarkerBase{TImage, TColor}" /> foreground Color. The default is Black.</param>
    public abstract void SetForeColor(TColor color);

    /// <summary>
    /// Gets the zero-based marker index this object represents.
    /// </summary>
    /// <returns>The marker index within the <see cref="MarkerCollectionBase{TMarker, TImage, TColor}" />.</returns>
    public int Index { get; private set; }

    /// <summary>
    /// Gets or sets the marker symbol.
    /// </summary>
    /// <returns>
    /// One of the <see cref="MarkerSymbol" /> enumeration values.
    /// The default is <see cref="MarkerSymbol.Circle" />.
    /// </returns>
    public virtual MarkerSymbol Symbol
    {
        get => (MarkerSymbol)ScintillaApi.DirectMessage(SCI_MARKERSYMBOLDEFINED, new IntPtr(Index));
        set
        {
            var markerSymbol = (int)value;
            ScintillaApi.DirectMessage(SCI_MARKERDEFINE, new IntPtr(Index), new IntPtr(markerSymbol));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkerBase{TImage, TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this marker.</param>
    /// <param name="index">The index of this style within the <see cref="MarkerCollectionBase{TMarker, TImage, TColor}" /> that created it.</param>
    public MarkerBase(IScintillaApi scintilla, int index)
    {
        this.ScintillaApi = scintilla;
        Index = index;
    }
}