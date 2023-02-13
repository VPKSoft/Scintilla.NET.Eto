﻿using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;

namespace Scintilla.NET.EtoForms.Shared.Collections;

/// <summary>
/// An immutable collection of markers in a <see cref="WinForms.Scintilla" /> control.
/// </summary>
public class MarkerCollection : MarkerCollectionBase<Marker, Image, Color>
{
    /// <summary>
    /// Gets a <see cref="Marker" /> object at the specified index.
    /// </summary>
    /// <param name="index">The marker index.</param>
    /// <returns>An object representing the marker at the specified <paramref name="index" />.</returns>
    /// <remarks>Markers 25 through 31 are used by Scintilla for folding.</remarks>
    public override Marker this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Marker(ScintillaApi, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkerCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that created this collection.</param>
    public MarkerCollection(IScintillaApi scintilla) : base(scintilla)
    {

    }
}