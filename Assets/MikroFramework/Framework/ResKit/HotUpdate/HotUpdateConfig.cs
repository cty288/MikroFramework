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

        /// <summary>
        /// Get Local AssetBundle ResVersion from local AB folder. Return null if not found
        /// </summary>
        /// <param name="getResVersion"></param>
        /// <returns></returns>
        public static IEnumerator GetLocalAssetResVersion(Action<ResVersion> getResVersion, Action<HotUpdateError>
            onGetFailed) {
            string localResVersionPath = HotUpdateConfig.LocalResVersionFilePath;

            UnityWebRequest request = UnityWebRequest.Get(localResVersionPath);

            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError) {
                Debug.Log(request.error);
                //getResVersion.Invoke(null);
                onGetFailed.Invoke(HotUpdateError.LocalResVersionNotExist);
            }
            else {
                string jsonString = request.downloadHandler.text;
                ResVersion resVersion = JsonUtility.FromJson<ResVersion>(jsonString);
                getResVersion.Invoke(resVersion);
            }
        }

        /// <summary>
        /// Get the ResVersion file from remote server
        /// </summary>
        /// <param name="onResDownloaded"></param>
        /// <returns></returns>
        public static IEnumerator RequestGetRemoteResVersion(Action<ResVersion> onResDownloaded,
            Action<HotUpdateError> onError) {
            string remoteResVersionPath = RemoteResVersionURL;
            Debug.Log(remoteResVersionPath);

            UnityWebRequest request = UnityWebRequest.Get(remoteResVersionPath);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError) {
                onError.Invoke(HotUpdateError.DownloadRemoeResVersionFailed);
            }
            else {
                string jsonString = request.downloadHandler.text;
                ResVersion resVersion = JsonUtility.FromJson<ResVersion>(jsonString);
                onResDownloaded.Invoke(resVersion);
            }
        }

        /// <summary>
        /// Download all resources from remote server.
        /// </summary>
        /// <param name="remoteResVersion"></param>
        /// <param name="onloadedDone"></param>
        /// <returns></returns>
        public static void DoDownloadResVersion(ResVersion remoteResVersion) {
            if (!Directory.Exists(ResKitUtility.TempAssetBundlesPath)) {
                Directory.CreateDirectory(ResKitUtility.TempAssetBundlesPath);
            }

            string tempResVersionFilePath = ResKitUtility.TempAssetBundlesPath + "ResVersion.json";
            string tempResVersionJson = JsonUtility.ToJson(remoteResVersion, true);
            File.WriteAllText(tempResVersionFilePath, tempResVersionJson);
        }


        /// <summary>
        /// Download Asset Bundles from the remote server to the temp folder, given a list of ABMD5 infos.
        /// </summary>
        /// <param name="downloadList"></param>
        /// <param name="onDownloadDone">Event triggered after download. If download failed, the bool parameter will be false</param>
        /// <returns></returns>
        public static IEnumerator DoDownloadRemoteABs(List<ABMD5Base> downloadList,
            Action<List<ABMD5Base>> onDownloadDone,Action<HotUpdateError> onDownloadFailed) {


            string remoteBasePath = RemoteAssetBundleBaseURL;

            foreach (ABMD5Base downloadAsset in downloadList) {
                UnityWebRequest request = UnityWebRequest.Get(remoteBasePath + downloadAsset.AssetName);

                yield return request.SendWebRequest();

                if (request.isHttpError || request.isNetworkError) {
                    Debug.Log(request.error);
                    onDownloadFailed.Invoke(HotUpdateError.DownloadRemoteABFailed);
                    break;
                }
                else {
                    byte[] bytes = request.downloadHandler.data;
                    string filePath = ResKitUtility.TempAssetBundlesPath + downloadAsset.AssetName;

                    string directory = filePath.Substring(0, filePath.LastIndexOf('/'));

                    Debug.Log(directory);
                    if (!Directory.Exists(directory)) {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllBytes(filePath, bytes);
                    Debug.Log($"{downloadAsset.AssetName} downloaded success. Added to the temp folder");
                }
            }

            onDownloadDone?.Invoke(downloadList);
        }

    }

    //add manifest name
        //remoteResVersion.AssetBundleNames.Add(ResKitUtility.CurrentPlatformName);
}

