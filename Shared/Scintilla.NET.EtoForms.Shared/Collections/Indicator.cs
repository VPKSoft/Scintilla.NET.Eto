using System.ComponentModel;
using Eto.Drawing;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.EtoForms.Shared.EventArgs;
using Scintilla.NET.EtoForms.Shared.Utilities;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.EtoForms.Shared.Collections;

/// <summary>
/// Represents an indicator in a <see cref="Scintilla" /> control.
/// </summary>
public class Indicator : IndicatorBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap, Color>
{
    #region Properties

    /// <summary>
    /// Gets or sets the color used to draw an indicator.
    /// </summary>
    /// <returns>The Color used to draw an indicator. The default varies.</returns>
    /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
    /// <seealso cref="HoverForeColor" />
    public override Color ForeColor
    {
        get
        {
            var color = scintilla.DirectMessage(SCI_INDICGETFORE, new IntPtr(Index)).ToInt32();
            return ColorTranslatorEto.ToColor(color);
        }
        set
        {
            var color = ColorTranslatorEto.ToArgb(value);
            scintilla.DirectMessage(SCI_INDICSETFORE, new IntPtr(Index), new IntPtr(color));
        }
    }

    /// <summary>
    /// Gets or sets the color used to draw an indicator when the mouse or caret is over an indicator.
    /// </summary>
    /// <returns>
    /// The Color used to draw an indicator.
    /// By default, the hover style is equal to the regular <see cref="ForeColor" />.
    /// </returns>
    /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
    /// <seealso cref="ForeColor" />
    public override Color HoverForeColor
    {
        get
        {
            var color = scintilla.DirectMessage(SCI_INDICGETHOVERFORE, new IntPtr(Index)).ToInt32();
            return ColorTranslatorEto.ToColor(color);
        }
        set
        {
            var color = ColorTranslatorEto.ToArgb(value);
            scintilla.DirectMessage(SCI_INDICSETHOVERFORE, new IntPtr(Index), new IntPtr(color));
        }
    }


    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Indicator" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this indicator.</param>
    /// <param name="index">The index of this style within the <see cref="IndicatorCollection" /> that created it.</param>
    public Indicator(
        IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection,
            SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Bitmap,
            Color> scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}