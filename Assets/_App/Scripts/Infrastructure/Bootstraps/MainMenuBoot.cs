using Cysharp.Threading.Tasks;
using DevKit.UI.MVVM;
using R3;
using Reflex.Core;
using SparksOfTheAbyss.Infrastructure.Contexts;
using SparksOfTheAbyss.Infrastructure.TypedKeys;
using SparksOfTheAbyss.TypedKeys;
using SparksOfTheAbyss.UI.MVVM;

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

            var waitQueue = _container.Resolve<BootstrapWaitQueueKey>().Value;
            await waitQueue.ExecuteAsync(); // CRUTIAL: resolve and await modules init

            // var settingsBinder = _container.Resolve<IViewBinder<SettingsViewModel>>();
            // settingsBinder.Open();

            var campaignBinder = _container.Resolve<IViewBinder<CampaignViewModel>>();
            campaignBinder.Open();

            var startSessionSignal = _container.Resolve<StartSessionSubjectKey>().Value;
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