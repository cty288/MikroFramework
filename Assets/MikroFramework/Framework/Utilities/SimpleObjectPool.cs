using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Utilities {
    public interface IPool<T> {
        /// <summary>
        /// Allocate an object from the ObjectPool
        /// </summary>
        /// <returns>allocated object</returns>
        T Allocate();

        /// <summary>
        /// Recycle the object back to its pool.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>If recycle success</returns>
        bool Recycle(T obj);
    }

    public interface IObjectFactory<T> {
        /// <summary>
        /// Create an object from the Object's Factory
        /// </summary>
        /// <returns>created object</returns>
        T Create();
    }

    public abstract class Pool<T>: IPool<T> {
        protected Stack<T> cachedStack = new Stack<T>();
        protected IObjectFactory<T> factory;

        public int CurrentCount {
            get { return cachedStack.Count; }
        }
        public virtual T Allocate() {
            return cachedStack.Count > 0 ? cachedStack.Pop() : factory.Create();
        }

        public abstract bool Recycle(T obj);
    }

    public class CustomObjectFactory<T> : IObjectFactory<T> {
        private Func<T> factoryCreateMethod;
        /// <summary>
        /// The method that can create or get the Object of type T
        /// </summary>
        /// <param name="factoryMethod"></param>
        public CustomObjectFactory(Func<T> factoryMethod) {
            factoryCreateMethod = factoryMethod;
        }
        public T Create() {
            return factoryCreateMethod();
        }
    }

    public class SimpleObjectPool<T> : Pool<T> {
        private Action<T> resetMethod;

        public SimpleObjectPool(Func<T> createMethod, Action<T> resetMethod = null, int initialCount = 0) {
            factory = new CustomObjectFactory<T>(createMethod);
            this.resetMethod = resetMethod;
            for (int i = 0; i < initialCount; i++) {
                cachedStack.Push(factory.Create());
            }
        }

        public override bool Recycle(T obj) {
            if (resetMethod != null) {
                resetMethod(obj);
            }

            cachedStack.Push(obj);
            return true;
        }

    }

}
