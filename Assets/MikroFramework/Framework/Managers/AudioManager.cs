using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.Managers {
    public class AudioManager : MonoPersistentMikroSingleton<AudioManager>
    {
        
        private AudioListener audioListener;
        /// <summary>
        /// Play an audio clip
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayAudio(string audioName)
        {
            CheckAudioListener();

            AudioClip clip = Resources.Load<AudioClip>(audioName);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.clip = clip;
            audioSource.Play();
        }

        private AudioSource bgmAudioSource = null;

        /// <summary>
        /// Play a background music (a scene can only have one bgm at a time)
        /// </summary>
        /// <param name="musicName"></param>
        /// <param name="loop"></param>
        public void PlayBGM(string musicName,bool loop)
        {
            CheckAudioListener();

            if (!bgmAudioSource) {
                bgmAudioSource = gameObject.AddComponent<AudioSource>();
            }
            AudioClip clip = Resources.Load<AudioClip>(musicName);

            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = loop;
            bgmAudioSource.Play();
        }

        /// <summary>
        /// Stop playing the current BGM
        /// </summary>
        public void StopBGM() {
            bgmAudioSource.Stop();
        }


        /// <summary>
        /// Pause playing the current BGM
        /// </summary>
        public void PauseBGM() {
            bgmAudioSource.Pause();
        }

        /// <summary>
        /// Resume the paused BGM
        /// </summary>
        public void ResumeBGM() {
            bgmAudioSource.UnPause();
        }

        /// <summary>
        /// Mute the BGM
        /// </summary>
        public void BGMMute() {
            bgmAudioSource.Pause();
            bgmAudioSource.mute = true;
        }

        /// <summary>
        /// Turn off all audios
        /// </summary>
        public void AudioOff() {
            AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources) {
                if (audioSource != bgmAudioSource) {
                    audioSource.Pause();
                    audioSource.mute = true;
                }
            }
        }

        /// <summary>
        /// Turn on the BGM
        /// </summary>
        public void BGMOn()
        {
            bgmAudioSource.UnPause();
            bgmAudioSource.mute = false;
        }
        /// <summary>
        /// Turn on all audios
        /// </summary>
        public void AudioOn()
        {
            AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != bgmAudioSource)
                {
                    audioSource.UnPause();
                    audioSource.mute = false;
                }
            }
        }

        private void CheckAudioListener()
        {
            if (!audioListener) {
                audioListener = FindObjectOfType<AudioListener>();

                if (!audioListener) {
                    audioListener = gameObject.AddComponent<AudioListener>();
                }
                
            }
        }
    }

}
