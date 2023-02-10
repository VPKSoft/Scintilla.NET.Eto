using System.Collections;
using System.ComponentModel;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.Interfaces.Collections;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Collections;

/// <summary>
/// An immutable collection of margins in a <see cref="Scintilla" /> control.
/// </summary>
public abstract class MarginCollectionBase<TMargin, TColor> : 
    IScintillaMarginCollection<TMargin, TColor>
    where TMargin : MarginBase<TColor>
    where TColor: struct
{
    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    protected IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Removes all text displayed in every <see cref="MarginType.Text" /> and <see cref="MarginType.RightText" /> margins.
    /// </summary>
    public virtual void ClearAllText()
    {
        ScintillaApi.DirectMessage(SCI_MARGINTEXTCLEARALL);
    }

    /// <summary>
    /// Provides an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An object that contains all <see cref="IScintillaMargin{TColor}" />.</returns>
    public virtual IEnumerator<TMargin> GetEnumerator()
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
    /// Gets or sets the number of margins in the <see cref="MarginCollectionBase{TMargin,TColor}" />.
    /// </summary>
    /// <returns>The number of margins in the collection. The default is 5.</returns>
    public virtual int Capacity
    {
        get => ScintillaApi.DirectMessage(SCI_GETMARGINS).ToInt32();
        set
        {
            value = HelpersGeneral.ClampMin(value, 0);
            ScintillaApi.DirectMessage(SCI_SETMARGINS, new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets the number of margins in the <see cref="MarginCollectionBase{TMargin,TColor}" />.
    /// </summary>
    /// <returns>The number of margins in the collection.</returns>
    /// <remarks>This property is kept for convenience. The return value will always be equal to <see cref="Capacity" />.</remarks>
    /// <seealso cref="Capacity" />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int Count => Capacity;

    /// <summary>
    /// Gets or sets the width in pixels of the left margin padding.
    /// </summary>
    /// <returns>The left margin padding measured in pixels. The default is 1.</returns>
    [DefaultValue(1)]
    [Description("The left margin padding in pixels.")]
    public virtual int Left
    {
        get => ScintillaApi.DirectMessage(SCI_GETMARGINLEFT).ToInt32();
        set
        {
            value = HelpersGeneral.ClampMin(value, 0);
            ScintillaApi.DirectMessage(SCI_SETMARGINLEFT, IntPtr.Zero, new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets or sets the width in pixels of the right margin padding.
    /// </summary>
    /// <returns>The right margin padding measured in pixels. The default is 1.</returns>
    [DefaultValue(1)]
    [Description("The right margin padding in pixels.")]
    public virtual int Right
    {
        get => ScintillaApi.DirectMessage(SCI_GETMARGINRIGHT).ToInt32();
        set
        {
            value = HelpersGeneral.ClampMin(value, 0);
            ScintillaApi.DirectMessage(SCI_SETMARGINRIGHT, IntPtr.Zero, new IntPtr(value));
        }
    }

    /// <summary>
    /// Gets a <see cref="MarginBase{TColor}" /> object at the specified index.
    /// </summary>
    /// <param name="index">The margin index.</param>
    /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
    /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract TMargin this[int index] { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarginCollectionBase{TMargin,TColor}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    protected MarginCollectionBase(IScintillaApi scintilla)
    {
        this.ScintillaApi = scintilla;
    }
}