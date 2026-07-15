using Cysharp.Threading.Tasks;
using Reflex.Core;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using UnityEngine;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public abstract class ModuleInstaller : MonoBehaviour, IInstaller, IModuleInstaller
    {
        public abstract void Install(ContainerBuilder builder);
        public abstract UniTask OnInitialize();

        protected Container _container;

        void IInstaller.InstallBindings(ContainerBuilder builder)
        {
            Install(builder);
            builder.OnContainerBuilt += OnBuilt;
        }

        private void OnBuilt(Container container)
        {
            _container = container;
            
            var waitQueue = container.Resolve<BootstrapWaitQueueKey>().Value;
            waitQueue.Add(OnInitialize);
        }
    }
}