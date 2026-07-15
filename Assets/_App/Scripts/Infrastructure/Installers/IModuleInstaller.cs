using Cysharp.Threading.Tasks;
using Reflex.Core;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public interface IModuleInstaller
    {
        public void Install(ContainerBuilder containerBuilder);
        public UniTask OnInitialize();
    }
}