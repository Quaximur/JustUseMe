using System;
using DevKit.UI.MVVM;
using Reflex.Core;
using UnityEngine;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public class RootInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private RootUIBinder _rootUIBinderPrefab;
        // [SerializeField] private LocalizationLoader _rootLocalizationLoader;

        private RootUIBinder _rootUIBinder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _rootUIBinder = Instantiate(_rootUIBinderPrefab);
            DontDestroyOnLoad(_rootUIBinder);

            builder.RegisterValue(_rootUIBinder, new Type[] { typeof(IRootUIBinder) });
            builder.OnContainerBuilt += OnBuilt;

            // builder.RegisterValue(new RootLocalizationLoaderRef(_rootLocalizationLoader));
        }

        private void OnBuilt(Container container)
        {
            if (container.TryGetResolver<LoadingScreen>(out var loadingScreenResolver))
            {
                var loadingScreen = (LoadingScreen)loadingScreenResolver.Resolve(container);
                loadingScreen.transform.SetParent(_rootUIBinder.transform);
            }
        }
    }
}