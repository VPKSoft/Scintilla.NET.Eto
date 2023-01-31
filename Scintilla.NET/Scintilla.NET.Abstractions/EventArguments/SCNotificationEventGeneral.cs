using static Scintilla.NET.Abstractions.Classes.ScintillaApiStructs;

namespace Scintilla.NET.Abstractions.EventArguments;

/// <summary>
/// Notifications are sent (fired) from the Scintilla control to its container when an event has occurred that may interest the container. This class cannot be inherited.
/// Implements the <see cref="SCNotificationEventArgsBase" />
/// </summary>
/// <seealso cref="SCNotificationEventArgsBase" />
// ReSharper disable once InconsistentNaming, part of the API
public sealed class SCNotificationEventGeneral : SCNotificationEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SCNotificationEventGeneral"/> class.
    /// </summary>
    /// <param name="scn">The Scintilla notification data structure.</param>
    public SCNotificationEventGeneral(SCNotification scn) : base(scn)
    {
    }
}