using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.ActionKit;
using UnityEngine;

namespace MikroFramework
{
    public class AutoDeleteActionsExample : MonoBehaviour {
        private void Awake() {
            this.Sequence().Event(()=>{Debug.Log("This example will show how to prevent errors when trying to accessing a destroyed object via MikroAction");}). 
                Delay(2f).Event((() => { Destroy(gameObject); })).Delay(2f).Event((() => {
                Debug.Log($"Accessing transform.position will cause an error: {transform.position}");
            })).StopActionWhenGameObjectDestroyed(gameObject).Execute();
        }
    }
}
