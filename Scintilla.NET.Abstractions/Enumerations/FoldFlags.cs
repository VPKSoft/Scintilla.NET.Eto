﻿namespace Scintilla.NET.Abstractions.Enumerations;

/// <summary>
/// Additional display options for folds.
/// </summary>
[Flags]
public enum FoldFlags
{
    /// <summary>
    /// A line is drawn above if expanded.
    /// </summary>
    LineBeforeExpanded = ScintillaConstants.SC_FOLDFLAG_LINEBEFORE_EXPANDED,

    /// <summary>
    /// A line is drawn above if not expanded.
    /// </summary>
    LineBeforeContracted = ScintillaConstants.SC_FOLDFLAG_LINEBEFORE_CONTRACTED,

    /// <summary>
    /// A line is drawn below if expanded.
    /// </summary>
    LineAfterExpanded = ScintillaConstants.SC_FOLDFLAG_LINEAFTER_EXPANDED,

    /// <summary>
    /// A line is drawn below if not expanded.
    /// </summary>
    LineAfterContracted = ScintillaConstants.SC_FOLDFLAG_LINEAFTER_CONTRACTED,

    /// <summary>
    /// Displays the hexadecimal fold levels in the margin to aid with debugging.
    /// This feature may change in the future.
    /// </summary>
    LevelNumbers = ScintillaConstants.SC_FOLDFLAG_LEVELNUMBERS,

    /// <summary>
    /// Displays the hexadecimal line state in the margin to aid with debugging. This flag
    /// cannot be used at the same time as the <see cref="LevelNumbers" /> flag.
    /// </summary>
    LineState = ScintillaConstants.SC_FOLDFLAG_LINESTATE
}