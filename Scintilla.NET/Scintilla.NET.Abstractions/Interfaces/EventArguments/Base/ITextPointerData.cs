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

using static System.Net.Mime.MediaTypeNames;

namespace ScintillaNet.Abstractions.Interfaces.EventArguments.Base;

/// <summary>
/// Text pointer data for event delegation.
/// </summary>
public interface ITextPointerData
{
    /// <summary>
    /// Gets the the byte length.
    /// </summary>
    /// <value>The byte length.</value>
    int ByteLength { get; }

    /// <summary>
    /// Gets the text pointer for the <see cref="Text"/> property for event delegation purpose.
    /// </summary>
    /// <value>The text pointer for event delegation purpose..</value>
    IntPtr TextPtr { get; }
}