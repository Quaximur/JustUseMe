using R3;
using VContainer.Unity;

namespace SparksOfTheAbyss.Infrastructure.Bootstraps
{
    public class Bootstrap : LifetimeScope
    {
        protected CompositeDisposable _disposables = new();
    }
}