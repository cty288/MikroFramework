using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorExtensionsLearning {
    public class IMGUIEditorWindowExample : EditorWindow {
        private GUILayoutView layoutAPI = new GUILayoutView();
        private GUIAPI guiAPI = new GUIAPI();

        private APIMode apiMode = APIMode.GUILayout;

        private EditorGUIAPI editorGUIAPI = new EditorGUIAPI();
        private EditorGUILayoutAPI editorGuiLayoutApi = new EditorGUILayoutAPI();
        enum APIMode {
            GUILayout,
            GUI,
            EditorGUI,
            EditorGUILayout
        }
        enum PageID {
            Basic,
            GUIEnabled,
            Rotate,
            Scale,
            Color,
            Other,
        }
       [MenuItem("EditorExtensions/02. IMGUI/01. GUILayoutExample")]
        public static void OpenGUILayoutExample() {
            GetWindow<IMGUIEditorWindowExample>().Show();
        }

        #region Basic

        void Basic() {
            if (apiMode == APIMode.GUILayout) {
                layoutAPI.Draw();
            }
            else if (apiMode == APIMode.GUI) {
                guiAPI.Draw();
            }
            else if (apiMode == APIMode.EditorGUI) {
                editorGUIAPI.Draw();
            }
            else {
                editorGuiLayoutApi.Draw();
            }
        }

        #endregion

        private PageID pageID = PageID.Basic;
        //ALL GUILayout API
        private void OnGUI() {
            apiMode = (APIMode) EditorGUILayout.EnumPopup("API Mode", apiMode);
            
            pageID = (PageID) GUILayout.Toolbar((int) pageID, Enum.GetNames(typeof(PageID)));
            switch (pageID) {
                case PageID.Basic:
                    Basic();
                    break;
                case PageID.GUIEnabled:
                    GUIEnabled();
                    break;
                case PageID.Rotate:
                    Rotate();
                    break;
                case PageID.Other:
                    Other();
                    break;
                case PageID.Scale:
                    Scale();
                    break;
                case PageID.Color:
                    Color();
                    break;
                default:
                    break;
            }
        }

        #region Color

        private bool openColorEffect = false;
        private void Color() {
            openColorEffect = GUILayout.Toggle(openColorEffect, "Open Color Effect");
            if (openColorEffect) {
                GUI.color = UnityEngine.Color.blue;
            }

            Basic();
        }

        #endregion

        #region Scale

        private bool openScale = false;
        private void Scale() {
            openScale = GUILayout.Toggle(openScale, "Open Scale Effect");
            if (openScale) {
                GUIUtility.ScaleAroundPivot(Vector2.one * 0.5f, new Vector2(200, 200));
            }
            Basic();
        }
        #endregion

        #region Rotate

        private bool openRotateEffect = false;
        private void Rotate() {
            openRotateEffect = GUILayout.Toggle(openRotateEffect, "Open Rotate Effect");
            if (openRotateEffect) {
                GUIUtility.RotateAroundPivot(45, Vector2.one * 200);
            }
            Basic();
        }

        #endregion
        
        #region GUIEnabled

        private bool enableInteractive = true;
        private void GUIEnabled() {
            enableInteractive = GUILayout.Toggle(enableInteractive, "Enable Interactive");
            if (GUI.enabled != enableInteractive) {
                GUI.enabled = enableInteractive;
            }
            Basic();


            GUI.enabled = true;
            GUILayout.Button( "Enabled Button");


            
        }

        #endregion



        private void Other() {
            
        }
    }

}
