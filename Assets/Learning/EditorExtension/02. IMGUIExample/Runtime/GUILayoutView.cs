using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorExtensionsLearning {
    public class GUILayoutView {
        #region Basic

        private string textFieldValue;
        private string textAreaValue;
        private string passwordValue = String.Empty;

        private Vector2 scrollPosition;

        private float sliderValue;

        private bool toggleValue;
        private int toolbarValue;
        private int selectionGridIndex;


        public void Draw()
        {
            GUILayout.Label("Label: Hello IMGUI");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            {
                GUILayout.BeginVertical("Box");
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("TextField");
                        textFieldValue = GUILayout.TextField(textFieldValue);
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.Space(100);


                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("TextArea");
                        textAreaValue = GUILayout.TextArea(textAreaValue);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("PasswordArea");
                        passwordValue = GUILayout.PasswordField(passwordValue, '*');
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Button");
                        //GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Button", GUILayout.MinWidth(100), GUILayout.MaxWidth(150), GUILayout.MinHeight(100), GUILayout.MaxHeight(150)))
                        {
                            Debug.Log("Button Clicked");
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Repeat Button");
                        if (GUILayout.RepeatButton("Repeat Button", GUILayout.Width(150), GUILayout.Height(150)))
                        {
                            Debug.Log("Repeat Button Clicked");
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Box");
                        GUILayout.Box("AutoLayout Bot");
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Horizontal Slider");
                        sliderValue = GUILayout.HorizontalSlider(sliderValue, 0, 1);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Vertical Slider");
                        sliderValue = GUILayout.VerticalSlider(sliderValue, 0, 1, GUILayout.Height(100));
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Area");
                        GUILayout.BeginArea(new Rect(0, 0, 100, 100));
                        {
                            GUI.Label(new Rect(0, 0, 20, 20), "1");
                        }
                        GUILayout.EndArea();
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Window");
                        GUILayout.Window(1, new Rect(0, 0, 100, 100), id => {

                        }, "2");
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("ToolBar");
                        toolbarValue = GUILayout.Toolbar(toolbarValue, new string[] { "1", "2", "3", "4", "5" });
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Toggle");
                        toggleValue = GUILayout.Toggle(toggleValue, "toggle");
                    }
                    GUILayout.EndHorizontal();

                    selectionGridIndex =
                        GUILayout.SelectionGrid(selectionGridIndex, new string[] { "1", "2", "3", "4", "5" }, 3);

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();

        }
        #endregion
    }

}
