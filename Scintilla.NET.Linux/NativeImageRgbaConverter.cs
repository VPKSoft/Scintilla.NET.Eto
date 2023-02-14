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

namespace ScintillaNet.Linux;

/// <summary>
/// A class to convert <see cref="Gtk.Image"/>s to RGBA byte data.
/// Implements the <see cref="INativeImageToRgbaConverter{TImage}" />
/// </summary>
/// <seealso cref="INativeImageToRgbaConverter{TImage}" />
public static class NativeImageRgbaConverter
{
    /// <summary>
    /// Converts a <see cref="Gtk.Image"/> to ARGB byte array.
    /// </summary>
    /// <param name="image">The image to covert.</param>
    /// <returns>The bitmap converted to ARGB byte array (<see cref="byte"/>[]).</returns>
    public static byte[] PixBufToBytes(Gtk.Image image)
    {
        return PixBufToBytes(image.Pixbuf);
    }

    /// <summary>
    /// Converts a <see cref="Gdk.Pixbuf"/> to ARGB byte array.
    /// </summary>
    /// <param name="pixBuf">The pix buf to covert.</param>
    /// <returns>The bitmap converted to ARGB byte array (<see cref="byte"/>[]).</returns>
    public static byte[] PixBufToBytes(Gdk.Pixbuf pixBuf)
    {
        var width = pixBuf.Width;
        var height = pixBuf.Height;
        var alpha = pixBuf.HasAlpha;
        var bytes = pixBuf.PixelBytes.Data;

        var result = new byte[width * height* 4];
        var readIndex = 0;
        var writeIndex = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                result[writeIndex++] = bytes[readIndex++];
                result[writeIndex++] = bytes[readIndex++];
                result[writeIndex++] = bytes[readIndex++];
                result[writeIndex++] = alpha ? bytes[readIndex++] : (byte)255;
            }
        }

        return result;
    }
}