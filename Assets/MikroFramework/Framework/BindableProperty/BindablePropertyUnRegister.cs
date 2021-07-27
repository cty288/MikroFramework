using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Event;

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
    }
}
