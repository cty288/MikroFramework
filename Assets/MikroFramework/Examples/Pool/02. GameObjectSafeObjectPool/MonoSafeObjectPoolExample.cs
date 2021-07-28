using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Pool;
using UnityEngine;

namespace MikroFramework.Examples
{
    public class MonoSafeObjectPoolExample : MonoBehaviour
    {

        List<GameObject> allocatedObjs = new List<GameObject>();

        private SafeGameObjectPool gameObjectPool;
        public GameObject PooledGameObjectPrefab1;

        private void Start() {
            gameObjectPool = SafeGameObjectPool.Create(PooledGameObjectPrefab1,5,15);
            

            //Thread.Sleep(1000);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.D)) {

                foreach (GameObject allocatedObj in allocatedObjs) {
                    gameObjectPool.Recycle(allocatedObj);
                }

                allocatedObjs.Clear();
            }

            if (Input.GetKeyDown(KeyCode.A)) {

                GameObject obj = gameObjectPool.Allocate();
                allocatedObjs.Add(obj);
                Debug.Log(gameObjectPool.CurrentCount);
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                gameObjectPool.MaxCount = 10;
            }

            if (Input.GetKeyDown(KeyCode.F)) {
                gameObjectPool.Recycle(allocatedObjs[0]);
                allocatedObjs.RemoveAt(0);
            }
        }
    }

   
}
