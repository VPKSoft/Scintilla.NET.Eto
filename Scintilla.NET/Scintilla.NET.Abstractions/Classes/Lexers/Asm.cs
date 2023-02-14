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
/// Style constants for use with the <see cref="Lexer.Asm" /> lexer.
/// </summary>
public static class Asm
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_ASM_DEFAULT;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_ASM_COMMENT;

    /// <summary>
    /// Comment block style index.
    /// </summary>
    public const int CommentBlock = SCE_ASM_COMMENTBLOCK;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_ASM_NUMBER;

    /// <summary>
    /// Math instruction (keword list 1) style index.
    /// </summary>
    public const int MathInstruction = SCE_ASM_MATHINSTRUCTION;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int String = SCE_ASM_STRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int Character = SCE_ASM_CHARACTER;

    /// <summary>
    /// CPU instruction (keyword list 0) style index.
    /// </summary>
    public const int CpuInstruction = SCE_ASM_CPUINSTRUCTION;

    /// <summary>
    /// Register (keyword list 2) style index.
    /// </summary>
    public const int Register = SCE_ASM_REGISTER;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_ASM_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_ASM_IDENTIFIER;

    /// <summary>
    /// Unclosed string EOL style index.
    /// </summary>
    public const int StringEol = SCE_ASM_STRINGEOL;

    /// <summary>
    /// Directive (keyword list 3) string style index.
    /// </summary>
    public const int Directive = SCE_ASM_DIRECTIVE;

    /// <summary>
    /// Directive operand (keyword list 4) style index.
    /// </summary>
    public const int DirectiveOperand = SCE_ASM_DIRECTIVEOPERAND;

    /// <summary>
    /// Extended instruction (keyword list 5) style index.
    /// </summary>
    public const int ExtInstruction = SCE_ASM_EXTINSTRUCTION;

    /// <summary>
    /// Comment directive style index.
    /// </summary>
    public const int CommentDirective = SCE_ASM_COMMENTDIRECTIVE;
}