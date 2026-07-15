using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Reflex.Extensions;
using System;
using R3;
using DevKit.Utils;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.Shared.Utils;
using SparksOfTheAbyss.Infrastructure.Installers;

namespace SparksOfTheAbyss.Infrastructure.Bootstraps
{
    public abstract class Bootstrap : MonoBehaviour, IDisposable
    {
        [SerializeField] private ContainerScope _containerScope;

        protected Container _container;
        protected DisposableBag _disposables = new();

        protected void Build()
        {
            ContainerScope.OnSceneContainerBuilding += AdditionalRegistrations;
            _containerScope.Run();
            _container = gameObject.scene.GetSceneContainer();
        }

        protected virtual void OnConfigure(ContainerBuilder builder)
        {
        }

        private void AdditionalRegistrations(Scene _, ContainerBuilder builder)
        {
            builder.RegisterValue(new BootstrapWaitQueueKey(new WaitQueue()));

            OnConfigure(builder);

            var modules = GetComponentsInChildren<IModuleRegistrator>(); // TODO: cache found IInstallers from Reflex

            foreach (var module in modules)
                module.Register(builder);

            ContainerScope.OnSceneContainerBuilding -= AdditionalRegistrations;
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