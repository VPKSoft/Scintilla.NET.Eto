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
/// Style constants for use with the <see cref="Lexer.Ruby" /> lexer.
/// </summary>
public static class Ruby
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_RB_DEFAULT;

    /// <summary>
    /// Error style index.
    /// </summary>
    public const int Error = SCE_RB_ERROR;

    /// <summary>
    /// Line comment style index.
    /// </summary>
    public const int CommentLine = SCE_RB_COMMENTLINE;

    /// <summary>
    /// POD style index.
    /// </summary>
    public const int Pod = SCE_RB_POD;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_RB_NUMBER;

    /// <summary>
    /// Keyword style index.
    /// </summary>
    public const int Word = SCE_RB_WORD;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_RB_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_RB_CHARACTER;

    /// <summary>
    /// Class name style index.
    /// </summary>
    public const int ClassName = SCE_RB_CLASSNAME;

    /// <summary>
    /// Definition style index.
    /// </summary>
    public const int DefName = SCE_RB_DEFNAME;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_RB_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_RB_IDENTIFIER;

    /// <summary>
    /// Regular expression style index.
    /// </summary>
    public const int Regex = SCE_RB_REGEX;

    /// <summary>
    /// Global style index.
    /// </summary>
    public const int Global = SCE_RB_GLOBAL;

    /// <summary>
    /// Symbol style index.
    /// </summary>
    public const int Symbol = SCE_RB_SYMBOL;

    /// <summary>
    /// Module name style index.
    /// </summary>
    public const int ModuleName = SCE_RB_MODULE_NAME;

    /// <summary>
    /// Instance variable style index.
    /// </summary>
    public const int InstanceVar = SCE_RB_INSTANCE_VAR;

    /// <summary>
    /// Class variable style index.
    /// </summary>
    public const int ClassVar = SCE_RB_CLASS_VAR;

    /// <summary>
    /// Backticks style index.
    /// </summary>
    public const int BackTicks = SCE_RB_BACKTICKS;

    /// <summary>
    /// Data section style index.
    /// </summary>
    public const int DataSection = SCE_RB_DATASECTION;

    /// <summary>
    /// HereDoc delimiter style index.
    /// </summary>
    public const int HereDelim = SCE_RB_HERE_DELIM;

    /// <summary>
    /// HereDoc Q quote style index.
    /// </summary>
    public const int HereQ = SCE_RB_HERE_Q;

    /// <summary>
    /// HereDoc QQ quote style index.
    /// </summary>
    public const int HereQq = SCE_RB_HERE_QQ;

    /// <summary>
    /// HereDoc QX quote style index.
    /// </summary>
    public const int HereQx = SCE_RB_HERE_QX;

    /// <summary>
    /// Q quote string style index.
    /// </summary>
    public const int StringQ = SCE_RB_STRING_Q;

    /// <summary>
    /// QQ quote string style index.
    /// </summary>
    public const int StringQq = SCE_RB_STRING_QQ;

    /// <summary>
    /// QX quote string style index.
    /// </summary>
    public const int StringQx = SCE_RB_STRING_QX;

    /// <summary>
    /// QR quote string style index.
    /// </summary>
    public const int StringQr = SCE_RB_STRING_QR;

    /// <summary>
    /// QW quote style index.
    /// </summary>
    public const int StringQw = SCE_RB_STRING_QW;

    /// <summary>
    /// Demoted keyword style index.
    /// </summary>
    public const int WordDemoted = SCE_RB_WORD_DEMOTED;

    /// <summary>
    /// Standard-in style index.
    /// </summary>
    public const int StdIn = SCE_RB_STDIN;

    /// <summary>
    /// Standard-out style index.
    /// </summary>
    public const int StdOut = SCE_RB_STDOUT;

    /// <summary>
    /// Standard-error style index.
    /// </summary>
    public const int StdErr = SCE_RB_STDERR;

    // public const int UpperBound = ScintillaConstants.SCE_RB_UPPER_BOUND;
}
