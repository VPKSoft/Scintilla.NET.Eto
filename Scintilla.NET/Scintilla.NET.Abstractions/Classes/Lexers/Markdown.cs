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

using Scintilla.NET.Abstractions.Enumerations;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Classes.Lexers;

/// <summary>
/// Style constants for use with the <see cref="Lexer.Markdown" /> lexer.
/// </summary>
public static class Markdown
{
    /// <summary>
    /// Default text style index.
    /// </summary>
    public const int Default = SCE_MARKDOWN_DEFAULT;

    /// <summary>
    /// Line begin style index.
    /// </summary>
    public const int LineBegin = SCE_MARKDOWN_LINE_BEGIN;

    /// <summary>
    /// Strong type 1 style index.
    /// </summary>
    public const int Strong1 = SCE_MARKDOWN_STRONG1;

    /// <summary>
    /// Strong type 2 style index.
    /// </summary>
    public const int Strong2 = SCE_MARKDOWN_STRONG2;

    /// <summary>
    /// Emphasis type 1 style index.
    /// </summary>
    public const int Em1 = SCE_MARKDOWN_EM1;

    /// <summary>
    /// Empasis type 2 style index.
    /// </summary>
    public const int Em2 = SCE_MARKDOWN_EM2;

    /// <summary>
    /// Header type 1 style index.
    /// </summary>
    public const int Header1 = SCE_MARKDOWN_HEADER1;

    /// <summary>
    /// Header type 2 style index.
    /// </summary>
    public const int Header2 = SCE_MARKDOWN_HEADER2;

    /// <summary>
    /// Header type 3 style index.
    /// </summary>
    public const int Header3 = SCE_MARKDOWN_HEADER3;

    /// <summary>
    /// Header type 4 style index.
    /// </summary>
    public const int Header4 = SCE_MARKDOWN_HEADER4;

    /// <summary>
    /// Header type 5 style index.
    /// </summary>
    public const int Header5 = SCE_MARKDOWN_HEADER5;

    /// <summary>
    /// Header type 6 style index.
    /// </summary>
    public const int Header6 = SCE_MARKDOWN_HEADER6;

    /// <summary>
    /// Pre char style index.
    /// </summary>
    public const int PreChar = SCE_MARKDOWN_PRECHAR;

    /// <summary>
    /// Unordered list style index.
    /// </summary>
    public const int UListItem = SCE_MARKDOWN_ULIST_ITEM;

    /// <summary>
    /// Ordered list style index.
    /// </summary>
    public const int OListItem = SCE_MARKDOWN_OLIST_ITEM;

    /// <summary>
    /// Blockquote style index.
    /// </summary>
    public const int BlockQuote = SCE_MARKDOWN_BLOCKQUOTE;

    /// <summary>
    /// Strikeout style index.
    /// </summary>
    public const int Strikeout = SCE_MARKDOWN_STRIKEOUT;

    /// <summary>
    /// Horizontal rule style index.
    /// </summary>
    public const int HRule = SCE_MARKDOWN_HRULE;

    /// <summary>
    /// Link style index.
    /// </summary>
    public const int Link = SCE_MARKDOWN_LINK;

    /// <summary>
    /// Code type 1 style index.
    /// </summary>
    public const int Code = SCE_MARKDOWN_CODE;

    /// <summary>
    /// Code type 2 style index.
    /// </summary>
    public const int Code2 = SCE_MARKDOWN_CODE2;

    /// <summary>
    /// Code block style index.
    /// </summary>
    public const int CodeBk = SCE_MARKDOWN_CODEBK;
}