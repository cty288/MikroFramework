using System.Collections;
using System.Collections.Generic;
using MikroFramework.Managers;
using UnityEngine;

namespace MikroFramework.Examples {
    public class AudioManagerExample : MikroBehavior {
        protected override void OnBeforeDestroy() {
            
        }
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MikroFramework/Examples/Managers/AudioManager", false)]
        static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("AudioExample").AddComponent<AudioManagerExample>();
        }
#endif

        void Start() {
            AudioManager.Singleton.PlayAudio("Lose");
            AudioManager.Singleton.PlayBGM("War loop",true);
            Delay(5f, () => {
                AudioManager.Singleton.StopBGM();
                Delay(2f, () => {
                    AudioManager.Singleton.PlayBGM("War loop", true);
                    Delay(3f, () => {
                        AudioManager.Singleton.PauseBGM();
                        Delay(1f, () => {
                            AudioManager.Singleton.ResumeBGM();
                            Delay(2f, () => {
                                AudioManager.Singleton.BGMMute();
                                Delay(2f, () => { AudioManager.Singleton.BGMOn(); });
                            });
                            
                        });

                    });

                });


            });

        }
    }
}

