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
/// Style constants for use with the <see cref="Lexer.Matlab" /> lexer.
/// </summary>
// ReSharper disable three times CommentTypo, for next explanation
// ReSharper disable once IdentifierTypo, MATLAB, MatLab, matlab or Matlab ?
public static class Matlab
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_MATLAB_DEFAULT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int Comment = SCE_MATLAB_COMMENT;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_MATLAB_NUMBER;

    /// <summary>
    /// String style index.
    /// </summary>
    public const int String = SCE_MATLAB_STRING;

    /// <summary>
    /// Command style index.
    /// </summary>
    public const int Command = SCE_MATLAB_COMMAND;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Keyword = SCE_MATLAB_KEYWORD;

    /// <summary>
    /// Double quote string style index.
    /// </summary>
    public const int DoubleQuoteString = SCE_MATLAB_DOUBLEQUOTESTRING;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_MATLAB_IDENTIFIER;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_MATLAB_OPERATOR;
}
