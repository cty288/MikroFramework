#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.EditorFramework;
using UnityEditor;
using UnityEngine;

namespace MikroFramework.Examples {
    [CustomEditorWindow(2)]
    public class GUIBaseExample : EditorWindow {
        public class MyDescAttribute : Attribute {
            public string Type;
            public MyDescAttribute(string type) {
                Type = type;
            }
        }



        
        public class DescriptionBase {
            public virtual string Description { get; set; }
        }

        public class MyDescription : DescriptionBase {
            public override string Description { get; set; } = "DescriptionA";
        }


        [MyDesc("TypeB")]
        public class MyDescriptionB : DescriptionBase {
            public override string Description { get; set; } = "DescriptionB";
        }

        
        public class Label : GUIBase {
            private string text;

            public Label(string text) {
                this.text = text;
            }

            public override void OnGUI(Rect position) {
                base.OnGUI(position);
                GUILayout.Label(text);
            }

            protected override void OnDisposed() {
                Debug.Log("Disposed");
            }
        }

        private IEnumerable<Type> descriptionTypes;
        private IEnumerable<Type> descriptionTypesWithAttribute;


        private void OnEnable() {
            descriptionTypes = typeof(DescriptionBase).GetSubTypesInAssemblies();
            descriptionTypesWithAttribute = typeof(DescriptionBase).GetSubTypesInAssemblies<MyDescAttribute>();
        }

        private Label label = new Label("123");
        private Label label2 = new Label("456");
        private void OnGUI() {
            label.OnGUI(default);
            label2.OnGUI(default);
            foreach (Type descriptionType in descriptionTypes) {
                GUILayout.Label(descriptionType.Name);
            }

            GUILayout.BeginHorizontal("box");
            foreach (Type type in descriptionTypesWithAttribute) {
               
                GUILayout.Label(type.Name);
              
            }
            GUILayout.EndHorizontal();
        }
    }
}


#endif
