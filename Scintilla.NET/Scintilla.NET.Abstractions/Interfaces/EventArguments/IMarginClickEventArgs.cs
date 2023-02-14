﻿#region License
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

using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments.Base;

namespace ScintillaNet.Abstractions.Interfaces.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys, TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs,TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs,TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs,TStyleNeededEventArgs, TUpdateUiEventArgs, TScNotificationEventArgs}.MarginClick" /> events.
/// </summary>
public interface IMarginClickEventArgs<out TKeys>
    : IScintillaEventArgs,
        IModifierKeys<TKeys>,
        IPosition
    where TKeys: Enum
{
    /// <summary>
    /// Gets the margin clicked.
    /// </summary>
    /// <returns>The zero-based index of the clicked margin.</returns>
    int Margin { get; }

    /// <summary>
    /// Gets the zero-based document position where the line adjacent to the clicked margin starts.
    /// </summary>
    /// <returns>The zero-based character position within the document of the start of the line adjacent to the margin clicked.</returns>
    new int Position { get; }

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    IScintillaLineCollectionGeneral LineCollectionGeneral { get; }
}