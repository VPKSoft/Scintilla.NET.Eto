﻿using Scintilla.NET.Abstractions;
using Scintilla.NET.Abstractions.Enumerations;
using Scintilla.NET.Abstractions.EventArguments;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.CallTipClick" /> event.
/// </summary>
public class CallTipClickEventArgs: CallTipClickEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DwellEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="WinForms.Scintilla" /> control that generated this event.</param>
    /// /// <param name="callTipClickType">Type of the call tip click.</param>
    public CallTipClickEventArgs(
        IScintillaApi scintilla, CallTipClickType callTipClickType) : base(scintilla, callTipClickType)
    {
    }
}