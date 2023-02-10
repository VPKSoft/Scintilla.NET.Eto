using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// An immutable collection of margins in the Scintilla control.
/// </summary>
public interface IScintillaMarginCollection<out TMargin, TColor> : IEnumerable<TMargin>
    where TMargin : IScintillaMargin<TColor>
    where TColor: struct
{
    /// <summary>
    /// Removes all text displayed in every <see cref="MarginType.Text" /> and <see cref="MarginType.RightText" /> margins.
    /// </summary>
    void ClearAllText();

    /// <summary>
    /// Gets or sets the number of margins in the <see cref="IScintillaMarginCollection{TMargin,TColor}" />.
    /// </summary>
    /// <returns>The number of margins in the collection. The default is 5.</returns>
    int Capacity { get; set; }

    /// <summary>
    /// Gets the number of margins in the <see cref="IScintillaMarginCollection{TMargin,TColor}" />.
    /// </summary>
    /// <returns>The number of margins in the collection.</returns>
    /// <remarks>This property is kept for convenience. The return value will always be equal to <see cref="Capacity" />.</remarks>
    /// <seealso cref="Capacity" />
    public int Count { get; }

    /// <summary>
    /// Gets or sets the width in pixels of the left margin padding.
    /// </summary>
    /// <returns>The left margin padding measured in pixels. The default is 1.</returns>
    int Left { get; set; }

    /// <summary>
    /// Gets or sets the width in pixels of the right margin padding.
    /// </summary>
    /// <returns>The right margin padding measured in pixels. The default is 1.</returns>
    int Right { get; set; }

    /// <summary>
    /// Gets a <see cref="IScintillaMargin{TColor}" /> object at the specified index.
    /// </summary>
    /// <param name="index">The margin index.</param>
    /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
    /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
    TMargin this[int index] { get; }
}