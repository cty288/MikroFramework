using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikroFramework.EditorFramework {
    public static class DragAndDropTool{

        public class DragInfo {
            public bool Dragging;
            public bool EnterArea;
            public bool Complete;

            public Object[] ObjectReferences => DragAndDrop.objectReferences;
            public string[] Paths => DragAndDrop.paths;
            public DragAndDropVisualMode VisualMode => DragAndDrop.visualMode;
            public int ActiveControlID => DragAndDrop.activeControlID;
        }

        private static DragInfo dragInfo = new DragInfo();

        private static bool enterArea;
        private static bool complete;
        private static bool dragging;
        
        public static DragInfo Drag(UnityEngine.Event e, Rect content, DragAndDropVisualMode mode = DragAndDropVisualMode.Generic ) {
            if (e.type == EventType.DragUpdated)
            {
                enterArea = content.Contains(e.mousePosition);
                dragging = true;
                complete = false;
                if (enterArea)
                {
                    DragAndDrop.visualMode = mode;
                    UnityEngine.Event.current.Use();
                }
            }
            else if (e.type == EventType.DragPerform)
            {
                enterArea = content.Contains(e.mousePosition);
                dragging = false;
                complete = true;
                DragAndDrop.AcceptDrag();
                e.Use();
            }
            else if (e.type == EventType.DragExited)
            {
                enterArea = content.Contains(e.mousePosition);
                dragging = false;
                complete = true;
            }
            else
            {
                enterArea = content.Contains(e.mousePosition);
                dragging = false;
                complete = false;
            }

            dragInfo.Complete = complete && e.type == EventType.Used;
            dragInfo.EnterArea = enterArea;
            dragInfo.Dragging = dragging;
            
           

            return dragInfo;
        }

    }

}
