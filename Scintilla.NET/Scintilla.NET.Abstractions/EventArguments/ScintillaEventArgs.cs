using ScintillaNet.Abstractions.Interfaces.EventArguments.Base;

namespace ScintillaNet.Abstractions.EventArguments;

/// <summary>
/// A base class for the event for the Scintilla control API.
/// </summary>
public abstract class ScintillaEventArgs : EventArgs, IScintillaEventArgs
{
    /// <summary>
    /// Gets the scintilla API.
    /// </summary>
    /// <value>The scintilla API.</value>
    public IScintillaApi ScintillaApi { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScintillaEventArgs"/> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="IScintillaApi" /> control interface that generated this event.</param>
    protected ScintillaEventArgs(IScintillaApi scintilla)
    {
        ScintillaApi = scintilla;
    }
}