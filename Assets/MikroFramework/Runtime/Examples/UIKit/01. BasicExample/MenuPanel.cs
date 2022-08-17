using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public class MenuPanel : AbstractPanelContainer {
        [SerializeField] private Button loadButton;
        [SerializeField] private Button aboutButton;
        public override void OnInit() {
            Debug.Log("Menu Panel Init");
            loadButton.onClick.AddListener(() => {
                UIRoot.Singleton.Open<LoadPanel>(this, null);
            });

            aboutButton.onClick.AddListener(() => {
                UIRoot.Singleton.Open<AboutPanel>(null, null);
            });
        }

        public override void OnOpen(UIMsg msg) {
            Debug.Log("Menu Panel Open");
        }

        public override void OnClosed() {
            Debug.Log("Menu Panel Closed");
        }
    }
}
