using System.Runtime.InteropServices;

namespace Scintilla.NET.WinForms;

public static class NativeMethods
{
    #region Constants
    private const string DLL_NAME_KERNEL32 = "kernel32.dll";
    private const string DLL_NAME_OLE32 = "ole32.dll";
    private const string DLL_NAME_USER32 = "user32.dll";
    #endregion

    #region Callbacks

    public delegate nint Scintilla_DirectFunction(nint ptr, int iMessage, nint wParam, nint lParam);

    public delegate nint CreateLexer(string lexerName);

    public delegate void GetLexerName(nuint index, nint name, nint bufferLength);

    public delegate nint GetLexerCount();

    public delegate string LexerNameFromID(nint identifier);

    #endregion Callbacks

    #region Functions

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseClipboard();

    [DllImport(DLL_NAME_KERNEL32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern nint GetProcAddress(HandleRef hModule, string lpProcName);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EmptyClipboard();

    [DllImport(DLL_NAME_KERNEL32, EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern nint LoadLibrary(string lpFileName);

    [DllImport(DLL_NAME_KERNEL32, EntryPoint = "RtlMoveMemory", SetLastError = true)]
    public static extern void MoveMemory(nint dest, nint src, int length);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool OpenClipboard(nint hWndNewOwner);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    public static extern uint RegisterClipboardFormat(string lpszFormat);

    [DllImport(DLL_NAME_OLE32, ExactSpelling = true)]
    public static extern int RevokeDragDrop(nint hwnd);

    [DllImport(DLL_NAME_USER32, EntryPoint = "SendMessageW", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern nint SendMessage(HandleRef hWnd, int msg, nint wParam, nint lParam);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    public static extern nint SetClipboardData(uint uFormat, nint hMem);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    public static extern nint SetParent(nint hWndChild, nint hWndNewParent);

    #endregion Functions
}
