using Eto.Drawing;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.EtoForms.Utilities;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.EtoForms.Collections;

/// <summary>
/// Represents a margin displayed on the left edge of a Scintilla control.
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
            var color = ScintillaApi.DirectMessage(SCI_GETMARGINBACKN, new IntPtr(Index)).ToInt32();
            return ColorTranslatorEto.ToColor(color);
        }
        set
        {
            var color = ColorTranslatorEto.ToArgb(value);
            ScintillaApi.DirectMessage(SCI_SETMARGINBACKN, new IntPtr(Index), new IntPtr(color));
        }
    }
    
    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Margin" /> class.
    /// </summary>
    /// <param name="scintilla">The Scintilla control that created this margin.</param>
    /// <param name="index">The index of this margin within the <see cref="MarginCollection" /> that created it.</param>
    public Margin(IScintillaApi scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}