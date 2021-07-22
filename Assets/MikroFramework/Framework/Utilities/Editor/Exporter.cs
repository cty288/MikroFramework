#if UNITY_EDITOR


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikroFramework.Utilities {
    public class Exporter
    {
        private static string GeneratePackageName()
        {
            return "Builds/MikroFramework_build_" + DateTime.Now.ToString("yyyyMMdd_HH") + ".unitypackage";
        }


        [MenuItem("MikroFramework/Framework/Editor/Export this Framework as UnityPackage %e", false, 1)]
        private static void MenuCreateUnityPackage()
        {
            string fileName = GeneratePackageName();

            CommonUtility.CopyText(fileName);

            string assetPathName = "Assets/MikroFramework";

            Debug.Log($"Generated file: {fileName}");

            EditorUtility.ExportPackage(assetPathName, fileName);

            EditorUtility.OpenInFolder(Environment.CurrentDirectory + "/Builds");
        }

    }
}

#endif