using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.ResKit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MikroFramework.ResKit
{
    public class ResourcesRes : Res
    {
        
        
        public ResourcesRes(string assetPath)
        {
            AssetPath = assetPath.Substring("resources://".Length);
            Name = assetPath;
        }

        public override bool LoadSync() {
            State = ResState.Loading;
            Asset = Resources.Load(AssetPath);
            State = ResState.Loaded;
            return Asset;
        }

        public override void LoadAsync() {
            State = ResState.Loading;
            var request = Resources.LoadAsync(AssetPath);
            request.completed += operation => {
                Asset = request.asset;
                State = ResState.Loaded;
            };
        }

        protected override void OnReleaseRes() {
            if (Asset is GameObject)
            {
                Asset = null;
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Resources.UnloadAsset(Asset);
            }

            ResManager.Singleton.SharedLoadedResources.Remove(this);
            Asset = null;
        }

    }
}
