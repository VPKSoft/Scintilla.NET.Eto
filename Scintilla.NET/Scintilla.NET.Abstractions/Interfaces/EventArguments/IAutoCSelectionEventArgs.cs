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

using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments.Base;
using ScintillaNet.Abstractions.Interfaces.Methods;

namespace ScintillaNet.Abstractions.Interfaces.EventArguments;
/// <summary>
/// Provides data for the Scintilla.AutoCSelection event.
/// </summary>
public interface  IAutoCSelectionEventArgs : IScintillaEventArgs, IPosition
{
    /// <summary>
    /// Gets the fill-up character that caused the completion.
    /// </summary>
    /// <returns>The fill-up character used to cause the completion; otherwise, 0.</returns>
    /// <remarks>Only a <see cref="ListCompletionMethod" /> of <see cref="Enumerations.ListCompletionMethod.FillUp" /> will return a non-zero character.</remarks>
    /// <seealso cref="IScintillaMethods.AutoCSetFillUps" />
    int Char { get; }

    /// <summary>
    /// Gets the text pointer for the <see cref="Text"/> property for event delegation purpose.
    /// </summary>
    /// <value>The text pointer for event delegation purpose..</value>
    public IntPtr TextPtr { get; }

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets a value indicating how the completion occurred.
    /// </summary>
    /// <returns>One of the <see cref="Enumerations.ListCompletionMethod" /> enumeration values.</returns>
    ListCompletionMethod ListCompletionMethod { get; }

    /// <summary>
    /// Gets the start position of the word being completed.
    /// </summary>
    /// <returns>The zero-based document position of the word being completed.</returns>
    new int Position { get; }

    /// <summary>
    /// Gets the text of the selected auto-completion item.
    /// </summary>
    /// <returns>The selected auto-completion item text.</returns>
    string Text { get; }
}