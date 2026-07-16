using System.Linq;
using UnityEngine;

namespace SparksOfTheAbyss.Data.Settings
{
    [CreateAssetMenu(fileName = "ResolutionSettingDataSO",
        menuName = "Scriptable Objects/Settings/ResolutionSettingDataSO")]
    public class ResolutionSettingDataSO : OptionsSettingDataSO
    {
        public override string[] Options => Screen.resolutions.Select(x => $"{x.width}x{x.height}").ToArray();
    }
}