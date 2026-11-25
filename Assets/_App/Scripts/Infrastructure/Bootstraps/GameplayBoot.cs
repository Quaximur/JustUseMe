using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using JustUseMe.Infrastructure.Contexts;
using JustUseMe.TypedKeys;
using System.Threading;

namespace JustUseMe.Infrastructure.Bootstraps
{
    public class GameplayBoot : Bootstrap<GameplayEnterContext, GameplayExitContext>
    {
        protected override void OnConfigure(ContainerBuilder builder)
        {
            // WRITE ADDITIONAL SCENE LEVEL REGISTRATIONS

            builder.RegisterValue(new ExitGameplaySubjectKey(new Subject<Unit>()));

            //
        }

        protected override async UniTask<Observable<GameplayExitContext>> OnBootAsync(GameplayEnterContext gameplayEnterContext, 
            CancellationToken cancellationToken = default)
        {
            Build(); // CRUTIAL: build the Container with dependencies

            // WRITE ADDITIONAL SCENE LEVEL INITIALIZATION

            //

            await BuildModulesAsync(cancellationToken); // CRUTIAL: await modules init

            var exitSignal = Container.Resolve<ExitGameplaySubjectKey>().Value;
            var gameplayExitSignal = new Subject<GameplayExitContext>();
            var mainMenuEnterContext = new MainMenuEnterContext();

            var gameplayExitContext = new GameplayExitContext(mainMenuEnterContext);

            exitSignal.Take(1)
                .Subscribe(_ =>
                {
                    gameplayExitSignal.OnNext(gameplayExitContext);
                })
                .AddTo(ref _disposables);

            return gameplayExitSignal;
        }
    }
}