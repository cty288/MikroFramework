using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace EditorExtensionsLearning
{
    public class EditorGUILayoutAPI {
        private BuildTargetGroup buildTargetGroupValue;
        
        AnimBool animBool = new AnimBool(true);
        private bool foldoutGoup = false;

        private bool groupValue = false;
        private bool toggle1Value = false;
        private bool toggle2Value = true;
        public void Draw() {
            animBool.target = EditorGUILayout.ToggleLeft("Open Fade Group", animBool.target);

            foldoutGoup = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutGoup, "FoldOut");
            if (foldoutGoup) {
                EditorGUILayout.BeginFadeGroup(animBool.faded);
                if(animBool.target){
                    buildTargetGroupValue = EditorGUILayout.BeginBuildTargetSelectionGrouping();
                    EditorGUILayout.EndBuildTargetSelectionGrouping();
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            groupValue = EditorGUILayout.BeginToggleGroup("All set", groupValue);
            toggle1Value = EditorGUILayout.Toggle(toggle1Value);
            toggle2Value = EditorGUILayout.Toggle(toggle2Value);
            EditorGUILayout.EndToggleGroup();
        }
    }
}
