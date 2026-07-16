namespace SparksOfTheAbyss.Data.Settings
{
    public interface ISliderSettingData<T> : ISettingBarData
    {
        public T MinValue { get; }
        public T MaxValue { get; }
    }
}