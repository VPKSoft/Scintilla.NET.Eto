using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.DoubleClick" /> event.
/// </summary>
public abstract class DoubleClickEventArgsBase<TKeys> : ScintillaEventArgs, IDoubleClickEventArgs
    where TKeys: Enum
{
    private readonly int bytePosition;
    private int? position;

    /// <summary>
    /// Gets the line double clicked.
    /// </summary>
    /// <returns>The zero-based index of the double clicked line.</returns>
    public virtual int Line { get; private set; }

    /// <inheritdoc />
    public IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when double clicked.
    /// </summary>
    /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
    public virtual TKeys Modifiers { get; }

    /// <summary>
    /// Gets the zero-based document position of the text double clicked.
    /// </summary>
    /// <returns>
    /// The zero-based character position within the document of the double clicked text;
    /// otherwise, -1 if not a document position.
    /// </returns>
    public int Position
    {
        get
        {
            position ??= LineCollectionGeneral.ByteToCharPosition(bytePosition);

            return (int)position;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleClickEventArgsBase{TKeys}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the double click.</param>
    /// <param name="bytePosition">The zero-based byte position of the double clicked text.</param>
    /// <param name="line">The zero-based line index of the double clicked text.</param>
    protected DoubleClickEventArgsBase(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        TKeys modifiers, 
        int bytePosition, 
        int line) : base(scintilla)
    {
        this.bytePosition = bytePosition;
        Modifiers = modifiers;
        Line = line;
        LineCollectionGeneral = lineCollectionGeneral;

        if (bytePosition == -1)
        {
            position = -1;
        }
    }
}