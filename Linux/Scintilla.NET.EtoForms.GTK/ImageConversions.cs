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
using Eto.GtkSharp;

namespace ScintillaNet.EtoForms.GTK;

/// <summary>
/// Eto.Forms &lt;--&gt; GTK image conversions.
/// </summary>
internal class ImageConversions
{
    /// <summary>
    /// Converts a <see cref="Gdk.Pixbuf"/> to ARGB byte array.
    /// </summary>
    /// <param name="pixBuf">The pix buf to covert.</param>
    /// <returns>The bitmap converted to ARGB byte array (<see cref="byte"/>[]).</returns>
    public static byte[] BitmapToArgb(Gdk.Pixbuf pixBuf)
    {
        return BitmapToArgb(pixBuf.ToEto());
    }

    /// <summary>
    /// Converts a <see cref="Gtk.Image"/> to ARGB byte array.
    /// </summary>
    /// <param name="image">The image to covert.</param>
    /// <returns>The bitmap converted to ARGB byte array (<see cref="byte"/>[]).</returns>
    public static byte[] BitmapToArgb(Gtk.Image image)
    {
        return BitmapToArgb(image.Pixbuf);
    }

    /// <summary>
    /// Converts a <see cref="Image"/> to ARGB byte array.
    /// </summary>
    /// <param name="image">The image to covert.</param>
    /// <returns>The bitmap converted to ARGB byte array (<see cref="byte"/>[]).</returns>
    public static byte[] BitmapToArgb(Image image)
    {
        using var bitmap = new Bitmap(image);
        return BitmapToArgb(bitmap);
    }

    /// <summary>
    /// Converts a <see cref="Bitmap"/> to RGBA byte array.
    /// </summary>
    /// <param name="bitmap">The bitmap to covert.</param>
    /// <returns>The bitmap converted to ARGB byte array (<see cref="byte"/>[]).</returns>
    public static byte[] BitmapToArgb(Bitmap bitmap)
    {
        var bytes = new byte[4 * bitmap.Width * bitmap.Height];

        var i = 0;
        for (var y = 0; y < bitmap.Height; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                var color = bitmap.GetPixel(x, y);
                bytes[i++] = (byte)color.Rb;
                bytes[i++] = (byte)color.Gb;
                bytes[i++] = (byte)color.Bb;
                bytes[i++] = (byte)color.Ab;
            }
        }

        return bytes;
    }
}