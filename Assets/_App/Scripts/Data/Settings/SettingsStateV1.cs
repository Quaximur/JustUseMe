using System;
using UnityEngine;

namespace JustUseMe.Data.Settings
{
    [Serializable]
    public class SettingsStateV1 : ISettingsState
    {
        public int Version { get; set; } = 1;

        // Sound settings
        [Range(0, 100)] public int MusicVolume { get; set; } = 80;
        [Range(0, 100)] public int SfxVolume { get; set; } = 80;

        // Input settings
        [Range(1, 100)] public int Sensitivity { get; set; } = 50;

        // Video settings
        public bool VSync { get; set; } = false;
        public int FPS { get; set; } = -1;
        [Range(0, 100)] public int Brightness { get; set; } = 30;
        public bool IsPostProcessingEnabled { get; set; } = true;
        public bool IsBloomEnabled { get; set; } = true;
        public bool IsFilmGrainEnabled { get; set; } = true;
        public bool IsChromaticAberrationEnabled { get; set; } = true;
        public bool IsAntiAliasingEnabled { get; set; } = true;
        // public int QualityPreset { get; set; } = 2;
        public int ScreenWidth { get; set; } = -1;
        public int ScreenHeight { get; set; } = -1;
        public bool Fullscreen { get; set; } = true;

        // Language settings
        public string Language { get; set; } = "en";
    }
}
