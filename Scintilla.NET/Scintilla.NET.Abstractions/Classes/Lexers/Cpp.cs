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
/// Style constants for use with the <see cref="Lexer.Cpp" /> lexer.
/// </summary>
public static class Cpp
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_C_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_C_COMMENT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_C_COMMENTLINE;

    /// <summary>
    /// Documentation comment style index.
    /// </summary>
    public const int CommentDoc = SCE_C_COMMENTDOC;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_C_NUMBER;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Word = SCE_C_WORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_C_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_C_CHARACTER;

    /// <summary>
    /// UUID style index.
    /// </summary>
    public const int Uuid = SCE_C_UUID;

    /// <summary>
    /// Preprocessor style index.
    /// </summary>
    public const int Preprocessor = SCE_C_PREPROCESSOR;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_C_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_C_IDENTIFIER;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_C_STRINGEOL;

    /// <summary>
    /// Verbatim string style index.
    /// </summary>
    public const int Verbatim = SCE_C_VERBATIM;

    /// <summary>
    /// Regular expression style index.
    /// </summary>
    public const int Regex = SCE_C_REGEX;

    /// <summary>
    /// Documentation comment line style index.
    /// </summary>
    public const int CommentLineDoc = SCE_C_COMMENTLINEDOC;

    /// <summary>
    /// Keyword style 2 index.
    /// </summary>
    public const int Word2 = SCE_C_WORD2;

    /// <summary>
    /// Comment keyword style index.
    /// </summary>
    public const int CommentDocKeyword = SCE_C_COMMENTDOCKEYWORD;

    /// <summary>
    /// Comment keyword error style index.
    /// </summary>
    public const int CommentDocKeywordError = SCE_C_COMMENTDOCKEYWORDERROR;

    /// <summary>
    /// Global class style index.
    /// </summary>
    public const int GlobalClass = SCE_C_GLOBALCLASS;

    /// <summary>
    /// Raw string style index.
    /// </summary>
    public const int StringRaw = SCE_C_STRINGRAW;

    /// <summary>
    /// Triple-quoted string style index.
    /// </summary>
    public const int TripleVerbatim = SCE_C_TRIPLEVERBATIM;

    /// <summary>
    /// Hash-quoted string style index.
    /// </summary>
    public const int HashQuotedString = SCE_C_HASHQUOTEDSTRING;

    /// <summary>
    /// Preprocessor comment style index.
    /// </summary>
    public const int PreprocessorComment = SCE_C_PREPROCESSORCOMMENT;

    /// <summary>
    /// Preprocessor documentation comment style index.
    /// </summary>
    public const int PreprocessorCommentDoc = SCE_C_PREPROCESSORCOMMENTDOC;

    /// <summary>
    /// User-defined literal style index.
    /// </summary>
    public const int UserLiteral = SCE_C_USERLITERAL;

    /// <summary>
    /// Task marker style index.
    /// </summary>
    public const int TaskMarker = SCE_C_TASKMARKER;

    /// <summary>
    /// Escape sequence style index.
    /// </summary>
    public const int EscapeSequence = SCE_C_ESCAPESEQUENCE;
}