using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using JustUseMe.Infrastructure.Contexts;
using JustUseMe.Infrastructure.TypedKeys;
using JustUseMe.TypedKeys;

namespace JustUseMe.Infrastructure.Bootstraps
{
    public class GameplayBoot : Bootstrap<GameplayEnterContext, GameplayExitContext>
    {
        private GameplayEnterContext _gameplayEnterContext;

        protected override void OnConfigure(ContainerBuilder builder)
        {
            // REGISTRATIONS
            // + additional loaded configs registering from IEnterContext

            builder.RegisterValue(new ExitGameplaySubjectKey(new Subject<Unit>()));

            //
        }

        public override async UniTask<Observable<GameplayExitContext>> BootAsync(GameplayEnterContext gameplayEnterContext)
        {
            _gameplayEnterContext = gameplayEnterContext;

            // + wait for additional assets loading from enter context

            Build(); // CRUTIAL: build the Container with dependencies

            // INITIALIZATION

            //

            var waitQueue = Container.Resolve<BootstrapWaitQueueKey>().Value;
            await waitQueue.ExecuteAsync(); // CRUTIAL: resolve and await modules init

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