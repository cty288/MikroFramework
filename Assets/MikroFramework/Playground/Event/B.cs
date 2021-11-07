using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class B : MonoBehaviour {
        private void Start() {
            TypeEventSystem.RegisterGlobalEvent<SendInfo>(msg => {
                Debug.Log("2333");
            }).UnRegisterWhenGameObjectDestroyed(this.gameObject);

        }
    }
}
