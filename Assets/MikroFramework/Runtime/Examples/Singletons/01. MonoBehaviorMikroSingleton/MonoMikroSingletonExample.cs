using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using MikroFramework.Utilities;
using UnityEngine;

namespace MikroFramework.Examples
{
    public class MonoMikroSingletonExample : MonoMikroSingleton<MonoMikroSingletonExample>
    {
        
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MikroFramework/Examples/Singletons/01. MonoMikroSingleton (see code)", false, 1)]
        private static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("MonoMikroSingletonExample").AddComponent<MonoMikroSingletonExample>();
        }
#endif

        private void Awake() {
            Singleton.Test();
        }

        void Test(){}

        public override void OnSingletonInit() {
            base.OnSingletonInit();
            Debug.Log("Singleton Init");
        }

        //[RuntimeInitializeOnLoadMethod]
        private static void Example() {
            var initInstance= MonoMikroSingletonExample.Singleton;

            initInstance = MonoMikroSingletonExample.Singleton;
        }


    }

}