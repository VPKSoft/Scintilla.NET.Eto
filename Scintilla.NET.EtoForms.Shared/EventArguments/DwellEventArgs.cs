using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.EventArguments;
using Scintilla.NET.Abstractions.Interfaces.Collections;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.DwellStart" /> and <see cref="WinForms.Scintilla.DwellEnd" /> events.
/// </summary>
public class DwellEventArgs : DwellEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DwellEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where the mouse pointer was lingering.</param>
    /// <param name="x">The x-coordinate of the mouse pointer relative to the <see cref="WinForms.Scintilla" /> control.</param>
    /// <param name="y">The y-coordinate of the mouse pointer relative to the <see cref="WinForms.Scintilla" /> control.</param>
    public DwellEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, 
        int x, 
        int y) : base(scintilla, lineCollectionGeneral, bytePosition, x, y)
    {
    }
}