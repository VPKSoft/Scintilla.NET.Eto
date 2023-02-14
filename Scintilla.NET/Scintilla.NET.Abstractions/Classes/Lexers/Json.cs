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
/// Style constants for use with the <see cref="Lexer.Json" /> lexer.
/// </summary>
public static class Json
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_JSON_DEFAULT;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_JSON_NUMBER;

    /// <summary>
    /// String style index.
    /// </summary>
    public const int String = SCE_JSON_STRING;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_JSON_STRINGEOL;

    /// <summary>
    /// Property name style index.
    /// </summary>
    public const int PropertyName = SCE_JSON_PROPERTYNAME;

    /// <summary>
    /// Escape sequence style index.
    /// </summary>
    public const int EscapeSequence = SCE_JSON_ESCAPESEQUENCE;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int LineComment = SCE_JSON_LINECOMMENT;

    /// <summary>
    /// Block comment style index.
    /// </summary>
    public const int BlockComment = SCE_JSON_BLOCKCOMMENT;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_JSON_OPERATOR;

    /// <summary>
    /// URI style index.
    /// </summary>
    public const int Uri = SCE_JSON_URI;

    /// <summary>
    /// Compact Internationalized Resource Identifier (IRI) style index.
    /// </summary>
    public const int CompactIRI = SCE_JSON_COMPACTIRI;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Keyword = SCE_JSON_KEYWORD;

    /// <summary>
    /// Linked data (LD) keyword style index.
    /// </summary>
    public const int LdKeyword = SCE_JSON_LDKEYWORD;

    /// <summary>
    /// Error style index.
    /// </summary>
    public const int Error = SCE_JSON_ERROR;
}