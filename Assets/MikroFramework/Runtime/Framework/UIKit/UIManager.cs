using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Managers;
using MikroFramework.Singletons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace MikroFramework.UIKit {
    public enum UILayer
    {
        Bg,
        Common,
        Top,
    }



    public struct DefaultUISettings {
        public Vector2 ReferenceResolution;
        public float MatchWidthOrHeight;

        public DefaultUISettings(Vector2 referenceResolution, float matchWidthOrHeight) {
            this.ReferenceResolution = referenceResolution;
            this.MatchWidthOrHeight = matchWidthOrHeight;
        }
    }
    public class UIManager : ManagerBehavior, ISingleton {
        private Dictionary<IPanel, IUIRoot> allPanelsToRoots = new Dictionary<IPanel, IUIRoot>();
        private List<IUIRoot> allRoots = new List<IUIRoot>();

        public static UIManager Singleton {
            get {
                return SingletonProperty<UIManager>.Singleton;
            }
        }

        public static DefaultUISettings DefaultUISettings { get; private set; } = new DefaultUISettings()
            {MatchWidthOrHeight = 0, ReferenceResolution = new Vector2(1920, 1080)};


        void ISingleton.OnSingletonInit() {
            
        }


        public T GetPanel<T>(bool isActive) where T:IPanel{
            foreach (var panel in allPanelsToRoots.Keys) {
                if (panel is T && panel.IsOpening == isActive) {
                    return (T)panel;
                }
            }

            return default(T);
        }
        

        public void RegisterPanel(IPanel panel, IUIRoot root) {

            if (panel.PanelType != PanelType.Root) {
                if (!allPanelsToRoots.ContainsKey(panel)) {
                    allPanelsToRoots.Add(panel, root);
                }
            }
          
            if (!allRoots.Contains(root)) {
                allRoots.Add(root);
            }
        }

        /// <summary>
        /// Open a panel. This will always open a currently closed panel. If no closed panel exists or no pre-existing panel exists, it will try to create a new one (if createNewIfNotExist is true)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">The parent of this panel. Can be root or any panel containers. If null, the parent will be a random root panel. Note that MainPanels can only have the root UI as its parent</param>
        /// <param name="message">The message to be passed to the panel</param>
        /// <param name="createNewIfNotExist"> </param>
        /// <param name="assetNameIfNotExist">The asset name in the asset bundle for the panel. Used to create the panel if it doesn't exist in the scene</param>
        public T Open<T>(IPanelContainer parent, UIMsg message, bool createNewIfNotExist = true,
            string assetNameIfNotExist = "") where T: class, IPanel{
            if (allRoots.Count == 0) {
                if (!EventSystem.current) {
                   EventSystem eventSystem = new GameObject("EventSystem").AddComponent<EventSystem>();
                    eventSystem.gameObject.AddComponent<StandaloneInputModule>();
                }
                
                GameObject rootPanel = new GameObject("UIRoot");
                var uiRoot = rootPanel.AddComponent<UIRoot>();
               
            }
            
            if (parent == null) {
                IUIRoot selectedRoot = allRoots[Random.Range(0, allRoots.Count)];
                return selectedRoot.Open<T>(null, message, createNewIfNotExist, assetNameIfNotExist);
            }

            if (parent is IUIRoot root) {
                return root.Open<T>(null, message, createNewIfNotExist, assetNameIfNotExist);
            }

            if (allPanelsToRoots.ContainsKey(parent)) {
                return allPanelsToRoots[parent].Open<T>(parent, message, createNewIfNotExist, assetNameIfNotExist);
            }
            else {
                Debug.LogException(new Exception($"The parent container {parent} is not registered into the UI Manager!"));
                return default(T);
            }
        }


        /// <summary>
        /// Close the current panel. If the current panel is the main panel, then it will return to the last opened main panel.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="alsoCloseChild"></param>
        public void ClosePanel(IPanel panel, bool alsoCloseChild = true) {
            if (allPanelsToRoots.ContainsKey(panel)) {
                allPanelsToRoots[panel].ClosePanel(panel, alsoCloseChild);
            }
            else {
                Debug.LogException(new Exception($"The panel {panel} is not registered into the UI Manager!"));
            }
        }


        public static void SetResolution(DefaultUISettings uiSettings) {
            DefaultUISettings = uiSettings;
        }


    }

}
