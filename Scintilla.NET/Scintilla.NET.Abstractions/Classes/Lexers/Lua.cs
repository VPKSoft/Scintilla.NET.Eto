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
/// Style constants for use with the <see cref="Lexer.Lua" /> lexer.
/// </summary>
public static class Lua
{
    /// <summary>
    /// Default style index.
    /// </summary>
    public const int Default = SCE_LUA_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_LUA_COMMENT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_LUA_COMMENTLINE;

    /// <summary>
    /// Documentation comment style index.
    /// </summary>
    public const int CommentDoc = SCE_LUA_COMMENTDOC;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_LUA_NUMBER;

    /// <summary>
    /// Keyword list 1 (index 0) style index.
    /// </summary>
    public const int Word = SCE_LUA_WORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_LUA_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_LUA_CHARACTER;

    /// <summary>
    /// Literal string style index.
    /// </summary>
    public const int LiteralString = SCE_LUA_LITERALSTRING;

    /// <summary>
    /// Preprocessor style index.
    /// </summary>
    public const int Preprocessor = SCE_LUA_PREPROCESSOR;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_LUA_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_LUA_IDENTIFIER;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_LUA_STRINGEOL;

    /// <summary>
    /// Keywords list 2 (index 1) style index.
    /// </summary>
    public const int Word2 = SCE_LUA_WORD2;

    /// <summary>
    /// Keywords list 3 (index 2) style index.
    /// </summary>
    public const int Word3 = SCE_LUA_WORD3;

    /// <summary>
    /// Keywords list 4 (index 3) style index.
    /// </summary>
    public const int Word4 = SCE_LUA_WORD4;

    /// <summary>
    /// Keywords list 5 (index 4) style index.
    /// </summary>
    public const int Word5 = SCE_LUA_WORD5;

    /// <summary>
    /// Keywords list 6 (index 5) style index.
    /// </summary>
    public const int Word6 = SCE_LUA_WORD6;

    /// <summary>
    /// Keywords list 7 (index 6) style index.
    /// </summary>
    public const int Word7 = SCE_LUA_WORD7;

    /// <summary>
    /// Keywords list 8 (index 7) style index.
    /// </summary>
    public const int Word8 = SCE_LUA_WORD8;

    /// <summary>
    /// Label style index.
    /// </summary>
    public const int Label = SCE_LUA_LABEL;
}
