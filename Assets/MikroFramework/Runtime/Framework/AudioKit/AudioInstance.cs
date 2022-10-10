using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.AudioKit
{
    public class AudioInstance<T> : MonoBehaviour, ISingleton where T : AudioInstance<T> {

        private AudioSource audioSource;
        public static AudioSource Singleton {
            get {
                return SingletonProperty<AudioInstance<T>>.Singleton.audioSource;
            }
        }

        public static AudioSource singleton;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
            if (singleton != null)
            {
                Destroy(gameObject);
            }
            else {
                singleton = Singleton;
                DontDestroyOnLoad(gameObject);
            }

         
        }

        public void OnSingletonInit() {
            
        }
    }
}
