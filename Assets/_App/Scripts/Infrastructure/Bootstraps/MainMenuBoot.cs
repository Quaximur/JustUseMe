using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using SparksOfTheAbyss.Infrastructure.Contexts;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.TypedKeys;
using UnityEngine;

namespace SparksOfTheAbyss.Infrastructure.Bootstraps
{
    public class MainMenuBoot : Bootstrap
    {
        private MainMenuEnterContext _mainMenuEnterContext;

       protected override void OnConfigure(ContainerBuilder builder)
        {
            // REGISTRATIONS

            builder.RegisterValue(new StartSessionSubjectKey(new Subject<Unit>()));
 
            //
        }

        public async UniTask<Observable<MainMenuExitContext>> BootAsync(MainMenuEnterContext mainMenuEnterContext = null)
        {
            _mainMenuEnterContext = mainMenuEnterContext;
            
            Build();

            // INITIALIZATION

            //

            var waitQueue = Container.Resolve<BootstrapWaitQueueKey>().Value;
            await waitQueue.ExecuteAsync(); // CRUTIAL: resolve and await modules init

            var startSessionSignal = Container.Resolve<StartSessionSubjectKey>().Value;
            var mainMenuExitSignal = new Subject<MainMenuExitContext>();
            var gameplayEnterContext = new GameplayEnterContext(); // TODO: take context from state
            var mainMenuExitContext = new MainMenuExitContext(gameplayEnterContext);

            startSessionSignal.Take(1).Subscribe(_ =>
            {
                mainMenuExitSignal.OnNext(mainMenuExitContext);
            }).AddTo(ref _disposables);

            return mainMenuExitSignal;
        }
    }
}