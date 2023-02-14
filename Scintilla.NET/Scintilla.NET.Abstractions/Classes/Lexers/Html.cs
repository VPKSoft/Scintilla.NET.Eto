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
/// Style constants for use with the <see cref="Lexer.Html" /> lexer.
/// </summary>
public static class Html
{
    /// <summary>
    /// Content style index.
    /// </summary>
    public const int Default = SCE_H_DEFAULT;

    /// <summary>
    /// Tag style index.
    /// </summary>
    public const int Tag = SCE_H_TAG;

    /// <summary>
    /// Unknown tag style index.
    /// </summary>
    public const int TagUnknown = SCE_H_TAGUNKNOWN;

    /// <summary>
    /// Attribute style index.
    /// </summary>
    public const int Attribute = SCE_H_ATTRIBUTE;

    /// <summary>
    /// Unknown attribute style index.
    /// </summary>
    public const int AttributeUnknown = SCE_H_ATTRIBUTEUNKNOWN;

    /// <summary>
    /// Number style index.
    /// </summary>
    public const int Number = SCE_H_NUMBER;

    /// <summary>
    /// Double-quoted string style index.
    /// </summary>
    public const int DoubleString = SCE_H_DOUBLESTRING;

    /// <summary>
    /// Single-quoted string style index.
    /// </summary>
    public const int SingleString = SCE_H_SINGLESTRING;

    /// <summary>
    /// Other tag content (not elements or attributes) style index.
    /// </summary>
    public const int Other = SCE_H_OTHER;

    /// <summary>
    /// Comment style index.
    /// </summary>
    public const int Comment = SCE_H_COMMENT;

    /// <summary>
    /// Entity ($nnn;) name style index.
    /// </summary>
    public const int Entity = SCE_H_ENTITY;

    /// <summary>
    /// End-tag style index.
    /// </summary>
    public const int TagEnd = SCE_H_TAGEND;

    /// <summary>
    /// Start of XML declaration (&lt;?xml&gt;) style index.
    /// </summary>
    public const int XmlStart = SCE_H_XMLSTART;

    /// <summary>
    /// End of XML declaration (?&gt;) style index.
    /// </summary>
    public const int XmlEnd = SCE_H_XMLEND;

    /// <summary>
    /// Script tag (&lt;script&gt;) style index.
    /// </summary>
    public const int Script = SCE_H_SCRIPT;

    /// <summary>
    /// ASP-like script engine block (&lt;%) style index.
    /// </summary>
    public const int Asp = SCE_H_ASP;

    /// <summary>
    /// ASP-like language declaration (&lt;%@) style index.
    /// </summary>
    public const int AspAt = SCE_H_ASPAT;

    /// <summary>
    /// CDATA section style index.
    /// </summary>
    public const int CData = SCE_H_CDATA;

    /// <summary>
    /// Question mark style index.
    /// </summary>
    public const int Question = SCE_H_QUESTION;

    /// <summary>
    /// Value style index.
    /// </summary>
    public const int Value = SCE_H_VALUE;

    /// <summary>
    /// Script engine comment (&lt;%--) style index.
    /// </summary>
    public const int XcComment = SCE_H_XCCOMMENT;
}