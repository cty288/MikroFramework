using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.EditorModulization {
    public class ModulePlatformReadme : IEditorPlatformModule {
        public void OnGUI() {
            GUILayout.Label("Editor Modulization - Instructions", new GUIStyle() {
                fontSize = 20,
                fontStyle = FontStyle.Bold
            });

            GUILayout.Label("1. Create a new class in Editor folder");
            GUILayout.Label("2. Implement IEditorPlatformModule interface like this: ");
            GUILayout.Label("public class ModulePlatformReadme : IEditorPlatformModule{");
            GUILayout.Label("    public void OnGUI() {");
            GUILayout.Label("            //Your code here");
            GUILayout.Label("    }");
            GUILayout.Label("}");

        }
    }

}
