using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace JustUseMe.Shared.Utils
{
    public class WaitQueue
    {
        private readonly List<Func<CancellationToken, UniTask>> _awaitableFuncs = new();

        public async UniTask ExecuteAsync(CancellationToken token = default)
        {
            foreach (var awaitableFunc in _awaitableFuncs)
                await awaitableFunc(token);
        }

        /// <summary> 
        /// Add to wait group. Func will start executing in queue after <see cref="ExecuteAsync"/> call
        /// </summary>
        public void Add(Func<CancellationToken, UniTask> awaitableFunc)
        {
            _awaitableFuncs.Add(awaitableFunc);
        }
    }
}