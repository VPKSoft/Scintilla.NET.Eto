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
/// Implements the <see cref="global::Scintilla.NET.WinForms.IScintillaWinFormsCollections" />
/// Implements the <see cref="global::Scintilla.NET.Abstractions.Interfaces.IScintillaProperties{TColor}" />
/// Implements the <see cref="IScintillaProperties" />
/// Implements the <see cref="IScintillaMethods" />
/// Implements the <see cref="global::Scintilla.NET.Abstractions.Interfaces.Methods.IScintillaMethodsColor{TColor}" />
/// Implements the <see cref="global::Scintilla.NET.Abstractions.Interfaces.Methods.IScintillaMethodsKeys{TKeys}" />
/// Implements the <see cref="global::Scintilla.NET.Abstractions.Interfaces.Methods.IScintillaMethodsImage{TImage}" />
/// Implements the <see cref="global::Scintilla.NET.WinForms.IScintillaWinFormsEvents" />
/// </summary>
/// <seealso cref="global::Scintilla.NET.WinForms.IScintillaWinFormsCollections" />
/// <seealso cref="global::Scintilla.NET.Abstractions.Interfaces.IScintillaProperties{TColor}" />
/// <seealso cref="IScintillaProperties" />
/// <seealso cref="IScintillaMethods" />
/// <seealso cref="global::Scintilla.NET.Abstractions.Interfaces.Methods.IScintillaMethodsColor{TColor}" />
/// <seealso cref="global::Scintilla.NET.Abstractions.Interfaces.Methods.IScintillaMethodsKeys{TKeys}" />
/// <seealso cref="global::Scintilla.NET.Abstractions.Interfaces.Methods.IScintillaMethodsImage{TImage}" />
/// <seealso cref="global::Scintilla.NET.WinForms.IScintillaWinFormsEvents" />
public interface IScintillaWinForms: 
    IScintillaWinFormsCollections,
    IScintillaProperties<Color>,
    IScintillaProperties,
    IScintillaMethods,
    IScintillaMethodsColor<Color>,
    IScintillaMethodsKeys<Keys>,
    IScintillaMethodsImage<Image>,
    IScintillaWinFormsEvents    
{
}

/// <summary>
/// An interface for the Scintilla WinForms events.
/// Implements the <see cref="global::Scintilla.NET.Abstractions.Interfaces.IScintillaEvents{TKeys, TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs, TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs, TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs, TStyleNeededEventArgs, TUpdateUIEventArgs, TSCNotificationEventArgs}" />
/// </summary>
/// <seealso cref="global::Scintilla.NET.Abstractions.Interfaces.IScintillaEvents{TKeys, TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs, TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs, TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs, TStyleNeededEventArgs, TUpdateUIEventArgs, TSCNotificationEventArgs}" />
public interface IScintillaWinFormsEvents : IScintillaEvents<Keys, AutoCSelectionEventArgs, BeforeModificationEventArgs, ModificationEventArgs, ChangeAnnotationEventArgs, CharAddedEventArgs, DoubleClickEventArgs, DwellEventArgs, CallTipClickEventArgs, HotspotClickEventArgs<Keys>, IndicatorClickEventArgs, IndicatorReleaseEventArgs, InsertCheckEventArgs, MarginClickEventArgs, NeedShownEventArgs, StyleNeededEventArgs, UpdateUIEventArgs, SCNotificationEventArgs>
{

}

/// <summary>
/// An interface for the Scintilla WinForms collections.
/// Implements the <see cref="global::Scintilla.NET.Abstractions.IScintillaApi{TMarkerCollection, TStyleCollection, TIndicatorCollection, TLineCollection, TMarginCollection, TSelectionCollection, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TImage, TColor}" />
/// </summary>
/// <seealso cref="global::Scintilla.NET.Abstractions.IScintillaApi{TMarkerCollection, TStyleCollection, TIndicatorCollection, TLineCollection, TMarginCollection, TSelectionCollection, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TImage, TColor}" />
public interface IScintillaWinFormsCollections : IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection,
    LineCollection, MarginCollection,
    SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{

}