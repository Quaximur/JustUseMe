using Cysharp.Threading.Tasks;
using Reflex.Core;

namespace JustUseMe.Infrastructure.Installers
{
    public interface IModuleInstaller
    {
        public void Install(ContainerBuilder containerBuilder);
        public UniTask OnInitialize();
    }
}