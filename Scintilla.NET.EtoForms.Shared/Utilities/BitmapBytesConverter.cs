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

namespace ScintillaNet.EtoForms.Utilities;

/// <summary>
/// A class to convert <see cref="Bitmap"/> pixels into bytes.
/// </summary>
/// <remarks>Direct Eto.Forms translation base on Scintilla.NET, see: https://github.com/VPKSoft/Scintilla.NET source.</remarks>
public class BitmapBytesConverter
{
    /// <summary>
    /// Converts the bitmap image pixels to format understood by the Scintilla control.
    /// </summary>
    /// <param name="image">The bitmap image which pixel byte data to get.</param>
    /// <returns><see cref="byte"/>[].</returns>
    public static byte[] BitmapToArgb(Bitmap image)
    {
        var bytes = new byte[4 * image.Width * image.Height];

        var i = 0;
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var color = image.GetPixel(x, y);
                bytes[i++] = (byte)color.Rb;
                bytes[i++] = (byte)color.Gb;
                bytes[i++] = (byte)color.Bb;
                bytes[i++] = (byte)color.Ab;
            }
        }

        return bytes;
    }
}