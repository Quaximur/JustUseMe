using System;
using System.Collections.Generic;
using R3;

namespace JustUseMe.Shared.Utils
{
    /// <summary>
    /// Emits the product of the current values of a dynamic set of Observable&lt;float&gt; sources.
    /// Re-emits whenever:
    ///   - an observable is added to the set (Add)
    ///   - an observable is removed from the set (Remove)
    ///   - any observable currently in the set emits a new value
    ///
    /// Useful for things like AudioPlayer's volume scalers, where the set of contributing
    /// scalers (music ducking, settings volume, fade, mute, etc.) can grow/shrink at runtime.
    /// </summary>
    public class ObservableProd : Observable<float>, IDisposable
    {
        private readonly List<Observable<float>> _sources = new();
        private readonly Subject<Unit> _collectionChanged = new();
        private readonly object _gate = new();

        public ObservableProd() { }

        public ObservableProd(IEnumerable<Observable<float>> observables)
        {
            _sources.AddRange(observables);
        }

        public void Add(Observable<float> source)
        {
            lock (_gate)
            {
                _sources.Add(source);
            }
            
            _collectionChanged.OnNext(Unit.Default);
        }

        public bool Remove(Observable<float> source)
        {
            bool removed;
            lock (_gate)
            {
                removed = _sources.Remove(source);
            }

            if (removed)
                _collectionChanged.OnNext(Unit.Default);

            return removed;
        }

        protected override IDisposable SubscribeCore(Observer<float> observer)
        {
            // Holds the "live" CombineLatest subscription so it can be swapped out
            // whenever the source collection changes.
            var currentCombine = new SerialDisposable();
            var changeSubscription = _collectionChanged.Subscribe(_ => Resubscribe());

            // Establish the initial CombineLatest over whatever sources exist right now.
            Resubscribe();

            return Disposable.Combine(currentCombine, changeSubscription);

            void Resubscribe()
            {
                List<Observable<float>> snapshot;
                lock (_gate)
                {
                    snapshot = new List<Observable<float>>(_sources);
                }

                if (snapshot.Count == 0)
                {
                    // Nothing to multiply - identity value, and nothing more will
                    // arrive from this branch until a source is added again.
                    currentCombine.Disposable = Disposable.Empty;
                    observer.OnNext(1f);
                    return;
                }

                currentCombine.Disposable = Observable.CombineLatest(snapshot)
                    .Select(static values =>
                    {
                        var product = 1f;
                        foreach (var v in values)
                            product *= v;
                        return product;
                    })
                    .Subscribe(x => observer.OnNext(x));
            }
        }

        public void Dispose()
        {
            _collectionChanged.Dispose();
        }
    }
}