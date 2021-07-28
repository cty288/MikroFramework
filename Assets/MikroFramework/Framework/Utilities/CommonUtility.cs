using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Utilities {
    public partial class CommonUtility
    {
        /// <summary>
        /// Copy a specific text to the clipboard
        /// </summary>
        /// <param name="text">text to copy</param>
        public static void CopyText(string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }

        /// <summary>
        /// Return if the new version is bigger or equal than the old version
        /// </summary>
        /// <param name="newVersion"></param>
        /// <param name="oldVersion"></param>
        /// <returns></returns>
        public static bool CompareVersions(string newVersion, string oldVersion)
        {
            Version v1 = new Version(newVersion);
            Version v2 = new Version(oldVersion);
            return v1 > v2;
        }

        /// <summary>
        /// Delete the "(Clone)" text on object's name if the object is cloned
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DeleteCloneName(GameObject obj) {
            int cloneNameStartIndex = obj.name.IndexOf("(Clone)");
            if (cloneNameStartIndex >= 0)
            {
                return obj.name.Substring(0, cloneNameStartIndex);
            }

            return obj.name;
        }
    }
}
