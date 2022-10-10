using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorExtensionsLearning {
    public class GUIContentAndGUIStyleExample : EditorWindow {

        [MenuItem("EditorExtensions/02. IMGUI/02. GUIContentAndGUIStyle")]
        static void Show() {
            ((EditorWindow) GetWindow<GUIContentAndGUIStyleExample>()).Show();
        }
        enum Mode {
            GUIContent,
            GUIStyle,
        }

        private int toolbarIndex;

        private Lazy<GUIStyle> fontStyle = new Lazy<GUIStyle>(() => {
            var style = new GUIStyle("label");
            style.fontSize = 30;
            style.fontStyle = FontStyle.BoldAndItalic;
            style.normal.textColor = Color.red;
            style.hover.textColor = Color.green;
            style.active.textColor = Color.blue;
            style.focused.textColor = Color.yellow;
            style.active.textColor = Color.magenta;
            style.normal.background = Texture2D.whiteTexture;
            return style;
        });

        private Lazy<GUIStyle> buttonStyle = new Lazy<GUIStyle>(() => {
            var style =new GUIStyle(GUI.skin.button);
            style.fontSize = 30;
            style.fontStyle = FontStyle.BoldAndItalic;
            style.normal.textColor = Color.red;
            style.hover.textColor = Color.green;
            style.active.textColor = Color.blue;
            style.focused.textColor = Color.yellow;
            style.active.textColor = Color.magenta;
            style.normal.background = Texture2D.whiteTexture;
            return style;
        });
        private void OnEnable() {
         
        }

        private void OnGUI() {
            toolbarIndex = GUILayout.Toolbar(toolbarIndex, Enum.GetNames(typeof(Mode)));
            if (toolbarIndex == (int)Mode.GUIContent) {
                GUILayout.Label("Put mouse on me");
                GUILayout.Label(new GUIContent("Put mouse on me", "This is tooltip"));
                GUILayout.Label(new GUIContent("Put mouse on me", Texture2D.redTexture));
                GUILayout.Label(new GUIContent("Put mouse on me", Texture2D.whiteTexture, "This is tooltip"));

            }
            else {
                GUIStyle boxStyle = "box";
                
              

                GUILayout.Label("Style of box default");
                GUILayout.Label("Style of box", boxStyle);

                GUILayout.Label("Style of font", fontStyle.Value);

                if (GUILayout.Button("Style of button", buttonStyle.Value)) {
                    Debug.Log("button");
                }
            }
        }
    }
}

