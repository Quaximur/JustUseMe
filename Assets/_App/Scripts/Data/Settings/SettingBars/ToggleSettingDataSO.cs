using UnityEngine;

namespace JustUseMe.Data.Settings
{
    [CreateAssetMenu(fileName = "ToggleSettingDataSO", menuName = "Scriptable Objects/Settings/ToggleSettingDataSO")]
    public class ToggleSettingDataSO : SettingBarDataSO, IToggleSettingData
    {
    }
}