#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.EditorFramework
{
    public abstract class GUIBase : IDisposable {
        public bool Disposed { get; set; }
        public Rect Position { get; private set; }
        public virtual void OnGUI(Rect position)
        {
            Position = position;
        }

        public virtual void Dispose()
        {
            if (Disposed)
            {
                return;
            }
            OnDisposed();
            Disposed = true;
        }

        protected abstract void OnDisposed();
    }

}

#endif
