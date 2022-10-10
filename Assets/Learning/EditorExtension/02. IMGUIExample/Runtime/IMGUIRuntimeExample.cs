using System;
using System.Collections;
using System.Collections.Generic;
using EditorExtensionsLearning;
using UnityEngine;

public class IMGUIRuntimeExample : MonoBehaviour {
    private GUILayoutView layoutAPI = new GUILayoutView();
    private int index = 0;
    private GUIAPI guiAPI = new GUIAPI();
    private void OnGUI() {
        index = GUILayout.Toolbar(index, new string[] { "GUILayout", "GUI" });
        if (index == 0) {
            layoutAPI.Draw();
        }
        else {
            guiAPI.Draw();
        }
       
    }
}
