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

using System.Text;
using Eto.Forms;
using Eto.GtkSharp.Forms;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Extensions;
using Scintilla.NET.EtoForms.Shared;

namespace Scintilla.NET.EtoForms.GTK;

/// <summary>
/// Scintilla control handler for GTK.
/// Implements the <see cref="Eto.GtkSharp.Forms.GtkControl{TControl, TWidget, TCallback}" />
/// Implements the <see cref="IScintillaControl" />
/// </summary>
/// <seealso cref="Eto.GtkSharp.Forms.GtkControl{TControl, TWidget, TCallback}" />
/// <seealso cref="IScintillaControl" />
public class ScintillaControlHandler : GtkControl<ScintillaGtk, ScintillaControl, Control.ICallback>, IScintillaControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScintillaControlHandler"/> class.
    /// </summary>
    public ScintillaControlHandler()
    {
        var nativeControl = new ScintillaGtk();
        Control = nativeControl;
    }
    

    private static ILexilla? lexillaInstance;

    /// <summary>
    /// Gets the singleton instance of the <see cref="Lexilla"/> class.
    /// </summary>
    /// <value>The singleton instance of the <see cref="Lexilla"/> class.</value>
    private static ILexilla LexillaSingleton
    {
        get
        {
            lexillaInstance ??= new Lexilla();
            return lexillaInstance;
        }
    }

    /// <inheritdoc cref="IScintillaControl.SetParameter"/>
    public IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam)
    {
        return Control.SetParameter(message, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int)"/>
    public IntPtr DirectMessage(int msg)
    {
        return Control.DirectMessage(msg);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam)
    {
        return Control.DirectMessage(msg, wParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        return Control.DirectMessage(msg, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
    {
        return Control.DirectMessage(sciPtr, msg, wParam, lParam);
    }

    /// <inheritdoc />
    public void MarkerDeleteAll(int marker)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string GetTextRange(int position, int length)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void FoldAll(FoldAction action)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0)
    {
        this.InitDocumentExtension(eolMode, useTabs, tabWidth, indentWidth);
    }

    /// <inheritdoc />
    public int TextLength { get; }

    /// <inheritdoc cref="IScintillaControl.ReleaseUnmanagedResources" />
    public void ReleaseUnmanagedResources()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the Lexilla library access.
    /// </summary>
    /// <value>The lexilla library access.</value>
    public ILexilla Lexilla => LexillaSingleton;

    /// <inheritdoc />
    public Encoding Encoding => Control.Encoding;
}