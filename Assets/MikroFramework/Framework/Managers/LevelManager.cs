using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MikroFramework.Managers {
    public class LevelManager : MonoBehaviour {
        private static List<string> levelNames;
        public static int Index { get; set; }

        public static void Init(List<string> levelNames) {
            LevelManager.levelNames = levelNames;
            Index = 0;
        }

        public static void LoadCurrent() {
            SceneManager.LoadScene(levelNames[Index]);
        }

        public static void LoadNext() {
            Index++;
            if (Index >= levelNames.Count) {
                Index = 0;
            }

            SceneManager.LoadScene(levelNames[Index]);
        }
    }

}
