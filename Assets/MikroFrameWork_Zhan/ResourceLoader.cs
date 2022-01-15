using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    //~~~~~~~~~~~~~~~~~~~~同步~~~~~~~~~~~~~~~~~~~~~~
    public T Load<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        return res;
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        T[] res = Resources.LoadAll<T>(path);
        return res;
    }


    //~~~~~~~~~~~~~~~~~~~~异步~~~~~~~~~~~~~~~~~~~~~~
    public void LoadAsync<T>(string path, System.Action<T> callback) where T : Object
    {
        MonoManager.Instance.StartCoroutine(LoadAsyncCorou<T>(path, callback));
    }

    private IEnumerator LoadAsyncCorou<T>(string path, System.Action<T> callback) where T : Object
    {
        ResourceRequest res = Resources.LoadAsync<T>(path);
        yield return res;
        Debug.Log("异步加载资源完成");
        callback(res.asset as T);
    }
}
