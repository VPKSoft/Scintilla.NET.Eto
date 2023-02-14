using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Abstractions.Interfaces.EventArguments;

namespace ScintillaNet.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.HotspotClick" />, <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.HotspotDoubleClick" />,
/// and <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.HotspotReleaseClick" /> events.
/// </summary>
public abstract class HotspotClickEventArgsBase<TKeys> : ScintillaEventArgs, IHotspotClickEventArgs<TKeys>
    where TKeys: Enum
{
    private readonly int bytePosition;
    private int? position;

    /// <summary>
    /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when clicked.
    /// </summary>
    /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
    public virtual TKeys Modifiers { get; }

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
    /// Initializes a new instance of the <see cref="HotspotClickEventArgsBase{TKeys}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
    protected HotspotClickEventArgsBase(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        TKeys modifiers,
        int bytePosition) : base(scintilla)
    {
        this.bytePosition = bytePosition;
        Modifiers = modifiers;
        LineCollectionGeneral = lineCollectionGeneral;
    }
}