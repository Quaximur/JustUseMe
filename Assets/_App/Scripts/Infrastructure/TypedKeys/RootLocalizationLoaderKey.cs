using JustUseMe.Shared;
using JustUseMe.Shared.Localization;

namespace JustUseMe.Infrastructure.TypedKeys
{
    public class RootLocalizationLoaderKey : TypedKey<ILocalizationLoader>
    {
        public RootLocalizationLoaderKey(ILocalizationLoader value) : base(value)
        {
        }
    }
}