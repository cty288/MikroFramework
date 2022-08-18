using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.UIKit
{
    public abstract class AbstractPanelContainer : AbstractPanel, IPanelContainer{
        public IPanelContainer Parent { get; set; }
        public List<IPanel> Children { get; } = new List<IPanel>();
        public IPanel GetTopChild() {
            if (Children.Count == 0) {
                return null;
            }
            return Children[Children.Count - 1];
        }
    }
}
