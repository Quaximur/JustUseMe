using System.Collections.Generic;
using UnityEngine;

namespace ITCafe.Data
{
    public interface IMissionSetup
    {
        public GameObject SceneSetupPrefab { get; }
        public IMissionEvaluation MissionEvaluation { get; }
        // public ILocaleTableCollection LocaleTableCollection { get; }
    }
}