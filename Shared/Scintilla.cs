#undef Windows
#define Linux
#region License
/*
MIT License

Copyright(c) 2023 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Runtime.InteropServices;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Methods;
using ScintillaNet.Abstractions.Structs;
using ScintillaNet.EtoForms;
using ScintillaNet.EtoForms.Collections;
using ScintillaNet.EtoForms.EventArguments;
using Command = ScintillaNet.Abstractions.Enumerations.Command;
using WrapMode = ScintillaNet.Abstractions.Enumerations.WrapMode;



#if Windows
using ScintillaNet.WinForms;
using Eto.WinForms;
#elif Linux
using ScintillaNet.Gtk;
using Eto.GtkSharp;
#elif OSX
#endif


namespace ScintillaNet.Eto;

/// <summary>
/// A Scintilla control wrapper for Eto.Forms.
/// Implements the <see cref="ScintillaControl" />
/// </summary>
/// <seealso cref="ScintillaControl" />
public partial class Scintilla: ScintillaControl, IScintillaEtoForms
{
    private static bool initialized;

    /// <summary>
    /// Initializes static members of the <see cref="Scintilla"/> class.
    /// </summary>
    static Scintilla()
    {
        PlatformInitialize();
    }

    /// <summary>
    /// Initialization of the Scintilla control for the appropriate Eto platform.
    /// </summary>
    public static void PlatformInitialize()
    {
        if (initialized)
        {
            return;
        }
        
        #if Linux
        if (!initialized && RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            global::Eto.Platform.Instance.Add<IScintillaControl>(() => new EtoForms.GTK.ScintillaControlHandler());
            initialized = true;
        }
        #endif

        #if Windows
        if (!initialized && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (global::Eto.Platform.Instance.IsWpf)
            {
                global::Eto.Platform.Instance.Add<IScintillaControl>(() => new EtoForms.Wpf.ScintillaControlHandler());
            }

            if (global::Eto.Platform.Instance.IsWinForms)
            {
                global::Eto.Platform.Instance.Add<IScintillaControl>(() => new EtoForms.WinForms.ScintillaControlHandler());
            }
            initialized = true;
        }
        #endif
    }

    private IScintillaControl BaseControl => (IScintillaControl)Handler;

    /// <summary>
    /// Gets the Lexilla library access.
    /// </summary>
    /// <value>The lexilla library access.</value>
    public ILexilla Lexilla => BaseControl.Lexilla;

    /// <inheritdoc cref="IHasEncoding.Encoding" />
    public override Encoding Encoding => BaseControl.Encoding;

    /// <inheritdoc />
    public void RegisterRgbaImage(int type, Image image)
    {
        #if Windows
        ((IScintillaMethodsImage<System.Drawing.Image>)BaseControl).RegisterRgbaImage(type, image.ToSD());
        #elif Linux
        throw new NotImplementedException();
        #elif OSX
        #endif
    }

    /// <inheritdoc />
    public IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam) => BaseControl.SetParameter(message, wParam, lParam);

    /// <inheritdoc />
    public IntPtr DirectMessage(int message) => BaseControl.DirectMessage(message);

    /// <inheritdoc />
    public IntPtr DirectMessage(int message, IntPtr wParam) => BaseControl.DirectMessage(message, wParam);

    /// <inheritdoc />
    public IntPtr DirectMessage(int message, IntPtr wParam, IntPtr lParam) =>
        BaseControl.DirectMessage(message, wParam, lParam);

    /// <inheritdoc />
    public IntPtr DirectMessage(IntPtr scintillaPointer, int message, IntPtr wParam, IntPtr lParam) =>
        BaseControl.DirectMessage(scintillaPointer, message, wParam, lParam);

    /// <inheritdoc />
    public void MarkerDeleteAll(int marker) => BaseControl.MarkerDeleteAll(marker);

    /// <inheritdoc />
    public int TextLength => BaseControl.TextLength;

    /// <inheritdoc />
    public string GetTextRange(int position, int length) => BaseControl.GetTextRange(position, length);

    /// <inheritdoc />
    public void FoldAll(FoldAction action) => BaseControl.FoldAll(action);

    /// <inheritdoc />
    public void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0) =>
        BaseControl.InitDocument(eolMode, useTabs, tabWidth, indentWidth);

    private IndicatorCollection? indicators;

    /// <inheritdoc />
    public IndicatorCollection Indicators {
        get
        {
#if Windows
            indicators ??= new IndicatorCollection(this, ((IScintillaWinForms)BaseControl.NativeControl).Lines);
            return indicators;
#elif Linux
            indicators ??= new IndicatorCollection(this, ((IScintillaLinux)BaseControl.NativeControl).Lines);
            return indicators;
#elif OSX
#endif

        }
    }

    private LineCollection? linesCollection;

    /// <inheritdoc />
    public LineCollection Lines {
        get
        {
#if Windows
            linesCollection ??= new LineCollection(this, 
                ((IScintillaWinForms)BaseControl.NativeControl).Styles,
                ((IScintillaWinForms)BaseControl.NativeControl).Markers);
            return linesCollection;
#elif Linux
            linesCollection ??= new LineCollection(this, 
                ((IScintillaLinux)BaseControl.NativeControl).Styles,
                ((IScintillaLinux)BaseControl.NativeControl).Markers);
            return linesCollection;            
#elif OSX
#endif            
        }
    }

    private MarginCollection? margins;

    /// <inheritdoc />
    public MarginCollection Margins
    {
        get
        {
            margins ??= new MarginCollection(BaseControl);
            return margins;
        }
    }

    private MarkerCollection? markers;

    /// <inheritdoc />
    public MarkerCollection Markers
    {
        get
        {
            markers ??= new MarkerCollection(BaseControl);
            return markers;
        }
    }

    SelectionCollection? selections;

    /// <inheritdoc />
    public SelectionCollection Selections
    {
        get
        {
#if Windows
            selections ??= new SelectionCollection(BaseControl, ((IScintillaWinForms)BaseControl.NativeControl).Lines);
            return selections;
#elif Linux
            selections ??= new SelectionCollection(BaseControl, ((IScintillaLinux)BaseControl.NativeControl).Lines);
            return selections;
#elif OSX
#endif
        }
    }

    private StyleCollection? styles;

    /// <inheritdoc />
    public StyleCollection Styles {
        get
        {
            styles ??= new StyleCollection(BaseControl);
            return styles;
        }
    }

    /// <inheritdoc />
    public Color AdditionalCaretForeColor
    {
        get
        {
#if Windows
            return ((IScintillaWinForms)BaseControl.NativeControl).AdditionalCaretForeColor.ToEto();
#elif Linux
            return ((IScintillaLinux)BaseControl.NativeControl).AdditionalCaretForeColor.ToEto();
#elif OSX
#endif
        }

        set
        {
#if Windows
            ((IScintillaWinForms)BaseControl.NativeControl).AdditionalCaretForeColor = value.ToSD();
#elif Linux
            ((IScintillaLinux)BaseControl.NativeControl).AdditionalCaretForeColor = value.ToGdk();
#elif OSX
#endif
        }
    }

    /// <inheritdoc />
    public Color CaretForeColor 
    {
        get
        {
#if Windows
            return ((IScintillaWinForms)BaseControl.NativeControl).CaretForeColor.ToEto();
#elif Linux
            return ((IScintillaLinux)BaseControl.NativeControl).CaretForeColor.ToEto();
#elif OSX
#endif
        }

        set
        {
#if Windows
            ((IScintillaWinForms)BaseControl.NativeControl).CaretForeColor = value.ToSD();
#elif Linux
            ((IScintillaLinux)BaseControl.NativeControl).CaretForeColor = value.ToGdk();
#elif OSX
#endif
        }
    }

    /// <inheritdoc />
    public Color CaretLineBackColor
    {
        get
        {
#if Windows
            return ((IScintillaWinForms)BaseControl.NativeControl).CaretLineBackColor.ToEto();
#elif Linux
            return ((IScintillaLinux)BaseControl.NativeControl).CaretLineBackColor.ToEto();
#elif OSX
#endif
        }

        set
        {
#if Windows
            ((IScintillaWinForms)BaseControl.NativeControl).CaretLineBackColor = value.ToSD();
#elif Linux
            ((IScintillaLinux)BaseControl.NativeControl).CaretLineBackColor = value.ToGdk();
#elif OSX
#endif
        }
    }

    /// <inheritdoc />
    public Color EdgeColor
    {
        get
        {
#if Windows
            return ((IScintillaWinForms)BaseControl.NativeControl).EdgeColor.ToEto();
#elif Linux
            return ((IScintillaLinux)BaseControl.NativeControl).EdgeColor.ToEto();
#elif OSX
#endif
        }

        set
        {
#if Windows
            ((IScintillaWinForms)BaseControl.NativeControl).EdgeColor = value.ToSD();
#elif Linux
            ((IScintillaLinux) BaseControl.NativeControl).EdgeColor = value.ToGdk();
#elif OSX
#endif
        }
    }

    /// <inheritdoc />
    public BiDirectionalDisplayType BiDirectionality
    {
        get => ((IScintillaProperties)BaseControl).BiDirectionality;

        set => ((IScintillaProperties)BaseControl).BiDirectionality = value;
    }

    /// <inheritdoc />
    public bool AdditionalCaretsBlink
    {
        get => ((IScintillaProperties)BaseControl).AdditionalCaretsBlink;

        set => ((IScintillaProperties)BaseControl).AdditionalCaretsBlink = value;
    }

    /// <inheritdoc />
    public bool AdditionalCaretsVisible 
    {
        get => ((IScintillaProperties)BaseControl).AdditionalCaretsVisible;

        set => ((IScintillaProperties)BaseControl).AdditionalCaretsVisible = value;
    }

    /// <inheritdoc />
    public int AnchorPosition
    {
        get => ((IScintillaProperties)BaseControl).AnchorPosition;

        set => ((IScintillaProperties)BaseControl).AnchorPosition = value;
    }

    /// <inheritdoc />
    public int AdditionalSelAlpha
    {
        get => ((IScintillaProperties)BaseControl).AdditionalSelAlpha;

        set => ((IScintillaProperties)BaseControl).AdditionalSelAlpha = value;
    }

    /// <inheritdoc />
    public bool AdditionalSelectionTyping
    {
        get => ((IScintillaProperties)BaseControl).AdditionalSelectionTyping;

        set => ((IScintillaProperties)BaseControl).AdditionalSelectionTyping = value;
    }

    /// <inheritdoc />
    public Annotation AnnotationVisible
    {
        get => ((IScintillaProperties)BaseControl).AnnotationVisible;

        set => ((IScintillaProperties)BaseControl).AnnotationVisible = value;
    }

    /// <inheritdoc />
    public bool AutoCActive => ((IScintillaProperties)BaseControl).AutoCActive;

    /// <inheritdoc />
    public bool AutoCAutoHide
    {
        get => ((IScintillaProperties)BaseControl).AutoCAutoHide;

        set => ((IScintillaProperties)BaseControl).AutoCAutoHide = value;
    }

    /// <inheritdoc />
    public bool AutoCCancelAtStart
    {
        get => ((IScintillaProperties)BaseControl).AutoCCancelAtStart;

        set => ((IScintillaProperties)BaseControl).AutoCCancelAtStart = value;
    }

    /// <inheritdoc />
    public int AutoCCurrent => ((IScintillaProperties)BaseControl).AutoCCurrent;

    /// <inheritdoc />
    public bool AutoCChooseSingle
    {
        get => ((IScintillaProperties)BaseControl).AutoCChooseSingle;

        set => ((IScintillaProperties)BaseControl).AutoCChooseSingle = value;
    }

    /// <inheritdoc />
    public bool AutoCDropRestOfWord
    {
        get => ((IScintillaProperties)BaseControl).AutoCDropRestOfWord;

        set => ((IScintillaProperties)BaseControl).AutoCDropRestOfWord = value;
    }

    /// <inheritdoc />
    public bool AutoCIgnoreCase
    {
        get => ((IScintillaProperties)BaseControl).AutoCIgnoreCase;

        set => ((IScintillaProperties)BaseControl).AutoCIgnoreCase = value;
    }

    /// <inheritdoc />
    public int AutoCMaxHeight
    {
        get => ((IScintillaProperties)BaseControl).AutoCMaxHeight;

        set => ((IScintillaProperties)BaseControl).AutoCMaxHeight = value;
    }

    /// <inheritdoc />
    public int AutoCMaxWidth
    {
        get => ((IScintillaProperties)BaseControl).AutoCMaxWidth;

        set => ((IScintillaProperties)BaseControl).AutoCMaxWidth = value;
    }

    /// <inheritdoc />
    public Order AutoCOrder
    {
        get => ((IScintillaProperties)BaseControl).AutoCOrder;

        set => ((IScintillaProperties)BaseControl).AutoCOrder = value;
    }

    /// <inheritdoc />
    public int AutoCPosStart => ((IScintillaProperties)BaseControl).AutoCPosStart;

    /// <inheritdoc />
    public Document Document
    {
        get => ((IScintillaProperties)BaseControl).Document;

        set => ((IScintillaProperties)BaseControl).Document = value;
    }

    /// <inheritdoc />
    public int RectangularSelectionAnchor
    {
        get => ((IScintillaProperties)BaseControl).RectangularSelectionAnchor;

        set => ((IScintillaProperties)BaseControl).RectangularSelectionAnchor = value;
    }

    /// <inheritdoc />
    public int RectangularSelectionCaret
    {
        get => ((IScintillaProperties)BaseControl).RectangularSelectionCaret;

        set => ((IScintillaProperties)BaseControl).RectangularSelectionCaret = value;
    }

    /// <inheritdoc />
    public char AutoCSeparator
    {
        get => ((IScintillaProperties)BaseControl).AutoCSeparator;

        set => ((IScintillaProperties)BaseControl).AutoCSeparator = value;
    }

    /// <inheritdoc />
    public char AutoCTypeSeparator
    {
        get => ((IScintillaProperties)BaseControl).AutoCTypeSeparator;

        set => ((IScintillaProperties)BaseControl).AutoCTypeSeparator = value;
    }

    /// <inheritdoc />
    public AutomaticFold AutomaticFold
    {
        get => ((IScintillaProperties)BaseControl).AutomaticFold;

        set => ((IScintillaProperties)BaseControl).AutomaticFold = value;
    }

    /// <inheritdoc />
    public bool BackspaceUnIndents
    {
        get => ((IScintillaProperties)BaseControl).BackspaceUnIndents;

        set => ((IScintillaProperties)BaseControl).BackspaceUnIndents = value;
    }

    /// <inheritdoc />
    public bool BufferedDraw
    {
        get => ((IScintillaProperties)BaseControl).BufferedDraw;

        set => ((IScintillaProperties)BaseControl).BufferedDraw = value;
    }

    /// <inheritdoc />
    public bool CallTipActive => ((IScintillaProperties)BaseControl).CallTipActive;

    /// <inheritdoc />
    public bool CanPaste => ((IScintillaProperties)BaseControl).CanPaste;

    /// <inheritdoc />
    public bool CanRedo => ((IScintillaProperties)BaseControl).CanRedo;

    /// <inheritdoc />
    public bool CanUndo => ((IScintillaProperties)BaseControl).CanUndo;

    /// <inheritdoc />
    public int CaretLineBackColorAlpha
    {
        get => ((IScintillaProperties)BaseControl).CaretLineBackColorAlpha;

        set => ((IScintillaProperties)BaseControl).CaretLineBackColorAlpha = value;
    }

    /// <inheritdoc />
    public int CaretLineFrame
    {
        get => ((IScintillaProperties)BaseControl).CaretLineFrame;

        set => ((IScintillaProperties)BaseControl).CaretLineFrame = value;
    }

    /// <inheritdoc />
    public bool CaretLineVisible
    {
        get => ((IScintillaProperties)BaseControl).CaretLineVisible;

        set => ((IScintillaProperties)BaseControl).CaretLineVisible = value;
    }

    /// <inheritdoc />
    public bool CaretLineVisibleAlways
    {
        get => ((IScintillaProperties)BaseControl).CaretLineVisibleAlways;

        set => ((IScintillaProperties)BaseControl).CaretLineVisibleAlways = value;
    }

    /// <inheritdoc />
    public Layer CaretLineLayer
    {
        get => ((IScintillaProperties)BaseControl).CaretLineLayer;

        set => ((IScintillaProperties)BaseControl).CaretLineLayer = value;
    }

    /// <inheritdoc />
    public int CaretPeriod
    {
        get => ((IScintillaProperties)BaseControl).CaretPeriod;

        set => ((IScintillaProperties)BaseControl).CaretPeriod = value;
    }

    /// <inheritdoc />
    public CaretStyle CaretStyle
    {
        get => ((IScintillaProperties)BaseControl).CaretStyle;

        set => ((IScintillaProperties)BaseControl).CaretStyle = value;
    }

    /// <inheritdoc />
    public int CaretWidth
    {
        get => ((IScintillaProperties)BaseControl).CaretWidth;

        set => ((IScintillaProperties)BaseControl).CaretWidth = value;
    }

    /// <inheritdoc />
    public int CurrentLine => ((IScintillaProperties)BaseControl).CurrentLine;

    /// <inheritdoc />
    public int CurrentPosition
    {
        get => ((IScintillaProperties)BaseControl).CurrentPosition;

        set => ((IScintillaProperties)BaseControl).CurrentPosition = value;
    }

    /// <inheritdoc />
    public int DistanceToSecondaryStyles => ((IScintillaProperties)BaseControl).DistanceToSecondaryStyles;

    /// <inheritdoc />
    public int EdgeColumn
    {
        get => ((IScintillaProperties)BaseControl).EdgeColumn;

        set => ((IScintillaProperties)BaseControl).EdgeColumn = value;
    }

    /// <inheritdoc />
    public EdgeMode EdgeMode
    {
        get => ((IScintillaProperties)BaseControl).EdgeMode;

        set => ((IScintillaProperties)BaseControl).EdgeMode = value;
    }

    /// <inheritdoc />
    public bool EndAtLastLine
    {
        get => ((IScintillaProperties)BaseControl).EndAtLastLine;

        set => ((IScintillaProperties)BaseControl).EndAtLastLine = value;
    }

    /// <inheritdoc />
    public Eol EolMode
    {
        get => ((IScintillaProperties)BaseControl).EolMode;

        set => ((IScintillaProperties)BaseControl).EolMode = value;
    }

    /// <inheritdoc />
    public int ExtraAscent
    {
        get => ((IScintillaProperties)BaseControl).ExtraAscent;

        set => ((IScintillaProperties)BaseControl).ExtraAscent = value;
    }

    /// <inheritdoc />
    public int ExtraDescent
    {
        get => ((IScintillaProperties)BaseControl).ExtraDescent;

        set => ((IScintillaProperties)BaseControl).ExtraDescent = value;
    }

    /// <inheritdoc />
    public int FirstVisibleLine
    {
        get => ((IScintillaProperties)BaseControl).FirstVisibleLine;

        set => ((IScintillaProperties)BaseControl).FirstVisibleLine = value;
    }

    /// <inheritdoc />
    public FontQuality FontQuality
    {
        get => ((IScintillaProperties)BaseControl).FontQuality;

        set => ((IScintillaProperties)BaseControl).FontQuality = value;
    }

    /// <inheritdoc />
    public int HighlightGuide
    {
        get => ((IScintillaProperties)BaseControl).HighlightGuide;

        set => ((IScintillaProperties)BaseControl).HighlightGuide = value;
    }

    /// <inheritdoc />
    public bool HScrollBar
    {
        get => ((IScintillaProperties)BaseControl).HScrollBar;

        set => ((IScintillaProperties)BaseControl).HScrollBar = value;
    }

    /// <inheritdoc />
    public IdleStyling IdleStyling
    {
        get => ((IScintillaProperties)BaseControl).IdleStyling;

        set => ((IScintillaProperties)BaseControl).IdleStyling = value;
    }

    /// <inheritdoc />
    public int IndentWidth
    {
        get => ((IScintillaProperties)BaseControl).IndentWidth;

        set => ((IScintillaProperties)BaseControl).IndentWidth = value;
    }

    /// <inheritdoc />
    public IndentView IndentationGuides
    {
        get => ((IScintillaProperties)BaseControl).IndentationGuides;

        set => ((IScintillaProperties)BaseControl).IndentationGuides = value;
    }

    /// <inheritdoc />
    public int IndicatorCurrent
    {
        get => ((IScintillaProperties)BaseControl).IndicatorCurrent;

        set => ((IScintillaProperties)BaseControl).IndicatorCurrent = value;
    }

    /// <inheritdoc />
    public int IndicatorValue
    {
        get => ((IScintillaProperties)BaseControl).IndicatorValue;

        set => ((IScintillaProperties)BaseControl).IndicatorValue = value;
    }

    /// <inheritdoc />
    public bool InternalFocusFlag
    {
        get => ((IScintillaProperties)BaseControl).InternalFocusFlag;

        set => ((IScintillaProperties)BaseControl).InternalFocusFlag = value;
    }

    /// <inheritdoc />
    public string? LexerName
    {
        get => ((IScintillaProperties)BaseControl).LexerName;

        set => ((IScintillaProperties)BaseControl).LexerName = value;
    }

    /// <inheritdoc />
    public Layer SelectionLayer
    {
        get => ((IScintillaProperties)BaseControl).SelectionLayer;

        set => ((IScintillaProperties)BaseControl).SelectionLayer = value;
    }

    /// <inheritdoc />
    public int SelectionEnd
    {
        get => ((IScintillaProperties)BaseControl).SelectionEnd;

        set => ((IScintillaProperties)BaseControl).SelectionEnd = value;
    }

    /// <inheritdoc />
    public int SelectionStart
    {
        get => ((IScintillaProperties)BaseControl).SelectionStart;

        set => ((IScintillaProperties)BaseControl).SelectionStart = value;
    }

    /// <inheritdoc />
    [Obsolete("This property will get more obsolete as time passes as the Scintilla v.5+ now uses strings to define lexers. Please use the LexerName property instead.")]
    public Lexer Lexer
    {
        get => ((IScintillaProperties)BaseControl).Lexer;

        set => ((IScintillaProperties)BaseControl).Lexer = value;
    }

    /// <inheritdoc />
    public string LexerLanguage
    {
        get => ((IScintillaProperties)BaseControl).LexerLanguage;

        set => ((IScintillaProperties)BaseControl).LexerLanguage = value;
    }

    /// <inheritdoc />
    public LineEndType LineEndTypesActive => ((IScintillaProperties)BaseControl).LineEndTypesActive;

    /// <inheritdoc />
    public LineEndType LineEndTypesAllowed
    {
        get => ((IScintillaProperties)BaseControl).LineEndTypesAllowed;

        set => ((IScintillaProperties)BaseControl).LineEndTypesAllowed = value;
    }

    /// <inheritdoc />
    public LineEndType LineEndTypesSupported => ((IScintillaProperties)BaseControl).LineEndTypesSupported;

    /// <inheritdoc />
    public int LinesOnScreen => ((IScintillaProperties)BaseControl).LinesOnScreen;

    /// <inheritdoc />
    public int MainSelection
    {
        get => ((IScintillaProperties)BaseControl).MainSelection;

        set => ((IScintillaProperties)BaseControl).MainSelection = value;
    }

    /// <inheritdoc />
    public bool Modified => ((IScintillaProperties)BaseControl).Modified;

    /// <inheritdoc />
    public int MouseDwellTime
    {
        get => ((IScintillaProperties)BaseControl).MouseDwellTime;

        set => ((IScintillaProperties)BaseControl).MouseDwellTime = value;
    }

    /// <inheritdoc />
    public bool MouseSelectionRectangularSwitch
    {
        get => ((IScintillaProperties)BaseControl).MouseSelectionRectangularSwitch;

        set => ((IScintillaProperties)BaseControl).MouseSelectionRectangularSwitch = value;
    }

    /// <inheritdoc />
    public bool MultipleSelection
    {
        get => ((IScintillaProperties)BaseControl).MultipleSelection;

        set => ((IScintillaProperties)BaseControl).MultipleSelection = value;
    }

    /// <inheritdoc />
    public MultiPaste MultiPaste
    {
        get => ((IScintillaProperties)BaseControl).MultiPaste;

        set => ((IScintillaProperties)BaseControl).MultiPaste = value;
    }

    /// <inheritdoc />
    public bool OverType
    {
        get => ((IScintillaProperties)BaseControl).OverType;

        set => ((IScintillaProperties)BaseControl).OverType = value;
    }

    /// <inheritdoc />
    public bool PasteConvertEndings
    {
        get => ((IScintillaProperties)BaseControl).PasteConvertEndings;

        set => ((IScintillaProperties)BaseControl).PasteConvertEndings = value;
    }

    /// <inheritdoc />
    public Phases PhasesDraw
    {
        get => ((IScintillaProperties)BaseControl).PhasesDraw;

        set => ((IScintillaProperties)BaseControl).PhasesDraw = value;
    }

    /// <inheritdoc />
    public bool ReadOnly
    {
        get => ((IScintillaProperties)BaseControl).ReadOnly;

        set => ((IScintillaProperties)BaseControl).ReadOnly = value;
    }

    /// <inheritdoc />
    public int RectangularSelectionAnchorVirtualSpace
    {
        get => ((IScintillaProperties)BaseControl).RectangularSelectionAnchorVirtualSpace;

        set => ((IScintillaProperties)BaseControl).RectangularSelectionAnchorVirtualSpace = value;
    }

    /// <inheritdoc />
    public int RectangularSelectionCaretVirtualSpace
    {
        get => ((IScintillaProperties)BaseControl).RectangularSelectionCaretVirtualSpace;

        set => ((IScintillaProperties)BaseControl).RectangularSelectionCaretVirtualSpace = value;
    }

    /// <inheritdoc />
    public int ScrollWidth
    {
        get => ((IScintillaProperties)BaseControl).ScrollWidth;

        set => ((IScintillaProperties)BaseControl).ScrollWidth = value;
    }

    /// <inheritdoc />
    public bool ScrollWidthTracking
    {
        get => ((IScintillaProperties)BaseControl).ScrollWidthTracking;

        set => ((IScintillaProperties)BaseControl).ScrollWidthTracking = value;
    }

    /// <inheritdoc />
    public SearchFlags SearchFlags
    {
        get => ((IScintillaProperties)BaseControl).SearchFlags;

        set => ((IScintillaProperties)BaseControl).SearchFlags = value;
    }

    /// <inheritdoc />
    public string SelectedText => ((IScintillaProperties)BaseControl).SelectedText;

    /// <inheritdoc />
    public bool SelectionEolFilled
    {
        get => ((IScintillaProperties)BaseControl).SelectionEolFilled;

        set => ((IScintillaProperties)BaseControl).SelectionEolFilled = value;
    }

    /// <inheritdoc />
    public Status Status
    {
        get => ((IScintillaProperties)BaseControl).Status;

        set => ((IScintillaProperties)BaseControl).Status = value;
    }

    /// <inheritdoc />
    public TabDrawMode TabDrawMode
    {
        get => ((IScintillaProperties)BaseControl).TabDrawMode;

        set => ((IScintillaProperties)BaseControl).TabDrawMode = value;
    }

    /// <inheritdoc />
    public bool TabIndents
    {
        get => ((IScintillaProperties)BaseControl).TabIndents;

        set => ((IScintillaProperties)BaseControl).TabIndents = value;
    }

    /// <inheritdoc />
    public int TabWidth
    {
        get => ((IScintillaProperties)BaseControl).TabWidth;

        set => ((IScintillaProperties)BaseControl).TabWidth = value;
    }

    /// <inheritdoc />
    public int TargetEnd
    {
        get => ((IScintillaProperties)BaseControl).TargetEnd;

        set => ((IScintillaProperties)BaseControl).TargetEnd = value;
    }

    /// <inheritdoc />
    public int TargetStart
    {
        get => ((IScintillaProperties)BaseControl).TargetStart;

        set => ((IScintillaProperties)BaseControl).TargetStart = value;
    }

    /// <inheritdoc />
    public string TargetText => ((IScintillaProperties)BaseControl).TargetText;

    /// <inheritdoc />
    public Technology Technology
    {
        get => ((IScintillaProperties)BaseControl).Technology;

        set => ((IScintillaProperties)BaseControl).Technology = value;
    }

    /// <inheritdoc />
    public string Text
    {
        get => ((IScintillaProperties)BaseControl).Text;

        set => ((IScintillaProperties)BaseControl).Text = value;
    }

    /// <inheritdoc />
    public bool UseTabs
    {
        get => ((IScintillaProperties)BaseControl).UseTabs;

        set => ((IScintillaProperties)BaseControl).UseTabs = value;
    }

    /// <inheritdoc />
    public bool ViewEol
    {
        get => ((IScintillaProperties)BaseControl).ViewEol;

        set => ((IScintillaProperties)BaseControl).ViewEol = value;
    }

    /// <inheritdoc />
    public WhitespaceMode ViewWhitespace
    {
        get => ((IScintillaProperties)BaseControl).ViewWhitespace;

        set => ((IScintillaProperties)BaseControl).ViewWhitespace = value;
    }

    /// <inheritdoc />
    public VirtualSpace VirtualSpaceOptions
    {
        get => ((IScintillaProperties)BaseControl).VirtualSpaceOptions;

        set => ((IScintillaProperties)BaseControl).VirtualSpaceOptions = value;
    }

    /// <inheritdoc />
    public bool VScrollBar
    {
        get => ((IScintillaProperties)BaseControl).VScrollBar;

        set => ((IScintillaProperties)BaseControl).VScrollBar = value;
    }

    /// <inheritdoc />
    public int VisibleLineCount => ((IScintillaProperties)BaseControl).VisibleLineCount;

    /// <inheritdoc />
    public string WhitespaceChars
    {
        get => ((IScintillaProperties)BaseControl).WhitespaceChars;

        set => ((IScintillaProperties)BaseControl).WhitespaceChars = value;
    }

    /// <inheritdoc />
    public int WhitespaceSize
    {
        get => ((IScintillaProperties)BaseControl).WhitespaceSize;

        set => ((IScintillaProperties)BaseControl).WhitespaceSize = value;
    }

    /// <inheritdoc />
    public string WordChars
    {
        get => ((IScintillaProperties)BaseControl).WordChars;

        set => ((IScintillaProperties)BaseControl).WordChars = value;
    }

    /// <inheritdoc />
    public WrapIndentMode WrapIndentMode
    {
        get => ((IScintillaProperties)BaseControl).WrapIndentMode;

        set => ((IScintillaProperties)BaseControl).WrapIndentMode = value;
    }

    /// <inheritdoc />
    public WrapMode WrapMode
    {
        get => ((IScintillaProperties)BaseControl).WrapMode;

        set => ((IScintillaProperties)BaseControl).WrapMode = value;
    }

    /// <inheritdoc />
    public int WrapStartIndent
    {
        get => ((IScintillaProperties)BaseControl).WrapStartIndent;

        set => ((IScintillaProperties)BaseControl).WrapStartIndent = value;
    }

    /// <inheritdoc />
    public WrapVisualFlags WrapVisualFlags
    {
        get => ((IScintillaProperties)BaseControl).WrapVisualFlags;

        set => ((IScintillaProperties)BaseControl).WrapVisualFlags = value;
    }

    /// <inheritdoc />
    public WrapVisualFlagLocation WrapVisualFlagLocation
    {
        get => ((IScintillaProperties)BaseControl).WrapVisualFlagLocation;

        set => ((IScintillaProperties)BaseControl).WrapVisualFlagLocation = value;
    }

    /// <inheritdoc />
    public int XOffset
    {
        get => ((IScintillaProperties)BaseControl).XOffset;

        set => ((IScintillaProperties)BaseControl).XOffset = value;
    }

    /// <inheritdoc />
    public int Zoom
    {
        get => ((IScintillaProperties)BaseControl).Zoom;

        set => ((IScintillaProperties)BaseControl).Zoom = value;
    }

    /// <inheritdoc />
    public void AddRefDocument(Document document) => ((IScintillaMethods)BaseControl.NativeControl).AddRefDocument(document);

    /// <inheritdoc />
    public void AddSelection(int caret, int anchor) => ((IScintillaMethods)BaseControl.NativeControl).AddSelection(caret, anchor);

    /// <inheritdoc />
    public void AddText(string text) => ((IScintillaMethods)BaseControl.NativeControl).AddText(text);

    /// <inheritdoc />
    public int AllocateSubStyles(int styleBase, int numberStyles) => ((IScintillaMethods)BaseControl.NativeControl).AllocateSubStyles(styleBase, numberStyles);

    /// <inheritdoc />
    public void AnnotationClearAll() => ((IScintillaMethods)BaseControl.NativeControl).AnnotationClearAll();

    /// <inheritdoc />
    public void AppendText(string text) => ((IScintillaMethods)BaseControl.NativeControl).AppendText(text);

    /// <inheritdoc />
    public void AutoCCancel() => ((IScintillaMethods)BaseControl.NativeControl).AutoCCancel();

    /// <inheritdoc />
    public void AutoCComplete() => ((IScintillaMethods)BaseControl.NativeControl).AutoCComplete();

    /// <inheritdoc />
    public void AutoCSelect(string select) => ((IScintillaMethods)BaseControl.NativeControl).AutoCSelect(select);

    /// <inheritdoc />
    public void AutoCSetFillUps(string chars) => ((IScintillaMethods)BaseControl.NativeControl).AutoCSetFillUps(chars);

    /// <inheritdoc />
    public void AutoCShow(int lenEntered, string list) => ((IScintillaMethods)BaseControl.NativeControl).AutoCShow(lenEntered, list);

    /// <inheritdoc />
    public void AutoCStops(string chars) => ((IScintillaMethods)BaseControl.NativeControl).AutoCStops(chars);

    /// <inheritdoc />
    public void BeginUndoAction() => ((IScintillaMethods)BaseControl.NativeControl).BeginUndoAction();

    /// <inheritdoc />
    public void BraceBadLight(int position) => ((IScintillaMethods)BaseControl.NativeControl).BraceBadLight(position);

    /// <inheritdoc />
    public void BraceHighlight(int position1, int position2) => ((IScintillaMethods)BaseControl.NativeControl).BraceHighlight(position1, position2);

    /// <inheritdoc />
    public int BraceMatch(int position) => ((IScintillaMethods)BaseControl.NativeControl).BraceMatch(position);

    /// <inheritdoc />
    public void CallTipCancel() => ((IScintillaMethods)BaseControl.NativeControl).CallTipCancel();

    /// <inheritdoc />
    public void CallTipSetHlt(int hlStart, int hlEnd) => ((IScintillaMethods)BaseControl.NativeControl).CallTipSetHlt(hlStart, hlEnd);

    /// <inheritdoc />
    public void CallTipSetPosition(bool above) => ((IScintillaMethods)BaseControl.NativeControl).CallTipSetPosition(above);

    /// <inheritdoc />
    public void CallTipShow(int posStart, string definition) => ((IScintillaMethods)BaseControl.NativeControl).CallTipShow(posStart, definition);

    /// <inheritdoc />
    public void CallTipTabSize(int tabSize) => ((IScintillaMethods)BaseControl.NativeControl).CallTipTabSize(tabSize);

    /// <inheritdoc />
    public void ChangeLexerState(int startPos, int endPos) => ((IScintillaMethods)BaseControl.NativeControl).ChangeLexerState(startPos, endPos);

    /// <inheritdoc />
    public int CharPositionFromPoint(int x, int y) => ((IScintillaMethods)BaseControl.NativeControl).CharPositionFromPoint(x, y);

    /// <inheritdoc />
    public int CharPositionFromPointClose(int x, int y) => ((IScintillaMethods)BaseControl.NativeControl).CharPositionFromPointClose(x, y);

    /// <inheritdoc />
    public void ChooseCaretX() => ((IScintillaMethods)BaseControl.NativeControl).ChooseCaretX();

    /// <inheritdoc />
    public void Clear() => ((IScintillaMethods)BaseControl.NativeControl).Clear();

    /// <inheritdoc />
    public void ClearAll() => ((IScintillaMethods)BaseControl.NativeControl).ClearAll();

    /// <inheritdoc />
    public void ClearAllCmdKeys() => ((IScintillaMethods)BaseControl.NativeControl).ClearAllCmdKeys();

    /// <inheritdoc />
    public void ClearDocumentStyle() => ((IScintillaMethods)BaseControl.NativeControl).ClearDocumentStyle();

    /// <inheritdoc />
    public void ClearRegisteredImages() => ((IScintillaMethods)BaseControl.NativeControl).ClearRegisteredImages();

    /// <inheritdoc />
    public void ClearSelections() => ((IScintillaMethods)BaseControl.NativeControl).ClearSelections();

    /// <inheritdoc />
    public void Colorize(int startPos, int endPos) => ((IScintillaMethods)BaseControl.NativeControl).Colorize(startPos, endPos);

    /// <inheritdoc />
    public void ConvertEols(Eol eolMode) => ((IScintillaMethods)BaseControl.NativeControl).ConvertEols(eolMode);

    /// <inheritdoc />
    public void Copy() => ((IScintillaMethods)BaseControl.NativeControl).Copy();

    /// <inheritdoc />
    public void Copy(CopyFormat format) => ((IScintillaMethods)BaseControl.NativeControl).Copy(format);

    /// <inheritdoc />
    public void CopyAllowLine() => ((IScintillaMethods)BaseControl.NativeControl).CopyAllowLine();

    /// <inheritdoc />
    public void CopyAllowLine(CopyFormat format) => ((IScintillaMethods)BaseControl.NativeControl).CopyAllowLine(format);

    /// <inheritdoc />
    public void CopyRange(int start, int end) => ((IScintillaMethods)BaseControl.NativeControl).CopyRange(start, end);

    /// <inheritdoc />
    public void CopyRange(int start, int end, CopyFormat format) => ((IScintillaMethods)BaseControl.NativeControl).CopyRange(start, end, format);

    /// <inheritdoc />
    public Document CreateDocument() => ((IScintillaMethods)BaseControl.NativeControl).CreateDocument();

    /// <inheritdoc />
    public ILoader CreateLoader(int length) => ((IScintillaMethods)BaseControl.NativeControl).CreateLoader(length);

    /// <inheritdoc />
    public void Cut() => ((IScintillaMethods)BaseControl.NativeControl).Cut();

    /// <inheritdoc />
    public void DeleteRange(int position, int length) => ((IScintillaMethods)BaseControl.NativeControl).DeleteRange(position, length);

    /// <inheritdoc />
    public string DescribeKeywordSets() => ((IScintillaMethods)BaseControl.NativeControl).DescribeKeywordSets();

    /// <inheritdoc />
    public string DescribeProperty(string name) => ((IScintillaMethods)BaseControl.NativeControl).DescribeProperty(name);

    /// <inheritdoc />
    public int DocLineFromVisible(int displayLine) => ((IScintillaMethods)BaseControl.NativeControl).DocLineFromVisible(displayLine);

    /// <inheritdoc />
    public void DropSelection(int selection) => ((IScintillaMethods)BaseControl.NativeControl).DropSelection(selection);

    /// <inheritdoc />
    public void EmptyUndoBuffer() => ((IScintillaMethods)BaseControl.NativeControl).EmptyUndoBuffer();

    /// <inheritdoc />
    public void EndUndoAction() => ((IScintillaMethods)BaseControl.NativeControl).EndUndoAction();

    /// <inheritdoc />
    public void ExecuteCmd(Command sciCommand) => ((IScintillaMethods)BaseControl.NativeControl).ExecuteCmd(sciCommand);

    /// <inheritdoc />
    public void FoldDisplayTextSetStyle(FoldDisplayText style) => ((IScintillaMethods)BaseControl.NativeControl).FoldDisplayTextSetStyle(style);

    /// <inheritdoc />
    public void FreeSubStyles() => ((IScintillaMethods)BaseControl.NativeControl).FreeSubStyles();

    /// <inheritdoc />
    public int GetCharAt(int position) => ((IScintillaMethods)BaseControl.NativeControl).GetCharAt(position);

    /// <inheritdoc />
    public int GetColumn(int position) => ((IScintillaMethods)BaseControl.NativeControl).GetColumn(position);

    /// <inheritdoc />
    public int GetEndStyled() => ((IScintillaMethods)BaseControl.NativeControl).GetEndStyled();

    /// <inheritdoc />
    public int GetPrimaryStyleFromStyle(int style) => ((IScintillaMethods)BaseControl.NativeControl).GetPrimaryStyleFromStyle(style);

    /// <inheritdoc />
    public string GetScintillaProperty(string name) => ((IScintillaMethods)BaseControl.NativeControl).GetScintillaProperty(name);

    /// <inheritdoc />
    public string GetPropertyExpanded(string name) => ((IScintillaMethods)BaseControl.NativeControl).GetPropertyExpanded(name);

    /// <inheritdoc />
    public int GetPropertyInt(string name, int defaultValue) => ((IScintillaMethods)BaseControl.NativeControl).GetPropertyInt(name, defaultValue);

    /// <inheritdoc />
    public int GetStyleAt(int position) => ((IScintillaMethods)BaseControl.NativeControl).GetStyleAt(position);

    /// <inheritdoc />
    public int GetStyleFromSubStyle(int subStyle) => ((IScintillaMethods)BaseControl.NativeControl).GetStyleFromSubStyle(subStyle);

    /// <inheritdoc />
    public int GetSubStylesLength(int styleBase) => ((IScintillaMethods)BaseControl.NativeControl).GetSubStylesLength(styleBase);

    /// <inheritdoc />
    public int GetSubStylesStart(int styleBase) => ((IScintillaMethods)BaseControl.NativeControl).GetSubStylesStart(styleBase);

    /// <inheritdoc />
    public string GetTag(int tagNumber) => ((IScintillaMethods)BaseControl.NativeControl).GetTag(tagNumber);

    /// <inheritdoc />
    public string GetTextRangeAsHtml(int position, int length) => ((IScintillaMethods)BaseControl.NativeControl).GetTextRangeAsHtml(position, length);

    /// <inheritdoc />
    public string GetWordFromPosition(int position) => ((IScintillaMethods)BaseControl.NativeControl).GetWordFromPosition(position);

    /// <inheritdoc />
    public void GotoPosition(int position) => ((IScintillaMethods)BaseControl.NativeControl).GotoPosition(position);

    /// <inheritdoc />
    public void HideLines(int lineStart, int lineEnd) => ((IScintillaMethods)BaseControl.NativeControl).HideLines(lineStart, lineEnd);

    /// <inheritdoc />
    public uint IndicatorAllOnFor(int position) => ((IScintillaMethods)BaseControl.NativeControl).IndicatorAllOnFor(position);

    /// <inheritdoc />
    public void IndicatorClearRange(int position, int length) => ((IScintillaMethods)BaseControl.NativeControl).IndicatorClearRange(position, length);

    /// <inheritdoc />
    public void IndicatorFillRange(int position, int length) => ((IScintillaMethods)BaseControl.NativeControl).IndicatorFillRange(position, length);

    /// <inheritdoc />
    public void InsertText(int position, string text) => ((IScintillaMethods)BaseControl.NativeControl).InsertText(position, text);

    /// <inheritdoc />
    public bool IsRangeWord(int start, int end) => ((IScintillaMethods)BaseControl.NativeControl).IsRangeWord(start, end);

    /// <inheritdoc />
    public int LineFromPosition(int position) => ((IScintillaMethods)BaseControl.NativeControl).LineFromPosition(position);

    /// <inheritdoc />
    public void LineScroll(int lines, int columns) => ((IScintillaMethods)BaseControl.NativeControl).LineScroll(lines, columns);

    /// <inheritdoc />
    public void LoadLexerLibrary(string path) => ((IScintillaMethods)BaseControl.NativeControl).LoadLexerLibrary(path);

    /// <inheritdoc />
    public void MarkerDeleteHandle(MarkerHandle markerHandle) => ((IScintillaMethods)BaseControl.NativeControl).MarkerDeleteHandle(markerHandle);

    /// <inheritdoc />
    public void MarkerEnableHighlight(bool enabled) => ((IScintillaMethods)BaseControl.NativeControl).MarkerEnableHighlight(enabled);

    /// <inheritdoc />
    public int MarkerLineFromHandle(MarkerHandle markerHandle) => ((IScintillaMethods)BaseControl.NativeControl).MarkerLineFromHandle(markerHandle);

    /// <inheritdoc />
    public void MultiEdgeClearAll() => ((IScintillaMethods)BaseControl.NativeControl).MultiEdgeClearAll();

    /// <inheritdoc />
    public void MultipleSelectAddEach() => ((IScintillaMethods)BaseControl.NativeControl).MultipleSelectAddEach();

    /// <inheritdoc />
    public void MultipleSelectAddNext() => ((IScintillaMethods)BaseControl.NativeControl).MultipleSelectAddNext();

    /// <inheritdoc />
    public void Paste() => ((IScintillaMethods)BaseControl.NativeControl).Paste();

    /// <inheritdoc />
    public int PointXFromPosition(int pos) => ((IScintillaMethods)BaseControl.NativeControl).PointXFromPosition(pos);

    /// <inheritdoc />
    public int PointYFromPosition(int pos) => ((IScintillaMethods)BaseControl.NativeControl).PointYFromPosition(pos);

    /// <inheritdoc />
    public string PropertyNames() => ((IScintillaMethods)BaseControl.NativeControl).PropertyNames();

    /// <inheritdoc />
    public PropertyType PropertyType(string name) => ((IScintillaMethods)BaseControl.NativeControl).PropertyType(name);

    /// <inheritdoc />
    public void Redo() => ((IScintillaMethods)BaseControl.NativeControl).Redo();

    /// <inheritdoc />
    public void ReleaseDocument(Document document) => ((IScintillaMethods)BaseControl.NativeControl).ReleaseDocument(document);

    /// <inheritdoc />
    public void ReplaceSelection(string text) => ((IScintillaMethods)BaseControl.NativeControl).ReplaceSelection(text);

    /// <inheritdoc />
    public int ReplaceTarget(string text) => ((IScintillaMethods)BaseControl.NativeControl).ReplaceTarget(text);

    /// <inheritdoc />
    public int ReplaceTargetRe(string text) => ((IScintillaMethods)BaseControl.NativeControl).ReplaceTargetRe(text);

    /// <inheritdoc />
    public void RotateSelection() => ((IScintillaMethods)BaseControl.NativeControl).RotateSelection();

    /// <inheritdoc />
    public void ScrollCaret() => ((IScintillaMethods)BaseControl.NativeControl).ScrollCaret();

    /// <inheritdoc />
    public void ScrollRange(int start, int end) => ((IScintillaMethods)BaseControl.NativeControl).ScrollRange(start, end);

    /// <inheritdoc />
    public int SearchInTarget(string text) => ((IScintillaMethods)BaseControl.NativeControl).SearchInTarget(text);

    /// <inheritdoc />
    public void SelectAll() => ((IScintillaMethods)BaseControl.NativeControl).SelectAll();

    /// <inheritdoc />
    public void SetEmptySelection(int pos) => ((IScintillaMethods)BaseControl.NativeControl).SetEmptySelection(pos);

    /// <inheritdoc />
    public void SetXCaretPolicy(CaretPolicy caretPolicy, int caretSlop) =>
        ((IScintillaMethods)BaseControl.NativeControl).SetXCaretPolicy(caretPolicy, caretSlop);

    /// <inheritdoc />
    public void SetYCaretPolicy(CaretPolicy caretPolicy, int caretSlop) =>
        ((IScintillaMethods)BaseControl.NativeControl).SetYCaretPolicy(caretPolicy, caretSlop);

    /// <inheritdoc />
    public void SetFoldFlags(FoldFlags flags) => ((IScintillaMethods)BaseControl.NativeControl).SetFoldFlags(flags);

    /// <inheritdoc />
    public void SetIdentifiers(int style, string identifiers) => ((IScintillaMethods)BaseControl.NativeControl).SetIdentifiers(style, identifiers);

    /// <inheritdoc />
    public void SetKeywords(int set, string keywords) => ((IScintillaMethods)BaseControl.NativeControl).SetKeywords(set, keywords);

    /// <inheritdoc />
    public void SetProperty(string name, string value) => ((IScintillaMethods)BaseControl.NativeControl).SetProperty(name, value);

    /// <inheritdoc />
    public void SetSavePoint() => ((IScintillaMethods)BaseControl.NativeControl).SetSavePoint();

    /// <inheritdoc />
    public void SetSel(int anchorPos, int currentPos) => ((IScintillaMethods)BaseControl.NativeControl).SetSel(anchorPos, currentPos);

    /// <inheritdoc />
    public void SetSelection(int caret, int anchor) => ((IScintillaMethods)BaseControl.NativeControl).SetSelection(caret, anchor);

    /// <inheritdoc />
    public void SetStyling(int length, int style) => ((IScintillaMethods)BaseControl.NativeControl).SetStyling(length, style);

    /// <inheritdoc />
    public void SetTargetRange(int start, int end) => ((IScintillaMethods)BaseControl.NativeControl).SetTargetRange(start, end);

    /// <inheritdoc />
    public void ShowLines(int lineStart, int lineEnd) => ((IScintillaMethods)BaseControl.NativeControl).ShowLines(lineStart, lineEnd);

    /// <inheritdoc />
    public void StartStyling(int position) => ((IScintillaMethods)BaseControl.NativeControl).StartStyling(position);

    /// <inheritdoc />
    public void StyleClearAll() => ((IScintillaMethods)BaseControl.NativeControl).StyleClearAll();

    /// <inheritdoc />
    public void StyleResetDefault() => ((IScintillaMethods)BaseControl.NativeControl).StyleResetDefault();

    /// <inheritdoc />
    public void SwapMainAnchorCaret() => ((IScintillaMethods)BaseControl.NativeControl).SwapMainAnchorCaret();

    /// <inheritdoc />
    public void TargetFromSelection() => ((IScintillaMethods)BaseControl.NativeControl).TargetFromSelection();

    /// <inheritdoc />
    public void TargetWholeDocument() => ((IScintillaMethods)BaseControl.NativeControl).TargetWholeDocument();

    /// <inheritdoc />
    public int TextWidth(int style, string text) => ((IScintillaMethods)BaseControl.NativeControl).TextWidth(style, text);

    /// <inheritdoc />
    public void Undo() => ((IScintillaMethods)BaseControl.NativeControl).Undo();

    /// <inheritdoc />
    public void UsePopup(bool enablePopup) => ((IScintillaMethods)BaseControl.NativeControl).UsePopup(enablePopup);

    /// <inheritdoc />
    public void UsePopup(PopupMode popupMode) => ((IScintillaMethods)BaseControl.NativeControl).UsePopup(popupMode);

    /// <inheritdoc />
    public int WordEndPosition(int position, bool onlyWordCharacters) => ((IScintillaMethods)BaseControl.NativeControl).WordEndPosition(position, onlyWordCharacters);

    /// <inheritdoc />
    public int WordStartPosition(int position, bool onlyWordCharacters) => ((IScintillaMethods)BaseControl.NativeControl).WordStartPosition(position, onlyWordCharacters);

    /// <inheritdoc />
    public void ZoomIn() => ((IScintillaMethods)BaseControl.NativeControl).ZoomIn();

    /// <inheritdoc />
    public void ZoomOut() => ((IScintillaMethods)BaseControl.NativeControl).ZoomOut();

    /// <inheritdoc />
    public void SetRepresentation(string encodedString, string representationString) => ((IScintillaMethods)BaseControl.NativeControl).SetRepresentation(encodedString, representationString);

    /// <inheritdoc />
    public string GetRepresentation(string encodedString) => ((IScintillaMethods)BaseControl.NativeControl).GetRepresentation(encodedString);

    /// <inheritdoc />
    public void ClearRepresentation(string encodedString) => ((IScintillaMethods)BaseControl.NativeControl).ClearRepresentation(encodedString);

    /// <inheritdoc />
    public void CallTipSetForeHlt(Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).CallTipSetForeHlt(color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void MultiEdgeAddLine(int column, Color edgeColor)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).MultiEdgeAddLine(column, edgeColor.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetAdditionalSelBack(Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetAdditionalSelBack(color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetAdditionalSelFore(Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetAdditionalSelFore(color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetFoldMarginColor(bool use, Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetFoldMarginColor(use, color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetFoldMarginHighlightColor(bool use, Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetFoldMarginHighlightColor(use, color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetSelectionBackColor(bool use, Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetSelectionBackColor(use, color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetSelectionForeColor(bool use, Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetSelectionForeColor(use, color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetWhitespaceBackColor(bool use, Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetWhitespaceBackColor(use, color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void SetWhitespaceForeColor(bool use, Color color)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).SetWhitespaceForeColor(use, color.ToSD());
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void AssignCmdKey(Keys keyDefinition, Command sciCommand)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).AssignCmdKey(keyDefinition.ToSWF(), sciCommand);
#elif Linux
#elif OSX
#endif
    }

    /// <inheritdoc />
    public void ClearCmdKey(Keys keyDefinition)
    {
#if Windows
        ((IScintillaWinForms)BaseControl.NativeControl).ClearCmdKey(keyDefinition.ToSWF());
#elif Linux
#elif OSX
#endif
    }
}