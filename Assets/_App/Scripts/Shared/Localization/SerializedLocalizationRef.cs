using System;
using SparksOfTheAbyss.Shared.Refs;
using UnityEngine;

namespace SparksOfTheAbyss.Shared.Localization
{
    [Serializable]
    public abstract class SerializedLocalizationRef : ISerializedTypedRef<SerializableLocalizationLoader>
    {
        public abstract Type RegistrationType { get; }

        [field: SerializeField] public SerializableLocalizationLoader Value { get; private set; }
    }
}