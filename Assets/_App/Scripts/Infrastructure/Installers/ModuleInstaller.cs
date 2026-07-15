using Cysharp.Threading.Tasks;
using Reflex.Core;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using UnityEngine;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public abstract class ModuleInstaller : MonoBehaviour, IInstaller, IModuleRegistrator
    {
        public abstract void Install(ContainerBuilder containerBuilder);
        public abstract UniTask OnInitialize();

        void IInstaller.InstallBindings(ContainerBuilder containerBuilder)
        {
            Install(containerBuilder);
        }

        void IModuleRegistrator.Register(ContainerBuilder builder)
        {
            builder.OnContainerBuilt += OnBuilt;
        }

        private void OnBuilt(Container container)
        {
            var waitQueue = container.Resolve<BootstrapWaitQueueKey>().Value;
            waitQueue.Add(OnInitialize);
        }
    }
}