using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class A : MonoBehaviour
    {
        private void Start() {
            TypeEventSystem.RegisterGlobalEvent<SendInfo>(OnMessageGet);
        }

        private void OnMessageGet(SendInfo msg) {
            Debug.Log("Name received "+msg.Name);
        }

        private void OnDestroy() {
            TypeEventSystem.UnRegisterGlobalEvent<SendInfo>(OnMessageGet);
        }
    }
}
