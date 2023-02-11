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

using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.Methods;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Classes;

/// <summary>
/// Constants related to styles.
/// </summary>
public static class StyleConstants
{
    /// <summary>
    /// Default style index. This style is used to define properties that all styles receive when calling <see cref="IScintillaMethods.StyleClearAll" />.
    /// </summary>
    public const int Default = STYLE_DEFAULT;

    /// <summary>
    /// Line number style index. This style is used for text in line number margins. The background color of this style also
    /// sets the background color for all margins that do not have any folding mask set.
    /// </summary>
    public const int LineNumber = STYLE_LINENUMBER;

    /// <summary>
    /// Call tip style index. Only font name, size, foreground color, background color, and character set attributes
    /// can be used when displaying a call tip.
    /// </summary>
    public const int CallTip = STYLE_CALLTIP;

    /// <summary>
    /// Indent guide style index. This style is used to specify the foreground and background colors of <see cref="IScintillaProperties.IndentationGuides" />.
    /// </summary>
    public const int IndentGuide = STYLE_INDENTGUIDE;

    /// <summary>
    /// Brace highlighting style index. This style is used on a brace character when set with the <see cref="IScintillaMethods.BraceHighlight" /> method
    /// or the indentation guide when used with the <see cref="IScintillaProperties.HighlightGuide" /> property.
    /// </summary>
    public const int BraceLight = STYLE_BRACELIGHT;

    /// <summary>
    /// Bad brace style index. This style is used on an unmatched brace character when set with the <see cref="IScintillaMethods.BraceBadLight" /> method.
    /// </summary>
    public const int BraceBad = STYLE_BRACEBAD;

    /// <summary>
    /// Fold text tag style index. This is the style used for drawing text tags attached to folded text when
    /// <see cref="IScintillaMethods.FoldDisplayTextSetStyle" /> and <see cref="IScintillaLine.ToggleFoldShowText" /> are used.
    /// </summary>
    public const int FoldDisplayText = STYLE_FOLDDISPLAYTEXT;
}