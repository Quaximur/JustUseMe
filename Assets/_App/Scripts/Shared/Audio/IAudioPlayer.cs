using UnityEngine;

namespace JustUseMe.Shared
{
    public interface IAudioPlayer
    {
        public void PlaySingletonMusic(AudioClip musicClip, bool loop = true);
        public void PauseMusic();
        public void StopSingletonMusic();
        public void UnPauseMusic();

        public SfxBuilder GetSfxBuilder();
        public void PlayOneShotSingletonSfx(AudioClip clip, float volumeScale = 1);
        public void Release(SfxSource sfxSource);
        public void StopAllSfx();
    }
}