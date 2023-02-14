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
/// Style constants for use with the <see cref="Lexer.Verilog" /> lexer.
/// </summary>
// ReSharper disable once IdentifierTypo, it is written like this, see: https://en.wikipedia.org/wiki/Verilog
public static class Verilog
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_V_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_V_COMMENT;

    /// <summary>
    /// Comment line style index.
    /// </summary>
    public const int CommentLine = SCE_V_COMMENTLINE;

    /// <summary>
    /// Comment line bang (exclamation) style index.
    /// </summary>
    public const int CommentLineBang = SCE_V_COMMENTLINEBANG;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_V_NUMBER;

    /// <summary>
    /// Keyword (set 0) style index.
    /// </summary>
    public const int Word = SCE_V_WORD;

    /// <summary>
    /// String style index.
    /// </summary>
    public const int String = SCE_V_STRING;

    /// <summary>
    /// Keyword (set 1) style index.
    /// </summary>
    public const int Word2 = SCE_V_WORD2;

    /// <summary>
    /// Keyword (set 2) style index.
    /// </summary>
    public const int Word3 = SCE_V_WORD3;

    /// <summary>
    /// Preprocessor style index.
    /// </summary>
    public const int Preprocessor = SCE_V_PREPROCESSOR;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_V_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_V_IDENTIFIER;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_V_STRINGEOL;

    /// <summary>
    /// User word (set 3) style index.
    /// </summary>
    public const int User = SCE_V_USER;

    /// <summary>
    /// Comment word (set 4) style index.
    /// </summary>
    public const int CommentWord = SCE_V_COMMENT_WORD;

    /// <summary>
    /// Input style index.
    /// </summary>
    public const int Input = SCE_V_INPUT;

    /// <summary>
    /// Output style index.
    /// </summary>
    public const int Output = SCE_V_OUTPUT;

    /// <summary>
    /// In-out style index.
    /// </summary>
    public const int InOut = SCE_V_INOUT;

    /// <summary>
    /// Port connect style index.
    /// </summary>
    public const int PortConnect = SCE_V_PORT_CONNECT;
}