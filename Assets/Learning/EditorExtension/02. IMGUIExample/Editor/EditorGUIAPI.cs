using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;


namespace EditorExtensionsLearning {
    public class EditorGUIAPI {
        private Rect labelRect = new Rect(10, 50, 200, 20);
        private Rect disableGroupToggleRect = new Rect(10, 80, 200, 20);
        private Rect floatFieldRect = new Rect(10, 110, 200, 20);
        private Rect textFieldRect = new Rect(10, 170, 500, 20);
        private Rect textAreaRect = new Rect(10, 200, 500, 100);
        private Rect passwordFieldRect = new Rect(10, 310, 500, 20);
        private Rect dropdownButtonRect = new Rect(10, 340, 200, 20);
        private Rect linkedButtonRect = new Rect(10, 370, 200, 20);
        private Rect toggleRect = new Rect(10, 400, 200, 20);
        private Rect toggleLeftRect = new Rect(10, 430, 200, 20);
        private Rect helpBoxRect = new Rect(10, 460, 500, 100);
        private Rect colorFieldRect = new Rect(10, 570, 500, 20);
        private Rect boundsFieldRect = new Rect(10, 600, 500, 20);
        private Rect boundsIntFieldRect = new Rect(10, 660, 500, 20);
        private Rect curveFieldRect = new Rect(10, 770, 500, 20);
        private Rect delayedDoubleFieldRect = new Rect(10, 800, 500, 20);
        private Rect enumFlagsFieldRect = new Rect(10, 830, 500, 20);
        private Rect enumPopRect = new Rect(10, 860, 500, 20);


        private Rect gradientFieldRect = new Rect(550, 80, 500, 20);

        private bool canUseGroup = true;
        private float floatFieldValue = 0;
        private string textFieldValue = "Hello World";
        private string textAreaValue = "Hello World";
        private string passwordFieldValue = "";
        private bool toggleVelue;
        private Color colorFieldValue = Color.white;
        private Bounds boundsFieldValue = new Bounds(Vector3.zero, Vector3.one);
        private BoundsInt boundsIntFieldValue = new BoundsInt(Vector3Int.zero, Vector3Int.one);
        private AnimationCurve curveFieldValue = new AnimationCurve();
        private double delayedDoubleFieldValue = 0;
        private EnumFlagsFieldValue enumFlagsFieldValue = EnumFlagsFieldValue.A | EnumFlagsFieldValue.B;
        private Gradient gradientFieldValue = new Gradient();

        private bool foldoutValue = true;
        private enum EnumFlagsFieldValue {
            A=1,B,C

        }

        public void Draw() {
            EditorGUI.LabelField(labelRect , "LabelField");

            foldoutValue = EditorGUI.Foldout(new Rect(250, 80, 200, 20), foldoutValue, "Foldout");

            if (foldoutValue) {
                canUseGroup = EditorGUI.Toggle(disableGroupToggleRect, "Disable Group", canUseGroup);
                EditorGUI.BeginDisabledGroup(!canUseGroup);


                floatFieldValue = EditorGUI.FloatField(floatFieldRect, "FloatField", floatFieldValue);
                EditorGUI.FloatField(new Rect(10, 140, 200, 20), "FloatField2", 20);
                EditorGUI.EndDisabledGroup();


                textFieldValue = EditorGUI.TextField(textFieldRect, "Text Field", textFieldValue);
                textAreaValue = EditorGUI.TextArea(textAreaRect, textAreaValue);
                passwordFieldValue = EditorGUI.PasswordField(passwordFieldRect, "Password Field", passwordFieldValue);


                var enumValues = Enum.GetValues(typeof(BuildOptions));
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent($"{enumValues}"), false, (f) => { }, enumValues);
                if (EditorGUI.DropdownButton(dropdownButtonRect, new GUIContent("Button"), FocusType.Keyboard))
                {
                    Debug.Log("Dropdown Button Clicked");
                    menu.ShowAsContext();
                }


                if (EditorGUI.LinkButton(linkedButtonRect, "LinkedButton"))
                {
                    Debug.Log("Linked Button Clicked");
                }


                toggleVelue = EditorGUI.Toggle(toggleRect, "Toggle", toggleVelue);
                toggleVelue = EditorGUI.ToggleLeft(toggleLeftRect, "Toggle Left", toggleVelue);


                EditorGUI.HelpBox(helpBoxRect, "Help Box", MessageType.Info);


                colorFieldValue = EditorGUI.ColorField(colorFieldRect, "Color Field", colorFieldValue);
                boundsFieldValue = EditorGUI.BoundsField(boundsFieldRect, "Bounds Field", boundsFieldValue);

                boundsIntFieldValue = EditorGUI.BoundsIntField(boundsIntFieldRect, "BoundsInt Field", boundsIntFieldValue);
                curveFieldValue = EditorGUI.CurveField(curveFieldRect, "Curve Field", curveFieldValue);

                delayedDoubleFieldValue = EditorGUI.DelayedDoubleField(delayedDoubleFieldRect, "Delayed Double Field",
                    delayedDoubleFieldValue);

                enumFlagsFieldValue = (EnumFlagsFieldValue)EditorGUI.EnumFlagsField(enumFlagsFieldRect, "Enum Flags Field",
                    enumFlagsFieldValue);

                enumFlagsFieldValue = (EnumFlagsFieldValue)EditorGUI.EnumPopup(enumPopRect, "Enum Popup Field",
                    enumFlagsFieldValue);

                gradientFieldValue = EditorGUI.GradientField(gradientFieldRect, "Gradient Field", gradientFieldValue);
            }
            
        }
    }
}

