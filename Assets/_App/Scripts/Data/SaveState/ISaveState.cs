using JustUseMe.Data.Settings;

namespace JustUseMe.Data.SaveState
{
    public interface ISaveState
    {
        public SettingsState SettingsState { get; }
    }
}