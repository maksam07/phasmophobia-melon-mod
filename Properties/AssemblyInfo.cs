using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(C4PhasMod.BuildInfo.Name)]
[assembly: AssemblyDescription(C4PhasMod.BuildInfo.Description)]
[assembly: AssemblyCompany(C4PhasMod.BuildInfo.Company)]
[assembly: AssemblyProduct(C4PhasMod.BuildInfo.Name)]
[assembly: AssemblyCopyright("Copyright © " + C4PhasMod.BuildInfo.Author + " 2021")]
[assembly: AssemblyTrademark(C4PhasMod.BuildInfo.Company)]
[assembly: AssemblyVersion(C4PhasMod.BuildInfo.Version)]
[assembly: AssemblyFileVersion(C4PhasMod.BuildInfo.Version)]
[assembly: MelonInfo(typeof(C4PhasMod.Main), C4PhasMod.BuildInfo.Name, C4PhasMod.BuildInfo.Version, C4PhasMod.BuildInfo.Author, C4PhasMod.BuildInfo.DownloadLink)]
[assembly: MelonColor(System.ConsoleColor.DarkCyan)]
[assembly: MelonGame("Kinetic Games", "Phasmophobia")]
