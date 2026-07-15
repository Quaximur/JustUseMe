namespace SparksOfTheAbyss.Shared
{
    public abstract class TypedKey<T>
    {
        public readonly T Value;

        protected TypedKey(T value) => Value = value;
        

        public static implicit operator T(TypedKey<T> typedInstance) => typedInstance.Value;
    }
}