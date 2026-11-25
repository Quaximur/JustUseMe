using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Reflex.Extensions;
using System;
using R3;
using DevKit.Utils;
using JustUseMe.Infrastructure.TypedKeys;
using JustUseMe.Shared.Utils;
using JustUseMe.Infrastructure.Contexts;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace JustUseMe.Infrastructure.Bootstraps
{
    public abstract class Bootstrap<TEnterContext, TExitContext> : MonoBehaviour, IDisposable
        where TEnterContext : IEnterContext
        where TExitContext : IExitContext
    {
        [SerializeField] private ContainerScope _containerScope;

        protected Container Container { get; private set; }
        protected TEnterContext EnterContext { get; private set; }
        protected DisposableBag _disposables = new();

        public UniTask<Observable<TExitContext>> BootAsync(TEnterContext enterContext,
            CancellationToken cancellationToken = default)
        {
            EnterContext = enterContext;
            return OnBootAsync(enterContext, cancellationToken);
        }

        protected abstract UniTask<Observable<TExitContext>> OnBootAsync(TEnterContext enterContext,
            CancellationToken cancellationToken = default);

        protected void Build()
        {
            ContainerScope.OnSceneContainerBuilding += AdditionalRegistrations;

            _containerScope.Run();
            Container = gameObject.scene.GetSceneContainer();
        }

        protected async UniTask BuildModulesAsync(CancellationToken cancellationToken = default)
        {
            var waitQueue = Container.Resolve<BootstrapWaitQueueKey>().Value;
            await waitQueue.ExecuteAsync(cancellationToken);
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