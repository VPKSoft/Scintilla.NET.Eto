﻿using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.EtoForms.Shared.Collections;

/// <summary>
/// Represents a margin marker in a <see cref="WinForms.Scintilla" /> control.
/// </summary>
public class Marker : MarkerBase<Image, Color>
{
    /// <summary>
    /// Sets the marker symbol to a custom image.
    /// </summary>
    /// <param name="image">The Bitmap to use as a marker symbol.</param>
    /// <remarks>Calling this method will also update the <see cref="MarkerBase{TImage, TColor}.Symbol" /> property to <see cref="MarkerSymbol.RgbaImage" />.</remarks>
    public override void DefineRgbaImage(Image image)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets the background color of the marker.
    /// </summary>
    /// <param name="color">The <see cref="Marker" /> background Color. The default is White.</param>
    /// <remarks>
    /// The background color of the whole line will be drawn in the <paramref name="color" /> specified when the marker is not visible
    /// because it is hidden by a <see cref="IScintillaMargin{TColor}.Width" /> is zero.
    /// </remarks>
    /// <seealso cref="MarkerBase{TImage, TColor}.SetAlpha" />
    public override void SetBackColor(Color color)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets the foreground color of the marker.
    /// </summary>
    /// <param name="color">The <see cref="Marker" /> foreground Color. The default is Black.</param>
    public override void SetForeColor(Color color)
    {
            throw new NotImplementedException();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Marker" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that created this marker.</param>
    /// <param name="index">The index of this style within the <see cref="MarkerCollection" /> that created it.</param>
    public Marker(IScintillaApi scintilla, int index) : base(scintilla, index)
    {
    }
}