using System;
using DevKit.Locator;
using DevKit.Saves;
using DevKit.UI.MVVM;
using Reflex.Core;
using SparksOfTheAbyss.Infrastructure.Saves;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.SaveState;
using SparksOfTheAbyss.Shared.Localization;
using UnityEngine;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public class RootInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private RootUIBinder _rootUIBinderPrefab;
        [SerializeField] private SerializableLocalizationLoader _rootLocalizationLoader;

        private RootUIBinder _rootUIBinder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _rootUIBinder = Instantiate(_rootUIBinderPrefab);
            DontDestroyOnLoad(_rootUIBinder);

            builder.RegisterValue(_rootUIBinder, new Type[] { typeof(IRootUIBinder) });

            var localizer = new UnityLocalizer();
            ServiceLocator.Current.Register<ILocalizer>(localizer);

            RegisterSaves(builder);

            builder.RegisterValue(new RootLocalizationLoaderKey(_rootLocalizationLoader));

            builder.OnContainerBuilt += OnBuilt;
        }

        private void OnBuilt(Container container)
        {
            if (container.TryGetResolver<LoadingScreen>(out var loadingScreenResolver))
            {
                var loadingScreen = (LoadingScreen)loadingScreenResolver.Resolve(container);
                loadingScreen.transform.SetParent(_rootUIBinder.transform);
            }
        }

        private void RegisterSaves(ContainerBuilder builder)
        {
            // var serializer = new NewtonsoftSerializer();
            var serializer = new JsonUtilitySerializer();
            var storage = new FileStorage(fileExtension: "json");
            var saveSystem = new SimpleSaveSystem(serializer, storage);
            var saveStateService = new SaveStateService(saveSystem);

            saveStateService.LoadAll();

            builder.RegisterValue(saveStateService,
                new Type[] { typeof(ISaveStateLoader), typeof(ISaveStateProvider) });

            // var saveState = saveStateProvider.SaveState;
            // var settingsState = saveState.SettingsState;
            // builder.RegisterInstance<SettingsState>(settingsState);
            // builder.Register<SettingsModel>(Lifetime.Singleton);
        }
    }
}