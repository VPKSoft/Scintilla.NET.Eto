using System.Collections;
using System.Text;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Abstractions.Collections;

/// <summary>
/// A style definition in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections : SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMarker: MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap: class
    where TColor: struct
{
    #region Constants

    /// <summary>
    /// Default style index. This style is used to define properties that all styles receive when calling <see cref="Scintilla.StyleClearAll" />.
    /// </summary>
    public const int Default = ScintillaConstants.STYLE_DEFAULT;

    /// <summary>
    /// Line number style index. This style is used for text in line number margins. The background color of this style also
    /// sets the background color for all margins that do not have any folding mask set.
    /// </summary>
    public const int LineNumber = ScintillaConstants.STYLE_LINENUMBER;

    /// <summary>
    /// Call tip style index. Only font name, size, foreground color, background color, and character set attributes
    /// can be used when displaying a call tip.
    /// </summary>
    public const int CallTip = ScintillaConstants.STYLE_CALLTIP;

    /// <summary>
    /// Indent guide style index. This style is used to specify the foreground and background colors of <see cref="Scintilla.IndentationGuides" />.
    /// </summary>
    public const int IndentGuide = ScintillaConstants.STYLE_INDENTGUIDE;

    /// <summary>
    /// Brace highlighting style index. This style is used on a brace character when set with the <see cref="Scintilla.BraceHighlight" /> method
    /// or the indentation guide when used with the <see cref="Scintilla.HighlightGuide" /> property.
    /// </summary>
    public const int BraceLight = ScintillaConstants.STYLE_BRACELIGHT;

    /// <summary>
    /// Bad brace style index. This style is used on an unmatched brace character when set with the <see cref="Scintilla.BraceBadLight" /> method.
    /// </summary>
    public const int BraceBad = ScintillaConstants.STYLE_BRACEBAD;

    /// <summary>
    /// Fold text tag style index. This is the style used for drawing text tags attached to folded text when
    /// <see cref="Scintilla.FoldDisplayTextSetStyle" /> and <see cref="Line.ToggleFoldShowText" /> are used.
    /// </summary>
    public const int FoldDisplayText = ScintillaConstants.STYLE_FOLDDISPLAYTEXT;

    #endregion Constants

    #region Methods
    /// <inheritdoc />
    public void CopyTo(IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>? destination)
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
    public IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> ScintillaApi
    {
        get;
    }

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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETBOLD, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETEOLFILLED, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETHOTSPOT, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var hotspot = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETHOTSPOT, new IntPtr(Index), hotspot);
        }
    }

    /// <summary>
    /// Gets the zero-based style definition index.
    /// </summary>
    /// <returns>The style definition index within the <see cref="StyleCollectionBase" />.</returns>
    public int Index { get; private set; }

    /// <summary>
    /// Gets or sets whether the style font is italic.
    /// </summary>
    /// <returns>true if italic; otherwise, false. The default is false.</returns>
    public virtual bool Italic
    {
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETITALIC, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETSIZE, new IntPtr(Index), IntPtr.Zero).ToInt32();
        }
        set
        {
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETSIZE, new IntPtr(Index), new IntPtr(value));
        }
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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETUNDERLINE, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETVISIBLE, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
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
        get
        {
            return ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLEGETWEIGHT, new IntPtr(Index), IntPtr.Zero).ToInt32();
        }
        set
        {
            ScintillaApi.DirectMessage(ScintillaConstants.SCI_STYLESETWEIGHT, new IntPtr(Index), new IntPtr(value));
        }
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instances of the <see cref="StyleBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this style.</param>
    /// <param name="index">The index of this style within the <see cref="StyleCollectionBase" /> that created it.</param>
    public StyleBase(IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> scintilla, int index)
    {
        this.ScintillaApi = scintilla;
        Index = index;
    }

    #endregion Constructors
}