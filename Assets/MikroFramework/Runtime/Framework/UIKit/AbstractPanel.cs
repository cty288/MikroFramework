using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.UIKit;
using UnityEngine;

namespace MikroFramework.UIKit{
    public abstract class AbstractPanel : MonoBehaviour, IPanel {
    
        public bool IsOnTop() {
            if (Parent != null) {
                return Parent.GetTopChild() == this && Parent.IsOnTop();
            }
            return true;
        }

        public IPanelContainer Parent { get; set; }
        
        [field: SerializeField]
        public PanelType PanelType { get; set; }

        [field: SerializeField]
        public GameObject DefaultSelectedGameObjectWhenFocused { get; set; }

        public bool IsOpening { get; set; }

        public virtual void OnFocused() {
            
        }

        public abstract void OnInit();
        public abstract void OnOpen(UIMsg msg);
        public abstract void OnClosed();
    }
}
