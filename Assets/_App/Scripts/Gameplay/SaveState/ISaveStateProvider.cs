using JustUseMe.Data.SaveState;

namespace JustUseMe.SaveState
{
    public interface ISaveStateProvider
    {
        public ISaveState SaveState { get; }
    }
}