using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.EventArguments;

namespace ScintillaNet.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.CallTipClick" /> event.
/// </summary>
public abstract class CallTipClickEventArgsBase : ScintillaEventArgs, ICallTipClickEventArgs
{
    /// <summary>
    /// Gets the type of the call tip click.
    /// </summary>
    public virtual CallTipClickType CallTipClickType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DwellEventArgsBase" /> class.
    /// </summary>
    /// <param name="scintillaApi">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="callTipClickType">Type of the call tip click.</param>
    protected CallTipClickEventArgsBase(IScintillaApi scintillaApi, CallTipClickType callTipClickType) : base(scintillaApi)
    {
        CallTipClickType = callTipClickType;
    }
}