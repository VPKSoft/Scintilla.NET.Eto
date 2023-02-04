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
/// Style constants for use with the <see cref="Lexer.Fortran" /> lexer.
/// </summary>
public static class Fortran
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_F_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_F_COMMENT;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_F_NUMBER;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int String1 = SCE_F_STRING1;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String2 = SCE_F_STRING2;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_F_STRINGEOL;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_F_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_F_IDENTIFIER;

    /// <summary>
    /// Keyword (list 0) style index.
    /// </summary>
    public const int Word = SCE_F_WORD;

    /// <summary>
    /// Keyword 2 (list 1) style index.
    /// </summary>
    public const int Word2 = SCE_F_WORD2;

    /// <summary>
    /// Keyword 3 (list 2) style index.
    /// </summary>
    public const int Word3 = SCE_F_WORD3;

    /// <summary>
    /// Preprocessor style index.
    /// </summary>
    public const int Preprocessor = SCE_F_PREPROCESSOR;

    /// <summary>
    /// Operator 2 style index.
    /// </summary>
    public const int Operator2 = SCE_F_OPERATOR2;

    /// <summary>
    /// Label string style index.
    /// </summary>
    public const int Label = SCE_F_LABEL;

    /// <summary>
    /// Continuation style index.
    /// </summary>
    public const int Continuation = SCE_F_CONTINUATION;
}