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

using Scintilla.NET.Abstractions.Enumerations;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Classes.Lexers;

/// <summary>
/// Style constants for use with the <see cref="Lexer.PowerShell" /> lexer.
/// </summary>
public static class PowerShell
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_POWERSHELL_DEFAULT;

    /// <summary>
    /// Line comment style index
    /// </summary>
    public const int Comment = SCE_POWERSHELL_COMMENT;

    /// <summary>
    /// String style index.
    /// </summary>
    public const int String = SCE_POWERSHELL_STRING;

    /// <summary>
    /// Character style index.
    /// </summary>
    public const int Character = SCE_POWERSHELL_CHARACTER;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_POWERSHELL_NUMBER;

    /// <summary>
    /// Variable style index.
    /// </summary>
    public const int Variable = SCE_POWERSHELL_VARIABLE;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_POWERSHELL_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_POWERSHELL_IDENTIFIER;

    /// <summary>
    /// Keyword (set 0) style index.
    /// </summary>
    public const int Keyword = SCE_POWERSHELL_KEYWORD;

    /// <summary>
    /// Cmdlet (set 1) style index.
    /// </summary>
    public const int Cmdlet = SCE_POWERSHELL_CMDLET;

    /// <summary>
    /// Alias (set 2) style index.
    /// </summary>
    public const int Alias = SCE_POWERSHELL_ALIAS;

    /// <summary>
    /// Function (set 3) style index.
    /// </summary>
    public const int Function = SCE_POWERSHELL_FUNCTION;

    /// <summary>
    /// User word (set 4) style index.
    /// </summary>
    public const int User1 = SCE_POWERSHELL_USER1;

    /// <summary>
    /// Multi-line comment style index.
    /// </summary>
    public const int CommentStream = SCE_POWERSHELL_COMMENTSTREAM;

    /// <summary>
    /// Here string style index.
    /// </summary>
    public const int HereString = SCE_POWERSHELL_HERE_STRING;

    /// <summary>
    /// Here character style index.
    /// </summary>
    public const int HereCharacter = SCE_POWERSHELL_HERE_CHARACTER;

    /// <summary>
    /// Comment based help keyword style index.
    /// </summary>
    public const int CommentDocKeyword = SCE_POWERSHELL_COMMENTDOCKEYWORD;
}
