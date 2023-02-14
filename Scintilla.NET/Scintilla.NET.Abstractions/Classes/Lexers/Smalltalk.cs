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
/// Style constants for use with the <see cref="Lexer.Smalltalk" /> lexer.
/// </summary>
public static class Smalltalk
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_ST_DEFAULT;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_ST_STRING;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_ST_NUMBER;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_ST_COMMENT;

    /// <summary>
    /// Symbol style index.
    /// </summary>
    public const int Symbol = SCE_ST_SYMBOL;

    /// <summary>
    /// Binary style index.
    /// </summary>
    public const int Binary = SCE_ST_BINARY;

    /// <summary>
    /// Bool style index.
    /// </summary>
    public const int Bool = SCE_ST_BOOL;

    /// <summary>
    /// Self style index.
    /// </summary>
    public const int Self = SCE_ST_SELF;

    /// <summary>
    /// Super style index.
    /// </summary>
    public const int Super = SCE_ST_SUPER;

    /// <summary>
    /// NIL style index.
    /// </summary>
    public const int Nil = SCE_ST_NIL;

    /// <summary>
    /// Global style index.
    /// </summary>
    public const int Global = SCE_ST_GLOBAL;

    /// <summary>
    /// Return style index.
    /// </summary>
    public const int Return = SCE_ST_RETURN;

    /// <summary>
    /// Special style index.
    /// </summary>
    public const int Special = SCE_ST_SPECIAL;

    /// <summary>
    /// KWS End style index.
    /// </summary>
    public const int KwsEnd = SCE_ST_KWSEND;

    /// <summary>
    /// Assign style index.
    /// </summary>
    public const int Assign = SCE_ST_ASSIGN;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_ST_CHARACTER;

    /// <summary>
    /// Special selector style index.
    /// </summary>
    public const int SpecSel = SCE_ST_SPEC_SEL;
}