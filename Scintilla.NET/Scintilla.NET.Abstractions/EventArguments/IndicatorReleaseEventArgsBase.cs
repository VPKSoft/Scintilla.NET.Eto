using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// </summary>
public abstract class IndicatorReleaseEventArgsBase : ScintillaEventArgs, IIndicatorReleaseEventArgs
{
    private readonly int bytePosition;
    private int? position;

    /// <summary>
    /// Gets the zero-based document position of the text clicked.
    /// </summary>
    /// <returns>The zero-based character position within the document of the clicked text.</returns>
    public virtual int Position
    {
        get
        {
            position ??= LineCollectionGeneral.ByteToCharPosition(bytePosition);

            return (int)position;
        }
    }

    /// <inheritdoc />
    public IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorReleaseEventArgsBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
    protected IndicatorReleaseEventArgsBase(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition) : base(scintilla)

    {
        this.bytePosition = bytePosition;
        LineCollectionGeneral = lineCollectionGeneral;
    }
}