using System;
using Cysharp.Threading.Tasks;
using DevKit.Utils;
using R3;
using Reflex.Core;
using JustUseMe.Infrastructure.TypedKeys;
using UnityEngine;

namespace JustUseMe.Infrastructure.Installers
{
    public abstract class ModuleInstaller : MonoBehaviour, IInstaller, IModuleInstaller
    {
        public abstract void Install(ContainerBuilder builder);
        public abstract UniTask OnInitialize();

        protected Container Container { get; private set; }

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
    }
}