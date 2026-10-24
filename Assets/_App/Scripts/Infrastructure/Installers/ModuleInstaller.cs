using System;
using Cysharp.Threading.Tasks;
using DevKit.Utils;
using R3;
using Reflex.Core;
using JustUseMe.Infrastructure.TypedKeys;
using UnityEngine;

namespace JustUseMe.Infrastructure.Installers
{
    public abstract class ModuleInstaller : MonoBehaviour, IInstaller, IModuleInstaller, IDisposable
    {
        public abstract void Install(ContainerBuilder builder);
        public abstract UniTask OnInitialize();

        protected Container Container { get; private set; }
        protected DisposableBag _disposables = new();

        void IInstaller.InstallBindings(ContainerBuilder builder)
        {
            Install(builder);
            builder.OnContainerBuilt += OnBuilt;
        }

        private void OnBuilt(Container container)
        {
            Container = container;

            var waitQueue = container.Resolve<BootstrapWaitQueueKey>().Value;
            waitQueue.Add(OnInitialize);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            Disposes.ClearDispose(ref _disposables);
        }
    }
}