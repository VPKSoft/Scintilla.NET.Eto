using Eto.Forms;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;

namespace ScintillaNet.EtoForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEtoFormsEvents.IndicatorClick" /> event.
/// </summary>
public class IndicatorClickEventArgs : IndicatorClickEventArgsBase<Keys>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorClickEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The Scintilla control that generated this event.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    public IndicatorClickEventArgs(IScintillaApi scintilla, Keys modifiers) : base(scintilla, modifiers)
    {
    }
}