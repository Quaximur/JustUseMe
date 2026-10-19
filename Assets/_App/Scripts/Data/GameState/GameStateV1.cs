using DevKit.Saves;

namespace JustUseMe.Data.GameState
{
    public class GameStateV1 : SaveStateBase, IGameState
    {
        public override int Version { get; set; } = 1;
    }
}
