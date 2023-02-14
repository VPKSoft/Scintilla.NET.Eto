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

using Gdk;
using static ScintillaNet.Abstractions.ScintillaConstants;

namespace ScintillaNet.Linux;

/// <summary>
/// Some platform-depended helper methods.
/// </summary>
public static class Helpers
{
    /// <summary>
    /// This should translate keys to the Scintilla control values if the are different compared to the platform.
    /// </summary>
    /// <param name="keys">The key value(s).</param>
    /// <returns>The key translated to Scintilla form as <see cref="int"/>.</returns>
    public static int TranslateKeys(Key keys) // TODO::Should this be Gdk.EventKey
    {
        int keyCode;

        // For some reason Scintilla uses different values for these keys...
        switch (keys)
        {
            case Key.Down:
                keyCode = SCK_DOWN;
                break;
            case Key.Up:
                keyCode = SCK_UP;
                break;
            case Key.Left:
                keyCode = SCK_LEFT;
                break;
            case Key.Right:
                keyCode = SCK_RIGHT;
                break;
            case Key.Home:
                keyCode = SCK_HOME;
                break;
            case Key.End:
                keyCode = SCK_END;
                break;
            case Key.Prior:
                keyCode = SCK_PRIOR;
                break;
            case Key.Next:
                keyCode = SCK_NEXT;
                break;
            case Key.Delete:
                keyCode = SCK_DELETE;
                break;
            case Key.Insert:
                keyCode = SCK_INSERT;
                break;
            case Key.Escape:
                keyCode = SCK_ESCAPE;
                break;
            case Key.BackSpace:
                keyCode = SCK_BACK;
                break;
            case Key.Tab:
                keyCode = SCK_TAB;
                break;
            case Key.Return:
                keyCode = SCK_RETURN;
                break;
            case Key.KP_Add:
                keyCode = SCK_ADD;
                break;
            case Key.KP_Subtract:
                keyCode = SCK_SUBTRACT;
                break;
            case Key.KP_Divide:
                keyCode = SCK_DIVIDE;
                break;
            case Key.Meta_L:
                keyCode = SCK_WIN;
                break;
            case Key.Meta_R:
                keyCode = SCK_RWIN;
                break;
            case Key.Menu:
                keyCode = SCK_MENU;
                break;
            //case Keys.Oem2: TODO:GTK these!
            //    keyCode = (byte)'/';
            //    break;
            //case Keys.Oem3:
            //    keyCode = (byte)'`';
            //    break;
            //case Keys.Oem4:
            //    keyCode = '[';
            //    break;
            //case Keys.Oem5:
            //    keyCode = '\\';
            //    break;
            //case Keys.Oem6:
            //    keyCode = ']';
            //    break;
            default:
                // keyCode = TODO::Verify this on GTK: (int)(keys & Keys.KeyCode);
                keyCode = (int)keys;
                break;
        }

        // No translation necessary for the modifiers. Just add them back in.
        // TODO::Verify this on GTK: var keyDefinition = keyCode | (int)(keys & Keys.Modifiers);
        var keyDefinition = keyCode;
        return keyDefinition;
    }
}