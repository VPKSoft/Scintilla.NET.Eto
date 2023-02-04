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
/// Style constants for use with the <see cref="Lexer.PureBasic" /> lexer.
/// </summary>
public static class PureBasic
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_B_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_B_COMMENT;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_B_NUMBER;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Keyword = SCE_B_KEYWORD;

    /// <summary>
    /// String style index.
    /// </summary>
    public const int String = SCE_B_STRING;

    /// <summary>
    /// Preprocessor style index.
    /// </summary>
    public const int Preprocessor = SCE_B_PREPROCESSOR;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_B_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_B_IDENTIFIER;

    /// <summary>
    /// Date style index.
    /// </summary>
    public const int Date = SCE_B_DATE;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_B_STRINGEOL;

    /// <summary>
    /// Keyword list 2 (index 1) style index.
    /// </summary>
    public const int Keyword2 = SCE_B_KEYWORD2;

    /// <summary>
    /// Keyword list 3 (index 2) style index.
    /// </summary>
    public const int Keyword3 = SCE_B_KEYWORD3;

    /// <summary>
    /// Keyword list 4 (index 3) style index.
    /// </summary>
    public const int Keyword4 = SCE_B_KEYWORD4;

    /// <summary>
    /// Constant style index.
    /// </summary>
    public const int Constant = SCE_B_CONSTANT;

    /// <summary>
    /// Inline assembler style index.
    /// </summary>
    public const int Asm = SCE_B_ASM;

    /// <summary>
    /// Label style index.
    /// </summary>
    public const int Label = SCE_B_LABEL;

    /// <summary>
    /// Error style index.
    /// </summary>
    public const int Error = SCE_B_ERROR;

    /// <summary>
    /// Hexadecimal number style index.
    /// </summary>
    public const int HexNumber = SCE_B_HEXNUMBER;

    /// <summary>
    /// Binary number style index.
    /// </summary>
    public const int BinNumber = SCE_B_BINNUMBER;

    /// <summary>
    /// Block comment style index.
    /// </summary>
    public const int CommentBlock = SCE_B_COMMENTBLOCK;

    /// <summary>
    /// Documentation line style index.
    /// </summary>
    public const int DocLine = SCE_B_DOCLINE;

    /// <summary>
    /// Documentation block style index.
    /// </summary>
    public const int DocBlock = SCE_B_DOCBLOCK;

    /// <summary>
    /// Documentation keyword style index.
    /// </summary>
    public const int DocKeyword = SCE_B_DOCKEYWORD;
}