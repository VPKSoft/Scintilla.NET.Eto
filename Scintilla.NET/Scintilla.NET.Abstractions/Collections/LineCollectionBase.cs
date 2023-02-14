using System.Collections;
using System.Diagnostics;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments;
using ScintillaNet.Abstractions.Structs;
using ScintillaNet.Abstractions.UtilityClasses;
using static ScintillaNet.Abstractions.ScintillaConstants;
using static ScintillaNet.Abstractions.Classes.ScintillaApiStructs;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// An immutable collection of lines of text in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class LineCollectionBase<TLine> : IScintillaLineCollection<TLine>
    where TLine : LineBase
{
    #region Fields    
    /// <summary>
    /// Data per Scintilla control line.
    /// </summary>
    protected GapBuffer<PerLine> PerLineData { get; set; }

    // The 'step' is a break in the continuity of our line starts. It allows us
    // to delay the updating of every line start when text is inserted/deleted.
    private int stepLine;
    private int stepLength;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Adjust the number of CHARACTERS in a line.
    /// </summary>
    public virtual void AdjustLineLength(int index, int delta)
    {
        MoveStep(index);
        stepLength += delta;

        // Invalidate multi-byte flag
        var perLine = PerLineData[index];
        perLine.ContainsMultiByte = ContainsMultiByte.Unknown;
        PerLineData[index] = perLine;
    }

    /// <summary>
    /// Converts a BYTE offset to a CHARACTER offset.
    /// </summary>
    public virtual int ByteToCharPosition(int pos)
    {
        Debug.Assert(pos >= 0);
        Debug.Assert(pos <= ScintillaApi.DirectMessage(SCI_GETLENGTH).ToInt32());

        var line = ScintillaApi.DirectMessage(SCI_LINEFROMPOSITION, new IntPtr(pos)).ToInt32();
        var byteStart = ScintillaApi.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(line)).ToInt32();
        var count = CharPositionFromLine(line) + GetCharCount(byteStart, pos - byteStart);

        return count;
    }

    /// <summary>
    /// Returns the number of CHARACTERS in a line.
    /// </summary>
    public virtual int CharLineLength(int index)
    {
        Debug.Assert(index >= 0);
        Debug.Assert(index < Count);

        // A line's length is calculated by subtracting its start offset from
        // the start of the line following. We keep a terminal (faux) line at
        // the end of the list so we can calculate the length of the last line.

        if (index + 1 <= stepLine)
        {
            return PerLineData[index + 1].Start - PerLineData[index].Start;
        }
        else if (index <= stepLine)
        {
            return PerLineData[index + 1].Start + stepLength - PerLineData[index].Start;
        }
        else
        {
            return PerLineData[index + 1].Start + stepLength - (PerLineData[index].Start + stepLength);
        }
    }

    /// <summary>
    /// Returns the CHARACTER offset where the line begins.
    /// </summary>
    public virtual int CharPositionFromLine(int index)
    {
        Debug.Assert(index >= 0);
        Debug.Assert(index < PerLineData.Count); // Allow query of terminal line start

        var start = PerLineData[index].Start;
        if (index > stepLine)
        {
            start += stepLength;
        }

        return start;
    }

    /// <summary>
    /// Gets the byte position from the specified character position.
    /// </summary>
    /// <param name="pos">The character position within the document.</param>
    /// <returns>The byte position of the specified character position.</returns>
    public virtual int CharToBytePosition(int pos)
    {
        Debug.Assert(pos >= 0);
        Debug.Assert(pos <= TextLength);

        // Adjust to the nearest line start
        var line = LineFromCharPosition(pos);
        var bytePos = ScintillaApi.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(line)).ToInt32();
        pos -= CharPositionFromLine(line);

        // Optimization when the line contains NO multibyte characters
        if (!LineContainsMultiByteChar(line))
        {
            return bytePos + pos;
        }

        while (pos > 0)
        {
            // Move char-by-char
            bytePos = ScintillaApi.DirectMessage(SCI_POSITIONRELATIVE, new IntPtr(bytePos), new IntPtr(1)).ToInt32();
            pos--;
        }

        return bytePos;
    }

    /// <summary>
    /// Deletes the specified line characters specified by the line index.
    /// </summary>
    /// <param name="index">The line index.</param>
    public virtual void DeletePerLine(int index)
    {
        Debug.Assert(index != 0);

        MoveStep(index);

        // Subtract the line length
        stepLength -= CharLineLength(index);

        // Remove the line
        PerLineData.RemoveAt(index);

        // Move the step to the line before the one removed
        stepLine--;
    }

    /// <summary>
    /// Gets the number of CHARACTERS int a BYTE range.
    /// </summary>
    public virtual int GetCharCount(int pos, int length)
    {
        var ptr = ScintillaApi.DirectMessage(SCI_GETRANGEPOINTER, new IntPtr(pos), new IntPtr(length));
        return HelpersGeneral.GetCharCount(ptr, length, ScintillaApi.Encoding);
    }

    /// <summary>
    /// Provides an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An object that contains all <see cref="LineBase" />.</returns>
    public IEnumerator<TLine> GetEnumerator()
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
    /// Gets a value indicating whether a line specified by its index contains multi-byte character(s).
    /// </summary>
    /// <param name="index">The line index.</param>
    /// <returns><c>true</c> if the line specified by its index contains multi-byte character(s), <c>false</c> otherwise.</returns>
    public virtual bool LineContainsMultiByteChar(int index)
    {
        var perLine = PerLineData[index];
        if (perLine.ContainsMultiByte == ContainsMultiByte.Unknown)
        {
            perLine.ContainsMultiByte =
                ScintillaApi.DirectMessage(SCI_LINELENGTH, new IntPtr(index)).ToInt32() == CharLineLength(index)
                    ? ContainsMultiByte.No
                    : ContainsMultiByte.Yes;

            PerLineData[index] = perLine;
        }

        return perLine.ContainsMultiByte == ContainsMultiByte.Yes;
    }

    /// <summary>
    /// Returns the line index containing the CHARACTER position.
    /// </summary>
    public virtual int LineFromCharPosition(int pos)
    {
        Debug.Assert(pos >= 0);

        // Iterative binary search
        // http://en.wikipedia.org/wiki/Binary_search_algorithm
        // System.Collections.Generic.ArraySortHelper.InternalBinarySearch

        var low = 0;
        var high = Count - 1;

        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            var start = CharPositionFromLine(mid);

            if (pos == start)
            {
                return mid;
            }
            else if (start < pos)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        // After while exit, 'low' will point to the index where 'pos' should be
        // inserted (if we were creating a new line start). The line containing
        // 'pos' then would be 'low - 1'.
        return low - 1;
    }

    /// <summary>
    /// Tracks a new line with the given CHARACTER length.
    /// </summary>
    public virtual void InsertPerLine(int index, int length = 0)
    {
        MoveStep(index);

        PerLine data;

        // Add the new line length to the existing line start
        data = PerLineData[index];
        var lineStart = data.Start;
        data.Start += length;
        PerLineData[index] = data;

        // Insert the new line
        data = new PerLine { Start = lineStart, };
        PerLineData.Insert(index, data);

        // Move the step
        stepLength += length;
        stepLine++;
    }

    /// <summary>
    /// Moves the step.
    /// </summary>
    /// <param name="line">The line.</param>
    public virtual void MoveStep(int line)
    {
        if (stepLength == 0)
        {
            stepLine = line;
        }
        else if (stepLine < line)
        {
            while (stepLine < line)
            {
                stepLine++;
                var data = PerLineData[stepLine];
                data.Start += stepLength;
                PerLineData[stepLine] = data;
            }
        }
        else if (stepLine > line)
        {
            while (stepLine > line)
            {
                var data = PerLineData[stepLine];
                data.Start -= stepLength;
                PerLineData[stepLine] = data;
                stepLine--;
            }
        }
    }

    /// <summary>
    /// Rebuilds the line data.
    /// </summary>
    public virtual void RebuildLineData()
    {
        stepLine = 0;
        stepLength = 0;

        PerLineData = new GapBuffer<PerLine>
        {
            new() { Start = 0, },
            new() { Start = 0, }, // Terminal
        };

        // Fake an insert notification
        var scn = new SCNotification();
        var adjustedLines = ScintillaApi.DirectMessage(SCI_GETLINECOUNT).ToInt32() - 1;
        scn.linesAdded = new IntPtr(adjustedLines);
        scn.position = IntPtr.Zero;
        scn.length = ScintillaApi.DirectMessage(SCI_GETLENGTH);
        scn.text = ScintillaApi.DirectMessage(SCI_GETRANGEPOINTER, scn.position, scn.length);
        TrackInsertText(scn);
    }

    /// <summary>
    /// Scintilla notification callback delegate.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The <see cref="ISCNotificationEventArgs"/> instance containing the event data.</param>
    public abstract void ScNotificationCallback(object sender, ISCNotificationEventArgs e);

    /// <summary>
    /// Handles the Scintilla's text change events.
    /// </summary>
    /// <param name="scn">The Scintilla notification data.</param>
    public virtual void ScnModified(SCNotification scn)
    {
        if ((scn.modificationType & SC_MOD_DELETETEXT) > 0)
        {
            TrackDeleteText(scn);
        }

        if ((scn.modificationType & SC_MOD_INSERTTEXT) > 0)
        {
            TrackInsertText(scn);
        }
    }

    /// <summary>
    /// Tracks the delete text.
    /// </summary>
    /// <param name="scn">The Scintilla notification data.</param>
    public virtual void TrackDeleteText(SCNotification scn)
    {
        var startLine = ScintillaApi.DirectMessage(SCI_LINEFROMPOSITION, scn.position).ToInt32();
        if (scn.linesAdded == IntPtr.Zero)
        {
            // That was easy
            var delta = HelpersGeneral.GetCharCount(scn.text, scn.length.ToInt32(), ScintillaApi.Encoding);
            AdjustLineLength(startLine, delta * -1);
        }
        else
        {
            // Adjust the existing line
            var lineByteStart = ScintillaApi.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(startLine)).ToInt32();
            var lineByteLength = ScintillaApi.DirectMessage(SCI_LINELENGTH, new IntPtr(startLine)).ToInt32();
            AdjustLineLength(startLine, GetCharCount(lineByteStart, lineByteLength) - CharLineLength(startLine));

            var linesRemoved = scn.linesAdded.ToInt32() * -1;
            for (var i = 0; i < linesRemoved; i++)
            {
                // Deleted line
                DeletePerLine(startLine + 1);
            }
        }
    }

    /// <summary>
    /// Tracks the insert text.
    /// </summary>
    /// <param name="scn">The Scintilla notification data.</param>
    public virtual void TrackInsertText(SCNotification scn)
    {
        var startLine = ScintillaApi.DirectMessage(SCI_LINEFROMPOSITION, scn.position).ToInt32();
        if (scn.linesAdded == IntPtr.Zero)
        {
            // That was easy
            var delta = GetCharCount(scn.position.ToInt32(), scn.length.ToInt32());
            AdjustLineLength(startLine, delta);
        }
        else
        {
            // Adjust existing line
            var lineByteStart = ScintillaApi.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(startLine)).ToInt32();
            var lineByteLength = ScintillaApi.DirectMessage(SCI_LINELENGTH, new IntPtr(startLine)).ToInt32();
            AdjustLineLength(startLine, GetCharCount(lineByteStart, lineByteLength) - CharLineLength(startLine));

            for (var i = 1; i <= scn.linesAdded.ToInt32(); i++)
            {
                var line = startLine + i;

                // Insert new line
                lineByteStart += lineByteLength;
                lineByteLength = ScintillaApi.DirectMessage(SCI_LINELENGTH, new IntPtr(line)).ToInt32();
                InsertPerLine(line, GetCharCount(lineByteStart, lineByteLength));
            }
        }
    }

    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }

    #endregion Methods

    #region Properties

    /// <summary>
    /// Gets a value indicating whether all the document lines are visible (not hidden).
    /// </summary>
    /// <returns>true if all the lines are visible; otherwise, false.</returns>
    public virtual bool AllLinesVisible => ScintillaApi.DirectMessage(SCI_GETALLLINESVISIBLE) != IntPtr.Zero;

    /// <summary>
    /// Gets the number of lines.
    /// </summary>
    /// <returns>The number of lines in the <see cref="LineCollectionBase{TLine}" />.</returns>
    public virtual int Count =>
        // Subtract the terminal line
        PerLineData.Count - 1;

    /// <summary>
    /// Gets the number of CHARACTERS in the document.
    /// </summary>
    public virtual int TextLength =>
        // Where the terminal line begins
        CharPositionFromLine(PerLineData.Count - 1);

    /// <summary>
    /// Gets the <see cref="LineBase" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="LineBase" /> to get.</param>
    /// <returns>The <see cref="LineBase" /> at the specified index.</returns>
    public abstract TLine this[int index] { get; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LineCollectionBase{TLine}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public LineCollectionBase(IScintillaApi scintilla)
    {
        this.ScintillaApi = scintilla;

        PerLineData = new GapBuffer<PerLine>
        {
            new() { Start = 0, },
            new() { Start = 0, }, // Terminal
        };
    }

    #endregion Constructors
}