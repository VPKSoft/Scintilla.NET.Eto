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

using Scintilla.NET.Abstractions.Collections;
using Scintilla.NET.Abstractions.Enumerations;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Classes;

/// <summary>
/// Marker-related constants.
/// </summary>
public static class MarkerConstants
{
    /// <summary>
    /// An unsigned 32-bit mask of all <see cref="MarginBase{TColor}" /> indexes where each bit corresponds to a margin index.
    /// </summary>
    public const uint MaskAll = unchecked((uint)-1);

    /// <summary>
    /// An unsigned 32-bit mask of folder <see cref="MarginBase{TColor}" /> indexes (25 through 31) where each bit corresponds to a margin index.
    /// </summary>
    /// <seealso cref="MarginBase{TColor}.Mask" />
    public const uint MaskFolders = SC_MASK_FOLDERS;

    /// <summary>
    /// Folder end marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxPlusConnected" /> symbol.
    /// </summary>
    public const int FolderEnd = SC_MARKNUM_FOLDEREND;

    /// <summary>
    /// Folder open marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxMinusConnected" /> symbol.
    /// </summary>
    public const int FolderOpenMid = SC_MARKNUM_FOLDEROPENMID;

    /// <summary>
    /// Folder mid tail marker index. This marker is typically configured to display the <see cref="MarkerSymbol.TCorner" /> symbol.
    /// </summary>
    public const int FolderMidTail = SC_MARKNUM_FOLDERMIDTAIL;

    /// <summary>
    /// Folder tail marker index. This marker is typically configured to display the <see cref="MarkerSymbol.LCorner" /> symbol.
    /// </summary>
    public const int FolderTail = SC_MARKNUM_FOLDERTAIL;

    /// <summary>
    /// Folder sub marker index. This marker is typically configured to display the <see cref="MarkerSymbol.VLine" /> symbol.
    /// </summary>
    public const int FolderSub = SC_MARKNUM_FOLDERSUB;

    /// <summary>
    /// Folder marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxPlus" /> symbol.
    /// </summary>
    public const int Folder = SC_MARKNUM_FOLDER;

    /// <summary>
    /// Folder open marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxMinus" /> symbol.
    /// </summary>
    public const int FolderOpen = SC_MARKNUM_FOLDEROPEN;
}