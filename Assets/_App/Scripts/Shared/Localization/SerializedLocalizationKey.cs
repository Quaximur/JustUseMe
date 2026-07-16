using System;
using SparksOfTheAbyss.Shared.Keys;
using UnityEngine;

namespace SparksOfTheAbyss.Shared.Localization
{
    [Serializable]
    public abstract class SerializedLocalizationKey : ISerializedTypedKey<SerializableLocalizationLoader>
    {
        public abstract Type RegistrationType { get; }

        [field: SerializeField] public SerializableLocalizationLoader Value { get; private set; }
    }
}