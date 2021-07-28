using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Pool;
using MikroFramework.Singletons;
using MikroFramework.Utilities;
using UnityEngine;

namespace MikroFramework
{
    public class GameObjectPoolManager : MonoMikroSingleton<GameObjectPoolManager>, 
        IGameObjectPoolManager<SafeGameObjectPool> {

        private Dictionary<string, SafeGameObjectPool> pools;
        public Dictionary<string, SafeGameObjectPool> GameObjectPools {
            get {
                return pools;
            }
        }

        private void Awake() {
            pools = new Dictionary<string, SafeGameObjectPool>();
        }

        public SafeGameObjectPool GetOrCreatePool(GameObject prefab) {
            return CreatePool(prefab, 10, 50);
        }

        public SafeGameObjectPool CreatePool(GameObject prefab, int initialCount, int maxCount) {
            string prefabName = prefab.name;

            if (pools.ContainsKey(prefabName))
            {
                return pools[prefabName];
            }

            
            SafeGameObjectPool pool= SafeGameObjectPool.Create(prefab, initialCount,maxCount);
            GameObjectPools.Add(prefabName,pool);
            return pool;
        }

        public GameObject Allocate(GameObject prefab) {
            string prefabName = prefab.name;

            if (!pools.ContainsKey(prefabName)) {
                Debug.LogError($"The pool does not exist for {prefabName}! Use CreatePool() to create its pool!");
                return null;
            }
            else {
                return pools[prefabName].Allocate();
            }
        }

        public bool Recycle(GameObject recycledObject) {
            string prefabName = CommonUtility.DeleteCloneName(recycledObject);
            recycledObject.name = prefabName;

            if (!pools.ContainsKey(prefabName))
            {
                Debug.LogError($"The pool does not exist for {prefabName}! Use CreatePool() to create its pool!");
                return false;
            }
            else {
                return pools[prefabName].Recycle(recycledObject);
            }

        }
    }
}
