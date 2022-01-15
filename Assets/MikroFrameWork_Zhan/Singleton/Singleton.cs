using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{

    //单例模式  : 全局只允许有一个类的对象
    private static T _instance;
    public static T Instance//属性。property
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

}
