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

namespace Scintilla.NET.Abstractions;

/// <summary>
/// Interface for the Lexilla library.
/// </summary>
public interface ILexilla
{
    /// <summary>
    /// Gets the lexer count.
    /// </summary>
    /// <returns>System.Int32.</returns>
    int LexerCount { get; }

    /// <summary>
    /// Gets the name of the lexer with a specified index.
    /// </summary>
    /// <param name="index">The index of the lexer which name to get.</param>
    /// <returns>The name of the lexer or <c>null</c> is one was not found.</returns>
    string GetLexerName(uint index);

    /// <summary>
    /// Creates a lexer with the specified name.
    /// </summary>
    /// <param name="lexerName">The name of the lexer to create.</param>
    /// <returns>A <see cref="IntPtr"/> containing the lexer interface pointer.</returns>
    IntPtr CreateLexer(string lexerName);
}