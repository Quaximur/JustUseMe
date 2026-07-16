using System.Collections.Generic;

namespace SparksOfTheAbyss.Data.Campaign
{
    public interface ILocationData
    {
        public string Id { get; }
        public string Name { get; }
        public IReadOnlyList<string> MissionIds { get; }
    }
}