namespace JustUseMe.Shared
{
    public static class Constants
    {
        public const string GAMEPLAY_START_SIGNAL = nameof(GAMEPLAY_START_SIGNAL);
        public const string GAMEPLAY_RESTART_SIGNAL = nameof(GAMEPLAY_RESTART_SIGNAL);
        public const string GAMEPLAY_EXIT_SIGNAL = nameof(GAMEPLAY_EXIT_SIGNAL);

        #region Addressables Paths
        public const string SETTINGS_DATA_PATH = "settings_data";
        #endregion

        #region Localization Tables
        public const string SHARED_TABLE = "Shared";
        public const string SETTINGS_DATA_TABLE = "SettingsData";
        #endregion
    }
}