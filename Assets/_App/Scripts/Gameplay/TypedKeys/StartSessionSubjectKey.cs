using R3;
using SparksOfTheAbyss.Shared;

namespace SparksOfTheAbyss.TypedKeys
{
    public class StartSessionSubjectKey : TypedKey<Subject<Unit>>
    {
        public StartSessionSubjectKey(Subject<Unit> value) : base(value)
        {
        }
    }
}