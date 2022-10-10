using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public struct OpenFilePanelMsg: UIMsg {
        public int FileCount;
    }
    public class LoadPanel : AbstractPanelContainer {
        [SerializeField] private Button openFilesButton;
        [SerializeField] private Button backButton;

        [SerializeField] private bool alsoCloseChild = false;
        public override void OnInit() {
            //pass msg
            openFilesButton.onClick.AddListener(() => {
                UIManager.Singleton.Open<OpenFilesPanel>(this, new OpenFilePanelMsg() {FileCount = Random.Range(1, 100)},
                    true, "OpenFilesSubPanel");
            });

            //not close child, this will instantiate a new open file panel next time when we press openFilesButton
            /*
            backButton.onClick.AddListener(() => {
                UIManager.Singleton.ClosePanel(this, alsoCloseChild);
            });*/
        }

        public override void OnOpen(UIMsg msg) {
            Debug.Log("LoadPanel.OnOpen");
        }

        public override void OnClosed() {
            Debug.Log("LoadPanel.OnClosed");
        }
    }
}
