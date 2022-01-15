using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    protected static bool isGlobal = true;

    private void Awake()
    {
        if(_instance != null)
            Destroy(this.gameObject);
    }

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                T[] singletons = FindObjectsOfType<T>();
                if (singletons.Length > 1)
                {
                    Debug.LogWarning("Singleton of type " + typeof(T).Name + " should never be more than one in scene");
                    for (int i = 0, c = singletons.Length; i < c - 1; i++)
                    {
                        Destroy(singletons[1].gameObject);
                    }
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject go = GameObject.Find("ManagerObjs");
                    if (go == null)
                    {
                        GameObject singletonGO = new GameObject();
                        singletonGO.name = "ManagerObjs";
                    }
                    GameObject childSingleton = new GameObject();
                    childSingleton.name = "(singleton)" + typeof(T).Name;
                    childSingleton.transform.SetParent(go.transform);
                    _instance = childSingleton.AddComponent<T>();

                    if (isGlobal && Application.isPlaying)
                    {
                        DontDestroyOnLoad(childSingleton);
                    }
                    return _instance;
                }
            }
            return _instance;
        }
    }

}
