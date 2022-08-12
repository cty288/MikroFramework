using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.ActionKit
{
    class ActionPlayerUnregister : IUnRegister {
    
        public Action OnExecuting { get; set; }
        public MikroAction action;
        public ActionPlayerUnregister(MikroAction action, Action OnExecuting) {
            this.OnExecuting = OnExecuting;
            this.action = action;
        }

        public void UnRegister()
        {
            ActionPlayer.Singleton.UnRegisterUpdate(OnExecuting);
            action.Finished.Value = true;
        }

    }
    
    public class ActionPlayer:MikroSingleton<ActionPlayer> {
       private ActionPlayer() { }

        public IUnRegister RegisterUpdate(MikroAction action, Action onUpdate) {
            UpdateActionExecutor.ActionKitUpdateTrigger.Singleton.OnUpdate += onUpdate;
            return new ActionPlayerUnregister(action, onUpdate);
        }

        public void UnRegisterUpdate(Action onUpdate) {
            UpdateActionExecutor.ActionKitUpdateTrigger.Singleton.OnUpdate -= onUpdate;
        }


        public void Play(MikroAction action)
        {
            action.Execute();
        }

    }
}
