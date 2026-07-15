using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Reflex.Extensions;
using System;
using R3;
using DevKit.Utils;

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

        protected virtual void OnContainerPreBuilt(ContainerBuilder sceneBuilder)
        {

        }

        private void AdditionalRegistrations(Scene _, ContainerBuilder sceneBuilder)
        {
            OnContainerPreBuilt(sceneBuilder);
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