using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Managers;
using UnityEngine;

namespace MikroFramework.Examples {


    public class GUIManagerExample : MikroBehavior
    {
        protected override void OnBeforeDestroy()
        {
        }

        void Start() {
            GameObject panel= GUIManager.LoadPanel("HomePanel",UILayer.Common);
            Delay(3f,()=>{GUIManager.UnLoadPanel("HomePanel");});
        }

    }
}

