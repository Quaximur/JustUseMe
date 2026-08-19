using DevKit.UI.MVVM;
using DevKit.UI.MVVM.Bases;
using DevKit.Utils;
using UnityEngine;

namespace SparksOfTheAbyss.UI.MVVM
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