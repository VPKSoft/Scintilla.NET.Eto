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

using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions;
using System.Drawing;
using System.Windows.Forms;
using Scintilla.NET.Abstractions.Interfaces.Methods;
using Scintilla.NET.WinForms.Collections;
using Scintilla.NET.WinForms.EventArguments;

namespace Scintilla.NET.WinForms;

/// <summary>
/// Interface IScintillaWinForms
/// Implements the <see cref="Scintilla.NET.Abstractions.IScintillaApi{Scintilla.NET.WinForms.Collections.MarkerCollection, Scintilla.NET.WinForms.Collections.StyleCollection, Scintilla.NET.WinForms.Collections.IndicatorCollection, Scintilla.NET.WinForms.Collections.LineCollection, Scintilla.NET.WinForms.Collections.MarginCollection, Scintilla.NET.WinForms.Collections.SelectionCollection, Scintilla.NET.WinForms.Collections.Marker, Scintilla.NET.WinForms.Collections.Style, Scintilla.NET.WinForms.Collections.Indicator, Scintilla.NET.WinForms.Collections.Line, Scintilla.NET.WinForms.Collections.Margin, Scintilla.NET.WinForms.Collections.Selection, System.Drawing.Image, System.Drawing.Color}" />
/// Implements the <see cref="Scintilla.NET.Abstractions.Interfaces.IScintillaProperties{System.Drawing.Color}" />
/// Implements the <see cref="IScintillaProperties" />
/// Implements the <see cref="Scintilla.NET.Abstractions.Interfaces.IScintillaMethods{System.Drawing.Color, System.Windows.Forms.Keys, System.Drawing.Image}" />
/// Implements the <see cref="Scintilla.NET.Abstractions.Interfaces.IScintillaEvents{System.Windows.Forms.Keys, Scintilla.NET.WinForms.EventArguments.AutoCSelectionEventArgs, Scintilla.NET.WinForms.EventArguments.BeforeModificationEventArgs, Scintilla.NET.WinForms.EventArguments.ModificationEventArgs, Scintilla.NET.WinForms.EventArguments.ChangeAnnotationEventArgs, Scintilla.NET.WinForms.EventArguments.CharAddedEventArgs, Scintilla.NET.WinForms.EventArguments.DoubleClickEventArgs, Scintilla.NET.WinForms.EventArguments.DwellEventArgs, Scintilla.NET.WinForms.EventArguments.CallTipClickEventArgs, Scintilla.NET.WinForms.EventArguments.HotspotClickEventArgs{System.Windows.Forms.Keys}, Scintilla.NET.WinForms.EventArguments.IndicatorClickEventArgs, Scintilla.NET.WinForms.EventArguments.IndicatorReleaseEventArgs, Scintilla.NET.WinForms.EventArguments.InsertCheckEventArgs, Scintilla.NET.WinForms.EventArguments.MarginClickEventArgs, Scintilla.NET.WinForms.NeedShownEventArgs, Scintilla.NET.WinForms.EventArguments.StyleNeededEventArgs, Scintilla.NET.WinForms.EventArguments.UpdateUIEventArgs, Scintilla.NET.WinForms.EventArguments.SCNotificationEventArgs}" />
/// </summary>
/// <seealso cref="Scintilla.NET.Abstractions.IScintillaApi{Scintilla.NET.WinForms.Collections.MarkerCollection, Scintilla.NET.WinForms.Collections.StyleCollection, Scintilla.NET.WinForms.Collections.IndicatorCollection, Scintilla.NET.WinForms.Collections.LineCollection, Scintilla.NET.WinForms.Collections.MarginCollection, Scintilla.NET.WinForms.Collections.SelectionCollection, Scintilla.NET.WinForms.Collections.Marker, Scintilla.NET.WinForms.Collections.Style, Scintilla.NET.WinForms.Collections.Indicator, Scintilla.NET.WinForms.Collections.Line, Scintilla.NET.WinForms.Collections.Margin, Scintilla.NET.WinForms.Collections.Selection, System.Drawing.Image, System.Drawing.Color}" />
/// <seealso cref="Scintilla.NET.Abstractions.Interfaces.IScintillaProperties{System.Drawing.Color}" />
/// <seealso cref="IScintillaProperties" />
/// <seealso cref="Scintilla.NET.Abstractions.Interfaces.IScintillaMethods{System.Drawing.Color, System.Windows.Forms.Keys, System.Drawing.Image}" />
/// <seealso cref="Scintilla.NET.Abstractions.Interfaces.IScintillaEvents{System.Windows.Forms.Keys, Scintilla.NET.WinForms.EventArguments.AutoCSelectionEventArgs, Scintilla.NET.WinForms.EventArguments.BeforeModificationEventArgs, Scintilla.NET.WinForms.EventArguments.ModificationEventArgs, Scintilla.NET.WinForms.EventArguments.ChangeAnnotationEventArgs, Scintilla.NET.WinForms.EventArguments.CharAddedEventArgs, Scintilla.NET.WinForms.EventArguments.DoubleClickEventArgs, Scintilla.NET.WinForms.EventArguments.DwellEventArgs, Scintilla.NET.WinForms.EventArguments.CallTipClickEventArgs, Scintilla.NET.WinForms.EventArguments.HotspotClickEventArgs{System.Windows.Forms.Keys}, Scintilla.NET.WinForms.EventArguments.IndicatorClickEventArgs, Scintilla.NET.WinForms.EventArguments.IndicatorReleaseEventArgs, Scintilla.NET.WinForms.EventArguments.InsertCheckEventArgs, Scintilla.NET.WinForms.EventArguments.MarginClickEventArgs, Scintilla.NET.WinForms.NeedShownEventArgs, Scintilla.NET.WinForms.EventArguments.StyleNeededEventArgs, Scintilla.NET.WinForms.EventArguments.UpdateUIEventArgs, Scintilla.NET.WinForms.EventArguments.SCNotificationEventArgs}" />
public interface IScintillaWinForms: 
    IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection,
        SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>,
    IScintillaProperties<Color>,
    IScintillaProperties,
    IScintillaMethods,
    IScintillaMethodsColor<Color>,
    IScintillaMethodsKeys<Keys>,
    IScintillaMethodsImage<Image>,
    IScintillaEvents<Keys, AutoCSelectionEventArgs, BeforeModificationEventArgs, ModificationEventArgs, ChangeAnnotationEventArgs, CharAddedEventArgs, DoubleClickEventArgs, DwellEventArgs, CallTipClickEventArgs, HotspotClickEventArgs<Keys>, IndicatorClickEventArgs, IndicatorReleaseEventArgs, InsertCheckEventArgs, MarginClickEventArgs, NeedShownEventArgs, StyleNeededEventArgs, UpdateUIEventArgs, SCNotificationEventArgs>
{
}