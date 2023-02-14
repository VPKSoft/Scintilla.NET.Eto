using System.Collections;
using System.ComponentModel;
using ScintillaNet.Abstractions.Interfaces.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Abstractions.Collections;

/// <summary>
/// An immutable collection of indicators in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class IndicatorCollectionBase<TIndicator, TColor> : IScintillaIndicatorCollection<TIndicator, TColor>
    where TIndicator : IScintillaIndicator<TColor>
    where TColor: struct
{
    /// <summary>
    /// Provides an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An object that contains all <see cref="IndicatorBase{TColor}" />.</returns>
    public virtual IEnumerator<TIndicator> GetEnumerator()
    {
        var count = Count;
        for (var i = 0; i < count; i++)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Gets the number of indicators.
    /// </summary>
    /// <returns>The number of indicators in the <see cref="IndicatorCollectionBase{TIndicator, TColor}" />.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int Count => INDIC_MAX + 1;

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
    public abstract TIndicator this[int index] { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorCollectionBase{TIndicator, TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    protected IndicatorCollectionBase(IScintillaApi scintilla)
    {
        ScintillaApi = scintilla;
    }
}