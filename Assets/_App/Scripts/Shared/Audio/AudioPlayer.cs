using UnityEngine;
using System.Collections.Generic;
using R3;
using UnityEngine.Pool;
using JustUseMe.Shared.Utils;

namespace JustUseMe.Shared
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public readonly ReactiveProperty<float> VolumeMultiplier = new(1.0f);

        [SerializeField] private AudioSource _singletonMusicSource;
        [SerializeField] private AudioSource _singletonSfxSource;
        [SerializeField] private SfxSource _sfxSourcePrefab;
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxPoolSize = 100;

        private IObjectPool<SfxSource> _sfxSourcePool;
        private readonly ReactiveProperty<float> _audioVolumeFadeScale = new(1.0f);

        private readonly List<SfxSource> _activeSfxSources = new();
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

        public SfxSource Get()
        {
            return _sfxSourcePool.Get();
        }

        public void Release(SfxSource soundEmitter)
        {
            _sfxSourcePool.Release(soundEmitter);
        }

        public void StopAllSfx()
        {
            var tempList = new List<SfxSource>(_activeSfxSources);

            foreach (var soundEmitter in tempList)
                soundEmitter.Stop();
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
        /// SFX will continue playing even if scene is unloaded until it`s source will be stopped by script.
        /// No pooling.
        /// </summary>
        public void PlayOneShotSingletonSFX(AudioClip clip, float volumeScale = 1.0f)
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

        private void OnTakeFromPool(SfxSource soundEmitter)
        {
            soundEmitter.gameObject.SetActive(true);
            _activeSfxSources.Add(soundEmitter);
        }

        private void OnReturnedToPool(SfxSource soundEmitter)
        {
            soundEmitter.gameObject.SetActive(false);
            _activeSfxSources.Remove(soundEmitter);
        }

        private void OnDestroyPoolObject(SfxSource soundEmitter)
        {
            if (soundEmitter != null && soundEmitter.gameObject != null)
                Destroy(soundEmitter.gameObject);
        }
    }
}