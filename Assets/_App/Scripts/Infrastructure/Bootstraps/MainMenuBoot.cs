using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using JustUseMe.Infrastructure.Contexts;
using JustUseMe.TypedKeys;
using UnityEngine;
using System.Threading;

namespace JustUseMe.Infrastructure.Bootstraps
{
    public class MainMenuBoot : Bootstrap<MainMenuEnterContext, MainMenuExitContext>
    {
        protected override void OnConfigure(ContainerBuilder builder)
        {
            // WRITE ADDITIONAL SCENE LEVEL REGISTRATIONS

            builder.RegisterValue(new StartSessionSubjectKey(new Subject<Unit>()));

            //
        }

        protected override async UniTask<Observable<MainMenuExitContext>> OnBootAsync(MainMenuEnterContext mainMenuEnterContext = null, 
            CancellationToken cancellationToken = default)
        {
            Build();

            // WRITE ADDITIONAL SCENE LEVEL INITIALIZATION

            //

            await BuildModulesAsync(cancellationToken); // CRUTIAL: await modules init

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