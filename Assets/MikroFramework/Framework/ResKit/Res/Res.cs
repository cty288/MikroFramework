
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MikroFramework.ResKit
{
    public enum ResState {
        /// <summary>
        /// Just created Res object, haven't loaded the asset yet
        /// </summary>
        Waiting,
        /// <summary>
        /// Loading the asset
        /// </summary>
        Loading,
        /// <summary>
        /// Asset loading success
        /// </summary>
        Loaded
    }

    /// <summary>
    /// Class that Load and Unload a specific Resource, inherited by ResourceRes, Assetbundle Res and AssetRes
    /// Inherit this class to customize your resource loader.
    /// </summary>
    public abstract class Res : SimpleRC {
        public ResState State {
            get {
                return state;
            }
            protected set {
                state = value;

                if (state == ResState.Loaded) {
                    onLoadedEvent?.Invoke(this);
                }
            }
        }
        private ResState state;

        public Object Asset;
        /// <summary>
        /// Name of the resource
        /// </summary>
        public string Name {
            get;
            protected set;
        }

        /// <summary>
        /// THe path of the asset
        /// </summary>
        public string AssetPath {
            get;
            protected set;
        }

        /// <summary>
        /// Load the resource
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSync();


        /// <summary>
        /// Load the resource asynchronously
        /// </summary>
        public abstract void LoadAsync();

        /// <summary>
        /// Unload this resource
        /// </summary>
        protected abstract void OnReleaseRes();

        protected override void OnZeroRef() {
            OnReleaseRes();
        }

        private event Action<Res> onLoadedEvent;

        /// <summary>
        /// Register a listener to the event of this Resource that triggered when the resource is loaded
        /// </summary>
        /// <param name="onLoaded"></param>
        public void RegisterOnLoadedEvent(Action<Res> onLoaded) {
            onLoadedEvent += onLoaded;
        }

        /// <summary>
        /// Unregister the listener to the event of this Resource that triggered when the resource is loaded
        /// </summary>
        /// <param name="onLoaded"></param>
        public void UnRegisterOnLoadedEvent(Action<Res> onLoaded) {
            onLoadedEvent -= onLoaded;
        }
    }
}
