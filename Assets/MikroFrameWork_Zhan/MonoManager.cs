using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoManager : MonoSingleton<MonoManager>{

    public new static void Instantiate(Object obj)
    {
        Object.Instantiate(obj);
    }

    public new static void Destroy(Object obj)
    {
        Object.Destroy(obj);
    }

    public new void StartCoroutine(IEnumerator corou)
    {
        base.StartCoroutine(corou);
    }
}
