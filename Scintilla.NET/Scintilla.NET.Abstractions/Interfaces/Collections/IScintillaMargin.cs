using Scintilla.NET.Abstractions.Classes;
using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// Represents a margin displayed on the left edge of a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaMargin<TColor>
    where TColor : struct
{
    #region Properties
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets or sets the background color of the margin when the <see cref="Type" /> property is set to <see cref="MarginType.Color" />.
    /// </summary>
    /// <returns>A Color object representing the margin background color. The default is Black.</returns>
    /// <remarks>Alpha color values are ignored.</remarks>
    TColor BackColor { get; set; }

    /// <summary>
    /// Gets or sets the mouse cursor style when over the margin.
    /// </summary>
    /// <returns>One of the <see cref="MarginCursor" /> enumeration values. The default is <see cref="MarginCursor.Arrow" />.</returns>
    MarginCursor Cursor { get; set; }

    /// <summary>
    /// Gets the zero-based margin index this object represents.
    /// </summary>
    /// <returns>The margin index within the <see cref="IScintillaMarginCollection{TMargins,TColor}" />.</returns>
    int Index { get; }

    /// <summary>
    /// Gets or sets whether the margin is sensitive to mouse clicks.
    /// </summary>
    /// <returns>true if the margin is sensitive to mouse clicks; otherwise, false. The default is false.</returns>
    /// <seealso cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.MarginClick" />
    bool Sensitive { get; set; }

    /// <summary>
    /// Gets or sets the margin type.
    /// </summary>
    /// <returns>One of the <see cref="MarginType" /> enumeration values. The default is <see cref="MarginType.Symbol" />.</returns>
    MarginType Type { get; set; }

    /// <summary>
    /// Gets or sets the width in pixels of the margin.
    /// </summary>
    /// <returns>The width of the margin measured in pixels.</returns>
    /// <remarks>Scintilla assigns various default widths.</remarks>
    int Width { get; set; }

    /// <summary>
    /// Gets or sets a mask indicating which markers this margin can display.
    /// </summary>
    /// <returns>
    /// An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="IScintillaMargin{TColor}" /> indexes.
    /// The default is 0x1FFFFFF, which is every marker except folder markers (i.e. 0 through 24).
    /// </returns>
    /// <remarks>
    /// For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).
    /// <see cref="MarkerConstants.MaskFolders" /> is a useful constant for working with just folder margin indexes.
    /// </remarks>
    uint Mask { get; set; }
    #endregion Properties
}