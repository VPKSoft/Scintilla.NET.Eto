using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Structs;

namespace ScintillaNet.Abstractions.Interfaces.Collections;

/// <summary>
/// Represents a line of text in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaLine
{
    #region Methods

    /// <summary>
    /// Expands any parent folds to ensure the line is visible.
    /// </summary>
    void EnsureVisible();

    /// <summary>
    /// Performs the specified fold action on the current line and all child lines.
    /// </summary>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    void FoldChildren(FoldAction action);

    /// <summary>
    /// Performs the specified fold action on the current line.
    /// </summary>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    void FoldLine(FoldAction action);

    /// <summary>
    /// Searches for the next line that has a folding level that is less than or equal to <paramref name="level" />
    /// and returns the previous line index.
    /// </summary>
    /// <param name="level">The level of the line to search for. A value of -1 will use the current line <see cref="FoldLevel" />.</param>
    /// <returns>
    /// The zero-based index of the next line that has a <see cref="FoldLevel" /> less than or equal
    /// to <paramref name="level" />. If the current line is a fold point and <paramref name="level"/> is -1 the
    /// index returned is the last line that would be made visible or hidden by toggling the fold state.
    /// </returns>
    int GetLastChild(int level);

    /// <summary>
    /// Navigates the caret to the start of the line.
    /// </summary>
    /// <remarks>Any selection is discarded.</remarks>
    void Goto();

    /// <summary>
    /// Adds the specified <see cref="MarkerBase{TImage,TColor}" /> to the line.
    /// </summary>
    /// <param name="marker">The zero-based index of the marker to add to the line.</param>
    /// <returns>A <see cref="MarkerHandle" /> which can be used to track the line.</returns>
    /// <remarks>This method does not check if the line already contains the <paramref name="marker" />.</remarks>
    MarkerHandle MarkerAdd(int marker);

    /// <summary>
    /// Adds one or more markers to the line in a single call using a bit mask.
    /// </summary>
    /// <param name="markerMask">An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarginBase{TColor}" /> indexes to add.</param>
    void MarkerAddSet(uint markerMask);

    /// <summary>
    /// Removes the specified <see cref="MarkerBase{TImage, TColor}" /> from the line.
    /// </summary>
    /// <param name="marker">The zero-based index of the marker to remove from the line or -1 to delete all markers from the line.</param>
    /// <remarks>If the same marker has been added to the line more than once, this will delete one copy each time it is used.</remarks>
    void MarkerDelete(int marker);

    /// <summary>
    /// Returns a bit mask indicating which markers are present on the line.
    /// </summary>
    /// <returns>An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarkerBase{TImage, TColor}" /> indexes.</returns>
    uint MarkerGet();

    /// <summary>
    /// Efficiently searches from the current line forward to the end of the document for the specified markers.
    /// </summary>
    /// <param name="markerMask">An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarginBase{TColor}" /> indexes.</param>
    /// <returns>If found, the zero-based line index containing one of the markers in <paramref name="markerMask" />; otherwise, -1.</returns>
    /// <remarks>For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).</remarks>
    int MarkerNext(uint markerMask);

    /// <summary>
    /// Efficiently searches from the current line backward to the start of the document for the specified markers.
    /// </summary>
    /// <param name="markerMask">An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarginBase{TColor}" /> indexes.</param>
    /// <returns>If found, the zero-based line index containing one of the markers in <paramref name="markerMask" />; otherwise, -1.</returns>
    /// <remarks>For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).</remarks>
    int MarkerPrevious(uint markerMask);

    /// <summary>
    /// Toggles the folding state of the line; expanding or contracting all child lines.
    /// </summary>
    /// <remarks>The line must be set as a <see cref="Enumerations.FoldLevelFlags.Header" />.</remarks>
    /// <seealso cref="ToggleFoldShowText"/>
    void ToggleFold();

    /// <summary>
    /// Toggles the folding state of the line; expanding or contracting all child lines, and specifies the text tag to display to the right of the fold.
    /// </summary>
    /// <param name="text">The text tag to show to the right of the folded text.</param>
    /// <remarks>The display of fold text tags are determined by the <see cref="IScintillaMethods{TColor,TKeys,TBitmap}.FoldDisplayTextSetStyle" /> method.</remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.FoldDisplayTextSetStyle" />
    void ToggleFoldShowText(string text);

    #endregion Methods

    #region Properties
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets the number of annotation lines of text.
    /// </summary>
    /// <returns>The number of annotation lines.</returns>
    int AnnotationLines { get; }

    /// <summary>
    /// Gets or sets the style of the annotation text.
    /// </summary>
    /// <returns>
    /// The zero-based index of the annotation text <see cref="StyleBase{TColor}" /> or 256 when <see cref="AnnotationStyles" />
    /// has been used to set individual character styles.
    /// </returns>
    /// <seealso cref="AnnotationStyles" />
    int AnnotationStyle { get; set; }

    /// <summary>
    /// Gets or sets an array of style indexes corresponding to each character in the <see cref="AnnotationText" />
    /// so that each character may be individually styled.
    /// </summary>
    /// <returns>
    /// An array of <see cref="StyleBase{TColor}" /> indexes corresponding with each annotation text character or an uninitialized
    /// array when <see cref="AnnotationStyle" /> has been used to set a single style for all characters.
    /// </returns>
    /// <remarks>
    /// <see cref="AnnotationText" /> must be set prior to setting this property.
    /// The <paramref name="value" /> specified should have a length equal to the <see cref="AnnotationText" /> length to properly style all characters.
    /// </remarks>
    /// <seealso cref="AnnotationStyle" />
    byte[]? AnnotationStyles { get; set; }

    /// <summary>
    /// Gets or sets the line annotation text.
    /// </summary>
    /// <returns>A String representing the line annotation text.</returns>
    string? AnnotationText { get; set; }

    /// <summary>
    /// Searches from the current line to find the index of the next contracted fold header.
    /// </summary>
    /// <returns>The zero-based line index of the next contracted folder header.</returns>
    /// <remarks>If the current line is contracted the current line index is returned.</remarks>
    int ContractedFoldNext { get; }

    /// <summary>
    /// Gets the zero-based index of the line as displayed in a <see cref="ScintillaApi" /> control
    /// taking into consideration folded (hidden) lines.
    /// </summary>
    /// <returns>The zero-based display line index.</returns>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.DocLineFromVisible" />
    int DisplayIndex { get; }

    /// <summary>
    /// Gets the zero-based character position in the document where the line ends (exclusive).
    /// </summary>
    /// <returns>The equivalent of <see cref="Position" /> + <see cref="Length" />.</returns>
    int EndPosition { get; }

    /// <summary>
    /// Gets or sets the expanded state (not the visible state) of the line.
    /// </summary>
    /// <remarks>
    /// For toggling the fold state of a single line the <see cref="ToggleFold" /> method should be used.
    /// This property is useful for toggling the state of many folds without updating the display until finished.
    /// </remarks>
    /// <seealso cref="ToggleFold" />
    bool Expanded { get; set; }

    /// <summary>
    /// Gets or sets the fold level of the line.
    /// </summary>
    /// <returns>The fold level ranging from 0 to 4095. The default is 1024.</returns>
    int FoldLevel { get; set; }

    /// <summary>
    /// Gets or sets the fold level flags.
    /// </summary>
    /// <returns>A bitwise combination of the <see cref="FoldLevelFlags" /> enumeration.</returns>
    FoldLevelFlags FoldLevelFlags { get; set; }

    /// <summary>
    /// Gets the zero-based line index of the first line before the current line that is marked as
    /// <see cref="Enumerations.FoldLevelFlags.Header" /> and has a <see cref="FoldLevel" /> less than the current line.
    /// </summary>
    /// <returns>The zero-based line index of the fold parent if present; otherwise, -1.</returns>
    int FoldParent { get; }

    /// <summary>
    /// Gets the height of the line in pixels.
    /// </summary>
    /// <returns>The height in pixels of the line.</returns>
    /// <remarks>Currently all lines are the same height.</remarks>
    int Height { get; }

    /// <summary>
    /// Gets the line index.
    /// </summary>
    /// <returns>The zero-based line index within the <see cref="LineCollectionBase{TLine}" /> that created it.</returns>
    int Index { get; }

    /// <summary>
    /// Gets the length of the line.
    /// </summary>
    /// <returns>The number of characters in the line including any end of line characters.</returns>
    int Length { get; }

    /// <summary>
    /// Gets or sets the style of the margin text in a <see cref="MarginType.Text" /> or <see cref="MarginType.RightText" /> margin.
    /// </summary>
    /// <returns>
    /// The zero-based index of the margin text <see cref="StyleBase{TColor}" /> or 256 when <see cref="MarginStyles" />
    /// has been used to set individual character styles.
    /// </returns>
    /// <seealso cref="MarginStyles" />
    int MarginStyle { get; set; }

    /// <summary>
    /// Gets or sets an array of style indexes corresponding to each character in the <see cref="MarginText" />
    /// so that each character may be individually styled.
    /// </summary>
    /// <returns>
    /// An array of <see cref="StyleBase{TColor}" /> indexes corresponding with each margin text character or an uninitialized
    /// array when <see cref="MarginStyle" /> has been used to set a single style for all characters.
    /// </returns>
    /// <remarks>
    /// <see cref="MarginText" /> must be set prior to setting this property.
    /// The <paramref name="value" /> specified should have a length equal to the <see cref="MarginText" /> length to properly style all characters.
    /// </remarks>
    /// <seealso cref="MarginStyle" />
    byte[]? MarginStyles { get; set; }

    /// <summary>
    /// Gets or sets the text displayed in the line margin when the margin type is
    /// <see cref="MarginType.Text" /> or <see cref="MarginType.RightText" />.
    /// </summary>
    /// <returns>The text displayed in the line margin.</returns>
    string MarginText { get; set; }

    /// <summary>
    /// Gets the zero-based character position in the document where the line begins.
    /// </summary>
    /// <returns>The document position of the first character in the line.</returns>
    int Position { get; }

    /// <summary>
    /// Gets the line text.
    /// </summary>
    /// <returns>A string representing the document line.</returns>
    /// <remarks>The returned text includes any end of line characters.</remarks>
    string Text { get; }

    /// <summary>
    /// Sets or gets the line indentation.
    /// </summary>
    /// <returns>The indentation measured in character columns, which corresponds to the width of space characters.</returns>
    int Indentation { get; set; }

    /// <summary>
    /// Gets a value indicating whether the line is visible.
    /// </summary>
    /// <returns>true if the line is visible; otherwise, false.</returns>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.ShowLines" />
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.HideLines" />
    bool Visible { get; }

    /// <summary>
    /// Gets the number of display lines this line would occupy when wrapping is enabled.
    /// </summary>
    /// <returns>The number of display lines needed to wrap the current document line.</returns>
    int WrapCount { get; }

    #endregion Properties
}