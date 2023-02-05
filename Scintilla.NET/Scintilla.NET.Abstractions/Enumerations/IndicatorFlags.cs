using Scintilla.NET.Abstractions.Classes.Lexers;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Abstractions.Enumerations;

/// <summary>
/// Flags associated with a <see cref="IScintillaIndicator{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />.
/// </summary>
/// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
[Flags]
public enum IndicatorFlags
{
    /// <summary>
    /// No flags. This is the default.
    /// </summary>
    None = 0,

    /// <summary>
    /// When set, will treat an indicator value as a RGB color that has been OR-ed with <see cref="IndicatorConstants.ValueBit" />
    /// and will use that instead of the value specified in the <see cref="IScintillaIndicator{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}.ForeColor" /> property. This allows
    /// an indicator to display more than one color.
    /// </summary>
    ValueFore = ScintillaConstants.SC_INDICFLAG_VALUEFORE
}