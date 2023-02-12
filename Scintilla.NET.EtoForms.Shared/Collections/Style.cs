using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;

namespace Scintilla.NET.EtoForms.Shared.Collections;

/// <summary>
/// A style definition in a <see cref="WinForms.Scintilla" /> control.
/// </summary>
public class Style : StyleBase<Color>
{
    #region Properties

    /// <summary>
    /// Gets or sets the background color of the style.
    /// </summary>
    /// <returns>A Color object representing the style background color. The default is White.</returns>
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

    /// <summary>
    /// Gets or sets the foreground color of the style.
    /// </summary>
    /// <returns>A Color object representing the style foreground color. The default is Black.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    public override Color ForeColor
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
    /// Initializes a new instances of the <see cref="Style" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that created this style.</param>
    /// <param name="index">The index of this style within the <see cref="StyleCollection" /> that created it.</param>
    public Style(IScintillaApi scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}