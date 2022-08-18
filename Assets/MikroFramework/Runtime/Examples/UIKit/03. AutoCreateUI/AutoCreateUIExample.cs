using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;

namespace MikroFramework
{
    public class AutoCreateUIExample : MonoBehaviour
    {
        private void Awake() {
            UIManager.SetResolution(new DefaultUISettings(new Vector2(1080, 720), 0));
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                UIManager.Singleton.Open<AboutPanel>(null, null);
            }
        }
    }
}
