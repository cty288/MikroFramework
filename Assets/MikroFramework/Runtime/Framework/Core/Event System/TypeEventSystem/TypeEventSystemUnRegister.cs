using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Pool;

namespace MikroFramework.Event
{
    public class TypeEventSystemUnRegister<T> : IUnRegister
    {
        public ITypeEventSystem TypeEventSystem;
        public Action<T> OnEvent;
        public void UnRegister()
        {
            TypeEventSystem.UnRegister<T>(OnEvent);
            TypeEventSystem = null;
            OnEvent = null;
        }

        public void OnRecycled() {
            
        }

        public bool IsRecycled { get; set; }
        public void RecycleToCache()
        {
            SafeObjectPool<TypeEventSystemUnRegister<T>>.Singleton.Recycle(this);
        }

        public static TypeEventSystemUnRegister<T> Allocate( ITypeEventSystem typeEventSystem, Action<T> onEvent)
        {
            TypeEventSystemUnRegister<T> unRegister = SafeObjectPool<TypeEventSystemUnRegister<T>>.Singleton.Allocate();
            unRegister.TypeEventSystem = typeEventSystem;
            unRegister.OnEvent = onEvent;
            return unRegister;
        }
    }

}
