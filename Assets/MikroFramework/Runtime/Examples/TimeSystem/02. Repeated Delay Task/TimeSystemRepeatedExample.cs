using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.TimeSystem;
using UnityEngine;

namespace MikroFramework
{
    public class TimeSystemRepeatedExample : MonoBehaviour {
        private int count = 1;
        private ITimeSystem timeSystem;
        private void Awake() {
            timeSystem = new TimeSystem.TimeSystem();
            timeSystem.AddDelayTask(1f, () => {
                Debug.Log($"Repeat {count} times");
                count++;
            }, TimerUnit.Second, 3);


            timeSystem.AddDelayTask(3f, () => {
                Debug.Log($"Infinite Loop");
            }, TimerUnit.Second, -1);
        }


        private void Update() {
            if (Input.GetKeyDown(KeyCode.P)) {
                Debug.Log("Paused");
                timeSystem.Pause();
            }
            
            if (Input.GetKeyDown(KeyCode.R)) {
                Debug.Log("Resumed");
                timeSystem.Start();
            }
        }
    }
}
