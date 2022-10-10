using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorExtensionsLearning {
    public class ContextMenuExample : MonoBehaviour {
        [ContextMenu("Hello ContextMenu")]
        void HelloContextMenu()
        {
            Debug.Log("Hello ContextMenu");
        }

        [ContextMenuItem("Print Value", "HelloContextMenuItem")][SerializeField]
        private string contextMenuItemValue;
        void HelloContextMenuItem() {
            Debug.Log(contextMenuItemValue);
        }

#if UNITY_EDITOR
        /*
        private const string findScriptPath = "CONTEXT/MonoBehaviour/FindScript";
        [UnityEditor.MenuItem(findScriptPath)]
        static void FindScript(UnityEditor.MenuCommand command) {
            UnityEditor.Selection.activeObject =
                UnityEditor.MonoScript.FromMonoBehaviour(command.context as MonoBehaviour);

        }*/

        private const string cameraScriptPath = "CONTEXT/Camera/LogSelf";
        [UnityEditor.MenuItem(cameraScriptPath)]
        static void FindScriptCamera(UnityEditor.MenuCommand command)
        {
            UnityEditor.Selection.activeObject =
                UnityEditor.MonoScript.FromMonoBehaviour(command.context as MonoBehaviour);

        }
#endif
    }

}
