
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.EditorFramework
{
    public class CustomEditorWindow : Attribute {
        public int RenderOrder { get; private set; }

        public CustomEditorWindow(int order = -1) {
            RenderOrder = order;
        }
    }
}


#endif
