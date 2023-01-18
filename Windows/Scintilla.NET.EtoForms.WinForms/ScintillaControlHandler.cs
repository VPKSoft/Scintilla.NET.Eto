using System.Runtime.InteropServices;
using System.Text;
using Eto.WinForms.Forms;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.EtoForms.Shared;
using Control = Eto.Forms.Control;

namespace Scintilla.NET.EtoForms.WinForms;

/// <summary>
/// Scintilla control handler for GTK.
/// Implements the <see cref="Eto.WinForms.Forms.WindowsControl{TControl, TWidget, TCallback}" />
/// Implements the <see cref="IScintillaControl" />
/// </summary>
/// <seealso cref="Eto.WinForms.Forms.WindowsControl{TControl, TWidget, TCallback}" />
/// <seealso cref="IScintillaControl" />

public class ScintillaControlHandler : WindowsControl<ScintillaWinForms, ScintillaControl, Control.ICallback>, IScintillaControl
{
    /// <summary>
    /// The main entry point allows sending any of the messages described in this document.
    /// </summary>
    /// <param name="ptr">The ScintillaObject pointer.</param>
    /// <param name="iMessage">The message identifier to send to the control.</param>
    /// <param name="wParam">The message <c>wParam</c> field.</param>
    /// <param name="lParam">The message <c>lParam</c> field.</param>
    /// <returns>IntPtr.</returns>
    [DllImport("Scintilla.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Scintilla_DirectFunction(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);


    readonly IntPtr editor;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ScintillaControlHandler"/> class.
    /// </summary>
    public ScintillaControlHandler()
    {
//        editor = SciPointer;
        var nativeControl = new ScintillaWinForms();
        Control = nativeControl;
        Lexilla = LexillaSingleton;
        ScintillaDirectApi.ScintillaCall = Scintilla_DirectFunction;
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
        return ScintillaDirectApi.ScintillaCall(editor, message, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int)"/>
    public IntPtr DirectMessage(int msg)
    {
        return SetParameter(msg, IntPtr.Zero, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam)
    {
        return SetParameter(msg, wParam, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        return SetParameter(msg, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
    {
        return Scintilla_DirectFunction(sciPtr, msg, wParam, lParam);
    }

    /// <inheritdoc />
    public void MarkerDeleteAll(int marker)
    {
    }

    /// <inheritdoc />
    public Encoding Encoding { get; }

    /// <inheritdoc />
    public int TextLength { get; }

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

    /// <inheritdoc cref="IScintillaControl.ReleaseUnmanagedResources" />
    public void ReleaseUnmanagedResources()
    {

    }

    /// <summary>
    /// Gets the Lexilla library access.
    /// </summary>
    /// <value>The lexilla library access.</value>
    public ILexilla Lexilla { get; }
}