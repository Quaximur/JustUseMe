using System;
using Cysharp.Threading.Tasks;
using R3;

namespace SparksOfTheAbyss.Shared.Localization
{
    public interface ILocalizationLoader : IDisposable
    {
        public void Init();
        public UniTask LoadTablesAsync();
        public Observable<Unit> LoadTablesObservable();
    }
}