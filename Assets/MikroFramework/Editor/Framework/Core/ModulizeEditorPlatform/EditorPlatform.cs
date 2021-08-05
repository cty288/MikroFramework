using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MikroFramework.ServiceLocator;
using UnityEditor;
using UnityEngine;

namespace MikroFramework.EditorModulization
{
    public class EditorModulizationPlatformEditor : EditorWindow {
        private ModuleContainer moduleContainer = null;

        [MenuItem("MikroFramework/Framework/EditorModulizationPlatform")]
        public static void Open() {
            EditorModulizationPlatformEditor editorPlatform = GetWindow<EditorModulizationPlatformEditor>();

            editorPlatform.position = new Rect(Screen.width / 2, Screen.height /2, 600, 500);

            DefaultModuleCache cache = new DefaultModuleCache();
            AssemblyModuleFactory factory = new AssemblyModuleFactory(typeof(IEditorPlatformModule).Assembly,
                typeof(IEditorPlatformModule));

            editorPlatform.moduleContainer = new ModuleContainer(cache, factory);
            

            editorPlatform.Show();
            
        }

        private void OnGUI() {
            var modules = moduleContainer.GetAllModules<IEditorPlatformModule>();

            foreach (IEditorPlatformModule editorPlatformModule in modules) {
                editorPlatformModule.OnGUI();
            }
        }
    }
}
