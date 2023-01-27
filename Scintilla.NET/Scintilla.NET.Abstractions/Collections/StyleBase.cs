using System.Collections;
using System.Text;
using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Collections;

/// <summary>
/// A style definition in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections : SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TEventArgs : EventArgs
    where TMarker: MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
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

    #region Fields

    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    protected readonly IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> scintilla;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Copies the current style to another style.
    /// </summary>
    /// <param name="destination">The <see cref="StyleBase" /> to which the current style should be copied.</param>
    public void CopyTo(StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>? destination)
    {
        if (destination == null)
            return;

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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETBOLD, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var bold = value ? new IntPtr(1) : IntPtr.Zero;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETBOLD, new IntPtr(Index), bold);
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
            var @case = scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETCASE, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return (StyleCase)@case;
        }
        set
        {
            // Just an excuse to use @... syntax
            var @case = (int)value;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETCASE, new IntPtr(Index), new IntPtr(@case));
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETEOLFILLED, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var fillLine = value ? new IntPtr(1) : IntPtr.Zero;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETEOLFILLED, new IntPtr(Index), fillLine);
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
            var length = scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETFONT, new IntPtr(Index), IntPtr.Zero).ToInt32();
            var font = new byte[length];
            unsafe
            {
                fixed (byte* bp = font)
                {
                    scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETFONT, new IntPtr(Index), new IntPtr(bp));
                }
            }

            var name = Encoding.UTF8.GetString(font, 0, length);
            return name;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
                value = "Verdana";

            // Scintilla expects UTF-8
            var font = HelpersGeneral.GetBytes(value, Encoding.UTF8, true);
            unsafe
            {
                fixed (byte* bp = font)
                {
                    scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETFONT, new IntPtr(Index), new IntPtr(bp));
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETHOTSPOT, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var hotspot = value ? new IntPtr(1) : IntPtr.Zero;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETHOTSPOT, new IntPtr(Index), hotspot);
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETITALIC, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var italic = value ? new IntPtr(1) : IntPtr.Zero;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETITALIC, new IntPtr(Index), italic);
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETSIZE, new IntPtr(Index), IntPtr.Zero).ToInt32();
        }
        set
        {
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETSIZE, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets the size of the style font in fractoinal points.
    /// </summary>
    /// <returns>The size of the style font in fractional number of points. The default is 8.</returns>
    public virtual float SizeF
    {
        get
        {
            var fraction = scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETSIZEFRACTIONAL, new IntPtr(Index), IntPtr.Zero).ToInt32();
            return (float)fraction / ScintillaConstants.SC_FONT_SIZE_MULTIPLIER;
        }
        set
        {
            var fraction = (int)(value * ScintillaConstants.SC_FONT_SIZE_MULTIPLIER);
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETSIZEFRACTIONAL, new IntPtr(Index), new IntPtr(fraction));
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETUNDERLINE, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var underline = value ? new IntPtr(1) : IntPtr.Zero;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETUNDERLINE, new IntPtr(Index), underline);
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETVISIBLE, new IntPtr(Index), IntPtr.Zero) != IntPtr.Zero;
        }
        set
        {
            var visible = value ? new IntPtr(1) : IntPtr.Zero;
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETVISIBLE, new IntPtr(Index), visible);
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
            return scintilla.DirectMessage(ScintillaConstants.SCI_STYLEGETWEIGHT, new IntPtr(Index), IntPtr.Zero).ToInt32();
        }
        set
        {
            scintilla.DirectMessage(ScintillaConstants.SCI_STYLESETWEIGHT, new IntPtr(Index), new IntPtr(value));
        }
    }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instances of the <see cref="StyleBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this style.</param>
    /// <param name="index">The index of this style within the <see cref="StyleCollectionBase" /> that created it.</param>
    public StyleBase(IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TEventArgs, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> scintilla, int index)
    {
        this.scintilla = scintilla;
        Index = index;
    }

    #endregion Constructors

    #region Ada

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Ada" /> lexer.
    /// </summary>
    public static class Ada
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_ADA_DEFAULT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_ADA_COMMENTLINE;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_ADA_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_ADA_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_ADA_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_ADA_CHARACTER;

        /// <summary>
        /// Delimiter style index.
        /// </summary>
        public const int Delimiter = ScintillaConstants.SCE_ADA_DELIMITER;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_ADA_LABEL;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_ADA_IDENTIFIER;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_ADA_STRINGEOL;

        /// <summary>
        /// Unclosed character EOL style index.
        /// </summary>
        public const int CharacterEol = ScintillaConstants.SCE_ADA_CHARACTEREOL;

        /// <summary>
        /// Illegal identifier or keyword style index.
        /// </summary>
        public const int Illegal = ScintillaConstants.SCE_ADA_ILLEGAL;
    }

    #endregion Ada

    #region Asm

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Asm" /> lexer.
    /// </summary>
    public static class Asm
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_ASM_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_ASM_COMMENT;

        /// <summary>
        /// Comment block style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_ASM_COMMENTBLOCK;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_ASM_NUMBER;

        /// <summary>
        /// Math instruction (keword list 1) style index.
        /// </summary>
        public const int MathInstruction = ScintillaConstants.SCE_ASM_MATHINSTRUCTION;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_ASM_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_ASM_CHARACTER;

        /// <summary>
        /// CPU instruction (keyword list 0) style index.
        /// </summary>
        public const int CpuInstruction = ScintillaConstants.SCE_ASM_CPUINSTRUCTION;

        /// <summary>
        /// Register (keyword list 2) style index.
        /// </summary>
        public const int Register = ScintillaConstants.SCE_ASM_REGISTER;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_ASM_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_ASM_IDENTIFIER;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_ASM_STRINGEOL;

        /// <summary>
        /// Directive (keyword list 3) string style index.
        /// </summary>
        public const int Directive = ScintillaConstants.SCE_ASM_DIRECTIVE;

        /// <summary>
        /// Directive operand (keyword list 4) style index.
        /// </summary>
        public const int DirectiveOperand = ScintillaConstants.SCE_ASM_DIRECTIVEOPERAND;

        /// <summary>
        /// Extended instruction (keyword list 5) style index.
        /// </summary>
        public const int ExtInstruction = ScintillaConstants.SCE_ASM_EXTINSTRUCTION;

        /// <summary>
        /// Comment directive style index.
        /// </summary>
        public const int CommentDirective = ScintillaConstants.SCE_ASM_COMMENTDIRECTIVE;
    }

    #endregion Asm

    #region BlitzBasic

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.BlitzBasic" /> lexer.
    /// </summary>
    public static class BlitzBasic
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_B_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_B_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_B_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_B_KEYWORD;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_B_STRING;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_B_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_B_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_B_IDENTIFIER;

        /// <summary>
        /// Date style index.
        /// </summary>
        public const int Date = ScintillaConstants.SCE_B_DATE;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_B_STRINGEOL;

        /// <summary>
        /// Keyword list 2 (index 1) style index.
        /// </summary>
        public const int Keyword2 = ScintillaConstants.SCE_B_KEYWORD2;

        /// <summary>
        /// Keyword list 3 (index 2) style index.
        /// </summary>
        public const int Keyword3 = ScintillaConstants.SCE_B_KEYWORD3;

        /// <summary>
        /// Keyword list 4 (index 3) style index.
        /// </summary>
        public const int Keyword4 = ScintillaConstants.SCE_B_KEYWORD4;

        /// <summary>
        /// Constant style index.
        /// </summary>
        public const int Constant = ScintillaConstants.SCE_B_CONSTANT;

        /// <summary>
        /// Inline assembler style index.
        /// </summary>
        public const int Asm = ScintillaConstants.SCE_B_ASM;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_B_LABEL;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_B_ERROR;

        /// <summary>
        /// Hexadecimal number style index.
        /// </summary>
        public const int HexNumber = ScintillaConstants.SCE_B_HEXNUMBER;

        /// <summary>
        /// Binary number style index.
        /// </summary>
        public const int BinNumber = ScintillaConstants.SCE_B_BINNUMBER;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_B_COMMENTBLOCK;

        /// <summary>
        /// Documentation line style index.
        /// </summary>
        public const int DocLine = ScintillaConstants.SCE_B_DOCLINE;

        /// <summary>
        /// Documentation block style index.
        /// </summary>
        public const int DocBlock = ScintillaConstants.SCE_B_DOCBLOCK;

        /// <summary>
        /// Documentation keyword style index.
        /// </summary>
        public const int DocKeyword = ScintillaConstants.SCE_B_DOCKEYWORD;
    }

    #endregion BlitzBasic

    #region Batch

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Batch" /> lexer.
    /// </summary>
    public static class Batch
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_BAT_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_BAT_COMMENT;

        /// <summary>
        /// Keyword (list 0) style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_BAT_WORD;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_BAT_LABEL;

        /// <summary>
        /// Hide (@ECHO OFF/ON) style index.
        /// </summary>
        public const int Hide = ScintillaConstants.SCE_BAT_HIDE;

        /// <summary>
        /// External command (keyword list 1) style index.
        /// </summary>
        public const int Command = ScintillaConstants.SCE_BAT_COMMAND;

        /// <summary>
        /// Identifier string style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_BAT_IDENTIFIER;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_BAT_OPERATOR;
    }

    #endregion Batch

    #region Clw

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Clw" /> lexer.
    /// </summary>
    public static class CLW
    {
        /// <summary>
        /// Attributes style index
        /// </summary>
        public const int Attributes = ScintillaConstants.SCE_CLW_ATTRIBUTE;
            
        /// <summary>
        /// Built in procedures function style index.
        /// </summary>
        public const int BuiltInProceduresFunction = ScintillaConstants.SCE_CLW_BUILTIN_PROCEDURES_FUNCTION;
            
        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_CLW_COMMENT;

        /// <summary>
        /// Compiler directive style index
        /// </summary>
        public const int CompilerDirective = ScintillaConstants.SCE_CLW_COMPILER_DIRECTIVE;

        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_CLW_DEFAULT;
            
        /// <summary>
        /// Depreciated style index
        /// </summary>
        public const int Depreciated = ScintillaConstants.SCE_CLW_DEPRECATED;
            
        /// <summary>
        /// Error style index
        /// </summary>
        public const int Error = ScintillaConstants.SCE_CLW_ERROR;
            
        /// <summary>
        /// Integer Constant style index.
        /// </summary>
        public const int IntegerConstant = ScintillaConstants.SCE_CLW_INTEGER_CONSTANT;
            
        /// <summary>
        /// Keyword style index
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_CLW_KEYWORD;
            
        /// <summary>
        /// Label string style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_CLW_LABEL;

        /// <summary>
        /// Real Constant style index.
        /// </summary>
        public const int PictureString = ScintillaConstants.SCE_CLW_PICTURE_STRING;
            
        /// <summary>
        /// Real Constant style index.
        /// </summary>
        public const int RealConstant = ScintillaConstants.SCE_CLW_REAL_CONSTANT;

        /// <summary>
        /// Runtime expressions style index
        /// </summary>
        public const int RuntimeExpressions = ScintillaConstants.SCE_CLW_RUNTIME_EXPRESSIONS;
            
        /// <summary>
        /// Standard equates style index
        /// </summary>
        public const int StandardEquates = ScintillaConstants.SCE_CLW_STANDARD_EQUATE;
            
        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_CLW_STRING;
            
        /// <summary>
        /// Structure data type style index.
        /// </summary>
        public const int StructureDataTypes = ScintillaConstants.SCE_CLW_STRUCTURE_DATA_TYPE;
            
        /// <summary>
        /// User Identifier style index.
        /// </summary>
        public const int UserIdentifier = ScintillaConstants.SCE_CLW_USER_IDENTIFIER;
    }

    #endregion Clw

    #region Cpp

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Cpp" /> lexer.
    /// </summary>
    public static class Cpp
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_C_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_C_COMMENT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_C_COMMENTLINE;

        /// <summary>
        /// Documentation comment style index.
        /// </summary>
        public const int CommentDoc = ScintillaConstants.SCE_C_COMMENTDOC;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_C_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_C_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_C_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_C_CHARACTER;

        /// <summary>
        /// UUID style index.
        /// </summary>
        public const int Uuid = ScintillaConstants.SCE_C_UUID;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_C_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_C_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_C_IDENTIFIER;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_C_STRINGEOL;

        /// <summary>
        /// Verbatim string style index.
        /// </summary>
        public const int Verbatim = ScintillaConstants.SCE_C_VERBATIM;

        /// <summary>
        /// Regular expression style index.
        /// </summary>
        public const int Regex = ScintillaConstants.SCE_C_REGEX;

        /// <summary>
        /// Documentation comment line style index.
        /// </summary>
        public const int CommentLineDoc = ScintillaConstants.SCE_C_COMMENTLINEDOC;

        /// <summary>
        /// Keyword style 2 index.
        /// </summary>
        public const int Word2 = ScintillaConstants.SCE_C_WORD2;

        /// <summary>
        /// Comment keyword style index.
        /// </summary>
        public const int CommentDocKeyword = ScintillaConstants.SCE_C_COMMENTDOCKEYWORD;

        /// <summary>
        /// Comment keyword error style index.
        /// </summary>
        public const int CommentDocKeywordError = ScintillaConstants.SCE_C_COMMENTDOCKEYWORDERROR;

        /// <summary>
        /// Global class style index.
        /// </summary>
        public const int GlobalClass = ScintillaConstants.SCE_C_GLOBALCLASS;

        /// <summary>
        /// Raw string style index.
        /// </summary>
        public const int StringRaw = ScintillaConstants.SCE_C_STRINGRAW;

        /// <summary>
        /// Triple-quoted string style index.
        /// </summary>
        public const int TripleVerbatim = ScintillaConstants.SCE_C_TRIPLEVERBATIM;

        /// <summary>
        /// Hash-quoted string style index.
        /// </summary>
        public const int HashQuotedString = ScintillaConstants.SCE_C_HASHQUOTEDSTRING;

        /// <summary>
        /// Preprocessor comment style index.
        /// </summary>
        public const int PreprocessorComment = ScintillaConstants.SCE_C_PREPROCESSORCOMMENT;

        /// <summary>
        /// Preprocessor documentation comment style index.
        /// </summary>
        public const int PreprocessorCommentDoc = ScintillaConstants.SCE_C_PREPROCESSORCOMMENTDOC;

        /// <summary>
        /// User-defined literal style index.
        /// </summary>
        public const int UserLiteral = ScintillaConstants.SCE_C_USERLITERAL;

        /// <summary>
        /// Task marker style index.
        /// </summary>
        public const int TaskMarker = ScintillaConstants.SCE_C_TASKMARKER;

        /// <summary>
        /// Escape sequence style index.
        /// </summary>
        public const int EscapeSequence = ScintillaConstants.SCE_C_ESCAPESEQUENCE;
    }

    #endregion Cpp
        
    #region Css

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Css" /> lexer.
    /// </summary>
    public static class Css
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_CSS_DEFAULT;

        /// <summary>
        /// Tag style index.
        /// </summary>
        public const int Tag = ScintillaConstants.SCE_CSS_TAG;

        /// <summary>
        /// Class style index.
        /// </summary>
        public const int Class = ScintillaConstants.SCE_CSS_CLASS;

        /// <summary>
        /// Pseudo class style index.
        /// </summary>
        public const int PseudoClass = ScintillaConstants.SCE_CSS_PSEUDOCLASS;

        /// <summary>
        /// Unknown pseudo class style index.
        /// </summary>
        public const int UnknownPseudoClass = ScintillaConstants.SCE_CSS_UNKNOWN_PSEUDOCLASS;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_CSS_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_CSS_IDENTIFIER;

        /// <summary>
        /// Unknown identifier style index.
        /// </summary>
        public const int UnknownIdentifier = ScintillaConstants.SCE_CSS_UNKNOWN_IDENTIFIER;

        /// <summary>
        /// Value style index.
        /// </summary>
        public const int Value = ScintillaConstants.SCE_CSS_VALUE;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_CSS_COMMENT;

        /// <summary>
        /// ID style index.
        /// </summary>
        public const int Id = ScintillaConstants.SCE_CSS_ID;

        /// <summary>
        /// Important style index.
        /// </summary>
        public const int Important = ScintillaConstants.SCE_CSS_IMPORTANT;

        /// <summary>
        /// Directive style index.
        /// </summary>
        public const int Directive = ScintillaConstants.SCE_CSS_DIRECTIVE;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int DoubleString = ScintillaConstants.SCE_CSS_DOUBLESTRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int SingleString = ScintillaConstants.SCE_CSS_SINGLESTRING;

        /// <summary>
        /// Identifier style 2 index.
        /// </summary>
        public const int Identifier2 = ScintillaConstants.SCE_CSS_IDENTIFIER2;

        /// <summary>
        /// Attribute style index.
        /// </summary>
        public const int Attribute = ScintillaConstants.SCE_CSS_ATTRIBUTE;

        /// <summary>
        /// Identifier style 3 index.
        /// </summary>
        public const int Identifier3 = ScintillaConstants.SCE_CSS_IDENTIFIER3;

        /// <summary>
        /// Pseudo element style index.
        /// </summary>
        public const int PseudoElement = ScintillaConstants.SCE_CSS_PSEUDOELEMENT;

        /// <summary>
        /// Extended identifier style index.
        /// </summary>
        public const int ExtendedIdentifier = ScintillaConstants.SCE_CSS_EXTENDED_IDENTIFIER;

        /// <summary>
        /// Extended pseudo class style index.
        /// </summary>
        public const int ExtendedPseudoClass = ScintillaConstants.SCE_CSS_EXTENDED_PSEUDOCLASS;

        /// <summary>
        /// Extended pseudo element style index.
        /// </summary>
        public const int ExtendedPseudoElement = ScintillaConstants.SCE_CSS_EXTENDED_PSEUDOELEMENT;

        /// <summary>
        /// Media style index.
        /// </summary>
        public const int Media = ScintillaConstants.SCE_CSS_MEDIA;

        /// <summary>
        /// Variable style index.
        /// </summary>
        public const int Variable = ScintillaConstants.SCE_CSS_VARIABLE;
    }

    #endregion Css

    #region Fortran

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Fortran" /> lexer.
    /// </summary>
    public static class Fortran
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_F_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_F_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_F_NUMBER;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int String1 = ScintillaConstants.SCE_F_STRING1;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String2 = ScintillaConstants.SCE_F_STRING2;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_F_STRINGEOL;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_F_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_F_IDENTIFIER;

        /// <summary>
        /// Keyword (list 0) style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_F_WORD;

        /// <summary>
        /// Keyword 2 (list 1) style index.
        /// </summary>
        public const int Word2 = ScintillaConstants.SCE_F_WORD2;

        /// <summary>
        /// Keyword 3 (list 2) style index.
        /// </summary>
        public const int Word3 = ScintillaConstants.SCE_F_WORD3;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_F_PREPROCESSOR;

        /// <summary>
        /// Operator 2 style index.
        /// </summary>
        public const int Operator2 = ScintillaConstants.SCE_F_OPERATOR2;

        /// <summary>
        /// Label string style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_F_LABEL;

        /// <summary>
        /// Continuation style index.
        /// </summary>
        public const int Continuation = ScintillaConstants.SCE_F_CONTINUATION;
    }

    #endregion Fortran

    #region FreeBasic

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.FreeBasic" /> lexer.
    /// </summary>
    public static class FreeBasic
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_B_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_B_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_B_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_B_KEYWORD;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_B_STRING;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_B_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_B_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_B_IDENTIFIER;

        /// <summary>
        /// Date style index.
        /// </summary>
        public const int Date = ScintillaConstants.SCE_B_DATE;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_B_STRINGEOL;

        /// <summary>
        /// Keyword list 2 (index 1) style index.
        /// </summary>
        public const int Keyword2 = ScintillaConstants.SCE_B_KEYWORD2;

        /// <summary>
        /// Keyword list 3 (index 2) style index.
        /// </summary>
        public const int Keyword3 = ScintillaConstants.SCE_B_KEYWORD3;

        /// <summary>
        /// Keyword list 4 (index 3) style index.
        /// </summary>
        public const int Keyword4 = ScintillaConstants.SCE_B_KEYWORD4;

        /// <summary>
        /// Constant style index.
        /// </summary>
        public const int Constant = ScintillaConstants.SCE_B_CONSTANT;

        /// <summary>
        /// Inline assembler style index.
        /// </summary>
        public const int Asm = ScintillaConstants.SCE_B_ASM;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_B_LABEL;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_B_ERROR;

        /// <summary>
        /// Hexadecimal number style index.
        /// </summary>
        public const int HexNumber = ScintillaConstants.SCE_B_HEXNUMBER;

        /// <summary>
        /// Binary number style index.
        /// </summary>
        public const int BinNumber = ScintillaConstants.SCE_B_BINNUMBER;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_B_COMMENTBLOCK;

        /// <summary>
        /// Documentation line style index.
        /// </summary>
        public const int DocLine = ScintillaConstants.SCE_B_DOCLINE;

        /// <summary>
        /// Documentation block style index.
        /// </summary>
        public const int DocBlock = ScintillaConstants.SCE_B_DOCBLOCK;

        /// <summary>
        /// Documentation keyword style index.
        /// </summary>
        public const int DocKeyword = ScintillaConstants.SCE_B_DOCKEYWORD;
    }

    #endregion FreeBasic

    #region Html

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Html" /> lexer.
    /// </summary>
    public static class Html
    {
        /// <summary>
        /// Content style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_H_DEFAULT;

        /// <summary>
        /// Tag style index.
        /// </summary>
        public const int Tag = ScintillaConstants.SCE_H_TAG;

        /// <summary>
        /// Unknown tag style index.
        /// </summary>
        public const int TagUnknown = ScintillaConstants.SCE_H_TAGUNKNOWN;

        /// <summary>
        /// Attribute style index.
        /// </summary>
        public const int Attribute = ScintillaConstants.SCE_H_ATTRIBUTE;

        /// <summary>
        /// Unknown attribute style index.
        /// </summary>
        public const int AttributeUnknown = ScintillaConstants.SCE_H_ATTRIBUTEUNKNOWN;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_H_NUMBER;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int DoubleString = ScintillaConstants.SCE_H_DOUBLESTRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int SingleString = ScintillaConstants.SCE_H_SINGLESTRING;

        /// <summary>
        /// Other tag content (not elements or attributes) style index.
        /// </summary>
        public const int Other = ScintillaConstants.SCE_H_OTHER;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_H_COMMENT;

        /// <summary>
        /// Entity ($nnn;) name style index.
        /// </summary>
        public const int Entity = ScintillaConstants.SCE_H_ENTITY;

        /// <summary>
        /// End-tag style index.
        /// </summary>
        public const int TagEnd = ScintillaConstants.SCE_H_TAGEND;

        /// <summary>
        /// Start of XML declaration (&lt;?xml&gt;) style index.
        /// </summary>
        public const int XmlStart = ScintillaConstants.SCE_H_XMLSTART;

        /// <summary>
        /// End of XML declaration (?&gt;) style index.
        /// </summary>
        public const int XmlEnd = ScintillaConstants.SCE_H_XMLEND;

        /// <summary>
        /// Script tag (&lt;script&gt;) style index.
        /// </summary>
        public const int Script = ScintillaConstants.SCE_H_SCRIPT;

        /// <summary>
        /// ASP-like script engine block (&lt;%) style index.
        /// </summary>
        public const int Asp = ScintillaConstants.SCE_H_ASP;

        /// <summary>
        /// ASP-like language declaration (&lt;%@) style index.
        /// </summary>
        public const int AspAt = ScintillaConstants.SCE_H_ASPAT;

        /// <summary>
        /// CDATA section style index.
        /// </summary>
        public const int CData = ScintillaConstants.SCE_H_CDATA;

        /// <summary>
        /// Question mark style index.
        /// </summary>
        public const int Question = ScintillaConstants.SCE_H_QUESTION;

        /// <summary>
        /// Value style index.
        /// </summary>
        public const int Value = ScintillaConstants.SCE_H_VALUE;

        /// <summary>
        /// Script engine comment (&lt;%--) style index.
        /// </summary>
        public const int XcComment = ScintillaConstants.SCE_H_XCCOMMENT;
    }

    #endregion Html

    #region JavaScript

    /// <summary>
    /// Embedded JavaScript style constants for use with the <see cref="Lexer.Html" /> lexer.
    /// </summary>
    public static class JavaScript
    {
        /// <summary>
        /// Start style index (allows EOL filled background to not start on same line as SCRIPT tag).
        /// </summary>
        public const int Start = ScintillaConstants.SCE_HJ_START;

        /// <summary>
        /// Default style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_HJ_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_HJ_COMMENT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_HJ_COMMENTLINE;

        /// <summary>
        /// Doc comment style index.
        /// </summary>
        public const int CommentDoc = ScintillaConstants.SCE_HJ_COMMENTDOC;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_HJ_NUMBER;

        /// <summary>
        /// Word style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_HJ_WORD;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_HJ_KEYWORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int DoubleString = ScintillaConstants.SCE_HJ_DOUBLESTRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int SingleString = ScintillaConstants.SCE_HJ_SINGLESTRING;

        /// <summary>
        /// Symbols style index.
        /// </summary>
        public const int Symbols = ScintillaConstants.SCE_HJ_SYMBOLS;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_HJ_STRINGEOL;

        /// <summary>
        /// Regular expression style index.
        /// </summary>
        public const int Regex = ScintillaConstants.SCE_HJ_REGEX;
    }

    #endregion JavaScript

    #region Json

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Json" /> lexer.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_JSON_DEFAULT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_JSON_NUMBER;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_JSON_STRING;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_JSON_STRINGEOL;

        /// <summary>
        /// Property name style index.
        /// </summary>
        public const int PropertyName = ScintillaConstants.SCE_JSON_PROPERTYNAME;

        /// <summary>
        /// Escape sequence style index.
        /// </summary>
        public const int EscapeSequence = ScintillaConstants.SCE_JSON_ESCAPESEQUENCE;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int LineComment = ScintillaConstants.SCE_JSON_LINECOMMENT;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int BlockComment = ScintillaConstants.SCE_JSON_BLOCKCOMMENT;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_JSON_OPERATOR;

        /// <summary>
        /// URI style index.
        /// </summary>
        public const int Uri = ScintillaConstants.SCE_JSON_URI;

        /// <summary>
        /// Compact Internationalized Resource Identifier (IRI) style index.
        /// </summary>
        public const int CompactIRI = ScintillaConstants.SCE_JSON_COMPACTIRI;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_JSON_KEYWORD;

        /// <summary>
        /// Linked data (LD) keyword style index.
        /// </summary>
        public const int LdKeyword = ScintillaConstants.SCE_JSON_LDKEYWORD;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_JSON_ERROR;
    }

    #endregion Json

    #region Lisp

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Lisp" /> lexer.
    /// </summary>
    public static class Lisp
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_LISP_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_LISP_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_LISP_NUMBER;

        /// <summary>
        /// Functions and special operators (list 0) style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_LISP_KEYWORD;

        /// <summary>
        /// Keywords (list 1) style index.
        /// </summary>
        public const int KeywordKw = ScintillaConstants.SCE_LISP_KEYWORD_KW;

        /// <summary>
        /// Symbol style index.
        /// </summary>
        public const int Symbol = ScintillaConstants.SCE_LISP_SYMBOL;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_LISP_STRING;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_LISP_STRINGEOL;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_LISP_IDENTIFIER;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_LISP_OPERATOR;

        /// <summary>
        /// Special character style index.
        /// </summary>
        public const int Special = ScintillaConstants.SCE_LISP_SPECIAL;

        /// <summary>
        /// Multi-line comment style index.
        /// </summary>
        public const int MultiComment = ScintillaConstants.SCE_LISP_MULTI_COMMENT;
    }

    #endregion Lisp

    #region Lua

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Lua" /> lexer.
    /// </summary>
    public static class Lua
    {
        /// <summary>
        /// Default style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_LUA_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_LUA_COMMENT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_LUA_COMMENTLINE;

        /// <summary>
        /// Documentation comment style index.
        /// </summary>
        public const int CommentDoc = ScintillaConstants.SCE_LUA_COMMENTDOC;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_LUA_NUMBER;

        /// <summary>
        /// Keyword list 1 (index 0) style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_LUA_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_LUA_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_LUA_CHARACTER;

        /// <summary>
        /// Literal string style index.
        /// </summary>
        public const int LiteralString = ScintillaConstants.SCE_LUA_LITERALSTRING;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_LUA_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_LUA_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_LUA_IDENTIFIER;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_LUA_STRINGEOL;

        /// <summary>
        /// Keywords list 2 (index 1) style index.
        /// </summary>
        public const int Word2 = ScintillaConstants.SCE_LUA_WORD2;

        /// <summary>
        /// Keywords list 3 (index 2) style index.
        /// </summary>
        public const int Word3 = ScintillaConstants.SCE_LUA_WORD3;

        /// <summary>
        /// Keywords list 4 (index 3) style index.
        /// </summary>
        public const int Word4 = ScintillaConstants.SCE_LUA_WORD4;

        /// <summary>
        /// Keywords list 5 (index 4) style index.
        /// </summary>
        public const int Word5 = ScintillaConstants.SCE_LUA_WORD5;

        /// <summary>
        /// Keywords list 6 (index 5) style index.
        /// </summary>
        public const int Word6 = ScintillaConstants.SCE_LUA_WORD6;

        /// <summary>
        /// Keywords list 7 (index 6) style index.
        /// </summary>
        public const int Word7 = ScintillaConstants.SCE_LUA_WORD7;

        /// <summary>
        /// Keywords list 8 (index 7) style index.
        /// </summary>
        public const int Word8 = ScintillaConstants.SCE_LUA_WORD8;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_LUA_LABEL;
    }

    #endregion Lua

    #region Matlab

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Matlab" /> lexer.
    /// </summary>
    public static class Matlab
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_MATLAB_DEFAULT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_MATLAB_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_MATLAB_NUMBER;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_MATLAB_STRING;

        /// <summary>
        /// Command style index.
        /// </summary>
        public const int Command = ScintillaConstants.SCE_MATLAB_COMMAND;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_MATLAB_KEYWORD;

        /// <summary>
        /// Double quote string style index.
        /// </summary>
        public const int DoubleQuoteString = ScintillaConstants.SCE_MATLAB_DOUBLEQUOTESTRING;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_MATLAB_IDENTIFIER;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_MATLAB_OPERATOR;
    }

    #endregion Matlab

    #region Pascal

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Pascal" /> lexer.
    /// </summary>
    public static class Pascal
    {
        /// <summary>
        /// Default style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_PAS_DEFAULT;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_PAS_IDENTIFIER;

        /// <summary>
        /// Comment style '{' index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_PAS_COMMENT;

        /// <summary>
        /// Comment style 2 "(*" index.
        /// </summary>
        public const int Comment2 = ScintillaConstants.SCE_PAS_COMMENT2;

        /// <summary>
        /// Comment line style "//" index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_PAS_COMMENTLINE;

        /// <summary>
        /// Preprocessor style "{$" index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_PAS_PREPROCESSOR;

        /// <summary>
        /// Preprocessor style 2 "(*$" index.
        /// </summary>
        public const int Preprocessor2 = ScintillaConstants.SCE_PAS_PREPROCESSOR2;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_PAS_NUMBER;

        /// <summary>
        /// Hexadecimal number style index.
        /// </summary>
        public const int HexNumber = ScintillaConstants.SCE_PAS_HEXNUMBER;

        /// <summary>
        /// Word (keyword set 0) style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_PAS_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_PAS_STRING;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_PAS_STRINGEOL;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_PAS_CHARACTER;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_PAS_OPERATOR;

        /// <summary>
        /// Assembly style index.
        /// </summary>
        public const int Asm = ScintillaConstants.SCE_PAS_ASM;
    }

    #endregion Pascal

    #region Perl

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Perl" /> lexer.
    /// </summary>
    public static class Perl
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_PL_DEFAULT;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_PL_ERROR;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_PL_COMMENTLINE;

        /// <summary>
        /// POD style index.
        /// </summary>
        public const int Pod = ScintillaConstants.SCE_PL_POD;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_PL_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_PL_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_PL_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_PL_CHARACTER;

        /// <summary>
        /// Punctuation style index.
        /// </summary>
        public const int Punctuation = ScintillaConstants.SCE_PL_PUNCTUATION;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_PL_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_PL_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_PL_IDENTIFIER;

        /// <summary>
        /// Scalar style index.
        /// </summary>
        public const int Scalar = ScintillaConstants.SCE_PL_SCALAR;

        /// <summary>
        /// Array style index.
        /// </summary>
        public const int Array = ScintillaConstants.SCE_PL_ARRAY;

        /// <summary>
        /// Hash style index.
        /// </summary>
        public const int Hash = ScintillaConstants.SCE_PL_HASH;

        /// <summary>
        /// Symbol table style index.
        /// </summary>
        public const int SymbolTable = ScintillaConstants.SCE_PL_SYMBOLTABLE;

        /// <summary>
        /// Variable indexer index.
        /// </summary>
        public const int VariableIndexer = ScintillaConstants.SCE_PL_VARIABLE_INDEXER;

        /// <summary>
        /// Regular expression style index.
        /// </summary>
        public const int Regex = ScintillaConstants.SCE_PL_REGEX;

        /// <summary>
        /// RegSubst style index.
        /// </summary>
        public const int RegSubst = ScintillaConstants.SCE_PL_REGSUBST;

        // public const int LongQuote = ScintillaConstants.SCE_PL_LONGQUOTE;

        /// <summary>
        /// Backtick (grave accent, backquote) style index.
        /// </summary>
        public const int BackTicks = ScintillaConstants.SCE_PL_BACKTICKS;

        /// <summary>
        /// Data section style index.
        /// </summary>
        public const int DataSection = ScintillaConstants.SCE_PL_DATASECTION;

        /// <summary>
        /// HereDoc delimiter style index.
        /// </summary>
        public const int HereDelim = ScintillaConstants.SCE_PL_HERE_DELIM;

        /// <summary>
        /// HereDoc single-quote style index.
        /// </summary>
        public const int HereQ = ScintillaConstants.SCE_PL_HERE_Q;

        /// <summary>
        /// HereDoc double-quote style index.
        /// </summary>
        public const int HereQq = ScintillaConstants.SCE_PL_HERE_QQ;

        /// <summary>
        /// HereDoc backtick style index.
        /// </summary>
        public const int HereQx = ScintillaConstants.SCE_PL_HERE_QX;

        /// <summary>
        /// Q quote style index.
        /// </summary>
        public const int StringQ = ScintillaConstants.SCE_PL_STRING_Q;

        /// <summary>
        /// QQ quote style index.
        /// </summary>
        public const int StringQq = ScintillaConstants.SCE_PL_STRING_QQ;

        /// <summary>
        /// QZ quote style index.
        /// </summary>
        public const int StringQx = ScintillaConstants.SCE_PL_STRING_QX;

        /// <summary>
        /// QR quote style index.
        /// </summary>
        public const int StringQr = ScintillaConstants.SCE_PL_STRING_QR;

        /// <summary>
        /// QW quote style index.
        /// </summary>
        public const int StringQw = ScintillaConstants.SCE_PL_STRING_QW;

        /// <summary>
        /// POD verbatim style index.
        /// </summary>
        public const int PodVerb = ScintillaConstants.SCE_PL_POD_VERB;

        /// <summary>
        /// Subroutine prototype style index.
        /// </summary>
        public const int SubPrototype = ScintillaConstants.SCE_PL_SUB_PROTOTYPE;

        /// <summary>
        /// Format identifier style index.
        /// </summary>
        public const int FormatIdent = ScintillaConstants.SCE_PL_FORMAT_IDENT;

        /// <summary>
        /// Format style index.
        /// </summary>
        public const int Format = ScintillaConstants.SCE_PL_FORMAT;

        /// <summary>
        /// String variable style index.
        /// </summary>
        public const int StringVar = ScintillaConstants.SCE_PL_STRING_VAR;

        /// <summary>
        /// XLAT style index.
        /// </summary>
        public const int XLat = ScintillaConstants.SCE_PL_XLAT;

        /// <summary>
        /// Regular expression variable style index.
        /// </summary>
        public const int RegexVar = ScintillaConstants.SCE_PL_REGEX_VAR;

        /// <summary>
        /// RegSubst variable style index.
        /// </summary>
        public const int RegSubstVar = ScintillaConstants.SCE_PL_REGSUBST_VAR;

        /// <summary>
        /// Backticks variable style index.
        /// </summary>
        public const int BackticksVar = ScintillaConstants.SCE_PL_BACKTICKS_VAR;

        /// <summary>
        /// HereDoc QQ quote variable style index.
        /// </summary>
        public const int HereQqVar = ScintillaConstants.SCE_PL_HERE_QQ_VAR;

        /// <summary>
        /// HereDoc QX quote variable style index.
        /// </summary>
        public const int HereQxVar = ScintillaConstants.SCE_PL_HERE_QX_VAR;

        /// <summary>
        /// QQ quote variable style index.
        /// </summary>
        public const int StringQqVar = ScintillaConstants.SCE_PL_STRING_QQ_VAR;

        /// <summary>
        /// QX quote variable style index.
        /// </summary>
        public const int StringQxVar = ScintillaConstants.SCE_PL_STRING_QX_VAR;

        /// <summary>
        /// QR quote variable style index.
        /// </summary>
        public const int StringQrVar = ScintillaConstants.SCE_PL_STRING_QR_VAR;
    }

    #endregion Perl

    #region PhpScript

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.PhpScript" /> lexer.
    /// </summary>
    public static class PhpScript
    {
        /// <summary>
        /// Complex Variable style index.
        /// </summary>
        public const int ComplexVariable = ScintillaConstants.SCE_HPHP_COMPLEX_VARIABLE;

        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_HPHP_DEFAULT;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int HString = ScintillaConstants.SCE_HPHP_HSTRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int SimpleString = ScintillaConstants.SCE_HPHP_SIMPLESTRING;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_HPHP_WORD;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_HPHP_NUMBER;

        /// <summary>
        /// Variable style index.
        /// </summary>
        public const int Variable = ScintillaConstants.SCE_HPHP_VARIABLE;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_HPHP_COMMENT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_HPHP_COMMENTLINE;

        /// <summary>
        /// Double-quoted string variable style index.
        /// </summary>
        public const int HStringVariable = ScintillaConstants.SCE_HPHP_HSTRING_VARIABLE;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_HPHP_OPERATOR;
    }

    #endregion PhpScript

    #region PowerShell

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.PowerShell" /> lexer.
    /// </summary>
    public static class PowerShell
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_POWERSHELL_DEFAULT;

        /// <summary>
        /// Line comment style index
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_POWERSHELL_COMMENT;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_POWERSHELL_STRING;

        /// <summary>
        /// Character style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_POWERSHELL_CHARACTER;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_POWERSHELL_NUMBER;

        /// <summary>
        /// Variable style index.
        /// </summary>
        public const int Variable = ScintillaConstants.SCE_POWERSHELL_VARIABLE;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_POWERSHELL_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_POWERSHELL_IDENTIFIER;

        /// <summary>
        /// Keyword (set 0) style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_POWERSHELL_KEYWORD;

        /// <summary>
        /// Cmdlet (set 1) style index.
        /// </summary>
        public const int Cmdlet = ScintillaConstants.SCE_POWERSHELL_CMDLET;

        /// <summary>
        /// Alias (set 2) style index.
        /// </summary>
        public const int Alias = ScintillaConstants.SCE_POWERSHELL_ALIAS;

        /// <summary>
        /// Function (set 3) style index.
        /// </summary>
        public const int Function = ScintillaConstants.SCE_POWERSHELL_FUNCTION;

        /// <summary>
        /// User word (set 4) style index.
        /// </summary>
        public const int User1 = ScintillaConstants.SCE_POWERSHELL_USER1;

        /// <summary>
        /// Multi-line comment style index.
        /// </summary>
        public const int CommentStream = ScintillaConstants.SCE_POWERSHELL_COMMENTSTREAM;

        /// <summary>
        /// Here string style index.
        /// </summary>
        public const int HereString = ScintillaConstants.SCE_POWERSHELL_HERE_STRING;

        /// <summary>
        /// Here character style index.
        /// </summary>
        public const int HereCharacter = ScintillaConstants.SCE_POWERSHELL_HERE_CHARACTER;

        /// <summary>
        /// Comment based help keyword style index.
        /// </summary>
        public const int CommentDocKeyword = ScintillaConstants.SCE_POWERSHELL_COMMENTDOCKEYWORD;
    }

    #endregion PowerShell

    #region Properties

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Properties" /> lexer.
    /// </summary>
    public static class Properties
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_PROPS_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_PROPS_COMMENT;

        /// <summary>
        /// Section style index.
        /// </summary>
        public const int Section = ScintillaConstants.SCE_PROPS_SECTION;

        /// <summary>
        /// Assignment operator index.
        /// </summary>
        public const int Assignment = ScintillaConstants.SCE_PROPS_ASSIGNMENT;

        /// <summary>
        /// Default (registry-only) value index.
        /// </summary>
        public const int DefVal = ScintillaConstants.SCE_PROPS_DEFVAL;

        /// <summary>
        /// Key style index.
        /// </summary>
        public const int Key = ScintillaConstants.SCE_PROPS_KEY;
    }

    #endregion Properties

    #region PureBasic

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.PureBasic" /> lexer.
    /// </summary>
    public static class PureBasic
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_B_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_B_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_B_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_B_KEYWORD;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_B_STRING;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_B_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_B_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_B_IDENTIFIER;

        /// <summary>
        /// Date style index.
        /// </summary>
        public const int Date = ScintillaConstants.SCE_B_DATE;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_B_STRINGEOL;

        /// <summary>
        /// Keyword list 2 (index 1) style index.
        /// </summary>
        public const int Keyword2 = ScintillaConstants.SCE_B_KEYWORD2;

        /// <summary>
        /// Keyword list 3 (index 2) style index.
        /// </summary>
        public const int Keyword3 = ScintillaConstants.SCE_B_KEYWORD3;

        /// <summary>
        /// Keyword list 4 (index 3) style index.
        /// </summary>
        public const int Keyword4 = ScintillaConstants.SCE_B_KEYWORD4;

        /// <summary>
        /// Constant style index.
        /// </summary>
        public const int Constant = ScintillaConstants.SCE_B_CONSTANT;

        /// <summary>
        /// Inline assembler style index.
        /// </summary>
        public const int Asm = ScintillaConstants.SCE_B_ASM;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_B_LABEL;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_B_ERROR;

        /// <summary>
        /// Hexadecimal number style index.
        /// </summary>
        public const int HexNumber = ScintillaConstants.SCE_B_HEXNUMBER;

        /// <summary>
        /// Binary number style index.
        /// </summary>
        public const int BinNumber = ScintillaConstants.SCE_B_BINNUMBER;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_B_COMMENTBLOCK;

        /// <summary>
        /// Documentation line style index.
        /// </summary>
        public const int DocLine = ScintillaConstants.SCE_B_DOCLINE;

        /// <summary>
        /// Documentation block style index.
        /// </summary>
        public const int DocBlock = ScintillaConstants.SCE_B_DOCBLOCK;

        /// <summary>
        /// Documentation keyword style index.
        /// </summary>
        public const int DocKeyword = ScintillaConstants.SCE_B_DOCKEYWORD;
    }

    #endregion PureBasic

    #region Python

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Python" /> lexer.
    /// </summary>
    public static class Python
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_P_DEFAULT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_P_COMMENTLINE;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_P_NUMBER;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_P_STRING;

        /// <summary>
        /// Single-quote style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_P_CHARACTER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_P_WORD;

        /// <summary>
        /// Triple single-quote style index.
        /// </summary>
        public const int Triple = ScintillaConstants.SCE_P_TRIPLE;

        /// <summary>
        /// Triple double-quote style index.
        /// </summary>
        public const int TripleDouble = ScintillaConstants.SCE_P_TRIPLEDOUBLE;

        /// <summary>
        /// Class name style index.
        /// </summary>
        public const int ClassName = ScintillaConstants.SCE_P_CLASSNAME;

        /// <summary>
        /// Function or method name style index.
        /// </summary>
        public const int DefName = ScintillaConstants.SCE_P_DEFNAME;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_P_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_P_IDENTIFIER;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_P_COMMENTBLOCK;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_P_STRINGEOL;

        /// <summary>
        /// Keyword style 2 index.
        /// </summary>
        public const int Word2 = ScintillaConstants.SCE_P_WORD2;

        /// <summary>
        /// Decorator style index.
        /// </summary>
        public const int Decorator = ScintillaConstants.SCE_P_DECORATOR;
    }

    #endregion Python

    #region Ruby

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Ruby" /> lexer.
    /// </summary>
    public static class Ruby
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_RB_DEFAULT;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_RB_ERROR;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_RB_COMMENTLINE;

        /// <summary>
        /// POD style index.
        /// </summary>
        public const int Pod = ScintillaConstants.SCE_RB_POD;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_RB_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_RB_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_RB_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_RB_CHARACTER;

        /// <summary>
        /// Class name style index.
        /// </summary>
        public const int ClassName = ScintillaConstants.SCE_RB_CLASSNAME;

        /// <summary>
        /// Definition style index.
        /// </summary>
        public const int DefName = ScintillaConstants.SCE_RB_DEFNAME;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_RB_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_RB_IDENTIFIER;

        /// <summary>
        /// Regular expression style index.
        /// </summary>
        public const int Regex = ScintillaConstants.SCE_RB_REGEX;

        /// <summary>
        /// Global style index.
        /// </summary>
        public const int Global = ScintillaConstants.SCE_RB_GLOBAL;

        /// <summary>
        /// Symbol style index.
        /// </summary>
        public const int Symbol = ScintillaConstants.SCE_RB_SYMBOL;

        /// <summary>
        /// Module name style index.
        /// </summary>
        public const int ModuleName = ScintillaConstants.SCE_RB_MODULE_NAME;

        /// <summary>
        /// Instance variable style index.
        /// </summary>
        public const int InstanceVar = ScintillaConstants.SCE_RB_INSTANCE_VAR;

        /// <summary>
        /// Class variable style index.
        /// </summary>
        public const int ClassVar = ScintillaConstants.SCE_RB_CLASS_VAR;

        /// <summary>
        /// Backticks style index.
        /// </summary>
        public const int BackTicks = ScintillaConstants.SCE_RB_BACKTICKS;

        /// <summary>
        /// Data section style index.
        /// </summary>
        public const int DataSection = ScintillaConstants.SCE_RB_DATASECTION;

        /// <summary>
        /// HereDoc delimiter style index.
        /// </summary>
        public const int HereDelim = ScintillaConstants.SCE_RB_HERE_DELIM;

        /// <summary>
        /// HereDoc Q quote style index.
        /// </summary>
        public const int HereQ = ScintillaConstants.SCE_RB_HERE_Q;

        /// <summary>
        /// HereDoc QQ quote style index.
        /// </summary>
        public const int HereQq = ScintillaConstants.SCE_RB_HERE_QQ;

        /// <summary>
        /// HereDoc QX quote style index.
        /// </summary>
        public const int HereQx = ScintillaConstants.SCE_RB_HERE_QX;

        /// <summary>
        /// Q quote string style index.
        /// </summary>
        public const int StringQ = ScintillaConstants.SCE_RB_STRING_Q;

        /// <summary>
        /// QQ quote string style index.
        /// </summary>
        public const int StringQq = ScintillaConstants.SCE_RB_STRING_QQ;

        /// <summary>
        /// QX quote string style index.
        /// </summary>
        public const int StringQx = ScintillaConstants.SCE_RB_STRING_QX;

        /// <summary>
        /// QR quote string style index.
        /// </summary>
        public const int StringQr = ScintillaConstants.SCE_RB_STRING_QR;

        /// <summary>
        /// QW quote style index.
        /// </summary>
        public const int StringQw = ScintillaConstants.SCE_RB_STRING_QW;

        /// <summary>
        /// Demoted keyword style index.
        /// </summary>
        public const int WordDemoted = ScintillaConstants.SCE_RB_WORD_DEMOTED;

        /// <summary>
        /// Standard-in style index.
        /// </summary>
        public const int StdIn = ScintillaConstants.SCE_RB_STDIN;

        /// <summary>
        /// Standard-out style index.
        /// </summary>
        public const int StdOut = ScintillaConstants.SCE_RB_STDOUT;

        /// <summary>
        /// Standard-error style index.
        /// </summary>
        public const int StdErr = ScintillaConstants.SCE_RB_STDERR;

        // public const int UpperBound = ScintillaConstants.SCE_RB_UPPER_BOUND;
    }

    #endregion Ruby

    #region Smalltalk

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Smalltalk" /> lexer.
    /// </summary>
    public static class Smalltalk
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_ST_DEFAULT;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_ST_STRING;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_ST_NUMBER;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_ST_COMMENT;

        /// <summary>
        /// Symbol style index.
        /// </summary>
        public const int Symbol = ScintillaConstants.SCE_ST_SYMBOL;

        /// <summary>
        /// Binary style index.
        /// </summary>
        public const int Binary = ScintillaConstants.SCE_ST_BINARY;

        /// <summary>
        /// Bool style index.
        /// </summary>
        public const int Bool = ScintillaConstants.SCE_ST_BOOL;

        /// <summary>
        /// Self style index.
        /// </summary>
        public const int Self = ScintillaConstants.SCE_ST_SELF;

        /// <summary>
        /// Super style index.
        /// </summary>
        public const int Super = ScintillaConstants.SCE_ST_SUPER;

        /// <summary>
        /// NIL style index.
        /// </summary>
        public const int Nil = ScintillaConstants.SCE_ST_NIL;

        /// <summary>
        /// Global style index.
        /// </summary>
        public const int Global = ScintillaConstants.SCE_ST_GLOBAL;

        /// <summary>
        /// Return style index.
        /// </summary>
        public const int Return = ScintillaConstants.SCE_ST_RETURN;

        /// <summary>
        /// Special style index.
        /// </summary>
        public const int Special = ScintillaConstants.SCE_ST_SPECIAL;

        /// <summary>
        /// KWS End style index.
        /// </summary>
        public const int KwsEnd = ScintillaConstants.SCE_ST_KWSEND;

        /// <summary>
        /// Assign style index.
        /// </summary>
        public const int Assign = ScintillaConstants.SCE_ST_ASSIGN;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_ST_CHARACTER;

        /// <summary>
        /// Special selector style index.
        /// </summary>
        public const int SpecSel = ScintillaConstants.SCE_ST_SPEC_SEL;
    }

    #endregion Smalltalk

    #region Sql

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Sql" /> lexer.
    /// </summary>
    public static class Sql
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_SQL_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_SQL_COMMENT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_SQL_COMMENTLINE;

        /// <summary>
        /// Documentation comment style index.
        /// </summary>
        public const int CommentDoc = ScintillaConstants.SCE_SQL_COMMENTDOC;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_SQL_NUMBER;

        /// <summary>
        /// Keyword list 1 (index 0) style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_SQL_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_SQL_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = ScintillaConstants.SCE_SQL_CHARACTER;

        /// <summary>
        /// Keyword from the SQL*Plus list (index 3) style index.
        /// </summary>
        public const int SqlPlus = ScintillaConstants.SCE_SQL_SQLPLUS;

        /// <summary>
        /// SQL*Plus prompt style index.
        /// </summary>
        public const int SqlPlusPrompt = ScintillaConstants.SCE_SQL_SQLPLUS_PROMPT;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_SQL_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_SQL_IDENTIFIER;

        /// <summary>
        /// SQL*Plus comment style index.
        /// </summary>
        public const int SqlPlusComment = ScintillaConstants.SCE_SQL_SQLPLUS_COMMENT;

        /// <summary>
        /// Documentation line comment style index.
        /// </summary>
        public const int CommentLineDoc = ScintillaConstants.SCE_SQL_COMMENTLINEDOC;

        /// <summary>
        /// Keyword list 2 (index 1) style index.
        /// </summary>
        public const int Word2 = ScintillaConstants.SCE_SQL_WORD2;

        /// <summary>
        /// Documentation (Doxygen) keyword style index.
        /// </summary>
        public const int CommentDocKeyword = ScintillaConstants.SCE_SQL_COMMENTDOCKEYWORD;

        /// <summary>
        /// Documentation (Doxygen) keyword error style index.
        /// </summary>
        public const int CommentDocKeywordError = ScintillaConstants.SCE_SQL_COMMENTDOCKEYWORDERROR;

        /// <summary>
        /// Keyword user-list 1 (index 4) style index.
        /// </summary>
        public const int User1 = ScintillaConstants.SCE_SQL_USER1;

        /// <summary>
        /// Keyword user-list 2 (index 5) style index.
        /// </summary>
        public const int User2 = ScintillaConstants.SCE_SQL_USER2;

        /// <summary>
        /// Keyword user-list 3 (index 6) style index.
        /// </summary>
        public const int User3 = ScintillaConstants.SCE_SQL_USER3;

        /// <summary>
        /// Keyword user-list 4 (index 7) style index.
        /// </summary>
        public const int User4 = ScintillaConstants.SCE_SQL_USER4;

        /// <summary>
        /// Quoted identifier style index.
        /// </summary>
        public const int QuotedIdentifier = ScintillaConstants.SCE_SQL_QUOTEDIDENTIFIER;

        /// <summary>
        /// Q operator style index.
        /// </summary>
        public const int QOperator = ScintillaConstants.SCE_SQL_QOPERATOR;
    }

    #endregion Sql

    #region Markdown

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Markdown" /> lexer.
    /// </summary>
    public static class Markdown
    {
        /// <summary>
        /// Default text style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_MARKDOWN_DEFAULT;

        /// <summary>
        /// Line begin style index.
        /// </summary>
        public const int LineBegin = ScintillaConstants.SCE_MARKDOWN_LINE_BEGIN;

        /// <summary>
        /// Strong type 1 style index.
        /// </summary>
        public const int Strong1 = ScintillaConstants.SCE_MARKDOWN_STRONG1;

        /// <summary>
        /// Strong type 2 style index.
        /// </summary>
        public const int Strong2 = ScintillaConstants.SCE_MARKDOWN_STRONG2;

        /// <summary>
        /// Empasis type 1 style index.
        /// </summary>
        public const int Em1 = ScintillaConstants.SCE_MARKDOWN_EM1;

        /// <summary>
        /// Empasis type 2 style index.
        /// </summary>
        public const int Em2 = ScintillaConstants.SCE_MARKDOWN_EM2;

        /// <summary>
        /// Header type 1 style index.
        /// </summary>
        public const int Header1 = ScintillaConstants.SCE_MARKDOWN_HEADER1;

        /// <summary>
        /// Header type 2 style index.
        /// </summary>
        public const int Header2 = ScintillaConstants.SCE_MARKDOWN_HEADER2;

        /// <summary>
        /// Header type 3 style index.
        /// </summary>
        public const int Header3 = ScintillaConstants.SCE_MARKDOWN_HEADER3;

        /// <summary>
        /// Header type 4 style index.
        /// </summary>
        public const int Header4 = ScintillaConstants.SCE_MARKDOWN_HEADER4;

        /// <summary>
        /// Header type 5 style index.
        /// </summary>
        public const int Header5 = ScintillaConstants.SCE_MARKDOWN_HEADER5;

        /// <summary>
        /// Header type 6 style index.
        /// </summary>
        public const int Header6 = ScintillaConstants.SCE_MARKDOWN_HEADER6;

        /// <summary>
        /// Pre char style index.
        /// </summary>
        public const int PreChar = ScintillaConstants.SCE_MARKDOWN_PRECHAR;

        /// <summary>
        /// Unordered list style index.
        /// </summary>
        public const int UListItem = ScintillaConstants.SCE_MARKDOWN_ULIST_ITEM;

        /// <summary>
        /// Ordered list style index.
        /// </summary>
        public const int OListItem = ScintillaConstants.SCE_MARKDOWN_OLIST_ITEM;

        /// <summary>
        /// Blockquote style index.
        /// </summary>
        public const int BlockQuote = ScintillaConstants.SCE_MARKDOWN_BLOCKQUOTE;

        /// <summary>
        /// Strikeout style index.
        /// </summary>
        public const int Strikeout = ScintillaConstants.SCE_MARKDOWN_STRIKEOUT;

        /// <summary>
        /// Horizontal rule style index.
        /// </summary>
        public const int HRule = ScintillaConstants.SCE_MARKDOWN_HRULE;

        /// <summary>
        /// Link style index.
        /// </summary>
        public const int Link = ScintillaConstants.SCE_MARKDOWN_LINK;

        /// <summary>
        /// Code type 1 style index.
        /// </summary>
        public const int Code = ScintillaConstants.SCE_MARKDOWN_CODE;

        /// <summary>
        /// Code type 2 style index.
        /// </summary>
        public const int Code2 = ScintillaConstants.SCE_MARKDOWN_CODE2;

        /// <summary>
        /// Code block style index.
        /// </summary>
        public const int CodeBk = ScintillaConstants.SCE_MARKDOWN_CODEBK;
    }

    #endregion Markdown

    #region R

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.R" /> lexer.
    /// </summary>
    public static class R
    {
        /// <summary>
        /// Default style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_R_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_R_COMMENT;

        /// <summary>
        /// Keyword (set 0) style index.
        /// </summary>
        public const int KWord = ScintillaConstants.SCE_R_KWORD;

        /// <summary>
        /// Base keyword (set 1) style index.
        /// </summary>
        public const int BaseKWord = ScintillaConstants.SCE_R_BASEKWORD;

        /// <summary>
        /// Other keyword (set 2) style index.
        /// </summary>
        public const int OtherKWord = ScintillaConstants.SCE_R_OTHERKWORD;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_R_NUMBER;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_R_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int String2 = ScintillaConstants.SCE_R_STRING2;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_R_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_R_IDENTIFIER;

        /// <summary>
        /// Infix style index.
        /// </summary>
        public const int Infix = ScintillaConstants.SCE_R_INFIX;

        /// <summary>
        /// Unclosed infix EOL style index.
        /// </summary>
        public const int InfixEol = ScintillaConstants.SCE_R_INFIXEOL;
    }

    #endregion R

    #region Vb

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Vb" /> lexer.
    /// </summary>
    public static class Vb
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_B_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_B_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_B_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_B_KEYWORD;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_B_STRING;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_B_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_B_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_B_IDENTIFIER;

        /// <summary>
        /// Date style index.
        /// </summary>
        public const int Date = ScintillaConstants.SCE_B_DATE;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_B_STRINGEOL;

        /// <summary>
        /// Keyword list 2 (index 1) style index.
        /// </summary>
        public const int Keyword2 = ScintillaConstants.SCE_B_KEYWORD2;

        /// <summary>
        /// Keyword list 3 (index 2) style index.
        /// </summary>
        public const int Keyword3 = ScintillaConstants.SCE_B_KEYWORD3;

        /// <summary>
        /// Keyword list 4 (index 3) style index.
        /// </summary>
        public const int Keyword4 = ScintillaConstants.SCE_B_KEYWORD4;

        /// <summary>
        /// Constant style index.
        /// </summary>
        public const int Constant = ScintillaConstants.SCE_B_CONSTANT;

        /// <summary>
        /// Inline assembler style index.
        /// </summary>
        public const int Asm = ScintillaConstants.SCE_B_ASM;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_B_LABEL;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_B_ERROR;

        /// <summary>
        /// Hexadecimal number style index.
        /// </summary>
        public const int HexNumber = ScintillaConstants.SCE_B_HEXNUMBER;

        /// <summary>
        /// Binary number style index.
        /// </summary>
        public const int BinNumber = ScintillaConstants.SCE_B_BINNUMBER;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_B_COMMENTBLOCK;

        /// <summary>
        /// Documentation line style index.
        /// </summary>
        public const int DocLine = ScintillaConstants.SCE_B_DOCLINE;

        /// <summary>
        /// Documentation block style index.
        /// </summary>
        public const int DocBlock = ScintillaConstants.SCE_B_DOCBLOCK;

        /// <summary>
        /// Documentation keyword style index.
        /// </summary>
        public const int DocKeyword = ScintillaConstants.SCE_B_DOCKEYWORD;
    }

    #endregion Vb

    #region VbScript

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.VbScript" /> lexer.
    /// </summary>
    public static class VbScript
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_B_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_B_COMMENT;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_B_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Keyword = ScintillaConstants.SCE_B_KEYWORD;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_B_STRING;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_B_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_B_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_B_IDENTIFIER;

        /// <summary>
        /// Date style index.
        /// </summary>
        public const int Date = ScintillaConstants.SCE_B_DATE;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_B_STRINGEOL;

        /// <summary>
        /// Keyword list 2 (index 1) style index.
        /// </summary>
        public const int Keyword2 = ScintillaConstants.SCE_B_KEYWORD2;

        /// <summary>
        /// Keyword list 3 (index 2) style index.
        /// </summary>
        public const int Keyword3 = ScintillaConstants.SCE_B_KEYWORD3;

        /// <summary>
        /// Keyword list 4 (index 3) style index.
        /// </summary>
        public const int Keyword4 = ScintillaConstants.SCE_B_KEYWORD4;

        /// <summary>
        /// Constant style index.
        /// </summary>
        public const int Constant = ScintillaConstants.SCE_B_CONSTANT;

        /// <summary>
        /// Inline assembler style index.
        /// </summary>
        public const int Asm = ScintillaConstants.SCE_B_ASM;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = ScintillaConstants.SCE_B_LABEL;

        /// <summary>
        /// Error style index.
        /// </summary>
        public const int Error = ScintillaConstants.SCE_B_ERROR;

        /// <summary>
        /// Hexadecimal number style index.
        /// </summary>
        public const int HexNumber = ScintillaConstants.SCE_B_HEXNUMBER;

        /// <summary>
        /// Binary number style index.
        /// </summary>
        public const int BinNumber = ScintillaConstants.SCE_B_BINNUMBER;

        /// <summary>
        /// Block comment style index.
        /// </summary>
        public const int CommentBlock = ScintillaConstants.SCE_B_COMMENTBLOCK;

        /// <summary>
        /// Documentation line style index.
        /// </summary>
        public const int DocLine = ScintillaConstants.SCE_B_DOCLINE;

        /// <summary>
        /// Documentation block style index.
        /// </summary>
        public const int DocBlock = ScintillaConstants.SCE_B_DOCBLOCK;

        /// <summary>
        /// Documentation keyword style index.
        /// </summary>
        public const int DocKeyword = ScintillaConstants.SCE_B_DOCKEYWORD;
    }

    #endregion VbScript

    #region Verilog

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Verilog" /> lexer.
    /// </summary>
    public static class Verilog
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_V_DEFAULT;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_V_COMMENT;

        /// <summary>
        /// Comment line style index.
        /// </summary>
        public const int CommentLine = ScintillaConstants.SCE_V_COMMENTLINE;

        /// <summary>
        /// Comment line bang (exclamation) style index.
        /// </summary>
        public const int CommentLineBang = ScintillaConstants.SCE_V_COMMENTLINEBANG;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_V_NUMBER;

        /// <summary>
        /// Keyword (set 0) style index.
        /// </summary>
        public const int Word = ScintillaConstants.SCE_V_WORD;

        /// <summary>
        /// String style index.
        /// </summary>
        public const int String = ScintillaConstants.SCE_V_STRING;

        /// <summary>
        /// Keyword (set 1) style index.
        /// </summary>
        public const int Word2 = ScintillaConstants.SCE_V_WORD2;

        /// <summary>
        /// Keyword (set 2) style index.
        /// </summary>
        public const int Word3 = ScintillaConstants.SCE_V_WORD3;

        /// <summary>
        /// Preprocessor style index.
        /// </summary>
        public const int Preprocessor = ScintillaConstants.SCE_V_PREPROCESSOR;

        /// <summary>
        /// Operator style index.
        /// </summary>
        public const int Operator = ScintillaConstants.SCE_V_OPERATOR;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = ScintillaConstants.SCE_V_IDENTIFIER;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = ScintillaConstants.SCE_V_STRINGEOL;

        /// <summary>
        /// User word (set 3) style index.
        /// </summary>
        public const int User = ScintillaConstants.SCE_V_USER;

        /// <summary>
        /// Comment word (set 4) style index.
        /// </summary>
        public const int CommentWord = ScintillaConstants.SCE_V_COMMENT_WORD;

        /// <summary>
        /// Input style index.
        /// </summary>
        public const int Input = ScintillaConstants.SCE_V_INPUT;

        /// <summary>
        /// Output style index.
        /// </summary>
        public const int Output = ScintillaConstants.SCE_V_OUTPUT;

        /// <summary>
        /// In-out style index.
        /// </summary>
        public const int InOut = ScintillaConstants.SCE_V_INOUT;

        /// <summary>
        /// Port connect style index.
        /// </summary>
        public const int PortConnect = ScintillaConstants.SCE_V_PORT_CONNECT;
    }

    #endregion Verilog

    #region Xml

    /// <summary>
    /// Style constants for use with the <see cref="Lexer.Xml" /> lexer.
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Content style index.
        /// </summary>
        public const int Default = ScintillaConstants.SCE_H_DEFAULT;

        /// <summary>
        /// Tag style index.
        /// </summary>
        public const int Tag = ScintillaConstants.SCE_H_TAG;

        /// <summary>
        /// Unknown tag style index.
        /// </summary>
        public const int TagUnknown = ScintillaConstants.SCE_H_TAGUNKNOWN;

        /// <summary>
        /// Attribute style index.
        /// </summary>
        public const int Attribute = ScintillaConstants.SCE_H_ATTRIBUTE;

        /// <summary>
        /// Unknown attribute style index.
        /// </summary>
        public const int AttributeUnknown = ScintillaConstants.SCE_H_ATTRIBUTEUNKNOWN;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = ScintillaConstants.SCE_H_NUMBER;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int DoubleString = ScintillaConstants.SCE_H_DOUBLESTRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int SingleString = ScintillaConstants.SCE_H_SINGLESTRING;

        /// <summary>
        /// Other tag content (not elements or attributes) style index.
        /// </summary>
        public const int Other = ScintillaConstants.SCE_H_OTHER;

        /// <summary>
        /// Comment style index.
        /// </summary>
        public const int Comment = ScintillaConstants.SCE_H_COMMENT;

        /// <summary>
        /// Entity ($nnn;) name style index.
        /// </summary>
        public const int Entity = ScintillaConstants.SCE_H_ENTITY;

        /// <summary>
        /// End-tag style index.
        /// </summary>
        public const int TagEnd = ScintillaConstants.SCE_H_TAGEND;

        /// <summary>
        /// Start of XML declaration (&lt;?xml&gt;) style index.
        /// </summary>
        public const int XmlStart = ScintillaConstants.SCE_H_XMLSTART;

        /// <summary>
        /// End of XML declaration (?&gt;) style index.
        /// </summary>
        public const int XmlEnd = ScintillaConstants.SCE_H_XMLEND;

        /// <summary>
        /// Script tag (&lt;script&gt;) style index.
        /// </summary>
        public const int Script = ScintillaConstants.SCE_H_SCRIPT;

        /// <summary>
        /// ASP-like script engine block (&lt;%) style index.
        /// </summary>
        public const int Asp = ScintillaConstants.SCE_H_ASP;

        /// <summary>
        /// ASP-like language declaration (&lt;%@) style index.
        /// </summary>
        public const int AspAt = ScintillaConstants.SCE_H_ASPAT;

        /// <summary>
        /// CDATA section style index.
        /// </summary>
        public const int CData = ScintillaConstants.SCE_H_CDATA;

        /// <summary>
        /// Question mark style index.
        /// </summary>
        public const int Question = ScintillaConstants.SCE_H_QUESTION;

        /// <summary>
        /// Value style index.
        /// </summary>
        public const int Value = ScintillaConstants.SCE_H_VALUE;

        /// <summary>
        /// Script engine comment (&lt;%--) style index.
        /// </summary>
        public const int XcComment = ScintillaConstants.SCE_H_XCCOMMENT;
    }

    #endregion Xml
}