using Eto.Forms;
using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.IndicatorClick" /> event.
/// </summary>
public class IndicatorClickEventArgs : IndicatorClickEventArgsBase<Keys>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorClickEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    public IndicatorClickEventArgs(IScintillaApi scintilla, Keys modifiers) : base(scintilla, modifiers)
    {
    }
}