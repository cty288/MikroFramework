using System.Collections;
using System.Collections.Generic;
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
                UIRoot.Singleton.ClosePanel(this);
            });

            settingsButton.onClick.AddListener(() => {
                UIRoot.Singleton.Open<SettingsPanel>(this, null);
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
