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

        public void Add(Func<UniTask> initFunc)
        {
            
        }
    }
}