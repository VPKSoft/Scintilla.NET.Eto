﻿#region License
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

namespace Scintilla.NET.Abstractions.Interfaces;

/// <summary>
/// An interface to convert <see cref="int"/> values to and from to the platform-depended color structure.
/// </summary>
/// <typeparam name="TColor">The type of the color used in the platform.</typeparam>
public interface IColorTranslator<TColor>
    where TColor : struct
{
    /// <summary>
    /// Translates an <see cref="int"/> color value to a platform-depended <typeparamref name="TColor"/> structure.
    /// </summary>
    public TColor FromInt(int value);

    /// <summary>
    /// Translates the specified <typeparamref name="TColor"/> to an integer value color.
    /// </summary>
    public int FromColor(TColor color);
}