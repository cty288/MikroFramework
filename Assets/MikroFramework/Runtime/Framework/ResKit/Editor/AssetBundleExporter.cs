#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MikroFramework.Utilities;
using UnityEditor;
using UnityEngine;
using EditorUtility = MikroFramework.Utilities.EditorUtility;

namespace MikroFramework.ResKit
{

    public class AssetBundleExporter : MonoBehaviour
    {


        [MenuItem("MikroFramework/Framework/ResKit/Build AssetBundles to Local Path", false,1)]
        private static void NormalBuild() {
            BuildAB();
        }

        [MenuItem("MikroFramework/Framework/ResKit/Build AssetBundles to Asset Data Path", false,2)]
        private static void ABBuild()
        {
            BuildAB(true);
        }


        public static void BuildAB(bool isHotUpdate=false) {
            string outputPath = "";
            if (!isHotUpdate) {
                 outputPath= HotUpdateConfig.LocalAssetBundleFolder;
            }
            else {
                outputPath = HotUpdateConfig.AssetBundleResVersionBuildPath;
            }
            

            if (!Directory.Exists(outputPath)) {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);

            AssetDatabase.Refresh();

            EditorUtility.OpenInFolder(outputPath);


            WriteVersionConfig(outputPath);
            
        }

        private static void WriteVersionConfig(string outPutPath) {
            string versionConfigFilePath = outPutPath + "ResVersion.json";

            if (File.Exists(versionConfigFilePath))
            {
                File.Delete(versionConfigFilePath);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(outPutPath);
            FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

            ResVersion resVersion = new ResVersion();
            resVersion.ABMD5List = new List<ABMD5Base>();


            for (int i = 0; i < files.Length; i++) {
                if (!files[i].Name.EndsWith(".meta") && !files[i].Name.EndsWith(".manifest")) {
                    string fileCompletePath = files[i].FullName;
                    fileCompletePath = fileCompletePath.Replace('\\', '/');

                    string fileAssetPath= fileCompletePath.Substring(outPutPath.Length);
                  
                   
                    
                    ABMD5Base abmd5Base = new ABMD5Base() {
                        AssetName = fileAssetPath,
                        MD5 = ResKitUtility.BuildFileMd5(files[i].FullName),
                        FileSize = files[i].Length / 1024.0f
                    };
                    resVersion.ABMD5List.Add(abmd5Base);
                }
            }

            resVersion.Version = Application.version;
            string resVersionJson = JsonUtility.ToJson(resVersion,true);

            
            
            File.WriteAllText(versionConfigFilePath, resVersionJson);
        }
    }
}
#endif