using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.UpdateUi" /> event.
/// </summary>
// ReSharper disable once InconsistentNaming, part of the API
public abstract class UpdateUIEventArgsBase : EventArgs
{
    /// <summary>
    /// The UI update that occurred.
    /// </summary>
    /// <returns>A bitwise combination of <see cref="UpdateChange" /> values specifying the UI update that occurred.</returns>
    public virtual UpdateChange Change { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUIEventArgsBase" /> class.
    /// </summary>
    /// <param name="change">A bitwise combination of <see cref="UpdateChange" /> values specifying the reason to update the UI.</param>
    protected UpdateUIEventArgsBase(UpdateChange change)
    {
        Change = change;
    }
}