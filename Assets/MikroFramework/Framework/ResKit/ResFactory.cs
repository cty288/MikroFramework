using System.Collections;
using System.Collections.Generic;
using MikroFramework.Examples;
using UnityEngine;

namespace MikroFramework.ResKit
{
    public class ResFactory {
        /// <summary>
        /// Create a proper Res Object depends on assetName and assetBundleName
        /// For Resources asset: start with "resources://"
        /// For AssetBundle and its single asset: simply write AB file (asset) name
        /// For customized Res Class Object: Customize their create method here.
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static Res CreateRes(string assetName, string assetBundleName) {
            Res res = null;

            if (assetName.StartsWith("resources://"))
            {
                res = new ResourcesRes(assetName);
            }//else if(asserName.StartsWith("customizeIt://")
            else
            {
                if (assetBundleName != null)
                {
                    res = new AssetRes(assetName, assetBundleName);
                }
                else
                {
                    res = new AssetBundleRes(assetName);
                }

            }

            return res;
        }
    }
}
