namespace JustUseMe.Infrastructure.Contexts
{
    public abstract class SceneContext : IEnterContext
    {
        public string ToSceneName { get; set; }

        public SceneContext(string toSceneName)
        {
            ToSceneName = toSceneName;
        }
    }
}