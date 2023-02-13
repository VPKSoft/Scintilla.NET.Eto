﻿using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.EtoForms.Shared.Collections;

/// <summary>
/// Represents a margin displayed on the left edge of a <see cref="WinForms.Scintilla" /> control.
/// </summary>
public class Margin : MarginBase<Color>
{
    #region Properties

    /// <summary>
    /// Gets or sets the background color of the margin when the <see cref="Type" /> property is set to <see cref="MarginType.Color" />.
    /// </summary>
    /// <returns>A Color object representing the margin background color. The default is Black.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    public override Color BackColor
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }
    
    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Margin" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that created this margin.</param>
    /// <param name="index">The index of this margin within the <see cref="MarginCollection" /> that created it.</param>
    public Margin(IScintillaApi scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}