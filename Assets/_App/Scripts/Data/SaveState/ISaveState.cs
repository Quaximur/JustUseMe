using SparksOfTheAbyss.Data.Settings;

namespace SparksOfTheAbyss.Data.SaveState
{
    public interface ISaveState
    {
        public SettingsState SettingsState { get; }
    }
}