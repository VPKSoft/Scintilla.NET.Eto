using ScintillaNet.Abstractions.Interfaces;

namespace ScintillaNet.Abstractions.Enumerations;

/// <summary>
/// Configuration options for automatic code folding.
/// </summary>
/// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
[Flags]
public enum AutomaticFold
{
    /// <summary>
    /// Automatic folding is disabled. This is the default.
    /// </summary>
    None = 0,

    /// <summary>
    /// <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.NeedShown" /> event is not raised when this value is used.
    /// </summary>
    Show = ScintillaConstants.SC_AUTOMATICFOLD_SHOW,

    /// <summary>
    /// <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.MarginClick" /> event is not raised for folding margins when this value is used.
    /// </summary>
    Click = ScintillaConstants.SC_AUTOMATICFOLD_CLICK,

    /// <summary>
    /// Show lines as needed when the fold structure is changed.
    /// </summary>
    Change = ScintillaConstants.SC_AUTOMATICFOLD_CHANGE
}