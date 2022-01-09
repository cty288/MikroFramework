using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.TimeSystem;
using UnityEngine;

public class TimeSystemTest : MonoBehaviour
{
    private void Start() {
        Debug.Log(Time.time);

        TimeSystem timeSystem = new TimeSystem();
        timeSystem.AddDelayTask(3, () => {
            Debug.Log(Time.time);
        });

        timeSystem.AddDelayTask(5, () => {
            Debug.Log(Time.time);
        });
    }
}
