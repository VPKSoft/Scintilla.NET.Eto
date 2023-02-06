using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Interfaces.EventArguments;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEvents{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor,TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs}.CharAdded" /> event.
/// </summary>
public abstract class CharAddedEventArgsBase : EventArgs, ICharAddedEventArgs
{
    /// <summary>
    /// Gets the text character added to a <see cref="Scintilla" /> control.
    /// </summary>
    /// <returns>The character added.</returns>
    public virtual int Char { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharAddedEventArgsBase" /> class.
    /// </summary>
    /// <param name="ch">The character added.</param>
    protected CharAddedEventArgsBase(int ch)
    {
        Char = ch;
    }
}