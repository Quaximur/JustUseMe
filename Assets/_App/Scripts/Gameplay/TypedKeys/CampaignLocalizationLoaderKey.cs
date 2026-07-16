using ITCafe;
using SparksOfTheAbyss.Shared;

namespace SparksOfTheAbyss.TypedKeys
{
    public class CampaignLocalizationLoaderKey : TypedKey<ILocalizationLoader>
    {
        public CampaignLocalizationLoaderKey(ILocalizationLoader value) : base(value)
        {
        }
    }
}