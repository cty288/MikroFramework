using System.Collections;
using System.Collections.Generic;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.ResKit
{
    public class ResManager : MonoPersistentMikroSingleton<ResManager> {
        /// <summary>
        /// Global resource pool
        /// </summary>
        public List<Res> SharedLoadedResources = new List<Res>();

        private const string SimulationModeKey = "simulation mode";

        private static int simulationMode = -1;

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

                GUILayout.EndVertical();
            }

        }
#endif
    }
}
