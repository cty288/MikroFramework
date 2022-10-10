using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.AudioKit;
using UnityEngine;

namespace MikroFramework
{
    public class AudioSystemBasicExample : MonoBehaviour {
        private void Awake() {
            AudioSystem.Singleton.Initialize((() => {}));
        }

        private void Start() {
            //bgm
            AudioSystem.Singleton.PlayMusic("War loop");
        }

        private void Update() {
            //2d
            if (Input.GetKeyDown(KeyCode.A)) {
                AudioSystem.Singleton.Play2DSound("Lose", 2f);
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                AudioSystem.Singleton.PauseMusic();
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                AudioSystem.Singleton.ResumeMusic();
            }
            
            //3d
            if (Input.GetKeyDown(KeyCode.B)) {
                AudioSystem.Singleton.Play3DSound("Lose", new Vector3(3, 3, 3));
            }
           
        }
    }
}
