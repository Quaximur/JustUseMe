using System;

namespace SparksOfTheAbyss.Shared.Refs
{
    /// <summary>
    /// Used for dynamic registration only. 
    /// If only one module (or with child modules dependent on the first) registers and uses the key, 
    /// create TypedKey directly in ModuleInstaller class instead
    /// </summary>
    public interface ISerializedTypedRef<T>
    {
        public Type RegistrationType { get; }
        public T Value { get; }
    }
}