using System.Runtime.InteropServices;

namespace Scintilla.NET.EtoForms.WinForms;

public static class NativeMethods
{
    #region Constants

    private const string DLL_NAME_KERNEL32 = "kernel32.dll";
    private const string DLL_NAME_OLE32 = "ole32.dll";
    private const string DLL_NAME_USER32 = "user32.dll";


    public const int SCI_GETDIRECTPOINTER = 2185;
    public const int WM_SETCURSOR = 0x0020;
    public const int WM_NOTIFY = 0x004E;
    public const int WM_LBUTTONDBLCLK = 0x0203;
    public const int WM_RBUTTONDBLCLK = 0x0206;
    public const int WM_MBUTTONDBLCLK = 0x0209;
    public const int WM_USER = 0x0400;
    public const int WM_REFLECT = WM_USER + 0x1C00;

    // Window styles
    public const int WS_BORDER = 0x00800000;
    public const int WS_EX_CLIENTEDGE = 0x00000200;

    #endregion Constants

    #region Callbacks

    public delegate IntPtr Scintilla_DirectFunction(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);

    #endregion Callbacks

    #region Functions

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseClipboard();

    [DllImport(DLL_NAME_KERNEL32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EmptyClipboard();

    [DllImport(DLL_NAME_KERNEL32, EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport(DLL_NAME_KERNEL32, EntryPoint = "RtlMoveMemory", SetLastError = true)]
    public static extern void MoveMemory(IntPtr dest, IntPtr src, int length);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    public static extern uint RegisterClipboardFormat(string lpszFormat);

    [DllImport(DLL_NAME_OLE32, ExactSpelling = true)]
    public static extern int RevokeDragDrop(IntPtr hwnd);

    [DllImport(DLL_NAME_USER32, EntryPoint = "SendMessageW", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    #endregion Functions
}