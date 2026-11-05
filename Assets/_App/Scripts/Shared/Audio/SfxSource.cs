using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace JustUseMe.Shared
{
    [RequireComponent(typeof(AudioSource))]
    public class SfxSource : MonoBehaviour
    {
        public Observable<Unit> OnDisposed => _onDisposed;
        public SfxData Data { get; private set; }
        public LinkedListNode<SfxSource> Node { get; set; }

        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        private bool IsAppPaused => !Application.runInBackground && !IsFocused;

#if UNITY_EDITOR
        private bool IsFocused => UnityEditorInternal.InternalEditorUtility.isApplicationActive;
#else
        private bool IsFocused => Application.isFocused;
#endif


        private Coroutine _playingCoroutine;
        private AudioPlayer _audioPlayer;
        private readonly Subject<Unit> _onDisposed = new();
        private float _volumeMultiplier = 1.0f;

        public void Init(SfxData data, AudioPlayer audioPlayer)
        {
            Data = data;
            _audioPlayer = audioPlayer;

            AudioSource.clip = data.AudioClip;
            AudioSource.loop = data.IsLoop;

            AudioSource.volume = data.VolumeScale * _volumeMultiplier;
            AudioSource.pitch = data.Pitch;
            AudioSource.spatialBlend = data.SpacialBlend;
            AudioSource.minDistance = data.MinDistance;
            AudioSource.maxDistance = data.MaxDistance;
            AudioSource.rolloffMode = data.RolloffMode;

            if (data.PitchShift)
                AudioSource.pitch += Random.Range(data.MinPitchShift, data.MaxPitchShift);
        }

        public void SetVolumeMultiplier(float volumeScale)
        {
            _volumeMultiplier = volumeScale;
            if (Data != null)
                AudioSource.volume = volumeScale * Data.VolumeScale;
        }

        public void Play()
        {
            if (_playingCoroutine != null)
                StopCoroutine(_playingCoroutine);

            AudioSource.Play();
            _playingCoroutine = StartCoroutine(WaitForSfxToEnd());
        }

        private IEnumerator WaitForSfxToEnd()
        {
            yield return new WaitWhile(() => AudioSource.isPlaying || AudioSource.loop || IsAppPaused);

            Stop();
        }

        public void Stop()
        {
            if (this == null)
                return;

            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }

            AudioSource.Stop();
            _audioPlayer.Release(this);
            _onDisposed.OnNext(Unit.Default);
        }
    }
}