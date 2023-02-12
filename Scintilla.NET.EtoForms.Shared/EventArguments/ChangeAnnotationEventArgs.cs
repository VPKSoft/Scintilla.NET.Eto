﻿using Scintilla.NET.Abstractions.EventArguments;

namespace Scintilla.NET.Eto.Windows.EventArguments;

/// <summary>
/// Provides data for the <see cref="WinForms.Scintilla.ChangeAnnotation" /> event.
/// </summary>
public class ChangeAnnotationEventArgs : ChangeAnnotationEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeAnnotationEventArgs" /> class.
    /// </summary>
    /// <param name="line">The zero-based line index of the annotation that changed.</param>
    public ChangeAnnotationEventArgs(int line) : base(line)
    {
    }
}