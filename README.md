﻿# Phasmophobia MelonLoader Mod (Only crucial updates)   
Only works with a [bypass](https://github.com/Cr4nkSt4r/PhasBypass)  
This mod is **discontinued** and only receives crucial updates.  
Useful pull requests will be accepted.

# Issues are disabled again
Most of the leechers aren't even able to fill proper issue templates nor are they able to read the instructions at the end.  
I get so many useless Issues, I can't spent my time on that anymore.  
As a result, I lost the fun on this project again and keep it in the discontinued state.


# Informations
Just a private project to learn some basic programming of Unity Engine Mods with the help of MelonLoader.  
Place the generated DLL into the MelonLoader Mods folder.   
  
  
**Features**  
\- Simple Box ESP  
\- Ghost Bone ESP  
\- Bone/OuijaBoard/FuseBox/EMF ESP  
\- Show Ghost ESP function  
\- Fullbright mode  
\- Basic ghost informations (Ghost Name/Type/State, responds to)  
\- Revealed evidence  
\- Show own sanity  
\- Show active and completed missions  
\- Console window for logging (MelonLoader)  
\- Change player name (can be automated on launch)  


**Hotkeys**  
Insert | Delete | Right Ctrl | RIGHT arrow: Open GUI  
UP arrow: Toggle ESP  
LEFT arrow: Toggle basic informations  
DOWN arrow: Toggle fullbright  


**Screenshots**  
\- [Mod v.11.1.1](Images/v11.1.1.png)  
\- [Mod v.11.1.0](Images/v11.1.0.jpg)  
\- [Mod v.7.2](Images/v7.2.png)  
\- [Mod v.7.0](Images/v7.0_HAC-Edition.jpg)  
\- [Mod v.6.1](Images/v6.1.png)  
\- [Mod v.5](Images/v5.png)  
\- [Mod v.4.3](Images/v4.3.png)  
\- [Mod v.4.2](Images/v4.2.png)  
\- [Mod v.4](Images/v4.png)  
\- [Mod v.3](Images/v3.png)  
\- [Mod v.2](Images/v2.png)  
\- [Mod v.1](Images/v1.png)


# How to build
1. [Install MelonLoader](https://melonwiki.xyz/#/README) (v0.4.3.0)
2. Make modifications to the MelonLoader files, so it will work with Phasmophobia (look through the solved issues)
2. Start the game without Mod, only with MelonLoader. ML will download stuff to handle the IL2CPP from Phasmophobia
3. Move the `Managed` folder from the `MelonLoader\Managed` location, inside your games folder, to the project folder
4. Move the `Phasmophobia\MelonLoader\MelonLoader.dll` file to the projects new `Managed` folder
5. Compile (Release x64) and move the `bin\x64\Release\C4PhasMod.dll` to the Mod folder inside your `Phasmophobia\MelonLoader` directory

This mod only works with an MelonLoader Bypass or the game will crash because of MelonLoader detections.



# Credits
**Fullbright:** `ShieldSupporter` for sharing the code from `Plagues`  
**2D Box ESP idea:** `EBro912`  
**Others:** `HYPExMon5ter`, `tecnocat`, `Jbosh123`, `Martin951`, `Unk0wnV3rm`, `wh0am15533`, `Obimook`  


# License
**GNU General Public License 3**
