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

using Eto.Forms;

namespace Scintilla.NET.EtoForms.Shared;

/// <summary>
/// An interface for the Scintilla control.
/// Implements the <see cref="Eto.Forms.Control.IHandler" />
/// </summary>
/// <seealso cref="Eto.Forms.Control.IHandler" />
public interface IScintillaControl : Control.IHandler
{
    /// <summary>
    /// The platform-depended implementation of the <see cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/> method.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    /// <remarks>The WParam of the call is set to <see cref="IntPtr.Zero"/>.</remarks>
    /// <remarks>The lParam of the call is set to <see cref="IntPtr.Zero"/>.</remarks>
    IntPtr DirectMessage(int message);
            
    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    /// <remarks>The lParam of the call is set to <see cref="IntPtr.Zero"/>.</remarks>
    IntPtr DirectMessage(int message, IntPtr wParam);

    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    IntPtr DirectMessage(int message, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Sends the specified message directly to the native Scintilla control.
    /// </summary>
    /// <param name="scintillaPointer">The Scintilla control pointer.</param>
    /// <param name="message">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>The message result as <see cref="IntPtr"/>.</returns>
    IntPtr DirectMessage(IntPtr scintillaPointer, int message, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Releases the unmanaged resources after all the Scintilla control instances have been destroyed.
    /// </summary>
    void ReleaseUnmanagedResources();

    /// <summary>
    /// Gets the Lexilla instance.
    /// </summary>
    ILexilla Lexilla { get; }
}