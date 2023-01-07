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

using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Scintilla.NET.EtoForms.WinForms;

public partial class ScintillaWinForms : Control //, CodeEditor.IHandler
{
    private IntPtr moduleHandle;
    private IntPtr sciPtr;

    private BorderStyle borderStyle;
    //private static NativeMethods.Scintilla_DirectFunction directFunction;
    public NativeMethods.Scintilla_DirectFunction directFunction;

    private IntPtr SciPointer
    {
        get
        {
            // Enforce illegal cross-thread calls the way the Handle property does
            if (Control.CheckForIllegalCrossThreadCalls && InvokeRequired)
            {
                string message = string.Format(CultureInfo.InvariantCulture,
                    "Control '{0}' accessed from a thread other than the thread it was created on.", Name);
                throw new InvalidOperationException(message);
            }

            if (sciPtr == IntPtr.Zero)
            {
                // Get a pointer to the native Scintilla object (i.e. C++ 'this') to use with the
                // direct function. This will happen for each Scintilla control instance.
                sciPtr = NativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.SCI_GETDIRECTPOINTER,
                    IntPtr.Zero, IntPtr.Zero);
            }

            return sciPtr;
        }
    }

    public ScintillaWinForms()
    {
        base.SetStyle(ControlStyles.UserPaint, false);
    }

    /// <summary>
    /// Gets the required creation parameters when the control handle is created.
    /// </summary>
    /// <returns>A CreateParams that contains the required creation parameters when the handle to the control is created.</returns>
    protected override CreateParams CreateParams
    {
        get
        {
            if (moduleHandle == IntPtr.Zero)
            {
                var path = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "Scintilla.dll");

                // Load the native Scintilla library
                moduleHandle = NativeMethods.LoadLibrary(path);
                if (moduleHandle == IntPtr.Zero)
                {
                    var message = string.Format(CultureInfo.InvariantCulture,
                        "Could not load the Scintilla module at the path '{0}'.", path);
                    throw new Win32Exception(message, new Win32Exception()); // Calls GetLastError
                }

                // Get the native Scintilla direct function -- the only function the library exports
                var directFunctionPointer =
                    NativeMethods.GetProcAddress(new HandleRef(this, moduleHandle), "Scintilla_DirectFunction");
                if (directFunctionPointer == IntPtr.Zero)
                {
                    var message = "The Scintilla module has no export for the 'Scintilla_DirectFunction' procedure.";
                    throw new Win32Exception(message, new Win32Exception()); // Calls GetLastError
                }

                // Create a managed callback
                directFunction = (NativeMethods.Scintilla_DirectFunction)Marshal.GetDelegateForFunctionPointer(
                    directFunctionPointer,
                    typeof(NativeMethods.Scintilla_DirectFunction));
            }

            CreateParams cp = base.CreateParams;
            //cp.ClassName = "ScintillaControl";
            cp.ClassName = "Scintilla";

            // The border effect is achieved through a native Windows style
            cp.ExStyle &= (~NativeMethods.WS_EX_CLIENTEDGE);
            cp.Style &= (~NativeMethods.WS_BORDER);
            switch (borderStyle)
            {
                case BorderStyle.Fixed3D:
                    cp.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                    break;
                case BorderStyle.FixedSingle:
                    cp.Style |= NativeMethods.WS_BORDER;
                    break;
            }

            return cp;
        }
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case (NativeMethods.WM_REFLECT + NativeMethods.WM_NOTIFY):
                WmReflectNotify(ref m);
                break;

            case NativeMethods.WM_SETCURSOR:
                DefWndProc(ref m);
                break;

            case NativeMethods.WM_LBUTTONDBLCLK:
            case NativeMethods.WM_RBUTTONDBLCLK:
            case NativeMethods.WM_MBUTTONDBLCLK:
            //case NativeMethods.WM_XBUTTONDBLCLK:
            //    doubleClick = true;
            //    goto default;

            //case NativeMethods.WM_DESTROY:
            //    WmDestroy(ref m);
            //    break;

            default:
                base.WndProc(ref m);
                break;
        }
    }

    private void WmReflectNotify(ref Message m)
    {
    }
}

