using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameStart : MonoBehaviour {
    private readonly Lazy<GUIStyle> labelStyle = new Lazy<GUIStyle>(() =>
        new GUIStyle(GUI.skin.label) {
            fontSize = 80,
            alignment = TextAnchor.MiddleCenter
        });

    private readonly Lazy<GUIStyle> buttonStyle = new Lazy<GUIStyle>(() =>
        new GUIStyle(GUI.skin.button) {
            fontSize = 40,
            alignment = TextAnchor.MiddleCenter
        });

    private void OnGUI() {

        Rect labelRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, Screen.height * 0.5f, 800, 100);

        GUI.Label(labelRect,"ShootingEditor2D",labelStyle.Value);

        Rect buttonRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, Screen.height * 0.5f + 150, 400, 100);
        if (GUI.Button(buttonRect, "Start", buttonStyle.Value)) {
            SceneManager.LoadScene("Game");
        }
    }
}
