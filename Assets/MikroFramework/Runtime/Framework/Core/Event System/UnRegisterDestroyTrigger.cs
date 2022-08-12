using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MikroFramework.Event
{
    public class UnRegisterDestroyTrigger : MonoBehaviour
    {
       // private HashSet<IUnRegister> unRegisters = new HashSet<IUnRegister>();
        private Dictionary<IUnRegister, bool> unRegisters = new Dictionary<IUnRegister, bool>();
        public void AddUnRegister(IUnRegister unRegister, bool alsoUnregisterWhenDisabled) {
            if (unRegisters.ContainsKey(unRegister)) {
                return;
            }
            unRegisters.Add(unRegister, alsoUnregisterWhenDisabled);
        }

        private void OnDestroy() {
            foreach (IUnRegister unRegister in unRegisters.Keys) {
                unRegister.UnRegister();
            }
            unRegisters.Clear();
        }
        
        private void OnDisable() {
            HashSet<IUnRegister> unregistered = new HashSet<IUnRegister>();
            foreach (IUnRegister unRegister in unRegisters.Keys) {
                if (unRegisters[unRegister]) {
                    unRegister.UnRegister();
                    unregistered.Add(unRegister);
                }
            }

            foreach (IUnRegister unRegister in unregistered) {
                unRegisters.Remove(unRegister);
            }
        }
    }


    public static class UnRegisterExtension
    {
        /// <summary>
        /// Unregister this listener when a specific gameObject is destroyed
        /// </summary>
        /// <param name="unRegister"></param>
        /// <param name="gameObject"></param>
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject, bool alsoUnRegisterWhenDisabled = false)
        {
            UnRegisterDestroyTrigger trigger = gameObject.GetComponent<UnRegisterDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterDestroyTrigger>();
            }

            trigger.AddUnRegister(unRegister, alsoUnRegisterWhenDisabled);
        }
    }

}
