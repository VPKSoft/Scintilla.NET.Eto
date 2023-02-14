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
/// Style constants for use with the <see cref="Lexer.Python" /> lexer.
/// </summary>
public static class Python
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_P_DEFAULT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_P_COMMENTLINE;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_P_NUMBER;

    /// <summary>
    /// String style index.
    /// </summary>
    public const int String = SCE_P_STRING;

    /// <summary>
    /// Single-quote style index.
    /// </summary>
    public const int Character = SCE_P_CHARACTER;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Word = SCE_P_WORD;

    /// <summary>
    /// Triple single-quote style index.
    /// </summary>
    public const int Triple = SCE_P_TRIPLE;

    /// <summary>
    /// Triple double-quote style index.
    /// </summary>
    public const int TripleDouble = SCE_P_TRIPLEDOUBLE;

    /// <summary>
    /// Class name style index.
    /// </summary>
    public const int ClassName = SCE_P_CLASSNAME;

    /// <summary>
    /// Function or method name style index.
    /// </summary>
    public const int DefName = SCE_P_DEFNAME;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_P_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_P_IDENTIFIER;

    /// <summary>
    /// Block comment style index.
    /// </summary>
    public const int CommentBlock = SCE_P_COMMENTBLOCK;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_P_STRINGEOL;

    /// <summary>
    /// Keyword style 2 index.
    /// </summary>
    public const int Word2 = SCE_P_WORD2;

    /// <summary>
    /// Decorator style index.
    /// </summary>
    public const int Decorator = SCE_P_DECORATOR;
}
