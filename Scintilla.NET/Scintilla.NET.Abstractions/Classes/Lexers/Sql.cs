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
/// Style constants for use with the <see cref="Lexer.Sql" /> lexer.
/// </summary>
public static class Sql
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_SQL_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_SQL_COMMENT;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_SQL_COMMENTLINE;

    /// <summary>
    /// Documentation comment style index.
    /// </summary>
    public const int CommentDoc = SCE_SQL_COMMENTDOC;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_SQL_NUMBER;

    /// <summary>
    /// Keyword list 1 (index 0) style index.
    /// </summary>
    public const int Word = SCE_SQL_WORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_SQL_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_SQL_CHARACTER;

    /// <summary>
    /// Keyword from the SQL*Plus list (index 3) style index.
    /// </summary>
    public const int SqlPlus = SCE_SQL_SQLPLUS;

    /// <summary>
    /// SQL*Plus prompt style index.
    /// </summary>
    public const int SqlPlusPrompt = SCE_SQL_SQLPLUS_PROMPT;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_SQL_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_SQL_IDENTIFIER;

    /// <summary>
    /// SQL*Plus comment style index.
    /// </summary>
    public const int SqlPlusComment = SCE_SQL_SQLPLUS_COMMENT;

    /// <summary>
    /// Documentation line comment style index.
    /// </summary>
    public const int CommentLineDoc = SCE_SQL_COMMENTLINEDOC;

    /// <summary>
    /// Keyword list 2 (index 1) style index.
    /// </summary>
    public const int Word2 = SCE_SQL_WORD2;

    /// <summary>
    /// Documentation (Doxygen) keyword style index.
    /// </summary>
    public const int CommentDocKeyword = SCE_SQL_COMMENTDOCKEYWORD;

    /// <summary>
    /// Documentation (Doxygen) keyword error style index.
    /// </summary>
    public const int CommentDocKeywordError = SCE_SQL_COMMENTDOCKEYWORDERROR;

    /// <summary>
    /// Keyword user-list 1 (index 4) style index.
    /// </summary>
    public const int User1 = SCE_SQL_USER1;

    /// <summary>
    /// Keyword user-list 2 (index 5) style index.
    /// </summary>
    public const int User2 = SCE_SQL_USER2;

    /// <summary>
    /// Keyword user-list 3 (index 6) style index.
    /// </summary>
    public const int User3 = SCE_SQL_USER3;

    /// <summary>
    /// Keyword user-list 4 (index 7) style index.
    /// </summary>
    public const int User4 = SCE_SQL_USER4;

    /// <summary>
    /// Quoted identifier style index.
    /// </summary>
    public const int QuotedIdentifier = SCE_SQL_QUOTEDIDENTIFIER;

    /// <summary>
    /// Q operator style index.
    /// </summary>
    public const int QOperator = SCE_SQL_QOPERATOR;
}
