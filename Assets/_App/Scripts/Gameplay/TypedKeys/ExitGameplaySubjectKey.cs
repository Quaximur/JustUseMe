using R3;
using SparksOfTheAbyss.Shared;

namespace SparksOfTheAbyss.TypedKeys
{
    public class ExitGameplaySubjectKey : TypedKey<Subject<Unit>>
    {
        public ExitGameplaySubjectKey(Subject<Unit> value) : base(value)
        {
        }
    }
}