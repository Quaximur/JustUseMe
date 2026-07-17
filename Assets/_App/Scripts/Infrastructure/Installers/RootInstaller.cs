using System;
using DevKit.Locator;
using DevKit.Saves;
using DevKit.UI.MVVM;
using R3;
using Reflex.Core;
using SparksOfTheAbyss.Data.Settings;
using SparksOfTheAbyss.Infrastructure.Saves;
using SparksOfTheAbyss.Infrastructure.SettingsAppliers;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.SaveState;
using SparksOfTheAbyss.Shared.Localization;
using SparksOfTheAbyss.UI.MVVM;
using TradeArea.UI.ConfirmPopUp;
using UnityEngine;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public class RootInstaller : MonoBehaviour, IInstaller
    {
        [Header("Root")]
        [SerializeField] private RootUIBinder _rootUIBinderPrefab;
        [SerializeField] private SerializableLocalizationLoader _rootLocalizationLoader;

        [Space(4), Header("Settings")]
        [SerializeField] private SerializableLocalizationLoader _settingsLocalizationLoader;
        [SerializeField] private SettingsView _settingsViewPrefab;
        [SerializeField] private ConfirmPopUpView _confirmPopUpPrefab;

        private RootUIBinder _rootUIBinder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _rootUIBinder = Instantiate(_rootUIBinderPrefab);
            DontDestroyOnLoad(_rootUIBinder);

            builder.RegisterValue(_rootUIBinder, new Type[] { typeof(IRootUIBinder) });

            var localizer = new UnityLocalizer();
            ServiceLocator.Current.Register<ILocalizer>(localizer);

            RegisterSaves(builder);
            RegisterUI(builder);

            builder.RegisterValue(new RootLocalizationLoaderKey(_rootLocalizationLoader));
            builder.RegisterValue(new SettingsLocalizationLoaderKey(_settingsLocalizationLoader));

            builder.OnContainerBuilt += OnBuilt;
        }

        private void OnBuilt(Container container)
        {
            if (container.TryGetResolver<LoadingScreen>(out var loadingScreenResolver))
            {
                var loadingScreen = (LoadingScreen)loadingScreenResolver.Resolve(container);
                loadingScreen.transform.SetParent(_rootUIBinder.transform);
            }

            InitSettings(container);
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

            var saveSignal = new Subject<Unit>();
            saveSignal.Subscribe(_ => saveStateService.SaveAll())
                .AddTo(this);

            builder.RegisterValue(new SaveSettingsSignalKey(saveSignal));

            var saveState = saveStateService.SaveState;
            var settingsState = saveState.SettingsState;
            builder.RegisterValue(settingsState);
            builder.RegisterType(typeof(SettingsModel), Lifetime.Singleton, Resolution.Lazy);
        }

        private void RegisterUI(ContainerBuilder builder)
        {
            builder.RegisterMVVM<SettingsView, SettingsViewModel, SettingsBinder>(_settingsViewPrefab,
                viewModelLifetime: Lifetime.Transient);

            // PopUp registration with transient binder
            builder.RegisterMVVM<ConfirmPopUpView, ConfirmPopUpViewModel>(_confirmPopUpPrefab,
                viewModelLifetime: Lifetime.Transient,
                binderLifetime: Lifetime.Transient);
        }

        private void RegisterAudio(ContainerBuilder builder)
        {
            //     builder.Register<AudioPlayer>(resolver => 
            //     {
            //         var settingsModel = resolver.Resolve<SettingsModel>();

            //         _audioPlayer.Init(musicVolume: settingsModel.MusicVolume.Select(x => x / 100.0f), 
            //             sfxVolume: settingsModel.SfxVolume.Select(x => x / 100.0f));

            //         ServiceLocator.Current.Register<AudioPlayer>(_audioPlayer);

            //         var loadingScreen = resolver.Resolve<LoadingScreen>();
            //         loadingScreen.OverlayFillProgress.Subscribe(x => _audioPlayer.VolumeMultiplier.Value = 1.0f - x);

            //         var sceneLoader = resolver.Resolve<SceneLoader>();
            //         sceneLoader.OnLoadingStarted.Subscribe(_ => 
            //         {
            //             _audioPlayer.StopAllSfx();
            //             _audioPlayer.PauseMusic();
            //         });
            //         sceneLoader.OnLoadingFinished.Subscribe(_ =>
            //         {
            //             _audioPlayer.UnPauseMusic();
            //         });

            //         return _audioPlayer;
            //     }, Lifetime.Singleton);
        }

        private void InitSettings(Container container)
        {
            var appSettingsModel = container.Resolve<SettingsModel>();

            var appSettingsApplier = new AppSettingsApplier();
            appSettingsApplier.BindSettings(appSettingsModel)
                .AddTo(this);
        }
    }
}