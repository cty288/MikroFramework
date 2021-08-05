using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Pool;

namespace MikroFramework.Event { 

    public class EnumEventSystemUnRegister : IUnRegister {
        public Action<MikroMessage> Callback;
        public EventType EventType;
        public void UnRegister() {

            EventCenter.RemoveListener(EventType,Callback);
            Callback = null;
            RecycleToCache();
        }

        public void OnRecycled() {
            
        }

        public bool IsRecycled { get; set; }
        public void RecycleToCache() {
            SafeObjectPool<EnumEventSystemUnRegister>.Singleton.Recycle(this);
        }

        public static EnumEventSystemUnRegister Allocate(EventType eventType, Action<MikroMessage> callback) {
            EnumEventSystemUnRegister unRegister = SafeObjectPool<EnumEventSystemUnRegister>.Singleton.Allocate();
            unRegister.EventType = eventType;
            unRegister.Callback = callback;
            return unRegister;
        }

    }
}
