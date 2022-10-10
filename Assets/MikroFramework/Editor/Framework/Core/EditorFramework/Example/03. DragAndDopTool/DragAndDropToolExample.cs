using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.EditorFramework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MikroFramework.Examples {
    [CustomEditorWindow(4)]
    public class DragAndDropToolExample : EditorWindow {
        private void OnGUI() {
            Rect rect = new Rect(10, 10, 300, 150);
            GUI.Box(rect, "Drag and drop here:");
            var e = UnityEngine.Event.current;
            DragAndDropTool.DragInfo info = DragAndDropTool.Drag(e, rect);

            if (info.EnterArea && info.Complete && !info.Dragging)
            {
                foreach (string path in info.Paths)
                {
                    Debug.Log(path);
                }
                foreach (Object reference in info.ObjectReferences)
                {
                    Debug.Log(reference);
                }
            }
        }
    }
}
