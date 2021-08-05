using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Event;
using MikroFramework.Pool;

namespace MikroFramework.BindableProperty
{
    class BindablePropertyUnRegister<T>:IUnRegister where T:IEquatable<T>
    {
        public BindableProperty<T> Bindable { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister() {
            Bindable.UnRegisterOnValueChanged(OnValueChanged);
            Bindable = null;
        }

        public void OnRecycled() {
          
        }

        public bool IsRecycled { get; set; }
        public void RecycleToCache()
        {
            SafeObjectPool<BindablePropertyUnRegister<T>>.Singleton.Recycle(this);
        }

        public static BindablePropertyUnRegister<T> Allocate(BindableProperty<T> bindable, Action<T> onValueChanged)
        {
            BindablePropertyUnRegister<T> unRegister = SafeObjectPool<BindablePropertyUnRegister<T>>.Singleton.Allocate();
            unRegister.Bindable = bindable;
            unRegister.OnValueChanged = onValueChanged;
            return unRegister;
        }
    }
}
