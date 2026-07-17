using System.Collections.Generic;
// using DevKit.Newtonsoft;
using DevKit.Saves;
using DevKit.Utils;
// using SparksOfTheAbyss.Data.Settings;
using SparksOfTheAbyss.Data.Campaign;
using SparksOfTheAbyss.Data.SaveState;
using SparksOfTheAbyss.Data.Settings;
using SparksOfTheAbyss.SaveState;

namespace SparksOfTheAbyss.Infrastructure.Saves
{
    public class SaveStateService : ISaveStateLoader, ISaveStateProvider
    {
        public ISaveState SaveState => _saveState;

        private SaveStateV1 _saveState;
        private readonly ISaveSystem _saveSystem;
        private const string SAVE_STATE = "SAVE_STATE";

        public SaveStateService(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void LoadAll()
        {
            if (_saveSystem.Exists(SAVE_STATE))
            {
                // var saveFileFata = _saveSystem.LoadRaw(SAVE_STATE);
                // var migrator = new Migrator(new MigrationV1ToV2());

                // if (migrator.TryMigrateIfNecessary<SaveStateV2>(saveFileFata, out _saveState))
                //     SaveAll();

                _saveState = _saveSystem.Load<SaveStateV1>(SAVE_STATE);

                if (_saveState != null && _saveState.CampaignState != null)
                    return;
            }

            _saveState = new SaveStateV1
            {
                SettingsState = new SettingsState(),
                CampaignState = new CampaignState
                {
                    Locations = new List<LocationState>
                        {
                            new("location_1", false, new List<MissionState>
                            {
                                new("mission_1_1", false)
                            })
                        },
                    SelectedLocationId = "location_1",
                    SelectedMissionId = "mission_1_1"
                }
            };

            SaveAll();
            FLogger.Log<SaveStateService>("New save file was created");
        }

        public void SaveAll()
        {
            _saveSystem.Save(SAVE_STATE, _saveState);
            FLogger.LogGood<SaveStateService>("State was saved");
        }
    }
}