using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using SparksOfTheAbyss.Infrastructure.Contexts;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.TypedKeys;

namespace SparksOfTheAbyss.Infrastructure.Bootstraps
{
    public class GameplayBoot : Bootstrap
    {
        private GameplayEnterContext _gameplayEnterContext;

        protected override void OnConfigure(ContainerBuilder builder)
        {
            // REGISTRATIONS

            builder.RegisterValue(new ExitGameplaySubjectKey(new Subject<Unit>()));

            //
        }

        public async UniTask<Observable<GameplayExitContext>> BootAsync(GameplayEnterContext gameplayEnterContext)
        {
            _gameplayEnterContext = gameplayEnterContext;

            Build(); // CRUTIAL: build the Container with dependencies

            // INITIALIZATION

            //

            var waitQueue = _container.Resolve<BootstrapWaitQueueKey>().Value;
            await waitQueue.WaitForCompletion(); // CRUTIAL: resolve and await modules init

            var exitSignal = _container.Resolve<ExitGameplaySubjectKey>().Value;
            var gameplayExitSignal = new Subject<GameplayExitContext>();
            var mainMenuEnterContext = new MainMenuEnterContext();

            var gameplayExitContext = new GameplayExitContext(mainMenuEnterContext);

            exitSignal.Take(1)
                .Subscribe(_ =>
                {
                    gameplayExitSignal.OnNext(gameplayExitContext);
                })
                .AddTo(ref _disposables);

            return await UniTask.FromResult<Observable<GameplayExitContext>>(gameplayExitSignal);
        }
    }
}