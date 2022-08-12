using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrderB : MonoBehaviour
{
    public void InitA()
    {
        Debug.Log($"Awake A {gameObject.name}");
    }

    public void InitB()
    {
        Debug.Log($"Awake B {gameObject.name}");
    }
}
