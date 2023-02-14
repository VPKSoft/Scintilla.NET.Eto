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

using Eto.Forms;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.EtoForms.EventArguments;

#if Windows
using Eto.WinForms;
using ScintillaNet.WinForms;
#elif Linux
using Eto.GtkSharp;
using Scintilla.NET.Linux;
#elif OSX
#endif

namespace ScintillaNet.Eto;
partial class Scintilla
{
    #region EventHandlerFields
    #if Windows
    private readonly
        List<KeyValuePair<EventHandler<AutoCSelectionEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.AutoCSelectionEventArgs>>?>
        autoCSelectionEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<BeforeModificationEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.BeforeModificationEventArgs>>?>
        beforeModificationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<ChangeAnnotationEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.ChangeAnnotationEventArgs>>?>
        changeAnnotationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<CharAddedEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.CharAddedEventArgs>>?>
        charAddedEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<ModificationEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.ModificationEventArgs>>?>
        modificationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<DoubleClickEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.DoubleClickEventArgs>>?>
        doubleClickEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<DwellEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.DwellEventArgs>>?>
        dwellEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<CallTipClickEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.CallTipClickEventArgs>>?>
        callTipClickEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<UpdateUIEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.UpdateUIEventArgs>>?>
        updateUiEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<StyleNeededEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.StyleNeededEventArgs>>?>
        styleNeededEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<SCNotificationEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.SCNotificationEventArgs>>?>
        sCNotificationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<NeedShownEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.NeedShownEventArgs>>?>
        needShownEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<MarginClickEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.MarginClickEventArgs>>?>
        marginClickEventHandlers = new();
    
    private readonly
        List<KeyValuePair<EventHandler<InsertCheckEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.InsertCheckEventArgs>>?>
        insertCheckEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<IndicatorReleaseEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.IndicatorReleaseEventArgs>>?>
        indicatorReleaseEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<IndicatorClickEventArgs>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.IndicatorClickEventArgs>>?>
        indicatorClickEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
            EventHandler<global::ScintillaNet.WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys>>>?>
        hotspotClickEventHandlers = new();
#elif Linux
    private readonly
        List<KeyValuePair<EventHandler<AutoCSelectionEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.AutoCSelectionEventArgs>>?>
        autoCSelectionEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<BeforeModificationEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.BeforeModificationEventArgs>>?>
        beforeModificationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<ChangeAnnotationEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.ChangeAnnotationEventArgs>>?>
        changeAnnotationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<CharAddedEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.CharAddedEventArgs>>?>
        charAddedEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<ModificationEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.ModificationEventArgs>>?>
        modificationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<DoubleClickEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.DoubleClickEventArgs>>?>
        doubleClickEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<DwellEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.DwellEventArgs>>?>
        dwellEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<CallTipClickEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.CallTipClickEventArgs>>?>
        callTipClickEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<UpdateUIEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.UpdateUIEventArgs>>?>
        updateUiEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<StyleNeededEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.StyleNeededEventArgs>>?>
        styleNeededEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<SCNotificationEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.SCNotificationEventArgs>>?>
        sCNotificationEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<NeedShownEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.NeedShownEventArgs>>?>
        needShownEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<MarginClickEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.MarginClickEventArgs>>?>
        marginClickEventHandlers = new();
    
    private readonly
        List<KeyValuePair<EventHandler<InsertCheckEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.InsertCheckEventArgs>>?>
        insertCheckEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<IndicatorReleaseEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.IndicatorReleaseEventArgs>>?>
        indicatorReleaseEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<IndicatorClickEventArgs>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.IndicatorClickEventArgs>>?>
        indicatorClickEventHandlers = new();

    private readonly
        List<KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
            EventHandler<global::Scintilla.NET.Linux.EventArguments.HotspotClickEventArgs<Gdk.Key>>>?>
        hotspotClickEventHandlers = new();    
#elif OSX
#endif

    #endregion

    /// <inheritdoc cref="IScintillaEvents.AutoCCancelled" />
    public event EventHandler<EventArgs>? AutoCCancelled
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).AutoCCancelled += value;

        remove => ((IScintillaEvents)BaseControl.NativeControl).AutoCCancelled -= value;
    }

    /// <inheritdoc cref="IScintillaEvents.AutoCCharDeleted" />
    public event EventHandler<EventArgs>? AutoCCharDeleted
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).AutoCCharDeleted += value;

        remove => ((IScintillaEvents)BaseControl.NativeControl).AutoCCharDeleted -= value;
    }
    
    /// <inheritdoc />
    public event EventHandler<AutoCSelectionEventArgs>? AutoCCompleted
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.AutoCSelectionEventArgs args) => value.Invoke(
                    sender,
                    new AutoCSelectionEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Position,
                        args.TextPtr, args.Char, args.ListCompletionMethod));

                autoCSelectionEventHandlers.Add(
                    new KeyValuePair<EventHandler<AutoCSelectionEventArgs>,
                        EventHandler<WinForms.EventArguments.AutoCSelectionEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).AutoCCompleted += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.AutoCSelectionEventArgs args) => value.Invoke(
                    sender,
                    new AutoCSelectionEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Position,
                        args.TextPtr, args.Char, args.ListCompletionMethod));

                autoCSelectionEventHandlers.Add(
                    new KeyValuePair<EventHandler<AutoCSelectionEventArgs>,
                        EventHandler<Linux.EventArguments.AutoCSelectionEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).AutoCCompleted += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = autoCSelectionEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    autoCSelectionEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).AutoCCompleted -= handlers.Value.Value;
                }
#elif Linux
                var handlers = autoCSelectionEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    autoCSelectionEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).AutoCCompleted -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }


    /// <inheritdoc />
    public event EventHandler<AutoCSelectionEventArgs>? AutoCSelection
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.AutoCSelectionEventArgs args) => value.Invoke(
                    sender,
                    new AutoCSelectionEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Position,
                        args.TextPtr, args.Char, args.ListCompletionMethod));

                autoCSelectionEventHandlers.Add(
                    new KeyValuePair<EventHandler<AutoCSelectionEventArgs>,
                        EventHandler<WinForms.EventArguments.AutoCSelectionEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).AutoCCompleted += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.AutoCSelectionEventArgs args) => value.Invoke(
                    sender,
                    new AutoCSelectionEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Position,
                        args.TextPtr, args.Char, args.ListCompletionMethod));

                autoCSelectionEventHandlers.Add(
                    new KeyValuePair<EventHandler<AutoCSelectionEventArgs>,
                        EventHandler<Linux.EventArguments.AutoCSelectionEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).AutoCCompleted += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = autoCSelectionEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    autoCSelectionEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).AutoCCompleted -= handlers.Value.Value;
                }
#elif Linux
                var handlers = autoCSelectionEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    autoCSelectionEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).AutoCCompleted -= handlers.Value.Value;
                }                
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? BeforeDelete
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.BeforeModificationEventArgs args) => value.Invoke(
                    sender,
                    new BeforeModificationEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Source, args.Position, args.ByteLength, args.TextPtr));

                beforeModificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<BeforeModificationEventArgs>,
                        EventHandler<WinForms.EventArguments.BeforeModificationEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).BeforeDelete += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.BeforeModificationEventArgs args) => value.Invoke(
                    sender,
                    new BeforeModificationEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Source, args.Position, args.ByteLength, args.TextPtr));

                beforeModificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<BeforeModificationEventArgs>,
                        EventHandler<Linux.EventArguments.BeforeModificationEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).BeforeDelete += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = beforeModificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    beforeModificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).BeforeDelete -= handlers.Value.Value;
                }
#elif Linux
                var handlers = beforeModificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    beforeModificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).BeforeDelete -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? BeforeInsert
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.BeforeModificationEventArgs args) => value.Invoke(
                    sender,
                    new BeforeModificationEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Source, args.Position, args.ByteLength, args.TextPtr));

                beforeModificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<BeforeModificationEventArgs>,
                        EventHandler<WinForms.EventArguments.BeforeModificationEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).BeforeDelete += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.BeforeModificationEventArgs args) => value.Invoke(
                    sender,
                    new BeforeModificationEventArgs((IScintillaApi)BaseControl.NativeControl, Lines, args.Source, args.Position, args.ByteLength, args.TextPtr));

                beforeModificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<BeforeModificationEventArgs>,
                        EventHandler<Linux.EventArguments.BeforeModificationEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).BeforeDelete += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = beforeModificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    beforeModificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).BeforeDelete -= handlers.Value.Value;
                }
#elif Linux
                var handlers = beforeModificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    beforeModificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).BeforeDelete -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<ChangeAnnotationEventArgs>? ChangeAnnotation
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.ChangeAnnotationEventArgs args) => value.Invoke(
                    sender,
                    new ChangeAnnotationEventArgs(args.Line));

                changeAnnotationEventHandlers.Add(
                    new KeyValuePair<EventHandler<ChangeAnnotationEventArgs>,
                        EventHandler<WinForms.EventArguments.ChangeAnnotationEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).ChangeAnnotation += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.ChangeAnnotationEventArgs args) => value.Invoke(
                    sender,
                    new ChangeAnnotationEventArgs(args.Line));

                changeAnnotationEventHandlers.Add(
                    new KeyValuePair<EventHandler<ChangeAnnotationEventArgs>,
                        EventHandler<Linux.EventArguments.ChangeAnnotationEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).ChangeAnnotation += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = changeAnnotationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    changeAnnotationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).ChangeAnnotation -= handlers.Value.Value;
                }
#elif Linux
                var handlers = changeAnnotationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    changeAnnotationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).ChangeAnnotation -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<CharAddedEventArgs>? CharAdded
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.CharAddedEventArgs args) => value.Invoke(
                    sender,
                    new CharAddedEventArgs(args.Char));

                charAddedEventHandlers.Add(
                    new KeyValuePair<EventHandler<CharAddedEventArgs>,
                        EventHandler<WinForms.EventArguments.CharAddedEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).CharAdded += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.CharAddedEventArgs args) => value.Invoke(
                    sender,
                    new CharAddedEventArgs(args.Char));

                charAddedEventHandlers.Add(
                    new KeyValuePair<EventHandler<CharAddedEventArgs>,
                        EventHandler<Linux.EventArguments.CharAddedEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).CharAdded += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = charAddedEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    charAddedEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).CharAdded -= handlers.Value.Value;
                }
#elif Linux
                var handlers = charAddedEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    charAddedEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).CharAdded -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<ModificationEventArgs>? Delete
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.ModificationEventArgs args) => value.Invoke(
                    sender,
                    new ModificationEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Source, args.Position,
                        args.ByteLength, args.TextPtr, args.LinesAdded));

                modificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<ModificationEventArgs>,
                        EventHandler<WinForms.EventArguments.ModificationEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).Delete += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.ModificationEventArgs args) => value.Invoke(
                    sender,
                    new ModificationEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Source, args.Position,
                        args.ByteLength, args.TextPtr, args.LinesAdded));

                modificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<ModificationEventArgs>,
                        EventHandler<Linux.EventArguments.ModificationEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).Delete += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = modificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    modificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).Delete -= handlers.Value.Value;
                }
#elif Linux
                var handlers = modificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    modificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).Delete -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<DoubleClickEventArgs>? DoubleClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.DoubleClickEventArgs args) => value.Invoke(
                    sender,
                    new DoubleClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Modifiers.ToEto(), args.Position, args.Line));

                doubleClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<DoubleClickEventArgs>,
                        EventHandler<WinForms.EventArguments.DoubleClickEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).DoubleClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.DoubleClickEventArgs args) => value.Invoke(
                    sender,
                    new DoubleClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Modifiers.ToEto(), args.Position, args.Line));

                doubleClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<DoubleClickEventArgs>,
                        EventHandler<Linux.EventArguments.DoubleClickEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).DoubleClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = doubleClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    doubleClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).DoubleClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = doubleClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    doubleClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).DoubleClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellEnd
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.DwellEventArgs args) => value.Invoke(
                    sender,
                    new DwellEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Position, args.X, args.Y));

                dwellEventHandlers.Add(
                    new KeyValuePair<EventHandler<DwellEventArgs>,
                        EventHandler<WinForms.EventArguments.DwellEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).DwellEnd += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.DwellEventArgs args) => value.Invoke(
                    sender,
                    new DwellEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Position, args.X, args.Y));

                dwellEventHandlers.Add(
                    new KeyValuePair<EventHandler<DwellEventArgs>,
                        EventHandler<Linux.EventArguments.DwellEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).DwellEnd += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = dwellEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    dwellEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).DwellEnd -= handlers.Value.Value;
                }
#elif Linux
                var handlers = dwellEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    dwellEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).DwellEnd -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<CallTipClickEventArgs>? CallTipClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.CallTipClickEventArgs args) => value.Invoke(
                    sender,
                    new CallTipClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        args.CallTipClickType));

                callTipClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<CallTipClickEventArgs>,
                        EventHandler<WinForms.EventArguments.CallTipClickEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).CallTipClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.CallTipClickEventArgs args) => value.Invoke(
                    sender,
                    new CallTipClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        args.CallTipClickType));

                callTipClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<CallTipClickEventArgs>,
                        EventHandler<Linux.EventArguments.CallTipClickEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).CallTipClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = callTipClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    callTipClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).CallTipClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = callTipClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    callTipClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).CallTipClick -= handlers.Value.Value;
                }                
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellStart
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.DwellEventArgs args) => value.Invoke(
                    sender,
                    new DwellEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Position, args.X, args.Y));

                dwellEventHandlers.Add(
                    new KeyValuePair<EventHandler<DwellEventArgs>,
                        EventHandler<WinForms.EventArguments.DwellEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).DwellStart += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.DwellEventArgs args) => value.Invoke(
                    sender,
                    new DwellEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Position, args.X, args.Y));

                dwellEventHandlers.Add(
                    new KeyValuePair<EventHandler<DwellEventArgs>,
                        EventHandler<Linux.EventArguments.DwellEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).DwellStart += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = dwellEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    dwellEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).DwellStart -= handlers.Value.Value;
                }
#elif Linux
                var handlers = dwellEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    dwellEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).DwellStart -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys> args) => value.Invoke(
                    sender,
                    new HotspotClickEventArgs<Keys>(
                        (IScintillaApi)BaseControl.NativeControl, args.LineCollectionGeneral, args.Modifiers.ToEto(), args.Position));

                hotspotClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
                        EventHandler<WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys>>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).HotspotClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.HotspotClickEventArgs<Gdk.Key> args) => value.Invoke(
                    sender,
                    new HotspotClickEventArgs<Keys>(
                        (IScintillaApi)BaseControl.NativeControl, args.LineCollectionGeneral, args.Modifiers.ToEto(), args.Position));

                hotspotClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
                        EventHandler<Linux.EventArguments.HotspotClickEventArgs<Gdk.Key>>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).HotspotClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = hotspotClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    hotspotClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).HotspotClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = hotspotClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    hotspotClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).HotspotClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotDoubleClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys> args) => value.Invoke(
                    sender,
                    new HotspotClickEventArgs<Keys>(
                        (IScintillaApi)BaseControl.NativeControl, args.LineCollectionGeneral, args.Modifiers.ToEto(), args.Position));

                hotspotClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
                        EventHandler<WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys>>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).HotspotDoubleClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.HotspotClickEventArgs<Gdk.Key> args) => value.Invoke(
                    sender,
                    new HotspotClickEventArgs<Keys>(
                        (IScintillaApi)BaseControl.NativeControl, args.LineCollectionGeneral, args.Modifiers.ToEto(), args.Position));

                hotspotClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
                        EventHandler<Linux.EventArguments.HotspotClickEventArgs<Gdk.Key>>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).HotspotDoubleClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = hotspotClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    hotspotClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).HotspotDoubleClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = hotspotClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    hotspotClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).HotspotDoubleClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotReleaseClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys> args) => value.Invoke(
                    sender,
                    new HotspotClickEventArgs<Keys>(
                        (IScintillaApi)BaseControl.NativeControl, args.LineCollectionGeneral, args.Modifiers.ToEto(), args.Position));

                hotspotClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
                        EventHandler<WinForms.EventArguments.HotspotClickEventArgs<System.Windows.Forms.Keys>>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).HotspotReleaseClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.HotspotClickEventArgs<Gdk.Key> args) => value.Invoke(
                    sender,
                    new HotspotClickEventArgs<Keys>(
                        (IScintillaApi)BaseControl.NativeControl, args.LineCollectionGeneral, args.Modifiers.ToEto(), args.Position));

                hotspotClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<HotspotClickEventArgs<Keys>>,
                        EventHandler<Linux.EventArguments.HotspotClickEventArgs<Gdk.Key>>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).HotspotReleaseClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = hotspotClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    hotspotClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).HotspotReleaseClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = hotspotClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    hotspotClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).HotspotReleaseClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<IndicatorClickEventArgs>? IndicatorClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.IndicatorClickEventArgs args) => value.Invoke(
                    sender,
                    new IndicatorClickEventArgs((IScintillaApi)BaseControl.NativeControl, args.Modifiers.ToEto()));

                indicatorClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<IndicatorClickEventArgs>,
                        EventHandler<WinForms.EventArguments.IndicatorClickEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).IndicatorClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.IndicatorClickEventArgs args) => value.Invoke(
                    sender,
                    new IndicatorClickEventArgs((IScintillaApi)BaseControl.NativeControl, args.Modifiers.ToEto()));

                indicatorClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<IndicatorClickEventArgs>,
                        EventHandler<Linux.EventArguments.IndicatorClickEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).IndicatorClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = indicatorClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    indicatorClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).IndicatorClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = indicatorClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    indicatorClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).IndicatorClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<IndicatorReleaseEventArgs>? IndicatorRelease
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.IndicatorReleaseEventArgs args) => value.Invoke(
                    sender,
                    new IndicatorReleaseEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Position));

                indicatorReleaseEventHandlers.Add(
                    new KeyValuePair<EventHandler<IndicatorReleaseEventArgs>,
                        EventHandler<WinForms.EventArguments.IndicatorReleaseEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).IndicatorRelease += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.IndicatorReleaseEventArgs args) => value.Invoke(
                    sender,
                    new IndicatorReleaseEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Position));

                indicatorReleaseEventHandlers.Add(
                    new KeyValuePair<EventHandler<IndicatorReleaseEventArgs>,
                        EventHandler<Linux.EventArguments.IndicatorReleaseEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).IndicatorRelease += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = indicatorReleaseEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    indicatorReleaseEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).IndicatorRelease -= handlers.Value.Value;
                }
#elif Linux
                var handlers = indicatorReleaseEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    indicatorReleaseEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).IndicatorRelease -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<ModificationEventArgs>? Insert
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.ModificationEventArgs args) => value.Invoke(
                    sender,
                    new ModificationEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Source, args.Position,
                        args.ByteLength, args.TextPtr, args.LinesAdded));

                modificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<ModificationEventArgs>,
                        EventHandler<WinForms.EventArguments.ModificationEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).Insert += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.ModificationEventArgs args) => value.Invoke(
                    sender,
                    new ModificationEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Source, args.Position,
                        args.ByteLength, args.TextPtr, args.LinesAdded));

                modificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<ModificationEventArgs>,
                        EventHandler<Linux.EventArguments.ModificationEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).Insert += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = modificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    modificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).Insert -= handlers.Value.Value;
                }
#elif Linux
                var handlers = modificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    modificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).Insert -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<InsertCheckEventArgs>? InsertCheck
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.InsertCheckEventArgs args) => value.Invoke(
                    sender,
                    new InsertCheckEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Position, args.ByteLength, args.TextPtr));

                insertCheckEventHandlers.Add(
                    new KeyValuePair<EventHandler<InsertCheckEventArgs>,
                        EventHandler<WinForms.EventArguments.InsertCheckEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).InsertCheck += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.InsertCheckEventArgs args) => value.Invoke(
                    sender,
                    new InsertCheckEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Position, args.ByteLength, args.TextPtr));

                insertCheckEventHandlers.Add(
                    new KeyValuePair<EventHandler<InsertCheckEventArgs>,
                        EventHandler<Linux.EventArguments.InsertCheckEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).InsertCheck += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = insertCheckEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    insertCheckEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).InsertCheck -= handlers.Value.Value;
                }
#elif Linux
                var handlers = insertCheckEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    insertCheckEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).InsertCheck -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }


    /// <inheritdoc />
    public event EventHandler<MarginClickEventArgs>? MarginClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.MarginClickEventArgs args) => value.Invoke(
                    sender,
                    new MarginClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Modifiers.ToEto(), args.Position, args.Margin));

                marginClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<MarginClickEventArgs>,
                        EventHandler<WinForms.EventArguments.MarginClickEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).MarginClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.MarginClickEventArgs args) => value.Invoke(
                    sender,
                    new MarginClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Modifiers.ToEto(), args.Position, args.Margin));

                marginClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<MarginClickEventArgs>,
                        EventHandler<Linux.EventArguments.MarginClickEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).MarginClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = marginClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    marginClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).MarginClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = marginClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    marginClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).MarginClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<MarginClickEventArgs>? MarginRightClick
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.MarginClickEventArgs args) => value.Invoke(
                    sender,
                    new MarginClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Modifiers.ToEto(), args.Position, args.Margin));

                marginClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<MarginClickEventArgs>,
                        EventHandler<WinForms.EventArguments.MarginClickEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).MarginRightClick += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.MarginClickEventArgs args) => value.Invoke(
                    sender,
                    new MarginClickEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Modifiers.ToEto(), args.Position, args.Margin));

                marginClickEventHandlers.Add(
                    new KeyValuePair<EventHandler<MarginClickEventArgs>,
                        EventHandler<Linux.EventArguments.MarginClickEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).MarginRightClick += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = marginClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    marginClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).MarginRightClick -= handlers.Value.Value;
                }
#elif Linux
                var handlers = marginClickEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    marginClickEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).MarginRightClick -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc cref="IScintillaEvents.ModifyAttempt" />
    public event EventHandler<EventArgs>? ModifyAttempt
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).ModifyAttempt += value; 

        remove => ((IScintillaEvents)BaseControl.NativeControl).ModifyAttempt -= value;
    }

    /// <inheritdoc />
    public event EventHandler<NeedShownEventArgs>? NeedShown
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.NeedShownEventArgs args) => value.Invoke(
                    sender,
                    new NeedShownEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Position, args.Length));

                needShownEventHandlers.Add(
                    new KeyValuePair<EventHandler<NeedShownEventArgs>,
                        EventHandler<WinForms.EventArguments.NeedShownEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).NeedShown += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.NeedShownEventArgs args) => value.Invoke(
                    sender,
                    new NeedShownEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Position, args.Length));

                needShownEventHandlers.Add(
                    new KeyValuePair<EventHandler<NeedShownEventArgs>,
                        EventHandler<Linux.EventArguments.NeedShownEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).NeedShown += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = needShownEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    needShownEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).NeedShown -= handlers.Value.Value;
                }
#elif Linux
                var handlers = needShownEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    needShownEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).NeedShown -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<SCNotificationEventArgs>? SCNotification
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.SCNotificationEventArgs args) => value.Invoke(
                    sender,
                    new SCNotificationEventArgs(args.SCNotification));

                sCNotificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<SCNotificationEventArgs>,
                        EventHandler<WinForms.EventArguments.SCNotificationEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).SCNotification += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.SCNotificationEventArgs args) => value.Invoke(
                    sender,
                    new SCNotificationEventArgs(args.SCNotification));

                sCNotificationEventHandlers.Add(
                    new KeyValuePair<EventHandler<SCNotificationEventArgs>,
                        EventHandler<Linux.EventArguments.SCNotificationEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).SCNotification += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = sCNotificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    sCNotificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).SCNotification -= handlers.Value.Value;
                }
#elif Linux
                var handlers = sCNotificationEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    sCNotificationEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).SCNotification -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc cref="IScintillaEvents.Painted" />
    public event EventHandler<EventArgs>? Painted
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).Painted += value;

        remove => ((IScintillaEvents)BaseControl.NativeControl).Painted -= value;
    }

    /// <inheritdoc cref="IScintillaEvents.SavePointLeft" />
    public event EventHandler<EventArgs>? SavePointLeft
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).SavePointLeft += value;

        remove => ((IScintillaEvents)BaseControl.NativeControl).SavePointLeft -= value;
    }

    /// <inheritdoc cref="IScintillaEvents.SavePointReached" />
    public event EventHandler<EventArgs>? SavePointReached
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).SavePointReached += value;

        remove => ((IScintillaEvents)BaseControl.NativeControl).SavePointReached -= value;
    }

    /// <inheritdoc />
    public event EventHandler<StyleNeededEventArgs>? StyleNeeded
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.StyleNeededEventArgs args) => value.Invoke(
                    sender,
                    new StyleNeededEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaWinForms)BaseControl.NativeControl).Lines, args.Position));

                styleNeededEventHandlers.Add(
                    new KeyValuePair<EventHandler<StyleNeededEventArgs>,
                        EventHandler<WinForms.EventArguments.StyleNeededEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).StyleNeeded += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.StyleNeededEventArgs args) => value.Invoke(
                    sender,
                    new StyleNeededEventArgs((IScintillaApi)BaseControl.NativeControl,
                        ((IScintillaLinux)BaseControl.NativeControl).Lines, args.Position));

                styleNeededEventHandlers.Add(
                    new KeyValuePair<EventHandler<StyleNeededEventArgs>,
                        EventHandler<Linux.EventArguments.StyleNeededEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).StyleNeeded += Handler;
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = styleNeededEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    styleNeededEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).StyleNeeded -= handlers.Value.Value;
                }
#elif Linux
                var handlers = styleNeededEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    styleNeededEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).StyleNeeded -= handlers.Value.Value;
                }
#elif OSX
#endif
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<UpdateUIEventArgs>? UpdateUi
    {
        add
        {
            if (value != null)
            {
#if Windows
                void Handler(object? sender, WinForms.EventArguments.UpdateUIEventArgs args) => value.Invoke(
                    sender,
                    new UpdateUIEventArgs((IScintillaApi)BaseControl.NativeControl,
                        args.Change));

                updateUiEventHandlers.Add(
                    new KeyValuePair<EventHandler<UpdateUIEventArgs>,
                        EventHandler<WinForms.EventArguments.UpdateUIEventArgs>>(value, Handler));

                ((IScintillaWinForms)BaseControl.NativeControl).UpdateUi += Handler;
#elif Linux
                void Handler(object? sender, Linux.EventArguments.UpdateUIEventArgs args) => value.Invoke(
                    sender,
                    new UpdateUIEventArgs((IScintillaApi)BaseControl.NativeControl,
                        args.Change));

                updateUiEventHandlers.Add(
                    new KeyValuePair<EventHandler<UpdateUIEventArgs>,
                        EventHandler<Linux.EventArguments.UpdateUIEventArgs>>(value, Handler));

                ((IScintillaLinux)BaseControl.NativeControl).UpdateUi += Handler;                
#elif OSX
#endif
            }
        }

        remove
        {
            if (value != null)
            {
#if Windows
                var handlers = updateUiEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    updateUiEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaWinForms)BaseControl.NativeControl).UpdateUi -= handlers.Value.Value;
                }
#elif Linux
                var handlers = updateUiEventHandlers.FirstOrDefault(f => f!.Value.Key == value);

                if (handlers != null)
                {
                    updateUiEventHandlers.RemoveAll(f => f!.Value.Key == value);
                    ((IScintillaLinux)BaseControl.NativeControl).UpdateUi -= handlers.Value.Value;
                }
#elif OSX
#endif

            }
        }
    }

    /// <inheritdoc cref="IScintillaEvents.ZoomChanged" />
    public event EventHandler<EventArgs>? ZoomChanged
    {
        add => ((IScintillaEvents)BaseControl.NativeControl).ZoomChanged += value;

        remove => ((IScintillaEvents)BaseControl.NativeControl).ZoomChanged -= value;
    }
}