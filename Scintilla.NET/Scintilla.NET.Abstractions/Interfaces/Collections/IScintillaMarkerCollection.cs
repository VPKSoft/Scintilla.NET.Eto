namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// An immutable collection of markers in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaMarkerCollection<out TMarker, TImage, TColor> : IEnumerable<TMarker>, IScintillaMarkerCollectionGeneral
    where TMarker : IScintillaMarker<TImage, TColor>
    where TImage: class
    where TColor: struct
{
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets a <typeparamref name="TMarker"/> object at the specified index.
    /// </summary>
    /// <param name="index">The marker index.</param>
    /// <returns>An object representing the marker at the specified <paramref name="index" />.</returns>
    /// <remarks>Markers 25 through 31 are used by Scintilla for folding.</remarks>
    TMarker this[int index] { get; }
}