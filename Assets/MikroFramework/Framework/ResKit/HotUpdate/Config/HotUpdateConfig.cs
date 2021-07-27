using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MikroFramework.ResKit;
using MikroFramework.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace MikroFramework
{
    public static class HotUpdateConfig {
       
        

        /// <summary>
        /// Path that hot update asset bundles are saved
        /// </summary>
        public static string HotUpdateAssetBundlesFolder {
            get { return Application.persistentDataPath + "/AssetBundles/"; }
        }

        /// <summary>
        /// Path that the local Res Version file saved
        /// </summary>
        public static string LocalResVersionFilePath {
            get {
                return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtility.CurrentPlatformName +
                       "/ResVersion.json";
            }
        }

        /// <summary>
        /// Path that the local AssetBundle files saved
        /// </summary>
        public static string LocalAssetBundleFolder {
            get { return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtility.CurrentPlatformName + "/"; }
        }

        /// <summary>
        /// URL for ResVersion file that saved on the remote server
        /// </summary>
        public static string RemoteResVersionURL {
            get {
                return Application.dataPath + "/MikroFramework/Framework/ResKit/HotUpdate/Remote/" +
                       ResKitUtility.CurrentPlatformName + "/ResVersion.json";
            }
        }

        /// <summary>
        /// URL in which Asset Bundles are saved on the remote server
        /// </summary>
        public static string RemoteAssetBundleBaseURL {
            get {
                return Application.dataPath + "/MikroFramework/Framework/ResKit/HotUpdate/Remote/" +
                       ResKitUtility.CurrentPlatformName + "/";
            }
        }

        public static string AssetBundleResVersionBuildPath {
            get {
                return Application.dataPath + "/AssetBundleBuilds/" + Application.version + "/" +
                       ResKitUtility.CurrentPlatformName + "/";
            }
        }


        /// <summary>
        /// Get HotUpdate ResVersion from HotUpdate folder
        /// </summary>
        /// <returns></returns>
        public static ResVersion LoadHotUpdateAssetBundlesFolderResVersion() {
            string hotUpdateResVersionFilePath = HotUpdateAssetBundlesFolder + "ResVersion.json";


            if (!File.Exists(hotUpdateResVersionFilePath)) {
                return null;
            }

            string persistResVersionJson = File.ReadAllText(hotUpdateResVersionFilePath);
            ResVersion persistResVersion = JsonUtility.FromJson<ResVersion>(persistResVersionJson);

            return persistResVersion;

        }

       


    }

    //add manifest name
        //remoteResVersion.AssetBundleNames.Add(ResKitUtility.CurrentPlatformName);
}

