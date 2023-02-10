using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.EventArguments;

namespace Scintilla.NET.Linux.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.UpdateUi" /> event.
/// </summary>
// ReSharper disable once InconsistentNaming, part of the API
public class UpdateUIEventArgs : UpdateUIEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUIEventArgs" /> class.
    /// </summary>
    /// <param name="scintillaApi">The <see cref="IScintillaApi" /> control interface that generated this event.</param>
    /// <param name="change">A bitwise combination of <see cref="UpdateChange" /> values specifying the reason to update the UI.</param>
    public UpdateUIEventArgs(IScintillaApi scintillaApi, UpdateChange change) : base(scintillaApi, change)
    {
    }
}