using System.Collections.Generic;
using DevKit.UI.MVVM.Bases;
using DevKit.Utils;
using SparksOfTheAbyss.Campaign;
using SparksOfTheAbyss.Data.Campaign;
using ObservableCollections;
using R3;
using SparksOfTheAbyss.TypedKeys;

namespace SparksOfTheAbyss.UI.MVVM
{
    public enum CampaignViewState
    {
        None, // if Location is selected but not exist already (due to game update). Wait for data
        Locations,
        Missions
    }

    public class CampaignViewModel : ScreenViewModel
    {
        public Observable<IReadOnlyDictionary<string, ILocationData>> LocationsDataMap => _locationsDataMap;
        public Observable<IReadOnlyDictionary<string, IMissionData>> CurrentMissionsDataMap => _currentMissionsDataMap;
        public ReadOnlyReactiveProperty<ILocationData> SelectedLocationData => _selectedLocationData;
        public ReadOnlyReactiveProperty<IMissionData> SelectedMissionData => _selectedMissionData;
        public IReadOnlyObservableDictionary<string, LocationModel> OpenedLocationsMap => _openedLocationsMap;
        public IReadOnlyObservableDictionary<string, MissionModel> OpenedMissionsMap => _openedMissionsMap;
        public Observable<CampaignViewState> OnStateShanged => _onStateChanged;

        private readonly CampaignDataLoader _campaignDataLoader;
        private readonly Subject<Unit> _startMissionSubject;

        private CampaignModel _campaignModel;
        private CampaignDataModel _campaignDataModel;

        private readonly ReactiveProperty<ILocationData> _selectedLocationData = new();
        private readonly ReactiveProperty<IMissionData> _selectedMissionData = new();
        private readonly ReactiveProperty<IReadOnlyDictionary<string, ILocationData>> _locationsDataMap = new();
        private readonly ReactiveProperty<IReadOnlyDictionary<string, IMissionData>> _currentMissionsDataMap = new();
        private readonly ObservableDictionary<string, MissionModel> _openedMissionsMap = new();
        private readonly ObservableDictionary<string, LocationModel> _openedLocationsMap = new();
        private readonly ReactiveProperty<CampaignViewState> _onStateChanged = new(CampaignViewState.None);
        private CompositeDisposable _disposables;

        public CampaignViewModel(CampaignDataLoader campaignDataLoader,
            StartSessionSubjectKey startMissionSubjectKey)
        {
            _campaignDataLoader = campaignDataLoader;
            _startMissionSubject = startMissionSubjectKey.Value;
        }

        public void Bind(CampaignModel campaignModel, CampaignDataModel campaignDataModel)
        {
            _disposables = new();

            _campaignModel = campaignModel;
            BindOpenedLocations(_campaignModel.OpenedLocationsMap);

            _campaignDataModel = campaignDataModel;
            _campaignDataModel.IsLoaded
                .Where(x => x == true)
                .Take(1)
                .Subscribe(_ => BindDataOnLoaded())
                .AddTo(_disposables);
        }

        public void StartGameplay()
        {
            _campaignModel.LastLaunchedLocationId.Value = _campaignModel.SelectedLocationId.Value;
            _campaignModel.LastLaunchedMissionId.Value = _campaignModel.SelectedMissionId.Value;
            _startMissionSubject.OnNext(Unit.Default);
        }

        public void SelectLocation(ILocationData locationData)
        {
            // if (locationData != null && !_openedLocationsMap.ContainsKey(locationData.Id))
            //     return;

            _campaignModel.SelectedLocationId.Value = locationData?.Id;
            _campaignModel.SelectedMissionId.Value = null;
            GoToMissions();
        }

        public void GoToLocations()
        {
            SelectLocation(null);
            _onStateChanged.Value = CampaignViewState.Locations;
        }

        private void GoToMissions()
        {
            _onStateChanged.Value = CampaignViewState.Missions;
        }

        public void SelectMission(IMissionData missionData)
        {
            _campaignModel.SelectedMissionId.Value = missionData?.Id;
        }

        /// <summary>
        /// Binds Campaign opened locations/missions providing
        /// runtime locations/missions opening synchronization support.
        /// </summary>
        private void BindOpenedLocations(IDictionary<string, LocationModel> locationsMap)
        {
            foreach (var idLocationPair in locationsMap)
                BindLocation(idLocationPair.Value);

            _campaignModel.OpenedLocationsMap.ObserveAdd()
                .Subscribe(x => BindLocation(x.Value.Value))
                .AddTo(_disposables);

            return;


            void BindLocation(LocationModel locationModel)
            {
                _openedLocationsMap.Add(locationModel.State.Id, locationModel);

                foreach (var idMissionPair in locationModel.OpenedMissionsMap)
                    _openedMissionsMap.Add(idMissionPair.Key, idMissionPair.Value);

                locationModel.OpenedMissionsMap.ObserveAdd()
                    .Subscribe(x => _openedMissionsMap.Add(x.Value.Key, x.Value.Value))
                    .AddTo(_disposables);
            }
        }

        private void BindDataOnLoaded()
        {
            FLogger.Log<CampaignViewModel>("Campaign Data Binding");

            _campaignDataModel.LocationsDataMap.Subscribe(x => _locationsDataMap.Value = x)
                .AddTo(_disposables);

            _campaignDataModel.SelectedLocationData.Subscribe(OnCurrentLocationDataChanged)
                .AddTo(_disposables);

            _campaignDataModel.CurrentMissionsDataMap.Subscribe(OnCurrentMissionsDataMapChanged)
                .AddTo(_disposables);

            _campaignDataModel.SelectedMissionData.Subscribe(x => _selectedMissionData.Value = x)
                .AddTo(_disposables);

            _campaignModel.SelectedLocationId
                .Subscribe(OnLocationIdSelected)
                .AddTo(_disposables);

            _campaignModel.SelectedMissionId.Subscribe(OnMissionIdSelected).AddTo(_disposables);
        }

        private void OnLocationIdSelected(string id)
        {
            // if (!string.IsNullOrEmpty(id))
            //     _campaignModel.SelectedMissionId.Value = null;

            if (string.IsNullOrEmpty(id))
            {
                _campaignDataLoader.DeselectLocation(_campaignDataModel);
                return;
            }

            if (!_campaignDataModel.LocationsDataMap.CurrentValue.TryGetValue(id, out var locationData))
            {
                FLogger.LogWarning<CampaignDataLoader>($"No location data found for {id}");
                return;
            }

            _campaignDataModel.SelectedLocationData.Value = locationData;
            _campaignDataLoader.RefreshCurrentMissionsAsync(_campaignDataModel, locationData.MissionIds).Forget();
            _onStateChanged.Value = CampaignViewState.Missions;
        }

        private void OnMissionIdSelected(string id)
        {
            // TODO: SelectMission + RefreshCurrentMissionInfoAsync
            _campaignDataLoader.SelectMissionAsync(_campaignDataModel, id).Forget();
        }

        private void OnCurrentLocationDataChanged(ILocationData locationData)
        {
            _selectedLocationData.Value = locationData;
        }

        private void OnCurrentMissionsDataMapChanged(IReadOnlyDictionary<string, IMissionData> missionsDataMap)
        {
            _currentMissionsDataMap.Value = missionsDataMap ?? new Dictionary<string, IMissionData>();
            // TODO: SelectMission + RefreshCurrentMissionInfoAsync
            _campaignDataLoader.SelectMissionAsync(_campaignDataModel, _campaignModel.SelectedMissionId.CurrentValue)
                .Forget();
        }

        public override void Dispose()
        {
            Disposes.ClearDispose(ref _disposables);
            _campaignDataLoader.UnloadAll(_campaignDataModel);
        }
    }
}