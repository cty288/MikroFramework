using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MikroFramework.Singletons
{
    /// <summary>
    /// Declare a singleton property for a class if the class can't inherit MikroSingleton 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonProperty<T> where T: class, ISingleton {
        private static T instance;

        public static T Singleton {
            get {
                if (instance == null) {
                    if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T))) { //is MonoBehavior
                        instance = Object.FindObjectOfType(typeof(T)) as T;

                        if (instance!=null) { 
                            Debug.LogWarning($"More than one singleton {typeof(T).Name} exists in the current scene!");
                            return instance;
                        }

                        if (instance == null) {
                            GameObject instanceGameObject = GetOrCreateInstanceGo<T>();
                            instance = instanceGameObject.AddComponent(typeof(T)) as T;
                            Object.DontDestroyOnLoad(instanceGameObject);
                            Debug.Log($"Added new singleton {typeof(T).Name} to the game!");

                        }else {
                            Debug.Log($"{instance.GetType()} already exists!");
                        }

                    }else {
                        var creators = typeof(T).GetConstructors(BindingFlags.Instance |
                                                                 BindingFlags.NonPublic);
                        var creator = Array.Find(creators, c => c.GetParameters().Length == 0);

                        if (creator == null)
                        {
                            throw new Exception("Non-public ctor() not found!");
                        }

                        instance = creator.Invoke(null) as T;
                    }
                }

                return instance;
            }

        }

        private static GameObject GetOrCreateInstanceGo<T>()
        {
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
