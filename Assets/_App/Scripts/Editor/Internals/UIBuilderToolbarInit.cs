#if UNITY_EDITOR
using SparksOfTheAbyss.Editor.UIInternals;
using UnityEditor;
using UnityEngine;

namespace SparksOfTheAbyss.Editor.Internals
{
    internal class UIBuilderToolbarInit
    {
        [InitializeOnLoadMethod]
        private static void ModifyAddUssPath()
        {
            UIBuilderExtension.ModifyAddUssPath($"{Application.dataPath}/_App/UIToolkit");
        }
        
        [InitializeOnLoadMethod]
        private static void ModifyCreateNewUssPath()
        {
            UIBuilderExtension.ModifyCreateUssPath($"{Application.dataPath}/_App/UIToolkit");
        }
    }
}
#endif