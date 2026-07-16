using DevKit.Utils;
using UnityEngine.Localization.Tables;

namespace SparksOfTheAbyss.Shared.Localization
{
    public interface ILocaleTableCollection
    {
        InspectorReadonlyList<TableReference> TableReferences { get; }
    }
}