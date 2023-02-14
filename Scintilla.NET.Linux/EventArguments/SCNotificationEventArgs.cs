using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.EventArguments;

namespace ScintillaNet.Linux.EventArguments;

/// <summary>
/// Notifications are sent (fired) from the Scintilla control to its container when an event has occurred that may interest the container. This class cannot be inherited.
/// Implements the <see cref="SCNotificationEventArgsBase" />
/// </summary>
/// <seealso cref="SCNotificationEventArgsBase" />
// ReSharper disable once InconsistentNaming, part of the API
public sealed class SCNotificationEventArgs : SCNotificationEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SCNotificationEventArgs"/> class.
    /// </summary>
    /// <param name="scn">The Scintilla notification data structure.</param>
    public SCNotificationEventArgs(ScintillaApiStructs.SCNotification scn) : base(scn)
    {
    }
}