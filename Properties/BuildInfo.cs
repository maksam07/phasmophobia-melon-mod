namespace C4PhasMod
{
    class BuildInfo
    {
#if _DEBUG
        public const string Name = "[DEBUG] C4PhasMod";
#else
        public const string Name = "C4PhasMod";
#endif
        public const string Description = "Cr4nkSt4rs Phasmophobia MelonLoader Mod";
        public const string Author = "Cr4nkSt4r";
        public const string Company = null;
#if _DEBUG
        public const string Version = "12.0.0.99";
#else
        public const string Version = "12.0.0";
#endif
        public const string DownloadLink = "https://github.com/Cr4nkSt4r/phasmophobia-melon-mod";
        public const string GameName = "Phamophobia";
        public const string GameDev = "Kinect Games";
    }
}
