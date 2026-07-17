using DevKit.UI.MVVM;
using DevKit.UI.MVVM.Bases;
using UnityEngine;

namespace SparksOfTheAbyss.UI.MVVM
{
    public class MainMenuViewModel : ScreenViewModel
    {
        private readonly IViewBinder<CampaignViewModel> _campaignBinder;
        private readonly IViewBinder<SettingsViewModel> _settingsBinder;
        public MainMenuViewModel(IViewBinder<CampaignViewModel> campaignBinder,
            IViewBinder<SettingsViewModel> settingsBinder)
        {
            _campaignBinder = campaignBinder;
            _settingsBinder = settingsBinder;
        }

        public void StartGameplay()
        {
            _campaignBinder.Open();
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