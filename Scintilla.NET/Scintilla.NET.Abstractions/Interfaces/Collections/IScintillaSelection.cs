namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// Represents a selection when there are multiple active selections in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaSelection
{
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets or sets the anchor position of the selection.
    /// </summary>
    /// <returns>The zero-based document position of the selection anchor.</returns>
    int Anchor { get; set; }

    /// <summary>
    /// Gets or sets the amount of anchor virtual space.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the selection anchor.</returns>
    int AnchorVirtualSpace { get; set; }

    /// <summary>
    /// Gets or sets the caret position of the selection.
    /// </summary>
    /// <returns>The zero-based document position of the selection caret.</returns>
    int Caret { get; set; }

    /// <summary>
    /// Gets or sets the amount of caret virtual space.
    /// </summary>
    /// <returns>The amount of virtual space past the end of the line offsetting the selection caret.</returns>
    int CaretVirtualSpace { get; set; }

    /// <summary>
    /// Gets or sets the end position of the selection.
    /// </summary>
    /// <returns>The zero-based document position where the selection ends.</returns>
    int End { get; set; }

    /// <summary>
    /// Gets the selection index.
    /// </summary>
    /// <returns>The zero-based selection index within the <see cref="IScintillaSelectionCollection{TSelection}" /> that created it.</returns>
    int Index { get; }

    /// <summary>
    /// Gets or sets the start position of the selection.
    /// </summary>
    /// <returns>The zero-based document position where the selection starts.</returns>
    int Start { get; set; }
}