using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class TestClass : MonoMikroSingleton<TestClass> {
        public void SayHello() {
            Debug.Log("Hello");
        }

        public override void OnSingletonInit() {
            Debug.Log("init");
        }
    }

    public class OtherClass {
        public void SomeFunction() {
            TestClass.Singleton.SayHello();
        }
    }
}
