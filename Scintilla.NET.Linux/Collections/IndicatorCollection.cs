﻿using System.ComponentModel;
using Color = Gdk.Color;

namespace ScintillaNet.Linux.Collections;

/// <summary>
/// An immutable collection of indicators in a <see cref="Scintilla" /> control.
/// </summary>
public class IndicatorCollection: IndicatorCollectionBase<Indicator, Color>
{
    private IScintillaLineCollectionGeneral lineCollectionGeneral;

    /// <summary>
    /// Gets an <see cref="Indicator" /> object at the specified index.
    /// </summary>
    /// <param name="index">The indicator index.</param>
    /// <returns>An object representing the indicator at the specified <paramref name="index" />.</returns>
    /// <remarks>
    /// Indicators 0 through 7 are used by lexers.
    /// Indicators 32 through 35 are used for IME.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Indicator this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Indicator(ScintillaApi, lineCollectionGeneral, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    public IndicatorCollection(IScintillaApi scintilla, IScintillaLineCollectionGeneral lineCollectionGeneral) : base(scintilla)
    {
        this.lineCollectionGeneral = lineCollectionGeneral;
    }
}