using SparksOfTheAbyss.Data.SaveState;

namespace SparksOfTheAbyss.SaveState
{
    public interface ISaveStateProvider
    {
        public ISaveState SaveState { get; }
    }
}