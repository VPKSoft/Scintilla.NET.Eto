using System.Text;
using Eto.Wpf.Forms;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.EtoForms.Shared;
using Scintilla.NET.EtoForms.WinForms;
using Control = Eto.Forms.Control;

namespace Scintilla.NET.EtoForms.Wpf;

/// <summary>
/// Scintilla control handler for GTK.
/// Implements the <see cref="WindowsFormsHostHandler{TControl,TWidget,TCallback}" />
/// Implements the <see cref="IScintillaControl" />
/// </summary>
/// <seealso cref="WindowsFormsHostHandler{TControl,TWidget,TCallback}" />
/// <seealso cref="IScintillaControl" />

public class ScintillaControlHandler :  WindowsFormsHostHandler<ScintillaWinForms, ScintillaControl, Control.ICallback>, IScintillaControl
{
    readonly IntPtr editor;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ScintillaControlHandler"/> class.
    /// </summary>
    public ScintillaControlHandler()
    {
        var nativeControl = new ScintillaWinForms();
        WinFormsControl = nativeControl;
        Lexilla = LexillaSingleton;
        editor = nativeControl.SciPointer;
    }
    
    private static Lexilla? lexillaInstance;

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
        return WinFormsControl.DirectMessage(editor, message, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int)"/>
    public IntPtr DirectMessage(int msg)
    {
        return WinFormsControl.DirectMessage(msg, IntPtr.Zero, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam)
    {
        return WinFormsControl.DirectMessage(msg, wParam, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        return WinFormsControl.DirectMessage(msg, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
    {
        return WinFormsControl.DirectMessage(sciPtr, msg, wParam, lParam);
    }

    /// <inheritdoc />
    public void MarkerDeleteAll(int marker)
    {
        WinFormsControl.MarkerDeleteAll(marker);
    }

    /// <inheritdoc />
    public Encoding Encoding => WinFormsControl.Encoding;

    /// <inheritdoc />
    public int TextLength => WinFormsControl.TextLength;

    /// <inheritdoc />
    public string GetTextRange(int position, int length)
    {
        return WinFormsControl.GetTextRange(position, length);
    }

    /// <inheritdoc />
    public void FoldAll(FoldAction action)
    {
        WinFormsControl.FoldAll(action);
    }

    /// <summary>
    /// Gets the Lexilla library access.
    /// </summary>
    /// <value>The lexilla library access.</value>
    public ILexilla Lexilla { get; }
}