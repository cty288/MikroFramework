using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.IOC
{
    public class IOCContainer {
        private Dictionary<Type, object> instances = new Dictionary<Type, object>();

        /// <summary>
        /// Register a singleton to the IOC Container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void Register<T>(T instance) {
            Type key = typeof(T);

            if (!instances.ContainsKey(key)) {
                instances.Add(key,instance);
            }
            else {
                instances[key] = instance;
            }
        }

        /// <summary>
        /// Get an instance from the IOC Container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class {
            Type key = typeof(T);

            object retInstance;

            if (instances.TryGetValue(key,out retInstance)) {
                return retInstance as T;
            }

            return null;
        }
    }
}
