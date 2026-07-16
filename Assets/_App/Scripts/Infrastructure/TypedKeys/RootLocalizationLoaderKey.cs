using SparksOfTheAbyss.Shared;
using SparksOfTheAbyss.Shared.Localization;

namespace SparksOfTheAbyss.Infrastructure.TypedKeys
{
    public class RootLocalizationLoaderKey : TypedKey<ILocalizationLoader>
    {
        public RootLocalizationLoaderKey(ILocalizationLoader value) : base(value)
        {
        }
    }
}