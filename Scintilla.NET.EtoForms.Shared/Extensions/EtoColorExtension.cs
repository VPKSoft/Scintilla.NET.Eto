#region License
/*
MIT License

Copyright(c) 2023 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using Eto.Drawing;

namespace ScintillaNet.EtoForms.Extensions;

/// <summary>
/// Extension methods for <see cref="Color"/> &lt;--&gt; <see cref="int"/> RGBA conversions.
/// </summary>
public static class EtoColorExtension
{
    /// <summary>
    /// Converts the <see cref="Color"/> to RGBA integer value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The <see cref="Color"/> converted to RGBA <see cref="int"/> value.</returns>
    public static int ToRgba(this Color value)
    {
        return value.Rb | (value.Gb << 8) | (value.Bb << 16) | (value.Ab << 24);
    }

    /// <summary>
    /// Converts the RGBA <see cref="int"/> value to <see cref="Color"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The RGBA <see cref="int"/> converted to <see cref="Color"/> value.</returns>
    public static Color FromRgba(this int value)
    {
        var a = (value >> 24) & 0xFF;
        var b = (value >> 16) & 0xFF;
        var g = (value >> 8) & 0xFF;
        var r = value & 0xFF;
        return Color.FromArgb(r, g, b, a);
    }
}