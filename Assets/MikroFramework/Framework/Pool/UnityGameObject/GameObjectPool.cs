using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Factory;
using MikroFramework.Pool;
using MikroFramework.Singletons;
using MikroFramework.Utilities;
using UnityEngine;

namespace MikroFramework.Pool
{
    public abstract class GameObjectPool : MonoBehaviour, IPool<GameObject>
    {
        protected Stack<GameObject> cachedStack = new Stack<GameObject>();
        protected IObjectFactory<GameObject> prefabFactory;

        [SerializeField] 
        protected GameObject pooledPrefab;

        protected bool inited = false;


        private void Awake() {
            Init();
        }

        public int CurrentCount {
            get { return cachedStack.Count; }
        }

        public virtual GameObject Allocate() {
            GameObject allocatedObj;
            if (!inited) {
                allocatedObj = prefabFactory.Create();
            }
            else {
                allocatedObj = cachedStack.Count > 0 ? cachedStack.Pop() : prefabFactory.Create();
            }

            allocatedObj.name = CommonUtility.DeleteCloneName(allocatedObj);
            
            return allocatedObj;

        }

        public abstract bool Recycle(GameObject obj);

        protected abstract void Init();
    }
}
