using SparksOfTheAbyss.Shared;
using SparksOfTheAbyss.Shared.Utils;

namespace SparksOfTheAbyss.Infrastructure.TypedKeys
{
    public class BootstrapWaitQueueKey : TypedKey<WaitQueue>
    {
        // TODO: WaitGroup class
        public BootstrapWaitQueueKey(WaitQueue value) : base(value)
        {
        }
    }
}