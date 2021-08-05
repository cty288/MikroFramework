using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Examples;
using MikroFramework.Managers;
using MikroFramework.Pool;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.ResKit
{
    [MonoSingletonPath("[FrameworkPersistent]/ResManager")]
    public class ResManager : ManagerBehavior,ISingleton {
        
        private List<Res> SharedLoadedResources = new List<Res>();

        private const string SimulationModeKey = "simulation mode";

        private static int simulationMode = -1;

        private static ResManager singleton {
            get {
                return SingletonProperty<ResManager>.Singleton;
            }
        }

        private void Awake() {
            DontDestroyOnLoad(this.gameObject);
            SafeObjectPool<ResourcesRes>.Singleton.Init(10,30);
            SafeObjectPool<AssetBundleRes>.Singleton.Init(20,50);
            SafeObjectPool<AssetRes>.Singleton.Init(30,100);
        }

        /// <summary>
        /// Get Global resource pool
        /// </summary>
        public static List<Res> GetSharedLoadedRes() {
            return singleton.SharedLoadedResources;
        }

        public static void RemoveSharedRes(Res res) {
            singleton.SharedLoadedResources.Remove(res);
            ResRecycleHelper.RecycleToPool(res);
        }

        void ISingleton.OnSingletonInit()
        {

        }

#if UNITY_EDITOR
        /// <summary>
        /// Get or set the current project's Simulation Mode
        /// Use IsSimultionModeLogic to determine whether the project is in Simulation Mode because this function will only run in the UnityEditor
        /// </summary>
        public static bool SimulationMode
        {
            get
            {
                if (simulationMode == -1)
                {
                    simulationMode = UnityEditor.EditorPrefs.GetBool(SimulationModeKey, true) ? 1 : 0;
                }

                return simulationMode != 0;
            }
            set
            {
                simulationMode = value ? 1 : 0;
                UnityEditor.EditorPrefs.SetBool(SimulationModeKey, value);
            }
        }
#endif


        /// <summary>
        /// get if the current project is in simulation mode
        /// it will always return false in RunTime build.
        /// SimulationMode makes you to "load" AssetBundles in UnityEditor without actually build AssetBundles
        /// </summary>
        public static bool IsSimulationModeLogic{
            get {
#if UNITY_EDITOR
                return SimulationMode;
#endif
                return false;
            }
        }
#if UNITY_EDITOR
        private void OnGUI()
        {
            if (Input.GetKey(KeyCode.F1))
            {
                GUILayout.BeginVertical("box");

                SharedLoadedResources.ForEach(loadedRes => {
                    GUILayout.Label($"Name: {loadedRes.Name}, ResCount: {loadedRes.RefCount}, ResState: {loadedRes.State}");
                });

                GUILayout.Label($"ResourceRes Object Pool count: {SafeObjectPool<ResourcesRes>.Singleton.CurrentCount}" +
                                $"/{SafeObjectPool<ResourcesRes>.Singleton.MaxCount}. " +
                                $"Active object count: {SafeObjectPool<ResourcesRes>.Singleton.NumActiveObject}");

                GUILayout.Label($"AssetBundleRes Object Pool count: {SafeObjectPool<AssetBundleRes>.Singleton.CurrentCount}" +
                                $"/{SafeObjectPool<AssetBundleRes>.Singleton.MaxCount}. " +
                                $"Active object count: {SafeObjectPool<AssetBundleRes>.Singleton.NumActiveObject}");

                GUILayout.Label($"AssetRes Object Pool count: {SafeObjectPool<AssetRes>.Singleton.CurrentCount}" +
                                $"/{SafeObjectPool<AssetRes>.Singleton.MaxCount}. " +
                                $"Active object count: {SafeObjectPool<AssetRes>.Singleton.NumActiveObject}");

                GUILayout.EndVertical();
            }

        }
#endif
    }
}
