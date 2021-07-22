using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Examples;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MikroFramework.ResKit
{
    public class ResLoader {


        private List<Res> recordedRes = new List<Res>();


        #region Helpers
        private Res GetResFromRecord(string assetName)
        {
            return recordedRes.Find(loadedAsset => loadedAsset.Name == assetName);
        }

        private Res GetResFromResManager(string assetName)
        {
            return ResManager.Singleton.SharedLoadedResources.Find(loadedAsset => loadedAsset.Name == assetName);
        }

        private void AddResToRecord(Res resFromResManager)
        {
            recordedRes.Add(resFromResManager);
            resFromResManager.Retain();
        }

        /// <summary>
        /// Customize your Resource file path header here
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private Res CreateRes(string assetName,string ownerBundle=null) {
            Res res = null;

            res = ResFactory.CreateRes(assetName, ownerBundle);

            AddResToRecord(res);
            ResManager.Singleton.SharedLoadedResources.Add(res);
            return res;
        }

        private Res GetRes(string assetName) {
            //local resource pool
            Res res = GetResFromRecord(assetName);
            if (res != null)
            {
                return res;
            }

            //global resource pool
            res = GetResFromResManager(assetName);
            if (res != null)
            {
                AddResToRecord(res);
                return res;
            }

            
            return null;
        }

        private T DoLoadSync<T>(string assetName, string assetBundlePath=null) where T : Object {
            var res = GetRes(assetName);
            if (res != null)
            {
                if (res.State == ResState.Loading)
                {
                    throw new Exception($"Do not load resource {res.Name} synchronously while it's loading" +
                                        $" asynchronously!");
                }
                else if (res.State == ResState.Loaded)
                {
                    return res.Asset as T;
                }
            }

            res = CreateRes(assetName, assetBundlePath);
            res.LoadSync();
            return res.Asset as T;
        }

        private void DoLoadAsync<T>(string assetName, string assetBundlePath, Action<T> onLoaded) where T:Object{
            //local resource pool
            var res = GetRes(assetName);

            //event triggered after the resource is loaded: triggered onloaded; unregister resource's loaded event
            Action<Res> onResLoaded = null;
            onResLoaded = (loadedRes) => {
                onLoaded(loadedRes.Asset as T);
                res.UnRegisterOnLoadedEvent(onResLoaded);
            };


            if (res != null)
            {
                switch (res.State)
                {
                    //resource is still loading: listen to resource's load success event.
                    case ResState.Loading:
                        res.RegisterOnLoadedEvent(onResLoaded);
                        break;
                    case ResState.Loaded:
                        onLoaded(res.Asset as T);
                        break;
                }
                return;
            }

            //no such a resource loaded in the game: Create a new Res Object, add listener to
            //Res Object's load success event, and let Res to load the real resource asyncly 
            
            res = CreateRes(assetName,assetBundlePath);

            res.RegisterOnLoadedEvent(onResLoaded);

            res.LoadAsync();
        }

        #endregion

        /// <summary>
        /// Load an asset from AssetBundle, as well as the AssetBundle itself. Return the loaded asset.
        /// If the asset is already loaded somewhere else, it will not load again but only return the asset
        /// </summary>
        /// <returns></returns>
        public T LoadSync<T>(string assetBundleName, string assetName) where T : Object {
            return DoLoadSync<T>(assetName,assetBundleName);
        }


        /// <summary>
        /// Load an asset (internal supported: Resources and AssetBundle) folder to the memory. Return the loaded asset.
        /// If the asset is already loaded somewhere else, it will not load again but only return the asset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath">for Resources assets: assetPath must start with header "resources://"
        /// for AssetBundle assets: assetPath do not need a header
        /// for other customized assets: define them in ResFactory</param>
        /// <returns></returns>
        public T LoadSync<T>(string assetPath) where T : Object {
            return DoLoadSync<T>(assetPath);
        }


        /// <summary>
        /// Load an asset from AssetBundle asynchronously, as well as the AssetBundle itself. Return the loaded asset.
        /// If the asset is already loaded somewhere else, it will not load again but only return the asset
        /// </summary>
        /// <param name="onLoaded">Event triggered when loaded success</param>
        /// <returns></returns>
        public void LoadAsync<T>(string assetBundleName, string assetName, Action<T> onLoaded) where T : Object {
            DoLoadAsync<T>(assetName, assetBundleName, onLoaded);
        }

        /// <summary>
        /// Load an asset (internal supported: Resources and AssetBundle) to the memory asynchronously. Return the loaded asset.
        /// If the asset is already loaded somewhere else, it will not load again but only return the asset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath">for Resources assets: assetPath must start with header "resources://resourcepath"
        /// for AssetBundle assets: assetPath do not need a header
        /// for other customized assets: define them in ResFactory</param>
        /// <param name="onLoaded">Event triggered when loaded success</param>
        /// <returns></returns>
        public void LoadAsync<T>(string assetPath,Action<T> onLoaded) where T : Object {
            DoLoadAsync<T>(assetPath, null,onLoaded);
        }


        /// <summary>
        /// Release all assets of this Resource Loader. This will not unload the same assets on other Resource Loaders
        /// </summary>
        public void ReleaseAllAssets() {
            recordedRes.ForEach(loadedAsset => {loadedAsset.Release();});
            recordedRes.Clear();
        }
    }
}
