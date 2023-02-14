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

namespace ScintillaNet.Abstractions.Interfaces.Methods;

/// <summary>
/// An interface for Scintilla methods with generic image.
/// </summary>
/// <typeparam name="TImage">The type of the image used in the platform.</typeparam>
public interface IScintillaMethodsImage<in TImage>
    where TImage : class
{
    /// <summary>
    /// Maps the specified image to a type identifier for use in an auto-completion list.
    /// </summary>
    /// <param name="type">The numeric identifier for this image.</param>
    /// <param name="image">The Bitmap to use in an auto-completion list.</param>
    /// <remarks>
    /// The <paramref name="image" /> registered can be referenced by its <paramref name="type" /> identifier in an auto-completion
    /// list by suffixing a word with the <see cref="IScintillaProperties.AutoCTypeSeparator" /> character and the <paramref name="type" /> value. e.g.
    /// "int?2 long?3 short?1" etc....
    /// </remarks>
    /// <seealso cref="IScintillaProperties.AutoCTypeSeparator" />
    void RegisterRgbaImage(int type, TImage image);
}