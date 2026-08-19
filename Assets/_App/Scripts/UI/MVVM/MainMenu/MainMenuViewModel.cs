using DevKit.UI.MVVM;
using DevKit.UI.MVVM.Bases;
using DevKit.Utils;
using UnityEngine;

namespace JustUseMe.UI.MVVM
{
    public class MainMenuViewModel : ScreenViewModel
    {
        private readonly IViewBinder<SettingsViewModel> _settingsBinder;

        public MainMenuViewModel(IViewBinder<SettingsViewModel> settingsBinder)
        {
            _settingsBinder = settingsBinder;
        }

        public void StartGameplay()
        {
            FLogger.Log<MainMenuViewModel>("Start Gameplay");
            // it can be _startGameplaySignal.OnNext(Unit.Default) or _campaignBinder.Open()
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OpenSettings()
        {
            _settingsBinder.Open();
        }
    }
}