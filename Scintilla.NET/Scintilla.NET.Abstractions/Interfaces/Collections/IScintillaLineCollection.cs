using Scintilla.NET.Abstractions.Collections;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// An immutable collection of lines of text in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaLineCollection<out TLine> : IEnumerable<TLine>, IScintillaLineCollectionGeneral
    where TLine : LineBase
{
    #region Properties
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets the <see cref="LineBase" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="LineBase" /> to get.</param>
    /// <returns>The <see cref="LineBase" /> at the specified index.</returns>
    TLine this[int index] { get; }

    #endregion Properties
}