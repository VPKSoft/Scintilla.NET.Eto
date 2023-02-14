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
/// Style constants for use with the <see cref="Lexer.R" /> lexer.
/// </summary>
public static class R
{
    /// <summary>
    /// Default style index.
    /// </summary>
    public const int Default = SCE_R_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_R_COMMENT;

    /// <summary>
    /// Keyword (set 0) style index.
    /// </summary>
    public const int KWord = SCE_R_KWORD;

    /// <summary>
    /// Base keyword (set 1) style index.
    /// </summary>
    public const int BaseKWord = SCE_R_BASEKWORD;

    /// <summary>
    /// Other keyword (set 2) style index.
    /// </summary>
    public const int OtherKWord = SCE_R_OTHERKWORD;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_R_NUMBER;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_R_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int String2 = SCE_R_STRING2;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_R_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_R_IDENTIFIER;

    /// <summary>
    /// Infix style index.
    /// </summary>
    public const int Infix = SCE_R_INFIX;

    /// <summary>
    /// Unclosed infix EOL style index.
    /// </summary>
    public const int InfixEol = SCE_R_INFIXEOL;
}