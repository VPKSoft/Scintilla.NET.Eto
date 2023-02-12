using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Methods;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.StyleNeeded" /> event.
/// </summary>
public abstract class StyleNeededEventArgsBase : ScintillaEventArgs, IStyleNeededEventArgs
{
    private readonly int bytePosition;
    private int? position;

    /// <inheritdoc />
    public IScintillaLineCollectionGeneral LineCollectionGeneral { get; }

    /// <summary>
    /// Gets the document position where styling should end. The <see cref="IScintillaMethods.GetEndStyled" /> method
    /// indicates the last position styled correctly and the starting place for where styling should begin.
    /// </summary>
    /// <returns>The zero-based position within the document to perform styling up to.</returns>
    public virtual int Position
    {
        get
        {
            position ??= LineCollectionGeneral.ByteToCharPosition(bytePosition);

            return (int)position;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleNeededEventArgsBase" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document to stop styling.</param>
    protected StyleNeededEventArgsBase(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition) : base(scintilla)
    {
        this.bytePosition = bytePosition;
        LineCollectionGeneral = lineCollectionGeneral;
    }
}