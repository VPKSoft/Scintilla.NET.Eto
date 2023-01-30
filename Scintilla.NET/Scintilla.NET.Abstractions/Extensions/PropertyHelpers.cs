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

using System.Text;
using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Structs;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Extensions;

/// <summary>
/// Helper and extension methods for the <see cref="IScintillaApi{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TEventArgs,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}"/> property value implementation.
/// </summary>
public static class PropertyHelpers
{
    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.BiDirectionality"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="BiDirectionalDisplayType" />.</returns>
    public static BiDirectionalDisplayType BiDirectionalityGet(this IScintillaApi scintilla) =>
        (BiDirectionalDisplayType)scintilla.DirectMessage(SCI_GETBIDIRECTIONAL).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.BiDirectionality"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void BiDirectionalitySet(this IScintillaApi scintilla, BiDirectionalDisplayType value)
    {
        if (value != BiDirectionalDisplayType.Disabled)
        {
            var technology = scintilla.DirectMessage(SCI_GETTECHNOLOGY).ToInt32();
            if (technology == SC_TECHNOLOGY_DEFAULT)
            {
                scintilla.DirectMessage(SCI_SETTECHNOLOGY, new IntPtr(SC_TECHNOLOGY_DIRECTWRITE));
            }
        }

        scintilla.DirectMessage(SCI_SETBIDIRECTIONAL, new IntPtr((int)value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AdditionalCaretForeColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="intToColorFunc">A delegate to a function to translate the ARGB integer value to platform-depended color value.</param>
    /// <returns>A value of type of <typeparamref name="TColor"/>.</returns>
    public static TColor AdditionalCaretForeColorGet<TColor>(this IScintillaApi scintilla,
        Func<int, TColor> intToColorFunc)
        where TColor : struct
    {
        var color = scintilla.DirectMessage(SCI_GETADDITIONALCARETFORE).ToInt32();
        return intToColorFunc(color);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AdditionalCaretForeColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    public static void AdditionalCaretForeColorSet<TColor>(this IScintillaApi scintilla, TColor value,
        Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var color = colorToIntFunc(value);
        scintilla.DirectMessage(SCI_SETADDITIONALCARETFORE, new IntPtr(color));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AdditionalCaretsBlink"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AdditionalCaretsBlinkGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETADDITIONALCARETSBLINK) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AdditionalCaretsBlink"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AdditionalCaretsBlinkSet(this IScintillaApi scintilla, bool value)
    {
        var additionalCaretsBlink = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETADDITIONALCARETSBLINK, additionalCaretsBlink);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AdditionalCaretsVisible"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AdditionalCaretsVisibleGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETADDITIONALCARETSVISIBLE) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AdditionalCaretsVisible"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AdditionalCaretsVisibleSet(this IScintillaApi scintilla, bool value)
    {
        var additionalCaretsBlink = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETADDITIONALCARETSVISIBLE, additionalCaretsBlink);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AdditionalSelAlpha"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int AdditionalSelAlphaGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETADDITIONALSELALPHA).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AdditionalSelAlpha"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AdditionalSelAlphaSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.Clamp(value, 0, SC_ALPHA_NOALPHA);
        scintilla.DirectMessage(SCI_SETADDITIONALSELALPHA, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AdditionalSelectionTyping"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AdditionalSelectionTypingGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETADDITIONALSELECTIONTYPING) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AdditionalSelectionTyping"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AdditionalSelectionTypingSet(this IScintillaApi scintilla, bool value)
    {
        var additionalSelectionTyping = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETADDITIONALSELECTIONTYPING, additionalSelectionTyping);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AnchorPosition"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static int AnchorPositionGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var bytePos = scintilla.DirectMessage(SCI_GETANCHOR).ToInt32();
        return lines.ByteToCharPosition(bytePos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AnchorPosition"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void AnchorPositionSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        var bytePos = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETANCHOR, new IntPtr(bytePos));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AnnotationVisible"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Annotation" />.</returns>
    public static Annotation AnnotationVisibleGet(this IScintillaApi scintilla) =>
        (Annotation)scintilla.DirectMessage(SCI_ANNOTATIONGETVISIBLE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AnnotationVisible"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AnnotationVisibleSet(this IScintillaApi scintilla, Annotation value)
    {
        var visible = (int)value;
        scintilla.DirectMessage(SCI_ANNOTATIONSETVISIBLE, new IntPtr(visible));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCActive"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AutoCActiveGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCACTIVE) != IntPtr.Zero;

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCAutoHide"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AutoCAutoHideGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETAUTOHIDE) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCAutoHide"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCAutoHideSet(this IScintillaApi scintilla, bool value)
    {
        var autoHide = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_AUTOCSETAUTOHIDE, autoHide);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCCancelAtStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AutoCCancelAtStartGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETCANCELATSTART) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCCancelAtStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCCancelAtStartSet(this IScintillaApi scintilla, bool value)
    {
        var cancel = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_AUTOCSETCANCELATSTART, cancel);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCCurrent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int AutoCCurrentGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETCURRENT).ToInt32();


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCChooseSingle"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AutoCChooseSingleGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETCHOOSESINGLE) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCChooseSingle"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCChooseSingleSet(this IScintillaApi scintilla, bool value)
    {
        var chooseSingle = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_AUTOCSETCHOOSESINGLE, chooseSingle);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCDropRestOfWord"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AutoCDropRestOfWordGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETDROPRESTOFWORD) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCDropRestOfWord"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCDropRestOfWordSet(this IScintillaApi scintilla, bool value)
    {
        var dropRestOfWord = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_AUTOCSETDROPRESTOFWORD, dropRestOfWord);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCIgnoreCase"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool AutoCIgnoreCaseGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETIGNORECASE) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCIgnoreCase"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCIgnoreCaseSet(this IScintillaApi scintilla, bool value)
    {
        var ignoreCase = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_AUTOCSETIGNORECASE, ignoreCase);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCMaxHeight"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int AutoCMaxHeightGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETMAXHEIGHT).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCMaxHeight"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCMaxHeightSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_AUTOCSETMAXHEIGHT, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCMaxWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int AutoCMaxWidthGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_AUTOCGETMAXWIDTH).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCMaxWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCMaxWidthSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_AUTOCSETMAXWIDTH, new IntPtr(value));
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCOrder"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Order" />.</returns>
    public static Order AutoCOrderGet(this IScintillaApi scintilla) =>
        (Order)scintilla.DirectMessage(SCI_AUTOCGETORDER).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCOrder"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCOrderSet(this IScintillaApi scintilla, Order value)
    {
        var order = (int)value;
        scintilla.DirectMessage(SCI_AUTOCSETORDER, new IntPtr(order));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCPosStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int AutoCPosStartGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var pos = scintilla.DirectMessage(SCI_AUTOCPOSSTART).ToInt32();
        pos = lines.ByteToCharPosition(pos);

        return pos;
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCSeparator"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="char" />.</returns>
    public static char AutoCSeparatorGet(this IScintillaApi scintilla)
    {
        var separator = scintilla.DirectMessage(SCI_AUTOCGETSEPARATOR).ToInt32();
        return (char)separator;
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCSeparator"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCSeparatorSet(this IScintillaApi scintilla, char value)
    {
        // The auto-completion separator character is stored as a byte within Scintilla,
        // not a character. Thus it's possible for a user to supply a character that does
        // not fit within a single byte. The likelyhood of this, however, seems so remote that
        // I'm willing to risk a possible conversion error to provide a better user experience.
        var separator = (byte)value;
        scintilla.DirectMessage(SCI_AUTOCSETSEPARATOR, new IntPtr(separator));
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutoCTypeSeparator"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="char" />.</returns>
    public static char AutoCTypeSeparatorGet(this IScintillaApi scintilla)
    {
        var separatorCharacter = scintilla.DirectMessage(SCI_AUTOCGETTYPESEPARATOR).ToInt32();
        return (char)separatorCharacter;
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutoCTypeSeparator"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutoCTypeSeparatorSet(this IScintillaApi scintilla, char value)
    {
        // The auto-completion type separator character is stored as a byte within Scintilla,
        // not a character. Thus it's possible for a user to supply a character that does
        // not fit within a single byte. The likelyhood of this, however, seems so remote that
        // I'm willing to risk a possible conversion error to provide a better user experience.
        var separatorCharacter = (byte)value;
        scintilla.DirectMessage(SCI_AUTOCSETTYPESEPARATOR, new IntPtr(separatorCharacter));
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.AutomaticFold"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="AutomaticFold" />.</returns>
    public static AutomaticFold AutomaticFoldGet(this IScintillaApi scintilla) => (AutomaticFold)scintilla.DirectMessage(SCI_GETAUTOMATICFOLD);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.AutomaticFold"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void AutomaticFoldSet(this IScintillaApi scintilla, AutomaticFold value)
    {
        var automaticFold = (int)value;
        scintilla.DirectMessage(SCI_SETAUTOMATICFOLD, new IntPtr(automaticFold));
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.BackspaceUnIndents"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool BackspaceUnIndentsGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETBACKSPACEUNINDENTS) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.BackspaceUnIndents"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void BackspaceUnIndentsSet(this IScintillaApi scintilla, bool value)
    {
        var ptr = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETBACKSPACEUNINDENTS, ptr);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.BufferedDraw"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool BufferedDrawGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_GETBUFFEREDDRAW) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.BufferedDraw"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void BufferedDrawSet(this IScintillaApi scintilla, bool value)
    {
        var isBuffered = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETBUFFEREDDRAW, isBuffered);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CallTipActive"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool CallTipActiveGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_CALLTIPACTIVE) != IntPtr.Zero;

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CanPaste"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool CanPasteGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_CANPASTE) != IntPtr.Zero;

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CanRedo"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool CanRedoGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_CANREDO) != IntPtr.Zero;

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CanUndo"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool CanUndoGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_CANUNDO) != IntPtr.Zero;


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretForeColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="intToColorFunc">A delegate to a function to translate the ARGB integer value to platform-depended color value.</param>
    /// <returns>A value of type of <typeparamref name="TColor"/>.</returns>
    public static TColor CaretForeColorGet<TColor>(this IScintillaApi scintilla, Func<int, TColor> intToColorFunc)
        where TColor : struct
    {
        var color = scintilla.DirectMessage(SCI_GETCARETFORE).ToInt32();
        return intToColorFunc(color);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretForeColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    public static void CaretForeColorSet<TColor>(this IScintillaApi scintilla, TColor value, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var color = colorToIntFunc(value);
        scintilla.DirectMessage(SCI_SETCARETFORE, new IntPtr(color));
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretLineBackColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="intToColorFunc">A delegate to a function to translate the ARGB integer value to platform-depended color value.</param>
    /// <returns>A value of type of <typeparamref name="TColor"/>.</returns>
    public static TColor CaretLineBackColorGet<TColor>(this IScintillaApi scintilla, Func<int, TColor> intToColorFunc)
        where TColor : struct
    {
        var color = scintilla.DirectMessage(SCI_GETCARETLINEBACK).ToInt32();
        return intToColorFunc(color);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretLineBackColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    public static void CaretLineBackColorSet<TColor>(this IScintillaApi scintilla, TColor value, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var color = colorToIntFunc(value);
        scintilla.DirectMessage(SCI_SETCARETLINEBACK, new IntPtr(color));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretLineBackColorAlpha"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int CaretLineBackColorAlphaGet(this IScintillaApi scintilla)  => scintilla.DirectMessage(SCI_GETCARETLINEBACKALPHA).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretLineBackColorAlpha"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretLineBackColorAlphaSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.Clamp(value, 0, SC_ALPHA_NOALPHA);
        scintilla.DirectMessage(SCI_SETCARETLINEBACKALPHA, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretLineFrame"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int CaretLineFrameGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETCARETLINEFRAME).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretLineFrame"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretLineFrameSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETCARETLINEFRAME, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretLineVisible"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool CaretLineVisibleGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETCARETLINEVISIBLE) != IntPtr.Zero; 

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretLineVisible"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretLineVisibleSet(this IScintillaApi scintilla, bool value)
    {
        var visible = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETCARETLINEVISIBLE, visible);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretLineVisibleAlways"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool CaretLineVisibleAlwaysGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETCARETLINEVISIBLEALWAYS) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretLineVisibleAlways"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretLineVisibleAlwaysSet(this IScintillaApi scintilla, bool value)
    {
        var visibleAlways = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETCARETLINEVISIBLEALWAYS, visibleAlways);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretLineLayer"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Layer" />.</returns>
    public static Layer CaretLineLayerGet(this IScintillaApi scintilla) =>
        (Layer)scintilla.DirectMessage(SCI_GETCARETLINELAYER).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretLineLayer"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretLineLayerSet(this IScintillaApi scintilla, Layer value)
    {
        var layer = (int)value;
        scintilla.DirectMessage(SCI_SETCARETLINELAYER, new IntPtr(layer));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretPeriod"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int CaretPeriodGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETCARETPERIOD).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretPeriod"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretPeriodSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETCARETPERIOD, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretStyle"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="CaretStyle" />.</returns>
    public static CaretStyle CaretStyleGet(this IScintillaApi scintilla) =>
        (CaretStyle)scintilla.DirectMessage(SCI_GETCARETSTYLE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretStyle"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretStyleSet(this IScintillaApi scintilla, CaretStyle value)
    {
        var style = (int)value;
        scintilla.DirectMessage(SCI_SETCARETSTYLE, new IntPtr(style));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CaretWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int CaretWidthGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_GETCARETWIDTH).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CaretWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void CaretWidthSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.Clamp(value, 0, 3);
        scintilla.DirectMessage(SCI_SETCARETWIDTH, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CurrentLine"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int CurrentLineGet(this IScintillaApi scintilla)
    {
        var currentPos = scintilla.DirectMessage(SCI_GETCURRENTPOS).ToInt32();
        var line = scintilla.DirectMessage(SCI_LINEFROMPOSITION, new IntPtr(currentPos)).ToInt32();
        return line;
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.CurrentPosition"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int CurrentPositionGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var bytePos = scintilla.DirectMessage(SCI_GETCURRENTPOS).ToInt32();
        return lines.ByteToCharPosition(bytePos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.CurrentPosition"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void CurrentPositionSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        var bytePos = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETCURRENTPOS, new IntPtr(bytePos));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.DistanceToSecondaryStyles"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int DistanceToSecondaryStylesGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_DISTANCETOSECONDARYSTYLES).ToInt32();

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Document"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Document" />.</returns>
    public static Document DocumentGet(this IScintillaApi scintilla)
    {
        var ptr = scintilla.DirectMessage(SCI_GETDOCPOINTER);
        return new Document { Value = ptr };
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.Document"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <param name="eolMode">The <see cref="IScintillaProperties{TColor}.EolMode"/> property value.</param>
    /// <param name="useTabs">The <see cref="IScintillaProperties{TColor}.UseTabs"/> property value.</param>
    /// <param name="tabWidth">The <see cref="IScintillaProperties{TColor}.TabWidth"/> property value.</param>
    /// <param name="indentWidth">The <see cref="IScintillaProperties{TColor}.IndentWidth"/> property value.</param>
    public static void DocumentSet(this IScintillaApi scintilla, Document value, ILineCollection lines, Eol eolMode, bool useTabs, int tabWidth, int indentWidth)
    {
        var ptr = value.Value;
        scintilla.DirectMessage(SCI_SETDOCPOINTER, IntPtr.Zero, ptr);

        // Carry over properties to new document
        scintilla.InitDocument(eolMode, useTabs, tabWidth, indentWidth);

        // Rebuild the line cache
        lines.RebuildLineData();
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.EdgeColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="intToColorFunc">A delegate to a function to translate the ARGB integer value to platform-depended color value.</param>
    /// <returns>A value of type of <typeparamref name="TColor"/>.</returns>
    public static TColor EdgeColorGet<TColor>(this IScintillaApi scintilla, Func<int, TColor> intToColorFunc)
        where TColor : struct
    {
        var color = scintilla.DirectMessage(SCI_GETEDGECOLOUR).ToInt32();
        return intToColorFunc(color);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.EdgeColor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="colorToIntFunc">A delegate to a function to translate the platform-depended color into ARGB integer value.</param>
    public static void EdgeColorSet<TColor>(this IScintillaApi scintilla, TColor value, Func<TColor, int> colorToIntFunc)
        where TColor : struct
    {
        var color = colorToIntFunc(value);
        scintilla.DirectMessage(SCI_SETEDGECOLOUR, new IntPtr(color));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.EdgeColumn"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int EdgeColumnGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETEDGECOLUMN).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.EdgeColumn"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void EdgeColumnSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETEDGECOLUMN, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.EdgeMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="EdgeMode" />.</returns>
    public static EdgeMode EdgeModeGet(this IScintillaApi scintilla) =>
        (EdgeMode)scintilla.DirectMessage(SCI_GETEDGEMODE);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.EdgeMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void EdgeModeSet(this IScintillaApi scintilla, EdgeMode value)
    {
        var edgeMode = (int)value;
        scintilla.DirectMessage(SCI_SETEDGEMODE, new IntPtr(edgeMode));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaApi.Encoding"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Encoding" />.</returns>
    public static Encoding EncodingGet(this IScintillaApi scintilla)
    {
        // Should always be UTF-8 unless someone has done an end run around us
        var codePage = (int)scintilla.DirectMessage(SCI_GETCODEPAGE);
        return codePage == 0 ? Encoding.Default : Encoding.GetEncoding(codePage);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.EndAtLastLine"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool EndAtLastLineGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_GETENDATLASTLINE) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.EndAtLastLine"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void EndAtLastLineSet(this IScintillaApi scintilla, bool value)
    {
        var endAtLastLine = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETENDATLASTLINE, endAtLastLine);
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.EolMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Eol" />.</returns>
    public static Eol EolModeGet(this IScintillaApi scintilla) => (Eol)scintilla.DirectMessage(SCI_GETEOLMODE);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.EolMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void EolModeSet(this IScintillaApi scintilla, Eol value)
    {
        var eolMode = (int)value;
        scintilla.DirectMessage(SCI_SETEOLMODE, new IntPtr(eolMode));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ExtraAscent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int ExtraAscentGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETEXTRAASCENT).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ExtraAscent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ExtraAscentSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETEXTRAASCENT, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ExtraDescent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int ExtraDescentGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETEXTRADESCENT).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ExtraDescent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ExtraDescentSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETEXTRADESCENT, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.FirstVisibleLine"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int FirstVisibleLineGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETFIRSTVISIBLELINE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.FirstVisibleLine"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void FirstVisibleLineSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETFIRSTVISIBLELINE, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.FontQuality"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="FontQuality" />.</returns>
    public static FontQuality FontQualityGet(this IScintillaApi scintilla) =>
        (FontQuality)scintilla.DirectMessage(SCI_GETFONTQUALITY);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.FontQuality"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void FontQualitySet(this IScintillaApi scintilla, FontQuality value)
    {
        var fontQuality = (int)value;
        scintilla.DirectMessage(SCI_SETFONTQUALITY, new IntPtr(fontQuality));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.HighlightGuide"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int HighlightGuideGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_GETHIGHLIGHTGUIDE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.HighlightGuide"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void HighlightGuideSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETHIGHLIGHTGUIDE, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.HScrollBar"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool HScrollBarGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETHSCROLLBAR) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.HScrollBar"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void HScrollBarSet(this IScintillaApi scintilla, bool value)
    {
        var visible = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETHSCROLLBAR, visible);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.IdleStyling"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="IdleStyling" />.</returns>
    public static IdleStyling IdleStylingGet(this IScintillaApi scintilla) =>
        (IdleStyling)scintilla.DirectMessage(SCI_GETIDLESTYLING);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.IdleStyling"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void IdleStylingSet(this IScintillaApi scintilla, IdleStyling value)
    {
        var idleStyling = (int)value;
        scintilla.DirectMessage(SCI_SETIDLESTYLING, new IntPtr(idleStyling));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.IndentWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int IndentWidthGet(this IScintillaApi scintilla) => 
        scintilla.DirectMessage(SCI_GETINDENT).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.IndentWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void IndentWidthSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETINDENT, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.IndentationGuides"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="IndentView" />.</returns>
    public static IndentView IndentationGuidesGet(this IScintillaApi scintilla) => (IndentView)scintilla.DirectMessage(SCI_GETINDENTATIONGUIDES);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.IndentationGuides"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void IndentationGuidesSet(this IScintillaApi scintilla, IndentView value)
    {
        var indentView = (int)value;
        scintilla.DirectMessage(SCI_SETINDENTATIONGUIDES, new IntPtr(indentView));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.IndicatorCurrent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int IndicatorCurrentGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETINDICATORCURRENT).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.IndicatorCurrent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="indicators">The indicator collection of the Scintilla control.</param>
    public static void IndicatorCurrentSet(this IScintillaApi scintilla, int value, IIndicatorCollection indicators)
    {
        value = HelpersGeneral.Clamp(value, 0, indicators.Count - 1);
        scintilla.DirectMessage(SCI_SETINDICATORCURRENT, new IntPtr(value));
    }


    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.IndicatorValue"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int IndicatorValueGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETINDICATORVALUE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.IndicatorValue"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void IndicatorValueSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETINDICATORVALUE, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.InternalFocusFlag"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool InternalFocusFlagGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETFOCUS) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.InternalFocusFlag"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void InternalFocusFlagSet(this IScintillaApi scintilla, bool value)
    {
        var focus = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETFOCUS, focus);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.LexerName"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lexerName">The private lexer name field value.</param>
    /// <returns><see cref="string" />.</returns>
    public static string? LexerNameGet(this IScintillaApi scintilla, string? lexerName) => lexerName;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.LexerName"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lexilla">The Lexilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lexerName">The private lexer name field reference value.</param>
    public static void LexerNameSet(this IScintillaApi scintilla, ILexilla lexilla, string? value, ref string? lexerName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            lexerName = value;

            return;
        }

        if (!SetLexerByName(scintilla, lexilla, value))
        {
            throw new InvalidOperationException(@$"Lexer with the name of '{value}' was not found.");
        }

        lexerName = value;
    }

    /// <summary>
    /// Sets the name of the lexer by its name.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lexilla">The Lexilla instance.</param>
    /// <param name="name">Name of the lexer.</param>
    /// <returns><c>true</c> if the lexer was successfully set, <c>false</c> otherwise.</returns>
    // ReSharper disable once MemberCanBePrivate.Global, Part of a helper class
    public static bool SetLexerByName(IScintillaApi scintilla, ILexilla lexilla, string? name)
    {
        if (name == null)
        {
            return false;
        }

        var ptr = lexilla.CreateLexer(name);

        if (ptr == IntPtr.Zero)
        {
            return false;
        }

        scintilla.DirectMessage(SCI_SETILEXER, IntPtr.Zero, ptr);

        return true;
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Lexer"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lexerName">The private lexer name field value.</param>
    /// <returns><see cref="Lexer" />.</returns>
    public static Lexer LexerGet(this IScintillaApi scintilla, string? lexerName)
    {
        if (string.IsNullOrWhiteSpace(lexerName))
        {
            return Lexer.NotFound;
        }


        if (NameConstantMap.ContainsValue(lexerName!))
        {
            return (Lexer) NameConstantMap.First(f => f.Value == lexerName).Key;
        }

        return Lexer.NotFound;
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.Lexer"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lexilla">The Lexilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lexerName">The private lexer name field reference value.</param>
    public static void LexerSet(this IScintillaApi scintilla, ILexilla lexilla, Lexer value, ref string? lexerName)
    {
        if (value == Lexer.NotFound)
        {
            return;
        }

        var lexer = (int)value;

        if (NameConstantMap.ContainsKey(lexer))
        {
            lexerName = NameConstantMap.First(f => f.Key == lexer).Value;

            if (string.IsNullOrEmpty(lexerName))
            {
                throw new InvalidOperationException(@"No lexer name was found with the specified value.");
            }
        }
        else
        {
            throw new InvalidOperationException(@"No lexer name was found with the specified value.");
        }

        SetLexerByName(scintilla, lexilla, lexerName);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.LexerLanguage"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="string" />.</returns>
    public static unsafe string LexerLanguageGet(this IScintillaApi scintilla)
    {
        var length = scintilla.DirectMessage(SCI_GETLEXERLANGUAGE).ToInt32();
        if (length == 0)
        {
            return string.Empty;
        }

        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_GETLEXERLANGUAGE, IntPtr.Zero, new IntPtr(bp));
            return HelpersGeneral.GetString(new IntPtr(bp), length, Encoding.ASCII);
        }
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.LexerLanguage"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static unsafe void LexerLanguageSet(this IScintillaApi scintilla, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            scintilla.DirectMessage(SCI_SETLEXERLANGUAGE, IntPtr.Zero, IntPtr.Zero);
        }
        else
        {
            var bytes = HelpersGeneral.GetBytes(value, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* bp = bytes)
            {
                scintilla.DirectMessage(SCI_SETLEXERLANGUAGE, IntPtr.Zero, new IntPtr(bp));
            }
        }
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.LineEndTypesActive"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="LineEndType" />.</returns>
    public static LineEndType LineEndTypesActiveGet(this IScintillaApi scintilla) =>
        (LineEndType)scintilla.DirectMessage(SCI_GETLINEENDTYPESACTIVE);

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.LineEndTypesAllowed"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="LineEndType" />.</returns>
    public static LineEndType LineEndTypesAllowedGet(this IScintillaApi scintilla) =>
        (LineEndType)scintilla.DirectMessage(SCI_GETLINEENDTYPESALLOWED);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.LineEndTypesAllowed"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void LineEndTypesAllowedSet(this IScintillaApi scintilla, LineEndType value)
    {
        var lineEndBitsSet = (int)value;
        scintilla.DirectMessage(SCI_SETLINEENDTYPESALLOWED, new IntPtr(lineEndBitsSet));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.LineEndTypesSupported"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="LineEndType" />.</returns>
    public static LineEndType LineEndTypesSupportedGet(this IScintillaApi scintilla) =>
        (LineEndType)scintilla.DirectMessage(SCI_GETLINEENDTYPESSUPPORTED);

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.LinesOnScreen"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int LinesOnScreenGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_LINESONSCREEN).ToInt32();

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.MainSelection"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int MainSelectionGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETMAINSELECTION).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.MainSelection"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void MainSelectionSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETMAINSELECTION, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Modified"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool ModifiedGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_GETMODIFY) != IntPtr.Zero;

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.MouseDwellTime"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int MouseDwellTimeGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETMOUSEDWELLTIME).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.MouseDwellTime"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void MouseDwellTimeSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETMOUSEDWELLTIME, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.MouseSelectionRectangularSwitch"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool MouseSelectionRectangularSwitchGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETMOUSESELECTIONRECTANGULARSWITCH) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.MouseSelectionRectangularSwitch"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void MouseSelectionRectangularSwitchSet(this IScintillaApi scintilla, bool value)
    {
        var mouseSelectionRectangularSwitch = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETMOUSESELECTIONRECTANGULARSWITCH, mouseSelectionRectangularSwitch);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.MultipleSelection"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool MultipleSelectionGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETMULTIPLESELECTION) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.MultipleSelection"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void MultipleSelectionSet(this IScintillaApi scintilla, bool value)
    {
        var multipleSelection = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETMULTIPLESELECTION, multipleSelection);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.MultiPaste"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="MultiPaste" />.</returns>
    public static MultiPaste MultiPasteGet(this IScintillaApi scintilla) =>
        (MultiPaste)scintilla.DirectMessage(SCI_GETMULTIPASTE);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.MultiPaste"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void MultiPasteSet(this IScintillaApi scintilla, MultiPaste value)
    {
        var multiPaste = (int)value;
        scintilla.DirectMessage(SCI_SETMULTIPASTE, new IntPtr(multiPaste));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.OverType"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool OverTypeGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETOVERTYPE) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.OverType"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void OverTypeSet(this IScintillaApi scintilla, bool value)
    {
        var overType = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETOVERTYPE, overType);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.PasteConvertEndings"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool PasteConvertEndingsGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETPASTECONVERTENDINGS) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.PasteConvertEndings"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void PasteConvertEndingsSet(this IScintillaApi scintilla, bool value)
    {
        var convert = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETPASTECONVERTENDINGS, convert);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.PhasesDraw"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Phases" />.</returns>
    public static Phases PhasesDrawGet(this IScintillaApi scintilla) =>
        (Phases)scintilla.DirectMessage(SCI_GETPHASESDRAW);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.PhasesDraw"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void PhasesDrawSet(this IScintillaApi scintilla, Phases value)
    {
        var phases = (int)value;
        scintilla.DirectMessage(SCI_SETPHASESDRAW, new IntPtr(phases));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ReadOnly"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool ReadOnlyGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETREADONLY) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ReadOnly"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ReadOnlySet(this IScintillaApi scintilla, bool value)
    {
        var readOnly = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETREADONLY, readOnly);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionAnchor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int RectangularSelectionAnchorGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var pos = scintilla.DirectMessage(SCI_GETRECTANGULARSELECTIONANCHOR).ToInt32();
        if (pos <= 0)
        {
            return pos;
        }

        return lines.ByteToCharPosition(pos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionAnchor"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void RectangularSelectionAnchorSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        value = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETRECTANGULARSELECTIONANCHOR, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionAnchorVirtualSpace"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int RectangularSelectionAnchorVirtualSpaceGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETRECTANGULARSELECTIONANCHORVIRTUALSPACE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionAnchorVirtualSpace"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void RectangularSelectionAnchorVirtualSpaceSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETRECTANGULARSELECTIONANCHORVIRTUALSPACE, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionCaret"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int RectangularSelectionCaretGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var pos = scintilla.DirectMessage(SCI_GETRECTANGULARSELECTIONCARET).ToInt32();
        if (pos <= 0)
        {
            return 0;
        }

        return lines.ByteToCharPosition(pos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionCaret"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void RectangularSelectionCaretSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        value = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETRECTANGULARSELECTIONCARET, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionCaretVirtualSpace"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int RectangularSelectionCaretVirtualSpaceGet(this IScintillaApi scintilla) => scintilla.DirectMessage(SCI_GETRECTANGULARSELECTIONCARETVIRTUALSPACE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.RectangularSelectionCaretVirtualSpace"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void RectangularSelectionCaretVirtualSpaceSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETRECTANGULARSELECTIONCARETVIRTUALSPACE, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.SelectionLayer"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Layer" />.</returns>
    public static Layer SelectionLayerGet(this IScintillaApi scintilla) => (Layer)scintilla.DirectMessage(SCI_GETSELECTIONLAYER).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.SelectionLayer"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void SelectionLayerSet(this IScintillaApi scintilla, Layer value)
    {
        var layer = (int)value;
        scintilla.DirectMessage(SCI_SETSELECTIONLAYER, new IntPtr(layer), IntPtr.Zero);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ScrollWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int ScrollWidthGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETSCROLLWIDTH).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ScrollWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ScrollWidthSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETSCROLLWIDTH, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ScrollWidthTracking"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool ScrollWidthTrackingGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETSCROLLWIDTHTRACKING) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ScrollWidthTracking"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ScrollWidthTrackingSet(this IScintillaApi scintilla, bool value)
    {
        var tracking = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETSCROLLWIDTHTRACKING, tracking);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.SearchFlags"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="SearchFlags" />.</returns>
    public static SearchFlags SearchFlagsGet(this IScintillaApi scintilla) =>
        (SearchFlags)scintilla.DirectMessage(SCI_GETSEARCHFLAGS).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.SearchFlags"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void SearchFlagsSet(this IScintillaApi scintilla, SearchFlags value)
    {
        var searchFlags = (int)value;
        scintilla.DirectMessage(SCI_SETSEARCHFLAGS, new IntPtr(searchFlags));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.SelectedText"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="string" />.</returns>
    public static unsafe string SelectedTextGet(this IScintillaApi scintilla)
    {
        // NOTE: For some reason the length returned by this API includes the terminating NULL
        var length = scintilla.DirectMessage(SCI_GETSELTEXT).ToInt32();

        if (length <= 0)
        {
            return string.Empty;
        }

        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_GETSELTEXT, IntPtr.Zero, new IntPtr(bp));
            return HelpersGeneral.GetString(new IntPtr(bp), length, scintilla.Encoding);
        }
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.SelectionEnd"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int SelectionEndGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var pos = scintilla.DirectMessage(SCI_GETSELECTIONEND).ToInt32();
        return lines.ByteToCharPosition(pos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.SelectionEnd"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void SelectionEndSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        value = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETSELECTIONEND, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.SelectionEolFilled"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool SelectionEolFilledGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETSELEOLFILLED) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.SelectionEolFilled"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void SelectionEolFilledSet(this IScintillaApi scintilla, bool value)
    {
        var eolFilled = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETSELEOLFILLED, eolFilled);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.SelectionStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int SelectionStartGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        var pos = scintilla.DirectMessage(SCI_GETSELECTIONSTART).ToInt32();
        return lines.ByteToCharPosition(pos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.SelectionStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void SelectionStartSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        value = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETSELECTIONSTART, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Status"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Status" />.</returns>
    public static Status StatusGet(this IScintillaApi scintilla) => 
        (Status)scintilla.DirectMessage(SCI_GETSTATUS);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.Status"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void StatusSet(this IScintillaApi scintilla, Status value)
    {
        var status = (int)value;
        scintilla.DirectMessage(SCI_SETSTATUS, new IntPtr(status));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.TabDrawMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="TabDrawMode" />.</returns>
    public static TabDrawMode TabDrawModeGet(this IScintillaApi scintilla) =>
        (TabDrawMode)scintilla.DirectMessage(SCI_GETTABDRAWMODE);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.TabDrawMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void TabDrawModeSet(this IScintillaApi scintilla, TabDrawMode value)
    {
        var tabDrawMode = (int)value;
        scintilla.DirectMessage(SCI_SETTABDRAWMODE, new IntPtr(tabDrawMode));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.TabIndents"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool TabIndentsGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETTABINDENTS) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.TabIndents"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void TabIndentsSet(this IScintillaApi scintilla, bool value)
    {
        var ptr = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETTABINDENTS, ptr);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.TabWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int TabWidthGet(this IScintillaApi scintilla) => 
        scintilla.DirectMessage(SCI_GETTABWIDTH).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.TabWidth"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void TabWidthSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETTABWIDTH, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.TargetEnd"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int TargetEndGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        // The position can become stale and point to a place outside of the document so we must clamp it
        var bytePos = HelpersGeneral.Clamp(scintilla.DirectMessage(SCI_GETTARGETEND).ToInt32(), 0,
            scintilla.DirectMessage(SCI_GETTEXTLENGTH).ToInt32());
        return lines.ByteToCharPosition(bytePos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.TargetEnd"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void TargetEndSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        value = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETTARGETEND, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.TargetStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int TargetStartGet(this IScintillaApi scintilla, ILineCollection lines)
    {
        // The position can become stale and point to a place outside of the document so we must clamp it
        var bytePos = HelpersGeneral.Clamp(scintilla.DirectMessage(SCI_GETTARGETSTART).ToInt32(), 0,
            scintilla.DirectMessage(SCI_GETTEXTLENGTH).ToInt32());
        return lines.ByteToCharPosition(bytePos);
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.TargetStart"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    public static void TargetStartSet(this IScintillaApi scintilla, int value, ILineCollection lines)
    {
        value = HelpersGeneral.Clamp(value, 0, scintilla.TextLength);
        value = lines.CharToBytePosition(value);
        scintilla.DirectMessage(SCI_SETTARGETSTART, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.TargetText"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="string" />.</returns>
    public static unsafe string TargetTextGet(this IScintillaApi scintilla)
    {
        var length = scintilla.DirectMessage(SCI_GETTARGETTEXT).ToInt32();
        if (length == 0)
        {
            return string.Empty;
        }

        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_GETTARGETTEXT, IntPtr.Zero, new IntPtr(bp));
            return HelpersGeneral.GetString(new IntPtr(bp), length, scintilla.Encoding);
        }
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Technology"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="Technology" />.</returns>
    public static Technology TechnologyGet(this IScintillaApi scintilla) =>
        (Technology)scintilla.DirectMessage(SCI_GETTECHNOLOGY);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.Technology"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void TechnologySet(this IScintillaApi scintilla, Technology value)
    {
        var technology = (int)value;
        scintilla.DirectMessage(SCI_SETTECHNOLOGY, new IntPtr(technology));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Text"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="string" />.</returns>
    public static unsafe string TextGet(this IScintillaApi scintilla)
    {
        var length = scintilla.DirectMessage(SCI_GETTEXTLENGTH).ToInt32();
        var ptr = scintilla.DirectMessage(SCI_GETRANGEPOINTER, new IntPtr(0), new IntPtr(length));
        if (ptr == IntPtr.Zero)
        {
            return string.Empty;
        }

        // Assumption is that moving the gap will always be equal to or less expensive
        // than using one of the APIs which requires an intermediate buffer.
        var text = new string((sbyte*)ptr, 0, length, scintilla.Encoding);
        return text;
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.Text"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    /// <param name="designMode">A value indicating whether the design mode is active. NOTE: Windows only!</param>
    /// <param name="readOnly">The <see cref="IScintillaProperties{TColor}.ReadOnly"/> property value.</param>
    /// <param name="appendTextAction">A <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.AppendText"/> delegate.</param>
    public static unsafe void TextSet(this IScintillaApi scintilla, string value, bool designMode, bool readOnly,
        Action<string> appendTextAction)
    {
        var previousReadOnly = designMode && readOnly;

        // Allow Text property change in read-only mode when the designer is active.
        if (previousReadOnly && designMode)
        {
            scintilla.DirectMessage(SCI_SETREADONLY, IntPtr.Zero);
        }

        if (string.IsNullOrEmpty(value))
        {
            scintilla.DirectMessage(SCI_CLEARALL);
        }
        else if (value.Contains("\0"))
        {
            scintilla.DirectMessage(SCI_CLEARALL);
            appendTextAction(value);
        }
        else
        {
            fixed (byte* bp = HelpersGeneral.GetBytes(value, scintilla.Encoding, zeroTerminated: true))
            {
                scintilla.DirectMessage(SCI_SETTEXT, IntPtr.Zero, new IntPtr(bp));
            }
        }

        // Allow Text property change in read-only mode when the designer is active.
        if (previousReadOnly && designMode)
        {
            scintilla.DirectMessage(SCI_SETREADONLY, new IntPtr(1));
        }
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaApi.TextLength"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="lines">The line collection of the Scintilla control.</param>
    /// <returns><see cref="int" />.</returns>
    public static int TextLengthGet(this IScintillaApi scintilla, ILineCollection lines) => 
        lines.TextLength;

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.UseTabs"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool UseTabsGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETUSETABS) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.UseTabs"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void UseTabsSet(this IScintillaApi scintilla, bool value)
    {
        var useTabs = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETUSETABS, useTabs);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ViewEol"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool ViewEolGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETVIEWEOL) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ViewEol"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ViewEolSet(this IScintillaApi scintilla, bool value)
    {
        var visible = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETVIEWEOL, visible);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.ViewWhitespace"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="WhitespaceMode" />.</returns>
    public static WhitespaceMode ViewWhitespaceGet(this IScintillaApi scintilla) => 
        (WhitespaceMode)scintilla.DirectMessage(SCI_GETVIEWWS);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.ViewWhitespace"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ViewWhitespaceSet(this IScintillaApi scintilla, WhitespaceMode value)
    {
        var wsMode = (int)value;
        scintilla.DirectMessage(SCI_SETVIEWWS, new IntPtr(wsMode));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.VirtualSpaceOptions"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="VirtualSpace" />.</returns>
    public static VirtualSpace VirtualSpaceOptionsGet(this IScintillaApi scintilla) =>
        (VirtualSpace)scintilla.DirectMessage(SCI_GETVIRTUALSPACEOPTIONS);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.VirtualSpaceOptions"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void VirtualSpaceOptionsSet(this IScintillaApi scintilla, VirtualSpace value)
    {
        var virtualSpace = (int)value;
        scintilla.DirectMessage(SCI_SETVIRTUALSPACEOPTIONS, new IntPtr(virtualSpace));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.VScrollBar"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="bool" />.</returns>
    public static bool VScrollBarGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETVSCROLLBAR) != IntPtr.Zero;

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.VScrollBar"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void VScrollBarSet(this IScintillaApi scintilla, bool value)
    {
        var visible = value ? new IntPtr(1) : IntPtr.Zero;
        scintilla.DirectMessage(SCI_SETVSCROLLBAR, visible);
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WhitespaceSize"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int WhitespaceSizeGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETWHITESPACESIZE).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WhitespaceSize"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void WhitespaceSizeSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETWHITESPACESIZE, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WordChars"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="string" />.</returns>
    public static unsafe string WordCharsGet(this IScintillaApi scintilla)
    {
        var length = scintilla.DirectMessage(SCI_GETWORDCHARS, IntPtr.Zero, IntPtr.Zero).ToInt32();
        var bytes = new byte[length + 1];
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_GETWORDCHARS, IntPtr.Zero, new IntPtr(bp));
            return HelpersGeneral.GetString(new IntPtr(bp), length, Encoding.ASCII);
        }
    }

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WordChars"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static unsafe void WordCharsSet(this IScintillaApi scintilla, string? value)
    {
        if (value == null)
        {
            scintilla.DirectMessage(SCI_SETWORDCHARS, IntPtr.Zero, IntPtr.Zero);
            return;
        }

        // Scintilla stores each of the characters specified in a char array which it then
        // uses as a lookup for word matching logic. Thus, any multi-byte chars wouldn't work.
        var bytes = HelpersGeneral.GetBytes(value, Encoding.ASCII, zeroTerminated: true);
        fixed (byte* bp = bytes)
        {
            scintilla.DirectMessage(SCI_SETWORDCHARS, IntPtr.Zero, new IntPtr(bp));
        }
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WrapIndentMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="WrapIndentMode" />.</returns>
    public static WrapIndentMode WrapIndentModeGet(this IScintillaApi scintilla) =>
        (WrapIndentMode)scintilla.DirectMessage(SCI_GETWRAPINDENTMODE);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WrapIndentMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void WrapIndentModeSet(this IScintillaApi scintilla, WrapIndentMode value)
    {
        var wrapIndentMode = (int)value;
        scintilla.DirectMessage(SCI_SETWRAPINDENTMODE, new IntPtr(wrapIndentMode));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WrapMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="WrapMode" />.</returns>
    public static WrapMode WrapModeGet(this IScintillaApi scintilla) =>
        (WrapMode)scintilla.DirectMessage(SCI_GETWRAPMODE);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WrapMode"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void WrapModeSet(this IScintillaApi scintilla, WrapMode value)
    {
        var wrapMode = (int)value;
        scintilla.DirectMessage(SCI_SETWRAPMODE, new IntPtr(wrapMode));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WrapStartIndent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int WrapStartIndentGet(this IScintillaApi scintilla) =>
        scintilla.DirectMessage(SCI_GETWRAPSTARTINDENT).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WrapStartIndent"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void WrapStartIndentSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETWRAPSTARTINDENT, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WrapVisualFlags"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="WrapVisualFlags" />.</returns>
    public static WrapVisualFlags WrapVisualFlagsGet(this IScintillaApi scintilla) =>
        (WrapVisualFlags)scintilla.DirectMessage(SCI_GETWRAPVISUALFLAGS);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WrapVisualFlags"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void WrapVisualFlagsSet(this IScintillaApi scintilla, WrapVisualFlags value)
    {
        var wrapVisualFlags = (int)value;
        scintilla.DirectMessage(SCI_SETWRAPVISUALFLAGS, new IntPtr(wrapVisualFlags));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.WrapVisualFlagLocation"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="WrapVisualFlagLocation" />.</returns>
    public static WrapVisualFlagLocation WrapVisualFlagLocationGet(this IScintillaApi scintilla) =>
        (WrapVisualFlagLocation)scintilla.DirectMessage(SCI_GETWRAPVISUALFLAGSLOCATION);

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.WrapVisualFlagLocation"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void WrapVisualFlagLocationSet(this IScintillaApi scintilla, WrapVisualFlagLocation value)
    {
        var location = (int)value;
        scintilla.DirectMessage(SCI_SETWRAPVISUALFLAGSLOCATION, new IntPtr(location));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.XOffset"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int XOffsetGet(this IScintillaApi scintilla) => 
        scintilla.DirectMessage(SCI_GETXOFFSET).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.XOffset"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void XOffsetSet(this IScintillaApi scintilla, int value)
    {
        value = HelpersGeneral.ClampMin(value, 0);
        scintilla.DirectMessage(SCI_SETXOFFSET, new IntPtr(value));
    }

    /// <summary>
    /// A get method for the <see cref="IScintillaProperties{TColor}.Zoom"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <returns><see cref="int" />.</returns>
    public static int ZoomGet(this IScintillaApi scintilla) => 
        scintilla.DirectMessage(SCI_GETZOOM).ToInt32();

    /// <summary>
    /// A set method for the <see cref="IScintillaProperties{TColor}.Zoom"/> property.
    /// </summary>
    /// <param name="scintilla">The Scintilla instance.</param>
    /// <param name="value">The property value.</param>
    public static void ZoomSet(this IScintillaApi scintilla, int value)
    {
        scintilla.DirectMessage(SCI_SETZOOM, new IntPtr(value));
    }
}