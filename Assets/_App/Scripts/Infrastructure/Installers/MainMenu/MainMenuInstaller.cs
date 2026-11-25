using Cysharp.Threading.Tasks;
using DevKit.UI.MVVM;
using Reflex.Core;
using JustUseMe.UI.MVVM;
using UnityEngine;
using System.Threading;

namespace JustUseMe.Infrastructure.Installers
{
    public class MainMenuInstaller : ModuleInstaller
    {
        [SerializeField] private MainMenuView _mainMenuViewPrefab;

        public override void Install(ContainerBuilder builder)
        {
            builder.RegisterMVVM<MainMenuView, MainMenuViewModel>(_mainMenuViewPrefab);
        }

        public override UniTask OnInitialize(CancellationToken token = default)
        {
            var mainMenuBinder = Container.Resolve<IViewBinder<MainMenuViewModel>>();
            mainMenuBinder.Open();

            return UniTask.CompletedTask;
        }
    }
}