#if  UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MikroFramework.EditorFramework
{
    public class RootWindow: EditorWindow
    {
        [MenuItem("MikroFramework/Framework/EditorFramework/Open &f")]
        public static void Open() {
            GetWindow<RootWindow>().Show();
        }

        private IEnumerable<Type> editorWindowTypes;
        private void OnEnable() {
            Type editorWindowType = typeof(EditorWindow);
            FieldInfo parent = editorWindowType.GetField("m_Parent", BindingFlags.Instance | BindingFlags.NonPublic);
            editorWindowTypes = editorWindowType.GetSubTypesInAssemblies<CustomEditorWindow>().OrderBy(t=> t.GetCustomAttribute<CustomEditorWindow>().RenderOrder);
        }            

        private void OnGUI() {
            
            foreach (Type windowType in editorWindowTypes) {
                GUILayout.BeginHorizontal("box");
                {
                    GUILayout.Label(windowType.Name);
                    if (GUILayout.Button("Open", GUILayout.Width(80))) {
                        GetWindow(windowType).Show();
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}


#endif
