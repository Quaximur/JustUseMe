using DevKit.Saves;
// using SparksOfTheAbyss.Data.Settings;
using SparksOfTheAbyss.Data.Campaign;
using SparksOfTheAbyss.Data.Settings;

namespace SparksOfTheAbyss.Data.SaveState
{
    public class SaveStateV1 : SaveStateBase, ISaveState
    {
        public override int Version { get; set; } = 1;
        public SettingsState SettingsState { get; set; } 
        public CampaignState CampaignState  { get; set; } 
    }
}