using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public class SettingsPanel : AbstractPanelContainer
    {
        [SerializeField] private Button exitButton;
        public override void OnInit() {
            Debug.Log("Settings Panel Init");
            exitButton.onClick.AddListener(() => {
                UIManager.Singleton.ClosePanel(this);
            });
        }

        public override void OnOpen(UIMsg msg)
        {
            Debug.Log("SettingsPanel Open");
        }

        public override void OnClosed()
        {
            Debug.Log("SettingsPanel Closed");
        }
    }
}
