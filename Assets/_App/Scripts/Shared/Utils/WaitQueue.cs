using System;
using Cysharp.Threading.Tasks;

namespace SparksOfTheAbyss.Shared.Utils
{
    public class WaitQueue
    {
        public UniTask WaitForCompletion()
        {
            return UniTask.CompletedTask;
        }

        // Add to wait group. Func will start executing in queue after WaitForCompletion call
        public void Add(Func<UniTask> initFunc)
        {
            
        }

        // TODO: Add and Execute immediately
    }
}