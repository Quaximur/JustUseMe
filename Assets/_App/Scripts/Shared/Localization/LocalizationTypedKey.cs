namespace SparksOfTheAbyss.Shared.Localization
{
    public abstract class LocalizationTypedKey : TypedKey<ILocalizationLoader>
    {
        protected LocalizationTypedKey(ILocalizationLoader value) : base(value)
        {
        }
    }
}