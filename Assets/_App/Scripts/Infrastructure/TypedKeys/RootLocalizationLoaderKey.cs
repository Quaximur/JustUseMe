using ITCafe;
using SparksOfTheAbyss.Shared;

namespace SparksOfTheAbyss.Infrastructure.TypedKeys
{
    public class RootLocalizationLoaderKey : TypedKey<ILocalizationLoader>
    {
        public RootLocalizationLoaderKey(ILocalizationLoader value) : base(value)
        {
        }
    }
}