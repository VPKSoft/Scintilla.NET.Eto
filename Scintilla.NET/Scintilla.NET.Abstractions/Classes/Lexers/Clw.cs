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
/// Style constants for use with the <see cref="Lexer.Clw" /> lexer.
/// </summary>
public static class Clw
{
    /// <summary>
    /// Attributes style index
    /// </summary>
    public const int Attributes = SCE_CLW_ATTRIBUTE;

    /// <summary>
    /// Built in procedures function style index.
    /// </summary>
    public const int BuiltInProceduresFunction = SCE_CLW_BUILTIN_PROCEDURES_FUNCTION;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_CLW_COMMENT;

    /// <summary>
    /// Compiler directive style index
    /// </summary>
    public const int CompilerDirective = SCE_CLW_COMPILER_DIRECTIVE;

    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_CLW_DEFAULT;

    /// <summary>
    /// Depreciated style index
    /// </summary>
    public const int Depreciated = SCE_CLW_DEPRECATED;

    /// <summary>
    /// Error style index
    /// </summary>
    public const int Error = SCE_CLW_ERROR;

    /// <summary>
    /// Integer Constant style index.
    /// </summary>
    public const int IntegerConstant = SCE_CLW_INTEGER_CONSTANT;

    /// <summary>
    /// Keyword style index
    /// </summary>
    public const int Keyword = SCE_CLW_KEYWORD;

    /// <summary>
    /// Label string style index.
    /// </summary>
    public const int Label = SCE_CLW_LABEL;

    /// <summary>
    /// Real Constant style index.
    /// </summary>
    public const int PictureString = SCE_CLW_PICTURE_STRING;

    /// <summary>
    /// Real Constant style index.
    /// </summary>
    public const int RealConstant = SCE_CLW_REAL_CONSTANT;

    /// <summary>
    /// Runtime expressions style index
    /// </summary>
    public const int RuntimeExpressions = SCE_CLW_RUNTIME_EXPRESSIONS;

    /// <summary>
    /// Standard equates style index
    /// </summary>
    public const int StandardEquates = SCE_CLW_STANDARD_EQUATE;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int String = SCE_CLW_STRING;

    /// <summary>
    /// Structure data type style index.
    /// </summary>
    public const int StructureDataTypes = SCE_CLW_STRUCTURE_DATA_TYPE;

    /// <summary>
    /// User Identifier style index.
    /// </summary>
    public const int UserIdentifier = SCE_CLW_USER_IDENTIFIER;
}