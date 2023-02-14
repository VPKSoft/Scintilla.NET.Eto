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

using ScintillaNet.Abstractions.Enumerations;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Abstractions.Classes.Lexers;

/// <summary>
/// Embedded JavaScript style constants for use with the <see cref="Lexer.Html" /> lexer.
/// </summary>
public static class JavaScript
{
    /// <summary>
    /// Start style index (allows EOL filled background to not start on same line as SCRIPT tag).
    /// </summary>
    public const int Start = SCE_HJ_START;

    /// <summary>
    /// Default style index.
    /// </summary>
    public const int Default = SCE_HJ_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_HJ_COMMENT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_HJ_COMMENTLINE;

    /// <summary>
    /// Doc comment style index.
    /// </summary>
    public const int CommentDoc = SCE_HJ_COMMENTDOC;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_HJ_NUMBER;

    /// <summary>
    /// Word style index.
    /// </summary>
    public const int Word = SCE_HJ_WORD;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Keyword = SCE_HJ_KEYWORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int DoubleString = SCE_HJ_DOUBLESTRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int SingleString = SCE_HJ_SINGLESTRING;

    /// <summary>
    /// Symbols style index.
    /// </summary>
    public const int Symbols = SCE_HJ_SYMBOLS;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_HJ_STRINGEOL;

    /// <summary>
    /// Regular expression style index.
    /// </summary>
    public const int Regex = SCE_HJ_REGEX;
}
