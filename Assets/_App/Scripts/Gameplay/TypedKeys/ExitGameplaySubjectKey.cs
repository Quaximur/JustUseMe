using R3;
using JustUseMe.Shared;

namespace JustUseMe.TypedKeys
{
    public class ExitGameplaySubjectKey : TypedKey<Subject<Unit>>
    {
        public ExitGameplaySubjectKey(Subject<Unit> value) : base(value)
        {
        }
    }
}