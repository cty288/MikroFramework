using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorExtensionsLearning{
    //shortcuts:
    //1. % ctrl/cmd
    //2. & alt/option
    //3. # shift
    public static class MenuItemExample  {
        [MenuItem("EditorExtensions/01. Menu/01. Hello Editor")] 
        private static void HelloEditor() {
            Debug.Log("Hello Editor");
        }

        [MenuItem("EditorExtensions/01. Menu/02. Open Bilibili")]
        private static void OpenBilibili() {
            Application.OpenURL("https://bilibili.com");
        }
        
        [MenuItem("EditorExtensions/01. Menu/03. Open Persistent Data Path")]
        private static void OpenPersistentDataPath() {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("EditorExtensions/01. Menu/04. Open Designer Folder")]
        private static void OpenDesignerPath() {
            EditorUtility.RevealInFinder(Application.dataPath.Replace("Assets", "Library"));
        }

        private static bool openShotcut = false;
        [MenuItem("EditorExtensions/01. Menu/05. Toggle Shortcut")]
        private static void ToggleShortCut() {
            openShotcut = !openShotcut;
            Menu.SetChecked("EditorExtensions/01. Menu/05. Toggle Shortcut", openShotcut);
        }


        [MenuItem("EditorExtensions/01. Menu/06. Hello Editor _c")]
        private static void HelloEditorWithShortcut() {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01. Menu/01. Hello Editor");
        }

        [MenuItem("EditorExtensions/01. Menu/06. Hello Editor _c", validate = true)]
        private static bool HelloEditorWithShortcutValidate() {
            return openShotcut;
        }


        [MenuItem("EditorExtensions/01. Menu/07. Open Bilibili %u")]
        private static void OpenBilibiliWithShortCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01. Menu/02. Open Bilibili");
        }

        [MenuItem("EditorExtensions/01. Menu/07. Open Bilibili %u", validate = true)]
        private static bool OpenBilibiliWithShortCutValidate() {
            return openShotcut;
        }

        [MenuItem("EditorExtensions/01. Menu/08. Open Persistent Data Path %#t")]
        private static void OpenPersistentDataPathShortCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01. Menu/03. Open Persistent Data Path");
        }

        [MenuItem("EditorExtensions/01. Menu/08. Open Persistent Data Path %#t", validate = true)]
        private static bool OpenPersistentDataPathShortCutValidate() {
            return openShotcut;
        }

        [MenuItem("EditorExtensions/01. Menu/09. Open Designer Folder With ShortCut &y")]
        private static void OpenDesignerFolderWithShortCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01. Menu/04. Open Designer Folder");
        }

        [MenuItem("EditorExtensions/01. Menu/09. Open Designer Folder With ShortCut &y", validate = true)]
        private static bool OpenDesignerFolderWithShortCuValidate() {
            return openShotcut;
        }

        //refresh toggles 
        static MenuItemExample() {
            Menu.SetChecked("EditorExtensions/01. Menu/05. Toggle Shortcut", openShotcut);
        }
    }

}
