// using SparksOfTheAbyss.Data.Settings;
using SparksOfTheAbyss.Data.Campaign;
using SparksOfTheAbyss.Data.Settings;

namespace SparksOfTheAbyss.Data.SaveState
{
    public interface ISaveState
    {
        public SettingsState SettingsState { get; }
        public CampaignState CampaignState { get; }
    }
}