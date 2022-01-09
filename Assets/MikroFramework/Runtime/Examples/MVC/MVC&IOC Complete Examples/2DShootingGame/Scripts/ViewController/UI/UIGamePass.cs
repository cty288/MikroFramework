using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGamePass : AbstractMikroController<ShootingEditor2D> {
    private readonly Lazy<GUIStyle> labelStyle = new Lazy<GUIStyle>(() => {
        return new GUIStyle() {fontSize = 80, alignment = TextAnchor.MiddleCenter};
    });

    private readonly Lazy<GUIStyle> buttonStyle = new Lazy<GUIStyle>(() => {
        return new GUIStyle(GUI.skin.button) { fontSize = 40, alignment = TextAnchor.MiddleCenter };
    });

    private void OnGUI() {
        
        Rect lavelRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, Screen.height * 0.5f, 400, 100);

        GUI.Label(lavelRect, "Game Pass", labelStyle.Value);

        Rect buttonRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, Screen.height * 0.5f+150, 400, 100);

        if (GUI.Button(buttonRect, "Back to Menu", buttonStyle.Value)) {
            SceneManager.LoadScene("GameStart");
        }
    }
}
