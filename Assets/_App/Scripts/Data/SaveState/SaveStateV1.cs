using DevKit.Saves;
using JustUseMe.Data.Settings;

namespace JustUseMe.Data.SaveState
{
    public class SaveStateV1 : SaveStateBase, ISaveState
    {
        public override int Version { get; set; } = 1;
        public SettingsState SettingsState { get; set; } 
    }
}