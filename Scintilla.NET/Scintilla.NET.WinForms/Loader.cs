using System.Text;
using Scintilla.NET.Abstractions.Classes;

namespace Scintilla.NET.WinForms;

internal sealed class Loader : LoaderBase
{
    /// <inheritdoc />
    public Loader(nint ptr, Encoding encoding) : base(ptr, encoding)
    {
    }
}