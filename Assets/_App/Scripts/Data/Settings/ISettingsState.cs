using DevKit.Saves;
using UnityEngine;

namespace JustUseMe.Data.Settings
{
    public interface ISettingsState : IVersioned
    {
        // Sound settings
        [Range(0, 100)] public int MusicVolume { get; set; }
        [Range(0, 100)] public int SfxVolume { get; set; }

        // Input settings
        [Range(1, 100)] public int Sensitivity { get; set; }

        // Video settings
        public bool VSync { get; set; }
        public int FPS { get; set; }
        [Range(0, 100)] public int Brightness { get; set; }
        public bool IsPostProcessingEnabled { get; set; }
        public bool IsBloomEnabled { get; set; }
        public bool IsFilmGrainEnabled { get; set; }
        public bool IsChromaticAberrationEnabled { get; set; }
        public bool IsAntiAliasingEnabled { get; set; }
        // public int QualityPreset { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public bool Fullscreen { get; set; }

        // Language settings
        public string Language { get; set; }
    }
}
