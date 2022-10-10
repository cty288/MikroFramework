using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MikroFramework.ResKit;
using MikroFramework.Singletons;
using MikroFramework.UIKit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MikroFramework.UIKit
{

    public enum PanelType
    {
        MainPanel,
        PopUp,
        Root
    }

    public interface IPanel
    {
        /// <summary>
        /// Return true if the panel is on the highest level among all UI (at the top of the stack)
        /// </summary>
        /// <returns></returns>
        bool IsOnTop();

        /// <summary>
        /// Every panel has a parent. The parent of all panels are UIRoot
        /// </summary>
        IPanelContainer Parent { get; set; }


        GameObject gameObject { get; }

        PanelType PanelType { get; }

        /// <summary>
        /// For XBoxController and GamePads: use this to assign a default selected game object when the panel is changed to be the top panel. Can leave as null
        /// </summary>

        GameObject DefaultSelectedGameObjectWhenFocused { get; }

        bool IsOpening { get; set; }

        /// <summary>
        /// Called at the point when the panel is on top
        /// </summary>
        void OnFocused();

        /// <summary>
        /// Called when the panel is first instantiated by the UIRoot; if the panel pre-exists, this will act like Awake()
        /// </summary>
        void OnInit();

        /// <summary>
        /// Called on the point when the panel is opened
        /// </summary>
        void OnOpen(UIMsg msg);

        /// <summary>
        /// Called on the point when the panel is closed
        /// </summary>
        void OnClosed();
    }

    public interface UIMsg { }
    public interface IPanelContainer : IPanel
    {
        List<IPanel> Children { get; }
        IPanel GetTopChild();
    }


    public interface IUIRoot : IPanelContainer
    {
        T Open<T>(IPanelContainer parent, UIMsg message, bool createNewIfNotExist = true,
            string assetNameIfNotExist = "") where T : class, IPanel;

        void ClosePanel(IPanel panel, bool alsoCloseChild = true);

    }

    //TODO: add "hide"
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        //examples:
        //1. Normal open/close
        //2. Load from AssetBundle
        //3. Nested Open/Close
        //4. Multiple UIRoots


        private Dictionary<Type, List<IPanel>> allGeneratedPanels = new Dictionary<Type, List<IPanel>>();
        private ResLoader resLoader;
        private IPanel currentMainPanel = null;

        [SerializeField] private GameObject defaultMainPanel;
        protected virtual void Awake()
        {
            if (!GetComponent<Canvas>())
            {
                DefaultUISettings uiSettings = UIManager.DefaultUISettings;
                Canvas canvas = gameObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = uiSettings.ReferenceResolution;
                canvasScaler.matchWidthOrHeight = uiSettings.MatchWidthOrHeight;
                gameObject.AddComponent<GraphicRaycaster>();
            }
            GetPreExistingPanels();
            resLoader = ResLoader.Allocate();

        }



        public bool IsOnTop()
        {
            return true;
        }

        /// <summary>
        /// Open a panel. This will always open a currently closed panel. If no closed panel exists or no pre-existing panel exists, it will try to create a new one (if createNewIfNotExist is true)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">The parent of this panel. Leave null if the parent is the root UI. Note that MainPanels can only have the root UI as its parent</param>
        /// <param name="message">The message to be passed to the panel</param>
        /// <param name="createNewIfNotExist"> </param>
        /// <param name="assetNameIfNotExist">The asset name in the asset bundle for the panel. Used to create the panel if it doesn't exist in the scene</param>
        public T Open<T>(IPanelContainer parent, UIMsg message, bool createNewIfNotExist = true,
            string assetNameIfNotExist = "") where T : class, IPanel
        {
            IPanelContainer parentContainer = null;
            if (parent == null)
            {
                parentContainer = this;
            }
            else
            {
                if (!parent.IsOpening)
                {
                    Debug.LogException(new Exception($"The parent panel is {parent} closed! You cannot open this panel on this parent panel!"));
                    return default(T);
                }
                parentContainer = parent;
            }

            IPanel firstUnOpenedPanel = GetFirstPanel(typeof(T), false);

            if (firstUnOpenedPanel == null)
            {
                if (!createNewIfNotExist)
                {
                    Debug.LogException(new Exception($"The panel of type {typeof(T)} does not currently exist!"));
                    return default(T);
                }

                if (String.IsNullOrEmpty(assetNameIfNotExist))
                {
                    assetNameIfNotExist = typeof(T).Name;
                }
                GameObject panel = resLoader.LoadSync<GameObject>(assetNameIfNotExist);
                if (!panel || panel.GetComponent<IPanel>() == null)
                {
                    Debug.LogException(new Exception($"The panel of type {typeof(T)} you try to create does not inherit from IPanel!"));
                    return default(T);
                }
                firstUnOpenedPanel = Instantiate(panel, transform).GetComponent<IPanel>();
                UIManager.Singleton.RegisterPanel(firstUnOpenedPanel, this);
                if (allGeneratedPanels.ContainsKey(typeof(T)))
                {
                    allGeneratedPanels[typeof(T)].Add(firstUnOpenedPanel);
                }
                else
                {
                    allGeneratedPanels.Add(typeof(T), new List<IPanel> { firstUnOpenedPanel });
                }

                firstUnOpenedPanel.OnInit();
            }

            //if the panel is a main panel, then close the current main panel but not removing it from the list (just set inactive)
            if (firstUnOpenedPanel.PanelType == PanelType.MainPanel)
            {
                if (parentContainer != this)
                {
                    parentContainer = this;
                    Debug.LogWarning("Main Panel's parent can only be the root panel");
                }

                if (currentMainPanel != null)
                {
                    DoClosePanel(currentMainPanel);
                }

                currentMainPanel = firstUnOpenedPanel;
            }

            DoOpen(firstUnOpenedPanel, parentContainer, message);
            return firstUnOpenedPanel as T;
        }

        private void DoOpen(IPanel panel, IPanelContainer parent, UIMsg msg)
        {
            parent.Children.Add(panel);
            panel.gameObject.transform.SetParent(parent.gameObject.transform);
            panel.gameObject.transform.SetAsLastSibling();
            panel.gameObject.SetActive(true);
            panel.Parent = parent;
            panel.IsOpening = true;
            panel.OnOpen(msg);
            if (parent.IsOnTop())
            {
                StartCoroutine(SelectFocused(panel.DefaultSelectedGameObjectWhenFocused));
                panel.OnFocused();
            }
        }

        /// <summary>
        /// Close the current panel. If the current panel is the main panel, then it will return to the last opened main panel.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="alsoCloseChild"></param>
        public void ClosePanel(IPanel panel, bool alsoCloseChild = true)
        {
            if (!allGeneratedPanels.ContainsKey(panel.GetType()) ||
                !allGeneratedPanels[panel.GetType()].Contains(panel))
            {
                Debug.LogException(
                    new Exception($"The panel to close: {panel.gameObject} does not exist in this UIRoot!"));
            }

            if (panel.PanelType == PanelType.MainPanel)
            {
                if (panel.Parent != null)
                {
                    panel.Parent.Children.Remove(panel);
                    DoClosePanel(panel, alsoCloseChild);
                    IPanel topMainPanel = Children.FindLast((p => p.PanelType == PanelType.MainPanel));
                    if (topMainPanel != null)
                    {
                        Children.Remove(topMainPanel);
                        DoOpen(topMainPanel, this, null);
                        currentMainPanel = topMainPanel;
                    }
                    else
                    {
                        if (defaultMainPanel)
                        {
                            DoOpen(defaultMainPanel.GetComponent<IPanel>(), this, null);
                        }
                    }
                }
            }
            else
            {
                DoClosePanel(panel, alsoCloseChild);
            }

        }

        private void DoClosePanel(IPanel panel, bool alsoCloseChild = true)
        {
            panel.OnClosed();
            panel.IsOpening = false;


            if (panel is IPanelContainer container)
            {
                if (alsoCloseChild)
                {
                    while (container.Children.Count > 0)
                    {
                        DoClosePanel(container.Children[0], true);
                    }
                }
            }

            panel.gameObject.SetActive(false);
            if (panel.Parent != null)
            {
                if (panel.PanelType != PanelType.MainPanel)
                {
                    panel.Parent.Children.Remove(panel);
                }

                //refresh selected game object
                IPanel topParentChild = panel.Parent.GetTopChild();
                if (topParentChild == null)
                {
                    topParentChild = panel.Parent;
                }
                if (topParentChild.IsOnTop())
                {
                    if (topParentChild.DefaultSelectedGameObjectWhenFocused)
                    {
                        StartCoroutine(SelectFocused(topParentChild.DefaultSelectedGameObjectWhenFocused));
                    }
                    topParentChild.OnFocused();
                }
            }
        }

        IEnumerator SelectFocused(GameObject obj)
        {
            if (obj)
            {
                yield return null;
                EventSystem.current.SetSelectedGameObject(obj);
            }
        }

        public IPanelContainer Parent { get; set; } = null;
        public PanelType PanelType { get; } = PanelType.Root;
        public GameObject DefaultSelectedGameObjectWhenFocused { get; } = null;
        public bool IsOpening { get; set; } = true;


        public List<IPanel> Children { get; } = new List<IPanel>();

        public IPanel GetTopChild()
        {
            if (Children.Count <= 0)
            {
                return null;
            }
            return Children[Children.Count - 1];
        }

        private IPanel GetFirstPanel(Type type, bool isOpen)
        {
            if (!allGeneratedPanels.ContainsKey(type) || allGeneratedPanels[type].Count <= 0)
            {
                return null;
            }

            foreach (var panel in allGeneratedPanels[type])
            {
                if (panel.IsOpening == isOpen)
                {
                    return panel;
                }
            }

            return null;
        }

        private void GetPreExistingPanels()
        {
            List<IPanel> allPanels =
                GetComponentsInChildren<IPanel>(true).Where((panel => panel.gameObject != this.gameObject)).ToList();

            allGeneratedPanels = new Dictionary<Type, List<IPanel>>();
            UIManager.Singleton.RegisterPanel(this, this);
            foreach (IPanel panel in allPanels)
            {
                panel.IsOpening = panel.gameObject.activeInHierarchy;
                panel.OnInit();
                if (panel.PanelType == PanelType.MainPanel)
                {
                    if (panel.IsOpening)
                    {
                        if (currentMainPanel == null)
                        {
                            currentMainPanel = panel;
                        }
                        else
                        {
                            panel.gameObject.SetActive(false);
                            panel.IsOpening = false;
                        }
                    }
                }
                if (allGeneratedPanels.ContainsKey(panel.GetType()))
                {
                    allGeneratedPanels[panel.GetType()].Add(panel);
                }
                else
                {
                    allGeneratedPanels.Add(panel.GetType(), new List<IPanel> { panel });
                }
                UIManager.Singleton.RegisterPanel(panel, this);
            }

            if (currentMainPanel == null)
            {
                if (defaultMainPanel)
                {
                    currentMainPanel = defaultMainPanel.GetComponent<IPanel>();
                    DoOpen(defaultMainPanel.GetComponent<IPanel>(), this, null);
                }
            }
            else
            {
                DoOpen(currentMainPanel, this, null);
            }

            if (currentMainPanel != null)
            {
                StartCoroutine(SelectFocused(currentMainPanel.DefaultSelectedGameObjectWhenFocused));
            }
        }

        void IPanel.OnFocused() { }

        void IPanel.OnInit() { }

        void IPanel.OnOpen(UIMsg msg) { }

        void IPanel.OnClosed() { }
    }
}
