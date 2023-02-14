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

using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Methods;
using ScintillaNet.Linux.Collections;
using ScintillaNet.Linux.EventArguments;
using Color = Gdk.Color;
using Image = Gtk.Image;
using Keys = Gdk.Key;

namespace ScintillaNet.Linux;
/// <summary>
/// Interface for the Scintilla Linux control.
/// Implements the <see cref="IScintillaLinuxCollections" />
/// Implements the <see cref="IScintillaProperties" />
/// Implements the <see cref="IScintillaProperties" />
/// Implements the <see cref="IScintillaMethods" />
/// Implements the <see cref="IScintillaMethodsColor{TColor}" />
/// Implements the <see cref="IScintillaMethodsKeys{TKeys}" />
/// Implements the <see cref="IScintillaMethodsImage{TImage}" />
/// Implements the <see cref="IScintillaLinuxEvents" />
/// </summary>
/// <seealso cref="IScintillaLinuxCollections" />
/// <seealso cref="IScintillaProperties" />
/// <seealso cref="IScintillaProperties" />
/// <seealso cref="IScintillaMethods" />
/// <seealso cref="IScintillaMethodsColor{TColor}" />
/// <seealso cref="IScintillaMethodsKeys{TKeys}" />
/// <seealso cref="IScintillaMethodsImage{TImage}" />
/// <seealso cref="IScintillaLinuxEvents" />
public interface IScintillaLinux: 
    IScintillaLinuxCollections,
    IScintillaProperties<Color>,
    IScintillaProperties,
    IScintillaMethods,
    IScintillaMethodsColor<Color>,
    IScintillaMethodsKeys<Keys>,
    IScintillaMethodsImage<Image>,
    IScintillaLinuxEvents,
    IScintillaEvents
{
}

/// <summary>
/// An interface for the Scintilla Linux events.
/// Implements the <see cref="IScintillaEvents" />
/// </summary>
/// <seealso cref="IScintillaEvents" />
public interface IScintillaLinuxEvents : IScintillaEvents<Keys, AutoCSelectionEventArgs, BeforeModificationEventArgs, ModificationEventArgs, ChangeAnnotationEventArgs, CharAddedEventArgs, DoubleClickEventArgs, DwellEventArgs, CallTipClickEventArgs, HotspotClickEventArgs<Keys>, IndicatorClickEventArgs, IndicatorReleaseEventArgs, InsertCheckEventArgs, MarginClickEventArgs, NeedShownEventArgs, StyleNeededEventArgs, UpdateUIEventArgs, SCNotificationEventArgs>
{

}

/// <summary>
/// An interface for the Scintilla Linux collections.
/// Implements the <see cref="IScintillaApi" />
/// </summary>
/// <seealso cref="IScintillaApi" />
public interface IScintillaLinuxCollections : IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection,
    LineCollection, MarginCollection,
    SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{

}