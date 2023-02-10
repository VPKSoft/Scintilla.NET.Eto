using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.UpdateUi" /> event.
/// </summary>
// ReSharper disable once InconsistentNaming, part of the API
public abstract class UpdateUIEventArgsBase : EventArgs, IUpdateUIEventArgs
{
    /// <summary>
    /// The UI update that occurred.
    /// </summary>
    /// <returns>A bitwise combination of <see cref="UpdateChange" /> values specifying the UI update that occurred.</returns>
    public virtual UpdateChange Change { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUIEventArgsBase" /> class.
    /// </summary>
    /// <param name="scintillaApi">The <see cref="IScintillaApi" /> control interface that generated this event.</param>
    /// <param name="change">A bitwise combination of <see cref="UpdateChange" /> values specifying the reason to update the UI.</param>
    protected UpdateUIEventArgsBase(IScintillaApi scintillaApi, UpdateChange change)
    {
        ScintillaApi = scintillaApi;
        Change = change;
    }

    /// <inheritdoc />
    public IScintillaApi ScintillaApi { get; }
}