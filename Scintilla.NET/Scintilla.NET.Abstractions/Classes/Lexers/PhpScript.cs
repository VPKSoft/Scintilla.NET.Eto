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

#region PhpScript

/// <summary>
/// Style constants for use with the <see cref="Lexer.PhpScript" /> lexer.
/// </summary>
public static class PhpScript
{
    /// <summary>
    /// Complex Variable style index.
    /// </summary>
    public const int ComplexVariable = SCE_HPHP_COMPLEX_VARIABLE;

    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_HPHP_DEFAULT;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int HString = SCE_HPHP_HSTRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int SimpleString = SCE_HPHP_SIMPLESTRING;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Word = SCE_HPHP_WORD;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_HPHP_NUMBER;

    /// <summary>
    /// Variable style index.
    /// </summary>
    public const int Variable = SCE_HPHP_VARIABLE;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_HPHP_COMMENT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_HPHP_COMMENTLINE;

    /// <summary>
    /// Double-quoted string variable style index.
    /// </summary>
    public const int HStringVariable = SCE_HPHP_HSTRING_VARIABLE;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_HPHP_OPERATOR;
}

#endregion PhpScript
