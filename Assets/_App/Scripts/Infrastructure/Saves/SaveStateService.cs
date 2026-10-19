using System;
using System.Globalization;
using DevKit.Saves;
using DevKit.Utils;
using JustUseMe.Data.GameState;
using JustUseMe.Data.Settings;
using JustUseMe.SaveState;

namespace JustUseMe.Infrastructure.Saves
{
    public class SaveStateService : ISaveStateLoader, ISaveStateProvider
    {
        public ISettingsState SettingsState => _settingsState;
        public IGameState GameState => _gameState;

        private SettingsStateV1 _settingsState;
        private GameStateV1 _gameState;

        private readonly ISaveSystem _saveSystem;

        private const string SAVED_GAMES_PATH = "SAVED_GAMES/";
        private const string SETTINGS_STATE = "SETTINGS_STATE";

        public SaveStateService(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void SaveSettings()
        {
            _saveSystem.Save(SETTINGS_STATE, _settingsState);
            FLogger.LogGood<SaveStateService>("Settings were saved");
        }

        public string SaveGame()
        {
            var saveId = DateTime.Now.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture);

            _saveSystem.Save(GetGameKey(saveId), _gameState);

            FLogger.LogGood<SaveStateService>($"Game was saved [{saveId}]");
            return saveId;
        }

        public void LoadSettings()
        {
            if (_saveSystem.Exists(SETTINGS_STATE))
            {
                // var saveFileFata = _saveSystem.LoadRaw(SETTINGS_STATE);
                // var migrator = new Migrator(new MigrationV1ToV2());

                // if (migrator.TryMigrateIfNecessary<SettingsStateV2>(saveFileFata, out _settingsState))
                //     SaveSettings();

                _settingsState = _saveSystem.Load<SettingsStateV1>(SETTINGS_STATE);

                if (_settingsState != null)
                {
                    FLogger.LogGood<SaveStateService>("Settings state loaded");
                    return;
                }
            }

            FLogger.Log<SaveStateService>("Settings state not found. Creating a new one");

            _settingsState = new SettingsStateV1();
            SaveSettings();
        }

        public void LoadGame(string gameId = null)
        {
            if (gameId != null)
            {
                _gameState = _saveSystem.Load<GameStateV1>(GetGameKey(gameId));

                if (_gameState != null)
                {
                    FLogger.LogGood<SaveStateService>($"Game state loaded: {gameId}");
                    return;
                }
            }

            FLogger.Log<SaveStateService>("Game state not found. Creating a new one");

            _gameState = new GameStateV1();
        }

        private static string GetGameKey(string gameId)
        {
            return $"{SAVED_GAMES_PATH}{gameId}";
        }
    }
}
