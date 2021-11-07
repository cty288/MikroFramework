using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class Broadcaster : MonoBehaviour
    {
        private void Start() {
            StartCoroutine(SendEventAfter5Sec());
        }

        private IEnumerator SendEventAfter5Sec() {
            yield return new WaitForSeconds(5f);

            TypeEventSystem.SendGlobalEvent(new SendInfo("Broadcaster Name"));
        }
    }
}
