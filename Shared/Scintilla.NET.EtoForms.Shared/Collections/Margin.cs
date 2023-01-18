using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.EtoForms.Shared.EventArgs;
using Scintilla.NET.EtoForms.Shared.Utilities;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.EtoForms.Shared.Collections;

/// <summary>
/// Represents a margin displayed on the left edge of a <see cref="Scintilla" /> control.
/// </summary>
public class Margin : MarginBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color>
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
            var color = scintilla.DirectMessage(SCI_GETMARGINBACKN, new IntPtr(Index)).ToInt32();
            return ColorTranslatorEto.ToColor(color);
        }
        set
        {
            value = ColorTranslatorEto.FallBackColor(value, Colors.Black);

            var color = ColorTranslatorEto.ToArgb(value);
            scintilla.DirectMessage(SCI_SETMARGINBACKN, new IntPtr(Index), new IntPtr(color));
        }
    }
    
    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Margin" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this margin.</param>
    /// <param name="index">The index of this margin within the <see cref="MarginCollection" /> that created it.</param>
    public Margin(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color> scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}