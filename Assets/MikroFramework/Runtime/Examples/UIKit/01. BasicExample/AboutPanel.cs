using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public class AboutPanel : AbstractPanelContainer
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingsButton;
        public override void OnInit()
        {
            Debug.Log("About Panel Init");
            exitButton.onClick.AddListener(() => {
                UIManager.Singleton.ClosePanel(this);
            });

            settingsButton.onClick.AddListener(() => {
                UIManager.Singleton.Open<SettingsPanel>(this, null);
            });
        }

        public override void OnOpen(UIMsg msg)
        {
            Debug.Log("About Panel Open");
        }

        public override void OnClosed()
        {
            Debug.Log("About Panel Closed");
        }
    }
}
