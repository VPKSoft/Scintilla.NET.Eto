# Scintilla.NET.Eto
A port of Scintilla.NET to [Eto.Forms](https://github.com/picoe/Eto) as cross-platform control. 

[![.NET NuGet Linux Release](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet_nuget_linux.yml/badge.svg)](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet_nuget_linux.yml) [![.NET NuGet Shared Release](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet_nuget_shared.yml/badge.svg)](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet_nuget_shared.yml) [![.NET NuGet Windows Release](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet-nuget_windows.yml/badge.svg)](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet-nuget_windows.yml) [![Linux and Windows .NET Core](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet.yml/badge.svg)](https://github.com/VPKSoft/Scintilla.NET.Eto/actions/workflows/dotnet.yml)

![Nuget](https://img.shields.io/nuget/v/Scintilla.NET.EtoForms.Shared) ![Nuget](https://img.shields.io/nuget/v/Scintilla.NET.EtoForms.GTK) ![Nuget](https://img.shields.io/nuget/v/Scintilla.NET.Eto.Linux) ![Nuget](https://img.shields.io/nuget/v/Scintilla.NET.EtoForms.WinForms) ![Nuget](https://img.shields.io/nuget/v/Scintilla.NET.EtoForms.Wpf) ![Nuget](https://img.shields.io/nuget/v/Scintilla.NET.Eto.Windows)

Current Support:
  * Gtk / Linux
  * Wpf or WinForms / Windows
The macOS part will probably not be created by me as I have no exprerience of how to use those Cocoa Objective C controls in .NET framework. Hopefully someone can help with that.

The code base of this project is based on the [Scintilla.NET](https://github.com/VPKSoft/Scintilla.NET) WinForms implementation. The API is not fullu compatible as some of the members have been renamed due typos, etc.

To run the test application (Linux / Windows):
```
git clone https://github.com/VPKSoft/Scintilla.NET.Eto.git
cd Scintilla.NET.Eto
otnet run TestApplication.sln
dotnet run --project TestApplication/TestApplication.csproj
```

Linux, GTK:

![image](https://user-images.githubusercontent.com/40712699/218517348-cb1b2feb-ab38-4b79-ab26-2a8ebe835dbe.png)

Windows (Eto.Wpf):

![image](https://user-images.githubusercontent.com/40712699/218517561-703e6fae-9d38-438b-a55a-6a2c8340a4e2.png)
