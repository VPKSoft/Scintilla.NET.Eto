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
/// Style constants for use with the <see cref="Lexer.Batch" /> lexer.
/// </summary>
public static class Batch
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_BAT_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_BAT_COMMENT;

    /// <summary>
    /// Keyword (list 0) style index.
    /// </summary>
    public const int Word = SCE_BAT_WORD;

    /// <summary>
    /// Label style index.
    /// </summary>
    public const int Label = SCE_BAT_LABEL;

    /// <summary>
    /// Hide (@ECHO OFF/ON) style index.
    /// </summary>
    public const int Hide = SCE_BAT_HIDE;

    /// <summary>
    /// External command (keyword list 1) style index.
    /// </summary>
    public const int Command = SCE_BAT_COMMAND;

    /// <summary>
    /// Identifier string style index.
    /// </summary>
    public const int Identifier = SCE_BAT_IDENTIFIER;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_BAT_OPERATOR;
}
