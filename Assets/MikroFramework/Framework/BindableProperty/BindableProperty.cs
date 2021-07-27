using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using MikroFramework.Managers;
using UnityEngine;

namespace MikroFramework.BindableProperty
{
    [Serializable]
    public class BindableProperty<T> where T:IEquatable<T> {
        
        private T value = default(T);

        public T Value {
            get => value;
            set {
                if (this.value != null) {
                    if (!this.value.Equals(value)) {
                        this.value = value;
                        this.onValueChanged?.Invoke(value);
                    }
                }
                else {
                    this.value = value;
                    this.onValueChanged?.Invoke(value);
                }
                
            }
        }
        
        private Action<T> onValueChanged = (v) => { };

        /// <summary>
        /// Register listeners to the event that triggered when the value of the property changes.
        /// </summary>
        /// <param name="onValueChanged"></param>
        /// <returns>The returned IUnRegister allows you to call its UnRegisterWhenGameObjectDestroyed()
        /// function to unregister the event more convenient instead of calling UnRegisterOnValueChanged function</returns>
        public IUnRegister RegisterOnValueChaned(Action<T> onValueChanged) {
            this.onValueChanged += onValueChanged;

            return new BindablePropertyUnRegister<T>() {
                Bindable = this,
                OnValueChanged = onValueChanged
            };
        }


        /// <summary>
        /// Unregister listeners to the event that triggered when the value of the property changes
        /// </summary>
        /// <param name="onValueChanged"></param>
        public void UnRegisterOnValueChanged(Action<T> onValueChanged) {
            this.onValueChanged -= onValueChanged;
        }
    }
}
