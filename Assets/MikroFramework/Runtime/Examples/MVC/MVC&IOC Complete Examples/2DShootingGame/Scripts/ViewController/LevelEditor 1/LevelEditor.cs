using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using MikroFramework.Utilities;
using UnityEngine;

public class LevelEditor : MonoBehaviour {

    public enum OperateMode {
        Draw,
        Erase
    }

    public enum BrushType {
        Ground,
        Hero
    }

    private OperateMode currentOperateMode = OperateMode.Draw;
    private BrushType currentBushBrushType = BrushType.Ground;

    private GameObject currentObjMouseOn;
    private readonly Lazy<GUIStyle> modeLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label) {
        fontSize = 30,
        alignment = TextAnchor.MiddleCenter
    });

    private readonly Lazy<GUIStyle> buttonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button) {
        fontSize = 30,
    });

    private readonly Lazy<GUIStyle> rightButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button) {
        fontSize = 25,
    });

    private void OnGUI() {
        var modeLabelRect = RectHelper.RectForAnchorCenter(Screen.width / 2, 35, 200, 50);
        if (currentOperateMode == OperateMode.Draw) {
            GUI.Label(modeLabelRect, currentOperateMode + ":" + currentBushBrushType.ToString(), modeLabelStyle.Value);
        }
        else
        {
            GUI.Label(modeLabelRect, currentOperateMode.ToString(), modeLabelStyle.Value);
        }
       

        Rect drawButtonRect = new Rect(10, 10, 150, 40);


        if (GUI.Button(drawButtonRect, "Draw", buttonStyle.Value)) {
            currentOperateMode = OperateMode.Draw;
        }

        Rect eraseButtonRect = new Rect(10, 60, 150, 40);
        if (GUI.Button(eraseButtonRect, "Eraser", buttonStyle.Value)) {
            currentOperateMode = OperateMode.Erase;
        }

        if (currentOperateMode == OperateMode.Draw) {
            Rect groundButtonRect = new Rect(Screen.width - 110, 10, 100, 40);
            if (GUI.Button(groundButtonRect, "Ground", rightButtonStyle.Value)) {
                currentBushBrushType = BrushType.Ground;
            }

            Rect heroButtonRect = new Rect(Screen.width - 110, 60, 100, 40);
            if (GUI.Button(heroButtonRect, "Hero", rightButtonStyle.Value)) {
                currentBushBrushType = BrushType.Hero;
            }
        }

        Rect saveButtonRect = new Rect(Screen.width - 110, Screen.height - 50, 100, 40);
        if (GUI.Button(saveButtonRect, "Save", rightButtonStyle.Value)) {
            Debug.Log("Save");
            List<LevelItemInfo> infos = new List<LevelItemInfo>(transform.childCount);

            foreach (Transform child in transform) {
                infos.Add(new LevelItemInfo() {
                    X = child.position.x,
                    Y = child.position.y,
                    Name = child.name,
                });
                Debug.Log($"Name: {child.name}, X: {child.position.x}, Y: {child.position.y}");
            }

            XmlDocument document = new XmlDocument();
            var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", "");
            document.AppendChild(declaration);

            var level = document.CreateElement("Level");
            document.AppendChild(level);

            foreach (LevelItemInfo levelItemInfo in infos) {
                var levelItem = document.CreateElement("LevelItem");
                levelItem.SetAttribute("name", levelItemInfo.Name);
                levelItem.SetAttribute("x", levelItemInfo.X.ToString());
                levelItem.SetAttribute("y", levelItemInfo.Y.ToString());
                level.AppendChild(levelItem);
            }


            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xmlWriter = new XmlTextWriter(sw);
            xmlWriter.Formatting = Formatting.Indented;
            document.WriteTo(xmlWriter);
            Debug.Log(sb.ToString());

            string levelFileFolder = Application.persistentDataPath + "/LevelFiles";
            Debug.Log(levelFileFolder);
            if (!Directory.Exists(levelFileFolder))
            {
                Directory.CreateDirectory(levelFileFolder);
            }

            string levelFilePath = levelFileFolder + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            document.Save(levelFilePath);
        }

      
        
    }

    class LevelItemInfo {
        public float X;
        public float Y;
        public string Name;
    }

    public SpriteRenderer EmptyHighlight;



    private bool canDraw;
    private void Update() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        worldMousePosition.x = Mathf.Floor(worldMousePosition.x + 0.5f);
        worldMousePosition.y = Mathf.Floor(worldMousePosition.y + 0.5f);
        
        
        
        if (Mathf.Abs(EmptyHighlight.transform.position.x - worldMousePosition.x) < 0.1f &&
            Mathf.Abs(EmptyHighlight.transform.position.y - mousePosition.y) < 0.1f) {

        }
        else {
            Vector3 highlightPos = worldMousePosition;
            highlightPos.z = -9;

            EmptyHighlight.transform.position = highlightPos;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero, 20);
            if (hit.collider) {
                if (currentOperateMode == OperateMode.Draw) {
                    EmptyHighlight.color = new Color(1, 0, 0, 0.5f);
                }
                else {
                    EmptyHighlight.color = new Color(1, 0.5f, 0, 0.5f);
                }

                currentObjMouseOn = hit.collider.gameObject;
                canDraw = false;
            }
            else {
                if (currentOperateMode == OperateMode.Draw) {
                    EmptyHighlight.color = new Color(1, 1, 1, 0.5f);
                }
                else
                {
                    EmptyHighlight.color = new Color(0, 0, 1, 0.5f);
                }

                currentObjMouseOn = null;
                canDraw = true;
            }
        }


        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && GUIUtility.hotControl == 0) {
            if (canDraw && currentOperateMode == OperateMode.Draw) {

                if (currentBushBrushType == BrushType.Ground) {
                    GameObject groundPrefab = Resources.Load<GameObject>("Ground");
                    GameObject groundObj = Instantiate(groundPrefab, transform);
                    groundObj.transform.position = worldMousePosition;
                    groundObj.name = "Ground";
                    canDraw = false;
                }
                else if (currentBushBrushType == BrushType.Hero) {
                    GameObject groundPrefab = Resources.Load<GameObject>("Ground");
                    GameObject groundObj = Instantiate(groundPrefab, transform);
                    groundObj.transform.position = worldMousePosition;
                    groundObj.name = "Player";

                    groundObj.GetComponent<SpriteRenderer>().color = Color.cyan;
                    canDraw = false;
                }
               
            }else if (currentObjMouseOn && currentOperateMode == OperateMode.Erase) {
                Destroy(currentObjMouseOn);
                currentObjMouseOn = null;
            }
           
        }
    }
}
