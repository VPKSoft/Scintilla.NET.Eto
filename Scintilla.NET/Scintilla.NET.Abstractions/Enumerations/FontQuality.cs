﻿namespace ScintillaNet.Abstractions.Enumerations;

/// <summary>
/// The font quality (anti-aliasing method) used to render text.
/// </summary>
public enum FontQuality
{
    /// <summary>
    /// Specifies that the character quality of the font does not matter; so the lowest quality can be used.
    /// This is the default.
    /// </summary>
    Default = ScintillaConstants.SC_EFF_QUALITY_DEFAULT,

    /// <summary>
    /// Specifies that anti-aliasing should not be used when rendering text.
    /// </summary>
    NonAntiAliased = ScintillaConstants.SC_EFF_QUALITY_NON_ANTIALIASED,

    /// <summary>
    /// Specifies that anti-aliasing should be used when rendering text, if the font supports it.
    /// </summary>
    AntiAliased = ScintillaConstants.SC_EFF_QUALITY_ANTIALIASED,

    /// <summary>
    /// Specifies that ClearType anti-aliasing should be used when rendering text, if the font supports it.
    /// </summary>
    LcdOptimized = ScintillaConstants.SC_EFF_QUALITY_LCD_OPTIMIZED
}