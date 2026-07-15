using Cysharp.Threading.Tasks;
using DevKit.Utils;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace SparksOfTheAbyss.Infrastructure
{
    public class EntryPoint
    {
        private static EntryPoint _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Enter()
        {
            _instance = new();
            _instance.Run();
        }

        private void Run()
        {
            var loadingScreenPrefab = Resources.Load<LoadingScreen>("loading_screen");
            var loadingScreen = Object.Instantiate(loadingScreenPrefab);
            Object.DontDestroyOnLoad(loadingScreen);

            loadingScreen.Show();

            ContainerScope.OnRootContainerBuilding += builder =>
            {
                builder.RegisterValue(loadingScreen);
                FLogger.Log<EntryPoint>("Additional Root registrations");
            };

            UnityInjector.InitRootContainer();

            RunAsync().Forget();

            return;

            async UniTaskVoid RunAsync()
            {
                // await LocalizationSettings.InitializationOperation;

                // var localizationLoader = Container.RootContainer.Resolve<RootLocalizationLoaderRef>().Value;
                // localizationLoader.Init();

                // await localizationLoader.LoadTablesAsync();

                var sceneLoader = new SceneLoader(loadingScreen);

                await sceneLoader.LoadStartScene();
            }
        }
    }
}