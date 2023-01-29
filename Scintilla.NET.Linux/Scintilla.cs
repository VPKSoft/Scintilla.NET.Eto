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

using System;
using System.Runtime.InteropServices;
using System.Text;
using Gtk;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Linux.Collections;
using Scintilla.NET.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;
using Key = Gdk.Key;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Structs;
using Style = Scintilla.NET.Linux.Collections.Style;
using Selection = Scintilla.NET.Linux.Collections.Selection;
using TabDrawMode = Scintilla.NET.Abstractions.Enumerations.TabDrawMode;
using WrapMode = Scintilla.NET.Abstractions.Enumerations.WrapMode;

namespace Scintilla.NET.Linux;

public class Scintilla : Widget, IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection,
        SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>,
    IScintillaProperties<Color>,
    IScintillaCollectionProperties<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection,
        MarginCollection,
        SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>,
    IScintillaMethods<Color, Key, Image>,
    IScintillaEvents<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection,
        SelectionCollection, SCNotificationEventArgs, Marker, Style, Indicator, Line, Margin, Selection, Image, Color,
        Key,
        AutoCSelectionEventArgs, BeforeModificationEventArgs, ChangeAnnotationEventArgs, CharAddedEventArgs,
        DoubleClickEventArgs,
        DwellEventArgs, CallTipClickEventArgs, HotspotClickEventArgs, IndicatorClickEventArgs, IndicatorReleaseEventArgs
        ,
        InsertCheckEventArgs, MarginClickEventArgs, NeedShownEventArgs, StyleNeededEventArgs, UpdateUIEventArgs,
        SCNotificationEventArgs>
{
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Scintilla" /> class.
    /// </summary>
    public Scintilla() : base(scintilla_new())
    {
        editor = base.Raw;
        Lines = new LineCollection(this);
        Styles = new StyleCollection(this);
        Indicators = new IndicatorCollection(this);
        Margins = new MarginCollection(this);
        Markers = new MarkerCollection(this);
        Selections = new SelectionCollection(this);
    }

    /// <summary>
    /// Create a new Scintilla widget. The returned pointer can be added to a container and displayed in the same way as other widgets.
    /// </summary>
    /// <returns>IntPtr.</returns>
    [DllImport("scintilla", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern IntPtr scintilla_new();

    /// <summary>
    /// The main entry point allows sending any of the messages described in this document.
    /// </summary>
    /// <param name="ptr">The ScintillaObject pointer.</param>
    /// <param name="iMessage">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>IntPtr.</returns>
    // ReSharper disable once StringLiteralTypo
    [DllImport("libscintilla", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr scintilla_send_message(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);

    // ReSharper disable once StringLiteralTypo
    [DllImport("libscintilla", CallingConvention = CallingConvention.Cdecl)]
    private static extern void scintilla_release_resources();

    readonly IntPtr editor;

    /// <inheritdoc />
    public Encoding Encoding { get; }

    /// <inheritdoc />
    public void LoadLexerLibrary(string path)
    {
        throw new NotImplementedException();
    }

    private static ILexilla? lexillaInstance;

    /// <summary>
    /// Gets the singleton instance of the <see cref="Lexilla"/> class.
    /// </summary>
    /// <value>The singleton instance of the <see cref="Lexilla"/> class.</value>
    private static ILexilla LexillaSingleton
    {
        get
        {
            lexillaInstance ??= new Lexilla();
            return lexillaInstance;
        }
    }

    /// <inheritdoc cref="IScintillaControl.SetParameter"/>
    public IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam)
    {
        return scintilla_send_message(editor, message, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int)"/>
    public IntPtr DirectMessage(int msg)
    {
        return SetParameter(msg, IntPtr.Zero, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam)
    {
        return SetParameter(msg, wParam, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        return SetParameter(msg, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
    {
        return scintilla_send_message(sciPtr, msg, wParam, lParam);
    }

    #region Methods
    /// <inheritdoc />
    public void MarkerDeleteAll(int marker)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MarkerDeleteHandle(MarkerHandle markerHandle)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MarkerEnableHighlight(bool enabled)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int MarkerLineFromHandle(MarkerHandle markerHandle)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MultiEdgeAddLine(int column, Color edgeColor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MultiEdgeClearAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MultipleSelectAddEach()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MultipleSelectAddNext()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Paste()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int PointXFromPosition(int pos)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int PointYFromPosition(int pos)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string PropertyNames()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public PropertyType PropertyType(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Redo()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void RegisterRgbaImage(int type, Image image)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ReleaseDocument(Document document)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ReplaceSelection(string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int ReplaceTarget(string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int ReplaceTargetRe(string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void RotateSelection()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ScrollCaret()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ScrollRange(int start, int end)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int SearchInTarget(string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SelectAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetAdditionalSelBack(Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetAdditionalSelFore(Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetEmptySelection(int pos)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetFoldFlags(FoldFlags flags)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetFoldMarginColor(bool use, Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetFoldMarginHighlightColor(bool use, Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetIdentifiers(int style, string identifiers)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetKeywords(int set, string keywords)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetProperty(string name, string value)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetSavePoint()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetSel(int anchorPos, int currentPos)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetSelection(int caret, int anchor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetSelectionBackColor(bool use, Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetSelectionForeColor(bool use, Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetStyling(int length, int style)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetTargetRange(int start, int end)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetWhitespaceBackColor(bool use, Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetWhitespaceForeColor(bool use, Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ShowLines(int lineStart, int lineEnd)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void StartStyling(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void StyleClearAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void StyleResetDefault()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SwapMainAnchorCaret()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void TargetFromSelection()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void TargetWholeDocument()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int TextWidth(int style, string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Undo()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void UsePopup(bool enablePopup)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void UsePopup(PopupMode popupMode)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int WordEndPosition(int position, bool onlyWordCharacters)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int WordStartPosition(int position, bool onlyWordCharacters)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ZoomIn()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ZoomOut()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SetRepresentation(string encodedString, string representationString)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string GetRepresentation(string encodedString)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearRepresentation(string encodedString)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    void IScintillaApi.MarkerDeleteAll(int marker)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int TextLength { get; }

    /// <inheritdoc />
    public string GetTag(int tagNumber)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    string IScintillaMethods<Color, Key, Image>.GetTextRange(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string GetTextRangeAsHtml(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string GetWordFromPosition(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void GotoPosition(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void HideLines(int lineStart, int lineEnd)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public uint IndicatorAllOnFor(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void IndicatorClearRange(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void IndicatorFillRange(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void InsertText(int position, string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool IsRangeWord(int start, int end)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int LineFromPosition(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void LineScroll(int lines, int columns)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    string IScintillaApi.GetTextRange(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AddRefDocument(Document document)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AddSelection(int caret, int anchor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AddText(string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int AllocateSubStyles(int styleBase, int numberStyles)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AnnotationClearAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AppendText(string text)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AssignCmdKey(Key keyDefinition, Command sciCommand)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AutoCCancel()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AutoCComplete()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AutoCSelect(string select)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AutoCSetFillUps(string chars)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AutoCShow(int lenEntered, string list)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AutoCStops(string chars)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void BeginUndoAction()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void BraceBadLight(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void BraceHighlight(int position1, int position2)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int BraceMatch(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CallTipCancel()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CallTipSetForeHlt(Color color)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CallTipSetHlt(int hlStart, int hlEnd)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CallTipSetPosition(bool above)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CallTipShow(int posStart, string definition)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CallTipTabSize(int tabSize)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ChangeLexerState(int startPos, int endPos)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int CharPositionFromPoint(int x, int y)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int CharPositionFromPointClose(int x, int y)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ChooseCaretX()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Clear()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearCmdKey(Key keyDefinition)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearAllCmdKeys()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearDocumentStyle()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearRegisteredImages()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ClearSelections()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Colorize(int startPos, int endPos)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ConvertEols(Eol eolMode)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Copy()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Copy(CopyFormat format)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CopyAllowLine()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CopyAllowLine(CopyFormat format)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CopyRange(int start, int end)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CopyRange(int start, int end, CopyFormat format)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Document CreateDocument()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ILoader CreateLoader(int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Cut()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void DeleteRange(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string DescribeKeywordSets()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string DescribeProperty(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int DocLineFromVisible(int displayLine)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void DropSelection(int selection)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void EmptyUndoBuffer()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void EndUndoAction()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ExecuteCmd(Command sciCommand)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    void IScintillaMethods<Color, Key, Image>.FoldAll(FoldAction action)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void FoldDisplayTextSetStyle(FoldDisplayText style)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void FreeSubStyles()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetCharAt(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetColumn(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetEndStyled()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetPrimaryStyleFromStyle(int style)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string GetProperty(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string GetPropertyExpanded(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetPropertyInt(string name, int defaultValue)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetStyleAt(int position)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetStyleFromSubStyle(int subStyle)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetSubStylesLength(int styleBase)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int GetSubStylesStart(int styleBase)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void FoldAll(FoldAction action)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Events
    /// <inheritdoc />
    public event EventHandler<EventArgs>? AutoCCancelled;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? AutoCCharDeleted;

    /// <inheritdoc />
    public event EventHandler<AutoCSelectionEventArgs>? AutoCCompleted;

    /// <inheritdoc />
    public event EventHandler<AutoCSelectionEventArgs>? AutoCSelection;

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? BeforeDelete;

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? BeforeInsert;

    /// <inheritdoc />
    public event EventHandler<ChangeAnnotationEventArgs>? ChangeAnnotation;

    /// <inheritdoc />
    public event EventHandler<CharAddedEventArgs>? CharAdded;

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? Delete;

    /// <inheritdoc />
    public event EventHandler<DoubleClickEventArgs>? DoubleClick;

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellEnd;

    /// <inheritdoc />
    public event EventHandler<CallTipClickEventArgs>? CallTipClick;

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellStart;

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs>? HotspotClick;

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs>? HotspotDoubleClick;

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs>? HotspotReleaseClick;

    /// <inheritdoc />
    public event EventHandler<IndicatorClickEventArgs>? IndicatorClick;

    /// <inheritdoc />
    public event EventHandler<IndicatorReleaseEventArgs>? IndicatorRelease;

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? Insert;

    /// <inheritdoc />
    public event EventHandler<InsertCheckEventArgs>? InsertCheck;

    /// <inheritdoc />
    public event EventHandler<MarginClickEventArgs>? MarginClick;

    /// <inheritdoc />
    public event EventHandler<MarginClickEventArgs>? MarginRightClick;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? ModifyAttempt;

    /// <inheritdoc />
    public event EventHandler<NeedShownEventArgs>? NeedShown;

    /// <inheritdoc />
    public event EventHandler<SCNotificationEventArgs>? SCNotification;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? Painted;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? SavePointLeft;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? SavePointReached;

    /// <inheritdoc />
    public event EventHandler<StyleNeededEventArgs>? StyleNeeded;

    /// <inheritdoc />
    public event EventHandler<UpdateUIEventArgs>? UpdateUi;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? ZoomChanged;
    #endregion

    #region Properties
    /// <inheritdoc />
    public MarkerCollection Markers { get; }

    /// <inheritdoc />
    public StyleCollection Styles { get; }

    /// <inheritdoc />
    public IndicatorCollection Indicators { get; }

    /// <inheritdoc />
    public LineCollection Lines { get; }

    /// <inheritdoc />
    public MarginCollection Margins { get; }

    /// <inheritdoc />
    public SelectionCollection Selections { get; }

    /// <inheritdoc />
    public BiDirectionalDisplayType BiDirectionality { get; set; }

    /// <inheritdoc />
    public Color AdditionalCaretForeColor { get; set; }

    /// <inheritdoc />
    public bool AdditionalCaretsBlink { get; set; }

    /// <inheritdoc />
    public bool AdditionalCaretsVisible { get; set; }

    /// <inheritdoc />
    public int AnchorPosition { get; set; }

    /// <inheritdoc />
    public int AdditionalSelAlpha { get; set; }

    /// <inheritdoc />
    public bool AdditionalSelectionTyping { get; set; }

    /// <inheritdoc />
    public Annotation AnnotationVisible { get; set; }

    /// <inheritdoc />
    public bool AutoCActive { get; }

    /// <inheritdoc />
    public bool AutoCAutoHide { get; set; }

    /// <inheritdoc />
    public bool AutoCCancelAtStart { get; set; }

    /// <inheritdoc />
    public int AutoCCurrent { get; }

    /// <inheritdoc />
    public bool AutoCChooseSingle { get; set; }

    /// <inheritdoc />
    public bool AutoCDropRestOfWord { get; set; }

    /// <inheritdoc />
    public bool AutoCIgnoreCase { get; set; }

    /// <inheritdoc />
    public int AutoCMaxHeight { get; set; }

    /// <inheritdoc />
    public int AutoCMaxWidth { get; set; }

    /// <inheritdoc />
    public Order AutoCOrder { get; set; }

    /// <inheritdoc />
    public int AutoCPosStart { get; }

    /// <inheritdoc />
    public Document Document { get; set; }

    /// <inheritdoc />
    public int RectangularSelectionAnchor { get; set; }

    /// <inheritdoc />
    public int RectangularSelectionCaret { get; set; }

    /// <inheritdoc />
    public char AutoCSeparator { get; set; }

    /// <inheritdoc />
    public char AutoCTypeSeparator { get; set; }

    /// <inheritdoc />
    public AutomaticFold AutomaticFold { get; set; }

    /// <inheritdoc />
    public bool BackspaceUnIndents { get; set; }

    /// <inheritdoc />
    public bool BufferedDraw { get; set; }

    /// <inheritdoc />
    public bool CallTipActive { get; }

    /// <inheritdoc />
    public bool CanPaste { get; }

    /// <inheritdoc />
    public bool CanRedo { get; }

    /// <inheritdoc />
    public bool CanUndo { get; }

    /// <inheritdoc />
    public Color CaretForeColor { get; set; }

    /// <inheritdoc />
    public Color CaretLineBackColor { get; set; }

    /// <inheritdoc />
    public int CaretLineBackColorAlpha { get; set; }

    /// <inheritdoc />
    public int CaretLineFrame { get; set; }

    /// <inheritdoc />
    public bool CaretLineVisible { get; set; }

    /// <inheritdoc />
    public bool CaretLineVisibleAlways { get; set; }

    /// <inheritdoc />
    public Layer CaretLineLayer { get; set; }

    /// <inheritdoc />
    public int CaretPeriod { get; set; }

    /// <inheritdoc />
    public CaretStyle CaretStyle { get; set; }

    /// <inheritdoc />
    public int CaretWidth { get; set; }

    /// <inheritdoc />
    public int CurrentLine { get; }

    /// <inheritdoc />
    public int CurrentPosition { get; set; }

    /// <inheritdoc />
    public int DistanceToSecondaryStyles { get; }

    /// <inheritdoc />
    public Color EdgeColor { get; set; }

    /// <inheritdoc />
    public int EdgeColumn { get; set; }

    /// <inheritdoc />
    public EdgeMode EdgeMode { get; set; }

    /// <inheritdoc />
    public bool EndAtLastLine { get; set; }

    /// <inheritdoc />
    public Eol EolMode { get; set; }

    /// <inheritdoc />
    public int ExtraAscent { get; set; }

    /// <inheritdoc />
    public int ExtraDescent { get; set; }

    /// <inheritdoc />
    public int FirstVisibleLine { get; set; }

    /// <inheritdoc />
    public FontQuality FontQuality { get; set; }

    /// <inheritdoc />
    public int HighlightGuide { get; set; }

    /// <inheritdoc />
    public bool HScrollBar { get; set; }

    /// <inheritdoc />
    public IdleStyling IdleStyling { get; set; }

    /// <inheritdoc />
    public int IndentWidth { get; set; }

    /// <inheritdoc />
    public IndentView IndentationGuides { get; set; }

    /// <inheritdoc />
    public int IndicatorCurrent { get; set; }

    /// <inheritdoc />
    public int IndicatorValue { get; set; }

    /// <inheritdoc />
    public bool InternalFocusFlag { get; set; }

    /// <inheritdoc />
    public string LexerName { get; set; }

    /// <inheritdoc />
    public Layer SelectionLayer { get; set; }

    /// <inheritdoc />
    public int SelectionEnd { get; set; }

    /// <inheritdoc />
    public int SelectionStart { get; set; }

    /// <inheritdoc />
    public Lexer Lexer { get; set; }

    /// <inheritdoc />
    public string LexerLanguage { get; set; }

    /// <inheritdoc />
    public LineEndType LineEndTypesActive { get; }

    /// <inheritdoc />
    public LineEndType LineEndTypesAllowed { get; set; }

    /// <inheritdoc />
    public LineEndType LineEndTypesSupported { get; }

    /// <inheritdoc />
    public int LinesOnScreen { get; }

    /// <inheritdoc />
    public int MainSelection { get; set; }

    /// <inheritdoc />
    public bool Modified { get; }

    /// <inheritdoc />
    public int MouseDwellTime { get; set; }

    /// <inheritdoc />
    public bool MouseSelectionRectangularSwitch { get; set; }

    /// <inheritdoc />
    public bool MultipleSelection { get; set; }

    /// <inheritdoc />
    public MultiPaste MultiPaste { get; set; }

    /// <inheritdoc />
    public bool OverType { get; set; }

    /// <inheritdoc />
    public bool PasteConvertEndings { get; set; }

    /// <inheritdoc />
    public Phases PhasesDraw { get; set; }

    /// <inheritdoc />
    public bool ReadOnly { get; set; }

    /// <inheritdoc />
    public int RectangularSelectionAnchorVirtualSpace { get; set; }

    /// <inheritdoc />
    public int RectangularSelectionCaretVirtualSpace { get; set; }

    /// <inheritdoc />
    public int ScrollWidth { get; set; }

    /// <inheritdoc />
    public bool ScrollWidthTracking { get; set; }

    /// <inheritdoc />
    public SearchFlags SearchFlags { get; set; }

    /// <inheritdoc />
    public string SelectedText { get; }

    /// <inheritdoc />
    public bool SelectionEolFilled { get; set; }

    /// <inheritdoc />
    public Status Status { get; set; }

    /// <inheritdoc />
    public TabDrawMode TabDrawMode { get; set; }

    /// <inheritdoc />
    public bool TabIndents { get; set; }

    /// <inheritdoc />
    public int TabWidth { get; set; }

    /// <inheritdoc />
    public int TargetEnd { get; set; }

    /// <inheritdoc />
    public int TargetStart { get; set; }

    /// <inheritdoc />
    public string TargetText { get; }

    /// <inheritdoc />
    public Technology Technology { get; set; }

    /// <inheritdoc />
    public string Text { get; set; }

    /// <inheritdoc />
    public bool UseTabs { get; set; }

    /// <inheritdoc />
    public bool ViewEol { get; set; }

    /// <inheritdoc />
    public WhitespaceMode ViewWhitespace { get; set; }

    /// <inheritdoc />
    public VirtualSpace VirtualSpaceOptions { get; set; }

    /// <inheritdoc />
    public bool VScrollBar { get; set; }

    /// <inheritdoc />
    public int WhitespaceSize { get; set; }

    /// <inheritdoc />
    public string WordChars { get; set; }

    /// <inheritdoc />
    public WrapIndentMode WrapIndentMode { get; set; }

    /// <inheritdoc />
    public WrapMode WrapMode { get; set; }

    /// <inheritdoc />
    public int WrapStartIndent { get; set; }

    /// <inheritdoc />
    public WrapVisualFlags WrapVisualFlags { get; set; }

    /// <inheritdoc />
    public WrapVisualFlagLocation WrapVisualFlagLocation { get; set; }

    /// <inheritdoc />
    public int XOffset { get; set; }

    /// <inheritdoc />
    public int Zoom { get; set; }
    #endregion
}