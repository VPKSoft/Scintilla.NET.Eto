using System.Text;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// A style definition in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class StyleBase<TColor> : IScintillaStyle<TColor>
    where TColor: struct
{
    #region Methods
    /// <inheritdoc />
    public void CopyTo<TDestination>(IScintillaStyle<TColor>? destination) where TDestination : IScintillaStyle<TColor>
    {
        if (destination == null)
        {
            return;
        }

        destination.BackColor = BackColor;
        // destination.Bold = Bold;
        destination.Case = Case;
        destination.FillLine = FillLine;
        destination.Font = Font;
        destination.ForeColor = ForeColor;
        destination.Hotspot = Hotspot;
        destination.Italic = Italic;
        destination.Size = Size;
        destination.SizeF = SizeF;
        destination.Underline = Underline;
        destination.Visible = Visible;
        destination.Weight = Weight;
    }
    #endregion Methods

    #region Properties
    /// <inheritdoc />
    public IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets or sets the background color of the style.
    /// </summary>
    /// <returns>A Color object representing the style background color. The default is White.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    public abstract TColor BackColor { get; set; }

    /// <summary>
    /// Gets or sets whether the style font is bold.
    /// </summary>
    /// <returns>true if bold; otherwise, false. The default is false.</returns>
    /// <remarks>Setting this property affects the <see cref="Weight" /> property.</remarks>
    public virtual bool Bold
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETBOLD, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        set
        {
            var bold = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETBOLD, new IntPtr(Index), bold);
        }
    }

    /// <summary>
    /// Gets or sets the casing used to display the styled text.
    /// </summary>
    /// <returns>One of the <see cref="StyleCase" /> enum values. The default is <see cref="StyleCase.Mixed" />.</returns>
    /// <remarks>This does not affect how text is stored, only displayed.</remarks>
    public virtual StyleCase Case
    {
        get
        {
            var @case = ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETCASE, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return (StyleCase)@case;
        }
        set
        {
            // Just an excuse to use @... syntax
            var @case = (int)value;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETCASE, new IntPtr(Index), new IntPtr(@case));
        }
    }

    /// <summary>
    /// Gets or sets whether the remainder of the line is filled with the <see cref="BackColor" />
    /// when this style is used on the last character of a line.
    /// </summary>
    /// <returns>true to fill the line; otherwise, false. The default is false.</returns>
    public virtual bool FillLine
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETEOLFILLED, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        set
        {
            var fillLine = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETEOLFILLED, new IntPtr(Index), fillLine);
        }
    }

    /// <summary>
    /// Gets or sets the style font name.
    /// </summary>
    /// <returns>The style font name. The default is Verdana.</returns>
    /// <remarks>Scintilla caches fonts by name so font names and casing should be consistent.</remarks>
    public virtual string Font
    {
        get
        {
            var length = ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETFONT, new IntPtr(Index), IntPtr.Zero).ToInt32();
            var font = new byte[length];
            unsafe
            {
                fixed (byte* bp = font)
                {
                    ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETFONT, new IntPtr(Index), new IntPtr(bp));
                }
            }

            var name = Encoding.UTF8.GetString(font, 0, length);
            return name;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "Verdana";
            }

            // Scintilla expects UTF-8
            var font = HelpersGeneral.GetBytes(value, Encoding.UTF8, true);
            unsafe
            {
                fixed (byte* bp = font)
                {
                    ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETFONT, new IntPtr(Index), new IntPtr(bp));
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the foreground color of the style.
    /// </summary>
    /// <returns>A Color object representing the style foreground color. The default is Black.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    public abstract TColor ForeColor { get; set; }

    /// <summary>
    /// Gets or sets whether hovering the mouse over the style text exhibits hyperlink behavior.
    /// </summary>
    /// <returns>true to use hyperlink behavior; otherwise, false. The default is false.</returns>
    public virtual bool Hotspot
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETHOTSPOT, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        set
        {
            var hotspot = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETHOTSPOT, new IntPtr(Index), hotspot);
        }
    }

    /// <summary>
    /// Gets the zero-based style definition index.
    /// </summary>
    /// <returns>The style definition index within the <see cref="StyleCollectionBase{TStyle,TColor}" />.</returns>
    public int Index { get; }

    /// <summary>
    /// Gets or sets whether the style font is italic.
    /// </summary>
    /// <returns>true if italic; otherwise, false. The default is false.</returns>
    public virtual bool Italic
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETITALIC, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        set
        {
            var italic = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETITALIC, new IntPtr(Index), italic);
        }
    }

    /// <summary>
    /// Gets or sets the size of the style font in points.
    /// </summary>
    /// <returns>The size of the style font as a whole number of points. The default is 8.</returns>
    public virtual int Size
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETSIZE, new IntPtr(Index), IntPtr.Zero).ToInt32();
        set => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETSIZE, new IntPtr(Index), new IntPtr(value));
    }

    /// <summary>
    /// Gets or sets the size of the style font in fractional points.
    /// </summary>
    /// <returns>The size of the style font in fractional number of points. The default is 8.</returns>
    public virtual float SizeF
    {
        get
        {
            var fraction = ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETSIZEFRACTIONAL, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return (float)fraction / ScintillaConstants.SC_FONT_SIZE_MULTIPLIER;
        }
        set
        {
            var fraction = (int)(value * ScintillaConstants.SC_FONT_SIZE_MULTIPLIER);
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETSIZEFRACTIONAL, new IntPtr(Index), new IntPtr(fraction));
        }
    }

    /// <summary>
    /// Gets or sets whether the style is underlined.
    /// </summary>
    /// <returns>true if underlined; otherwise, false. The default is false.</returns>
    public virtual bool Underline
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETUNDERLINE, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        set
        {
            var underline = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETUNDERLINE, new IntPtr(Index), underline);
        }
    }

    /// <summary>
    /// Gets or sets whether the style text is visible.
    /// </summary>
    /// <returns>true to display the style text; otherwise, false. The default is true.</returns>
    public virtual bool Visible
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETVISIBLE, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        set
        {
            var visible = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETVISIBLE, new IntPtr(Index), visible);
        }
    }

    /// <summary>
    /// Gets or sets the style font weight.
    /// </summary>
    /// <returns>The font weight. The default is 400.</returns>
    /// <remarks>Setting this property affects the <see cref="Bold" /> property.</remarks>
    public virtual int Weight
    {
        get => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETWEIGHT, new IntPtr(Index), IntPtr.Zero).ToInt32();
        set => ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETWEIGHT, new IntPtr(Index), new IntPtr(value));
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instances of the <see cref="StyleBase{TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this style.</param>
    /// <param name="index">The index of this style within the <see cref="StyleCollectionBase{TStyle,TColor}" /> that created it.</param>
    public StyleBase(IScintillaApi scintilla, int index)
    {
        this.ScintillaApi = scintilla;
        Index = index;
    }

    #endregion Constructors
}