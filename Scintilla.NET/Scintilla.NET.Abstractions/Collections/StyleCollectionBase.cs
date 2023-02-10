using System.Collections;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Collections;

/// <summary>
/// An immutable collection of style definitions in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class StyleCollectionBase<TStyle, TColor> : IScintillaStyleCollection<TStyle, TColor>, IScintillaStyleCollectionGeneral
    where TStyle : StyleBase<TColor>
    where TColor: struct
{
    /// <summary>
    /// Provides an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An object that contains all <see cref="StyleBase{TColor}" />.</returns>
    public virtual IEnumerator<TStyle> GetEnumerator()
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
    /// Gets the number of styles.
    /// </summary>
    /// <returns>The number of styles in the <see cref="StyleCollectionBase{TStyle, TColor}" />.</returns>
    public virtual int Count => STYLE_MAX + 1;

    /// <summary>
    /// Gets a <typeparamref name="TStyle"/> object at the specified index.
    /// </summary>
    /// <param name="index">The style definition index.</param>
    /// <returns>An object representing the style definition at the specified <paramref name="index" />.</returns>
    /// <remarks>Styles 32 through 39 have special significance.</remarks>
    public abstract TStyle this[int index] { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleCollectionBase{TStyle, TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="IScintillaApi" /> control that created this collection.</param>
    public StyleCollectionBase(IScintillaApi scintilla)
    {
        ScintillaApi = scintilla;
    }
}