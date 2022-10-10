using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework
{
    public class PanelCloseButton : MonoBehaviour {
        private Button button;
        [SerializeField] private bool alsoCloseChild = true;

        private void Awake() {
            button = GetComponent<Button>();
            if (!button) {
                Debug.LogError("PanelCloseButton: Button not found");
                return;
            }

            button.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked() {
            IPanel parentPanel = GetComponentInParent<IPanel>();
            if (parentPanel != null) {
                UIManager.Singleton.ClosePanel(parentPanel, alsoCloseChild);
            }
        }
    }
}
