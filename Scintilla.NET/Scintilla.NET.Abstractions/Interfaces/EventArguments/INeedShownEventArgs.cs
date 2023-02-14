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

using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments.Base;

namespace ScintillaNet.Abstractions.Interfaces.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys, TAutoCSelectionEventArgs, TBeforeModificationEventArgs, TModificationEventArgs, TChangeAnnotationEventArgs, TCharAddedEventArgs,TDoubleClickEventArgs, TDwellEventArgs, TCallTipClickEventArgs, THotspotClickEventArgs, TIndicatorClickEventArgs,TIndicatorReleaseEventArgs, TInsertCheckEventArgs, TMarginClickEventArgs, TNeedShownEventArgs,TStyleNeededEventArgs, TUpdateUiEventArgs, TScNotificationEventArgs}.NeedShown" /> events.
/// </summary>
public interface INeedShownEventArgs : IScintillaEventArgs, IPosition
{

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets the length of the text that needs to be shown.
    /// </summary>
    /// <returns>The length of text starting at <see cref="Position" /> that needs to be shown.</returns>
    int Length { get; }

    /// <summary>
    /// Gets the zero-based document position where text needs to be shown.
    /// </summary>
    /// <returns>The zero-based document position where the range of text to be shown starts.</returns>
    new int Position { get; }
}