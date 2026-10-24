namespace JustUseMe.Infrastructure.Saves
{
    public interface ISaveStateLoader
    {
        public void SaveSettings();
        public string SaveGame();
        public void LoadSettings();
        public void LoadGame(string gameId = null);
    }
}
