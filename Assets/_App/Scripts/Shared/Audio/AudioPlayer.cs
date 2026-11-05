using UnityEngine;
using System.Collections.Generic;
using R3;
using UnityEngine.Pool;
using JustUseMe.Shared.Utils;
using DevKit.Utils;

namespace JustUseMe.Shared
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public readonly ReactiveProperty<float> VolumeMultiplier = new(1.0f);
        public readonly List<SfxSource> ActiveCommonSfx = new();
        public readonly LinkedList<SfxSource> ActiveFrequentSfx = new();

        [SerializeField] private AudioSource _singletonMusicSource;
        [SerializeField] private AudioSource _singletonSfxSource;
        [SerializeField] private SfxSource _sfxSourcePrefab;
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxPoolSize = 100;
        [SerializeField] int _maxFrequentSfx = 30;

        private IObjectPool<SfxSource> _sfxSourcePool;
        private readonly ReactiveProperty<float> _audioVolumeFadeScale = new(1.0f);
        private readonly ObservableProd _musicProduct = new();
        private readonly ObservableProd _sfxProduct = new();

        public void Init()
        {
            _sfxSourcePool = new ObjectPool<SfxSource>(
                CreateSfxSource,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                _collectionCheck,
                _defaultCapacity,
                _maxPoolSize);

            _musicProduct.Add(VolumeMultiplier);
            _musicProduct.Add(_audioVolumeFadeScale);

            _sfxProduct.Add(VolumeMultiplier);
            _sfxProduct.Add(_audioVolumeFadeScale);

            _musicProduct.Subscribe(x => _singletonMusicSource.volume = x)
                .AddTo(this);

            _sfxProduct.Subscribe(x => _singletonSfxSource.volume = x)
                .AddTo(this);
        }

        public void AddMusicScaler(Observable<float> musicScaler)
        {
            _musicProduct.Add(musicScaler);
        }

        public void AddSfxScaler(Observable<float> sfxScaler)
        {
            _sfxProduct.Add(sfxScaler);
        }

        public bool CanPlaySfx(SfxData data)
        {
            if (!data.IsFrequent)
                return true;

            if (ActiveFrequentSfx.Count >= _maxFrequentSfx)
            {
                try
                {
                    ActiveFrequentSfx.First.Value.Stop();
                    return true;
                }
                catch
                {
                    FLogger.LogError<AudioPlayer>("SfxSource is already released");
                }

                return false;
            }

            return true;
        }

        public SfxSource Get()
        {
            return _sfxSourcePool.Get();
        }

        public void Release(SfxSource sfxSource)
        {
            _sfxSourcePool.Release(sfxSource);
        }

        public void StopAllSfx()
        {
            var commonSfxToStop = ActiveCommonSfx.ToArray();
            ActiveCommonSfx.Clear(); // micro optim to prevent O(N) Remove in OnReturnedToPool

            foreach (var sfxSource in commonSfxToStop)
                sfxSource.Stop();

            var frequentNode = ActiveFrequentSfx.First;
            while (frequentNode != null)
            {
                var nextFrequentNode = frequentNode.Next;
                frequentNode.Value.Stop();
                frequentNode = nextFrequentNode;
            }
        }

        public void PlaySingletonMusic(AudioClip musicClip, bool loop = true)
        {
            _singletonMusicSource.clip = musicClip;
            _singletonMusicSource.loop = loop;
            _singletonMusicSource.Play();
        }

        public void StopSingletonMusic()
        {
            _singletonMusicSource.Stop();
        }

        public SfxBuilder GetSfxBuilder()
        {
            return new SfxBuilder(this);
        }

        /// <summary>
        /// No SfxSource pooling. 
        /// Common audio settings will be applied.
        /// Sfx can not be cancelled.
        /// Use for optimization.
        /// </summary>
        public void PlayOneShotSingletonSfx(AudioClip clip, float volumeScale = 1.0f)
        {
            _singletonSfxSource.PlayOneShot(clip, volumeScale);
        }

        public void PauseMusic()
        {
            _singletonMusicSource.Pause();
        }

        public void UnPauseMusic()
        {
            _singletonMusicSource.UnPause();
        }

        private SfxSource CreateSfxSource()
        {
            var sfxSource = Instantiate(_sfxSourcePrefab, transform, true);

            // TODO: bind pause

            _sfxProduct.Subscribe(sfxSource.SetVolumeMultiplier)
                .AddTo(sfxSource.gameObject);

            sfxSource.gameObject.SetActive(false);

            return sfxSource;
        }

        private void OnTakeFromPool(SfxSource sfxSource)
        {
            sfxSource.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(SfxSource sfxSource)
        {
            sfxSource.gameObject.SetActive(false);

            if (sfxSource.Node != null)
            {
                ActiveFrequentSfx.Remove(sfxSource.Node);
                sfxSource.Node = null;
                return;
            }

            ActiveCommonSfx.Remove(sfxSource);
        }

        private void OnDestroyPoolObject(SfxSource sfxSource)
        {
            if (sfxSource != null && sfxSource.gameObject != null)
                Destroy(sfxSource.gameObject);
        }
    }
}