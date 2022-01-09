using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

public class LevelPlayer : MonoBehaviour {
    enum PlayerState {
        SelectLevelFile,
        Playing
    }

    private PlayerState currentLevelFile = PlayerState.SelectLevelFile;
    private string levelFilesFolder;



  
    private void Awake() {
        levelFilesFolder = Application.persistentDataPath + "/LevelFiles";
    }

    void ParseAndRun(string xml) {
        XmlDocument document = new XmlDocument();
        document.LoadXml(xml);

        var levelNode = document.SelectSingleNode("Level");

        foreach (XmlElement levelItemNode in levelNode.ChildNodes) {
            string levelItemName = levelItemNode.Attributes["name"].Value;
            int levelItemX = int.Parse(levelItemNode.Attributes["x"].Value);
            int levelItemY = int.Parse(levelItemNode.Attributes["y"].Value);

            GameObject levelItemPrefab = Resources.Load<GameObject>(levelItemName);
            GameObject levelItemObj = Instantiate(levelItemPrefab, transform);
            levelItemObj.transform.position = new Vector3(levelItemX, levelItemY, 0);
        }
    }

    private void OnGUI() {
        if (currentLevelFile == PlayerState.SelectLevelFile) {
            int y = 10;
            foreach (string filePath in Directory.GetFiles(levelFilesFolder).Where(f=>f.EndsWith("xml"))) {
                string fileName = Path.GetFileName(filePath);
                if (GUI.Button(new Rect(10, y, 150, 40), fileName)) {
                    var xml = File.ReadAllText(filePath);
                    ParseAndRun(xml);
                    currentLevelFile = PlayerState.Playing;
                }

                y += 50;
            }
        }
    }

   
}
