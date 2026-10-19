using JustUseMe.Data.GameState;
using JustUseMe.Data.Settings;

namespace JustUseMe.SaveState
{
    public interface ISaveStateProvider
    {
        public ISettingsState SettingsState { get; }
        public IGameState GameState { get; }
    }
}
