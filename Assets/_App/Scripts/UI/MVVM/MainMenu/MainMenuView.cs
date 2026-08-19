using DevKit.UI.MVVM.Bases;
using UnityEngine;
using UnityEngine.UIElements;
using JustUseMe.Shared;
using Reflex.Attributes;
using DevKit.Locator;

namespace JustUseMe.UI.MVVM
{
    public class MainMenuView : AttachableToolkitScreen<MainMenuViewModel>
    {
        [SerializeField] private string _playButtonName = "PlayButton";
        [SerializeField] private string _exitButtonName = "ExitButton";
        [SerializeField] private string _settingsButtonName = "SettingsButton";
        // [SerializeField] private string _creditsButtonName = "CreditsButton";

        [Header("SFX"), Space(4)]
        [SerializeField] private SfxData _buttonClickSfx;

        private Button _playButton;
        private Button _exitButton;
        private Button _settingsButton;
        // private Button _creditsButton;
        
        private IAudioPlayer _audioPlayer;

        protected override void OnInit()
        {
            _playButton = Root.Q<Button>(name: _playButtonName);
            _settingsButton = Root.Q<Button>(name: _settingsButtonName);
            // _creditsButton =  Root.Q<Button>(name: _creditsButtonName);
            _exitButton = Root.Q<Button>(name: _exitButtonName);

            _audioPlayer = ServiceLocator.Current.Get<IAudioPlayer>();
        }

        protected override void OnBind(MainMenuViewModel viewModel)
        {
            base.OnBind(viewModel);
            
            _playButton.RegisterCallback<ClickEvent>(OnStartClicked);
            _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);
            // _creditsButton.RegisterCallback<ClickEvent>(OnCreditsClicked);
            _exitButton.RegisterCallbackOnce<ClickEvent>(OnQuitClicked);
        }

        private void OnStartClicked(ClickEvent clickEvent)
        {
            PlayButtonSfx();
            ViewModel.StartGameplay();
        }

        private void OnSettingsClicked(ClickEvent clickEvent)
        {
            PlayButtonSfx();
            ViewModel.OpenSettings();
        }
        
        // private void OnCreditsClicked(ClickEvent clickEvent)
        // {
        //     PlayButtonSfx();
        //     ViewModel.OpenCredits();
        // }

        private void OnQuitClicked(ClickEvent clickEvent)
        {
            PlayButtonSfx();
            ViewModel.Quit();
        }

        private void PlayButtonSfx()
        {
            if (_buttonClickSfx.IsValid)
                _audioPlayer.GetSfxBuilder().Play(_buttonClickSfx);
        }
    }
}