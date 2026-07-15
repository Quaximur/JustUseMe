using Reflex.Core;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public interface IModuleRegistrator
    {
        public void Register(ContainerBuilder builder);
    }
}