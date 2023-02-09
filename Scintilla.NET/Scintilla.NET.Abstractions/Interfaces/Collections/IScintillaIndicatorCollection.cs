using System.ComponentModel;
using Scintilla.NET.Abstractions.Collections;

namespace Scintilla.NET.Abstractions.Interfaces.Collections;

/// <summary>
/// An immutable collection of indicators in a <see cref="ScintillaApi" /> control.
/// </summary>
public interface IScintillaIndicatorCollection<out TIndicator, TColor> : IEnumerable<TIndicator>
    where TIndicator: IScintillaIndicator<TColor>
    where TColor : struct
{
    /// <summary>
    /// A reference to the Scintilla control interface.
    /// </summary>
    IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets the number of indicators.
    /// </summary>
    /// <returns>The number of indicators in the <see cref="IndicatorCollectionBase{TIndicator,TColor}" />.</returns>
    int Count { get; }

    /// <summary>
    /// Gets an <see cref="IndicatorBase{TColor}" /> object at the specified index.
    /// </summary>
    /// <param name="index">The indicator index.</param>
    /// <returns>An object representing the indicator at the specified <paramref name="index" />.</returns>
    /// <remarks>
    /// Indicators 0 through 7 are used by lexers.
    /// Indicators 32 through 35 are used for IME.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    TIndicator this[int index] { get; }
}