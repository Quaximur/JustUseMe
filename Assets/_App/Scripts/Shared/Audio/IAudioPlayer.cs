using UnityEngine;

namespace JustUseMe.Shared
{
    public interface IAudioPlayer
    {
        public void PlaySingletonMusic(AudioClip musicClip, bool loop = true);
        public void PauseMusic();
        public void StopSingletonMusic();
        public void UnPauseMusic();

        public SfxSource Get();
        public SfxBuilder GetSfxBuilder();
        public void PlayOneShotSingletonSFX(AudioClip clip, float volumeScale = 1);
        public void Release(SfxSource soundEmitter);
        public void StopAllSfx();
    }
}