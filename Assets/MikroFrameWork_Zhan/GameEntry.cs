using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll(); 
        DontDestroyOnLoad(this.gameObject);
        AudioManager.Instance.Init();
        UIManager.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitEvent()
    {

        EventDispatcher.Inner.AddEventListener("FoundLake", OnFoundLake);
        EventDispatcher.Inner.AddEventListener("GameOver", OnGameOver);
    }

    private void OnFoundLake(object[] data)
    {
        UIManager.Instance.Push<UIMessageInGame>(UIDepthConst.TopDepth, false, "The road to the lake can allow pass now!", 5f);
    }

    private void OnGameOver(object[] data)
    {
        UIManager.Instance.Push<UIScreenCG>(UIDepthConst.TopDepth, false, "Maybe you have already realized. This world is only an imagination, a mirror of the real world. I created this world to guide those Frozen hearts, those who isolate themselves in this icy world, and those who are afraid to communicate with others just because of language barrier and culture difference. Someday you might realize that this world is not that complicated as you imgained. Unfreeze your heart and take your first step.             -- Aki, the creator of this world", 2f, 2.0f);
    }
}
