using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class MMSExample : MonoBehaviour {
        private void Start() {
            TestMMS.Singleton.Say233();
        }
    }


    public class TestMMS : MonoBehaviour, ISingleton  {

        public static TestMMS Singleton {
            get {
                return SingletonProperty<TestMMS>.Singleton;
            }
        }
        
        public void Say233() {
            Debug.Log("233");
        }

        void ISingleton.OnSingletonInit() {
            Debug.Log("Init");
        }
    }


}
