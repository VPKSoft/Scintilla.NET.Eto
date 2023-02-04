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
/// Style constants for use with the <see cref="Lexer.Pascal" /> lexer.
/// </summary>
public static class Pascal
{
    /// <summary>
    /// Default style index.
    /// </summary>
    public const int Default = SCE_PAS_DEFAULT;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_PAS_IDENTIFIER;

    /// <summary>
    /// Comment style '{' index.
    /// </summary>
    public const int Comment = SCE_PAS_COMMENT;

    /// <summary>
    /// Comment style 2 "(*" index.
    /// </summary>
    public const int Comment2 = SCE_PAS_COMMENT2;

    /// <summary>
    /// Comment line style "//" index.
    /// </summary>
    public const int CommentLine = SCE_PAS_COMMENTLINE;

    /// <summary>
    /// Preprocessor style "{$" index.
    /// </summary>
    public const int Preprocessor = SCE_PAS_PREPROCESSOR;

    /// <summary>
    /// Preprocessor style 2 "(*$" index.
    /// </summary>
    public const int Preprocessor2 = SCE_PAS_PREPROCESSOR2;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_PAS_NUMBER;

    /// <summary>
    /// Hexadecimal number style index.
    /// </summary>
    public const int HexNumber = SCE_PAS_HEXNUMBER;

    /// <summary>
    /// Word (keyword set 0) style index.
    /// </summary>
    public const int Word = SCE_PAS_WORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_PAS_STRING;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_PAS_STRINGEOL;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_PAS_CHARACTER;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_PAS_OPERATOR;

    /// <summary>
    /// Assembly style index.
    /// </summary>
    public const int Asm = SCE_PAS_ASM;
}
