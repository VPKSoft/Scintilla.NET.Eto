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
/// Style constants for use with the <see cref="Lexer.Css" /> lexer.
/// </summary>
public static class Css
{
    /// <summary>
    /// Default (whitespace) style index.
    /// </summary>
    public const int Default = SCE_CSS_DEFAULT;

    /// <summary>
    /// Tag style index.
    /// </summary>
    public const int Tag = SCE_CSS_TAG;

    /// <summary>
    /// Class style index.
    /// </summary>
    public const int Class = SCE_CSS_CLASS;

    /// <summary>
    /// Pseudo class style index.
    /// </summary>
    public const int PseudoClass = SCE_CSS_PSEUDOCLASS;

    /// <summary>
    /// Unknown pseudo class style index.
    /// </summary>
    public const int UnknownPseudoClass = SCE_CSS_UNKNOWN_PSEUDOCLASS;

    /// <summary>
    /// Operator style index.
    /// </summary>
    public const int Operator = SCE_CSS_OPERATOR;

    /// <summary>
    /// Identifier style index.
    /// </summary>
    public const int Identifier = SCE_CSS_IDENTIFIER;

    /// <summary>
    /// Unknown identifier style index.
    /// </summary>
    public const int UnknownIdentifier = SCE_CSS_UNKNOWN_IDENTIFIER;

    /// <summary>
    /// Value style index.
    /// </summary>
    public const int Value = SCE_CSS_VALUE;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_CSS_COMMENT;

    /// <summary>
    /// ID style index.
    /// </summary>
    public const int Id = SCE_CSS_ID;

    /// <summary>
    /// Important style index.
    /// </summary>
    public const int Important = SCE_CSS_IMPORTANT;

    /// <summary>
    /// Directive style index.
    /// </summary>
    public const int Directive = SCE_CSS_DIRECTIVE;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int DoubleString = SCE_CSS_DOUBLESTRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int SingleString = SCE_CSS_SINGLESTRING;

    /// <summary>
    /// Identifier style 2 index.
    /// </summary>
    public const int Identifier2 = SCE_CSS_IDENTIFIER2;

    /// <summary>
    /// Attribute style index.
    /// </summary>
    public const int Attribute = SCE_CSS_ATTRIBUTE;

    /// <summary>
    /// Identifier style 3 index.
    /// </summary>
    public const int Identifier3 = SCE_CSS_IDENTIFIER3;

    /// <summary>
    /// Pseudo element style index.
    /// </summary>
    public const int PseudoElement = SCE_CSS_PSEUDOELEMENT;

    /// <summary>
    /// Extended identifier style index.
    /// </summary>
    public const int ExtendedIdentifier = SCE_CSS_EXTENDED_IDENTIFIER;

    /// <summary>
    /// Extended pseudo class style index.
    /// </summary>
    public const int ExtendedPseudoClass = SCE_CSS_EXTENDED_PSEUDOCLASS;

    /// <summary>
    /// Extended pseudo element style index.
    /// </summary>
    public const int ExtendedPseudoElement = SCE_CSS_EXTENDED_PSEUDOELEMENT;

    /// <summary>
    /// Media style index.
    /// </summary>
    public const int Media = SCE_CSS_MEDIA;

    /// <summary>
    /// Variable style index.
    /// </summary>
    public const int Variable = SCE_CSS_VARIABLE;
}
