using System.Collections;
using ScintillaNet.Abstractions.Interfaces.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// A multiple selection collection.
/// </summary>
public abstract class SelectionCollectionBase<TSelection> : IScintillaSelectionCollection<TSelection>
    where TSelection : SelectionBase
{
    /// <summary>
    /// Provides an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An object that contains all <see cref="SelectionBase" />.</returns>
    public virtual IEnumerator<TSelection> GetEnumerator()
    {
        var count = Count;
        for (var i = 0; i < count; i++)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets the line collection general members.
    /// </summary>
    /// <value>The line collection  general members.</value>
    protected IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets the number of active selections.
    /// </summary>
    /// <returns>The number of selections in the <see cref="SelectionCollectionBase{TSelection}" />.</returns>
    public virtual int Count => ScintillaApi.DirectMessage(SCI_GETSELECTIONS).ToInt32();

    /// <summary>
    /// Gets a value indicating whether all selection ranges are empty.
    /// </summary>
    /// <returns>true if all selection ranges are empty; otherwise, false.</returns>
    public virtual bool IsEmpty => ScintillaApi.DirectMessage(SCI_GETSELECTIONEMPTY) != IntPtr.Zero;

    /// <summary>
    /// Gets the <see cref="SelectionBase" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="SelectionBase" /> to get.</param>
    /// <returns>The <see cref="SelectionBase" /> at the specified index.</returns>
    public abstract TSelection this[int index] { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionCollectionBase{TSelection}" /> class.
    /// </summary>
    /// <param name="scintilla"></param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    public SelectionCollectionBase(IScintillaApi scintilla, IScintillaLineCollectionGeneral lineCollectionGeneral)
    {
        ScintillaApi = scintilla;
        LineCollectionGeneral = lineCollectionGeneral;
    }
}