namespace JustUseMe.Data.Settings
{
    public interface IOptionsSettingData : ISettingBarData
    {
        public string[] Options { get; }
        public string[] OverrideDisplayOptions { get; }
    }
}