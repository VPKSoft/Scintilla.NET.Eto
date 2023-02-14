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

using Color = Gdk.Color;

namespace ScintillaNet.Linux.GdkUtils;

/// <summary>
/// <see cref="Color"/> to and from translations with <see cref="int"/> values.
/// </summary>
public static class ColorTranslator
{
    // ReSharper disable once CommentTypo
    /// <summary>
    /// Converts the specified <see cref="Color"/> to a 32-bit integer with full opacity.
    /// </summary>
    /// <param name="value">The color value to convert.</param>
    /// <returns>A 32-bit ARGB integer value with full opacity.</returns>
    public static int ToInt(Color value)
    {
        var r = (value.Red / 256);
        var g = (value.Green / 256) << 8;
        var b = (value.Blue / 256) << 16;
        var intColor = r | g | b | (255 << 24); // 255 for full opacity.
        return intColor;
    }

    // ReSharper disable once CommentTypo
    /// <summary>
    /// Converts a 32 bit ARGB integer value into a <see cref="Color"/>.
    /// </summary>
    /// <param name="value">The integer value to convert.</param>
    /// <returns>A <see cref="Color"/> value.</returns>
    public static Color ToColor(int value)
    {
        var _ = (value >> 24) & 0xFF; // Opacity
        var b = (value >> 16) & 0xFF;
        var g = (value >> 8) & 0xFF;
        var r = value & 0xFF;
        
        return new Color
        {
            Red = (ushort)(r * 256),
            Blue = (ushort)(b * 256),
            Green = (ushort)(g * 256),
        };
    }
}