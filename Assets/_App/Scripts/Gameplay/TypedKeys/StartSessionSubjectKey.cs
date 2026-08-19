using R3;
using JustUseMe.Shared;

namespace JustUseMe.TypedKeys
{
    public class StartSessionSubjectKey : TypedKey<Subject<Unit>>
    {
        public StartSessionSubjectKey(Subject<Unit> value) : base(value)
        {
        }
    }
}