namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// An immutable collection of style definitions in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaStyleCollection<out TStyle, TColor> : IEnumerable<TStyle>
    where TStyle : IScintillaStyle<TColor>
    where TColor: struct
{
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets a <typeparamref name="TStyle"/> object at the specified index.
    /// </summary>
    /// <param name="index">The style definition index.</param>
    /// <returns>An object representing the style definition at the specified <paramref name="index" />.</returns>
    /// <remarks>Styles 32 through 39 have special significance.</remarks>
    TStyle this[int index] { get; }
}