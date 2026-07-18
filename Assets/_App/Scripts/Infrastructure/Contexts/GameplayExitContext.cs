namespace SparksOfTheAbyss.Infrastructure.Contexts
{
    public class GameplayExitContext : IExitContext
    {
        public SceneContext EnterContext { get; }

        public GameplayExitContext(SceneContext enterContext)
        {
            EnterContext = enterContext;
        }
    }
}