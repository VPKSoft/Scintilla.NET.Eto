using Scintilla.NET.Abstractions.Collections;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// A multiple selection collection.
/// </summary>
public interface IScintillaSelectionCollection<out TSelection> : IEnumerable<TSelection>
    where TSelection : SelectionBase
{
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets the number of active selections.
    /// </summary>
    /// <returns>The number of selections in the <see cref="IScintillaSelectionCollection{TSelection}" />.</returns>
    int Count { get; }

    /// <summary>
    /// Gets a value indicating whether all selection ranges are empty.
    /// </summary>
    /// <returns>true if all selection ranges are empty; otherwise, false.</returns>
    bool IsEmpty { get; }

    /// <summary>
    /// Gets the <see cref="IScintillaSelectionCollection{TSelection}" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="IScintillaSelectionCollection{TSelection}" /> to get.</param>
    /// <returns>The <see cref="IScintillaSelectionCollection{TSelection}" /> at the specified index.</returns>
    TSelection this[int index] { get; }
}