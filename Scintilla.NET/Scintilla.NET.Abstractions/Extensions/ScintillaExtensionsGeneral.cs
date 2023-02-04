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

using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Scintilla.NET.Abstractions.Classes;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Structs;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Extensions;

/// <summary>
/// Extension methods for the common Scintilla methods and properties.
/// </summary>
public static class ScintillaExtensionsGeneral
{
    /// <summary>
    /// Increases the reference count of the specified document by 1.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="document">The document reference count to increase.</param>
    public static void AddRefDocumentExtension(this IScintillaApi scintilla, Document document)
    {
        var ptr = document.Value;
        scintilla.DirectMessage(SCI_ADDREFDOCUMENT, IntPtr.Zero, ptr);
    }

    /// <summary>
    /// Inserts the specified text at the current caret position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="text">The text to insert at the current caret position.</param>
    /// <remarks>The caret position is set to the end of the inserted text, but it is not scrolled into view.</remarks>
    public static unsafe void AddTextExtension(this IScintillaApi scintilla, string? text)
    {
        var bytes = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, zeroTerminated: false);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_ADDTEXT, new IntPtr(bytes.Length), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Adds an additional selection range to the existing main selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="caret">The zero-based document position to end the selection.</param>
    /// <param name="anchor">The zero-based document position to start the selection.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>A main selection must first have been set by a call to <see cref="SetSelectionExtension" />.</remarks>
    public static void AddSelectionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int caret, int anchor, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        caret = HelpersGeneral.Clamp(caret, 0, textLength);
        anchor = HelpersGeneral.Clamp(anchor, 0, textLength);

        caret = lines.CharToBytePosition(caret);
        anchor = lines.CharToBytePosition(anchor);

        scintilla.DirectMessage(SCI_ADDSELECTION, new IntPtr(caret), new IntPtr(anchor));
    }

    /// <summary>
    /// Allocates some number of sub-styles for a particular base style. Sub-styles are allocated contiguously.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="styleBase">The lexer style integer</param>
    /// <param name="numberStyles">The amount of sub-styles to allocate</param>
    /// <returns>Returns the first sub-style number allocated.</returns>
    public static int AllocateSubStylesExtension(this IScintillaApi scintilla, int styleBase, int numberStyles)
    {
        return scintilla.DirectMessage(SCI_ALLOCATESUBSTYLES, new IntPtr(styleBase), new IntPtr(numberStyles)).ToInt32();
    }

    /// <summary>
    /// Removes the annotation text for every <see cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" /> in the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void AnnotationClearAllExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_ANNOTATIONCLEARALL);
    }

    /// <summary>
    /// Adds the specified text to the end of the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="text">The text to add to the document.</param>
    /// <remarks>The current selection is not changed and the new text is not scrolled into view.</remarks>
    public static unsafe void AppendTextExtension(this IScintillaApi scintilla, string? text)
    {
        var bytes = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, zeroTerminated: false);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_APPENDTEXT, new IntPtr(bytes.Length), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Assigns the specified key definition to a <see cref="Scintilla" /> command.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="keyDefinition">The key combination to bind.</param>
    /// <param name="sciCommand">The command to assign.</param>
    /// <param name="translateKeysFunc">A delegate to translate the platform-depended keys enumeration value into integer understood by the Scintilla native.</param>
    public static void AssignCmdKeyExtension<TKeys>(this IScintillaApi scintilla, TKeys keyDefinition, Command sciCommand, Func<TKeys, int> translateKeysFunc)
        where TKeys : Enum
    {
        var keys = translateKeysFunc(keyDefinition);
        scintilla.DirectMessage(SCI_ASSIGNCMDKEY, new IntPtr(keys), new IntPtr((int)sciCommand));
    }

    /// <summary>
    /// Cancels any displayed auto-completion list.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="AutoCStopsExtension" />
    public static void AutoCCancelExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_AUTOCCANCEL);
    }

    /// <summary>
    /// Triggers completion of the current auto-completion word.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void AutoCCompleteExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_AUTOCCOMPLETE);
    }

    /// <summary>
    /// Selects an item in the auto-completion list.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="select">
    /// The auto-completion word to select.
    /// If found, the word in the auto-completion list is selected and the index can be obtained by calling <see cref="IScintillaProperties{TColor}.AutoCCurrent" />.
    /// If not found, the behavior is determined by <see cref="IScintillaProperties{TColor}.AutoCAutoHide" />.
    /// </param>
    /// <remarks>
    /// Comparisons are performed according to the <see cref="IScintillaProperties{TColor}.AutoCIgnoreCase" /> property
    /// and will match the first word starting with <paramref name="select" />.
    /// </remarks>
    public static unsafe void AutoCSelectExtension(this IScintillaApi scintilla, string select)
    {
        var bytes = HelpersGeneral.GetBytes(select, scintilla.Encoding, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_AUTOCSELECT, IntPtr.Zero, new IntPtr(bp));
        }
    }

    /// <summary>
    /// Sets the characters that, when typed, cause the auto-completion item to be added to the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="chars">A string of characters that trigger auto-completion. The default is null.</param>
    /// <param name="fillUpChars">A private pointer in the Scintilla control logic.</param>
    /// <remarks>Common fill-up characters are '(', '[', and '.' depending on the language.</remarks>
    public static unsafe void AutoCSetFillUpsExtension(this IScintillaApi scintilla, string? chars, ref IntPtr fillUpChars)
    {
        // Apparently Scintilla doesn't make a copy of our fill up string; it just keeps a pointer to it....
        // That means we need to keep a copy of the string around for the life of the control AND put it
        // in a place where it won't get moved by the GC.

        chars ??= string.Empty;

        if (fillUpChars != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(fillUpChars);
            fillUpChars = IntPtr.Zero;
        }

        var count = scintilla.Encoding.GetByteCount(chars) + 1;
        var newFillUpChars = Marshal.AllocHGlobal(count);
        fixed (char* ch = chars)
        {
            scintilla.Encoding.GetBytes(ch, chars.Length, (byte*)newFillUpChars, count);
        }

        ((byte*)newFillUpChars)[count - 1] = 0; // Null terminate
        fillUpChars = newFillUpChars;

        // var str = new String((sbyte*)fillUpChars, 0, count, Encoding);

        scintilla.DirectMessage(SCI_AUTOCSETFILLUPS, IntPtr.Zero, fillUpChars);
    }

    /// <summary>
    /// Displays an auto completion list.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="lenEntered">The number of characters already entered to match on.</param>
    /// <param name="list">A list of auto-completion words separated by the <see cref="IScintillaProperties{TColor}.AutoCSeparator" /> character.</param>
    public static unsafe void AutoCShowExtension(this IScintillaApi scintilla, int lenEntered, string list)
    {
        if (string.IsNullOrEmpty(list))
        {
            return;
        }

        lenEntered = HelpersGeneral.ClampMin(lenEntered, 0);
        if (lenEntered > 0)
        {
            // Convert to bytes by counting back the specified number of characters
            var endPos = scintilla.DirectMessage(SCI_GETCURRENTPOS).ToInt32();
            var startPos = endPos;
            for (var i = 0; i < lenEntered; i++)
                startPos = scintilla.DirectMessage(SCI_POSITIONRELATIVE, new IntPtr(startPos), new IntPtr(-1)).ToInt32();

            lenEntered = endPos - startPos;
        }

        var bytes = HelpersGeneral.GetBytes(list, scintilla.Encoding, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_AUTOCSHOW, new IntPtr(lenEntered), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Specifies the characters that will automatically cancel auto-completion without the need to call <see cref="AutoCCancelExtension" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="chars">A String of the characters that will cancel auto-completion. The default is empty.</param>
    /// <remarks>Characters specified should be limited to printable ASCII characters.</remarks>
    public static unsafe void AutoCStopsExtension(this IScintillaApi scintilla, string? chars)
    {
        var bytes = HelpersGeneral.GetBytes(chars ?? string.Empty, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_AUTOCSTOPS, IntPtr.Zero, new IntPtr(bp));
        }
    }

    /// <summary>
    /// Marks the beginning of a set of actions that should be treated as a single undo action.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>A call to <see cref="BeginUndoActionExtension" /> should be followed by a call to <see cref="EndUndoActionExtension" />.</remarks>
    /// <seealso cref="EndUndoActionExtension" />
    public static void BeginUndoActionExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_BEGINUNDOACTION);
    }

    /// <summary>
    /// Styles the specified character position with the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.BraceBad" /> style when there is an unmatched brace.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position of the unmatched brace character or <seealso cref="ApiConstants.InvalidPosition"/> to remove the highlight.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void BraceBadLightExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, -1, scintilla.TextLength);
        if (position > 0)
        {
            position = lines.CharToBytePosition(position);
        }

        scintilla.DirectMessage(SCI_BRACEBADLIGHT, new IntPtr(position));
    }

    /// <summary>
    /// Styles the specified character positions with the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.BraceLight" /> style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position1">The zero-based document position of the open brace character.</param>
    /// <param name="position2">The zero-based document position of the close brace character.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>Brace highlighting can be removed by specifying <see cref="ApiConstants.InvalidPosition" /> for <paramref name="position1" /> and <paramref name="position2" />.</remarks>
    /// <seealso cref="IScintillaProperties{TColor}.HighlightGuide" />
    public static void BraceHighlightExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position1, int position2, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;

        position1 = HelpersGeneral.Clamp(position1, -1, textLength);
        if (position1 > 0)
        {
            position1 = lines.CharToBytePosition(position1);
        }

        position2 = HelpersGeneral.Clamp(position2, -1, textLength);
        if (position2 > 0)
        {
            position2 = lines.CharToBytePosition(position2);
        }

        scintilla.DirectMessage(SCI_BRACEHIGHLIGHT, new IntPtr(position1), new IntPtr(position2));
    }

    /// <summary>
    /// Finds a corresponding matching brace starting at the position specified.
    /// The brace characters handled are '(', ')', '[', ']', '{', '}', '&lt;', and '&gt;'.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position of a brace character to start the search from for a matching brace character.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document position of the corresponding matching brace or <see cref="ApiConstants.InvalidPosition" /> it no matching brace could be found.</returns>
    /// <remarks>A match only occurs if the style of the matching brace is the same as the starting brace. Nested braces are handled correctly.</remarks>
    public static int BraceMatchExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);

        var match = scintilla.DirectMessage(SCI_BRACEMATCH, new IntPtr(position), IntPtr.Zero).ToInt32();
        if (match > 0)
        {
            match = lines.ByteToCharPosition(match);
        }

        return match;
    }

    /// <summary>
    /// Cancels the display of a call tip window.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void CallTipCancelExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CALLTIPCANCEL);
    }

    /// <summary>
    /// Sets the color of highlighted text in a call tip.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="color">The new highlight text Color. The default is dark blue.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    public static void CallTipSetForeHltExtension<TColor>(this IScintillaApi scintilla, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor: struct
    {
        var intColor = colorToIntFunc(color);
        scintilla.DirectMessage(SCI_CALLTIPSETFOREHLT, new IntPtr(intColor));
    }

    /// <summary>
    /// Sets the specified range of the call tip text to display in a highlighted style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="hlStart">The zero-based index in the call tip text to start highlighting.</param>
    /// <param name="hlEnd">The zero-based index in the call tip text to stop highlighting (exclusive).</param>
    /// <param name="lastCallTip">The previous call tip text of the Scintilla control wrapper.</param>
    public static unsafe void CallTipSetHltExtension(this IScintillaApi scintilla, int hlStart, int hlEnd, string lastCallTip)
    {
        // To do the char->byte translation we need to use a cached copy of the last call tip
        hlStart = HelpersGeneral.Clamp(hlStart, 0, lastCallTip.Length);
        hlEnd = HelpersGeneral.Clamp(hlEnd, 0, lastCallTip.Length);

        fixed (char* cp = lastCallTip)
        {
            hlEnd = scintilla.Encoding.GetByteCount(cp + hlStart, hlEnd - hlStart);  // The bytes between start and end
            hlStart = scintilla.Encoding.GetByteCount(cp, hlStart);                  // The bytes between 0 and start
            hlEnd += hlStart;                                              // The bytes between 0 and end
        }

        scintilla.DirectMessage(SCI_CALLTIPSETHLT, new IntPtr(hlStart), new IntPtr(hlEnd));
    }

    /// <summary>
    /// Determines whether to display a call tip above or below text.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="above">true to display above text; otherwise, false. The default is false.</param>
    public static void CallTipSetPositionExtension(this IScintillaApi scintilla, bool above)
    {
        var val = above ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_CALLTIPSETPOSITION, val);
    }

    /// <summary>
    /// Displays a call tip window.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="posStart">The zero-based document position where the call tip window should be aligned.</param>
    /// <param name="definition">The call tip text.</param>
    /// <param name="lastCallTip">The previous call tip text of the Scintilla control wrapper.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>
    /// Call tips can contain multiple lines separated by '\n' characters. Do not include '\r', as this will most likely print as an empty box.
    /// The '\t' character is supported and the size can be set by using <see cref="CallTipTabSizeExtension" />.
    /// </remarks>
    public static unsafe void CallTipShowExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int posStart, string? definition, ref string lastCallTip, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        posStart = HelpersGeneral.Clamp(posStart, 0, scintilla.TextLength);
        if (definition == null)
        {
            return;
        }

        lastCallTip = definition;
        posStart = lines.CharToBytePosition(posStart);
        var bytes = HelpersGeneral.GetBytes(definition, scintilla.Encoding, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_CALLTIPSHOW, new IntPtr(posStart), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Sets the call tip tab size in pixels.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="tabSize">The width in pixels of a tab '\t' character in a call tip. Specifying 0 disables special treatment of tabs.</param>
    public static void CallTipTabSizeExtension(this IScintillaApi scintilla, int tabSize)
    {
        // ReSharper disable twice CommentTypo
        // To support the STYLE_CALLTIP style we call SCI_CALLTIPUSESTYLE when the control is created. At
        // this point we're only adjusting the tab size. This breaks a bit with Scintilla convention, but
        // that's okay because the Scintilla convention is lame.

        tabSize = HelpersGeneral.ClampMin(tabSize, 0);
        scintilla.DirectMessage(SCI_CALLTIPUSESTYLE, new IntPtr(tabSize));
    }

    /// <summary>
    /// Indicates to the current <see cref="Lexer" /> that the internal lexer state has changed in the specified
    /// range and therefore may need to be redrawn.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="startPos">The zero-based document position at which the lexer state change starts.</param>
    /// <param name="endPos">The zero-based document position at which the lexer state change ends.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void ChangeLexerStateExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int startPos, int endPos, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        startPos = HelpersGeneral.Clamp(startPos, 0, textLength);
        endPos = HelpersGeneral.Clamp(endPos, 0, textLength);

        startPos = lines.CharToBytePosition(startPos);
        endPos = lines.CharToBytePosition(endPos);

        scintilla.DirectMessage(SCI_CHANGELEXERSTATE, new IntPtr(startPos), new IntPtr(endPos));
    }

    /// <summary>
    /// Finds the closest character position to the specified display point.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="x">The x pixel coordinate within the client rectangle of the control.</param>
    /// <param name="y">The y pixel coordinate within the client rectangle of the control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document position of the nearest character to the point specified.</returns>
    public static int CharPositionFromPointExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int x, int y, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var pos = scintilla.DirectMessage(SCI_CHARPOSITIONFROMPOINT, new IntPtr(x), new IntPtr(y)).ToInt32();
        pos = lines.ByteToCharPosition(pos);

        return pos;
    }

    /// <summary>
    /// Finds the closest character position to the specified display point or returns -1
    /// if the point is outside the window or not close to any characters.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="x">The x pixel coordinate within the client rectangle of the control.</param>
    /// <param name="y">The y pixel coordinate within the client rectangle of the control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document position of the nearest character to the point specified when near a character; otherwise, -1.</returns>
    public static int CharPositionFromPointCloseExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int x, int y, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var pos = scintilla.DirectMessage(SCI_CHARPOSITIONFROMPOINTCLOSE, new IntPtr(x), new IntPtr(y)).ToInt32();
        if (pos >= 0)
        {
            pos = lines.ByteToCharPosition(pos);
        }

        return pos;
    }

    /// <summary>
    /// Explicitly sets the current horizontal offset of the caret as the X position to track
    /// when the user moves the caret vertically using the up and down keys.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>
    /// When not set explicitly, Scintilla automatically sets this value each time the user moves
    /// the caret horizontally.
    /// </remarks>
    public static void ChooseCaretXExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CHOOSECARETX);
    }

    /// <summary>
    /// Removes the selected text from the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ClearExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CLEAR);
    }

    /// <summary>
    /// Deletes all document text, unless the document is read-only.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ClearAllExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CLEARALL);
    }

    /// <summary>
    /// Makes the specified key definition do nothing.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="keyDefinition">The key combination to bind.</param>
    /// <param name="translateKeysFunc">A delegate to translate the platform-depended keys enumeration value into integer understood by the Scintilla native.</param>
    /// <remarks>This is equivalent to binding the keys to <see cref="Command.Null" />.</remarks>
    public static void ClearCmdKeyExtension<TKeys>(this IScintillaApi scintilla, TKeys keyDefinition, Func<TKeys, int> translateKeysFunc)
        where TKeys : Enum
    {
        var keys = translateKeysFunc(keyDefinition);
        scintilla.DirectMessage(SCI_CLEARCMDKEY, new IntPtr(keys));
    }

    /// <summary>
    /// Removes all the key definition command mappings.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ClearAllCmdKeysExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CLEARALLCMDKEYS);
    }

    /// <summary>
    /// Removes all styling from the document and resets the folding state.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ClearDocumentStyleExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CLEARDOCUMENTSTYLE);
    }

    /// <summary>
    /// Removes all images registered for auto-completion lists.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ClearRegisteredImagesExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CLEARREGISTEREDIMAGES);
    }

    /// <summary>
    /// Sets a single empty selection at the start of the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ClearSelectionsExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CLEARSELECTIONS);
    }

    /// <summary>
    /// Requests that the current lexer restyle the specified range.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="startPos">The zero-based document position at which to start styling.</param>
    /// <param name="endPos">The zero-based document position at which to stop styling (exclusive).</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>This will also cause fold levels in the range specified to be reset.</remarks>
    public static void ColorizeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int startPos, int endPos, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        startPos = HelpersGeneral.Clamp(startPos, 0, textLength);
        endPos = HelpersGeneral.Clamp(endPos, 0, textLength);

        startPos = lines.CharToBytePosition(startPos);
        endPos = lines.CharToBytePosition(endPos);

        scintilla.DirectMessage(SCI_COLOURISE, new IntPtr(startPos), new IntPtr(endPos));
    }

    /// <summary>
    /// Changes all end-of-line characters in the document to the format specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="eolMode">One of the <see cref="Eol" /> enumeration values.</param>
    // ReSharper disable once IdentifierTypo
    public static void ConvertEolsExtension(this IScintillaApi scintilla, Eol eolMode)
    {
        var eol = (int)eolMode;
        scintilla.DirectMessage(SCI_CONVERTEOLS, new IntPtr(eol));
    }

    /// <summary>
    /// Copies the selected text from the document and places it on the clipboard.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void CopyExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_COPY);
    }

    /// <summary>
    /// Copies the selected text from the document and places it on the clipboard.
    /// If the selection is empty the current line is copied.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>
    /// If the selection is empty and the current line copied, an extra "MSDEVLineSelect" marker is added to the
    /// clipboard which is then used in <see cref="PasteExtension" /> to paste the whole line before the current line.
    /// </remarks>
    public static void CopyAllowLineExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_COPYALLOWLINE);
    }

    /// <summary>
    /// Copies the specified range of text to the clipboard.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="start">The zero-based character position in the document to start copying.</param>
    /// <param name="end">The zero-based character position (exclusive) in the document to stop copying.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void CopyRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int start, int end, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        start = HelpersGeneral.Clamp(start, 0, textLength);
        end = HelpersGeneral.Clamp(end, 0, textLength);

        // Convert to byte positions
        start = lines.CharToBytePosition(start);
        end = lines.CharToBytePosition(end);

        scintilla.DirectMessage(SCI_COPYRANGE, new IntPtr(start), new IntPtr(end));
    }

    /// <summary>
    /// Create a new, empty document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <returns>A new <see cref="Document" /> with a reference count of 1.</returns>
    /// <remarks>You are responsible for ensuring the reference count eventually reaches 0 or memory leaks will occur.</remarks>
    public static Document CreateDocumentExtension(this IScintillaApi scintilla)
    {
        var ptr = scintilla.DirectMessage(SCI_CREATEDOCUMENT);
        return new Document { Value = ptr };
    }

    /// <summary>
    /// Cuts the selected text from the document and places it on the clipboard.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void CutExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_CUT);
    }

    /// <summary>
    /// Deletes a range of text from the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based character position to start deleting.</param>
    /// <param name="length">The number of characters to delete.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void DeleteRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, int length, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        position = HelpersGeneral.Clamp(position, 0, textLength);
        length = HelpersGeneral.Clamp(length, 0, textLength - position);

        // Convert to byte position/length
        var byteStartPos = lines.CharToBytePosition(position);
        var byteEndPos = lines.CharToBytePosition(position + length);

        scintilla.DirectMessage(SCI_DELETERANGE, new IntPtr(byteStartPos), new IntPtr(byteEndPos - byteStartPos));
    }

    /// <summary>
    /// Retrieves a description of keyword sets supported by the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <returns>A String describing each keyword set separated by line breaks for the current lexer.</returns>
    public static unsafe string DescribeKeywordSetsExtension(this IScintillaApi scintilla)
    {
        var length = scintilla.DirectMessage(SCI_DESCRIBEKEYWORDSETS).ToInt32();
        var bytes = new byte[length + 1];

        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_DESCRIBEKEYWORDSETS, IntPtr.Zero, new IntPtr(bp));
        }

        var str = Encoding.ASCII.GetString(bytes, 0, length);
        return str;
    }

    /// <summary>
    /// Retrieves a brief description of the specified property name for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="name">A property name supported by the current <see cref="Lexer" />.</param>
    /// <returns>A String describing the lexer property name if found; otherwise, String.Empty.</returns>
    /// <remarks>A list of supported property names for the current <see cref="Lexer" /> can be obtained by calling <see cref="PropertyNamesExtension" />.</remarks>
    public static unsafe string DescribePropertyExtension(this IScintillaApi scintilla, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        var nameBytes = HelpersGeneral.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* nb = nameBytes)
        {
            var length = scintilla.DirectMessage(SCI_DESCRIBEPROPERTY, new IntPtr(nb), IntPtr.Zero).ToInt32();
            if (length == 0)
            {
                return string.Empty;
            }

            var descriptionBytes = new byte[length + 1];
            fixed (byte* db = descriptionBytes)
            {
                scintilla.DirectMessage(SCI_DESCRIBEPROPERTY, new IntPtr(nb), new IntPtr(db));
                return HelpersGeneral.GetString(new IntPtr(db), length, Encoding.ASCII);
            }
        }
    }

    /// <summary>
    /// Returns the zero-based document line index from the specified display line index.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="displayLine">The zero-based display line index.</param>
    /// <param name="visibleLineCount">The count of visible lines in the Scintilla control.</param>
    /// <returns>The zero-based document line index.</returns>
    /// <seealso cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.DisplayIndex" />
    public static int DocLineFromVisibleExtension(this IScintillaApi scintilla, int displayLine, int visibleLineCount)
    {
        displayLine = HelpersGeneral.Clamp(displayLine, 0, visibleLineCount);
        return scintilla.DirectMessage(SCI_DOCLINEFROMVISIBLE, new IntPtr(displayLine)).ToInt32();
    }

    /// <summary>
    /// If there are multiple selections, removes the specified selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="selection">The zero-based selection index.</param>
    /// <seealso cref="SelectionBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />
    public static void DropSelectionExtension(this IScintillaApi scintilla, int selection)
    {
        selection = HelpersGeneral.ClampMin(selection, 0);
        scintilla.DirectMessage(SCI_DROPSELECTIONN, new IntPtr(selection));
    }

    /// <summary>
    /// Clears any undo or redo history.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>This will also cause <see cref="SetSavePointExtension" /> to be called but will not raise the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.SavePointReached" /> event.</remarks>
    public static void EmptyUndoBufferExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_EMPTYUNDOBUFFER);
    }

    /// <summary>
    /// Marks the end of a set of actions that should be treated as a single undo action.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="BeginUndoActionExtension" />
    public static void EndUndoActionExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_ENDUNDOACTION);
    }

    /// <summary>
    /// Performs the specified <see cref="Scintilla" />command.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="sciCommand">The command to perform.</param>
    public static void ExecuteCmdExtension(this IScintillaApi scintilla, Command sciCommand)
    {
        var cmd = (int)sciCommand;
        scintilla.DirectMessage(cmd);
    }

    /// <summary>
    /// Performs the specified fold action on the entire document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    /// <remarks>When using <see cref="FoldAction.Toggle" /> the first fold header in the document is examined to decide whether to expand or contract.</remarks>
    public static void FoldAllExtension(this IScintillaApi scintilla, FoldAction action)
    {
        scintilla.DirectMessage(SCI_FOLDALL, new IntPtr((int)action));
    }

    /// <summary>
    /// Changes the appearance of fold text tags.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="style">One of the <see cref="FoldDisplayText" /> enumeration values.</param>
    /// <remarks>The text tag to display on a folded line can be set using <see cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.ToggleFoldShowText" />.</remarks>
    /// <seealso cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.ToggleFoldShowText" />.
    public static void FoldDisplayTextSetStyleExtension(this IScintillaApi scintilla, FoldDisplayText style)
    {
        scintilla.DirectMessage(SCI_FOLDDISPLAYTEXTSETSTYLE, new IntPtr((int)style));
    }

    /// <summary>
    /// Frees all allocated sub-styles.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void FreeSubStylesExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_FREESUBSTYLES);
    }

    /// <summary>
    /// Returns the character as the specified document position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position of the character to get.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The character at the specified <paramref name="position" />.</returns>
    public static unsafe int GetCharAtExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);

        var nextPosition = scintilla.DirectMessage(SCI_POSITIONRELATIVE, new IntPtr(position), new IntPtr(1)).ToInt32();
        var length = nextPosition - position;
        if (length <= 1)
        {
            // Position is at single-byte character
            return scintilla.DirectMessage(SCI_GETCHARAT, new IntPtr(position)).ToInt32();
        }

        // Position is at multi-byte character
        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            var range = stackalloc ScintillaApiStructs.Sci_TextRange[1];
            range->chrg.cpMin = position;
            range->chrg.cpMax = nextPosition;
            range->lpstrText = new IntPtr(bp);

            scintilla.DirectMessage(SCI_GETTEXTRANGE, IntPtr.Zero, new IntPtr(range));
            var str = HelpersGeneral.GetString(new IntPtr(bp), length, scintilla.Encoding);
            return str[0];
        }
    }

    /// <summary>
    /// Returns the column number of the specified document position, taking the width of tabs into account.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position to get the column for.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The number of columns from the start of the line to the specified document <paramref name="position" />.</returns>
    public static int GetColumnExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);
        return scintilla.DirectMessage(SCI_GETCOLUMN, new IntPtr(position)).ToInt32();
    }

    /// <summary>
    /// Returns the last document position likely to be styled correctly.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document position of the last styled character.</returns>
    public static int GetEndStyledExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct    
    {
        var pos = scintilla.DirectMessage(SCI_GETENDSTYLED).ToInt32();
        return lines.ByteToCharPosition(pos);
    }

    /// <summary>
    /// Gets the Primary style associated with the given Secondary style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="style">The secondary style</param>
    /// <returns>For a secondary style, return the primary style, else return the argument.</returns>
    public static int GetPrimaryStyleFromStyleExtension(this IScintillaApi scintilla, int style)
    {
        return scintilla.DirectMessage(SCI_GETPRIMARYSTYLEFROMSTYLE, new IntPtr(style)).ToInt32();
    }

    /// <summary>
    /// Lookup a property value for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="name">The property name to lookup.</param>
    /// <returns>
    /// A String representing the property value if found; otherwise, String.Empty.
    /// Any embedded property name macros as described in <see cref="SetPropertyExtension" /> will not be replaced (expanded).
    /// </returns>
    /// <seealso cref="GetPropertyExpandedExtension" />
    public static unsafe string GetPropertyExtension(this IScintillaApi scintilla, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        var nameBytes = HelpersGeneral.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* nb = nameBytes)
        {
            var length = scintilla.DirectMessage(SCI_GETPROPERTY, new IntPtr(nb)).ToInt32();
            if (length == 0)
            {
                return string.Empty;
            }

            var valueBytes = new byte[length + 1];
            fixed (byte* vb = valueBytes)
            {
                scintilla.DirectMessage(SCI_GETPROPERTY, new IntPtr(nb), new IntPtr(vb));
                return HelpersGeneral.GetString(new IntPtr(vb), length, Encoding.ASCII);
            }
        }
    }

    /// <summary>
    /// Lookup a property value for the current <see cref="Lexer" /> and expand any embedded property macros.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="name">The property name to lookup.</param>
    /// <returns>
    /// A String representing the property value if found; otherwise, String.Empty.
    /// Any embedded property name macros as described in <see cref="SetPropertyExtension" /> will be replaced (expanded).
    /// </returns>
    /// <seealso cref="GetPropertyExtension" />
    public static unsafe string GetPropertyExpandedExtension(this IScintillaApi scintilla, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        var nameBytes = HelpersGeneral.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* nb = nameBytes)
        {
            var length = scintilla.DirectMessage(SCI_GETPROPERTYEXPANDED, new IntPtr(nb)).ToInt32();
            if (length == 0)
            {
                return string.Empty;
            }

            var valueBytes = new byte[length + 1];
            fixed (byte* vb = valueBytes)
            {
                scintilla.DirectMessage(SCI_GETPROPERTYEXPANDED, new IntPtr(nb), new IntPtr(vb));
                return HelpersGeneral.GetString(new IntPtr(vb), length, Encoding.ASCII);
            }
        }
    }

    /// <summary>
    /// Lookup a property value for the current <see cref="Lexer" /> and convert it to an integer.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="name">The property name to lookup.</param>
    /// <param name="defaultValue">A default value to return if the property name is not found or has no value.</param>
    /// <returns>
    /// An Integer representing the property value if found;
    /// otherwise, <paramref name="defaultValue" /> if not found or the property has no value;
    /// otherwise, 0 if the property is not a number.
    /// </returns>
    public static unsafe int GetPropertyIntExtension(this IScintillaApi scintilla, string name, int defaultValue)
    {
        if (string.IsNullOrEmpty(name))
        {
            return defaultValue;
        }

        var bytes = HelpersGeneral.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            return scintilla.DirectMessage(SCI_GETPROPERTYINT, new IntPtr(bp), new IntPtr(defaultValue)).ToInt32();
        }
    }

    /// <summary>
    /// Gets the style of the specified document position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position of the character to get the style for.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" /> index used at the specified <paramref name="position" />.</returns>
    public static int GetStyleAtExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);

        return scintilla.DirectMessage(SCI_GETSTYLEAT, new IntPtr(position)).ToInt32();
    }

    /// <summary>
    /// Gets the lexer base style of a sub-style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="subStyle">The integer index of the sub-style</param>
    /// <returns>Returns the base style, else returns the argument.</returns>
    public static int GetStyleFromSubStyleExtension(this IScintillaApi scintilla, int subStyle)
    {
        return scintilla.DirectMessage(SCI_GETSTYLEFROMSUBSTYLE, new IntPtr(subStyle)).ToInt32();
    }

    /// <summary>
    /// Gets the length of the number of sub-styles allocated for a given lexer base style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="styleBase">The lexer style integer</param>
    /// <returns>Returns the length of the sub-styles allocated for a base style.</returns>
    public static int GetSubStylesLengthExtension(this IScintillaApi scintilla, int styleBase)
    {
        return scintilla.DirectMessage(SCI_GETSUBSTYLESLENGTH, new IntPtr(styleBase)).ToInt32();
    }

    /// <summary>
    /// Gets the start index of the sub-styles for a given lexer base style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="styleBase">The lexer style integer</param>
    /// <returns>Returns the start of the sub-styles allocated for a base style.</returns>
    public static int GetSubStylesStartExtension(this IScintillaApi scintilla, int styleBase)
    {
        return scintilla.DirectMessage(SCI_GETSUBSTYLESSTART, new IntPtr(styleBase)).ToInt32();
    }

    /// <summary>
    /// Returns the capture group text of the most recent regular expression search.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="tagNumber">The capture group (1 through 9) to get the text for.</param>
    /// <returns>A String containing the capture group text if it participated in the match; otherwise, an empty string.</returns>
    /// <seealso cref="SearchInTargetExtension" />
    public static unsafe string GetTagExtension(this IScintillaApi scintilla, int tagNumber)
    {
        tagNumber = HelpersGeneral.Clamp(tagNumber, 1, 9);
        var length = scintilla.DirectMessage(SCI_GETTAG, new IntPtr(tagNumber), IntPtr.Zero).ToInt32();
        if (length <= 0)
        {
            return string.Empty;
        }

        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_GETTAG, new IntPtr(tagNumber), new IntPtr(bp));
            return HelpersGeneral.GetString(new IntPtr(bp), length, scintilla.Encoding);
        }
    }

    /// <summary>
    /// Gets a range of text from the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based starting character position of the range to get.</param>
    /// <param name="length">The number of characters to get.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>A string representing the text range.</returns>
    public static string GetTextRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, int length, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        position = HelpersGeneral.Clamp(position, 0, textLength);
        length = HelpersGeneral.Clamp(length, 0, textLength - position);

        // Convert to byte position/length
        var byteStartPos = lines.CharToBytePosition(position);
        var byteEndPos = lines.CharToBytePosition(position + length);

        var ptr = scintilla.DirectMessage(SCI_GETRANGEPOINTER, new IntPtr(byteStartPos), new IntPtr(byteEndPos - byteStartPos));
        if (ptr == IntPtr.Zero)
        {
            return string.Empty;
        }

        return HelpersGeneral.GetString(ptr, byteEndPos - byteStartPos, scintilla.Encoding);
    }

    ///<summary>
    /// Gets the word from the position specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document character position to get the word from.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The word at the specified position.</returns>
    public static string GetWordFromPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var startPosition = WordStartPositionExtension(scintilla, position, true, lines);
        var endPosition = WordEndPositionExtension(scintilla, position, true, lines);
        return GetTextRangeExtension(scintilla, startPosition, endPosition - startPosition, lines);
    }

    /// <summary>
    /// Navigates the caret to the document position specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document character position to navigate to.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>Any selection is discarded.</remarks>
    public static void GotoPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);
        scintilla.DirectMessage(SCI_GOTOPOS, new IntPtr(position));
    }

    /// <summary>
    /// Hides the range of lines specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="lineStart">The zero-based index of the line range to start hiding.</param>
    /// <param name="lineEnd">The zero-based index of the line range to end hiding.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <seealso cref="ShowLinesExtension" />
    /// <seealso cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Visible" />
    public static void HideLinesExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int lineStart, int lineEnd, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        lineStart = HelpersGeneral.Clamp(lineStart, 0, lines.Count);
        lineEnd = HelpersGeneral.Clamp(lineEnd, lineStart, lines.Count);

        scintilla.DirectMessage(SCI_HIDELINES, new IntPtr(lineStart), new IntPtr(lineEnd));
    }

    /// <summary>
    /// Returns a bitmap representing the 32 indicators in use at the specified position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based character position within the document to test.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>A bitmap indicating which of the 32 indicators are in use at the specified <paramref name="position" />.</returns>
    public static uint IndicatorAllOnForExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);

        var bitmap = scintilla.DirectMessage(SCI_INDICATORALLONFOR, new IntPtr(position)).ToInt32();
        return unchecked((uint)bitmap);
    }

    /// <summary>
    /// Removes the <see cref="IScintillaProperties{TColor}.IndicatorCurrent" /> indicator (and user-defined value) from the specified range of text.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based character position within the document to start clearing.</param>
    /// <param name="length">The number of characters to clear.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void IndicatorClearRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, int length, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        position = HelpersGeneral.Clamp(position, 0, textLength);
        length = HelpersGeneral.Clamp(length, 0, textLength - position);

        var startPos = lines.CharToBytePosition(position);
        var endPos = lines.CharToBytePosition(position + length);

        scintilla.DirectMessage(SCI_INDICATORCLEARRANGE, new IntPtr(startPos), new IntPtr(endPos - startPos));
    }

    /// <summary>
    /// Adds the <see cref="IScintillaProperties{TColor}.IndicatorCurrent" /> indicator and <see cref="IScintillaProperties{TColor}.IndicatorValue" /> value to the specified range of text.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based character position within the document to start filling.</param>
    /// <param name="length">The number of characters to fill.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void IndicatorFillRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, int length, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        position = HelpersGeneral.Clamp(position, 0, textLength);
        length = HelpersGeneral.Clamp(length, 0, textLength - position);

        var startPos = lines.CharToBytePosition(position);
        var endPos = lines.CharToBytePosition(position + length);

        scintilla.DirectMessage(SCI_INDICATORFILLRANGE, new IntPtr(startPos), new IntPtr(endPos - startPos));
    }

    /// <summary>
    /// Initializes the document extension.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="eolMode">The eol mode.</param>
    /// <param name="useTabs">if set to <c>true</c> [use tabs].</param>
    /// <param name="tabWidth">Width of the tab.</param>
    /// <param name="indentWidth">Width of the indent.</param>
    public static void InitDocumentExtension(this IScintillaApi scintilla, Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0)
    {
        // Document.h
        // These properties are stored in the Scintilla document, not the control; meaning, when
        // a user changes documents these properties will change. If the user changes to a new
        // document, these properties will reset to defaults. That can cause confusion for our users
        // who would expect their tab settings, for example, to be unchanged based on which document
        // they have selected into the control. This is where we carry forward any of the user's
        // current settings -- and our default overrides -- to a new document.

        scintilla.DirectMessage(SCI_SETCODEPAGE, new IntPtr(SC_CP_UTF8));
        scintilla.DirectMessage(SCI_SETUNDOCOLLECTION, new IntPtr(1));
        scintilla.DirectMessage(SCI_SETEOLMODE, new IntPtr((int)eolMode));
        scintilla.DirectMessage(SCI_SETUSETABS, useTabs ? new IntPtr(1) : IntPtr.Zero);
        scintilla.DirectMessage(SCI_SETTABWIDTH, new IntPtr(tabWidth));
        scintilla.DirectMessage(SCI_SETINDENT, new IntPtr(indentWidth));
    }

    /// <summary>
    /// Inserts text at the specified position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based character position to insert the text. Specify -1 to use the current caret position.</param>
    /// <param name="text">The text to insert into the document.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="position" /> less than zero and not equal to -1. -or-
    /// <paramref name="position" /> is greater than the document length.
    /// </exception>
    /// <remarks>No scrolling is performed.</remarks>
    public static unsafe void InsertTextExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, string? text, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        if (position < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be greater or equal to zero, or -1.");
        }

        if (position != -1)
        {
            var textLength = scintilla.TextLength;
            if (position > textLength)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position cannot exceed document length.");
            }

            position = lines.CharToBytePosition(position);
        }

        fixed (byte* bp = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, zeroTerminated: true))
        {
            scintilla.DirectMessage(SCI_INSERTTEXT, new IntPtr(position), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Determines whether the specified <paramref name="start" /> and <paramref name="end" /> positions are
    /// at the beginning and end of a word, respectively.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="start">The zero-based document position of the possible word start.</param>
    /// <param name="end">The zero-based document position of the possible word end.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>
    /// true if <paramref name="start" /> and <paramref name="end" /> are at the beginning and end of a word, respectively;
    /// otherwise, false.
    /// </returns>
    /// <remarks>
    /// This method does not check whether there is whitespace in the search range,
    /// only that the <paramref name="start" /> and <paramref name="end" /> are at word boundaries.
    /// </remarks>
    public static bool IsRangeWordExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int start, int end, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var textLength = scintilla.TextLength;
        start = HelpersGeneral.Clamp(start, 0, textLength);
        end = HelpersGeneral.Clamp(end, 0, textLength);

        start = lines.CharToBytePosition(start);
        end = lines.CharToBytePosition(end);

        return scintilla.DirectMessage(SCI_ISRANGEWORD, new IntPtr(start), new IntPtr(end)) != IntPtr.Zero;
    }

    /// <summary>
    /// Returns the line that contains the document position specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document character position.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document line index containing the character <paramref name="position" />.</returns>
    public static int LineFromPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        return lines.LineFromCharPosition(position);
    }

    /// <summary>
    /// Scrolls the display the number of lines and columns specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="lines">The number of lines to scroll.</param>
    /// <param name="columns">The number of columns to scroll.</param>
    /// <remarks>
    /// Negative values scroll in the opposite direction.
    /// A column is the width in pixels of a space character in the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Default" /> style.
    /// </remarks>
    public static void LineScrollExtension(this IScintillaApi scintilla, int lines, int columns)
    {
        scintilla.DirectMessage(SCI_LINESCROLL, new IntPtr(columns), new IntPtr(lines));
    }

    /// <summary>
    /// Loads a <see cref="Scintilla" /> compatible lexer from an external DLL.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="path">The path to the external lexer DLL.</param>
    public static unsafe void LoadLexerLibraryExtension(this IScintillaApi scintilla, string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        var bytes = HelpersGeneral.GetBytes(path, Encoding.Default, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_LOADLEXERLIBRARY, IntPtr.Zero, new IntPtr(bp));
        }
    }

    /// <summary>
    /// Removes the specified marker from all lines.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="marker">The zero-based <see cref="MarkerBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" /> index to remove from all lines, or -1 to remove all markers from all lines.</param>
    /// <param name="markers">The Scintilla.Markers collection property value.</param>
    public static void MarkerDeleteAllExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int marker, IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> markers)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        marker = HelpersGeneral.Clamp(marker, -1, markers.Count - 1);
        scintilla.DirectMessage(SCI_MARKERDELETEALL, new IntPtr(marker));
    }

    /// <summary>
    /// Searches the document for the marker handle and deletes the marker if found.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="markerHandle">The <see cref="MarkerHandle" /> created by a previous call to <see cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.MarkerAdd" /> of the marker to delete.</param>
    public static void MarkerDeleteHandleExtension(this IScintillaApi scintilla, MarkerHandle markerHandle)
    {
        scintilla.DirectMessage(SCI_MARKERDELETEHANDLE, markerHandle.Value);
    }

    /// <summary>
    /// Enable or disable highlighting of the current folding block.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="enabled">true to highlight the current folding block; otherwise, false.</param>
    public static void MarkerEnableHighlightExtension(this IScintillaApi scintilla, bool enabled)
    {
        var val = enabled ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_MARKERENABLEHIGHLIGHT, val);
    }

    /// <summary>
    /// Searches the document for the marker handle and returns the line number containing the marker if found.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="markerHandle">The <see cref="MarkerHandle" /> created by a previous call to <see cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.MarkerAdd" /> of the marker to search for.</param>
    /// <returns>If found, the zero-based line index containing the marker; otherwise, -1.</returns>
    public static int MarkerLineFromHandleExtension(this IScintillaApi scintilla, MarkerHandle markerHandle)
    {
        return scintilla.DirectMessage(SCI_MARKERLINEFROMHANDLE, markerHandle.Value).ToInt32();
    }

    /// <summary>
    /// Specifies the long line indicator column number and color when <see cref="EdgeMode" /> is <see cref="global::Scintilla.NET.Abstractions.Enumerations.EdgeMode.MultiLine" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="column">The zero-based column number to indicate.</param>
    /// <param name="edgeColor">The color of the vertical long line indicator.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <remarks>A column is defined as the width of a space character in the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Default" /> style.</remarks>
    /// <seealso cref="MultiEdgeClearAllExtension" />
    public static void MultiEdgeAddLineExtension<TColor>(this IScintillaApi scintilla, int column, TColor edgeColor, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        column = HelpersGeneral.ClampMin(column, 0);
        var intColor = colorToIntFunc(edgeColor);

        scintilla.DirectMessage(SCI_MULTIEDGEADDLINE, new IntPtr(column), new IntPtr(intColor));
    }

    /// <summary>
    /// Removes all the long line column indicators specified using <seealso cref="MultiEdgeAddLineExtension{TColor}" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="MultiEdgeAddLineExtension{TColor}" />
    public static void MultiEdgeClearAllExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_MULTIEDGECLEARALL);
    }

    /// <summary>
    /// Searches for all instances of the main selection within the <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" />
    /// range and adds any matches to the selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>
    /// The <see cref="SearchFlags" /> property is respected when searching, allowing additional
    /// selections to match on different case sensitivity and word search options.
    /// </remarks>
    /// <seealso cref="MultipleSelectAddNextExtension" />
    public static void MultipleSelectAddEachExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_MULTIPLESELECTADDEACH);
    }

    /// <summary>
    /// Searches for the next instance of the main selection within the <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" />
    /// range and adds any match to the selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>
    /// The <see cref="SearchFlags" /> property is respected when searching, allowing additional
    /// selections to match on different case sensitivity and word search options.
    /// </remarks>
    /// <seealso cref="MultipleSelectAddNextExtension" />
    public static void MultipleSelectAddNextExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_MULTIPLESELECTADDNEXT);
    }

    /// <summary>
    /// Pastes the contents of the clipboard into the current selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void PasteExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_PASTE);
    }

    /// <summary>
    /// Returns the X display pixel location of the specified document position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="pos">The zero-based document character position.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The x-coordinate of the specified <paramref name="pos" /> within the client rectangle of the control.</returns>
    public static int PointXFromPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int pos, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        pos = HelpersGeneral.Clamp(pos, 0, scintilla.TextLength);
        pos = lines.CharToBytePosition(pos);
        return scintilla.DirectMessage(SCI_POINTXFROMPOSITION, IntPtr.Zero, new IntPtr(pos)).ToInt32();
    }

    /// <summary>
    /// Returns the Y display pixel location of the specified document position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="pos">The zero-based document character position.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The y-coordinate of the specified <paramref name="pos" /> within the client rectangle of the control.</returns>
    public static int PointYFromPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int pos, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        pos = HelpersGeneral.Clamp(pos, 0, scintilla.TextLength);
        pos = lines.CharToBytePosition(pos);
        return scintilla.DirectMessage(SCI_POINTYFROMPOSITION, IntPtr.Zero, new IntPtr(pos)).ToInt32();
    }

    /// <summary>
    /// Retrieves a list of property names that can be set for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <returns>A String of property names separated by line breaks.</returns>
    public static unsafe string PropertyNamesExtension(this IScintillaApi scintilla)
    {
        var length = scintilla.DirectMessage(SCI_PROPERTYNAMES).ToInt32();
        if (length == 0)
        {
            return string.Empty;
        }

        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_PROPERTYNAMES, IntPtr.Zero, new IntPtr(bp));
            return HelpersGeneral.GetString(new IntPtr(bp), length, Encoding.ASCII);
        }
    }

    /// <summary>
    /// Retrieves the data type of the specified property name for the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="name">A property name supported by the current <see cref="Lexer" />.</param>
    /// <returns>One of the <see cref="PropertyType" /> enumeration values. The default is <see cref="bool" />.</returns>
    /// <remarks>A list of supported property names for the current <see cref="Lexer" /> can be obtained by calling <see cref="PropertyNamesExtension" />.</remarks>
    public static unsafe PropertyType PropertyTypeExtension(this IScintillaApi scintilla, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return PropertyType.Boolean;
        }

        var bytes = HelpersGeneral.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            return (PropertyType)scintilla.DirectMessage(SCI_PROPERTYTYPE, new IntPtr(bp));
        }
    }

    /// <summary>
    /// Redoes the effect of an <see cref="UndoExtension" /> operation.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void RedoExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_REDO);
    }

    /// <summary>
    /// Decreases the reference count of the specified document by 1.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="document">
    /// The document reference count to decrease.
    /// When a document's reference count reaches 0 it is destroyed and any associated memory released.
    /// </param>
    public static void ReleaseDocumentExtension(this IScintillaApi scintilla, Document document)
    {
        var ptr = document.Value;
        scintilla.DirectMessage(SCI_RELEASEDOCUMENT, IntPtr.Zero, ptr);
    }

    /// <summary>
    /// Replaces the current selection with the specified text.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="text">The text that should replace the current selection.</param>
    /// <remarks>
    /// If there is not a current selection, the text will be inserted at the current caret position.
    /// Following the operation the caret is placed at the end of the inserted text and scrolled into view.
    /// </remarks>
    public static unsafe void ReplaceSelectionExtension(this IScintillaApi scintilla, string? text)
    {
        fixed (byte* bp = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, zeroTerminated: true))
        {
            scintilla.DirectMessage(SCI_REPLACESEL, IntPtr.Zero, new IntPtr(bp));
        }
    }

    /// <summary>
    /// Replaces the target defined by <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> with the specified <paramref name="text" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="text">The text that will replace the current target.</param>
    /// <returns>The length of the replaced text.</returns>
    /// <remarks>
    /// The <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> properties will be updated to the start and end positions of the replaced text.
    /// The recommended way to delete text in the document is to set the target range to be removed and replace the target with an empty string.
    /// </remarks>
    public static unsafe int ReplaceTargetExtension(this IScintillaApi scintilla, string? text)
    {
        text ??= string.Empty;

        var bytes = HelpersGeneral.GetBytes(text, scintilla.Encoding, false);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_REPLACETARGET, new IntPtr(bytes.Length), new IntPtr(bp));
        }

        return text.Length;
    }

    /// <summary>
    /// Replaces the target text defined by <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> with the specified value after first substituting
    /// "\1" through "\9" macros in the <paramref name="text" /> with the most recent regular expression capture groups.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="text">The text containing "\n" macros that will be substituted with the most recent regular expression capture groups and then replace the current target.</param>
    /// <param name="targetStart">The start position used when performing a search or replace within the Scintilla control.</param>
    /// <param name="targetEnd">The end position used when performing a search or replace within the Scintilla control.</param>
    /// <returns>The length of the replaced text.</returns>
    /// <remarks>
    /// The "\0" macro will be substituted by the entire matched text from the most recent search.
    /// The <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> properties will be updated to the start and end positions of the replaced text.
    /// </remarks>
    /// <seealso cref="GetTagExtension" />
    public static unsafe int ReplaceTargetReExtension(this IScintillaApi scintilla, string? text, int targetStart, int targetEnd)
    {
        var bytes = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, false);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_REPLACETARGETRE, new IntPtr(bytes.Length), new IntPtr(bp));
        }

        return Math.Abs(targetEnd - targetStart);
    }

    /// <summary>
    /// Makes the next selection the main selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void RotateSelectionExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_ROTATESELECTION);
    }

    /// <summary>
    /// Scrolls the current position into view, if it is not already visible.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void ScrollCaretExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_SCROLLCARET);
    }

    /// <summary>
    /// Scrolls the specified range into view.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="start">The zero-based document start position to scroll to.</param>
    /// <param name="end">
    /// The zero-based document end position to scroll to if doing so does not cause the <paramref name="start" />
    /// position to scroll out of view.
    /// </param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>This may be used to make a search match visible.</remarks>
    public static void ScrollRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int start, int end, int textLength, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        start = HelpersGeneral.Clamp(start, 0, textLength);
        end = HelpersGeneral.Clamp(end, 0, textLength);

        // Convert to byte positions
        start = lines.CharToBytePosition(start);
        end = lines.CharToBytePosition(end);

        // The arguments would  seem reverse from Scintilla documentation
        // but empirical  evidence suggests this is correct....
        scintilla.DirectMessage(SCI_SCROLLRANGE, new IntPtr(start), new IntPtr(end));
    }

    /// <summary>
    /// Searches for the first occurrence of the specified text in the target defined by <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="text">The text to search for. The interpretation of the text (i.e. whether it is a regular expression) is defined by the <see cref="SearchFlags" /> property.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based start position of the matched text within the document if successful; otherwise, -1.</returns>
    /// <remarks>
    /// If successful, the <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> properties will be updated to the start and end positions of the matched text.
    /// Searching can be performed in reverse using a <see cref="IScintillaProperties{TColor}.TargetStart" /> greater than the <see cref="IScintillaProperties{TColor}.TargetEnd" />.
    /// </remarks>
    public static unsafe int SearchInTargetExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, string? text, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        int bytePos;
        var bytes = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, zeroTerminated: false);
        fixed (byte* bp = bytes)
        {
            bytePos = scintilla.DirectMessage(SCI_SEARCHINTARGET, new IntPtr(bytes.Length), new IntPtr(bp)).ToInt32();
        }

        return bytePos == -1 ? bytePos : lines.ByteToCharPosition(bytePos);
    }

    /// <summary>
    /// Selects all the text in the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <remarks>The current position is not scrolled into view.</remarks>
    public static void SelectAllExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_SELECTALL);
    }

    /// <summary>
    /// Sets the background color of additional selections.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="color">Additional selections background color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <remarks>Calling <see cref="SetSelectionBackColorExtension{TColor}" /> will reset the <paramref name="color" /> specified.</remarks>
    public static void SetAdditionalSelBackExtension<TColor>(this IScintillaApi scintilla, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        scintilla.DirectMessage(SCI_SETADDITIONALSELBACK, new IntPtr(intColor));
    }

    /// <summary>
    /// Sets the foreground color of additional selections.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="color">Additional selections foreground color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <remarks>Calling <see cref="SetSelectionForeColorExtension{TColor}" /> will reset the <paramref name="color" /> specified.</remarks>
    public static void SetAdditionalSelForeExtension<TColor>(this IScintillaApi scintilla, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        scintilla.DirectMessage(SCI_SETADDITIONALSELFORE, new IntPtr(intColor));
    }

    /// <summary>
    /// Removes any selection and places the caret at the specified position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="pos">The zero-based document position to place the caret at.</param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>The caret is not scrolled into view.</remarks>
    public static void SetEmptySelectionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int pos, int textLength, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        pos = HelpersGeneral.Clamp(pos, 0, textLength);
        pos = lines.CharToBytePosition(pos);
        scintilla.DirectMessage(SCI_SETEMPTYSELECTION, new IntPtr(pos));
    }

    /// <summary>
    /// Sets additional options for displaying folds.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="flags">A bitwise combination of the <see cref="FoldFlags" /> enumeration.</param>
    public static void SetFoldFlagsExtension(this IScintillaApi scintilla, FoldFlags flags)
    {
        scintilla.DirectMessage(SCI_SETFOLDFLAGS, new IntPtr((int)flags));
    }

    /// <summary>
    /// Sets a global override to the fold margin color.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="use">true to override the fold margin color; otherwise, false.</param>
    /// <param name="color">The global fold margin color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <seealso cref="SetFoldMarginHighlightColorExtension{TColor}" />
    public static void SetFoldMarginColorExtension<TColor>(this IScintillaApi scintilla, bool use, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        var useFoldMarginColor = use ? new IntPtr(1) : IntPtr.Zero;

        scintilla.DirectMessage(SCI_SETFOLDMARGINCOLOUR, useFoldMarginColor, new IntPtr(intColor));
    }

    /// <summary>
    /// Sets a global override to the fold margin highlight color.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="use">true to override the fold margin highlight color; otherwise, false.</param>
    /// <param name="color">The global fold margin highlight color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <seealso cref="SetFoldMarginColorExtension{TColor}" />
    public static void SetFoldMarginHighlightColorExtension<TColor>(this IScintillaApi scintilla, bool use, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        var useFoldMarginHighlightColor = use ? new IntPtr(1) : IntPtr.Zero;

        scintilla.DirectMessage(SCI_SETFOLDMARGINHICOLOUR, useFoldMarginHighlightColor, new IntPtr(intColor));
    }

    /// <summary>
    /// Similar to <see cref="SetKeywordsExtension" /> but for sub-styles.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="style">The sub-style integer index</param>
    /// <param name="identifiers">A list of words separated by whitespace (space, tab, '\n', '\r') characters.</param>
    public static unsafe void SetIdentifiersExtension(this IScintillaApi scintilla, int style, string? identifiers)
    {
        var baseStyle = GetStyleFromSubStyleExtension(scintilla, style);
        var min = GetSubStylesStartExtension(scintilla, baseStyle);
        var length = GetStyleFromSubStyleExtension(scintilla, baseStyle);
        var max = length > 0 ? min + length - 1 : min;

        style = HelpersGeneral.Clamp(style, min, max);
        var bytes = HelpersGeneral.GetBytes(identifiers ?? string.Empty, Encoding.ASCII, zeroTerminated: true);

        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_SETIDENTIFIERS, new IntPtr(style), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Updates a keyword set used by the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="set">The zero-based index of the keyword set to update.</param>
    /// <param name="keywords">
    /// A list of keywords pertaining to the current <see cref="Lexer" /> separated by whitespace (space, tab, '\n', '\r') characters.
    /// </param>
    /// <remarks>The keywords specified will be styled according to the current <see cref="Lexer" />.</remarks>
    /// <seealso cref="DescribeKeywordSetsExtension" />
    public static unsafe void SetKeywordsExtension(this IScintillaApi scintilla, int set, string? keywords)
    {
        set = HelpersGeneral.Clamp(set, 0, KEYWORDSET_MAX);
        var bytes = HelpersGeneral.GetBytes(keywords ?? string.Empty, Encoding.ASCII, zeroTerminated: true);

        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_SETKEYWORDS, new IntPtr(set), new IntPtr(bp));
        }
    }

    /// <summary>
    /// Passes the specified property name-value pair to the current <see cref="Lexer" />.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="name">The property name to set.</param>
    /// <param name="value">
    /// The property value. Values can refer to other property names using the syntax $(name), where 'name' is another property
    /// name for the current <see cref="Lexer" />. When the property value is retrieved by a call to <see cref="GetPropertyExpandedExtension" />
    /// the embedded property name macro will be replaced (expanded) with that current property value.
    /// </param>
    /// <remarks>Property names are case-sensitive.</remarks>
    public static unsafe void SetPropertyExtension(this IScintillaApi scintilla, string name, string? value)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        var nameBytes = HelpersGeneral.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
        var valueBytes = HelpersGeneral.GetBytes(value ?? string.Empty, Encoding.ASCII, zeroTerminated: true);

        fixed (byte* nb = nameBytes)
        {
            fixed (byte* vb = valueBytes)
            {
                scintilla.DirectMessage(SCI_SETPROPERTY, new IntPtr(nb), new IntPtr(vb));
            }
        }
    }

    /// <summary>
    /// Marks the document as unmodified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="IScintillaProperties{TColor}.Modified" />
    public static void SetSavePointExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_SETSAVEPOINT);
    }

    /// <summary>
    /// Sets the anchor and current position.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="anchorPos">The zero-based document position to start the selection.</param>
    /// <param name="currentPos">The zero-based document position to end the selection.</param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>
    /// A negative value for <paramref name="currentPos" /> signifies the end of the document.
    /// A negative value for <paramref name="anchorPos" /> signifies no selection (i.e. sets the <paramref name="anchorPos" />
    /// to the same position as the <paramref name="currentPos" />).
    /// The current position is scrolled into view following this operation.
    /// </remarks>
    public static void SetSelExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int anchorPos, int currentPos, int textLength, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        if (anchorPos == currentPos)
        {
            // Optimization so that we don't have to translate the anchor position
            // when we can instead just pass -1 and have Scintilla handle it.
            anchorPos = -1;
        }

        if (anchorPos >= 0)
        {
            anchorPos = HelpersGeneral.Clamp(anchorPos, 0, textLength);
            anchorPos = lines.CharToBytePosition(anchorPos);
        }

        if (currentPos >= 0)
        {
            currentPos = HelpersGeneral.Clamp(currentPos, 0, textLength);
            currentPos = lines.CharToBytePosition(currentPos);
        }

        scintilla.DirectMessage(SCI_SETSEL, new IntPtr(anchorPos), new IntPtr(currentPos));
    }

    /// <summary>
    /// Sets a single selection from anchor to caret.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="caret">The zero-based document position to end the selection.</param>
    /// <param name="anchor">The zero-based document position to start the selection.</param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void SetSelectionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int caret, int anchor, int textLength, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        caret = HelpersGeneral.Clamp(caret, 0, textLength);
        anchor = HelpersGeneral.Clamp(anchor, 0, textLength);

        caret = lines.CharToBytePosition(caret);
        anchor = lines.CharToBytePosition(anchor);

        scintilla.DirectMessage(SCI_SETSELECTION, new IntPtr(caret), new IntPtr(anchor));
    }

    /// <summary>
    /// Sets a global override to the selection background color.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="use">true to override the selection background color; otherwise, false.</param>
    /// <param name="color">The global selection background color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <seealso cref="SetSelectionForeColorExtension{TColor}" />
    public static void SetSelectionBackColorExtension<TColor>(this IScintillaApi scintilla, bool use, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        var useSelectionForeColor = use ? new IntPtr(1) : IntPtr.Zero;

        scintilla.DirectMessage(SCI_SETSELBACK, useSelectionForeColor, new IntPtr(intColor));
    }

    /// <summary>
    /// Sets a global override to the selection foreground color.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="use">true to override the selection foreground color; otherwise, false.</param>
    /// <param name="color">The global selection foreground color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <seealso cref="SetSelectionBackColorExtension{TColor}" />
    public static void SetSelectionForeColorExtension<TColor>(this IScintillaApi scintilla, bool use, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        var useSelectionForeColor = use ? new IntPtr(1) : IntPtr.Zero;

        scintilla.DirectMessage(SCI_SETSELFORE, useSelectionForeColor, new IntPtr(intColor));
    }

    /// <summary>
    /// Styles the specified length of characters.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="length">The number of characters to style.</param>
    /// <param name="style">The <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" /> definition index to assign each character.</param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="stylingPosition">Private state of the Scintilla wrapper related to styling.</param>
    /// <param name="stylingBytePosition">Private state of the Scintilla wrapper related to styling.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <param name="styles">The style collection of the Scintilla control.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="length" /> or <paramref name="style" /> is less than zero. -or-
    /// The sum of a preceding call to <see cref="StartStylingExtension" /> or <see name="SetStylingExtension" /> and <paramref name="length" /> is greater than the document length. -or-
    /// <paramref name="style" /> is greater than or equal to the number of style definitions.
    /// </exception>
    /// <remarks>
    /// The styling position is advanced by <paramref name="length" /> after each call allowing multiple
    /// calls to <see cref="SetStylingExtension" /> for a single call to <see cref="StartStylingExtension" />.
    /// </remarks>
    /// <seealso cref="StartStylingExtension" />
    // ReSharper disable three times ParameterOnlyUsedForPreconditionCheck.Global
    public static void SetStylingExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int length, int style, int textLength,
        ref int stylingPosition, ref int stylingBytePosition, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines, IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> styles)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length cannot be less than zero.");
        }

        if (stylingPosition + length > textLength)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Position and length must refer to a range within the document.");
        }

        if (style < 0 || style >= styles.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(style), "Style must be non-negative and less than the size of the collection.");
        }

        var endPos = stylingPosition + length;
        var endBytePos = lines.CharToBytePosition(endPos);
        scintilla.DirectMessage(SCI_SETSTYLING, new IntPtr(endBytePos - stylingBytePosition), new IntPtr(style));

        // Track this for the next call
        stylingPosition = endPos;
        stylingBytePosition = endBytePos;
    }

    /// <summary>
    /// Sets the <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> properties in a single call.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="start">The zero-based character position within the document to start a search or replace operation.</param>
    /// <param name="end">The zero-based character position within the document to end a search or replace operation.</param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <seealso cref="IScintillaProperties{TColor}.TargetStart" />
    /// <seealso cref="IScintillaProperties{TColor}.TargetEnd" />
    public static void SetTargetRangeExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int start, int end, int textLength, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        start = HelpersGeneral.Clamp(start, 0, textLength);
        end = HelpersGeneral.Clamp(end, 0, textLength);

        start = lines.CharToBytePosition(start);
        end = lines.CharToBytePosition(end);

        scintilla.DirectMessage(SCI_SETTARGETRANGE, new IntPtr(start), new IntPtr(end));
    }

    /// <summary>
    /// Sets a global override to the whitespace background color.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="use">true to override the whitespace background color; otherwise, false.</param>
    /// <param name="color">The global whitespace background color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <remarks>When not overridden globally, the whitespace background color is determined by the current lexer.</remarks>
    /// <seealso cref="IScintillaProperties{TColor}.ViewWhitespace" />
    /// <seealso cref="SetWhitespaceForeColorExtension{T}" />
    public static void SetWhitespaceBackColorExtension<TColor>(this IScintillaApi scintilla, bool use, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        var useWhitespaceBackColor = use ? new IntPtr(1) : IntPtr.Zero;

        scintilla.DirectMessage(SCI_SETWHITESPACEBACK, useWhitespaceBackColor, new IntPtr(intColor));
    }

    /// <summary>
    /// Sets a global override to the whitespace foreground color.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="use">true to override the whitespace foreground color; otherwise, false.</param>
    /// <param name="color">The global whitespace foreground color.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    /// <remarks>When not overridden globally, the whitespace foreground color is determined by the current lexer.</remarks>
    /// <seealso cref="IScintillaProperties{TColor}.ViewWhitespace" />
    /// <seealso cref="SetWhitespaceBackColorExtension{T}" />
    public static void SetWhitespaceForeColorExtension<TColor>(this IScintillaApi scintilla, bool use, TColor color, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var intColor = colorToIntFunc(color);
        var useWhitespaceForeColor = use ? new IntPtr(1) : IntPtr.Zero;

        scintilla.DirectMessage(SCI_SETWHITESPACEFORE, useWhitespaceForeColor, new IntPtr(intColor));
    }

    /// <summary>
    /// Shows the range of lines specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="lineStart">The zero-based index of the line range to start showing.</param>
    /// <param name="lineEnd">The zero-based index of the line range to end showing.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <seealso cref="HideLinesExtension" />
    /// <seealso cref="LineBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Visible" />
    public static void ShowLinesExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int lineStart, int lineEnd, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        lineStart = HelpersGeneral.Clamp(lineStart, 0, lines.Count);
        lineEnd = HelpersGeneral.Clamp(lineEnd, lineStart, lines.Count);

        scintilla.DirectMessage(SCI_SHOWLINES, new IntPtr(lineStart), new IntPtr(lineEnd));
    }

    /// <summary>
    /// Prepares for styling by setting the styling <paramref name="position" /> to start at.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based character position in the document to start styling.</param>
    /// <param name="stylingPosition">Private state of the Scintilla wrapper related to styling.</param>
    /// <param name="stylingBytePosition">Private state of the Scintilla wrapper related to styling.</param>
    /// <param name="textLength">The total length of the text in the Scintilla control.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <remarks>
    /// After preparing the document for styling, use successive calls to <see cref="SetStylingExtension" />
    /// to style the document.
    /// </remarks>
    /// <seealso cref="SetStylingExtension" />
    public static void StartStylingExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, out int stylingPosition,
        out int stylingBytePosition, int textLength, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        position = HelpersGeneral.Clamp(position, 0, textLength);
        var pos = lines.CharToBytePosition(position);
        scintilla.DirectMessage(SCI_STARTSTYLING, new IntPtr(pos));

        // Track this so we can validate calls to SetStyling
        stylingPosition = position;
        stylingBytePosition = pos;
    }

    /// <summary>
    /// Resets all style properties to those currently configured for the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Default" /> style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="StyleResetDefaultExtension" />
    public static void StyleClearAllExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_STYLECLEARALL);
    }

    /// <summary>
    /// Resets the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.Default" /> style to its initial state.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="StyleClearAllExtension" />
    public static void StyleResetDefaultExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_STYLERESETDEFAULT);
    }

    /// <summary>
    /// Moves the caret to the opposite end of the main selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void SwapMainAnchorCaretExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_SWAPMAINANCHORCARET);
    }

    /// <summary>
    /// Sets the <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> to the start and end positions of the selection.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="TargetWholeDocumentExtension" />
    public static void TargetFromSelectionExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_TARGETFROMSELECTION);
    }

    /// <summary>
    /// Sets the <see cref="IScintillaProperties{TColor}.TargetStart" /> and <see cref="IScintillaProperties{TColor}.TargetEnd" /> to the start and end positions of the document.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="TargetFromSelectionExtension" />
    public static void TargetWholeDocumentExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_TARGETWHOLEDOCUMENT);
    }

    /// <summary>
    /// Measures the width in pixels of the specified string when rendered in the specified style.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="style">The index of the <see cref="StyleBase{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" /> to use when rendering the text to measure.</param>
    /// <param name="text">The text to measure.</param>
    /// <param name="styles">The style collection of the Scintilla control.</param>
    /// <returns>The width in pixels.</returns>
    public static unsafe int TextWidthExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int style, string? text, IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> styles)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        style = HelpersGeneral.Clamp(style, 0, styles.Count - 1);
        var bytes = HelpersGeneral.GetBytes(text ?? string.Empty, scintilla.Encoding, zeroTerminated: true);

        fixed (byte* bp = bytes)
        {
            return scintilla.DirectMessage(SCI_TEXTWIDTH, new IntPtr(style), new IntPtr(bp)).ToInt32();
        }
    }

    /// <summary>
    /// Undoes the previous action.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    public static void UndoExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_UNDO);
    }

    /// <summary>
    /// Determines whether to show the right-click context menu.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="enablePopup">true to enable the popup window; otherwise, false.</param>
    /// <seealso cref="UsePopupExtension(IScintillaApi, PopupMode)" />
    public static void UsePopupExtension(this IScintillaApi scintilla, bool enablePopup)
    {
        // NOTE: The behavior of UsePopup has changed in v3.7.1, however, this approach is still valid
        var bEnablePopup = enablePopup ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_USEPOPUP, bEnablePopup);
    }

    /// <summary>
    /// Determines the conditions for displaying the standard right-click context menu.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="popupMode">One of the <seealso cref="PopupMode" /> enumeration values.</param>
    public static void UsePopupExtension(this IScintillaApi scintilla, PopupMode popupMode)
    {
        scintilla.DirectMessage(SCI_USEPOPUP, new IntPtr((int)popupMode));
    }

    /// <summary>
    /// Returns the position where a word ends, searching forward from the position specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position to start searching from.</param>
    /// <param name="onlyWordCharacters">
    /// true to stop searching at the first non-word character regardless of whether the search started at a word or non-word character.
    /// false to use the first character in the search as a word or non-word indicator and then search for that word or non-word boundary.
    /// </param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document position of the word boundary.</returns>
    /// <seealso cref="WordStartPositionExtension" />
    public static int WordEndPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, bool onlyWordCharacters, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var onlyWordChars = onlyWordCharacters ? new IntPtr(1) : IntPtr.Zero;
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);
        position = scintilla.DirectMessage(SCI_WORDENDPOSITION, new IntPtr(position), onlyWordChars).ToInt32();
        return lines.ByteToCharPosition(position);
    }

    /// <summary>
    /// Returns the position where a word starts, searching backward from the position specified.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="position">The zero-based document position to start searching from.</param>
    /// <param name="onlyWordCharacters">
    /// true to stop searching at the first non-word character regardless of whether the search started at a word or non-word character.
    /// false to use the first character in the search as a word or non-word indicator and then search for that word or non-word boundary.
    /// </param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns>The zero-based document position of the word boundary.</returns>
    /// <seealso cref="WordEndPositionExtension" />
    public static int WordStartPositionExtension<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>(this IScintillaApi scintilla, int position, bool onlyWordCharacters, IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> lines)
        where TMarkers : IScintillaMarkerCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TStyles : IScintillaStyleCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TIndicators :IScintillaIndicatorCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TLines : IScintillaLineCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMargins : IScintillaMarginCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TSelections : IScintillaSelectionCollection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
        where TMarker: IScintillaMarker<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TStyle : IScintillaStyle<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TIndicator : IScintillaIndicator<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TLine : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TMargin : IScintillaMargin<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TSelection : IScintillaSelection<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
        where TBitmap: class
        where TColor: struct
    {
        var onlyWordChars = onlyWordCharacters ? new IntPtr(1) : IntPtr.Zero;
        position = HelpersGeneral.Clamp(position, 0, scintilla.TextLength);
        position = lines.CharToBytePosition(position);
        position = scintilla.DirectMessage(SCI_WORDSTARTPOSITION, new IntPtr(position), onlyWordChars).ToInt32();
        return lines.ByteToCharPosition(position);
    }

    /// <summary>
    /// Increases the zoom factor by 1 until it reaches 20 points.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="IScintillaProperties{TColor}.Zoom" />
    public static void ZoomInExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_ZOOMIN);
    }

    /// <summary>
    /// Decreases the zoom factor by 1 until it reaches -10 points.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <seealso cref="IScintillaProperties{TColor}.Zoom" />
    public static void ZoomOutExtension(this IScintillaApi scintilla)
    {
        scintilla.DirectMessage(SCI_ZOOMOUT);
    }

    /// <summary>
    /// Sets the representation for a specified character string.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="encodedString">The encoded string. I.e. the Ohm character: Ω = \u2126.</param>
    /// <param name="representationString">The representation string for the <paramref name="encodedString"/>. I.e. "OHM".</param>
    /// <remarks>The <see cref="IScintillaProperties{TColor}.ViewWhitespace"/> must be set to <see cref="WhitespaceMode.VisibleAlways"/> for this to work.</remarks>
    public static unsafe void SetRepresentationExtension(this IScintillaApi scintilla, string encodedString, string representationString)
    {
        var bytesEncoded = HelpersGeneral.GetBytes(encodedString, scintilla.Encoding, zeroTerminated: true);
        var bytesRepresentation = HelpersGeneral.GetBytes(representationString, scintilla.Encoding, zeroTerminated: true);
        fixed (byte* bpEncoded = bytesEncoded)
        {
            fixed (byte* bpRepresentation = bytesRepresentation)
            {
                scintilla.DirectMessage(SCI_SETREPRESENTATION, new IntPtr(bpEncoded), new IntPtr(bpRepresentation));
            }
        }
    }

    /// <summary>
    /// Sets the representation for a specified character string.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="encodedString">The encoded string. I.e. the Ohm character: Ω = \u2126.</param>
    /// <returns>The representation string for the <paramref name="encodedString"/>. I.e. "OHM".</returns>
    public static unsafe string GetRepresentationExtension(this IScintillaApi scintilla, string encodedString)
    {
        var bytesEncoded = HelpersGeneral.GetBytes(encodedString, scintilla.Encoding, zeroTerminated: true);

        fixed (byte* bpEncoded = bytesEncoded)
        {
            var length = scintilla.DirectMessage(SCI_GETREPRESENTATION, new IntPtr(bpEncoded), IntPtr.Zero)
                .ToInt32();
            var bytesRepresentation = new byte[length + 1];
            fixed (byte* bpRepresentation = bytesRepresentation)
            {
                scintilla.DirectMessage(SCI_GETREPRESENTATION, new IntPtr(bpEncoded), new IntPtr(bpRepresentation));
                return HelpersGeneral.GetString(new IntPtr(bpRepresentation), length, scintilla.Encoding);
            }
        }
    }

    /// <summary>
    /// Clears the representation from a specified character string.
    /// </summary>
    /// <param name="scintilla">A reference to the control implementing the <see cref="IScintillaApi"/>.</param>
    /// <param name="encodedString">The encoded string. I.e. the Ohm character: Ω = \u2126.</param>
    public static unsafe void ClearRepresentationExtension(this IScintillaApi scintilla, string encodedString)
    {
        var bytesEncoded = HelpersGeneral.GetBytes(encodedString, scintilla.Encoding, zeroTerminated: true);
        fixed (byte* bpEncoded = bytesEncoded)
        {
            scintilla.DirectMessage(SCI_CLEARREPRESENTATION, new IntPtr(bpEncoded), IntPtr.Zero);
        }
    }
}