using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public class OpenFilesPanel : AbstractPanelContainer
    {
        [SerializeField] private Button backButton;

        public override void OnInit() {
            //not close child, this will instantiate a new open file panel next time when we press openFilesButton
            /*
            backButton.onClick.AddListener(() => {
                UIManager.Singleton.ClosePanel(this, false);
            });*/
        }

        public override void OnOpen(UIMsg msg) {
            Debug.Log("OpenFilesPanel.OnOpen");
            OpenFilePanelMsg uiMsg = (OpenFilePanelMsg) msg;
            Debug.Log($"Open Files Panel File Count: {uiMsg.FileCount}");
        }

        public override void OnClosed() {
            Debug.Log("OpenFilesPanel.OnClosed");
        }
    }
}
