using Eto.Drawing;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.EtoForms.Extensions;

namespace ScintillaNet.EtoForms.Collections;

/// <summary>
/// A style definition in a Scintilla control.
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
            var color = ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETBACK, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return color.FromRgba();
        }
        set
        {
            var color = value.ToRgba();
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETBACK, new IntPtr(Index), new IntPtr(color));
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
            var color = ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETFORE, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return color.FromRgba();
        }
        set
        {
            var color = value.ToRgba();
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETFORE, new IntPtr(Index), new IntPtr(color));
        }
    }
    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instances of the <see cref="Style" /> class.
    /// </summary>
    /// <param name="scintilla">The Scintilla control that created this style.</param>
    /// <param name="index">The index of this style within the <see cref="StyleCollection" /> that created it.</param>
    public Style(IScintillaApi scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}