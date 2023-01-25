#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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

using Scintilla.NET.Abstractions;

namespace Scintilla.NET.EtoForms.Shared;
/// <summary>
/// Provides direct call to the Scintilla native API.
/// </summary>
public static class ScintillaDirectApi
{
    /// <summary>
    /// Initializes static members of the <see cref="ScintillaDirectApi"/> class.
    /// </summary>
    static ScintillaDirectApi()
    {
        ScintillaCall = EmptyCall;
    }

    private static IntPtr EmptyCall(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam)
    {
        throw new NotImplementedException(nameof(ScintillaCall));
    }

    /// <summary>
    /// A delegate for the native Scintilla API call to be used by  <see cref="IScintillaApi.DirectMessage(IntPtr, int, IntPtr, IntPtr)"/>
    /// </summary>
    /// <param name="ptr">The Scintilla control pointer.</param>
    /// <param name="iMessage">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    public delegate IntPtr ScintillaCallDelegate(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Gets or sets the direct native Scintilla API call.
    /// </summary>
    /// <value>The direct native Scintilla API call.</value>
    public static ScintillaCallDelegate ScintillaCall { get; set; }
}