using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MikroFramework.Singletons {
    /// <summary>
    /// Use MonoMikroSingleton for persistent singletons to inherit MonoBehavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoPersistentMikroSingleton<T> : MonoMikroSingleton<T> where T : MonoPersistentMikroSingleton<T> {

        protected static T instance=null;

        
        public static T Singleton {
            get
            {
                if (instance == null) {
                    instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1) {
                        Debug.LogWarning($"More than one singleton {typeof(T).Name} exists in the current scene!");
                        return instance;
                    }

                    if (instance == null) {
                        GameObject instanceGameObject = GetOrCreateInstanceGo<T>();


                        instance = instanceGameObject.AddComponent<T>();
                        DontDestroyOnLoad(instance);
                        Debug.Log($"Added new singleton {typeof(T).Name} to the game!");

                    }
                    else {
                        Debug.Log($"{instance.name} already exists!");
                    }
                }
                return instance;
            }
        }




        
    }


    /// <summary>
    /// Use MonoMikroSingleton for singletons to inherit MonoBehavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoMikroSingleton<T> : MonoBehaviour where T : MonoMikroSingleton<T>
    {

        protected static T instance = null;
        /// <summary>
        /// Get if the singleton exists in the game
        /// </summary>
        public static bool Exists
        {
            get
            {
                return FindObjectOfType<T>()!=null;
            }
        }

        public static T Singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogWarning($"More than one singleton {typeof(T).Name} exists in the current scene!");
                        return instance;
                    }

                    if (instance == null) {

                        GameObject instanceGameObject = GetOrCreateInstanceGo<T>();
                        instance = instanceGameObject.AddComponent<T>();

                        Debug.Log($"Added new singleton {typeof(T).Name} to the game!");

                    }
                    else
                    {
                        Debug.Log($"{instance.name} already exists!");
                    }
                }
                return instance;
            }
        }


        void OnDestroy()
        {
            instance = null;
        }

        protected static GameObject GetOrCreateInstanceGo<T>() {
            string instanceName = typeof(T).Name;
            GameObject instanceGameObject = GameObject.Find(instanceName);

            if (!instanceGameObject)
            {
                instanceGameObject = new GameObject(instanceName);
            }

            return instanceGameObject;
        }

    }

}
