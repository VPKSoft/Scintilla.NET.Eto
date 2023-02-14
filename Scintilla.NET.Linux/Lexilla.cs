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

using System;
using System.Runtime.InteropServices;
using ScintillaNet.Abstractions;

namespace ScintillaNet.Linux;

/// <summary>
/// Linux handler for the Scintilla's Lexilla library.
/// Implements the <see cref="ILexilla" />
/// </summary>
/// <seealso cref="ILexilla" />
public class Lexilla: ILexilla
{
    /// <inheritdoc cref="ILexilla.LexerCount"/>
    public int LexerCount => GetLexerCount();

    /// <summary>
    /// Gets the name of the lexer.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>System.String.</returns>
    public string GetLexerName(uint index)
    {
        var pointer = Marshal.AllocHGlobal(1024);
        GetLexerName(index, pointer, 1024);
        return Marshal.PtrToStringAnsi(pointer) ?? string.Empty;
    }

    /// <inheritdoc />
    public IntPtr CreateLexer(string lexerName)
    {
        var pointer = Marshal.StringToHGlobalAnsi(lexerName);
        var result = CreateLexerDll(pointer);
        Marshal.FreeHGlobal(pointer);
        return result;
    }

    [DllImport("liblexilla", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateLexer")]
    private static extern IntPtr CreateLexerDll(IntPtr lexerName);


    [DllImport("liblexilla", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern int GetLexerCount();

    [DllImport("liblexilla", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void GetLexerName(uint index, IntPtr name, int buflength);
}