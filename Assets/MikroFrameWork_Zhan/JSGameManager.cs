using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSGameManager : MonoSingleton<JSGameManager> {

    public static int currentLevelID;

    public void LevelStart()
    {
        StartCoroutine(Timer(120f));
    }

    private IEnumerator Timer(float t)
    {
        float tmp = t;
        while(tmp > 0)
        {
            yield return null;
            tmp -= Time.deltaTime;
            EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_UPDATETIMELEFT, tmp);
        }
        print("GameEnd");
    }
}
