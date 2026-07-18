using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Reflex.Extensions;
using System;
using R3;
using DevKit.Utils;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.Shared.Utils;
using SparksOfTheAbyss.Infrastructure.Contexts;
using Cysharp.Threading.Tasks;

namespace SparksOfTheAbyss.Infrastructure.Bootstraps
{
    public abstract class Bootstrap<TEnterContext, TExitContext> : MonoBehaviour, IDisposable
        where TEnterContext : IEnterContext
        where TExitContext : IExitContext
    {
        [SerializeField] private ContainerScope _containerScope;

        protected Container Container { get; private set; }
        protected DisposableBag _disposables = new();

        public abstract UniTask<Observable<TExitContext>> BootAsync(TEnterContext mainMenuEnterContext);

        protected void Build()
        {
            ContainerScope.OnSceneContainerBuilding += AdditionalRegistrations;

            _containerScope.Run();
            Container = gameObject.scene.GetSceneContainer();
        }

        protected virtual void OnConfigure(ContainerBuilder builder)
        {
        }

        private void AdditionalRegistrations(Scene _, ContainerBuilder builder)
        {
            builder.RegisterValue(new BootstrapWaitQueueKey(new WaitQueue()));

            OnConfigure(builder);

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