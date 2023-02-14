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
/// Style constants for use with the <see cref="Lexer.Perl" /> lexer.
/// </summary>
public static class Perl
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_PL_DEFAULT;

    /// <summary>
    /// Error style index.
    /// </summary>
    public const int Error = SCE_PL_ERROR;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_PL_COMMENTLINE;

    /// <summary>
    /// POD style index.
    /// </summary>
    public const int Pod = SCE_PL_POD;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_PL_NUMBER;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Word = SCE_PL_WORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_PL_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_PL_CHARACTER;

    /// <summary>
    /// Punctuation style index.
    /// </summary>
    public const int Punctuation = SCE_PL_PUNCTUATION;

    /// <summary>
    /// Preprocessor style index.
    /// </summary>
    public const int Preprocessor = SCE_PL_PREPROCESSOR;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_PL_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_PL_IDENTIFIER;

    /// <summary>
    /// Scalar style index.
    /// </summary>
    public const int Scalar = SCE_PL_SCALAR;

    /// <summary>
    /// Array style index.
    /// </summary>
    public const int Array = SCE_PL_ARRAY;

    /// <summary>
    /// Hash style index.
    /// </summary>
    public const int Hash = SCE_PL_HASH;

    /// <summary>
    /// Symbol table style index.
    /// </summary>
    public const int SymbolTable = SCE_PL_SYMBOLTABLE;

    /// <summary>
    /// Variable indexer index.
    /// </summary>
    public const int VariableIndexer = SCE_PL_VARIABLE_INDEXER;

    /// <summary>
    /// Regular expression style index.
    /// </summary>
    public const int Regex = SCE_PL_REGEX;

    /// <summary>
    /// RegSubst style index.
    /// </summary>
    public const int RegSubst = SCE_PL_REGSUBST;

    // public const int LongQuote = ScintillaConstants.SCE_PL_LONGQUOTE;

    /// <summary>
    /// Backtick (grave accent, backquote) style index.
    /// </summary>
    public const int BackTicks = SCE_PL_BACKTICKS;

    /// <summary>
    /// Data section style index.
    /// </summary>
    public const int DataSection = SCE_PL_DATASECTION;

    /// <summary>
    /// HereDoc delimiter style index.
    /// </summary>
    public const int HereDelim = SCE_PL_HERE_DELIM;

    /// <summary>
    /// HereDoc single-quote style index.
    /// </summary>
    public const int HereQ = SCE_PL_HERE_Q;

    /// <summary>
    /// HereDoc double-quote style index.
    /// </summary>
    public const int HereQq = SCE_PL_HERE_QQ;

    /// <summary>
    /// HereDoc backtick style index.
    /// </summary>
    public const int HereQx = SCE_PL_HERE_QX;

    /// <summary>
    /// Q quote style index.
    /// </summary>
    public const int StringQ = SCE_PL_STRING_Q;

    /// <summary>
    /// QQ quote style index.
    /// </summary>
    public const int StringQq = SCE_PL_STRING_QQ;

    /// <summary>
    /// QZ quote style index.
    /// </summary>
    public const int StringQx = SCE_PL_STRING_QX;

    /// <summary>
    /// QR quote style index.
    /// </summary>
    public const int StringQr = SCE_PL_STRING_QR;

    /// <summary>
    /// QW quote style index.
    /// </summary>
    public const int StringQw = SCE_PL_STRING_QW;

    /// <summary>
    /// POD verbatim style index.
    /// </summary>
    public const int PodVerb = SCE_PL_POD_VERB;

    /// <summary>
    /// Subroutine prototype style index.
    /// </summary>
    public const int SubPrototype = SCE_PL_SUB_PROTOTYPE;

    /// <summary>
    /// Format identifier style index.
    /// </summary>
    public const int FormatIdent = SCE_PL_FORMAT_IDENT;

    /// <summary>
    /// Format style index.
    /// </summary>
    public const int Format = SCE_PL_FORMAT;

    /// <summary>
    /// String variable style index.
    /// </summary>
    public const int StringVar = SCE_PL_STRING_VAR;

    /// <summary>
    /// XLAT style index.
    /// </summary>
    public const int XLat = SCE_PL_XLAT;

    /// <summary>
    /// Regular expression variable style index.
    /// </summary>
    public const int RegexVar = SCE_PL_REGEX_VAR;

    /// <summary>
    /// RegSubst variable style index.
    /// </summary>
    public const int RegSubstVar = SCE_PL_REGSUBST_VAR;

    /// <summary>
    /// Backticks variable style index.
    /// </summary>
    public const int BackticksVar = SCE_PL_BACKTICKS_VAR;

    /// <summary>
    /// HereDoc QQ quote variable style index.
    /// </summary>
    public const int HereQqVar = SCE_PL_HERE_QQ_VAR;

    /// <summary>
    /// HereDoc QX quote variable style index.
    /// </summary>
    public const int HereQxVar = SCE_PL_HERE_QX_VAR;

    /// <summary>
    /// QQ quote variable style index.
    /// </summary>
    public const int StringQqVar = SCE_PL_STRING_QQ_VAR;

    /// <summary>
    /// QX quote variable style index.
    /// </summary>
    public const int StringQxVar = SCE_PL_STRING_QX_VAR;

    /// <summary>
    /// QR quote variable style index.
    /// </summary>
    public const int StringQrVar = SCE_PL_STRING_QR_VAR;
}
