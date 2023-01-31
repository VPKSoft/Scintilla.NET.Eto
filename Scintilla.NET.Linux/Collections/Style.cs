using System;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Collections;
using Color = Gdk.Color;
using ColorTranslator = Scintilla.NET.Linux.GdkUtils.ColorTranslator;
using Image = Gtk.Image;

namespace Scintilla.NET.Linux.Collections;

/// <summary>
/// A style definition in a <see cref="Scintilla" /> control.
/// </summary>
public class Style : StyleBase<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
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
            var color = scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETBACK, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return ColorTranslator.ToColor(color);
        }
        set
        {
            var color = ColorTranslator.ToInt(value);
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETBACK, new IntPtr(Index), new IntPtr(color));
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
            var color = scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETFORE, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return ColorTranslator.ToColor(color);
        }
        set
        {
            var color = ColorTranslator.ToInt(value);
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETFORE, new IntPtr(Index), new IntPtr(color));
        }
    }
    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instances of the <see cref="Style" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this style.</param>
    /// <param name="index">The index of this style within the <see cref="StyleCollection" /> that created it.</param>
    public Style(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla, int index) : base(scintilla, index)
    {
    }

    #endregion Constructors
}