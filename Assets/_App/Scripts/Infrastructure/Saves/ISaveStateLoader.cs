namespace SparksOfTheAbyss.Infrastructure.Saves
{
    public interface ISaveStateLoader
    {
        public void SaveAll();
        public void LoadAll();
    }
}