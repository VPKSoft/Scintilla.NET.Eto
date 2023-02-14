using System;
using Gdk;
using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces.Collections;
using ScintillaNet.Linux.EventArguments;

namespace ScintillaNet.Linux;
using static Abstractions.ScintillaConstants;
using Keys = Gdk.Key;

/// <summary>
/// Extension methods fop native event generating and handling events for the current platform (Linux & GTK).
/// </summary>
internal static class NativeEventHandling
{
    internal static void ScnModified(this Scintilla scintilla,
        IScintillaLineCollectionGeneral lineCollectionGeneral, 
        ref ScintillaApiStructs.SCNotification scn,
        EventHandler<InsertCheckEventArgs>? insertCheck,
        EventHandler<BeforeModificationEventArgs>? beforeInsert,
        EventHandler<BeforeModificationEventArgs>? beforeDelete, 
        EventHandler<ModificationEventArgs>? insert,
        EventHandler<ModificationEventArgs>? delete,
        EventHandler<ChangeAnnotationEventArgs>? changeAnnotation,
        ref int? cachedPosition,
        ref string? cachedText)
    {
        // The InsertCheck, BeforeInsert, BeforeDelete, Insert, and Delete events can all potentially require
        // the same conversions: byte to char position, char* to string, etc.... To avoid doing the same work
        // multiple times we share that data between events.

        if ((scn.modificationType & SC_MOD_INSERTCHECK) > 0)
        {
            var eventArgs = new InsertCheckEventArgs(scintilla, lineCollectionGeneral, scn.position.ToInt32(), scn.length.ToInt32(), scn.text);
            insertCheck?.Invoke(scintilla, eventArgs);

            cachedPosition = eventArgs.CachedPosition;
            cachedText = eventArgs.CachedText;
        }

        const int sourceMask = SC_PERFORMED_USER | SC_PERFORMED_UNDO | SC_PERFORMED_REDO;

        if ((scn.modificationType & (SC_MOD_BEFOREDELETE | SC_MOD_BEFOREINSERT)) > 0)
        {
            var source = (ModificationSource)(scn.modificationType & sourceMask);
            var eventArgs = new BeforeModificationEventArgs(scintilla, lineCollectionGeneral, source, scn.position.ToInt32(), scn.length.ToInt32(), scn.text)
                {
                    CachedPosition = cachedPosition,
                    CachedText = cachedText,
                };

            if ((scn.modificationType & SC_MOD_BEFOREINSERT) > 0)
            {
                beforeInsert?.Invoke(scintilla, eventArgs);
            }
            else
            {
                beforeDelete?.Invoke(scintilla, eventArgs);
            }

            cachedPosition = eventArgs.CachedPosition;
            cachedText = eventArgs.CachedText;
        }

        if ((scn.modificationType & (SC_MOD_DELETETEXT | SC_MOD_INSERTTEXT)) > 0)
        {
            var source = (ModificationSource)(scn.modificationType & sourceMask);
            var eventArgs = new ModificationEventArgs(scintilla, lineCollectionGeneral, source, scn.position.ToInt32(), scn.length.ToInt32(), scn.text, scn.linesAdded.ToInt32())
                {
                    CachedPosition = cachedPosition,
                    CachedText = cachedText,
                };

            if ((scn.modificationType & SC_MOD_INSERTTEXT) > 0)
            {
                insert?.Invoke(scintilla, eventArgs);
            }
            else
            {
                delete?.Invoke(scintilla, eventArgs);
            }

            // Always clear the cache
            cachedPosition = null;
            cachedText = null;
        }

        if ((scn.modificationType & SC_MOD_CHANGEANNOTATION) > 0)
        {
            var eventArgs = new ChangeAnnotationEventArgs(scn.line.ToInt32());
            changeAnnotation?.Invoke(scintilla, eventArgs);
        }
    }
    
    internal static void ScnMarginClick(
        this Scintilla scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        ref ScintillaApiStructs.SCNotification scn,
        EventHandler<MarginClickEventArgs>? marginClick,
        EventHandler<MarginClickEventArgs>? marginRightClick)
    {
        var keys = (Key)(scn.modifiers << 16);
        var eventArgs = new MarginClickEventArgs(scintilla, lineCollectionGeneral, keys, scn.position.ToInt32(), scn.margin);

        if (scn.nmhdr.code == SCN_MARGINCLICK)
        {
            marginClick?.Invoke(scintilla, eventArgs);
        }
        else
        {
            marginRightClick?.Invoke(scintilla, eventArgs);
        }
    }

    internal static void ScnDoubleClick(
        this Scintilla scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        ref ScintillaApiStructs.SCNotification scn,
        EventHandler<DoubleClickEventArgs>? doubleClick)
    {
        var keys = (Key)(scn.modifiers << 16);
        var eventArgs = new DoubleClickEventArgs(scintilla, lineCollectionGeneral, keys, scn.position.ToInt32(), scn.line.ToInt32());
        doubleClick?.Invoke(scintilla, eventArgs);
    }
    
    internal static void ScnHotspotClick(this Scintilla scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        ref ScintillaApiStructs.SCNotification scn,
        EventHandler<HotspotClickEventArgs<Keys>>? hotspotClick,
        EventHandler<HotspotClickEventArgs<Keys>>? hotspotDoubleClick)
    {
        var keys = (Key)(scn.modifiers << 16);
        var eventArgs = new HotspotClickEventArgs<Keys>(scintilla, lineCollectionGeneral, keys, scn.position.ToInt32());
        switch (scn.nmhdr.code)
        {
            case SCN_HOTSPOTCLICK:
                hotspotClick?.Invoke(scintilla, eventArgs);
                break;

            case SCN_HOTSPOTDOUBLECLICK:
                hotspotDoubleClick?.Invoke(scintilla, eventArgs);
                break;

            case SCN_HOTSPOTRELEASECLICK:
                hotspotDoubleClick?.Invoke(scintilla, eventArgs);
                break;
        }
    }
    
    internal static void ScnIndicatorClick(
        this Scintilla scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral, 
        ref ScintillaApiStructs.SCNotification scn,
        EventHandler<IndicatorClickEventArgs>? indicatorClick,
        EventHandler<IndicatorReleaseEventArgs>? indicatorRelease)
    {
        switch (scn.nmhdr.code)
        {
            case SCN_INDICATORCLICK:
                var keys = (Key)(scn.modifiers << 16);
                indicatorClick?.Invoke(scintilla, new IndicatorClickEventArgs(scintilla, keys));
                break;

            case SCN_INDICATORRELEASE:
                indicatorRelease?.Invoke(scintilla, new IndicatorReleaseEventArgs(scintilla, lineCollectionGeneral, scn.position.ToInt32()));
                break;
        }
    }

}