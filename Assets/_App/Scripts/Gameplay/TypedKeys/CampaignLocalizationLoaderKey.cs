using System;
using SparksOfTheAbyss.Shared.Localization;

namespace SparksOfTheAbyss.TypedKeys
{
    [Serializable]
    public class CampaignLocalizationLoaderKey : SerializedLocalizationKey
    {
        public override Type RegistrationType => typeof(CampaignLocalizationLoaderKey);
    }
}