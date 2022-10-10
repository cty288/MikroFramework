#if  UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommonMenuItems
{
    [MenuItem("MikroFramework/Framework/Open/Open Persistent Data Path")]
    private static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}

#endif
