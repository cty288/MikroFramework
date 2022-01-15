using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher 
{
    //数据层要先于显示层更新
    private static EventDispatcher _inner;
    public static EventDispatcher Inner
    {
        get
        {
            if (_inner == null)
                _inner = new EventDispatcher();
            return _inner;
        }
    }

    private static EventDispatcher _outer;
    public static EventDispatcher Outer
    {
        get
        {
            if (_outer == null)
                _outer = new EventDispatcher();
            return _outer;
        }
    }


    public Dictionary<string, List<Action<object[]>>> eventPool = new Dictionary<string, List<Action<object[]>>>();

    //分发事件
    public void DispatchEvent(string eventName, params object[] data)
    {
        if (eventPool.ContainsKey(eventName))
        {
            for (int i = 0; i < eventPool[eventName].Count; i++)
            {
                eventPool[eventName][i].Invoke(data);
            }
        }
    }

    //添加监听
    public void AddEventListener(string eventName, Action<object[]> action)
    {
        if (eventPool.ContainsKey(eventName))
        {
            eventPool[eventName].Add(action);
        }
        else
        {
            eventPool.Add(eventName, new List<Action<object[]>>() { action });
        }
    }

    //移除监听
    public void RemoveListener(string eventName, Action<object[]> action)
    {
        if (eventPool.ContainsKey(eventName))
        {
            eventPool[eventName].Remove(action);
        }
    }

    //移除所有监听
    public void RemoveAllListener(string eventName)
    {
        if (eventPool.ContainsKey(eventName))
        {
            eventPool[eventName].Clear();
        }
    }
}


