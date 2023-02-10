using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.IndicatorClick" /> event.
/// </summary>
public abstract class IndicatorClickEventArgsBase<TKeys> : ScintillaEventArgs, IIndicatorClickEventArgs<TKeys>
    where TKeys: Enum
{
    /// <summary>
    /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when clicked.
    /// </summary>
    /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
    public virtual TKeys Modifiers { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorClickEventArgsBase{TKeys}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    protected IndicatorClickEventArgsBase(
        IScintillaApi scintilla,
        TKeys modifiers) : base(scintilla)
    {
        Modifiers = modifiers;
    }
}