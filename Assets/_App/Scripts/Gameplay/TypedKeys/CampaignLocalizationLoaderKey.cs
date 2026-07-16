using SparksOfTheAbyss.Shared;
using SparksOfTheAbyss.Shared.Localization;

namespace SparksOfTheAbyss.TypedKeys
{
    public class CampaignLocalizationLoaderKey : TypedKey<ILocalizationLoader>
    {
        public CampaignLocalizationLoaderKey(ILocalizationLoader value) : base(value)
        {
        }
    }
}