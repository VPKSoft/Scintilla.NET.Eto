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
    /// Style constants for use with the <see cref="Lexer.Ada" /> lexer.
    /// </summary>
    public static class Ada
    {
        /// <summary>
        /// Default (whitespace) style index.
        /// </summary>
        public const int Default = SCE_ADA_DEFAULT;

        /// <summary>
        /// Line comment style index.
        /// </summary>
        public const int CommentLine = SCE_ADA_COMMENTLINE;

        /// <summary>
        /// Number style index.
        /// </summary>
        public const int Number = SCE_ADA_NUMBER;

        /// <summary>
        /// Keyword style index.
        /// </summary>
        public const int Word = SCE_ADA_WORD;

        /// <summary>
        /// Double-quoted string style index.
        /// </summary>
        public const int String = SCE_ADA_STRING;

        /// <summary>
        /// Single-quoted string style index.
        /// </summary>
        public const int Character = SCE_ADA_CHARACTER;

        /// <summary>
        /// Delimiter style index.
        /// </summary>
        public const int Delimiter = SCE_ADA_DELIMITER;

        /// <summary>
        /// Label style index.
        /// </summary>
        public const int Label = SCE_ADA_LABEL;

        /// <summary>
        /// Identifier style index.
        /// </summary>
        public const int Identifier = SCE_ADA_IDENTIFIER;

        /// <summary>
        /// Unclosed string EOL style index.
        /// </summary>
        public const int StringEol = SCE_ADA_STRINGEOL;

        /// <summary>
        /// Unclosed character EOL style index.
        /// </summary>
        public const int CharacterEol = SCE_ADA_CHARACTEREOL;

        /// <summary>
        /// Illegal identifier or keyword style index.
        /// </summary>
        public const int Illegal = SCE_ADA_ILLEGAL;
    }