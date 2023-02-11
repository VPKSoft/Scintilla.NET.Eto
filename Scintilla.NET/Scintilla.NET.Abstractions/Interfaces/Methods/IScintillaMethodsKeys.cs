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

using Scintilla.NET.Abstractions.Enumerations;

namespace Scintilla.NET.Abstractions.Interfaces.Methods;

/// <summary>
/// An interface for Scintilla methods with generic key type.
/// </summary>
/// <typeparam name="TKeys">The type of the keys enumeration used by the platform.</typeparam>
public interface IScintillaMethodsKeys<in TKeys>
    where TKeys : Enum
{
    /// <summary>
    /// Assigns the specified key definition to a <see cref="Scintilla" /> command.
    /// </summary>
    /// <param name="keyDefinition">The key combination to bind.</param>
    /// <param name="sciCommand">The command to assign.</param>
    void AssignCmdKey(TKeys keyDefinition, Command sciCommand);

    /// <summary>
    /// Makes the specified key definition do nothing.
    /// </summary>
    /// <param name="keyDefinition">The key combination to bind.</param>
    /// <remarks>This is equivalent to binding the keys to <see cref="Command.Null" />.</remarks>
    void ClearCmdKey(TKeys keyDefinition);
}