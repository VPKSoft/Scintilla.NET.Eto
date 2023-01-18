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

using Eto.Forms;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.EtoForms.Shared.EventArgs;
using Control = System.Windows.Forms.Control;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.EtoForms.WinForms;

public partial class ScintillaWinForms : Control, IScintillaApi //, CodeEditor.IHandler
{
    #region Fields
    private IntPtr moduleHandle;
    private IntPtr sciPtr;

    private BorderStyle borderStyle;

    // Double-click
    private bool doubleClick;
    #endregion

    public IntPtr SciPointer
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
                sciPtr = NativeMethods.SendMessage(new HandleRef(this, Handle), SCI_GETDIRECTPOINTER,
                    IntPtr.Zero, IntPtr.Zero);
            }

            return sciPtr;
        }
    }

    public ScintillaWinForms()
    {
        base.SetStyle(ControlStyles.UserPaint, false);
        sciPtr = SciPointer;
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
            }

            CreateParams cp = base.CreateParams;
            //cp.ClassName = "ScintillaControl";
            cp.ClassName = "Scintilla";

            // The border effect is achieved through a native Windows style
            cp.ExStyle &= (~WS_EX_CLIENTEDGE);
            cp.Style &= (~WS_BORDER);
            switch (borderStyle)
            {
                case BorderStyle.Fixed3D:
                    cp.ExStyle |= WS_EX_CLIENTEDGE;
                    break;
                case BorderStyle.FixedSingle:
                    cp.Style |= WS_BORDER;
                    break;
            }

            return cp;
        }
    }

    #region WinFormsSpecific
    // WM_DESTROY workaround
    private static bool? reparentAll;
    private bool reparent;

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows Message to process.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case (WM_REFLECT + WM_NOTIFY):
                //WmReflectNotify(ref m);
                break;

            case WM_SETCURSOR:
                DefWndProc(ref m);
                break;

            case WM_LBUTTONDBLCLK:
            case WM_RBUTTONDBLCLK:
            case WM_MBUTTONDBLCLK:
            case WM_XBUTTONDBLCLK:
                doubleClick = true;
                goto default;

            case WM_DESTROY:
                WmDestroy(ref m);
                break;

            default:
                base.WndProc(ref m);
                break;
        }
    }

    private void WmDestroy(ref Message m)
    {
        // WM_DESTROY workaround
        if (reparent && IsHandleCreated)
        {
            // In some circumstances it's possible for the control's window handle to be destroyed
            // and recreated during the life of the control. I have no idea why Windows Forms was coded
            // this way but that creates an issue for us because most/all of our control state is stored
            // in the native Scintilla control (i.e. Handle) and to destroy it will bork us. So, rather
            // than destroying the handle as requested, we "reparent" ourselves to a message-only
            // (invisible) window to keep our handle alive. It doesn't appear that this causes any
            // issues to Windows Forms because it is completely unaware of it. When a control goes through
            // its regular (re)create handle process one of the steps is to assign the parent and so our
            // temporary bait-and-switch gets reconciled again automatically. Our Dispose method ensures
            // that we truly get destroyed when the time is right.

            NativeMethods.SetParent(Handle, new IntPtr(HWND_MESSAGE));
            m.Result = IntPtr.Zero;
            return;
        }

        base.WndProc(ref m);
    }

        //    private void WmReflectNotify(ref Message m)
        //{
        //    // A standard Windows notification and a Scintilla notification header are compatible
        //    ScintillaApiStructs.SCNotification scn = (ScintillaApiStructs.SCNotification)Marshal.PtrToStructure(m.LParam, typeof(ScintillaApiStructs.SCNotification));
        //    if (scn.nmhdr.code >= SCN_STYLENEEDED && scn.nmhdr.code <= SCN_AUTOCCOMPLETED)
        //    {
        //        var handler = Events[scNotificationEventKey] as EventHandler<SCNotificationEventArgs>;
        //        if (handler != null)
        //            handler(this, new SCNotificationEventArgs(scn));

        //        switch (scn.nmhdr.code)
        //        {
        //            case NativeMethods.SCN_PAINTED:
        //                OnPainted(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_MODIFIED:
        //                ScnModified(ref scn);
        //                break;

        //            case NativeMethods.SCN_MODIFYATTEMPTRO:
        //                OnModifyAttempt(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_STYLENEEDED:
        //                OnStyleNeeded(new StyleNeededEventArgs(this, scn.position.ToInt32()));
        //                break;

        //            case NativeMethods.SCN_SAVEPOINTLEFT:
        //                OnSavePointLeft(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_SAVEPOINTREACHED:
        //                OnSavePointReached(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_MARGINCLICK:
        //            case NativeMethods.SCN_MARGINRIGHTCLICK:
        //                ScnMarginClick(ref scn);
        //                break;

        //            case NativeMethods.SCN_UPDATEUI:
        //                OnUpdateUI(new UpdateUIEventArgs((UpdateChange)scn.updated));
        //                break;

        //            case NativeMethods.SCN_CHARADDED:
        //                OnCharAdded(new CharAddedEventArgs(scn.ch));
        //                break;

        //            case NativeMethods.SCN_AUTOCSELECTION:
        //                OnAutoCSelection(new AutoCSelectionEventArgs(this, scn.position.ToInt32(), scn.text, scn.ch, (ListCompletionMethod)scn.listCompletionMethod));
        //                break;

        //            case NativeMethods.SCN_AUTOCCOMPLETED:
        //                OnAutoCCompleted(new AutoCSelectionEventArgs(this, scn.position.ToInt32(), scn.text, scn.ch, (ListCompletionMethod)scn.listCompletionMethod));
        //                break;

        //            case NativeMethods.SCN_AUTOCCANCELLED:
        //                OnAutoCCancelled(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_AUTOCCHARDELETED:
        //                OnAutoCCharDeleted(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_DWELLSTART:
        //                OnDwellStart(new DwellEventArgs(this, scn.position.ToInt32(), scn.x, scn.y));
        //                break;

        //            case NativeMethods.SCN_DWELLEND:
        //                OnDwellEnd(new DwellEventArgs(this, scn.position.ToInt32(), scn.x, scn.y));
        //                break;

        //            case NativeMethods.SCN_DOUBLECLICK:
        //                ScnDoubleClick(ref scn);
        //                break;

        //            case NativeMethods.SCN_NEEDSHOWN:
        //                OnNeedShown(new NeedShownEventArgs(this, scn.position.ToInt32(), scn.length.ToInt32()));
        //                break;

        //            case NativeMethods.SCN_HOTSPOTCLICK:
        //            case NativeMethods.SCN_HOTSPOTDOUBLECLICK:
        //            case NativeMethods.SCN_HOTSPOTRELEASECLICK:
        //                ScnHotspotClick(ref scn);
        //                break;

        //            case NativeMethods.SCN_INDICATORCLICK:
        //            case NativeMethods.SCN_INDICATORRELEASE:
        //                ScnIndicatorClick(ref scn);
        //                break;

        //            case NativeMethods.SCN_ZOOM:
        //                OnZoomChanged(EventArgs.Empty);
        //                break;

        //            case NativeMethods.SCN_CALLTIPCLICK:
        //                OnCallTipClick(new CallTipClickEventArgs(this, (CallTipClickType)scn.position.ToInt32()));
        //                // scn.position: 1 = Up Arrow, 2 = DownArrow: 0 = Elsewhere
        //            break;

        //            default:
        //                // Not our notification
        //                base.WndProc(ref m);
        //                break;
        //        }
        //    }
        //}
    #endregion

    /// <inheritdoc />
    public IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IntPtr DirectMessage(int message)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IntPtr DirectMessage(int message, IntPtr wParam)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IntPtr DirectMessage(int message, IntPtr wParam, IntPtr lParam)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IntPtr DirectMessage(IntPtr scintillaPointer, int message, IntPtr wParam, IntPtr lParam)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void MarkerDeleteAll(int marker)
    {
        throw new NotImplementedException();
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
}

