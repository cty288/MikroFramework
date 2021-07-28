using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MikroFramework.Pool;
using UnityEngine;

namespace MikroFramework.Examples
{
    public class SafeObjectPoolExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MikroFramework/Examples/Pools/01. SafeObjectPoolExample", false, 1)]
        private static void MenuClicked()
        {
            SafeObjectPool<TestPool>.Singleton.Init(0,30);

            List<TestPool> allocatedObjs = new List<TestPool>();

            for (int i = 0; i < 50; i++) { 
                TestPool obj =SafeObjectPool<TestPool>.Singleton.Allocate();
                obj.Id = i;
                Debug.Log($"Allocated Object ID: {obj.Id}");
                allocatedObjs.Add(obj);
            }

            //Thread.Sleep(1000);

            foreach (TestPool allocatedObj in allocatedObjs) {
                SafeObjectPool<TestPool>.Singleton.Recycle(allocatedObj);
            }

            SafeObjectPool<TestPool>.Singleton.Allocate();
            Debug.Log(SafeObjectPool<TestPool>.Singleton.CurrentCount);
            
        }
#endif


        private class TestPool:IPoolable {
            public int Id = 0;


            public void OnRecycled() {
            }

            public bool IsRecycled { get; set; }
        }
    }
}
