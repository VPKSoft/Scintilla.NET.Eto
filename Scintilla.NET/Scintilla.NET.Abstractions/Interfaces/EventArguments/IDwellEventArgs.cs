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

using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments.Base;

namespace Scintilla.NET.Abstractions.Interfaces.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys, TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs,TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs,TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs,TStyleNeededEventArgs, TUpdateUiEventArgs, TScNotificationEventArgs}.DwellEnd" /> events.
/// </summary>
public interface IDwellEventArgs : IScintillaEventArgs, IPosition
{
    /// <summary>
    /// Gets the zero-based document position where the mouse pointer was lingering.
    /// </summary>
    /// <returns>The nearest zero-based document position to where the mouse pointer was lingering.</returns>
    new int Position { get; }

    /// <summary>
    /// Gets the x-coordinate of the mouse pointer.
    /// </summary>
    /// <returns>The x-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</returns>
    int X { get; }

    /// <summary>
    /// Gets the y-coordinate of the mouse pointer.
    /// </summary>
    /// <returns>The y-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</returns>
    int Y { get; }

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    IScintillaLineCollectionGeneral LineCollectionGeneral { get; }
}