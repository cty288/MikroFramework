using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EditorExtensionsLearning
{
    public class GUIAPI {
        private Rect labelRect = new Rect(20, 60, 200, 20);
        private Rect textFieldRect = new Rect(20, 90, 200, 20);
        private Rect textAreaRect = new Rect(20, 120, 200, 100);
        private Rect passwordFieldRect = new Rect(20, 240, 200, 20);
        private Rect buttonRect = new Rect(20, 270, 200, 20);
        private Rect repeatButtonRect = new Rect(20, 300, 200, 20);
        private Rect toggleRect = new Rect(20, 330, 200, 20);
        private Rect toolbarRect = new Rect(20, 360, 400, 20);
        private Rect boxRect = new Rect(20, 390, 200, 100);
        private Rect horizontalSliderRect = new Rect(20, 500, 200, 20);
        private Rect verticalSliderRect = new Rect(20, 530, 20, 200);
        private Rect groupRect = new Rect(20, 740, 200, 100);
        private Rect windowRect = new Rect(20, 850, 200, 100);

        private string textFieldValue;
        private string textAreaValue;
        private string passwordFieldValue = String.Empty;
        private bool toggleValue;
        private int toolbarValue;
        private int horizontalSliderValue;
        private int verticalSliderValue;

        public void Draw() {
            GUI.BeginScrollView(new Rect(20, 0, 400, 500), new Vector2(0, 0), new Rect(0, 0, 400, 500));
            
            GUI.Label(labelRect, "Label: Hello GUI API");
            textFieldValue = GUI.TextField(textFieldRect, textFieldValue);
            textAreaValue = GUI.TextArea(textAreaRect, textAreaValue);

            passwordFieldValue = GUI.PasswordField(passwordFieldRect, passwordFieldValue, '*');

            if (GUI.Button(buttonRect, "Button")) {
                Debug.Log("Button clicked");
            }

            if (GUI.RepeatButton(repeatButtonRect, "Repeat Button")) {
                Debug.Log("Repeat Button clicked");
            }

            toggleValue = GUI.Toggle(toggleRect, toggleValue, "Toggle");

            toolbarValue = GUI.Toolbar(toolbarRect, toolbarValue, new string[] { "Toolbar 1", "Toolbar 2", "Toolbar 3" });

            GUI.Box(boxRect, "Box");

           
            GUI.EndScrollView();

            horizontalSliderValue = (int)GUI.HorizontalSlider(horizontalSliderRect, horizontalSliderValue, 0, 100);
            verticalSliderValue = (int)GUI.VerticalSlider(verticalSliderRect, verticalSliderValue, 0, 100);

            GUI.BeginGroup(groupRect, "Group");
            GUI.Label(new Rect(20, 20, 200, 20), "Label in Group");
            GUI.EndGroup();

            GUI.Window(10000, windowRect,(id => {}), "Window");
        }
    }
}
