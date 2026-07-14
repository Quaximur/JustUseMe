namespace SparksOfTheAbyss.Infrastructure.Contexts
{
    public class GameplayExitContext
    {
        public SceneContext EnterContext { get; }

        public GameplayExitContext(SceneContext enterContext)
        {
            EnterContext = enterContext;
        }
    }
}