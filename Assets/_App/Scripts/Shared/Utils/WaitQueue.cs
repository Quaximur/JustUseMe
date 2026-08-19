using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DevKit.Utils;

namespace JustUseMe.Shared.Utils
{
    public class WaitQueue
    {
        private readonly List<Func<UniTask>> _awaitableFuncs = new();

        public async UniTask ExecuteAsync()
        {
            foreach (var awaitableFunc in _awaitableFuncs)
            {
                try
                {
                    await awaitableFunc();
                }
                catch (Exception ex)
                {
                    FLogger.LogError(ex.ToString());
                }
            }
        }

        /// <summary> 
        /// Add to wait group. Func will start executing in queue after <see cref="ExecuteAsync"/> call
        /// </summary>
        public void Add(Func<UniTask> awaitableFunc)
        {
            _awaitableFuncs.Add(awaitableFunc);
        }
    }
}