using System.Collections.Generic;
using UnityEngine;

namespace SparksOfTheAbyss.Data
{
    public interface IMissionSetup
    {
        public GameObject SceneSetupPrefab { get; }
        public IMissionEvaluation MissionEvaluation { get; }
        // public ILocaleTableCollection LocaleTableCollection { get; }
    }
}