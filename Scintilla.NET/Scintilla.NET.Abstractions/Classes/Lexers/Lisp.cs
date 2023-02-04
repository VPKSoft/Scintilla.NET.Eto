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
/// Style constants for use with the <see cref="Lexer.Lisp" /> lexer.
/// </summary>
public static class Lisp
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_LISP_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_LISP_COMMENT;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_LISP_NUMBER;

    /// <summary>
    /// Functions and special operators (list 0) style index.
    /// </summary>
    public const int Keyword = SCE_LISP_KEYWORD;

    /// <summary>
    /// Keywords (list 1) style index.
    /// </summary>
    public const int KeywordKw = SCE_LISP_KEYWORD_KW;

    /// <summary>
    /// Symbol style index.
    /// </summary>
    public const int Symbol = SCE_LISP_SYMBOL;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_LISP_STRING;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_LISP_STRINGEOL;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_LISP_IDENTIFIER;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_LISP_OPERATOR;

    /// <summary>
    /// Special character style index.
    /// </summary>
    public const int Special = SCE_LISP_SPECIAL;

    /// <summary>
    /// Multi-line comment style index.
    /// </summary>
    public const int MultiComment = SCE_LISP_MULTI_COMMENT;
}
