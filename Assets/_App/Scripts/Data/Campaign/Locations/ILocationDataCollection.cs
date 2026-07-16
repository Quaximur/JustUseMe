using System.Collections.Generic;
using DevKit.Saves;

namespace SparksOfTheAbyss.Data.Campaign
{
    public interface ILocationDataCollection : IVersioned
    {
        public IReadOnlyList<ILocationData> AllData { get; }
    }
}