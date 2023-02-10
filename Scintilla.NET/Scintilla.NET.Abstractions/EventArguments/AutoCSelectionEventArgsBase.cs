using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the Scintilla.AutoCSelection event.
/// </summary>
public abstract class AutoCSelectionEventArgsBase : ScintillaEventArgs, IAutoCSelectionEventArgs

{
    private readonly IntPtr textPtr;
    private readonly int bytePosition;
    private int? position;
    private string? text;

    /// <summary>
    /// Gets the fill-up character that caused the completion.
    /// </summary>
    /// <returns>The fill-up character used to cause the completion; otherwise, 0.</returns>
    /// <remarks>Only a <see cref="ListCompletionMethod" /> of <see cref="Scintilla.NET.Abstractions.Enumerations.ListCompletionMethod.FillUp" /> will return a non-zero character.</remarks>
    /// <seealso cref="IScintillaMethods{TColor,TKeys,TBitmap}.AutoCSetFillUps" />
    public virtual int Char { get; }

    /// <inheritdoc />
    public IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets a value indicating how the completion occurred.
    /// </summary>
    /// <returns>One of the <see cref="Scintilla.NET.Abstractions.Enumerations.ListCompletionMethod" /> enumeration values.</returns>
    public virtual ListCompletionMethod ListCompletionMethod { get; }

    /// <summary>
    /// Gets the start position of the word being completed.
    /// </summary>
    /// <returns>The zero-based document position of the word being completed.</returns>
    public virtual int Position
    {
        get
        {
            if (position == null)
            {
                position = LineCollectionGeneral.ByteToCharPosition(bytePosition);
            }

            return (int)position;
        }
    }

    /// <summary>
    /// Gets the text of the selected auto-completion item.
    /// </summary>
    /// <returns>The selected auto-completion item text.</returns>
    public virtual unsafe string Text
    {
        get
        {
            if (text == null)
            {
                var len = 0;
                while (((byte*)textPtr)[len] != 0)
                    len++;

                text = HelpersGeneral.GetString(textPtr, len, ScintillaApi.Encoding);
            }

            return text;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoCSelectionEventArgsBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document of the word being completed.</param>
    /// <param name="text">A pointer to the selected auto-completion text.</param>
    /// <param name="ch">The character that caused the completion.</param>
    /// <param name="listCompletionMethod">A value indicating the way in which the completion occurred.</param>
    protected AutoCSelectionEventArgsBase(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, IntPtr text, int ch,
        ListCompletionMethod listCompletionMethod) : base(scintilla)
    {
        this.bytePosition = bytePosition;
        textPtr = text;
        Char = ch;
        ListCompletionMethod = listCompletionMethod;
        LineCollectionGeneral = lineCollectionGeneral;
    }
}