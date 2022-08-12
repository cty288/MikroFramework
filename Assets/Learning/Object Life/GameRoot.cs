using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Awake()
    {
        Debug.Log($"Awake: {gameObject.name}");
    }
    /*
    // Update is called once per frame
    void Start()
    {
        Debug.Log($"Start: {gameObject.name}");
    }


    private int count = 0;
    private void FixedUpdate() {
        count++;
       // Debug.Log($"Fixed Update {count}");
    }

    private float sum = 0;
    private void Update() {
        //Debug.Log($"Update: {gameObject.name}");
        sum += Time.deltaTime;
        if (sum + Time.deltaTime >= 1)
        {
            sum -= 1;
           // Debug.Log($"Update +++++++++++++: {gameObject.name}");
        }
    }

    private void OnEnable() {
        Debug.Log($"Enable: {gameObject.name}");
    }

    private void OnDisable() {
        Debug.Log($"Disable: {gameObject.name}");
    }*/


    private void FixedUpdate() {
       // Debug.Log("Fixed: " + Time.realtimeSinceStartup);
    }

    private void Start() {
        TestOrder toa = GetComponent<TestOrder>();
        TestOrderB tob = GetComponent<TestOrderB>();
        
        toa.InitA();
        toa.InitB();
    }
}
