namespace JustUseMe.Infrastructure.Saves
{
    public interface ISaveStateLoader
    {
        public void SaveSettings();
        public string SaveGame(string gameId = null);
        public void LoadSettings();
        public void LoadGame(string gameId = null);
    }
}
