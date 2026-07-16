using DevKit.Saves;
// using SparksOfTheAbyss.Data.Settings;
using SparksOfTheAbyss.Data.Campaign;

namespace SparksOfTheAbyss.Infrastructure.Saves
{
    public class SaveStateV1 : SaveStateBase, ISaveState
    {
        public override int Version { get; set; } = 2;
        // public SettingsState SettingsState { get; set; } 
        public CampaignState CampaignState  { get; set; } 
    }
}