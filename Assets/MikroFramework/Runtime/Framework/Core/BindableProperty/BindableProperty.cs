using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using MikroFramework.Managers;
using MikroFramework.Pool;
using UnityEngine;

namespace MikroFramework.BindableProperty
{
    [Serializable]
    public class BindableProperty<T>
    {
        public BindableProperty(T defaultValue = default) {
            this.value = defaultValue;
        }

        [SerializeField]
        private T value = default(T);

        public T Value
        {
            get => value;
            set
            {
                if (value == null && this.value == null) {
                    return;
                }

                if (value != null && value.Equals(this.value)) {
                    return;
                }

                this.onValueChanged2?.Invoke(this.value, value);
                this.value = value;
                this.onValueChanged?.Invoke(value);
            }
        }
        

        private Action<T> onValueChanged = (v) => { };
        private Action<T, T> onValueChanged2 = (v, w) => { };

        /// <summary>
        /// RegisterInstance listeners to the event that triggered when the value of the property changes.
        /// </summary>
        /// <param name="onValueChanged"></param>
        /// <returns>The returned IUnRegister allows you to call its UnRegisterWhenGameObjectDestroyed()
        /// function to unregister the event more convenient instead of calling UnRegisterOnValueChanged function</returns>
        [Obsolete("Use the one with two values (new and old) instead")]
        public IUnRegister RegisterOnValueChaned(Action<T> onValueChanged)
        {
            this.onValueChanged += onValueChanged;

            return new BindablePropertyUnRegister<T>(this, onValueChanged);

        }
        [Obsolete("Use the one with two values (new and old) instead")]
        public IUnRegister RegisterWithInitValue(Action<T> onValueChanged) {
            onValueChanged?.Invoke(value);
            return RegisterOnValueChaned(onValueChanged);
        }


        public IUnRegister RegisterWithInitValue(Action<T,T> onValueChanged)
        {
            onValueChanged?.Invoke(default, value);
            return RegisterOnValueChaned(onValueChanged);
        }


        //a == b
        public static implicit operator T(BindableProperty<T> property) {
            return property.value;
        }

        public override string ToString() {
            return value.ToString();
        }

        /// <summary>
        /// RegisterInstance listeners to the event that triggered when the value of the property changes.
        /// </summary>
        /// <param name="onValueChanged">old and new values</param>
        /// <returns>The returned IUnRegister allows you to call its UnRegisterWhenGameObjectDestroyed()
        /// function to unregister the event more convenient instead of calling UnRegisterOnValueChanged function</returns>
        public IUnRegister RegisterOnValueChaned(Action<T, T> onValueChanged)
        {
            this.onValueChanged2 += onValueChanged;

            return new BindablePropertyUnRegister2<T>(this, onValueChanged2);

        }

        /// <summary>
        /// Unregister listeners to the event that triggered when the value of the property changes
        /// </summary>
        /// <param name="onValueChanged"></param>
        [Obsolete("Use the one with two parameters (new and old) instead")]
        public void UnRegisterOnValueChanged(Action<T> onValueChanged)
        {
            this.onValueChanged -= onValueChanged;
        }

        public void UnRegisterOnValueChanged(Action<T, T> onValueChanged)
        {
            this.onValueChanged2 -= onValueChanged;
        }
    }
}
