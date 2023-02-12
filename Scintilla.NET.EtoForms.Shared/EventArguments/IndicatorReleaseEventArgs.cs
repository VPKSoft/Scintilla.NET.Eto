using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.IndicatorRelease" /> event.
/// </summary>
public class IndicatorReleaseEventArgs : IndicatorReleaseEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorReleaseEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
    public IndicatorReleaseEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition) : base(scintilla, lineCollectionGeneral, bytePosition)
    {
    }
}