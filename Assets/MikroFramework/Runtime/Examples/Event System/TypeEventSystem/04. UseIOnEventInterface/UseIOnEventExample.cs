using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using UnityEngine;

namespace MikroFramework.Examples
{
    public class UseIOnEventExample : MonoBehaviour, IOnEvent<OnEventA>, IOnEvent<OnEventB>
    {
        private void Start()
        {
            this.RegisterEvent<OnEventA>();
            this.RegisterEvent<OnEventB>();
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<OnEventA>();
            this.UnRegisterEvent<OnEventB>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TypeEventSystem.SendGlobalEvent<OnEventA>();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                TypeEventSystem.SendGlobalEvent<OnEventB>();
            }
        }

        public void OnEvent(OnEventA e)
        {
            Debug.Log("Onevent A");
        }

        public void OnEvent(OnEventB e)
        {
            Debug.Log("Onevent B");
        }
    }

    public struct OnEventA
    {

    }

    public struct OnEventB
    {

    }
}