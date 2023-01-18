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
using Scintilla.NET.EtoForms.Shared.EventArgs;

namespace Scintilla.NET.EtoForms.Shared;
public interface IScintillaEventRaising
{
    /// <summary>
    /// Raises the <see cref="Painted" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnPainted(System.EventArgs e);

    void ScnModified(ref ScintillaApiStructs.SCNotification scn);

    /// <summary>
    /// Raises the <see cref="ModifyAttempt" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnModifyAttempt(System.EventArgs e);

    /// <summary>
    /// Raises the <see cref="StyleNeeded" /> event.
    /// </summary>
    /// <param name="e">A <see cref="StyleNeededEventArgs" /> that contains the event data.</param>
    void OnStyleNeeded(StyleNeededEventArgs e);

    /// <summary>
    /// Raises the <see cref="SavePointLeft" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnSavePointLeft(System.EventArgs e);

    /// <summary>
    /// Raises the <see cref="SavePointReached" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnSavePointReached(System.EventArgs e);

    void ScnMarginClick(ref ScintillaApiStructs.SCNotification scn);

    /// <summary>
    /// Raises the <see cref="UpdateUI" /> event.
    /// </summary>
    /// <param name="e">An <see cref="UpdateUIEventArgs" /> that contains the event data.</param>
    void OnUpdateUI(UpdateUIEventArgs e);

    /// <summary>
    /// Raises the <see cref="CharAdded" /> event.
    /// </summary>
    /// <param name="e">A <see cref="CharAddedEventArgs" /> that contains the event data.</param>
    void OnCharAdded(CharAddedEventArgs e);

    /// <summary>
    /// Raises the <see cref="AutoCSelection" /> event.
    /// </summary>
    /// <param name="e">An <see cref="AutoCSelectionEventArgs" /> that contains the event data.</param>
    void OnAutoCSelection(AutoCSelectionEventArgs e);

    /// <summary>
    /// Raises the <see cref="AutoCCompleted" /> event.
    /// </summary>
    /// <param name="e">An <see cref="AutoCSelectionEventArgs" /> that contains the event data.</param>
    void OnAutoCCompleted(AutoCSelectionEventArgs e);

    /// <summary>
    /// Raises the <see cref="AutoCCancelled" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnAutoCCancelled(System.EventArgs e);

    /// <summary>
    /// Raises the <see cref="AutoCCharDeleted" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnAutoCCharDeleted(System.EventArgs e);

    /// <summary>
    /// Raises the <see cref="DwellStart" /> event.
    /// </summary>
    /// <param name="e">A <see cref="DwellEventArgs" /> that contains the event data.</param>
    void OnDwellStart(DwellEventArgs e);

    /// <summary>
    /// Raises the <see cref="DwellEnd" /> event.
    /// </summary>
    /// <param name="e">A <see cref="DwellEventArgs" /> that contains the event data.</param>
    void OnDwellEnd(DwellEventArgs e);

    void ScnDoubleClick(ref ScintillaApiStructs.SCNotification scn);
    
    /// <summary>
    /// Raises the <see cref="NeedShown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="NeedShownEventArgs" /> that contains the event data.</param>
    void OnNeedShown(NeedShownEventArgs e);


    /// <summary>
    /// Class either the OnHotspotClick, OnHotspotDoubleClick or OnHotspotReleaseClick
    /// </summary>
    /// <param name="scn">The SCN.</param>
    void ScnHotspotClick(ref ScintillaApiStructs.SCNotification scn);

    void ScnIndicatorClick(ref ScintillaApiStructs.SCNotification scn);

    /// <summary>
    /// Raises the <see cref="ZoomChanged" /> event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    void OnZoomChanged(System.EventArgs e);

    /// <summary>
    /// Raises the <see cref="CallTipClick" /> event.
    /// </summary>
    /// <param name="e">A <see cref="CallTipClickEventArgs" /> that contains the event data.</param>
    void OnCallTipClick(CallTipClickEventArgs e);

    /// <summary>
    /// Raises the <see cref="HotspotClick" /> event.
    /// </summary>
    /// <param name="e">A <see cref="HotspotClickEventArgs" /> that contains the event data.</param>
    void OnHotspotClick(HotspotClickEventArgs e);
}
