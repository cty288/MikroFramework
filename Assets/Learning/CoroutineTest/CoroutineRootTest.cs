using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRootTest : MonoBehaviour {
    private IEnumerator ima;
    private void Start() { 
        //StartCoroutine(FuncA());
        ima = FuncA();
        //StartCoroutine(ima);
         StartCoroutine(FuncA());
    }

    private IEnumerator FuncA() {
        Debug.Log("Coroutine Log 1");
        var request =  Resources.LoadAsync<AudioClip>("Lose");
        yield return request;
        Debug.Log($"Load Done: {request.asset as AudioClip}");
        if (request.asset != null) {
            AudioSource.PlayClipAtPoint(request.asset as AudioClip, Vector3.zero);
            
        }
    }

  
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StopCoroutine(FuncA());
        }
    }
}
