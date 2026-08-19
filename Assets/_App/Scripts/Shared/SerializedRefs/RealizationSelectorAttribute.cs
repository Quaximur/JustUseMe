using System;
using UnityEngine;

namespace JustUseMe.Shared.SerializedRefs
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RealizationSelectorAttribute : PropertyAttribute
    {
        public Type ParentType { get; }

        public RealizationSelectorAttribute(Type parentType)
        {
            ParentType = parentType;
        }
    }
}