using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public class MenuPanel : AbstractPanelContainer {
        [SerializeField] private Button loadButton;
        [SerializeField] private Button aboutButton;
        public override void OnInit() {
            Debug.Log("Menu Panel Init");
            if (loadButton) {
                loadButton.onClick.AddListener(() => {
                    UIManager.Singleton.Open<LoadPanel>(this, null);
                    
                });
            }
            

            if (aboutButton) {
                aboutButton.onClick.AddListener(() => {
                    //load from asset bundle
                    UIManager.Singleton.Open<AboutPanel>(null, null, true);
                });
            }
           
        }

        public override void OnOpen(UIMsg msg) {
            Debug.Log("Menu Panel Open");
        }

        public override void OnClosed() {
            Debug.Log("Menu Panel Closed");
        }
    }
}
