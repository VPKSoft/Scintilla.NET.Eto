using System.ComponentModel;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using Color = Gdk.Color;

namespace ScintillaNet.Linux.Collections;

/// <summary>
/// An immutable collection of margins in a <see cref="Scintilla" /> control.
/// </summary>
public class MarginCollection : MarginCollectionBase<Margin, Color>
{
    /// <summary>
    /// Gets a <see cref="Margin" /> object at the specified index.
    /// </summary>
    /// <param name="index">The margin index.</param>
    /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
    /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Margin this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Margin(ScintillaApi, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarginCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public MarginCollection(IScintillaApi scintilla) : base(scintilla)
    {
    }
}