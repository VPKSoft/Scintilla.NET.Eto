#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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

using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Collections;

namespace Scintilla.NET.Abstractions.Interfaces;

/// <summary>
/// Properties for Scintilla API with generic type members.
/// </summary>
/// <typeparam name="TColor">The type of the color used in the platform.</typeparam>
public interface IScintillaProperties<TColor>
    where TColor : struct
{
    /// <summary>
    /// Gets or sets the bi-directionality of the Scintilla control.
    /// </summary>
    /// <value>The bi-directionality of the Scintilla control.</value>
    public BiDirectionalDisplayType BiDirectionality { get; set; }

    /// <summary>
    /// Gets or sets the caret foreground color for additional selections.
    /// </summary>
    /// <returns>The caret foreground color in additional selections. The default is (127, 127, 127).</returns>
    TColor AdditionalCaretForeColor { get; set; }

    /// <summary>
    /// Gets or sets whether the carets in additional selections will blink.
    /// </summary>
    /// <returns>true if additional selection carets should blink; otherwise, false. The default is true.</returns>
    bool AdditionalCaretsBlink { get; set; }

    /// <summary>
    /// Gets or sets whether the carets in additional selections are visible.
    /// </summary>
    /// <returns>true if additional selection carets are visible; otherwise, false. The default is true.</returns>
    bool AdditionalCaretsVisible { get; set; }

    /// <summary>
    /// Gets or sets the current anchor position.
    /// </summary>
    /// <returns>The zero-based character position of the anchor.</returns>
    /// <remarks>
    /// Setting the current anchor position will create a selection between it and the <see cref="CurrentPosition" />.
    /// The caret is not scrolled into view.
    /// </remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ScrollCaret" />
    public int AnchorPosition { get; set; }

    /// <summary>
    /// Gets or sets the alpha transparency of additional multiple selections.
    /// </summary>
    /// <returns>
    /// The alpha transparency ranging from 0 (completely transparent) to 255 (completely opaque).
    /// The value 256 will disable alpha transparency. The default is 256.
    /// </returns>
    int AdditionalSelAlpha { get; set; }

    /// <summary>
    /// Gets or sets whether additional typing affects multiple selections.
    /// </summary>
    /// <returns>true if typing will affect multiple selections instead of just the main selection; otherwise, false. The default is false.</returns>
    bool AdditionalSelectionTyping { get; set; }

    /// <summary>
    /// Gets or sets the display of annotations.
    /// </summary>
    /// <returns>One of the <see cref="Annotation" /> enumeration values. The default is <see cref="Annotation.Hidden" />.</returns>
    Annotation AnnotationVisible { get; set; }

    /// <summary>
    /// Gets a value indicating whether there is an auto-completion list displayed.
    /// </summary>
    /// <returns>true if there is an active auto-completion list; otherwise, false.</returns>
    bool AutoCActive { get;  }

    /// <summary>
    /// Gets or sets whether to automatically cancel auto-completion when there are no viable matches.
    /// </summary>
    /// <returns>
    /// true to automatically cancel auto-completion when there is no possible match; otherwise, false.
    /// The default is true.
    /// </returns>
    bool AutoCAutoHide { get; set; }

    /// <summary>
    /// Gets or sets whether to cancel an auto-completion if the caret moves from its initial location,
    /// or is allowed to move to the word start.
    /// </summary>
    /// <returns>
    /// true to cancel auto-completion when the caret moves.
    /// false to allow the caret to move to the beginning of the word without cancelling auto-completion.
    /// </returns>
    bool AutoCCancelAtStart { get; set; }

    /// <summary>
    /// Gets the index of the current auto-completion list selection.
    /// </summary>
    /// <returns>The zero-based index of the current auto-completion selection.</returns>
    int AutoCCurrent { get; }

    /// <summary>
    /// Gets or sets whether to automatically select an item when it is the only one in an auto-completion list.
    /// </summary>
    /// <returns>
    /// true to automatically choose the only auto-completion item and not display the list; otherwise, false.
    /// The default is false.
    /// </returns>
    bool AutoCChooseSingle { get; set; }

    /// <summary>
    /// Gets or sets whether to delete any word characters following the caret after an auto-completion.
    /// </summary>
    /// <returns>
    /// true to delete any word characters following the caret after auto-completion; otherwise, false.
    /// The default is false.</returns>
    bool AutoCDropRestOfWord { get; set; }

    /// <summary>
    /// Gets or sets whether matching characters to an auto-completion list is case-insensitive.
    /// </summary>
    /// <returns>true to use case-insensitive matching; otherwise, false. The default is false.</returns>
    bool AutoCIgnoreCase { get; set; }

    /// <summary>
    /// Gets or sets the maximum height of the auto-completion list measured in rows.
    /// </summary>
    /// <returns>The max number of rows to display in an auto-completion window. The default is 5.</returns>
    /// <remarks>If there are more items in the list than max rows, a vertical scrollbar is shown.</remarks>
    int AutoCMaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the width in characters of the auto-completion list.
    /// </summary>
    /// <returns>
    /// The width of the auto-completion list expressed in characters, or 0 to automatically set the width
    /// to the longest item. The default is 0.
    /// </returns>
    /// <remarks>Any items that cannot be fully displayed will be indicated with ellipsis.</remarks>
    int AutoCMaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the auto-completion list sort order to expect when calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.AutoCShow" />.
    /// </summary>
    /// <returns>One of the <see cref="Order" /> enumeration values. The default is <see cref="Order.Presorted" />.</returns>
    Order AutoCOrder { get; set; }

    /// <summary>
    /// Gets or sets the delimiter character used to separate words in an auto-completion list.
    /// </summary>
    /// <returns>The separator character used when calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.AutoCShow" />. The default is the space character.</returns>
    /// <remarks>The <paramref name="value" /> specified should be limited to printable ASCII characters.</remarks>
    char AutoCSeparator { get; set; }

    /// <summary>
    /// Gets or sets the delimiter character used to separate words and image type identifiers in an auto-completion list.
    /// </summary>
    /// <returns>The separator character used to reference an image registered with <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.RegisterRgbaImage" />. The default is '?'.</returns>
    /// <remarks>The <paramref name="value" /> specified should be limited to printable ASCII characters.</remarks>
    char AutoCTypeSeparator { get; set; }

    /// <summary>
    /// Gets or sets the automatic folding flags.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="Scintilla.NET.Abstractions.Enumerations.AutomaticFold" /> enumeration.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.AutomaticFold.None" />.
    /// </returns>
    AutomaticFold AutomaticFold { get; set; }

    /// <summary>
    /// Gets or sets whether backspace deletes a character, or unindents.
    /// </summary>
    /// <returns>Whether backspace deletes a character, (false) or unindents (true).</returns>
    bool BackspaceUnIndents { get; set; }

    /// <summary>
    /// Gets or sets whether drawing is double-buffered.
    /// </summary>
    /// <returns>
    /// true to draw each line into an offscreen bitmap first before copying it to the screen; otherwise, false.
    /// The default is true.
    /// </returns>
    /// <remarks>Disabling buffer can improve performance but will cause flickering.</remarks>
    bool BufferedDraw { get; set; }

    /// <summary>
    /// Gets a value indicating whether there is a call tip window displayed.
    /// </summary>
    /// <returns>true if there is an active call tip window; otherwise, false.</returns>
    bool CallTipActive { get; }

    /// <summary>
    /// Gets a value indicating whether there is text on the clipboard that can be pasted into the document.
    /// </summary>
    /// <returns>true when there is text on the clipboard to paste; otherwise, false.</returns>
    /// <remarks>The document cannot be <see cref="ReadOnly" />  and the selection cannot contain protected text.</remarks>
    bool CanPaste { get; }

    /// <summary>
    /// Gets a value indicating whether there is an undo action to redo.
    /// </summary>
    /// <returns>true when there is something to redo; otherwise, false.</returns>
    bool CanRedo { get; }

    /// <summary>
    /// Gets a value indicating whether there is an action to undo.
    /// </summary>
    /// <returns>true when there is something to undo; otherwise, false.</returns>
    bool CanUndo { get; }

    /// <summary>
    /// Gets or sets the caret foreground color.
    /// </summary>
    /// <returns>The caret foreground color. The default is black.</returns>
    TColor CaretForeColor { get; set; }

    /// <summary>
    /// Gets or sets the caret line background color.
    /// </summary>
    /// <returns>The caret line background color. The default is yellow.</returns>
    TColor CaretLineBackColor { get; set; }

    /// <summary>
    /// Gets or sets the alpha transparency of the <see cref="CaretLineBackColor" />.
    /// </summary>
    /// <returns>
    /// The alpha transparency ranging from 0 (completely transparent) to 255 (completely opaque).
    /// The value 256 will disable alpha transparency. The default is 256.
    /// </returns>
    int CaretLineBackColorAlpha { get; set; }

    /// <summary>
    /// Gets or sets the width of the caret line frame.
    /// </summary>
    /// <returns><see cref="CaretLineVisible" /> must be set to true. A value of 0 disables the frame. The default is 0.</returns>
    int CaretLineFrame { get; set; }


    /// <summary>
    /// Gets or sets whether the caret line is visible (highlighted).
    /// </summary>
    /// <returns>true if the caret line is visible; otherwise, false. The default is false.</returns>
    bool CaretLineVisible { get; set; }

    /// <summary>
    /// Gets or sets whether the caret line is always visible even when the window is not in focus.
    /// </summary>
    /// <returns>true if the caret line is always visible; otherwise, false. The default is false.</returns>
    bool CaretLineVisibleAlways { get; set; }

    /// <summary>
    /// Gets or sets the layer where the line caret will be painted. Default value is <see cref="Layer.Base"/>
    /// </summary>
    Layer CaretLineLayer { get; set; }

    /// <summary>
    /// Gets or sets the caret blink rate in milliseconds.
    /// </summary>
    /// <returns>The caret blink rate measured in milliseconds. The default is 530.</returns>
    /// <remarks>A value of 0 will stop the caret blinking.</remarks>
    int CaretPeriod { get; set; }

    /// <summary>
    /// Gets or sets the caret display style.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.CaretStyle" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.CaretStyle.Line" />.
    /// </returns>
    CaretStyle CaretStyle { get; set; }

    /// <summary>
    /// Gets or sets the width in pixels of the caret.
    /// </summary>
    /// <returns>The width of the caret in pixels. The default is 1 pixel.</returns>
    /// <remarks>
    /// The caret width can only be set to a value of 0, 1, 2 or 3 pixels and is only effective
    /// when the <see cref="CaretStyle" /> property is set to <see cref="Scintilla.NET.Abstractions.Enumerations.CaretStyle.Line" />.
    /// </remarks>
    int CaretWidth { get; set; }

    /// <summary>
    /// Gets the current line index.
    /// </summary>
    /// <returns>The zero-based line index containing the <see cref="CurrentPosition" />.</returns>
    int CurrentLine { get; }

    /// <summary>
    /// Gets or sets the current caret position.
    /// </summary>
    /// <returns>The zero-based character position of the caret.</returns>
    /// <remarks>
    /// Setting the current caret position will create a selection between it and the current <see cref="AnchorPosition" />.
    /// The caret is not scrolled into view.
    /// </remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ScrollCaret" />
    public int CurrentPosition { get; set; }

    /// <summary>
    /// Gets a value indicating the start index of the secondary styles.
    /// </summary>
    /// <returns>Returns the distance between a primary style and its corresponding secondary style.</returns>
    int DistanceToSecondaryStyles { get; }

    /// <summary>
    /// Gets or sets the background color to use when indicating long lines with
    /// <see cref="Scintilla.NET.Abstractions.Enumerations.EdgeMode.Background" />.
    /// </summary>
    /// <returns>The background Color. The default is Silver.</returns>
    TColor EdgeColor { get; set; }

    /// <summary>
    /// Gets or sets the column number at which to begin indicating long lines.
    /// </summary>
    /// <returns>The number of columns in a long line. The default is 0.</returns>
    /// <remarks>
    /// When using <see cref="Scintilla.NET.Abstractions.Enumerations.EdgeMode.Line"/>, a column is defined as the width of a space character in the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Default" /> style.
    /// When using <see cref="Scintilla.NET.Abstractions.Enumerations.EdgeMode.Background" /> a column is equal to a character (including tabs).
    /// </remarks>
    int EdgeColumn { get; set; }

    /// <summary>
    /// Gets or sets the mode for indicating long lines.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.EdgeMode" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.EdgeMode.None" />.
    /// </returns>
    EdgeMode EdgeMode { get; set; }

    /// <summary>
    /// Gets or sets whether vertical scrolling ends at the last line or can scroll past.
    /// </summary>
    /// <returns>true if the maximum vertical scroll position ends at the last line; otherwise, false. The default is true.</returns>
    bool EndAtLastLine { get; set; }

    /// <summary>
    /// Gets or sets the end-of-line mode, or rather, the characters added into
    /// the document when the user presses the Enter key.
    /// </summary>
    /// <returns>One of the <see cref="Eol" /> enumeration values. The default is <see cref="Eol.CrLf" />.</returns>
    Eol EolMode { get; set; }

    /// <summary>
    /// Gets or sets the amount of whitespace added to the ascent (top) of each line.
    /// </summary>
    /// <returns>The extra line ascent. The default is zero.</returns>
    int ExtraAscent { get; set; }

    /// <summary>
    /// Gets or sets the amount of whitespace added to the descent (bottom) of each line.
    /// </summary>
    /// <returns>The extra line descent. The default is zero.</returns>
    int ExtraDescent { get; set; }

    /// <summary>
    /// Gets or sets the first visible line on screen.
    /// </summary>
    /// <returns>The zero-based index of the first visible screen line.</returns>
    /// <remarks>The value is a visible line, not a document line.</remarks>
    int FirstVisibleLine { get; set; }

    /// <summary>
    /// Gets or sets font quality (anti-aliasing method) used to render fonts.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.FontQuality" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.FontQuality.Default" />.
    /// </returns>
    FontQuality FontQuality { get; set; }

    /// <summary>
    /// Gets or sets the column number of the indentation guide to highlight.
    /// </summary>
    /// <returns>The column number of the indentation guide to highlight or 0 if disabled.</returns>
    /// <remarks>Guides are highlighted in the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.BraceLight" /> style. Column numbers can be determined by calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.GetColumn" />.</remarks>
    int HighlightGuide { get; set; }

    /// <summary>
    /// Gets or sets whether to display the horizontal scroll bar.
    /// </summary>
    /// <returns>true to display the horizontal scroll bar when needed; otherwise, false. The default is true.</returns>
    bool HScrollBar { get; set; }

    /// <summary>
    /// Gets or sets the strategy used to perform styling using application idle time.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.IdleStyling" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.IdleStyling.None" />.
    /// </returns>
    IdleStyling IdleStyling { get; set; }

    /// <summary>
    /// Gets or sets the size of indentation in terms of space characters.
    /// </summary>
    /// <returns>The indentation size measured in characters. The default is 0.</returns>
    /// <remarks> A value of 0 will make the indent width the same as the tab width.</remarks>
    int IndentWidth { get; set; }

    /// <summary>
    /// Gets or sets whether to display indentation guides.
    /// </summary>
    /// <returns>One of the <see cref="IndentView" /> enumeration values. The default is <see cref="IndentView.None" />.</returns>
    /// <remarks>The <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.IndentGuide" /> style can be used to specify the foreground and background color of indentation guides.</remarks>
    IndentView IndentationGuides { get; set; }

    /// <summary>
    /// Gets or sets the indicator used in a subsequent call to <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.IndicatorFillRange" /> or <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.IndicatorClearRange" />.
    /// </summary>
    /// <returns>The zero-based indicator index to apply when calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.IndicatorFillRange" /> or remove when calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.IndicatorClearRange" />.</returns>
    public int IndicatorCurrent { get; set; }

    /// <summary>
    /// Gets or sets the user-defined value used in a subsequent call to <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.IndicatorFillRange" />.
    /// </summary>
    /// <returns>The indicator value to apply when calling <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.IndicatorFillRange" />.</returns>
    int IndicatorValue { get; set; }

    /// <summary>
    /// This is used by clients that have complex focus requirements such as having their own window
    /// that gets the real focus but with the need to indicate that Scintilla has the logical focus.
    /// </summary>
    bool InternalFocusFlag { get; set; }

    /// <summary>
    /// Gets or sets the name of the lexer.
    /// </summary>
    /// <value>The name of the lexer.</value>
    /// <exception cref="InvalidOperationException">Lexer with the name of 'Value' was not found.</exception>
    string LexerName { get; set; }

    /// <summary>
    /// Gets or sets the layer where the text selection will be painted. Default value is <see cref="Layer.Base"/>
    /// </summary>
    public Layer SelectionLayer { get; set; }

    /// <summary>
    /// Gets or sets the current lexer.
    /// </summary>
    /// <returns>One of the <see cref="Lexer" /> enumeration values. The default is <see cref="Scintilla.NET.Abstractions.Enumerations.Lexer.Container" />.</returns>
    /// <exception cref="InvalidOperationException">
    /// No lexer name was found with the specified value.
    /// </exception>
    /// <remarks>This property will get more obsolete as time passes as the Scintilla v.5+ now uses strings to define lexers. The Lexer enumeration is not maintained.</remarks>
    [Obsolete("This property will get more obsolete as time passes as the Scintilla v.5+ now uses strings to define lexers. Please use the LexerName property instead.")]
    Lexer Lexer { get; set; }

    /// <summary>
    /// Gets or sets the current lexer by name.
    /// </summary>
    /// <returns>A String representing the current lexer.</returns>
    /// <remarks>Lexer names are case-sensitive.</remarks>
    string LexerLanguage { get; set; }

    /// <summary>
    /// Gets the combined result of the <see cref="LineEndTypesSupported" /> and <see cref="LineEndTypesAllowed" />
    /// properties to report the line end types actively being interpreted.
    /// </summary>
    /// <returns>A bitwise combination of the <see cref="LineEndType" /> enumeration.</returns>
    LineEndType LineEndTypesActive { get; }

    /// <summary>
    /// Gets or sets the line ending types interpreted by the <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="LineEndType" /> enumeration.
    /// The default is <see cref="LineEndType.Default" />.
    /// </returns>
    /// <remarks>The line ending types allowed must also be supported by the current lexer to be effective.</remarks>
    LineEndType LineEndTypesAllowed { get; set; }

    /// <summary>
    /// Gets the different types of line ends supported by the current lexer.
    /// </summary>
    /// <returns>A bitwise combination of the <see cref="LineEndType" /> enumeration.</returns>
    LineEndType LineEndTypesSupported { get;  }

    /// <summary>
    /// Gets the number of lines that can be shown on screen given a constant
    /// line height and the space available.
    /// </summary>
    /// <returns>
    /// The number of screen lines which could be displayed (including any partial lines).
    /// </returns>
    int LinesOnScreen { get; }

    /// <summary>
    /// Gets or sets the main selection when their are multiple selections.
    /// </summary>
    /// <returns>The zero-based main selection index.</returns>
    int MainSelection { get; set; }

    /// <summary>
    /// Gets a value indicating whether the document has been modified (is dirty)
    /// since the last call to <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetSavePoint" />.
    /// </summary>
    /// <returns>true if the document has been modified; otherwise, false.</returns>
    bool Modified { get; }

    /// <summary>
    /// Gets or sets the time in milliseconds the mouse must linger to generate a <see cref="DwellStart" /> event.
    /// </summary>
    /// <returns>
    /// The time in milliseconds the mouse must linger to generate a <see cref="DwellStart" /> event
    /// or <see cref="IApiConstants.TimeForever" /> if dwell events are disabled.
    /// </returns>
    int MouseDwellTime { get; set; }

    /// <summary>
    /// Gets or sets the ability to switch to rectangular selection mode while making a selection with the mouse.
    /// </summary>
    /// <returns>
    /// true if the current mouse selection can be switched to a rectangular selection by pressing the ALT key; otherwise, false.
    /// The default is false.
    /// </returns>
    bool MouseSelectionRectangularSwitch { get; set; }

    /// <summary>
    /// Gets or sets whether multiple selection is enabled.
    /// </summary>
    /// <returns>
    /// true if multiple selections can be made by holding the CTRL key and dragging the mouse; otherwise, false.
    /// The default is false.
    /// </returns>
    bool MultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets the behavior when pasting text into multiple selections.
    /// </summary>
    /// <returns>One of the <see cref="Scintilla.NET.Abstractions.Enumerations.MultiPaste" /> enumeration values. The default is <see cref="Scintilla.NET.Abstractions.Enumerations.MultiPaste.Once" />.</returns>
    MultiPaste MultiPaste { get; set; }

    /// <summary>
    /// Gets or sets whether to write over text rather than insert it.
    /// </summary>
    /// <return>true to write over text; otherwise, false. The default is false.</return>
    bool OverType { get; set; }

    /// <summary>
    /// Gets or sets whether line endings in pasted text are convereted to the document <see cref="EolMode" />.
    /// </summary>
    /// <returns>true to convert line endings in pasted text; otherwise, false. The default is true.</returns>
    bool PasteConvertEndings { get; set; }

    /// <summary>
    /// Gets or sets the number of phases used when drawing.
    /// </summary>
    /// <returns>One of the <see cref="Phases" /> enumeration values. The default is <see cref="Phases.Two" />.</returns>
    Phases PhasesDraw { get; set; }

    /// <summary>
    /// Gets or sets whether the document is read-only.
    /// </summary>
    /// <returns>true if the document is read-only; otherwise, false. The default is false.</returns>
    /// <seealso cref="ModifyAttempt" />
    bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets the amount of anchor virtual space in a rectangular selection.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the rectangular selection anchor.</returns>
    int RectangularSelectionAnchorVirtualSpace { get; set; }

    /// <summary>
    /// Gets or sets the amount of caret virtual space in a rectangular selection.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the rectangular selection caret.</returns>
    int RectangularSelectionCaretVirtualSpace { get; set; }

    /// <summary>
    /// Gets or sets the range of the horizontal scroll bar.
    /// </summary>
    /// <returns>The range in pixels of the horizontal scroll bar. The default is 2000.</returns>
    /// <remarks>The width will automatically increase as needed when <see cref="ScrollWidthTracking" /> is enabled.</remarks>
    int ScrollWidth { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="ScrollWidth" /> is automatically increased as needed.
    /// </summary>
    /// <returns>
    /// true to automatically increase the horizontal scroll width as needed; otherwise, false.
    /// The default is true.
    /// </returns>
    bool ScrollWidthTracking { get; set; }

    /// <summary>
    /// Gets or sets the search flags used when searching text.
    /// </summary>
    /// <returns>A bitwise combination of <see cref="Scintilla.NET.Abstractions.Enumerations.SearchFlags" /> values. The default is <see cref="Scintilla.NET.Abstractions.Enumerations.SearchFlags.None" />.</returns>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SearchInTarget" />
    SearchFlags SearchFlags { get; set; }

    /// <summary>
    /// Gets the selected text.
    /// </summary>
    /// <returns>The selected text if there is any; otherwise, an empty string.</returns>
    string SelectedText { get; }

    /// <summary>
    /// Gets or sets whether to fill past the end of a line with the selection background color.
    /// </summary>
    /// <returns>true to fill past the end of the line; otherwise, false. The default is false.</returns>
    bool SelectionEolFilled { get; set; }

    /// <summary>
    /// Gets or sets the last internal error code used by Scintilla.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Status" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.Status.Ok" />.
    /// </returns>
    /// <remarks>The status can be reset by setting the property to <see cref="Scintilla.NET.Abstractions.Enumerations.Status.Ok" />.</remarks>
    Status Status { get; set; }

    /// <summary>
    /// Gets or sets how tab characters are represented when whitespace is visible.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.TabDrawMode" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.TabDrawMode.LongArrow" />.
    /// </returns>
    /// <seealso cref="ViewWhitespace" />
    TabDrawMode TabDrawMode { get; set; }

    /// <summary>
    /// Gets or sets whether tab inserts a tab character, or indents.
    /// </summary>
    /// <returns>Whether tab inserts a tab character (false), or indents (true).</returns>
    bool TabIndents { get; set; }

    /// <summary>
    /// Gets or sets the width of a tab as a multiple of a space character.
    /// </summary>
    /// <returns>The width of a tab measured in characters. The default is 4.</returns>
    int TabWidth { get; set; }

        /// <summary>
    /// Gets or sets the end position used when performing a search or replace.
    /// </summary>
    /// <returns>The zero-based character position within the document to end a search or replace operation.</returns>
    /// <seealso cref="TargetStart"/>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SearchInTarget" />
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ReplaceTarget" />
    public int TargetEnd { get; set; }

    /// <summary>
    /// Gets or sets the start position used when performing a search or replace.
    /// </summary>
    /// <returns>The zero-based character position within the document to start a search or replace operation.</returns>
    /// <seealso cref="TargetEnd"/>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SearchInTarget" />
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ReplaceTarget" />
    public int TargetStart { get; set; }

    /// <summary>
    /// Gets the current target text.
    /// </summary>
    /// <returns>A String representing the text between <see cref="TargetStart" /> and <see cref="TargetEnd" />.</returns>
    /// <remarks>Targets which have a start position equal or greater to the end position will return an empty String.</remarks>
    /// <seealso cref="TargetStart" />
    /// <seealso cref="TargetEnd" />
     string TargetText { get; }

    /// <summary>
    /// Gets or sets the rendering technology used.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Technology" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.Technology.Default" />.
    /// </returns>
    Technology Technology { get; set; }

    /// <summary>
    /// Gets or sets the current document text in the <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>The text displayed in the control.</returns>
    /// <remarks>Depending on the length of text get or set, this operation can be expensive.</remarks>
    string Text { get; set; }

    /// <summary>
    /// Gets or sets whether to use a mixture of tabs and spaces for indentation or purely spaces.
    /// </summary>
    /// <returns>true to use tab characters; otherwise, false. The default is true.</returns>
    bool UseTabs { get; set; }

    /// <summary>
    /// Gets or sets the visibility of end-of-line characters.
    /// </summary>
    /// <returns>true to display end-of-line characters; otherwise, false. The default is false.</returns>
    bool ViewEol { get; set; }

    /// <summary>
    /// Gets or sets how to display whitespace characters.
    /// </summary>
    /// <returns>One of the <see cref="WhitespaceMode" /> enumeration values. The default is <see cref="WhitespaceMode.Invisible" />.</returns>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetWhitespaceForeColor" />
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.SetWhitespaceBackColor" />
    WhitespaceMode ViewWhitespace { get; set; }

    /// <summary>
    /// Gets or sets the ability for the caret to move into an area beyond the end of each line, otherwise known as virtual space.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="VirtualSpace" /> enumeration.
    /// The default is <see cref="VirtualSpace.None" />.
    /// </returns>
    VirtualSpace VirtualSpaceOptions { get; set; }
    
    /// <summary>
    /// Gets or sets whether to display the vertical scroll bar.
    /// </summary>
    /// <returns>true to display the vertical scroll bar when needed; otherwise, false. The default is true.</returns>
    bool VScrollBar { get; set; }

    /// <summary>
    /// Gets or sets the size of the dots used to mark whitespace.
    /// </summary>
    /// <returns>The size of the dots used to mark whitespace. The default is 1.</returns>
    /// <seealso cref="ViewWhitespace" />
    int WhitespaceSize { get; set; }

    /// <summary>
    /// Gets or sets the characters considered 'word' characters when using any word-based logic.
    /// </summary>
    /// <returns>A string of word characters.</returns>
     string WordChars { get; set; }

    /// <summary>
    /// Gets or sets the line wrapping indent mode.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.WrapIndentMode" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.WrapIndentMode.Fixed" />.
    /// </returns>
    WrapIndentMode WrapIndentMode { get; set; }

    /// <summary>
    /// Gets or sets the line wrapping mode.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.WrapMode" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.WrapMode.Word" />.
    /// </returns>
    WrapMode WrapMode { get; set; }

    /// <summary>
    /// Gets or sets the indented size in pixels of wrapped sublines.
    /// </summary>
    /// <returns>The indented size of wrapped sublines measured in pixels. The default is 0.</returns>
    /// <remarks>
    /// Setting <see cref="WrapVisualFlags" /> to <see cref="Scintilla.NET.Abstractions.Enumerations.WrapVisualFlags.Start" /> will add an
    /// additional 1 pixel to the value specified.
    /// </remarks>
    int WrapStartIndent { get; set; }

    /// <summary>
    /// Gets or sets the wrap visual flags.
    /// </summary>
    /// <returns>
    /// A bitwise combination of the <see cref="Scintilla.NET.Abstractions.Enumerations.WrapVisualFlags" /> enumeration.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.WrapVisualFlags.None" />.
    /// </returns>
    WrapVisualFlags WrapVisualFlags { get; set; }

    /// <summary>
    /// Gets or sets additional location options when displaying wrap visual flags.
    /// </summary>
    /// <returns>
    /// One of the <see cref="Scintilla.NET.Abstractions.Enumerations.WrapVisualFlagLocation" /> enumeration values.
    /// The default is <see cref="Scintilla.NET.Abstractions.Enumerations.WrapVisualFlagLocation.Default" />.
    /// </returns>
    WrapVisualFlagLocation WrapVisualFlagLocation { get; set; }

    /// <summary>
    /// Gets or sets the horizontal scroll offset.
    /// </summary>
    /// <returns>The horizontal scroll offset in pixels.</returns>
    int XOffset { get; set; }

    /// <summary>
    /// Gets or sets the zoom factor.
    /// </summary>
    /// <returns>The zoom factor measured in points.</returns>
    /// <remarks>For best results, values should range from -10 to 20 points.</remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ZoomIn" />
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ZoomOut" />
    int Zoom { get; set; }
}