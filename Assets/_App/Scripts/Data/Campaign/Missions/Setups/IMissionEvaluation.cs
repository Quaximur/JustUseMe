using DevKit.Utils;

namespace SparksOfTheAbyss.Data
{
    public interface IMissionEvaluation
    {
        public InspectorReadonlyList<int> StarEvaluations { get; }
    }
}