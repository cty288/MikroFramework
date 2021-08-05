using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MikroFramework.ResKit;
using MikroFramework.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace MikroFramework.ResKit
{
    public static class HotUpdateDownloader
    {
        public static List<ABMD5Base> totalDownloadingFiles = new List<ABMD5Base>();
        public static List<ABMD5Base> filesAlreadyDownloaded = new List<ABMD5Base>();
        public static UnityWebRequest downloadingFileRequest = null;


        /// <summary>
        /// Get Local AssetBundle ResVersion from local AB folder. Return null if not found
        /// </summary>
        /// <param name="getResVersion"></param>
        /// <returns></returns>
        public static IEnumerator GetLocalAssetResVersion(Action<ResVersion> getResVersion, Action<HotUpdateError>
            onGetFailed)
        {
            string localResVersionPath = HotUpdateConfig.LocalResVersionFilePath;

            UnityWebRequest request = UnityWebRequest.Get(localResVersionPath);

            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
                //getResVersion.Invoke(null);
                onGetFailed.Invoke(HotUpdateError.LocalResVersionNotExist);
            }
            else
            {
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
            Action<HotUpdateError> onError)
        {
            string remoteResVersionPath =HotUpdateConfig.RemoteResVersionURL;
            Debug.Log(remoteResVersionPath);

            UnityWebRequest request = UnityWebRequest.Get(remoteResVersionPath);
            yield return request.SendWebRequest();
            
            if (request.isHttpError || request.isNetworkError)
            {
                onError.Invoke(HotUpdateError.DownloadRemoeResVersionFailed);
            }
            else
            {
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
        public static void DoDownloadResVersion(ResVersion remoteResVersion)
        {
            if (!Directory.Exists(ResKitUtility.TempAssetBundlesPath))
            {
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
            Action<List<ABMD5Base>> onDownloadDone, Action<HotUpdateError> onDownloadFailed)
        {

            filesAlreadyDownloaded.Clear();
            totalDownloadingFiles = downloadList;

            string remoteBasePath =HotUpdateConfig.RemoteAssetBundleBaseURL;

            foreach (ABMD5Base downloadAsset in downloadList)
            {
                downloadingFileRequest = UnityWebRequest.Get(remoteBasePath + downloadAsset.AssetName);

                
                yield return downloadingFileRequest.SendWebRequest();

                if (downloadingFileRequest.isHttpError || downloadingFileRequest.isNetworkError)
                {
                    Debug.Log(downloadingFileRequest.error);
                    onDownloadFailed.Invoke(HotUpdateError.DownloadRemoteABFailed);
                    break;
                }
                else
                {
                    filesAlreadyDownloaded.Add(downloadAsset);
                    byte[] bytes = downloadingFileRequest.downloadHandler.data;
                    string filePath = ResKitUtility.TempAssetBundlesPath + downloadAsset.AssetName;

                    string directory = filePath.Substring(0, filePath.LastIndexOf('/'));

                    Debug.Log(directory);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllBytes(filePath, bytes);
                    downloadingFileRequest = null;
                    Debug.Log($"{downloadAsset.AssetName} downloaded success. Added to the temp folder");
                }
            }

            onDownloadDone?.Invoke(downloadList);
        }
    }
}
