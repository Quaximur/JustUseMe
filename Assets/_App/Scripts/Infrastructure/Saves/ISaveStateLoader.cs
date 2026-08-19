namespace JustUseMe.Infrastructure.Saves
{
    public interface ISaveStateLoader
    {
        public void SaveAll();
        public void LoadAll();
    }
}