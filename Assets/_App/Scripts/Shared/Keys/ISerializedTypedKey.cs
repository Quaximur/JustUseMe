using System;

namespace SparksOfTheAbyss.Shared.Keys
{
    public interface ISerializedTypedKey<T>
    {
        public Type RegistrationType { get; }
        public T Value { get; }
    }
}