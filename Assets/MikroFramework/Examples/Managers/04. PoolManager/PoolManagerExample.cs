using System.Collections;
using System.Collections.Generic;
using MikroFramework.Pool;
using MikroFramework.Utilities;
using UnityEngine;

namespace MikroFramework.Examples {
    public class PoolManagerExample : MonoBehaviour
    {
        private class Fish{

        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MikroFramework/Examples/Managers/PoolManager", false)]
        private static void MenuClicked() {
            var fishPool = new SimpleObjectPool<Fish>(() => new Fish(), null, 100);
            Debug.Log($"FishPool current count:{fishPool.CurrentCount}");

            var fishOne = fishPool.Allocate();
            Debug.Log($"FishPool current count:{fishPool.CurrentCount}");

            fishPool.Recycle(fishOne);
            Debug.Log($"FishPool current count:{fishPool.CurrentCount}");

            for (int a = 0; a < 10; a++) {
                fishPool.Allocate();
            }
            Debug.Log($"FishPool current count:{fishPool.CurrentCount}");
        }
#endif
    }

}
