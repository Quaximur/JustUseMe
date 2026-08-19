using JustUseMe.Shared;
using JustUseMe.Shared.Utils;

namespace JustUseMe.Infrastructure.TypedKeys
{
    public class BootstrapWaitQueueKey : TypedKey<WaitQueue>
    {
        // TODO: WaitGroup class
        public BootstrapWaitQueueKey(WaitQueue value) : base(value)
        {
        }
    }
}