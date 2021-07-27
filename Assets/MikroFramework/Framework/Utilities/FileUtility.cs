using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MikroFramework.Utilities
{
    public class FileUtility
    {
        /// <summary>
        /// Get all files under a specific directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeManifest">do the results include manifest files (for ab)?</param>
        /// <param name="includeMeta">do the results include Unity .meta files?</param>
        /// <returns></returns>
        public static List<FileInfo> GetAllDirectoryFiles(string path, bool includeManifest, bool includeMeta)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] allFiles = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            List<FileInfo> processedResults = new List<FileInfo>();
            foreach (FileInfo fileInfo in allFiles)
            {

                if (fileInfo.Name.EndsWith(".manifest"))
                {
                    if (includeManifest)
                    {
                        processedResults.Add(fileInfo);
                    }
                    continue;
                }


                if (fileInfo.Name.EndsWith(".meta"))
                {
                    if (includeMeta)
                    {
                        processedResults.Add(fileInfo);
                    }
                    continue;
                }


                processedResults.Add(fileInfo);
            }

            return processedResults;
        }

        /// <summary>
        /// Delete a specific file at a specific path
        /// </summary>
        /// <param name="file"></param>
        /// <returns>If any error occurs</returns>
        public static bool DeleteFile(FileInfo file)
        {

            try
            {
                File.Delete(file.FullName);
                return true;
            }
            catch (IOException e)
            {
                Debug.Log(e.ToString());
                return false;
            }
        }
    }
}
