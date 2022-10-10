using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Utilities{
    public class FindScriptContextMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        private const string findScriptPath = "CONTEXT/MonoBehaviour/FindScript";
        [UnityEditor.MenuItem(findScriptPath)]
        static void FindScript(UnityEditor.MenuCommand command)
        {
            UnityEditor.Selection.activeObject =
                UnityEditor.MonoScript.FromMonoBehaviour(command.context as MonoBehaviour);

        }
    }

}
