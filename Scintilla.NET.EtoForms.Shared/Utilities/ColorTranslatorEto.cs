#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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
// ReSharper disable CommentTypo

namespace Scintilla.NET.EtoForms.Shared.Utilities;

/// <summary>
/// Some color helper methods to Eto.Forms framework.
/// </summary>
public static class ColorTranslatorEto
{
    /// <summary>
    /// Converts the color to a 32-bit ARGB value.
    /// </summary>
    /// <returns>The 32-bit ARGB value that corresponds to the color.</returns>
    // ReSharper disable once IdentifierTypo
    public static int ToArgb(Color color) => color.ToArgb();

    /// <summary>
    /// Creates a Color from a 32-bit ARGB value.
    /// </summary>
    /// <param name="argb">32-bit ARGB value with Alpha in the high byte.</param>
    /// <returns>A new instance of the Color object with the specified color.</returns>
    // ReSharper disable once IdentifierTypo
    public static Color ToColor(int argb) => Color.FromArgb(argb);

    /// <summary>
    /// Falls back tothe default color of the specified <paramref name="value"/> is an empty color.
    /// </summary>
    /// <param name="value">The color value.</param>
    /// <param name="fallBack">The fall back color.</param>
    /// <returns>Color.</returns>
    public static Color FallBackColor(Color value, Color fallBack)
    {
        return value == default ? fallBack : value;
    }

    /// <summary>
    /// Coverts the bitmap pixels to an array of bytes with each pixel as ARGB component.
    /// </summary>
    /// <param name="image">The image to convert.</param>
    /// <returns>The bitmap as <c>byte[]</c> array.</returns>
    public static byte[] BitmapToArgb(Bitmap image)
    {
        // This code originally used Image.LockBits and some fast byte copying, however, the endianness
        // of the image formats was making my brain hurt. For now I'm going to use the slow but simple
        // GetPixel approach.

        var bytes = new byte[4 * image.Width * image.Height];

        var i = 0;
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var color = image.GetPixel(x, y);
                bytes[i++] = (byte)color.R;
                bytes[i++] = (byte)color.G;
                bytes[i++] = (byte)color.B;
                bytes[i++] = (byte)color.A;
            }
        }

        return bytes;
    }
}