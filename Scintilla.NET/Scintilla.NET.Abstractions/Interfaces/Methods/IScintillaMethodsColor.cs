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

using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Enumerations;

namespace ScintillaNet.Abstractions.Interfaces.Methods;

/// <summary>
/// An interface for Scintilla methods with generic color.
/// </summary>
/// <typeparam name="TColor">The type of the color used in the platform.</typeparam>
public interface IScintillaMethodsColor<in TColor>
    where TColor : struct
{
    /// <summary>
    /// Sets the color of highlighted text in a call tip.
    /// </summary>
    /// <param name="color">The new highlight text Color. The default is dark blue.</param>
    void CallTipSetForeHlt(TColor color);

    /// <summary>
    /// Specifies the long line indicator column number and color when <see cref="EdgeMode" /> is <see cref="EdgeMode.MultiLine" />.
    /// </summary>
    /// <param name="column">The zero-based column number to indicate.</param>
    /// <param name="edgeColor">The color of the vertical long line indicator.</param>
    /// <remarks>A column is defined as the width of a space character in the <see cref="StyleConstants.Default" /> style.</remarks>
    /// <seealso cref="IScintillaMethods.MultiEdgeClearAll" />
    void MultiEdgeAddLine(int column, TColor edgeColor);

    /// <summary>
    /// Sets the background color of additional selections.
    /// </summary>
    /// <param name="color">Additional selections background color.</param>
    /// <remarks>Calling <see cref="SetSelectionBackColor" /> will reset the <paramref name="color" /> specified.</remarks>
    void SetAdditionalSelBack(TColor color);

    /// <summary>
    /// Sets the foreground color of additional selections.
    /// </summary>
    /// <param name="color">Additional selections foreground color.</param>
    /// <remarks>Calling <see cref="SetSelectionForeColor" /> will reset the <paramref name="color" /> specified.</remarks>
    void SetAdditionalSelFore(TColor color);

    /// <summary>
    /// Sets a global override to the fold margin color.
    /// </summary>
    /// <param name="use">true to override the fold margin color; otherwise, false.</param>
    /// <param name="color">The global fold margin color.</param>
    /// <seealso cref="SetFoldMarginHighlightColor" />
    void SetFoldMarginColor(bool use, TColor color);

    /// <summary>
    /// Sets a global override to the fold margin highlight color.
    /// </summary>
    /// <param name="use">true to override the fold margin highlight color; otherwise, false.</param>
    /// <param name="color">The global fold margin highlight color.</param>
    /// <seealso cref="SetFoldMarginColor" />
    void SetFoldMarginHighlightColor(bool use, TColor color);

    /// <summary>
    /// Sets a global override to the selection background color.
    /// </summary>
    /// <param name="use">true to override the selection background color; otherwise, false.</param>
    /// <param name="color">The global selection background color.</param>
    /// <seealso cref="SetSelectionForeColor" />
    void SetSelectionBackColor(bool use, TColor color);

    /// <summary>
    /// Sets a global override to the selection foreground color.
    /// </summary>
    /// <param name="use">true to override the selection foreground color; otherwise, false.</param>
    /// <param name="color">The global selection foreground color.</param>
    /// <seealso cref="SetSelectionBackColor" />
    void SetSelectionForeColor(bool use, TColor color);

    /// <summary>
    /// Sets a global override to the whitespace background color.
    /// </summary>
    /// <param name="use">true to override the whitespace background color; otherwise, false.</param>
    /// <param name="color">The global whitespace background color.</param>
    /// <remarks>When not overridden globally, the whitespace background color is determined by the current lexer.</remarks>
    /// <seealso cref="IScintillaProperties.ViewWhitespace" />
    /// <seealso cref="SetWhitespaceForeColor" />
    void SetWhitespaceBackColor(bool use, TColor color);

    /// <summary>
    /// Sets a global override to the whitespace foreground color.
    /// </summary>
    /// <param name="use">true to override the whitespace foreground color; otherwise, false.</param>
    /// <param name="color">The global whitespace foreground color.</param>
    /// <remarks>When not overridden globally, the whitespace foreground color is determined by the current lexer.</remarks>
    /// <seealso cref="IScintillaProperties.ViewWhitespace" />
    /// <seealso cref="SetWhitespaceBackColor" />
    void SetWhitespaceForeColor(bool use, TColor color);
}