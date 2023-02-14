using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments;

namespace ScintillaNet.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.NeedShown" /> event.
/// </summary>
public abstract class NeedShownEventArgsBase : ScintillaEventArgs, INeedShownEventArgs 
{
    private readonly int bytePosition;
    private readonly int byteLength;
    private int? position;
    private int? length;

    /// <inheritdoc />
    public IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets the length of the text that needs to be shown.
    /// </summary>
    /// <returns>The length of text starting at <see cref="Position" /> that needs to be shown.</returns>
    public virtual int Length
    {
        get
        {
            if (length == null)
            {
                var endBytePosition = bytePosition + byteLength;
                var endPosition = LineCollectionGeneral.ByteToCharPosition(endBytePosition);
                length = endPosition - Position;
            }

            return (int)length;
        }
    }

    /// <summary>
    /// Gets the zero-based document position where text needs to be shown.
    /// </summary>
    /// <returns>The zero-based document position where the range of text to be shown starts.</returns>
    public virtual int Position
    {
        get
        {
            position ??= LineCollectionGeneral.ByteToCharPosition(bytePosition);

            return (int)position;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NeedShownEventArgsBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text needs to be shown.</param>
    /// <param name="byteLength">The length in bytes of the text that needs to be shown.</param>
    protected NeedShownEventArgsBase(IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, int byteLength) : base(scintilla)

    {
        this.bytePosition = bytePosition;
        this.byteLength = byteLength;
        LineCollectionGeneral = lineCollectionGeneral;
    }
}