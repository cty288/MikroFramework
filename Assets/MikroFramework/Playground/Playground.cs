using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.ActionKit;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class Playground : MonoBehaviour
    {
        private void Start() {
            Timeline timeline = Timeline.Allocate();
            timeline.AddAction(2, CallbackAction.Allocate(() => {
                Debug.Log("waae");
            }));

            this.Repeat(5).AddAction(timeline).Execute();
        }
    }
}
