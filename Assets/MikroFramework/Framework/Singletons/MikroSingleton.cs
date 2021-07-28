 using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MikroFramework.Singletons {
    /// <summary>
    /// Inherit MikroSingleton for singletons that do not need to inherit MonoBehavior;
    /// Use MonoMikroSingleton for singletons to inherit MonoBehavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MikroSingleton<T>: ISingleton where T: MikroSingleton<T> {
        protected static T instance;
        public static T Singleton {
            get {
                if (instance == null) {
                    var creators = typeof(T).GetConstructors(BindingFlags.Instance |
                                                             BindingFlags.NonPublic);
                    var creator = Array.Find(creators, c => c.GetParameters().Length == 0);

                    if (creator == null) {
                        throw new Exception("Non-public ctor() not found!");
                    }

                    instance = creator.Invoke(null) as T;
                    
                }
                return instance;
            }
        }
    }

}
