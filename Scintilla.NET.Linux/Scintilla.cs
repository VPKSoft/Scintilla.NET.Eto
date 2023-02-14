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
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Extensions;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Structs;
using ScintillaNet.Linux.Collections;
using ScintillaNet.Linux.EventArguments;
using ScintillaNet.Linux.GdkUtils;
using Color = Gdk.Color;
using Image = Gtk.Image;
using Key = Gdk.Key;
using Style = ScintillaNet.Linux.Collections.Style;
using Status = ScintillaNet.Abstractions.Enumerations.Status;
using TabDrawMode = ScintillaNet.Abstractions.Enumerations.TabDrawMode;
using WrapMode = ScintillaNet.Abstractions.Enumerations.WrapMode;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Linux;

using Keys = Gdk.Key;

/// <summary>
/// Represents a Scintilla editor control.
/// </summary>
public class Scintilla : Widget, IScintillaLinux
{
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Scintilla" /> class.
    /// </summary>
    public Scintilla() : base(scintilla_new())
    {
        editor = base.Raw;
        Styles = new StyleCollection(this);
        Margins = new MarginCollection(this);
        Markers = new MarkerCollection(this);
        Lines = new LineCollection(this, Styles, Markers);
        Indicators = new IndicatorCollection(this, Lines);
        Selections = new SelectionCollection(this, Lines);
        
        this.SCNotification += Lines.ScNotificationCallback;
        
        AddSignalHandler ("sci-notify", OnSciNotified, new SciNotifyDelegate(OnSciNotified));
    }

    /// <summary>
    /// Gets the Scintilla pointer.
    /// </summary>
    /// <value>The Scintilla pointer.</value>
    public IntPtr SciPointer => editor;

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        RemoveSignalHandler("sci-notify", OnSciNotified);
    }

    private void OnSciNotified(IntPtr widget, IntPtr _, IntPtr notification, IntPtr userData)
    {
        var scn = (ScintillaApiStructs.SCNotification)Marshal.PtrToStructure(notification, typeof(ScintillaApiStructs.SCNotification))!;
        if (scn.nmhdr.code is >= SCN_STYLENEEDED and <= SCN_AUTOCCOMPLETED)
        {
            this.SCNotification?.Invoke(this, new SCNotificationEventArgs(scn));
            switch (scn.nmhdr.code)
            {
                case SCN_PAINTED:
                    this.Painted?.Invoke(this, EventArgs.Empty);
                    break;
                case SCN_MODIFIED:
                    this.ScnModified(Lines, ref scn, InsertCheck, BeforeInsert, BeforeDelete, Insert, Delete, ChangeAnnotation,
                        ref cachedPosition, ref cachedText);
                    break;
                case SCN_MODIFYATTEMPTRO:
                    ModifyAttempt?.Invoke(this, EventArgs.Empty);
                    break;
                case SCN_STYLENEEDED:
                    StyleNeeded?.Invoke(this, new StyleNeededEventArgs(this, Lines, scn.position.ToInt32()));
                    break;
                case SCN_SAVEPOINTLEFT:
                    SavePointLeft?.Invoke(this, EventArgs.Empty);
                    break;                
                case SCN_SAVEPOINTREACHED:
                    SavePointReached?.Invoke(this, EventArgs.Empty);
                    break;   
                case SCN_MARGINCLICK:
                case SCN_MARGINRIGHTCLICK:
                    this.ScnMarginClick(Lines, ref scn, MarginClick, MarginRightClick);
                    break;
                case SCN_UPDATEUI:
                    UpdateUi?.Invoke(this, new UpdateUIEventArgs(this, (UpdateChange)scn.updated));
                    break;
                case SCN_KEY:
                case SCI_ADDTEXT: // This is not in the documentation, but seems to be called when a "char is added"?
                    // The non-character keys seem to start from: Key.Key_3270_Duplicate == 64769 so assume 60000
                    if (scn.ch != 0 && scn.ch < 60000) 
                    {
                        this.CharAdded?.Invoke(this, new CharAddedEventArgs(scn.ch));
                    }
                    break;
                case SCN_AUTOCSELECTION:
                    AutoCSelection?.Invoke(this,
                        new AutoCSelectionEventArgs(this, Lines, scn.position.ToInt32(), scn.text, scn.ch,
                            (ListCompletionMethod) scn.listCompletionMethod));
                    break;                
                case SCN_AUTOCCOMPLETED:
                    AutoCCompleted?.Invoke(this,
                        new AutoCSelectionEventArgs(this, Lines, scn.position.ToInt32(), scn.text, scn.ch,
                            (ListCompletionMethod) scn.listCompletionMethod));
                    break;
                case SCN_AUTOCCANCELLED:
                    AutoCCancelled?.Invoke(this, EventArgs.Empty);
                    break;
                
                case SCN_AUTOCCHARDELETED:
                    AutoCCharDeleted?.Invoke(this, EventArgs.Empty);
                    break;                
                case SCN_DWELLSTART:
                    DwellStart?.Invoke(this, new DwellEventArgs(this, Lines, scn.position.ToInt32(), scn.x, scn.y));
                    break;
                case SCN_DWELLEND:
                    DwellEnd?.Invoke(this, new DwellEventArgs(this, Lines, scn.position.ToInt32(), scn.x, scn.y));
                    break;
                case SCN_DOUBLECLICK:
                    this.ScnDoubleClick(Lines, ref scn, DoubleClick);
                    break;
                case SCN_NEEDSHOWN:
                    NeedShown?.Invoke(this, new NeedShownEventArgs(this, Lines, scn.position.ToInt32(), scn.length.ToInt32()));
                    break;
                case SCN_HOTSPOTCLICK:
                case SCN_HOTSPOTDOUBLECLICK:
                case SCN_HOTSPOTRELEASECLICK:
                    this.ScnHotspotClick(Lines, ref scn, HotspotClick, HotspotDoubleClick);
                    break;
                case SCN_INDICATORCLICK:
                case SCN_INDICATORRELEASE:
                    this.ScnIndicatorClick(Lines, ref scn, IndicatorClick, IndicatorRelease);
                    break;
                case SCN_ZOOM:
                    ZoomChanged?.Invoke(this, EventArgs.Empty);
                    break;
                case SCN_CALLTIPCLICK:
                    // scn.position: 1 = Up Arrow, 2 = DownArrow: 0 = Elsewhere
                    CallTipClick?.Invoke(this, new CallTipClickEventArgs(this, (CallTipClickType)scn.position.ToInt32()));
                    break;                
            }
        }
    }

    /// <summary>
    /// A delegate to process the native Scintilla control notifications.
    /// </summary>
    /// <param name="widget">The widget pointer.</param>
    /// <param name="something">Unknown data with unknown use.</param>
    /// <param name="notification">The Scintilla notification code.</param>
    /// <param name="userData">The user data.</param>
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    private delegate void SciNotifyDelegate(IntPtr widget, IntPtr something, IntPtr notification, IntPtr userData);
    
    #region Fields
    // Pinned dataDwellStart
    private IntPtr fillUpChars;

    // For highlight calculations
    private string lastCallTip = string.Empty;

    // Set style
    private int stylingPosition;
    private int stylingBytePosition;
    
    // Modified event optimization
    private int? cachedPosition;
    private string? cachedText;    
    #endregion

    #region Native
    /// <summary>
    /// Create a new Scintilla widget. The returned pointer can be added to a container and displayed in the same way as other widgets.
    /// </summary>
    /// <returns>IntPtr.</returns>
    [DllImport("libscintilla", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
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
    [DllImport("libscintilla", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern IntPtr scintilla_send_message(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);


    readonly IntPtr editor;


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

    /// <inheritdoc cref="IScintillaApi.SetParameter"/>
    public IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam)
    {
        return scintilla_send_message(editor, message, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaApi.DirectMessage(int)"/>
    public IntPtr DirectMessage(int msg)
    {
        return SetParameter(msg, IntPtr.Zero, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaApi.DirectMessage(int, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam)
    {
        return SetParameter(msg, wParam, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaApi.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        return SetParameter(msg, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaApi.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
    {
        return scintilla_send_message(sciPtr, msg, wParam, lParam);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Increases the reference count of the specified document by 1.
    /// </summary>
    /// <param name="document">The document reference count to increase.</param>
    public void AddRefDocument(Document document)
    {
        this.AddRefDocumentExtension(document);
    }

    /// <summary>
    /// Adds an additional selection range to the existing main selection.
    /// </summary>
    /// <param name="caret">The zero-based document position to end the selection.</param>
    /// <param name="anchor">The zero-based document position to start the selection.</param>
    /// <remarks>A main selection must first have been set by a call to <see cref="SetSelection" />.</remarks>
    public void AddSelection(int caret, int anchor)
    {
        this.AddSelectionExtension(caret, anchor, Lines);
    }

    /// <summary>
    /// Inserts the specified text at the current caret position.
    /// </summary>
    /// <param name="text">The text to insert at the current caret position.</param>
    /// <remarks>The caret position is set to the end of the inserted text, but it is not scrolled into view.</remarks>
    public void AddText(string text)
    {
        this.AddTextExtension(text);
    }

    /// <summary>
    /// Allocates some number of sub-styles for a particular base style. Sub-styles are allocated contiguously.
    /// </summary>
    /// <param name="styleBase">The lexer style integer</param>
    /// <param name="numberStyles">The amount of sub-styles to allocate</param>
    /// <returns>Returns the first sub-style number allocated.</returns>
    public int AllocateSubStyles(int styleBase, int numberStyles)
    {
        return this.AllocateSubStylesExtension(styleBase, numberStyles);
    }

    /// <summary>
    /// Removes the annotation text for every <see cref="Line" /> in the document.
    /// </summary>
    public void AnnotationClearAll()
    {
        this.AnnotationClearAllExtension();
    }

    /// <summary>
    /// Adds the specified text to the end of the document.
    /// </summary>
    /// <param name="text">The text to add to the document.</param>
    /// <remarks>The current selection is not changed and the new text is not scrolled into view.</remarks>
    public void AppendText(string text)
    {
        this.AppendTextExtension(text);
    }

    /// <summary>
    /// Assigns the specified key definition to a <see cref="Scintilla" /> command.
    /// </summary>
    /// <param name="keyDefinition">The key combination to bind.</param>
    /// <param name="sciCommand">The command to assign.</param>
    public void AssignCmdKey(Key keyDefinition, Command sciCommand)
    {
        this.AssignCmdKeyExtension(keyDefinition, sciCommand, Helpers.TranslateKeys);
    }

    /// <summary>
    /// Cancels any displayed auto-completion list.
    /// </summary>
    /// <seealso cref="AutoCStops" />
    public void AutoCCancel()
    {
        this.AutoCCancelExtension();
    }

    /// <summary>
    /// Triggers completion of the current auto-completion word.
    /// </summary>
    public void AutoCComplete()
    {
        this.AutoCCompleteExtension();
    }

    /// <summary>
    /// Selects an item in the auto-completion list.
    /// </summary>
    /// <param name="select">
    /// The auto-completion word to select.
    /// If found, the word in the auto-completion list is selected and the index can be obtained by calling <see cref="AutoCCurrent" />.
    /// If not found, the behavior is determined by <see cref="AutoCAutoHide" />.
    /// </param>
    /// <remarks>
    /// Comparisons are performed according to the <see cref="AutoCIgnoreCase" /> property
    /// and will match the first word starting with <paramref name="select" />.
    /// </remarks>
    /// <seealso cref="AutoCCurrent" />
    /// <seealso cref="AutoCAutoHide" />
    /// <seealso cref="AutoCIgnoreCase" />
    public void AutoCSelect(string select)
    {
        this.AutoCSelectExtension(select);
    }

    /// <summary>
    /// Sets the characters that, when typed, cause the auto-completion item to be added to the document.
    /// </summary>
    /// <param name="chars">A string of characters that trigger auto-completion. The default is null.</param>
    /// <remarks>Common fill-up characters are '(', '[', and '.' depending on the language.</remarks>
    public void AutoCSetFillUps(string chars)
    {
        this.AutoCSetFillUpsExtension(chars, ref fillUpChars);
    }

    /// <summary>
    /// Displays an auto completion list.
    /// </summary>
    /// <param name="lenEntered">The number of characters already entered to match on.</param>
    /// <param name="list">A list of auto-completion words separated by the <see cref="AutoCSeparator" /> character.</param>
    public void AutoCShow(int lenEntered, string list)
    {
        this.AutoCShowExtension(lenEntered, list);
    }

    /// <summary>
    /// Specifies the characters that will automatically cancel auto-completion without the need to call <see cref="AutoCCancel" />.
    /// </summary>
    /// <param name="chars">A String of the characters that will cancel auto-completion. The default is empty.</param>
    /// <remarks>Characters specified should be limited to printable ASCII characters.</remarks>
    public void AutoCStops(string chars)
    {
        this.AutoCStopsExtension(chars);
    }

    /// <summary>
    /// Marks the beginning of a set of actions that should be treated as a single undo action.
    /// </summary>
    /// <remarks>A call to <see cref="BeginUndoAction" /> should be followed by a call to <see cref="EndUndoAction" />.</remarks>
    /// <seealso cref="EndUndoAction" />
    public void BeginUndoAction()
    {
        this.BeginUndoActionExtension();
    }

    /// <summary>
    /// Styles the specified character position with the <see cref="StyleConstants.BraceBad" /> style when there is an unmatched brace.
    /// </summary>
    /// <param name="position">The zero-based document position of the unmatched brace character or <seealso cref="ApiConstants.InvalidPosition"/> to remove the highlight.</param>
    public void BraceBadLight(int position)
    {
        this.BraceBadLightExtension(position, Lines);
    }

    /// <summary>
    /// Styles the specified character positions with the <see cref="StyleConstants.BraceLight" /> style.
    /// </summary>
    /// <param name="position1">The zero-based document position of the open brace character.</param>
    /// <param name="position2">The zero-based document position of the close brace character.</param>
    /// <remarks>Brace highlighting can be removed by specifying <see cref="ApiConstants.InvalidPosition" /> for <paramref name="position1" /> and <paramref name="position2" />.</remarks>
    /// <seealso cref="HighlightGuide" />
    public void BraceHighlight(int position1, int position2)
    {
        this.BraceHighlightExtension(position1, position2, Lines);
    }

    /// <summary>
    /// Finds a corresponding matching brace starting at the position specified.
    /// The brace characters handled are '(', ')', '', '{', '}', '&lt;', and '&gt;'.
    /// </summary>
    /// <param name="position">The zero-based document position of a brace character to start the search from for a matching brace character.</param>
    /// <returns>The zero-based document position of the corresponding matching brace or <see cref="ApiConstants.InvalidPosition" /> it no matching brace could be found.</returns>
    /// <remarks>A match only occurs if the style of the matching brace is the same as the starting brace. Nested braces are handled correctly.</remarks>
    public int BraceMatch(int position)
    {
        return this.BraceMatchExtension(position, Lines);
    }

    /// <summary>
    /// Cancels the display of a call tip window.
    /// </summary>
    public void CallTipCancel()
    {
        this.CallTipCancelExtension();
    }

    /// <summary>
    /// Sets the color of highlighted text in a call tip.
    /// </summary>
    /// <param name="color">The new highlight text Color. The default is dark blue.</param>
    public void CallTipSetForeHlt(Color color)
    {
        this.CallTipSetForeHltExtension(color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Sets the specified range of the call tip text to display in a highlighted style.
    /// </summary>
    /// <param name="hlStart">The zero-based index in the call tip text to start highlighting.</param>
    /// <param name="hlEnd">The zero-based index in the call tip text to stop highlighting (exclusive).</param>
    public void CallTipSetHlt(int hlStart, int hlEnd)
    {
        this.CallTipSetHltExtension(hlStart, hlEnd, lastCallTip);
    }

    /// <summary>
    /// Determines whether to display a call tip above or below text.
    /// </summary>
    /// <param name="above">true to display above text; otherwise, false. The default is false.</param>
    public void CallTipSetPosition(bool above)
    {
        this.CallTipSetPositionExtension(above);
    }

    /// <summary>
    /// Displays a call tip window.
    /// </summary>
    /// <param name="posStart">The zero-based document position where the call tip window should be aligned.</param>
    /// <param name="definition">The call tip text.</param>
    /// <remarks>
    /// Call tips can contain multiple lines separated by '\n' characters. Do not include '\r', as this will most likely print as an empty box.
    /// The '\t' character is supported and the size can be set by using <see cref="CallTipTabSize" />.
    /// </remarks>
    public void CallTipShow(int posStart, string definition)
    {
        this.CallTipShowExtension(posStart, definition, ref lastCallTip, Lines);
    }

    /// <summary>
    /// Sets the call tip tab size in pixels.
    /// </summary>
    /// <param name="tabSize">The width in pixels of a tab '\t' character in a call tip. Specifying 0 disables special treatment of tabs.</param>
    public void CallTipTabSize(int tabSize)
    {
        this.CallTipTabSizeExtension(tabSize);
    }

    /// <summary>
    /// Indicates to the current <see cref="Lexer" /> that the internal lexer state has changed in the specified
    /// range and therefore may need to be redrawn.
    /// </summary>
    /// <param name="startPos">The zero-based document position at which the lexer state change starts.</param>
    /// <param name="endPos">The zero-based document position at which the lexer state change ends.</param>
    public void ChangeLexerState(int startPos, int endPos)
    {
        this.ChangeLexerStateExtension(startPos, endPos, Lines);
    }

    /// <summary>
    /// Finds the closest character position to the specified display point.
    /// </summary>
    /// <param name="x">The x pixel coordinate within the client rectangle of the control.</param>
    /// <param name="y">The y pixel coordinate within the client rectangle of the control.</param>
    /// <returns>The zero-based document position of the nearest character to the point specified.</returns>
    public int CharPositionFromPoint(int x, int y)
    {
        return this.CharPositionFromPointExtension(x, y, Lines);
    }

    /// <summary>
    /// Finds the closest character position to the specified display point or returns -1
    /// if the point is outside the window or not close to any characters.
    /// </summary>
    /// <param name="x">The x pixel coordinate within the client rectangle of the control.</param>
    /// <param name="y">The y pixel coordinate within the client rectangle of the control.</param>
    /// <returns>The zero-based document position of the nearest character to the point specified when near a character; otherwise, -1.</returns>
    public int CharPositionFromPointClose(int x, int y)
    {
        return this.CharPositionFromPointCloseExtension(x, y, Lines);
    }

    /// <summary>
    /// Explicitly sets the current horizontal offset of the caret as the X position to track
    /// when the user moves the caret vertically using the up and down keys.
    /// </summary>
    /// <remarks>
    /// When not set explicitly, Scintilla automatically sets this value each time the user moves
    /// the caret horizontally.
    /// </remarks>
    public void ChooseCaretX()
    {
        this.ChooseCaretXExtension();
    }

    /// <summary>
    /// Removes the selected text from the document.
    /// </summary>
    public void Clear()
    {
        this.ClearExtension();
    }

    /// <summary>
    /// Deletes all document text, unless the document is read-only.
    /// </summary>
    public void ClearAll()
    {
        this.ClearAllExtension();
    }

    /// <summary>
    /// Makes the specified key definition do nothing.
    /// </summary>
    /// <param name="keyDefinition">The key combination to bind.</param>
    /// <remarks>This is equivalent to binding the keys to <see cref="Command.Null" />.</remarks>
    public void ClearCmdKey(Key keyDefinition)
    {
        this.ClearCmdKeyExtension(keyDefinition, Helpers.TranslateKeys);
    }

    /// <summary>
    /// Removes all the key definition command mappings.
    /// </summary>
    public void ClearAllCmdKeys()
    {
        this.ClearAllCmdKeysExtension();
    }

    /// <summary>
    /// Removes all styling from the document and resets the folding state.
    /// </summary>
    public void ClearDocumentStyle()
    {
        this.ClearDocumentStyleExtension();
    }

    /// <summary>
    /// Removes all images registered for auto-completion lists.
    /// </summary>
    public void ClearRegisteredImages()
    {
        this.ClearRegisteredImagesExtension();
    }

    /// <summary>
    /// Sets a single empty selection at the start of the document.
    /// </summary>
    public void ClearSelections()
    {
        this.ClearSelectionsExtension();
    }

    /// <summary>
    /// Requests that the current lexer restyle the specified range.
    /// </summary>
    /// <param name="startPos">The zero-based document position at which to start styling.</param>
    /// <param name="endPos">The zero-based document position at which to stop styling (exclusive).</param>
    /// <remarks>This will also cause fold levels in the range specified to be reset.</remarks>
    public void Colorize(int startPos, int endPos)
    {
       this.ColorizeExtension(startPos, endPos, Lines);
    }

    /// <summary>
    /// Changes all end-of-line characters in the document to the format specified.
    /// </summary>
    /// <param name="eolMode">One of the <see cref="Eol" /> enumeration values.</param>
    public void ConvertEols(Eol eolMode)
    {
        this.ConvertEolsExtension(eolMode);
    }

    /// <summary>
    /// Copies the selected text from the document and places it on the clipboard.
    /// </summary>
    public void Copy()
    {
        this.CopyExtension();
    }

    /// <inheritdoc />
    public void Copy(CopyFormat format)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Copies the selected text from the document and places it on the clipboard.
    /// If the selection is empty the current line is copied.
    /// </summary>
    /// <remarks>
    /// If the selection is empty and the current line copied, an extra "MSDEVLineSelect" marker is added to the
    /// clipboard which is then used in <see cref="Paste" /> to paste the whole line before the current line.
    /// </remarks>
    public void CopyAllowLine()
    {
        this.CopyAllowLineExtension();
    }

    /// <inheritdoc />
    public void CopyAllowLine(CopyFormat format)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Copies the specified range of text to the clipboard.
    /// </summary>
    /// <param name="start">The zero-based character position in the document to start copying.</param>
    /// <param name="end">The zero-based character position (exclusive) in the document to stop copying.</param>
    public void CopyRange(int start, int end)
    {
        this.CopyRangeExtension(start, end, Lines);
    }

    /// <inheritdoc />
    public void CopyRange(int start, int end, CopyFormat format)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create a new, empty document.
    /// </summary>
    /// <returns>A new <see cref="Document" /> with a reference count of 1.</returns>
    /// <remarks>You are responsible for ensuring the reference count eventually reaches 0 or memory leaks will occur.</remarks>
    public Document CreateDocument()
    {
        return this.CreateDocumentExtension();
    }

    /// <inheritdoc />
    public ILoader CreateLoader(int length)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Cuts the selected text from the document and places it on the clipboard.
    /// </summary>
    public void Cut()
    {
        this.CutExtension();
    }

    /// <summary>
    /// Deletes a range of text from the document.
    /// </summary>
    /// <param name="position">The zero-based character position to start deleting.</param>
    /// <param name="length">The number of characters to delete.</param>
    public void DeleteRange(int position, int length)
    {
        this.DeleteRangeExtension(position, length, Lines);
    }

    /// <summary>
    /// Retrieves a description of keyword sets supported by the current <see cref="Lexer" />.
    /// </summary>
    /// <returns>A String describing each keyword set separated by line breaks for the current lexer.</returns>
    public string DescribeKeywordSets()
    {
        return this.DescribeKeywordSetsExtension();
    }

    /// <summary>
    /// Retrieves a brief description of the specified property name for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="name">A property name supported by the current <see cref="Lexer" />.</param>
    /// <returns>A String describing the lexer property name if found; otherwise, String.Empty.</returns>
    /// <remarks>A list of supported property names for the current <see cref="Lexer" /> can be obtained by calling <see cref="PropertyNames" />.</remarks>
    public string DescribeProperty(string name)
    {
        return this.DescribePropertyExtension(name);
    }

    /// <summary>
    /// Returns the zero-based document line index from the specified display line index.
    /// </summary>
    /// <param name="displayLine">The zero-based display line index.</param>
    /// <returns>The zero-based document line index.</returns>
    /// <seealso cref="Line.DisplayIndex" />
    public int DocLineFromVisible(int displayLine)
    {
        return this.DocLineFromVisibleExtension(displayLine, VisibleLineCount);
    }

    /// <summary>
    /// Gets the visible line count of the Scintilla control.
    /// </summary>
    /// <value>The visible line count.</value>
    public int VisibleLineCount
    {
        get
        {
            var wordWrapDisabled = WrapMode == WrapMode.None;
            var allLinesVisible = Lines.AllLinesVisible;

            if (wordWrapDisabled && allLinesVisible)
            {
                return Lines.Count;
            }

            var count = 0;
            foreach (var line in Lines)
            {
                if (allLinesVisible || line.Visible)
                {
                    count += wordWrapDisabled ? 1 : line.WrapCount;
                }
            }

            return count;
        }
    }

    /// <summary>
    /// If there are multiple selections, removes the specified selection.
    /// </summary>
    /// <param name="selection">The zero-based selection index.</param>
    /// <seealso cref="Selections" />
    public void DropSelection(int selection)
    {
        this.DropSelectionExtension(selection);
    }

    /// <summary>
    /// Clears any undo or redo history.
    /// </summary>
    /// <remarks>This will also cause <see cref="SetSavePoint" /> to be called but will not raise the <see cref="SavePointReached" /> event.</remarks>
    public void EmptyUndoBuffer()
    {
        this.EmptyUndoBufferExtension();
    }

    /// <summary>
    /// Marks the end of a set of actions that should be treated as a single undo action.
    /// </summary>
    /// <seealso cref="BeginUndoAction" />
    public void EndUndoAction()
    {
        this.EndUndoActionExtension();
    }

    /// <summary>
    /// Performs the specified <see cref="Scintilla" />command.
    /// </summary>
    /// <param name="sciCommand">The command to perform.</param>
    public void ExecuteCmd(Command sciCommand)
    {
        this.ExecuteCmdExtension(sciCommand);
    }

    /// <summary>
    /// Performs the specified fold action on the entire document.
    /// </summary>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    /// <remarks>When using <see cref="FoldAction.Toggle" /> the first fold header in the document is examined to decide whether to expand or contract.</remarks>
    public void FoldAll(FoldAction action)
    {
        this.FoldAllExtension(action);
    }

    /// <summary>
    /// Changes the appearance of fold text tags.
    /// </summary>
    /// <param name="style">One of the <see cref="FoldDisplayText" /> enumeration values.</param>
    /// <remarks>The text tag to display on a folded line can be set using <see cref="Line.ToggleFoldShowText" />.</remarks>
    /// <seealso cref="Line.ToggleFoldShowText" />.
    public void FoldDisplayTextSetStyle(FoldDisplayText style)
    {
        this.FoldDisplayTextSetStyleExtension(style);
    }

    /// <summary>
    /// Frees all allocated sub-styles.
    /// </summary>
    public void FreeSubStyles()
    {
        this.FreeSubStylesExtension();
    }

    /// <summary>
    /// Returns the character as the specified document position.
    /// </summary>
    /// <param name="position">The zero-based document position of the character to get.</param>
    /// <returns>The character at the specified <paramref name="position" />.</returns>
    public int GetCharAt(int position)
    {
        return this.GetCharAtExtension(position, Lines);
    }

    /// <summary>
    /// Returns the column number of the specified document position, taking the width of tabs into account.
    /// </summary>
    /// <param name="position">The zero-based document position to get the column for.</param>
    /// <returns>The number of columns from the start of the line to the specified document <paramref name="position" />.</returns>
    public int GetColumn(int position)
    {
        return this.GetColumnExtension(position, Lines);
    }

    /// <summary>
    /// Returns the last document position likely to be styled correctly.
    /// </summary>
    /// <returns>The zero-based document position of the last styled character.</returns>
    public int GetEndStyled()
    {
        return this.GetEndStyledExtension(Lines);
    }

    /// <summary>
    /// Gets the Primary style associated with the given Secondary style.
    /// </summary>
    /// <param name="style">The secondary style</param>
    /// <returns>For a secondary style, return the primary style, else return the argument.</returns>
    public int GetPrimaryStyleFromStyle(int style)
    {
        return this.GetPrimaryStyleFromStyleExtension(style);
    }

    /// <summary>
    /// Lookup a property value for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="name">The property name to lookup.</param>
    /// <returns>
    /// A String representing the property value if found; otherwise, String.Empty.
    /// Any embedded property name macros as described in <see cref="SetProperty" /> will not be replaced (expanded).
    /// </returns>
    /// <seealso cref="GetPropertyExpanded" />
    public string GetScintillaProperty(string name)
    {
        return this.GetPropertyExtension(name);
    }

    /// <summary>
    /// Lookup a property value for the current <see cref="Lexer" /> and expand any embedded property macros.
    /// </summary>
    /// <param name="name">The property name to lookup.</param>
    /// <returns>
    /// A String representing the property value if found; otherwise, String.Empty.
    /// Any embedded property name macros as described in <see cref="SetProperty" /> will be replaced (expanded).
    /// </returns>
    /// <seealso cref="GetScintillaProperty" />
    public string GetPropertyExpanded(string name)
    {
        return this.GetPropertyExpandedExtension(name);
    }

    /// <summary>
    /// Lookup a property value for the current <see cref="Lexer" /> and convert it to an integer.
    /// </summary>
    /// <param name="name">The property name to lookup.</param>
    /// <param name="defaultValue">A default value to return if the property name is not found or has no value.</param>
    /// <returns>
    /// An Integer representing the property value if found;
    /// otherwise, <paramref name="defaultValue" /> if not found or the property has no value;
    /// otherwise, 0 if the property is not a number.
    /// </returns>
    public int GetPropertyInt(string name, int defaultValue)
    {
        return this.GetPropertyIntExtension(name, defaultValue);
    }

    /// <summary>
    /// Gets the style of the specified document position.
    /// </summary>
    /// <param name="position">The zero-based document position of the character to get the style for.</param>
    /// <returns>The zero-based <see cref="Style" /> index used at the specified <paramref name="position" />.</returns>
    public int GetStyleAt(int position)
    {
        return this.GetStyleAtExtension(position, Lines);
    }

    /// <summary>
    /// Gets the lexer base style of a sub-style.
    /// </summary>
    /// <param name="subStyle">The integer index of the sub-style</param>
    /// <returns>Returns the base style, else returns the argument.</returns>
    public int GetStyleFromSubStyle(int subStyle)
    {
        return this.GetStyleFromSubStyleExtension(subStyle);
    }

    /// <summary>
    /// Gets the length of the number of sub-styles allocated for a given lexer base style.
    /// </summary>
    /// <param name="styleBase">The lexer style integer</param>
    /// <returns>Returns the length of the sub-styles allocated for a base style.</returns>
    public int GetSubStylesLength(int styleBase)
    {
        return this.GetSubStylesLengthExtension(styleBase);
    }

    /// <summary>
    /// Gets the start index of the sub-styles for a given lexer base style.
    /// </summary>
    /// <param name="styleBase">The lexer style integer</param>
    /// <returns>Returns the start of the sub-styles allocated for a base style.</returns>
    public int GetSubStylesStart(int styleBase)
    {
        return this.GetSubStylesStartExtension(styleBase);
    }

    /// <summary>
    /// Returns the capture group text of the most recent regular expression search.
    /// </summary>
    /// <param name="tagNumber">The capture group (1 through 9) to get the text for.</param>
    /// <returns>A String containing the capture group text if it participated in the match; otherwise, an empty string.</returns>
    /// <seealso cref="SearchInTarget" />
    public string GetTag(int tagNumber)
    {
        return this.GetTagExtension(tagNumber);
    }

    /// <summary>
    /// Gets a range of text from the document.
    /// </summary>
    /// <param name="position">The zero-based starting character position of the range to get.</param>
    /// <param name="length">The number of characters to get.</param>
    /// <returns>A string representing the text range.</returns>
    public string GetTextRange(int position, int length)
    {
        return this.GetTextRangeExtension(position, length, Lines);
    }

    /// <inheritdoc />
    public string GetTextRangeAsHtml(int position, int length)
    {
        throw new NotImplementedException();
    }

    ///<summary>
    /// Gets the word from the position specified.
    /// </summary>
    /// <param name="position">The zero-based document character position to get the word from.</param>
    /// <returns>The word at the specified position.</returns>
    public string GetWordFromPosition(int position)
    {
        return this.GetWordFromPositionExtension(position, Lines);
    }

    /// <summary>
    /// Navigates the caret to the document position specified.
    /// </summary>
    /// <param name="position">The zero-based document character position to navigate to.</param>
    /// <remarks>Any selection is discarded.</remarks>
    public void GotoPosition(int position)
    {
        this.GotoPositionExtension(position, Lines);
    }

    /// <summary>
    /// Hides the range of lines specified.
    /// </summary>
    /// <param name="lineStart">The zero-based index of the line range to start hiding.</param>
    /// <param name="lineEnd">The zero-based index of the line range to end hiding.</param>
    /// <seealso cref="ShowLines" />
    /// <seealso cref="Line.Visible" />
    public void HideLines(int lineStart, int lineEnd)
    {
        this.HideLinesExtension(lineStart, lineEnd, Lines);
    }

    /// <summary>
    /// Returns a bitmap representing the 32 indicators in use at the specified position.
    /// </summary>
    /// <param name="position">The zero-based character position within the document to test.</param>
    /// <returns>A bitmap indicating which of the 32 indicators are in use at the specified <paramref name="position" />.</returns>
    public uint IndicatorAllOnFor(int position)
    {
        return this.IndicatorAllOnForExtension(position, Lines);
    }

    /// <summary>
    /// Removes the <see cref="IndicatorCurrent" /> indicator (and user-defined value) from the specified range of text.
    /// </summary>
    /// <param name="position">The zero-based character position within the document to start clearing.</param>
    /// <param name="length">The number of characters to clear.</param>
    public void IndicatorClearRange(int position, int length)
    {
        this.IndicatorClearRangeExtension(position, length, Lines);
    }

    /// <summary>
    /// Adds the <see cref="IndicatorCurrent" /> indicator and <see cref="IndicatorValue" /> value to the specified range of text.
    /// </summary>
    /// <param name="position">The zero-based character position within the document to start filling.</param>
    /// <param name="length">The number of characters to fill.</param>
    public void IndicatorFillRange(int position, int length)
    {
        this.IndicatorFillRangeExtension(position, length, Lines);
    }


    /// <summary>
    /// Initializes the Scintilla document.
    /// </summary>
    /// <param name="eolMode">The eol mode.</param>
    /// <param name="useTabs">if set to <c>true</c> use tabs instead of spaces.</param>
    /// <param name="tabWidth">Width of the tab.</param>
    /// <param name="indentWidth">Width of the indent.</param>
    public void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0)
    {
        this.InitDocumentExtension(eolMode, useTabs, tabWidth, indentWidth);
    }

    /// <summary>
    /// Inserts text at the specified position.
    /// </summary>
    /// <param name="position">The zero-based character position to insert the text. Specify -1 to use the current caret position.</param>
    /// <param name="text">The text to insert into the document.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="position" /> less than zero and not equal to -1. -or-
    /// <paramref name="position" /> is greater than the document length.
    /// </exception>
    /// <remarks>No scrolling is performed.</remarks>
    public void InsertText(int position, string text)
    {
       this.InsertTextExtension(position, text, Lines);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="start" /> and <paramref name="end" /> positions are
    /// at the beginning and end of a word, respectively.
    /// </summary>
    /// <param name="start">The zero-based document position of the possible word start.</param>
    /// <param name="end">The zero-based document position of the possible word end.</param>
    /// <returns>
    /// true if <paramref name="start" /> and <paramref name="end" /> are at the beginning and end of a word, respectively;
    /// otherwise, false.
    /// </returns>
    /// <remarks>
    /// This method does not check whether there is whitespace in the search range,
    /// only that the <paramref name="start" /> and <paramref name="end" /> are at word boundaries.
    /// </remarks>
    public bool IsRangeWord(int start, int end)
    {
        return this.IsRangeWordExtension(start, end, Lines);
    }

    /// <summary>
    /// Returns the line that contains the document position specified.
    /// </summary>
    /// <param name="position">The zero-based document character position.</param>
    /// <returns>The zero-based document line index containing the character <paramref name="position" />.</returns>
    public int LineFromPosition(int position)
    {
        return this.LineFromPositionExtension(position, Lines);
    }

    /// <summary>
    /// Scrolls the display the number of lines and columns specified.
    /// </summary>
    /// <param name="lines">The number of lines to scroll.</param>
    /// <param name="columns">The number of columns to scroll.</param>
    /// <remarks>
    /// Negative values scroll in the opposite direction.
    /// A column is the width in pixels of a space character in the <see cref="StyleConstants.Default" /> style.
    /// </remarks>
    public void LineScroll(int lines, int columns)
    {
        this.LineScrollExtension(lines, columns);
    }

    /// <summary>
    /// Loads a <see cref="Scintilla" /> compatible lexer from an external DLL.
    /// </summary>
    /// <param name="path">The path to the external lexer DLL.</param>
    public void LoadLexerLibrary(string path)
    {
        this.LoadLexerLibraryExtension(path);
    }

    /// <summary>
    /// Removes the specified marker from all lines.
    /// </summary>
    /// <param name="marker">The zero-based <see cref="Marker" /> index to remove from all lines, or -1 to remove all markers from all lines.</param>
    public void MarkerDeleteAll(int marker)
    {
        this.MarkerDeleteAllExtension(marker, Markers); 
    }

    /// <summary>
    /// Searches the document for the marker handle and deletes the marker if found.
    /// </summary>
    /// <param name="markerHandle">The <see cref="MarkerHandle" /> created by a previous call to <see cref="Line.MarkerAdd" /> of the marker to delete.</param>
    public void MarkerDeleteHandle(MarkerHandle markerHandle)
    {
        this.MarkerDeleteHandleExtension(markerHandle);
    }

    /// <summary>
    /// Enable or disable highlighting of the current folding block.
    /// </summary>
    /// <param name="enabled">true to highlight the current folding block; otherwise, false.</param>
    public void MarkerEnableHighlight(bool enabled)
    {
        this.MarkerEnableHighlightExtension(enabled);
    }

    /// <summary>
    /// Searches the document for the marker handle and returns the line number containing the marker if found.
    /// </summary>
    /// <param name="markerHandle">The <see cref="MarkerHandle" /> created by a previous call to <see cref="Line.MarkerAdd" /> of the marker to search for.</param>
    /// <returns>If found, the zero-based line index containing the marker; otherwise, -1.</returns>
    public int MarkerLineFromHandle(MarkerHandle markerHandle)
    {
        return this.MarkerLineFromHandleExtension(markerHandle);
    }

    /// <summary>
    /// Specifies the long line indicator column number and color when <see cref="EdgeMode" /> is <see cref="Abstractions.Enumerations.EdgeMode.MultiLine" />.
    /// </summary>
    /// <param name="column">The zero-based column number to indicate.</param>
    /// <param name="edgeColor">The color of the vertical long line indicator.</param>
    /// <remarks>A column is defined as the width of a space character in the <see cref="StyleConstants.Default" /> style.</remarks>
    /// <seealso cref="MultiEdgeClearAll" />
    public void MultiEdgeAddLine(int column, Color edgeColor)
    {
        this.MultiEdgeAddLineExtension(column, edgeColor, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Removes all the long line column indicators specified using <seealso cref="MultiEdgeAddLine" />.
    /// </summary>
    /// <seealso cref="MultiEdgeAddLine" />
    public void MultiEdgeClearAll()
    {
        this.MultiEdgeClearAllExtension();
    }

    /// <summary>
    /// Searches for all instances of the main selection within the <see cref="TargetStart" /> and <see cref="TargetEnd" />
    /// range and adds any matches to the selection.
    /// </summary>
    /// <remarks>
    /// The <see cref="SearchFlags" /> property is respected when searching, allowing additional
    /// selections to match on different case sensitivity and word search options.
    /// </remarks>
    /// <seealso cref="MultipleSelectAddNext" />
    public void MultipleSelectAddEach()
    {
        this.MultipleSelectAddEachExtension();
    }

    /// <summary>
    /// Searches for the next instance of the main selection within the <see cref="TargetStart" /> and <see cref="TargetEnd" />
    /// range and adds any match to the selection.
    /// </summary>
    /// <remarks>
    /// The <see cref="SearchFlags" /> property is respected when searching, allowing additional
    /// selections to match on different case sensitivity and word search options.
    /// </remarks>
    /// <seealso cref="MultipleSelectAddNext" />
    public void MultipleSelectAddNext()
    {
        this.MultipleSelectAddNextExtension();
    }

    /// <summary>
    /// Pastes the contents of the clipboard into the current selection.
    /// </summary>
    public void Paste()
    {
        this.PasteExtension();
    }

    /// <summary>
    /// Returns the X display pixel location of the specified document position.
    /// </summary>
    /// <param name="pos">The zero-based document character position.</param>
    /// <returns>The x-coordinate of the specified <paramref name="pos" /> within the client rectangle of the control.</returns>
    public int PointXFromPosition(int pos)
    {
        return this.PointXFromPositionExtension(pos, Lines);
    }

    /// <summary>
    /// Returns the Y display pixel location of the specified document position.
    /// </summary>
    /// <param name="pos">The zero-based document character position.</param>
    /// <returns>The y-coordinate of the specified <paramref name="pos" /> within the client rectangle of the control.</returns>
    public int PointYFromPosition(int pos)
    {
        return this.PointYFromPositionExtension(pos, Lines);
    }

    /// <summary>
    /// Retrieves a list of property names that can be set for the current <see cref="Lexer" />.
    /// </summary>
    /// <returns>A String of property names separated by line breaks.</returns>
    public string PropertyNames()
    {
        return this.PropertyNamesExtension();
    }

    /// <summary>
    /// Retrieves the data type of the specified property name for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="name">A property name supported by the current <see cref="Lexer" />.</param>
    /// <returns>One of the <see cref="Abstractions.Enumerations.PropertyType" /> enumeration values. The default is <see cref="bool" />.</returns>
    /// <remarks>A list of supported property names for the current <see cref="Lexer" /> can be obtained by calling <see cref="PropertyNames" />.</remarks>
    public PropertyType PropertyType(string name)
    {
        return this.PropertyTypeExtension(name);
    }

    /// <summary>
    /// Redoes the effect of an <see cref="Undo" /> operation.
    /// </summary>
    public void Redo()
    {
        this.RedoExtension();
    }

    /// <inheritdoc />
    public unsafe void RegisterRgbaImage(int type, Image? image)
    {
        if (image == null)
        {
            return;
        }

        DirectMessage(SCI_RGBAIMAGESETWIDTH, new IntPtr(image.Pixbuf.Width));
        DirectMessage(SCI_RGBAIMAGESETHEIGHT, new IntPtr(image.Pixbuf.Height));

        var bytes = NativeImageRgbaConverter.PixBufToBytes(image);
        fixed (byte* bp = bytes)
        {
            DirectMessage(SCI_REGISTERRGBAIMAGE, new IntPtr(type), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Decreases the reference count of the specified document by 1.
    /// </summary>
    /// <param name="document">
    /// The document reference count to decrease.
    /// When a document's reference count reaches 0 it is destroyed and any associated memory released.
    /// </param>
    public void ReleaseDocument(Document document)
    {
        this.ReleaseDocumentExtension(document);
    }

    /// <summary>
    /// Replaces the current selection with the specified text.
    /// </summary>
    /// <param name="text">The text that should replace the current selection.</param>
    /// <remarks>
    /// If there is not a current selection, the text will be inserted at the current caret position.
    /// Following the operation the caret is placed at the end of the inserted text and scrolled into view.
    /// </remarks>
    public void ReplaceSelection(string text)
    {
        this.ReplaceSelectionExtension(text);
    }

    /// <summary>
    /// Replaces the target defined by <see cref="TargetStart" /> and <see cref="TargetEnd" /> with the specified <paramref name="text" />.
    /// </summary>
    /// <param name="text">The text that will replace the current target.</param>
    /// <returns>The length of the replaced text.</returns>
    /// <remarks>
    /// The <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties will be updated to the start and end positions of the replaced text.
    /// The recommended way to delete text in the document is to set the target range to be removed and replace the target with an empty string.
    /// </remarks>
    public int ReplaceTarget(string text)
    {
        return this.ReplaceTargetExtension(text);
    }

    /// <summary>
    /// Replaces the target text defined by <see cref="TargetStart" /> and <see cref="TargetEnd" /> with the specified value after first substituting
    /// "\1" through "\9" macros in the <paramref name="text" /> with the most recent regular expression capture groups.
    /// </summary>
    /// <param name="text">The text containing "\n" macros that will be substituted with the most recent regular expression capture groups and then replace the current target.</param>
    /// <returns>The length of the replaced text.</returns>
    /// <remarks>
    /// The "\0" macro will be substituted by the entire matched text from the most recent search.
    /// The <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties will be updated to the start and end positions of the replaced text.
    /// </remarks>
    /// <seealso cref="GetTag" />
    public int ReplaceTargetRe(string text)
    {
        return this.ReplaceTargetReExtension(text, TargetStart, TargetEnd);
    }

    /// <summary>
    /// Makes the next selection the main selection.
    /// </summary>
    public void RotateSelection()
    {
        this.RotateSelectionExtension();
    }

    /// <summary>
    /// Scrolls the current position into view, if it is not already visible.
    /// </summary>
    public void ScrollCaret()
    {
        this.ScrollCaretExtension();
    }

    /// <summary>
    /// Scrolls the specified range into view.
    /// </summary>
    /// <param name="start">The zero-based document start position to scroll to.</param>
    /// <param name="end">
    /// The zero-based document end position to scroll to if doing so does not cause the <paramref name="start" />
    /// position to scroll out of view.
    /// </param>
    /// <remarks>This may be used to make a search match visible.</remarks>
    public void ScrollRange(int start, int end)
    {
       this.ScrollRangeExtension(start, end, TextLength, Lines);
    }

    /// <summary>
    /// Searches for the first occurrence of the specified text in the target defined by <see cref="TargetStart" /> and <see cref="TargetEnd" />.
    /// </summary>
    /// <param name="text">The text to search for. The interpretation of the text (i.e. whether it is a regular expression) is defined by the <see cref="SearchFlags" /> property.</param>
    /// <returns>The zero-based start position of the matched text within the document if successful; otherwise, -1.</returns>
    /// <remarks>
    /// If successful, the <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties will be updated to the start and end positions of the matched text.
    /// Searching can be performed in reverse using a <see cref="TargetStart" /> greater than the <see cref="TargetEnd" />.
    /// </remarks>
    public int SearchInTarget(string text)
    {
        return this.SearchInTargetExtension(text, Lines);
    }

    /// <summary>
    /// Selects all the text in the document.
    /// </summary>
    /// <remarks>The current position is not scrolled into view.</remarks>
    public void SelectAll()
    {
        this.SelectAllExtension();
    }

    /// <summary>
    /// Sets the background color of additional selections.
    /// </summary>
    /// <param name="color">Additional selections background color.</param>
    /// <remarks>Calling <see cref="SetSelectionBackColor" /> will reset the <paramref name="color" /> specified.</remarks>
    public void SetAdditionalSelBack(Color color)
    {
        this.SetAdditionalSelBackExtension(color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Sets the foreground color of additional selections.
    /// </summary>
    /// <param name="color">Additional selections foreground color.</param>
    /// <remarks>Calling <see cref="SetSelectionForeColor" /> will reset the <paramref name="color" /> specified.</remarks>
    public void SetAdditionalSelFore(Color color)
    {
        this.SetAdditionalSelForeExtension(color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Removes any selection and places the caret at the specified position.
    /// </summary>
    /// <param name="pos">The zero-based document position to place the caret at.</param>
    /// <remarks>The caret is not scrolled into view.</remarks>
    public void SetEmptySelection(int pos)
    {
        this.SetEmptySelectionExtension(pos, TextLength, Lines);
    }

    /// <summary>
    /// Sets additional options for displaying folds.
    /// </summary>
    /// <param name="flags">A bitwise combination of the <see cref="FoldFlags" /> enumeration.</param>
    public void SetFoldFlags(FoldFlags flags)
    {
        this.SetFoldFlagsExtension(flags);
    }

    /// <summary>
    /// Sets a global override to the fold margin color.
    /// </summary>
    /// <param name="use">true to override the fold margin color; otherwise, false.</param>
    /// <param name="color">The global fold margin color.</param>
    /// <seealso cref="SetFoldMarginHighlightColor" />
    public void SetFoldMarginColor(bool use, Color color)
    {
        this.SetFoldMarginColorExtension(use, color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Sets a global override to the fold margin highlight color.
    /// </summary>
    /// <param name="use">true to override the fold margin highlight color; otherwise, false.</param>
    /// <param name="color">The global fold margin highlight color.</param>
    /// <seealso cref="SetFoldMarginColor" />
    public void SetFoldMarginHighlightColor(bool use, Color color)
    {
        this.SetFoldMarginHighlightColorExtension(use, color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Similar to <see cref="SetKeywords" /> but for sub-styles.
    /// </summary>
    /// <param name="style">The sub-style integer index</param>
    /// <param name="identifiers">A list of words separated by whitespace (space, tab, '\n', '\r') characters.</param>
    public void SetIdentifiers(int style, string identifiers)
    {
        this.SetIdentifiersExtension(style, identifiers);
    }

    /// <summary>
    /// Updates a keyword set used by the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="set">The zero-based index of the keyword set to update.</param>
    /// <param name="keywords">
    /// A list of keywords pertaining to the current <see cref="Lexer" /> separated by whitespace (space, tab, '\n', '\r') characters.
    /// </param>
    /// <remarks>The keywords specified will be styled according to the current <see cref="Lexer" />.</remarks>
    /// <seealso cref="DescribeKeywordSets" />
    public void SetKeywords(int set, string keywords)
    {
        this.SetKeywordsExtension(set, keywords);
    }

    /// <summary>
    /// Passes the specified property name-value pair to the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="name">The property name to set.</param>
    /// <param name="value">
    /// The property value. Values can refer to other property names using the syntax $(name), where 'name' is another property
    /// name for the current <see cref="Lexer" />. When the property value is retrieved by a call to <see cref="GetPropertyExpanded" />
    /// the embedded property name macro will be replaced (expanded) with that current property value.
    /// </param>
    /// <remarks>Property names are case-sensitive.</remarks>
    public void SetProperty(string name, string value)
    {
        this.SetPropertyExtension(name, value);
    }

    /// <summary>
    /// Marks the document as unmodified.
    /// </summary>
    /// <seealso cref="Modified" />
    public void SetSavePoint()
    {
        this.SetSavePointExtension();
    }

    /// <summary>
    /// Sets the anchor and current position.
    /// </summary>
    /// <param name="anchorPos">The zero-based document position to start the selection.</param>
    /// <param name="currentPos">The zero-based document position to end the selection.</param>
    /// <remarks>
    /// A negative value for <paramref name="currentPos" /> signifies the end of the document.
    /// A negative value for <paramref name="anchorPos" /> signifies no selection (i.e. sets the <paramref name="anchorPos" />
    /// to the same position as the <paramref name="currentPos" />).
    /// The current position is scrolled into view following this operation.
    /// </remarks>
    public void SetSel(int anchorPos, int currentPos)
    {
        this.SetSelExtension(anchorPos, currentPos, TextLength, Lines);
    }

    /// <summary>
    /// Sets a single selection from anchor to caret.
    /// </summary>
    /// <param name="caret">The zero-based document position to end the selection.</param>
    /// <param name="anchor">The zero-based document position to start the selection.</param>
    public void SetSelection(int caret, int anchor)
    {
        this.SetSelectionExtension(caret, anchor, TextLength, Lines);
    }

    /// <summary>
    /// Sets a global override to the selection background color.
    /// </summary>
    /// <param name="use">true to override the selection background color; otherwise, false.</param>
    /// <param name="color">The global selection background color.</param>
    /// <seealso cref="SetSelectionForeColor" />
    public void SetSelectionBackColor(bool use, Color color)
    {
        this.SetSelectionBackColorExtension(use, color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Sets a global override to the selection foreground color.
    /// </summary>
    /// <param name="use">true to override the selection foreground color; otherwise, false.</param>
    /// <param name="color">The global selection foreground color.</param>
    /// <seealso cref="SetSelectionBackColor" />
    public void SetSelectionForeColor(bool use, Color color)
    {
        this.SetSelectionForeColorExtension(use, color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Styles the specified length of characters.
    /// </summary>
    /// <param name="length">The number of characters to style.</param>
    /// <param name="style">The <see cref="Style" /> definition index to assign each character.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="length" /> or <paramref name="style" /> is less than zero. -or-
    /// The sum of a preceding call to <see cref="StartStyling" /> or <see name="SetStyling" /> and <paramref name="length" /> is greater than the document length. -or-
    /// <paramref name="style" /> is greater than or equal to the number of style definitions.
    /// </exception>
    /// <remarks>
    /// The styling position is advanced by <paramref name="length" /> after each call allowing multiple
    /// calls to <see cref="SetStyling" /> for a single call to <see cref="StartStyling" />.
    /// </remarks>
    /// <seealso cref="StartStyling" />
    public void SetStyling(int length, int style)
    {
        this.SetStylingExtension(Lines, length, style, TextLength, ref stylingPosition, ref stylingBytePosition);
    }

    /// <summary>
    /// Sets the <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties in a single call.
    /// </summary>
    /// <param name="start">The zero-based character position within the document to start a search or replace operation.</param>
    /// <param name="end">The zero-based character position within the document to end a search or replace operation.</param>
    /// <seealso cref="TargetStart" />
    /// <seealso cref="TargetEnd" />
    public void SetTargetRange(int start, int end)
    {
        this.SetTargetRangeExtension(start, end, TextLength, Lines);
    }

    /// <summary>
    /// Sets a global override to the whitespace background color.
    /// </summary>
    /// <param name="use">true to override the whitespace background color; otherwise, false.</param>
    /// <param name="color">The global whitespace background color.</param>
    /// <remarks>When not overridden globally, the whitespace background color is determined by the current lexer.</remarks>
    /// <seealso cref="ViewWhitespace" />
    /// <seealso cref="SetWhitespaceForeColor" />
    public void SetWhitespaceBackColor(bool use, Color color)
    {
        this.SetWhitespaceBackColorExtension(use, color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Sets a global override to the whitespace foreground color.
    /// </summary>
    /// <param name="use">true to override the whitespace foreground color; otherwise, false.</param>
    /// <param name="color">The global whitespace foreground color.</param>
    /// <remarks>When not overridden globally, the whitespace foreground color is determined by the current lexer.</remarks>
    /// <seealso cref="ViewWhitespace" />
    /// <seealso cref="SetWhitespaceBackColor" />
    public void SetWhitespaceForeColor(bool use, Color color)
    {
        this.SetWhitespaceForeColorExtension(use, color, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Shows the range of lines specified.
    /// </summary>
    /// <param name="lineStart">The zero-based index of the line range to start showing.</param>
    /// <param name="lineEnd">The zero-based index of the line range to end showing.</param>
    /// <seealso cref="HideLines" />
    /// <seealso cref="Line.Visible" />
    public void ShowLines(int lineStart, int lineEnd)
    {
        this.ShowLinesExtension(lineStart, lineEnd, Lines);
    }

    /// <summary>
    /// Prepares for styling by setting the styling <paramref name="position" /> to start at.
    /// </summary>
    /// <param name="position">The zero-based character position in the document to start styling.</param>
    /// <remarks>
    /// After preparing the document for styling, use successive calls to <see cref="SetStyling" />
    /// to style the document.
    /// </remarks>
    /// <seealso cref="SetStyling" />
    public void StartStyling(int position)
    {
        this.StartStylingExtension(position, out stylingPosition, out stylingBytePosition, TextLength, Lines);
    }

    /// <summary>
    /// Resets all style properties to those currently configured for the <see cref="StyleConstants.Default" /> style.
    /// </summary>
    /// <seealso cref="StyleResetDefault" />
    public void StyleClearAll()
    {
        this.StyleClearAllExtension();
    }

    /// <summary>
    /// Resets the <see cref="StyleConstants.Default" /> style to its initial state.
    /// </summary>
    /// <seealso cref="StyleClearAll" />
    public void StyleResetDefault()
    {
        this.StyleResetDefaultExtension();
    }

    /// <summary>
    /// Moves the caret to the opposite end of the main selection.
    /// </summary>
    public void SwapMainAnchorCaret()
    {
        this.SwapMainAnchorCaretExtension();
    }

    /// <summary>
    /// Sets the <see cref="TargetStart" /> and <see cref="TargetEnd" /> to the start and end positions of the selection.
    /// </summary>
    /// <seealso cref="TargetWholeDocument" />
    public void TargetFromSelection()
    {
        this.TargetFromSelectionExtension();
    }

    /// <summary>
    /// Sets the <see cref="TargetStart" /> and <see cref="TargetEnd" /> to the start and end positions of the document.
    /// </summary>
    /// <seealso cref="TargetFromSelection" />
    public void TargetWholeDocument()
    {
        this.TargetWholeDocumentExtension();
    }

    /// <summary>
    /// Measures the width in pixels of the specified string when rendered in the specified style.
    /// </summary>
    /// <param name="style">The index of the <see cref="Style" /> to use when rendering the text to measure.</param>
    /// <param name="text">The text to measure.</param>
    /// <returns>The width in pixels.</returns>
    public int TextWidth(int style, string text)
    {
        return this.TextWidthExtension(style, text, Styles);
    }

    /// <summary>
    /// Undoes the previous action.
    /// </summary>
    public void Undo()
    {
        this.UndoExtension();
    }

    /// <summary>
    /// Determines whether to show the right-click context menu.
    /// </summary>
    /// <param name="enablePopup">true to enable the popup window; otherwise, false.</param>
    /// <seealso cref="UsePopup(PopupMode)" />
    public void UsePopup(bool enablePopup)
    {
        this.UsePopupExtension(enablePopup);
    }

    /// <summary>
    /// Determines the conditions for displaying the standard right-click context menu.
    /// </summary>
    /// <param name="popupMode">One of the <seealso cref="PopupMode" /> enumeration values.</param>
    public void UsePopup(PopupMode popupMode)
    {
        this.UsePopupExtension(popupMode);
    }

    /// <summary>
    /// Returns the position where a word ends, searching forward from the position specified.
    /// </summary>
    /// <param name="position">The zero-based document position to start searching from.</param>
    /// <param name="onlyWordCharacters">
    /// true to stop searching at the first non-word character regardless of whether the search started at a word or non-word character.
    /// false to use the first character in the search as a word or non-word indicator and then search for that word or non-word boundary.
    /// </param>
    /// <returns>The zero-based document position of the word boundary.</returns>
    /// <seealso cref="WordStartPosition" />
    public int WordEndPosition(int position, bool onlyWordCharacters)
    {
        return this.WordEndPositionExtension(position, onlyWordCharacters, Lines);
    }

    /// <summary>
    /// Returns the position where a word starts, searching backward from the position specified.
    /// </summary>
    /// <param name="position">The zero-based document position to start searching from.</param>
    /// <param name="onlyWordCharacters">
    /// true to stop searching at the first non-word character regardless of whether the search started at a word or non-word character.
    /// false to use the first character in the search as a word or non-word indicator and then search for that word or non-word boundary.
    /// </param>
    /// <returns>The zero-based document position of the word boundary.</returns>
    /// <seealso cref="WordEndPosition" />
    public int WordStartPosition(int position, bool onlyWordCharacters)
    {
        return this.WordStartPositionExtension(position, onlyWordCharacters, Lines);
    }

    /// <summary>
    /// Increases the zoom factor by 1 until it reaches 20 points.
    /// </summary>
    /// <seealso cref="Zoom" />
    public void ZoomIn()
    {
        this.ZoomInExtension();
    }

    /// <summary>
    /// Decreases the zoom factor by 1 until it reaches -10 points.
    /// </summary>
    /// <seealso cref="Zoom" />
    public void ZoomOut()
    {
        this.ZoomOutExtension();
    }

    /// <summary>
    /// Sets the representation for a specified character string.
    /// </summary>
    /// <param name="encodedString">The encoded string. I.e. the Ohm character: Ω = \u2126.</param>
    /// <param name="representationString">The representation string for the <paramref name="encodedString"/>. I.e. "OHM".</param>
    /// <remarks>The <see cref="ViewWhitespace"/> must be set to <see cref="WhitespaceMode.VisibleAlways"/> for this to work.</remarks>
    public void SetRepresentation(string encodedString, string representationString)
    {
        this.SetRepresentationExtension(encodedString, representationString);
    }

    /// <summary>
    /// Sets the representation for a specified character string.
    /// </summary>
    /// <param name="encodedString">The encoded string. I.e. the Ohm character: Ω = \u2126.</param>
    /// <returns>The representation string for the <paramref name="encodedString"/>. I.e. "OHM".</returns>
    public string GetRepresentation(string encodedString)
    {
        return this.GetRepresentationExtension(encodedString);
    }

    /// <summary>
    /// Clears the representation from a specified character string.
    /// </summary>
    /// <param name="encodedString">The encoded string. I.e. the Ohm character: Ω = \u2126.</param>
    public void ClearRepresentation(string encodedString)
    {
        this.ClearRepresentationExtension(encodedString);
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
    public event EventHandler<ModificationEventArgs>? Delete;

    /// <inheritdoc />
    public event EventHandler<DoubleClickEventArgs>? DoubleClick;

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellEnd;

    /// <inheritdoc />
    public event EventHandler<CallTipClickEventArgs>? CallTipClick;

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellStart;

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotClick;

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotDoubleClick;

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotReleaseClick;

    /// <inheritdoc />
    public event EventHandler<IndicatorClickEventArgs>? IndicatorClick;

    /// <inheritdoc />
    public event EventHandler<IndicatorReleaseEventArgs>? IndicatorRelease;

    /// <inheritdoc />
    public event EventHandler<ModificationEventArgs>? Insert;

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

    /// <inheritdoc cref="ScintillaApiStructs.SCNotification" />
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

    #region CollectionProperties
    /// <inheritdoc cref="IScintillaCollectionProperties{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Markers" />
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
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the bi-directionality of the Scintilla control.
    /// </summary>
    /// <value>The bi-directionality of the Scintilla control.</value>
    public BiDirectionalDisplayType BiDirectionality
    {
        get => this.BiDirectionalityGet();

        set => this.BiDirectionalitySet(value);
    }

    /// <summary>
    /// Gets or sets the caret foreground color for additional selections.
    /// </summary>
    /// <returns>The caret foreground color in additional selections. The default is (127, 127, 127).</returns>
    public Color AdditionalCaretForeColor
    {
        get => this.AdditionalCaretForeColorGet(ColorTranslator.ToColor);

        set => this.AdditionalCaretForeColorSet(value, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Gets or sets whether the carets in additional selections will blink.
    /// </summary>
    /// <returns>true if additional selection carets should blink; otherwise, false. The default is true.</returns>
    public bool AdditionalCaretsBlink
    {
        get => this.AdditionalCaretsBlinkGet();

        set => this.AdditionalCaretsBlinkSet(value);
    }

    /// <summary>
    /// Gets or sets whether the carets in additional selections are visible.
    /// </summary>
    /// <returns>true if additional selection carets are visible; otherwise, false. The default is true.</returns>
    public bool AdditionalCaretsVisible
    {
        get => this.AdditionalCaretsVisibleGet();

        set => this.AdditionalCaretsVisibleSet(value);
    }

    /// <summary>
    /// Gets or sets the alpha transparency of additional multiple selections.
    /// </summary>
    /// <returns>
    /// The alpha transparency ranging from 0 (completely transparent) to 255 (completely opaque).
    /// The value 256 will disable alpha transparency. The default is 256.
    /// </returns>
    public int AdditionalSelAlpha
    {
        get => this.AdditionalSelAlphaGet();

        set => this.AdditionalSelAlphaSet(value);
    }

    /// <summary>
    /// Gets or sets whether additional typing affects multiple selections.
    /// </summary>
    /// <returns>true if typing will affect multiple selections instead of just the main selection; otherwise, false. The default is false.</returns>
    public bool AdditionalSelectionTyping
    {
        get => this.AdditionalSelectionTypingGet();

        set => this.AdditionalSelectionTypingSet(value);
    }

    /// <summary>
    /// Gets or sets the current anchor position.
    /// </summary>
    /// <returns>The zero-based character position of the anchor.</returns>
    /// <remarks>
    /// Setting the current anchor position will create a selection between it and the <see cref="CurrentPosition" />.
    /// The caret is not scrolled into view.
    /// </remarks>
    /// <seealso cref="ScrollCaret" />
    public int AnchorPosition
    {
        get => this.AnchorPositionGet(Lines);

        set => this.AnchorPositionSet(value, Lines);
    }

    /// <summary>
    /// Gets or sets the display of annotations.
    /// </summary>
    /// <returns>One of the <see cref="Annotation" /> enumeration values. The default is <see cref="Annotation.Hidden" />.</returns>
    public Annotation AnnotationVisible
    {
        get => this.AnnotationVisibleGet();
        
        set => this.AnnotationVisibleSet(value);
    }

    /// <summary>
    /// Gets a value indicating whether there is an auto-completion list displayed.
    /// </summary>
    /// <returns>true if there is an active auto-completion list; otherwise, false.</returns>
    public bool AutoCActive => this.AutoCActiveGet();

    /// <summary>
    /// Gets or sets whether to automatically cancel auto-completion when there are no viable matches.
    /// </summary>
    /// <returns>
    /// true to automatically cancel auto-completion when there is no possible match; otherwise, false.
    /// The default is true.
    /// </returns>
    public bool AutoCAutoHide
    {
        get => this.AutoCAutoHideGet();
        
        set => this.AutoCAutoHideSet(value);
    }

    /// <summary>
    /// Gets or sets whether to cancel an auto-completion if the caret moves from its initial location,
    /// or is allowed to move to the word start.
    /// </summary>
    /// <returns>
    /// true to cancel auto-completion when the caret moves.
    /// false to allow the caret to move to the beginning of the word without cancelling auto-completion.
    /// </returns>
    public bool AutoCCancelAtStart
    {
        get => this.AutoCCancelAtStartGet();

        set => this.AutoCCancelAtStartSet(value);
    }

    /// <summary>
    /// Gets the index of the current auto-completion list selection.
    /// </summary>
    /// <returns>The zero-based index of the current auto-completion selection.</returns>
    public int AutoCCurrent => this.AutoCCurrentGet();

    /// <summary>
    /// Gets or sets whether to automatically select an item when it is the only one in an auto-completion list.
    /// </summary>
    /// <returns>
    /// true to automatically choose the only auto-completion item and not display the list; otherwise, false.
    /// The default is false.
    /// </returns>
    public bool AutoCChooseSingle
    {
        get => this.AutoCChooseSingleGet();

        set => this.AutoCChooseSingleSet(value);
    }

    /// <summary>
    /// Gets or sets whether to delete any word characters following the caret after an auto-completion.
    /// </summary>
    /// <returns>
    /// true to delete any word characters following the caret after auto-completion; otherwise, false.
    /// The default is false.</returns>
    public bool AutoCDropRestOfWord
    {
        get => this.AutoCDropRestOfWordGet();

        set => this.AutoCDropRestOfWordSet(value);
    }

    /// <summary>
    /// Gets or sets whether matching characters to an auto-completion list is case-insensitive.
    /// </summary>
    /// <returns>true to use case-insensitive matching; otherwise, false. The default is false.</returns>
    public bool AutoCIgnoreCase
    {
        get => this.AutoCIgnoreCaseGet();

        set => this.AutoCIgnoreCaseSet(value);
    }

    /// <summary>
    /// Gets or sets the maximum height of the auto-completion list measured in rows.
    /// </summary>
    /// <returns>The max number of rows to display in an auto-completion window. The default is 5.</returns>
    /// <remarks>If there are more items in the list than max rows, a vertical scrollbar is shown.</remarks>
    public int AutoCMaxHeight
    {
        get => this.AutoCMaxHeightGet();

        set => this.AutoCMaxHeightSet(value);
    }

    /// <summary>
    /// Gets or sets the width in characters of the auto-completion list.
    /// </summary>
    /// <returns>
    /// The width of the auto-completion list expressed in characters, or 0 to automatically set the width
    /// to the longest item. The default is 0.
    /// </returns>
    /// <remarks>Any items that cannot be fully displayed will be indicated with ellipsis.</remarks>
    public int AutoCMaxWidth
    {
        get => this.AutoCMaxWidthGet();
        
        set => this.AutoCMaxWidthSet(value);
    }

    /// <summary>
    /// Gets or sets the auto-completion list sort order to expect when calling <see cref="AutoCShow" />.
    /// </summary>
    /// <returns>One of the <see cref="Order" /> enumeration values. The default is <see cref="Order.Presorted" />.</returns>
    public Order AutoCOrder
    {
        get => this.AutoCOrderGet();

        set => this.AutoCOrderSet(value);
    }

    /// <summary>
    /// Gets the document position at the time <see cref="AutoCShow" /> was called.
    /// </summary>
    /// <returns>The zero-based document position at the time <see cref="AutoCShow" /> was called.</returns>
    /// <seealso cref="AutoCShow" />
    public int AutoCPosStart => this.AutoCPosStartGet(Lines);

    /// <summary>
    /// Gets or sets the delimiter character used to separate words in an auto-completion list.
    /// </summary>
    /// <returns>The separator character used when calling <see cref="AutoCShow" />. The default is the space character.</returns>
    /// <remarks>The <paramref name="value" /> specified should be limited to printable ASCII characters.</remarks>
    public char AutoCSeparator
    {
        get => this.AutoCSeparatorGet();

        set => this.AutoCSeparatorSet(value);
    }

    /// <summary>
    /// Gets or sets the delimiter character used to separate words and image type identifiers in an auto-completion list.
    /// </summary>
    /// <returns>The separator character used to reference an image registered with <see cref="RegisterRgbaImage" />. The default is '?'.</returns>
    /// <remarks>The <paramref name="value" /> specified should be limited to printable ASCII characters.</remarks>
    public char AutoCTypeSeparator
    {
        get => this.AutoCTypeSeparatorGet();

        set => this.AutoCTypeSeparatorSet(value);
    }

    /// <summary>
    /// Gets or sets the automatic folding flags.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="Abstractions.Enumerations.AutomaticFold" /> enumeration.
    /// The default is <see cref="Abstractions.Enumerations.AutomaticFold.None" />.
    /// </returns>
    public AutomaticFold AutomaticFold
    {
        get => this.AutomaticFoldGet();
        
        set => this.AutomaticFoldSet(value);
    }

    /// <summary>
    /// Gets or sets whether backspace deletes a character, or un-indents.
    /// </summary>
    /// <returns>Whether backspace deletes a character, (false) or un-indents (true).</returns>
    public bool BackspaceUnIndents
    {
        get => this.BackspaceUnIndentsGet();

        set => this.BackspaceUnIndentsSet(value);
    }

    /// <summary>
    /// Gets or sets whether drawing is double-buffered.
    /// </summary>
    /// <returns>
    /// true to draw each line into an offscreen bitmap first before copying it to the screen; otherwise, false.
    /// The default is true.
    /// </returns>
    /// <remarks>Disabling buffer can improve performance but will cause flickering.</remarks>
    public bool BufferedDraw
    {
        get => this.BufferedDrawGet();

        set => this.BufferedDrawSet(value);
    }

    /// <summary>
    /// Gets a value indicating whether there is a call tip window displayed.
    /// </summary>
    /// <returns>true if there is an active call tip window; otherwise, false.</returns>
    public bool CallTipActive => this.CallTipActiveGet();

    /// <summary>
    /// Gets a value indicating whether there is text on the clipboard that can be pasted into the document.
    /// </summary>
    /// <returns>true when there is text on the clipboard to paste; otherwise, false.</returns>
    /// <remarks>The document cannot be <see cref="ReadOnly" />  and the selection cannot contain protected text.</remarks>
    public bool CanPaste => this.CanPasteGet();

    /// <summary>
    /// Gets a value indicating whether there is an undo action to redo.
    /// </summary>
    /// <returns>true when there is something to redo; otherwise, false.</returns>
    public bool CanRedo => this.CanRedoGet();

    /// <summary>
    /// Gets a value indicating whether there is an action to undo.
    /// </summary>
    /// <returns>true when there is something to undo; otherwise, false.</returns>
    public bool CanUndo => this.CanUndoGet();

    /// <summary>
    /// Gets or sets the caret foreground color.
    /// </summary>
    /// <returns>The caret foreground color. The default is black.</returns>
    public Color CaretForeColor
    {
        get => this.CaretForeColorGet(ColorTranslator.ToColor);

        set => this.CaretForeColorSet(value, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Gets or sets the caret line background color.
    /// </summary>
    /// <returns>The caret line background color. The default is yellow.</returns>
    public Color CaretLineBackColor
    {
        get => this.CaretLineBackColorGet(ColorTranslator.ToColor);

        set => this.CaretLineBackColorSet(value, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Gets or sets the alpha transparency of the <see cref="CaretLineBackColor" />.
    /// </summary>
    /// <returns>
    /// The alpha transparency ranging from 0 (completely transparent) to 255 (completely opaque).
    /// The value 256 will disable alpha transparency. The default is 256.
    /// </returns>
    public int CaretLineBackColorAlpha
    {
        get => this.CaretLineBackColorAlphaGet();

        set => this.CaretLineBackColorAlphaSet(value);
    }

    /// <summary>
    /// Gets or sets the width of the caret line frame.
    /// </summary>
    /// <returns><see cref="CaretLineVisible" /> must be set to true. A value of 0 disables the frame. The default is 0.</returns>
    public int CaretLineFrame
    {
        get => this.CaretLineFrameGet();

        set => this.CaretLineFrameSet(value);
    }

    /// <summary>
    /// Gets or sets whether the caret line is visible (highlighted).
    /// </summary>
    /// <returns>true if the caret line is visible; otherwise, false. The default is false.</returns>
    public bool CaretLineVisible
    {
        get => this.CaretLineVisibleGet();

        set => this.CaretLineVisibleSet(value);
    }

    /// <summary>
    /// Gets or sets whether the caret line is always visible even when the window is not in focus.
    /// </summary>
    /// <returns>true if the caret line is always visible; otherwise, false. The default is false.</returns>
    public bool CaretLineVisibleAlways
    {
        get => this.CaretLineVisibleAlwaysGet();

        set => this.CaretLineVisibleAlwaysSet(value);
    }

    /// <summary>
    /// Gets or sets the layer where the line caret will be painted. Default value is <see cref="Layer.Base"/>
    /// </summary>
    public Layer CaretLineLayer
    {
        get => this.CaretLineLayerGet();

        set => this.CaretLineLayerSet(value);
    }

    /// <summary>
    /// Gets or sets the caret blink rate in milliseconds.
    /// </summary>
    /// <returns>The caret blink rate measured in milliseconds. The default is 530.</returns>
    /// <remarks>A value of 0 will stop the caret blinking.</remarks>
    public int CaretPeriod
    {
        get => this.CaretPeriodGet();

        set => this.CaretPeriodSet(value);
    }

    /// <summary>
    /// Gets or sets the caret display style.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.CaretStyle" /> enumeration values.
    /// The default is <see cref="Line" />.
    /// </returns>
    public CaretStyle CaretStyle
    {
        get => this.CaretStyleGet();

        set => this.CaretStyleSet(value);
    }

    /// <summary>
    /// Gets or sets the width in pixels of the caret.
    /// </summary>
    /// <returns>The width of the caret in pixels. The default is 1 pixel.</returns>
    /// <remarks>
    /// The caret width can only be set to a value of 0, 1, 2 or 3 pixels and is only effective
    /// when the <see cref="CaretStyle" /> property is set to <see cref="Line" />.
    /// </remarks>
    public int CaretWidth
    {
        get => this.CaretWidthGet();

        set => this.CaretWidthSet(value);
    }

    /// <summary>
    /// Gets the current line index.
    /// </summary>
    /// <returns>The zero-based line index containing the <see cref="CurrentPosition" />.</returns>
    public int CurrentLine => this.CurrentLineGet();

    /// <summary>
    /// Gets or sets the current caret position.
    /// </summary>
    /// <returns>The zero-based character position of the caret.</returns>
    /// <remarks>
    /// Setting the current caret position will create a selection between it and the current <see cref="AnchorPosition" />.
    /// The caret is not scrolled into view.
    /// </remarks>
    /// <seealso cref="ScrollCaret" />
    public int CurrentPosition
    {
        get => this.CurrentPositionGet(Lines);

        set => this.CurrentPositionSet(value, Lines);
    }

    /// <summary>
    /// Gets a value indicating the start index of the secondary styles.
    /// </summary>
    /// <returns>Returns the distance between a primary style and its corresponding secondary style.</returns>
    public int DistanceToSecondaryStyles => this.DistanceToSecondaryStylesGet();

    /// <summary>
    /// Gets or sets the current document used by the control.
    /// </summary>
    /// <returns>The current <see cref="Document" />.</returns>
    /// <remarks>
    /// Setting this property is equivalent to calling <see cref="ReleaseDocument" /> on the current document, and
    /// calling <see cref="CreateDocument" /> if the new <paramref name="value" /> is <see cref="Abstractions.Structs.Document.Empty" /> or
    /// <see cref="AddRefDocument" /> if the new <paramref name="value" /> is not <see cref="Abstractions.Structs.Document.Empty" />.
    /// </remarks>
    public Document Document
    {
        get => this.DocumentGet();

        set => this.DocumentSet(value, Lines, EolMode, UseTabs, TabWidth, IndentWidth);
    }

    /// <summary>
    /// Gets or sets the background color to use when indicating long lines.
    /// </summary>
    /// <returns>The background color to use when indicating long lines.</returns>
    public Color EdgeColor
    {
        get => this.EdgeColorGet(ColorTranslator.ToColor);
        
        set => this.EdgeColorSet(value, ColorTranslator.ToInt);
    }

    /// <summary>
    /// Gets or sets the column number at which to begin indicating long lines.
    /// </summary>
    /// <returns>The number of columns in a long line. The default is 0.</returns>
    /// <remarks>
    /// When using <see cref="Line"/>, a column is defined as the width of a space character in the <see cref="StyleConstants.Default" /> style.
    /// </remarks>
    public int EdgeColumn
    {
        get => this.EdgeColumnGet();

        set => this.EdgeColumnSet(value);
    }

    /// <summary>
    /// Gets or sets the mode for indicating long lines.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.EdgeMode" /> enumeration values.
    /// The default is <see cref="Abstractions.Enumerations.EdgeMode.None" />.
    /// </returns>
    public EdgeMode EdgeMode
    {
        get => this.EdgeModeGet();

        set => this.EdgeModeSet(value);
    }

    /// <summary>
    /// Gets the encoding of the <see cref="T:Scintilla.NET.Abstractions.IScintillaApi" /> control interface.
    /// </summary>
    /// <value>The encoding of the control.</value>
    public Encoding Encoding => this.EncodingGet();

    /// <summary>
    /// Gets or sets whether vertical scrolling ends at the last line or can scroll past.
    /// </summary>
    /// <returns>true if the maximum vertical scroll position ends at the last line; otherwise, false. The default is true.</returns>
    public bool EndAtLastLine
    {
        get => this.EndAtLastLineGet();

        set => this.EndAtLastLineSet(value);
    }

    /// <summary>
    /// Gets or sets the end-of-line mode, or rather, the characters added into
    /// the document when the user presses the Enter key.
    /// </summary>
    /// <returns>One of the <see cref="Eol" /> enumeration values. The default is <see cref="Eol.CrLf" />.</returns>
    public Eol EolMode
    {
        get => this.EolModeGet();
        
        set => this.EolModeSet(value);
    }

    /// <summary>
    /// Gets or sets the amount of whitespace added to the ascent (top) of each line.
    /// </summary>
    /// <returns>The extra line ascent. The default is zero.</returns>
    public int ExtraAscent
    {
        get => this.ExtraAscentGet();

        set => this.ExtraAscentSet(value);
    }

    /// <summary>
    /// Gets or sets the amount of whitespace added to the descent (bottom) of each line.
    /// </summary>
    /// <returns>The extra line descent. The default is zero.</returns>
    public int ExtraDescent
    {
        get => this.ExtraDescentGet();

        set => this.ExtraDescentSet(value);
    }

    /// <summary>
    /// Gets or sets the first visible line on screen.
    /// </summary>
    /// <returns>The zero-based index of the first visible screen line.</returns>
    /// <remarks>The value is a visible line, not a document line.</remarks>
    public int FirstVisibleLine
    {
        get => this.FirstVisibleLineGet();

        set => this.FirstVisibleLineSet(value);
    }

    /// <summary>
    /// Gets or sets font quality (anti-aliasing method) used to render fonts.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.FontQuality" /> enumeration values.
    /// The default is <see cref="Abstractions.Enumerations.FontQuality.Default" />.
    /// </returns>
    public FontQuality FontQuality
    {
        get => this.FontQualityGet();

        set => this.FontQualitySet(value);
    }

    /// <summary>
    /// Gets or sets the column number of the indentation guide to highlight.
    /// </summary>
    /// <returns>The column number of the indentation guide to highlight or 0 if disabled.</returns>
    /// <remarks>Guides are highlighted in the <see cref="StyleConstants.BraceLight" /> style. Column numbers can be determined by calling <see cref="GetColumn" />.</remarks>
    public int HighlightGuide
    {
        get => this.HighlightGuideGet();

        set => this.HighlightGuideSet(value);
    }

    /// <summary>
    /// Gets or sets whether to display the horizontal scroll bar.
    /// </summary>
    /// <returns>true to display the horizontal scroll bar when needed; otherwise, false. The default is true.</returns>
    public bool HScrollBar
    {
        get => this.HScrollBarGet();

        set => this.HScrollBarSet(value);
    }

    /// <summary>
    /// Gets or sets the strategy used to perform styling using application idle time.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.IdleStyling" /> enumeration values.
    /// The default is <see cref="Abstractions.Enumerations.IdleStyling.None" />.
    /// </returns>
    public IdleStyling IdleStyling
    {
        get => this.IdleStylingGet();

        set => this.IdleStylingSet(value);
    }

    /// <summary>
    /// Gets or sets the size of indentation in terms of space characters.
    /// </summary>
    /// <returns>The indentation size measured in characters. The default is 0.</returns>
    /// <remarks> A value of 0 will make the indent width the same as the tab width.</remarks>
    public int IndentWidth
    {
        get => this.IndentWidthGet();

        set => this.IndentWidthSet(value);
    }

    /// <summary>
    /// Gets or sets whether to display indentation guides.
    /// </summary>
    /// <returns>One of the <see cref="IndentView" /> enumeration values. The default is <see cref="IndentView.None" />.</returns>
    /// <remarks>The <see cref="StyleConstants.IndentGuide" /> style can be used to specify the foreground and background color of indentation guides.</remarks>
    public IndentView IndentationGuides
    {
        get => this.IndentationGuidesGet();
        
        set => this.IndentationGuidesSet(value);
    }

    /// <summary>
    /// Gets or sets the indicator used in a subsequent call to <see cref="IndicatorFillRange" /> or <see cref="IndicatorClearRange" />.
    /// </summary>
    /// <returns>The zero-based indicator index to apply when calling <see cref="IndicatorFillRange" /> or remove when calling <see cref="IndicatorClearRange" />.</returns>
    public int IndicatorCurrent
    {
        get => this.IndicatorCurrentGet();

        set => this.IndicatorCurrentSet(value, Lines);
    }

    /// <summary>
    /// Gets or sets the user-defined value used in a subsequent call to <see cref="IndicatorFillRange" />.
    /// </summary>
    /// <returns>The indicator value to apply when calling <see cref="IndicatorFillRange" />.</returns>
    public int IndicatorValue
    {
        get => this.IndicatorValueGet();

        set => this.IndicatorValueSet(value);
    }

    /// <summary>
    /// This is used by clients that have complex focus requirements such as having their own window
    /// that gets the real focus but with the need to indicate that Scintilla has the logical focus.
    /// </summary>
    public bool InternalFocusFlag
    {
        get => this.InternalFocusFlagGet();
        
        set => this.InternalFocusFlagSet(value);
    }

    private string? lexerName;

    /// <summary>
    /// Gets or sets the name of the lexer.
    /// </summary>
    /// <value>The name of the lexer.</value>
    /// <exception cref="InvalidOperationException">Lexer with the name of 'Value' was not found.</exception>
    public string? LexerName
    {
        get => this.LexerNameGet(lexerName);

        set => this.LexerNameSet(LexillaSingleton, value, ref lexerName);
    }

    /// <inheritdoc />
    public int SelectionStart { get; set; }

    /// <summary>
    /// Gets or sets the current lexer.
    /// </summary>
    /// <returns>One of the <see cref="Lexer" /> enumeration values. The default is <see cref="Container" />.</returns>
    /// <exception cref="InvalidOperationException">
    /// No lexer name was found with the specified value.
    /// </exception>
    /// <remarks>This property will get more obsolete as time passes as the Scintilla v.5+ now uses strings to define lexers. The Lexer enumeration is not maintained.</remarks>
    public Lexer Lexer
    {
        get => this.LexerGet(lexerName);

        set => this.LexerSet(LexillaSingleton, value, ref lexerName);
    }

    /// <summary>
    /// Gets or sets the current lexer by name.
    /// </summary>
    /// <returns>A String representing the current lexer.</returns>
    /// <remarks>Lexer names are case-sensitive.</remarks>
    public string LexerLanguage
    {
        get => this.LexerLanguageGet();

        set => this.LexerLanguageSet(value);
    }

    /// <summary>
    /// Gets the combined result of the <see cref="LineEndTypesSupported" /> and <see cref="LineEndTypesAllowed" />
    /// properties to report the line end types actively being interpreted.
    /// </summary>
    /// <returns>A bitwise combination of the <see cref="LineEndType" /> enumeration.</returns>
    public LineEndType LineEndTypesActive => this.LineEndTypesActiveGet();

    /// <summary>
    /// Gets or sets the line ending types interpreted by the <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="LineEndType" /> enumeration.
    /// The default is <see cref="LineEndType.Default" />.
    /// </returns>
    /// <remarks>The line ending types allowed must also be supported by the current lexer to be effective.</remarks>
    public LineEndType LineEndTypesAllowed
    {
        get => this.LineEndTypesAllowedGet();

        set => this.LineEndTypesAllowedSet(value);
    }

    /// <summary>
    /// Gets the different types of line ends supported by the current lexer.
    /// </summary>
    /// <returns>A bitwise combination of the <see cref="LineEndType" /> enumeration.</returns>
    public LineEndType LineEndTypesSupported => this.LineEndTypesSupportedGet();

    /// <summary>
    /// Gets the number of lines that can be shown on screen given a constant
    /// line height and the space available.
    /// </summary>
    /// <returns>
    /// The number of screen lines which could be displayed (including any partial lines).
    /// </returns>
    public int LinesOnScreen => this.LinesOnScreenGet();

    /// <summary>
    /// Gets or sets the main selection when their are multiple selections.
    /// </summary>
    /// <returns>The zero-based main selection index.</returns>
    public int MainSelection
    {
        get => this.MainSelectionGet();
        
        set => this.MainSelectionSet(value);
    }

    /// <summary>
    /// Gets a value indicating whether the document has been modified (is dirty)
    /// since the last call to <see cref="SetSavePoint" />.
    /// </summary>
    /// <returns>true if the document has been modified; otherwise, false.</returns>
    public bool Modified => this.ModifiedGet();

    /// <summary>
    /// Gets or sets the time in milliseconds the mouse must linger to generate a <see cref="DwellStart" /> event.
    /// </summary>
    /// <returns>
    /// The time in milliseconds the mouse must linger to generate a <see cref="DwellStart" /> event
    /// or <see cref="ApiConstants.TimeForever" /> if dwell events are disabled.
    /// </returns>
    public int MouseDwellTime
    {
        get => this.MouseDwellTimeGet();
        
        set => this.MouseDwellTimeSet(value);
    }

    /// <summary>
    /// Gets or sets the ability to switch to rectangular selection mode while making a selection with the mouse.
    /// </summary>
    /// <returns>
    /// true if the current mouse selection can be switched to a rectangular selection by pressing the ALT key; otherwise, false.
    /// The default is false.
    /// </returns>
    public bool MouseSelectionRectangularSwitch
    {
        get => this.MouseSelectionRectangularSwitchGet();

        set => this.MouseSelectionRectangularSwitchSet(value);
    }

    /// <summary>
    /// Gets or sets whether multiple selection is enabled.
    /// </summary>
    /// <returns>
    /// true if multiple selections can be made by holding the CTRL key and dragging the mouse; otherwise, false.
    /// The default is false.
    /// </returns>
    public bool MultipleSelection
    {
        get => this.MultipleSelectionGet();

        set => this.MultipleSelectionSet(value);
    }

    /// <summary>
    /// Gets or sets the behavior when pasting text into multiple selections.
    /// </summary>
    /// <returns>One of the <see cref="MultiPaste" /> enumeration values. The default is <see cref="Abstractions.Enumerations.MultiPaste.Once" />.</returns>
    public MultiPaste MultiPaste
    {
        get => this.MultiPasteGet();
        
        set => this.MultiPasteSet(value);
    }

    /// <summary>
    /// Gets or sets whether to write over text rather than insert it.
    /// </summary>
    /// <return>true to write over text; otherwise, false. The default is false.</return>
    public bool OverType
    {
        get => this.OverTypeGet();

        set => this.OverTypeSet(value);
    }

    /// <summary>
    /// Gets or sets whether line endings in pasted text are converted to the document <see cref="EolMode" />.
    /// </summary>
    /// <returns>true to convert line endings in pasted text; otherwise, false. The default is true.</returns>
    public bool PasteConvertEndings
    {
        get => this.PasteConvertEndingsGet();

        set => this.PasteConvertEndingsSet(value);
    }

    /// <summary>
    /// Gets or sets the number of phases used when drawing.
    /// </summary>
    /// <returns>One of the <see cref="Phases" /> enumeration values. The default is <see cref="Phases.Two" />.</returns>
    public Phases PhasesDraw
    {
        get => this.PhasesDrawGet();
        
        set => this.PhasesDrawSet(value);
    }

    /// <summary>
    /// Gets or sets whether the document is read-only.
    /// </summary>
    /// <returns>true if the document is read-only; otherwise, false. The default is false.</returns>
    /// <seealso cref="ModifyAttempt" />
    public bool ReadOnly
    {
        get => this.ReadOnlyGet();
        
        set => this.ReadOnlySet(value);
    }

    /// <summary>
    /// Gets or sets the anchor position of the rectangular selection.
    /// </summary>
    /// <returns>The zero-based document position of the rectangular selection anchor.</returns>
    public int RectangularSelectionAnchor
    {
        get => this.RectangularSelectionAnchorGet(Lines);

        set => this.RectangularSelectionAnchorSet(value, Lines);
    }

    /// <summary>
    /// Gets or sets the amount of anchor virtual space in a rectangular selection.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the rectangular selection anchor.</returns>
    public int RectangularSelectionAnchorVirtualSpace
    {
        get => this.RectangularSelectionAnchorVirtualSpaceGet();

        set => this.RectangularSelectionAnchorVirtualSpaceSet(value);
    }

    /// <summary>
    /// Gets or sets the caret position of the rectangular selection.
    /// </summary>
    /// <returns>The zero-based document position of the rectangular selection caret.</returns>
    public int RectangularSelectionCaret
    {
        get => this.RectangularSelectionCaretGet(Lines);

        set => this.RectangularSelectionCaretSet(value, Lines);
    }

    /// <summary>
    /// Gets or sets the amount of caret virtual space in a rectangular selection.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the rectangular selection caret.</returns>
    public int RectangularSelectionCaretVirtualSpace
    {
        get => this.RectangularSelectionCaretVirtualSpaceGet();

        set => this.RectangularSelectionCaretVirtualSpaceSet(value);
    }

    /// <summary>
    /// Gets or sets the layer where the text selection will be painted. Default value is <see cref="Layer.Base"/>
    /// </summary>
    public Layer SelectionLayer
    {
        get => this.SelectionLayerGet();
        
        set => this.SelectionLayerSet(value);
    }

    /// <summary>
    /// Gets or sets the range of the horizontal scroll bar.
    /// </summary>
    /// <returns>The range in pixels of the horizontal scroll bar. The default is 2000.</returns>
    /// <remarks>The width will automatically increase as needed when <see cref="ScrollWidthTracking" /> is enabled.</remarks>
    public int ScrollWidth
    {
        get => this.ScrollWidthGet();

        set => this.ScrollWidthSet(value);
    }

    /// <summary>
    /// Gets or sets whether the <see cref="ScrollWidth" /> is automatically increased as needed.
    /// </summary>
    /// <returns>
    /// true to automatically increase the horizontal scroll width as needed; otherwise, false.
    /// The default is true.
    /// </returns>
    public bool ScrollWidthTracking
    {
        get => this.ScrollWidthTrackingGet();

        set => this.ScrollWidthTrackingSet(value);
    }

    /// <summary>
    /// Gets or sets the search flags used when searching text.
    /// </summary>
    /// <returns>A bitwise combination of <see cref="Abstractions.Enumerations.SearchFlags" /> values. The default is <see cref="Abstractions.Enumerations.SearchFlags.None" />.</returns>
    /// <seealso cref="SearchInTarget" />
    public SearchFlags SearchFlags
    {
        get => this.SearchFlagsGet();

        set => this.SearchFlagsSet(value);
    }

    /// <summary>
    /// Gets the selected text.
    /// </summary>
    /// <returns>The selected text if there is any; otherwise, an empty string.</returns>
    public string SelectedText => this.SelectedTextGet();

    /// <summary>
    /// Gets or sets the end position of the selection.
    /// </summary>
    /// <returns>The zero-based document position where the selection ends.</returns>
    /// <remarks>
    /// When getting this property, the return value is <code>Math.Max(<see cref="AnchorPosition" />, <see cref="CurrentPosition" />)</code>.
    /// When setting this property, <see cref="CurrentPosition" /> is set to the value specified and <see cref="AnchorPosition" /> set to <code>Math.Min(<see cref="AnchorPosition" />, <paramref name="value" />)</code>.
    /// The caret is not scrolled into view.
    /// </remarks>
    /// <seealso cref="SelectionStart" />
    public int SelectionEnd
    {
        get => this.SelectionEndGet(Lines);

        set => this.SelectionEndSet(value, Lines);
    }

    /// <summary>
    /// Gets or sets whether to fill past the end of a line with the selection background color.
    /// </summary>
    /// <returns>true to fill past the end of the line; otherwise, false. The default is false.</returns>
    public bool SelectionEolFilled
    {
        get => this.SelectionEolFilledGet();

        set => this.SelectionEolFilledSet(value);
    }

    /// <summary>
    /// Gets or sets the start position of the selection.
    /// </summary>
    /// <returns>The zero-based document position where the selection starts.</returns>
    /// <remarks>
    /// When getting this property, the return value is <code>Math.Min(<see cref="AnchorPosition" />, <see cref="CurrentPosition" />)</code>.
    /// When setting this property, <see cref="AnchorPosition" /> is set to the value specified and <see cref="CurrentPosition" /> set to <code>Math.Max(<see cref="CurrentPosition" />, <paramref name="value" />)</code>.
    /// The caret is not scrolled into view.
    /// </remarks>
    /// <seealso cref="SelectionEnd" />

    /// <summary>
    /// Gets or sets the last internal error code used by Scintilla.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Status" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.Status.Ok" />.
    /// </returns>
    /// <remarks>The status can be reset by setting the property to <see cref="Scintilla.NET.Abstractions.Enumerations.Status.Ok" />.</remarks>
    public Status Status
    {
        get => this.StatusGet();

        set => this.StatusSet(value);
    }

    /// <summary>
    /// Gets or sets how tab characters are represented when whitespace is visible.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.TabDrawMode" /> enumeration values.
    /// The default is <see cref="Abstractions.Enumerations.TabDrawMode.LongArrow" />.
    /// </returns>
    /// <seealso cref="ViewWhitespace" />
    public TabDrawMode TabDrawMode
    {
        get => this.TabDrawModeGet();

        set => this.TabDrawModeSet(value);
    }

    /// <summary>
    /// Gets or sets whether tab inserts a tab character, or indents.
    /// </summary>
    /// <returns>Whether tab inserts a tab character (false), or indents (true).</returns>
    public bool TabIndents
    {
        get => this.TabIndentsGet();

        set => this.TabIndentsSet(value);
    }

    /// <summary>
    /// Gets or sets the width of a tab as a multiple of a space character.
    /// </summary>
    /// <returns>The width of a tab measured in characters. The default is 4.</returns>
    public int TabWidth
    {
        get => this.TabWidthGet();

        set => this.TabWidthSet(value);
    }

    /// <summary>
    /// Gets or sets the end position used when performing a search or replace.
    /// </summary>
    /// <returns>The zero-based character position within the document to end a search or replace operation.</returns>
    /// <seealso cref="TargetStart"/>
    /// <seealso cref="SearchInTarget" />
    /// <seealso cref="ReplaceTarget" />
    public int TargetEnd
    {
        get => this.TargetEndGet(Lines);

        set => this.TargetEndSet(value, Lines);
    }

    /// <summary>
    /// Gets or sets the start position used when performing a search or replace.
    /// </summary>
    /// <returns>The zero-based character position within the document to start a search or replace operation.</returns>
    /// <seealso cref="TargetEnd"/>
    /// <seealso cref="SearchInTarget" />
    /// <seealso cref="ReplaceTarget" />
    public int TargetStart
    {
        get => this.TargetStartGet(Lines);

        set => this.TargetStartSet(value, Lines);
    }

    /// <summary>
    /// Gets the current target text.
    /// </summary>
    /// <returns>A String representing the text between <see cref="TargetStart" /> and <see cref="TargetEnd" />.</returns>
    /// <remarks>Targets which have a start position equal or greater to the end position will return an empty String.</remarks>
    /// <seealso cref="TargetStart" />
    /// <seealso cref="TargetEnd" />
    public string TargetText => this.TargetTextGet();

    /// <summary>
    /// Gets or sets the rendering technology used.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Technology" /> enumeration values.
    /// The default is <see cref="Abstractions.Enumerations.Technology.Default" />.
    /// </returns>
    public Technology Technology
    {
        get => this.TechnologyGet();
        
        set => this.TechnologySet(value);
    }

    /// <summary>
    /// Gets or sets the current document text in the <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>The text displayed in the control.</returns>
    /// <remarks>Depending on the length of text get or set, this operation can be expensive.</remarks>
    public string Text
    {
        get => this.TextGet();

        set => this.TextSet(value, false, ReadOnly, AppendText);
    }

    /// <summary>
    /// Gets the length of the text in the control.
    /// </summary>
    /// <returns>The number of characters in the document.</returns>
    public int TextLength => this.TextLengthGet(Lines);

    /// <summary>
    /// Gets or sets whether to use a mixture of tabs and spaces for indentation or purely spaces.
    /// </summary>
    /// <returns>true to use tab characters; otherwise, false. The default is true.</returns>
    public bool UseTabs
    {
        get => this.UseTabsGet();

        set => this.UseTabsSet(value);
    }

    /// <summary>
    /// Gets or sets the visibility of end-of-line characters.
    /// </summary>
    /// <returns>true to display end-of-line characters; otherwise, false. The default is false.</returns>
    public bool ViewEol
    {
        get => this.ViewEolGet();

        set => this.ViewEolSet(value);
    }

    /// <summary>
    /// Gets or sets how to display whitespace characters.
    /// </summary>
    /// <returns>One of the <see cref="WhitespaceMode" /> enumeration values. The default is <see cref="WhitespaceMode.Invisible" />.</returns>
    /// <seealso cref="SetWhitespaceForeColor" />
    /// <seealso cref="SetWhitespaceBackColor" />
    public WhitespaceMode ViewWhitespace
    {
        get => this.ViewWhitespaceGet();

        set => this.ViewWhitespaceSet(value);
    }

    /// <summary>
    /// Gets or sets the ability for the caret to move into an area beyond the end of each line, otherwise known as virtual space.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="VirtualSpace" /> enumeration.
    /// The default is <see cref="VirtualSpace.None" />.
    /// </returns>
    public VirtualSpace VirtualSpaceOptions
    {
        get => this.VirtualSpaceOptionsGet();

        set => this.VirtualSpaceOptionsSet(value);
    }

    /// <summary>
    /// Gets or sets whether to display the vertical scroll bar.
    /// </summary>
    /// <returns>true to display the vertical scroll bar when needed; otherwise, false. The default is true.</returns>
    public bool VScrollBar
    {
        get => this.VScrollBarGet();
        
        set => this.VScrollBarSet(value);
    }

    /// <summary>
    /// Gets or sets the size of the dots used to mark whitespace.
    /// </summary>
    /// <returns>The size of the dots used to mark whitespace. The default is 1.</returns>
    /// <seealso cref="ViewWhitespace" />
    public int WhitespaceSize
    {
        get => this.WhitespaceSizeGet();

        set => this.WhitespaceSizeSet(value);
    }

    /// <summary>
    /// Gets or sets the characters considered 'word' characters when using any word-based logic.
    /// </summary>
    /// <returns>A string of word characters.</returns>
    public string WordChars
    {
        get => this.WordCharsGet();
        
        set => this.WordCharsSet(value);
    }

    /// <summary>
    /// Gets or sets the line wrapping indent mode.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.WrapIndentMode" /> enumeration values.
    /// The default is <see cref="Fixed" />.
    /// </returns>
    public WrapIndentMode WrapIndentMode
    {
        get => this.WrapIndentModeGet();

        set => this.WrapIndentModeSet(value);
    }

    /// <summary>
    /// Gets or sets the line wrapping mode.
    /// </summary>
    /// <returns>
    /// One of the <see cref="WrapMode" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.WrapMode.Word" />.
    /// </returns>
    public WrapMode WrapMode
    {
        get => this.WrapModeGet();

        set => this.WrapModeSet(value);
    }

    /// <summary>
    /// Gets or sets the indented size in pixels of wrapped sub-lines.
    /// </summary>
    /// <returns>The indented size of wrapped sub-lines measured in pixels. The default is 0.</returns>
    /// <remarks>
    /// Setting <see cref="WrapVisualFlags" /> to <see cref="Abstractions.Enumerations.WrapVisualFlags.Start" /> will add an
    /// additional 1 pixel to the value specified.
    /// </remarks>
    public int WrapStartIndent
    {
        get => this.WrapStartIndentGet();

        set => this.WrapStartIndentSet(value);
    }

    /// <summary>
    /// Gets or sets the wrap visual flags.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="Abstractions.Enumerations.WrapVisualFlags" /> enumeration.
    /// The default is <see cref="Abstractions.Enumerations.WrapVisualFlags.None" />.
    /// </returns>
    public WrapVisualFlags WrapVisualFlags
    {
        get => this.WrapVisualFlagsGet();

        set => this.WrapVisualFlagsSet(value);
    }

    /// <summary>
    /// Gets or sets additional location options when displaying wrap visual flags.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Abstractions.Enumerations.WrapVisualFlagLocation" /> enumeration values.
    /// The default is <see cref="Abstractions.Enumerations.WrapVisualFlagLocation.Default" />.
    /// </returns>
    public WrapVisualFlagLocation WrapVisualFlagLocation
    {
        get => this.WrapVisualFlagLocationGet();

        set => this.WrapVisualFlagLocationSet(value);
    }

    /// <summary>
    /// Gets or sets the horizontal scroll offset.
    /// </summary>
    /// <returns>The horizontal scroll offset in pixels.</returns>
    public int XOffset
    {
        get => this.XOffsetGet();

        set => this.XOffsetSet(value);
    }

    /// <summary>
    /// Gets or sets the zoom factor.
    /// </summary>
    /// <returns>The zoom factor measured in points.</returns>
    /// <remarks>For best results, values should range from -10 to 20 points.</remarks>
    /// <seealso cref="ZoomIn" />
    /// <seealso cref="ZoomOut" />
    public int Zoom
    {
        get => this.ZoomGet();

        set => this.ZoomSet(value);
    }
    #endregion
}