using DevKit.Utils;
using UnityEngine.Localization.Tables;

namespace JustUseMe.Shared.Localization
{
    public interface ILocaleTableCollection
    {
        InspectorReadonlyList<TableReference> TableReferences { get; }
    }
}