using System.Runtime.InteropServices;
using System.Text;
using Scintilla.NET.Abstractions.Interfaces;
using Scintilla.NET.Abstractions.Structs;
using static Scintilla.NET.Abstractions.ScintillaConstants;

namespace Scintilla.NET.Abstractions.Classes;

public abstract class LoaderBase : ILoader
{
    private readonly IntPtr self;
    private readonly ScintillaApiStructs.ILoaderVTable32 loader32;
    private readonly ScintillaApiStructs.ILoaderVTable64 loader64;
    private readonly Encoding encoding;

    public virtual unsafe bool AddData(char[] data, int length)
    {
        if (data != null)
        {
            length = HelpersGeneral.Clamp(length, 0, data.Length);
            var bytes = HelpersGeneral.GetBytes(data, length, encoding, zeroTerminated: false);
            fixed (byte* bp = bytes)
            {
                var status = (IntPtr.Size == 4 ? loader32.AddData(self, bp, bytes.Length) : loader64.AddData(self, bp, bytes.Length));
                if (status != SC_STATUS_OK)
                    return false;
            }
        }

        return true;
    }

    public virtual Document ConvertToDocument()
    {
        var ptr = (IntPtr.Size == 4 ? loader32.ConvertToDocument(self) : loader64.ConvertToDocument(self));
        var document = new Document { Value = ptr };
        return document;
    }

    public virtual int Release()
    {
        var count = (IntPtr.Size == 4 ? loader32.Release(self) : loader64.Release(self));
        return count;
    }

    protected unsafe LoaderBase(IntPtr ptr, Encoding encoding)
    {
        this.self = ptr;
        this.encoding = encoding;

        // http://stackoverflow.com/a/985820/2073621
        // http://stackoverflow.com/a/2094715/2073621
        // http://en.wikipedia.org/wiki/Virtual_method_table
        // http://www.openrce.org/articles/full_view/23
        // Because I know that I'm not going to remember all this... In C++, the first
        // variable of an object is a pointer (v[f]ptr) to the virtual table (v[f]table)
        // containing the addresses of each function. The first call below gets the vtable
        // address by following the object ptr to the vptr to the vtable. The second call
        // casts the vtable to a structure with the same memory layout so we can easily
        // invoke each function without having to do any pointer arithmetic. Depending on the
        // architecture, the function calling conventions can be different.

        IntPtr vfptr = *(IntPtr*)ptr;
        if (IntPtr.Size == 4)
        {
            loader32 = (ScintillaApiStructs.ILoaderVTable32)Marshal.PtrToStructure(vfptr,
                typeof(ScintillaApiStructs.ILoaderVTable32));
        }
        else
        {
            loader64 = (ScintillaApiStructs.ILoaderVTable64)Marshal.PtrToStructure(vfptr,
                typeof(ScintillaApiStructs.ILoaderVTable64));
        }
    }
}