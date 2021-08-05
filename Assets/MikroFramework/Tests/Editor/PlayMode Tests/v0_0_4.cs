using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MikroFramework.Test {
    public class v0_0_4
    {
        private class SingletonTestClass : MonoMikroSingleton<SingletonTestClass> {
            private SingletonTestClass() {

            }

        }
        // A Test behaves as an ordinary method
        [Test]
        public void v0_0_4SimplePasses() {
            var instanceA = SingletonTestClass.Singleton;
            var instanceB = SingletonTestClass.Singleton;

            Assert.AreEqual(instanceA.GetHashCode(),instanceB.GetHashCode());
            //Assert.AreEqual(instanceA.name,"test");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator v0_0_4WithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }

}
