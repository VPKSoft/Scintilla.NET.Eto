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

using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments.Base;

namespace Scintilla.NET.Abstractions.Interfaces.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys, TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs,TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs,TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs,TStyleNeededEventArgs, TUpdateUiEventArgs, TScNotificationEventArgs}.Insert" /> events.
/// </summary>
public interface IModificationEventArgs
    : IScintillaEventArgs,
        IPosition,
        ICachedText,
        ITextPointerData
{
    /// <summary>
    /// Gets the zero-based document position where the modification will occur.
    /// </summary>
    /// <returns>The zero-based character position within the document where text will be inserted or deleted.</returns>
    new int Position { get; }
    

    /// <summary>
    /// Gets the source of the modification.
    /// </summary>
    /// <returns>One of the <see cref="ModificationSource" /> enum values.</returns>
    ModificationSource Source { get; }

    /// <summary>
    /// Gets the text being inserted or deleted.
    /// </summary>
    /// <returns>
    /// The text about to be inserted or deleted, or null when the the source of the modification is an undo/redo operation.
    /// </returns>
    /// <remarks>
    /// This property will return null when <see cref="Source" /> is <see cref="ModificationSource.Undo" /> or <see cref="ModificationSource.Redo" />.
    /// </remarks>
    string? Text { get; }

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    IScintillaLineCollectionGeneral LineCollectionGeneral { get; }
}