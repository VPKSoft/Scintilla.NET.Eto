using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.EtoForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="IScintillaEtoFormsEvents.DwellStart" /> and <see cref="IScintillaEtoFormsEvents.DwellEnd" /> events.
/// </summary>
public class DwellEventArgs : DwellEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DwellEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The Scintilla control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where the mouse pointer was lingering.</param>
    /// <param name="x">The x-coordinate of the mouse pointer relative to the Scintilla control.</param>
    /// <param name="y">The y-coordinate of the mouse pointer relative to the Scintilla control.</param>
    public DwellEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, 
        int x, 
        int y) : base(scintilla, lineCollectionGeneral, bytePosition, x, y)
    {
    }
}