﻿using Scintilla.NET.Abstractions.Enumerations;
using System.Collections;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Structs;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Collections;

/// <summary>
/// Represents a line of text in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> : IScintillaLine<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMarkers : MarkerCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TStyles : StyleCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TIndicators :IndicatorCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TLines : LineCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMargins : MarginCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TSelections : SelectionCollectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>, IEnumerable
    where TMarker: MarkerBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TStyle : StyleBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TIndicator : IndicatorBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TLine : LineBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TMargin : MarginBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TSelection : SelectionBase<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor>
    where TBitmap: class
    where TColor: struct
{
    #region Methods

    /// <summary>
    /// Expands any parent folds to ensure the line is visible.
    /// </summary>
    public virtual void EnsureVisible()
    {
        ScintillaApi.DirectMessage(SCI_ENSUREVISIBLE, new IntPtr(Index));
    }

    /// <summary>
    /// Performs the specified fold action on the current line and all child lines.
    /// </summary>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    public virtual void FoldChildren(FoldAction action)
    {
        ScintillaApi.DirectMessage(SCI_FOLDCHILDREN, new IntPtr(Index), new IntPtr((int)action));
    }

    /// <summary>
    /// Performs the specified fold action on the current line.
    /// </summary>
    /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
    public virtual void FoldLine(FoldAction action)
    {
        ScintillaApi.DirectMessage(SCI_FOLDLINE, new IntPtr(Index), new IntPtr((int)action));
    }

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
    public virtual int GetLastChild(int level)
    {
        return ScintillaApi.DirectMessage(SCI_GETLASTCHILD, new IntPtr(Index), new IntPtr(level)).ToInt32();
    }

    /// <summary>
    /// Navigates the caret to the start of the line.
    /// </summary>
    /// <remarks>Any selection is discarded.</remarks>
    public virtual void Goto()
    {
        ScintillaApi.DirectMessage(SCI_GOTOLINE, new IntPtr(Index));
    }

    /// <summary>
    /// Adds the specified <see cref="MarkerBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> to the line.
    /// </summary>
    /// <param name="marker">The zero-based index of the marker to add to the line.</param>
    /// <returns>A <see cref="MarkerHandle" /> which can be used to track the line.</returns>
    /// <remarks>This method does not check if the line already contains the <paramref name="marker" />.</remarks>
    public virtual MarkerHandle MarkerAdd(int marker)
    {
        marker = HelpersGeneral.Clamp(marker, 0, ScintillaApi.Markers.Count - 1);
        var handle = ScintillaApi.DirectMessage(SCI_MARKERADD, new IntPtr(Index), new IntPtr(marker));
        return new MarkerHandle { Value = handle };
    }

    /// <summary>
    /// Adds one or more markers to the line in a single call using a bit mask.
    /// </summary>
    /// <param name="markerMask">An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarginBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> indexes to add.</param>
    public virtual void MarkerAddSet(uint markerMask)
    {
        var mask = unchecked((int)markerMask);
        ScintillaApi.DirectMessage(SCI_MARKERADDSET, new IntPtr(Index), new IntPtr(mask));
    }

    /// <summary>
    /// Removes the specified <see cref="MarkerBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> from the line.
    /// </summary>
    /// <param name="marker">The zero-based index of the marker to remove from the line or -1 to delete all markers from the line.</param>
    /// <remarks>If the same marker has been added to the line more than once, this will delete one copy each time it is used.</remarks>
    public virtual void MarkerDelete(int marker)
    {
        marker = HelpersGeneral.Clamp(marker, -1, ScintillaApi.Markers.Count - 1);
        ScintillaApi.DirectMessage(SCI_MARKERDELETE, new IntPtr(Index), new IntPtr(marker));
    }

    /// <summary>
    /// Returns a bit mask indicating which markers are present on the line.
    /// </summary>
    /// <returns>An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarkerBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> indexes.</returns>
    public virtual uint MarkerGet()
    {
        var mask = ScintillaApi.DirectMessage(SCI_MARKERGET, new IntPtr(Index)).ToInt32();
        return unchecked((uint)mask);
    }

    /// <summary>
    /// Efficiently searches from the current line forward to the end of the document for the specified markers.
    /// </summary>
    /// <param name="markerMask">An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarginBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> indexes.</param>
    /// <returns>If found, the zero-based line index containing one of the markers in <paramref name="markerMask" />; otherwise, -1.</returns>
    /// <remarks>For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).</remarks>
    public virtual int MarkerNext(uint markerMask)
    {
        var mask = unchecked((int)markerMask);
        return ScintillaApi.DirectMessage(SCI_MARKERNEXT, new IntPtr(Index), new IntPtr(mask)).ToInt32();
    }

    /// <summary>
    /// Efficiently searches from the current line backward to the start of the document for the specified markers.
    /// </summary>
    /// <param name="markerMask">An unsigned 32-bit value with each bit corresponding to one of the 32 zero-based <see cref="MarginBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> indexes.</param>
    /// <returns>If found, the zero-based line index containing one of the markers in <paramref name="markerMask" />; otherwise, -1.</returns>
    /// <remarks>For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).</remarks>
    public virtual int MarkerPrevious(uint markerMask)
    {
        var mask = unchecked((int)markerMask);
        return ScintillaApi.DirectMessage(SCI_MARKERPREVIOUS, new IntPtr(Index), new IntPtr(mask)).ToInt32();
    }

    /// <summary>
    /// Toggles the folding state of the line; expanding or contracting all child lines.
    /// </summary>
    /// <remarks>The line must be set as a <see cref="Scintilla.NET.Abstractions.Enumerations.FoldLevelFlags.Header" />.</remarks>
    /// <seealso cref="ToggleFoldShowText"/>
    public virtual void ToggleFold()
    {
        ScintillaApi.DirectMessage(SCI_TOGGLEFOLD, new IntPtr(Index));
    }

    /// <summary>
    /// Toggles the folding state of the line; expanding or contracting all child lines, and specifies the text tag to display to the right of the fold.
    /// </summary>
    /// <param name="text">The text tag to show to the right of the folded text.</param>
    /// <remarks>The display of fold text tags are determined by the <see cref="Scintilla.FoldDisplayTextSetStyle" /> method.</remarks>
    /// <seealso cref="Scintilla.FoldDisplayTextSetStyle" />
    public virtual unsafe void ToggleFoldShowText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            ScintillaApi.DirectMessage(SCI_TOGGLEFOLDSHOWTEXT, new IntPtr(Index), IntPtr.Zero);
        }
        else
        {
            var bytes = HelpersGeneral.GetBytes(text, ScintillaApi.Encoding, zeroTerminated: true);
            fixed (byte* bp = bytes)
            {
                ScintillaApi.DirectMessage(SCI_TOGGLEFOLDSHOWTEXT, new IntPtr(Index), new IntPtr(bp));
            }
        }
    }

    /// <inheritdoc />
    public IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> ScintillaApi
    {
        get;
    }

    #endregion Methods

    #region Properties

    /// <summary>
    /// Gets the number of annotation lines of text.
    /// </summary>
    /// <returns>The number of annotation lines.</returns>
    public virtual int AnnotationLines => ScintillaApi.DirectMessage(SCI_ANNOTATIONGETLINES, new IntPtr(Index)).ToInt32();

    /// <summary>
    /// Gets or sets the style of the annotation text.
    /// </summary>
    /// <returns>
    /// The zero-based index of the annotation text <see cref="StyleBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> or 256 when <see cref="AnnotationStyles" />
    /// has been used to set individual character styles.
    /// </returns>
    /// <seealso cref="AnnotationStyles" />
    public virtual int AnnotationStyle
    {
        get
        {
            return ScintillaApi.DirectMessage(SCI_ANNOTATIONGETSTYLE, new IntPtr(Index)).ToInt32();
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.Styles.Count - 1);
            ScintillaApi.DirectMessage(SCI_ANNOTATIONSETSTYLE, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets an array of style indexes corresponding to each character in the <see cref="AnnotationText" />
    /// so that each character may be individually styled.
    /// </summary>
    /// <returns>
    /// An array of <see cref="StyleBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> indexes corresponding with each annotation text character or an uninitialized
    /// array when <see cref="AnnotationStyle" /> has been used to set a single style for all characters.
    /// </returns>
    /// <remarks>
    /// <see cref="AnnotationText" /> must be set prior to setting this property.
    /// The <paramref name="value" /> specified should have a length equal to the <see cref="AnnotationText" /> length to properly style all characters.
    /// </remarks>
    /// <seealso cref="AnnotationStyle" />
    public virtual unsafe byte[]? AnnotationStyles
    {
        get
        {
            var length = ScintillaApi.DirectMessage(SCI_ANNOTATIONGETTEXT, new IntPtr(Index)).ToInt32();
            if (length == 0)
            {
                return new byte[0];
            }

            var text = new byte[length + 1];
            var styles = new byte[length + 1];

            fixed (byte* textPtr = text)
            {
                fixed (byte* stylePtr = styles)
                {
                    ScintillaApi.DirectMessage(SCI_ANNOTATIONGETTEXT, new IntPtr(Index), new IntPtr(textPtr));
                    ScintillaApi.DirectMessage(SCI_ANNOTATIONGETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));

                    return HelpersGeneral.ByteToCharStyles(stylePtr, textPtr, length, ScintillaApi.Encoding);
                }
            }
        }
        set
        {
            var length = ScintillaApi.DirectMessage(SCI_ANNOTATIONGETTEXT, new IntPtr(Index)).ToInt32();
            if (length == 0)
            {
                return;
            }

            var text = new byte[length + 1];
            fixed (byte* textPtr = text)
            {
                ScintillaApi.DirectMessage(SCI_ANNOTATIONGETTEXT, new IntPtr(Index), new IntPtr(textPtr));

                var styles = HelpersGeneral.CharToByteStyles(value ?? Array.Empty<byte>(), textPtr, length, ScintillaApi.Encoding);
                fixed (byte* stylePtr = styles)
                {
                    ScintillaApi.DirectMessage(SCI_ANNOTATIONSETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the line annotation text.
    /// </summary>
    /// <returns>A String representing the line annotation text.</returns>
    public virtual unsafe string? AnnotationText
    {
        get
        {
            var length = ScintillaApi.DirectMessage(SCI_ANNOTATIONGETTEXT, new IntPtr(Index)).ToInt32();
            if (length == 0)
            {
                return string.Empty;
            }

            var bytes = new byte[length + 1];
            fixed (byte* bp = bytes)
            {
                ScintillaApi.DirectMessage(SCI_ANNOTATIONGETTEXT, new IntPtr(Index), new IntPtr(bp));
                return HelpersGeneral.GetString(new IntPtr(bp), length, ScintillaApi.Encoding);
            }
        }
        set
        {
            // allow empty annotation, set to null to remove
            if (value == null)
            {
                // Scintilla docs suggest that setting to NULL rather than an empty string will free memory
                ScintillaApi.DirectMessage(SCI_ANNOTATIONSETTEXT, new IntPtr(Index), IntPtr.Zero);
            }
            else
            {
                var bytes = HelpersGeneral.GetBytes(value, ScintillaApi.Encoding, zeroTerminated: true);
                fixed (byte* bp = bytes)
                {
                    ScintillaApi.DirectMessage(SCI_ANNOTATIONSETTEXT, new IntPtr(Index), new IntPtr(bp));
                }
            }
        }
    }

    /// <summary>
    /// Searches from the current line to find the index of the next contracted fold header.
    /// </summary>
    /// <returns>The zero-based line index of the next contracted folder header.</returns>
    /// <remarks>If the current line is contracted the current line index is returned.</remarks>
    public virtual int ContractedFoldNext => ScintillaApi.DirectMessage(SCI_CONTRACTEDFOLDNEXT, new IntPtr(Index)).ToInt32();

    /// <summary>
    /// Gets the zero-based index of the line as displayed in a <see cref="Scintilla" /> control
    /// taking into consideration folded (hidden) lines.
    /// </summary>
    /// <returns>The zero-based display line index.</returns>
    /// <seealso cref="Scintilla.DocLineFromVisible" />
    public virtual int DisplayIndex => ScintillaApi.DirectMessage(SCI_VISIBLEFROMDOCLINE, new IntPtr(Index)).ToInt32();

    /// <summary>
    /// Gets the zero-based character position in the document where the line ends (exclusive).
    /// </summary>
    /// <returns>The equivalent of <see cref="Position" /> + <see cref="Length" />.</returns>
    public virtual int EndPosition => Position + Length;

    /// <summary>
    /// Gets or sets the expanded state (not the visible state) of the line.
    /// </summary>
    /// <remarks>
    /// For toggling the fold state of a single line the <see cref="ToggleFold" /> method should be used.
    /// This property is useful for toggling the state of many folds without updating the display until finished.
    /// </remarks>
    /// <seealso cref="ToggleFold" />
    public virtual bool Expanded
    {
        get
        {
            return ScintillaApi.DirectMessage(SCI_GETFOLDEXPANDED, new IntPtr(Index)) != IntPtr.Zero;
        }
        set
        {
            var expanded = value ? new IntPtr(1) : IntPtr.Zero;
            ScintillaApi.DirectMessage(SCI_SETFOLDEXPANDED, new IntPtr(Index), expanded);
        }
    }

    /// <summary>
    /// Gets or sets the fold level of the line.
    /// </summary>
    /// <returns>The fold level ranging from 0 to 4095. The default is 1024.</returns>
    public virtual int FoldLevel
    {
        get
        {
            var level = ScintillaApi.DirectMessage(SCI_GETFOLDLEVEL, new IntPtr(Index)).ToInt32();
            return level & SC_FOLDLEVELNUMBERMASK;
        }
        set
        {
            var bits = (int)FoldLevelFlags;
            bits |= value;

            ScintillaApi.DirectMessage(SCI_SETFOLDLEVEL, new IntPtr(Index), new IntPtr(bits));
        }
    }

    /// <summary>
    /// Gets or sets the fold level flags.
    /// </summary>
    /// <returns>A bitwise combination of the <see cref="FoldLevelFlags" /> enumeration.</returns>
    public virtual FoldLevelFlags FoldLevelFlags
    {
        get
        {
            var flags = ScintillaApi.DirectMessage(SCI_GETFOLDLEVEL, new IntPtr(Index)).ToInt32();
            return (FoldLevelFlags)(flags & ~SC_FOLDLEVELNUMBERMASK);
        }
        set
        {
            var bits = FoldLevel;
            bits |= (int)value;

            ScintillaApi.DirectMessage(SCI_SETFOLDLEVEL, new IntPtr(Index), new IntPtr(bits));
        }
    }

    /// <summary>
    /// Gets the zero-based line index of the first line before the current line that is marked as
    /// <see cref="Scintilla.NET.Abstractions.Enumerations.FoldLevelFlags.Header" /> and has a <see cref="FoldLevel" /> less than the current line.
    /// </summary>
    /// <returns>The zero-based line index of the fold parent if present; otherwise, -1.</returns>
    public virtual int FoldParent => ScintillaApi.DirectMessage(SCI_GETFOLDPARENT, new IntPtr(Index)).ToInt32();

    /// <summary>
    /// Gets the height of the line in pixels.
    /// </summary>
    /// <returns>The height in pixels of the line.</returns>
    /// <remarks>Currently all lines are the same height.</remarks>
    public virtual int Height => ScintillaApi.DirectMessage(SCI_TEXTHEIGHT, new IntPtr(Index)).ToInt32();

    /// <summary>
    /// Gets the line index.
    /// </summary>
    /// <returns>The zero-based line index within the <see cref="LineCollectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> that created it.</returns>
    public int Index { get; private set; }

    /// <summary>
    /// Gets the length of the line.
    /// </summary>
    /// <returns>The number of characters in the line including any end of line characters.</returns>
    public virtual int Length => ScintillaApi.Lines.CharLineLength(Index);

    /// <summary>
    /// Gets or sets the style of the margin text in a <see cref="MarginType.Text" /> or <see cref="MarginType.RightText" /> margin.
    /// </summary>
    /// <returns>
    /// The zero-based index of the margin text <see cref="StyleBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> or 256 when <see cref="MarginStyles" />
    /// has been used to set individual character styles.
    /// </returns>
    /// <seealso cref="MarginStyles" />
    public virtual int MarginStyle
    {
        get
        {
            return ScintillaApi.DirectMessage(SCI_MARGINGETSTYLE, new IntPtr(Index)).ToInt32();
        }
        set
        {
            value = HelpersGeneral.Clamp(value, 0, ScintillaApi.Styles.Count - 1);
            ScintillaApi.DirectMessage(SCI_MARGINSETSTYLE, new IntPtr(Index), new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets an array of style indexes corresponding to each character in the <see cref="MarginText" />
    /// so that each character may be individually styled.
    /// </summary>
    /// <returns>
    /// An array of <see cref="StyleBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> indexes corresponding with each margin text character or an uninitialized
    /// array when <see cref="MarginStyle" /> has been used to set a single style for all characters.
    /// </returns>
    /// <remarks>
    /// <see cref="MarginText" /> must be set prior to setting this property.
    /// The <paramref name="value" /> specified should have a length equal to the <see cref="MarginText" /> length to properly style all characters.
    /// </remarks>
    /// <seealso cref="MarginStyle" />
    public virtual unsafe byte[]? MarginStyles
    {
        get
        {
            var length = ScintillaApi.DirectMessage(SCI_MARGINGETTEXT, new IntPtr(Index)).ToInt32();
            if (length == 0)
            {
                return Array.Empty<byte>();
            }

            var text = new byte[length + 1];
            var styles = new byte[length + 1];

            fixed (byte* textPtr = text)
            {
                fixed (byte* stylePtr = styles)
                {
                    ScintillaApi.DirectMessage(SCI_MARGINGETTEXT, new IntPtr(Index), new IntPtr(textPtr));
                    ScintillaApi.DirectMessage(SCI_MARGINGETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));

                    return HelpersGeneral.ByteToCharStyles(stylePtr, textPtr, length, ScintillaApi.Encoding);
                }
            }
        }
        set
        {
            var length = ScintillaApi.DirectMessage(SCI_MARGINGETTEXT, new IntPtr(Index)).ToInt32();
            if (length == 0)
            {
                return;
            }

            var text = new byte[length + 1];
            fixed (byte* textPtr = text)
            {
                ScintillaApi.DirectMessage(SCI_MARGINGETTEXT, new IntPtr(Index), new IntPtr(textPtr));

                var styles = HelpersGeneral.CharToByteStyles(value ?? Array.Empty<byte>(), textPtr, length, ScintillaApi.Encoding);
                fixed (byte* stylePtr = styles)
                {
                    ScintillaApi.DirectMessage(SCI_MARGINSETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the text displayed in the line margin when the margin type is
    /// <see cref="MarginType.Text" /> or <see cref="MarginType.RightText" />.
    /// </summary>
    /// <returns>The text displayed in the line margin.</returns>
    public virtual unsafe string MarginText
    {
        get
        {
            var length = ScintillaApi.DirectMessage(SCI_MARGINGETTEXT, new IntPtr(Index)).ToInt32();
            if (length == 0)
            {
                return string.Empty;
            }

            var bytes = new byte[length + 1];
            fixed (byte* bp = bytes)
            {
                ScintillaApi.DirectMessage(SCI_MARGINGETTEXT, new IntPtr(Index), new IntPtr(bp));
                return HelpersGeneral.GetString(new IntPtr(bp), length, ScintillaApi.Encoding);
            }
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                // Scintilla docs suggest that setting to NULL rather than an empty string will free memory
                ScintillaApi.DirectMessage(SCI_MARGINSETTEXT, new IntPtr(Index), IntPtr.Zero);
            }
            else
            {
                var bytes = HelpersGeneral.GetBytes(value, ScintillaApi.Encoding, zeroTerminated: true);
                fixed (byte* bp = bytes)
                {
                    ScintillaApi.DirectMessage(SCI_MARGINSETTEXT, new IntPtr(Index), new IntPtr(bp));
                }
            }
        }
    }

    /// <summary>
    /// Gets the zero-based character position in the document where the line begins.
    /// </summary>
    /// <returns>The document position of the first character in the line.</returns>
    public virtual int Position => ScintillaApi.Lines.CharPositionFromLine(Index);

    /// <summary>
    /// Gets the line text.
    /// </summary>
    /// <returns>A string representing the document line.</returns>
    /// <remarks>The returned text includes any end of line characters.</remarks>
    public virtual unsafe string Text
    {
        get
        {
            var start = ScintillaApi.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(Index));
            var length = ScintillaApi.DirectMessage(SCI_LINELENGTH, new IntPtr(Index));
            var ptr = ScintillaApi.DirectMessage(SCI_GETRANGEPOINTER, start, length);
            if (ptr == IntPtr.Zero)
            {
                return string.Empty;
            }

            var text = new string((sbyte*)ptr, 0, length.ToInt32(), ScintillaApi.Encoding);
            return text;
        }
    }

    /// <summary>
    /// Sets or gets the line indentation.
    /// </summary>
    /// <returns>The indentation measured in character columns, which corresponds to the width of space characters.</returns>
    public virtual int Indentation
    {
        get => ScintillaApi.DirectMessage(SCI_GETLINEINDENTATION, new IntPtr(Index)).ToInt32();
        set => ScintillaApi.DirectMessage(SCI_SETLINEINDENTATION, new IntPtr(Index), new IntPtr(value));
    }

    /// <summary>
    /// Gets a value indicating whether the line is visible.
    /// </summary>
    /// <returns>true if the line is visible; otherwise, false.</returns>
    /// <seealso cref="Scintilla.ShowLines" />
    /// <seealso cref="Scintilla.HideLines" />
    public virtual bool Visible => ScintillaApi.DirectMessage(SCI_GETLINEVISIBLE, new IntPtr(Index)) != IntPtr.Zero;

    /// <summary>
    /// Gets the number of display lines this line would occupy when wrapping is enabled.
    /// </summary>
    /// <returns>The number of display lines needed to wrap the current document line.</returns>
    public virtual int WrapCount => ScintillaApi.DirectMessage(SCI_WRAPCOUNT, new IntPtr(Index)).ToInt32();

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LineBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this line.</param>
    /// <param name="index">The index of this line within the <see cref="LineCollectionBase{TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor}" /> that created it.</param>
    protected LineBase(IScintillaApi<TMarkers, TStyles, TIndicators, TLines, TMargins, TSelections, TMarker, TStyle, TIndicator, TLine, TMargin, TSelection, TBitmap, TColor> scintilla, int index)
    {
        this.ScintillaApi = scintilla;
        Index = index;
    }

    #endregion Constructors
}