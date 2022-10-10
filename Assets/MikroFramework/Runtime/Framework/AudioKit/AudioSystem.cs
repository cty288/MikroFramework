using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MikroFramework;
using MikroFramework.Architecture;
using MikroFramework.ResKit;
using MikroFramework.Singletons;

using UnityEngine;
using IArchitecture = MikroFramework.Architecture.IArchitecture;
using ISystem = MikroFramework.Architecture.ISystem;

namespace MikroFramework.AudioKit
{
    
    public enum SoundType {
        Sound2D,
        Sound3D
    }
    public interface IAudioSystem {
        public float MusicVolume { get; set; }
        public float SoundVolume { get; set; }
        void Destroy();
        AudioSource Play2DSound(AudioClip clip, float relativeVolume = 1f, bool loop = false);

        AudioSource Play3DSound(AudioClip clip, Vector3 position, float relativeVolume = 1f, bool loop = false, bool autoDestroy = true);
        AudioSource PlaySound(AudioClip clip, AudioSource audioSource, bool loop, float relativeVolume = 1f, bool autoDestroy = true);
        AudioSource Play2DSound(string clipName, float relativeVolume = 1f, bool loop = false);
        AudioSource Play3DSound(string clipName, Vector3 position, float relativeVolume = 1f, bool loop = false, bool autoDestroy = true);
        AudioSource PlaySound(string clipName, AudioSource audioSource, out AudioClip clip,  bool loop, float relativeVolume = 1f, bool autoDestroy = true);
        void PlayMusic(string clipPath, float relativeVolume = 1f);
        void PlayMusic(AudioClip clip, AudioSource audioSource, float relativeVolume = 1f);
        void PlayMusic(AudioClip clip, float relativeVolume = 1f);
        /// <summary>
        /// Stop all sounds with the same clip name
        /// </summary>
        /// <param name="clipName"></param>
        void StopSound(string clipName);
        void StopSound(AudioSource audioSource);
        /// <summary>
        /// Pause all sounds with the same clip name
        /// </summary>
        /// <param name="clipName"></param>
        void PauseSound(string clipName);

        void PauseSound(AudioSource audioSource);
        /// <summary>
        /// Resume all sounds with the same clip name
        /// </summary>
        /// <param name="clipName"></param>
        void ResumeSound(string clipName);
        
        void ResumeSound(AudioSource audioSource);

        void StopMusic();

        void PauseMusic();

        void ResumeMusic();

        void Initialize(Action onInitialize);
    }
    public class AudioSystem : MonoPersistentMikroSingleton<AudioSystem>, IAudioSystem {
        private AudioSource bgm;
       // private AudioSource sound2D;
        
        
        
        public const string MusicVolumeStorageKey = "AudioSystemMusicVolume";
        public const string SoundVolumeStorageKey = "AudioSystemSoundVolume";
        public const string MasterVolumeStorageKey = "AudioSystemMasterVolume";

        private Dictionary<string, AudioClip> _soundClipDict = new Dictionary<string, AudioClip>();
        private Dictionary<string, AudioClip> _musicClipDict = new Dictionary<string, AudioClip>();

        private ResLoader resLoader;
        private GameObject sound3DPrefab;
        private GameObject sound2DPrefab;

        private Dictionary<string, List<AudioSource>> playingSounds = new Dictionary<string, List<AudioSource>>();
        private Dictionary<AudioSource, float> pausedAudioSourceOriginalVolume = new Dictionary<AudioSource, float>();
        public void Initialize(Action onInitialize) {
            ResLoader.Create((loader => {
                resLoader = loader;

                bgm = BGMAudioInstance.Singleton;
                //sound2D = SoundAudioInstance2D.Singleton;
                //sound3D = SoundAudioInstance3D.Singleton;

                sound3DPrefab = resLoader.LoadSync<GameObject>("others", "3DSound");
                sound2DPrefab = resLoader.LoadSync<GameObject>("others", "2DSound");
                GameObjectPoolManager.AutoCreatePoolWhenAllocating = true;

                DontDestroyOnLoad(gameObject);
                bgm.volume = MusicVolume;
                //sound2D.volume = SoundVolume;

#if UNITY_EDITOR
                //Debug.Log(string.Format("Current BGM and Sound Volume£º{0}£¬{1}", bgm.volume, sound2D.volume));
#endif
        
                onInitialize?.Invoke();
            }));


        }

     


        private IEnumerator _playMusicCoroutine;
        private IEnumerator PlayMusicCoroutine(AudioClip newClip, float relativeVolume)
        {
            float maxVolume = bgm.volume;
            float duration = 1f;
            float timer = 0f;

            if (bgm.clip == null || newClip.name != bgm.clip.name)
            {
                float minVolume = 0f;

                while (timer < duration)
                {
                    bgm.volume = Mathf.Lerp(maxVolume, minVolume, timer / duration);
                    timer += Time.deltaTime;
                    yield return null;
                }
                bgm.Stop();
                bgm.volume = minVolume;
                bgm.clip = newClip;

                bgm.Play();
            }


            maxVolume = MusicVolume * relativeVolume;
            timer = 0f;
            while (timer < duration)
            {
                bgm.volume = Mathf.Lerp(bgm.volume, maxVolume, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }

        }
        private void StartPlayMusicCoroutine(AudioClip newClip, float relativeVolume)
        {
            _playMusicCoroutine = PlayMusicCoroutine(newClip, relativeVolume);
            StartCoroutine(_playMusicCoroutine);
        }
        private void StopPlayMusicCoroutine()
        {
            if (_playMusicCoroutine != null)
            {
                StopCoroutine(_playMusicCoroutine);
                _playMusicCoroutine = null;
            }
        }

    

        private AudioSource Play(string clipName, Dictionary<string, AudioClip> dict, AudioSource audioSource, float relativeVolume,bool loop, bool autoDestroy, Func<AudioClip, AudioSource, bool, float, bool, AudioSource> action, out AudioClip clip)
        {
            if (!dict.ContainsKey(clipName)) {
                 clip = resLoader.LoadSync<AudioClip>( clipName); 
                if (clip != null) {
                    dict.Add(clipName, clip);
#if UNITY_EDITOR
                    Debug.Log("Added Audio£º" + clipName);
#endif
                }
            }

            if (dict.ContainsKey(clipName)) {
                AudioSource source = action?.Invoke(dict[clipName], audioSource, loop, relativeVolume, autoDestroy);
                clip = dict[clipName];
                return source;
            }

            clip = null;
            return null;
        }

        private IArchitecture architecture;
        public void SetArchitecture(IArchitecture architecture) {
            this.architecture = architecture;
        }

      

     


        
        public float MasterVolume
        {
            get {
               
                return PlayerPrefs.GetFloat(MasterVolumeStorageKey, 1f);
            }
            set {
                value = Mathf.Clamp01(value);
                
                PlayerPrefs.SetFloat(MasterVolumeStorageKey, value);
                bgm.volume = MusicVolume;
                //sound2D.volume = SoundVolume;
                //sound3D.volume = SoundVolume;
            }
        }

        public float MusicVolume {
            get
            {
                return PlayerPrefs.GetFloat(MusicVolumeStorageKey, 1f) * 0.05f * MasterVolume;
            }
            set
            {
                value = Mathf.Clamp01(value);
                bgm.volume = value * 0.05f * MasterVolume;
             
                PlayerPrefs.SetFloat(MusicVolumeStorageKey, value);
            }
        }
        public float SoundVolume {
            get
            {
                return PlayerPrefs.GetFloat(SoundVolumeStorageKey, 1f) * 0.5f * MasterVolume ;
            }
            set
            {
                value = Mathf.Clamp01(value);
               // sound2D.volume = value * 0.5f * MasterVolume;
                PlayerPrefs.SetFloat(SoundVolumeStorageKey, value);
            }
        }
        public void Destroy() {
            _soundClipDict.Clear();
            _soundClipDict = null;

            _musicClipDict.Clear();
            _musicClipDict = null;
            resLoader.ReleaseAllAssets();
        }

        public AudioSource Play2DSound(AudioClip clip, float relativeVolume = 1f, bool loop = false) {
            AudioSource source = Create2DAudioSource();
            return PlaySound(clip, source, loop,  relativeVolume, true);
        }
        private AudioSource Create2DAudioSource() {
            GameObject audio2DObj =GameObjectPoolManager.Singleton.Allocate(sound2DPrefab);
            AudioSource audioSource = audio2DObj.GetComponent<AudioSource>();
            audioSource.volume = SoundVolume;
            return audioSource;
        }
        
        public AudioSource Play3DSound(AudioClip clip, Vector3 position, float relativeVolume = 1, bool loop = false, bool autoDestroy = true) {
            AudioSource source = Create3DAudioSource(position);
            PlaySound(clip, source,loop, relativeVolume, autoDestroy);
            return source;
        }
        public AudioSource Play3DSound(string clipName, Vector3 position, float relativeVolume = 1, bool loop = false, bool autoDestroy = true) {
            AudioSource source = Create3DAudioSource(position);
            PlaySound(clipName, source, out AudioClip clip, loop, relativeVolume, autoDestroy);
            return source;
        }

        private AudioSource Create3DAudioSource(Vector3 position) {
            GameObject audio3DObj = GameObjectPoolManager.Singleton.Allocate(sound3DPrefab);
            audio3DObj.transform.position = position;
            AudioSource audioSource = audio3DObj.GetComponent<AudioSource>();
            audioSource.volume = SoundVolume;
            return audioSource;
        }
        
        public AudioSource Play2DSound(string clipName, float relativeVolume = 1f, bool loop=false) {
            AudioSource source = Create2DAudioSource();
            return PlaySound(clipName, source, out AudioClip clip, loop, relativeVolume, true);
        }

      


        public AudioSource PlaySound(AudioClip clip, AudioSource audioSource, bool loop, float relativeVolume = 1f, bool autoDestroy = true) {
            
            audioSource.clip = clip;
            if (clip.length > 0.5f) {
                
                audioSource.volume = 0;
                audioSource.DOFade(SoundVolume * relativeVolume, 0.2f);
            }
            else {
                audioSource.volume = SoundVolume * relativeVolume;
            }
          
            audioSource.Play();
            
            audioSource.loop = loop;
            

            if (playingSounds.ContainsKey(clip.name)) {
                playingSounds[clip.name].Add(audioSource);
            }else {
                playingSounds.Add(clip.name, new List<AudioSource>() {audioSource});
            }
            
            if (autoDestroy && !loop) {
                this.Delay(clip.length, () => {
                    StopSound(audioSource);
                });
            }
            return audioSource;
            //audioSource.PlayOneShot(clip, relativeVolume);
        }
        
        public AudioSource PlaySound(string clipName, AudioSource audioSource,  out AudioClip clip, bool loop, float relativeVolume = 1f, bool autoDestroy = true) {
            audioSource.volume = SoundVolume;
            return Play(clipName, _soundClipDict, audioSource, relativeVolume,loop, autoDestroy, PlaySound, out clip);
        }



        public void PlayMusic(string clipPath, float relativeVolume = 1f) {
            Play(clipPath, _musicClipDict, bgm, relativeVolume, true , false, PlayMusic, out AudioClip clip);
        }

        private AudioSource PlayMusic(AudioClip clip, AudioSource audioSource, bool loop, float relativeVolume = 1f, bool autoDestroy = false) {
            PlayMusic(clip, audioSource, relativeVolume);
            return audioSource;
        }
        public void PlayMusic(AudioClip clip, AudioSource audioSource, float relativeVolume = 1f) {
            PlayMusic(clip, relativeVolume);
        }

        public void PlayMusic(AudioClip clip, float relativeVolume = 1f) {
            StopPlayMusicCoroutine();

            if (bgm.clip == null) {
                bgm.clip = clip;
                bgm.loop = true;
                bgm.volume = MusicVolume * relativeVolume;
            }
          
            StartPlayMusicCoroutine(clip, relativeVolume);
        }

        public void StopSound(string clipName) {
            if (playingSounds.ContainsKey(clipName)) {
                while (playingSounds[clipName].Count > 0) {
                    StopSound(playingSounds[clipName][0]);
                }
            }else {
                Debug.LogWarning("No sound clip named " + clipName + " is playing.");
            }
        }

        public void StopSound(AudioSource audioSource) {
            if (CheckAudioSourceExists(audioSource)) {
                
                audioSource.DOFade(0, 0.5f).OnComplete((() => {
                    if (audioSource) {
                        audioSource.Stop();
                        audioSource.clip = null;
                        GameObjectPoolManager.Singleton.Recycle(audioSource.gameObject);
                    }
                }));

                if (audioSource && audioSource.clip) {
                    if (playingSounds.ContainsKey(audioSource.clip.name)) {
                        playingSounds[audioSource.clip.name].Remove(audioSource);
                    }

                    if (pausedAudioSourceOriginalVolume.ContainsKey(audioSource)) {
                        pausedAudioSourceOriginalVolume.Remove(audioSource);
                    }
                }

                foreach (var sounds in playingSounds.Values) {
                    sounds.RemoveAll((source => !source));
                }

            }
           
        }

        private bool CheckAudioSourceExists(AudioSource source) {
            if (!source) {
                playingSounds.Clear();
                pausedAudioSourceOriginalVolume.Clear();
                return false;
            }

            return true;
        }
        public void PauseSound(string clipName) {
            if (playingSounds.ContainsKey(clipName)) {
                foreach (var audioSource in playingSounds[clipName]) {
                    PauseSound(audioSource);
                }
            }
        }

        public void PauseSound(AudioSource audioSource) {
            PauseAudioSource(audioSource);
        }

        public void ResumeSound(string clipName) {
            if (playingSounds.ContainsKey(clipName)) {
                foreach (var audioSource in playingSounds[clipName]) {
                    ResumeSound(audioSource);
                }
            }
        }

        public void ResumeSound(AudioSource audioSource) {
            ResumeAudioSource(audioSource);
        }

        public void StopMusic() {
            StopPlayMusicCoroutine();
        }

        public void PauseMusic() {
            PauseAudioSource(bgm);
        }

        public void ResumeMusic() {
            ResumeAudioSource(bgm);
        }

        private void PauseAudioSource(AudioSource source) {
            if (pausedAudioSourceOriginalVolume.ContainsKey(source)) {
                return;
            }

            pausedAudioSourceOriginalVolume.Add(source, source.volume);
            
            source.DOFade(0, 0.5f).OnComplete((() => {
                source.Pause();
            }));
        }

        private void ResumeAudioSource(AudioSource source) {
            if (!pausedAudioSourceOriginalVolume.ContainsKey(source)) {
                return;
            }
            
            source.DOFade(pausedAudioSourceOriginalVolume[source], 0.5f).OnComplete((() => {
                source.UnPause();
            }));
            pausedAudioSourceOriginalVolume.Remove(source);
        }
    }
}
