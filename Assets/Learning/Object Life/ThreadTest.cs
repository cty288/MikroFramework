using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadTest : MonoBehaviour {
    private void Start() {
        ThreadStart threadStart = new ThreadStart(ThreadMain);
        Thread thread = new Thread(threadStart);
        thread.Start();
        Debug.Log("Unity Thread ID: " + Thread.CurrentThread.ManagedThreadId.ToString());
    }

    void ThreadMain() {
        Debug.Log("Thread ID: " + Thread.CurrentThread.ManagedThreadId.ToString());
        //运行下面代码会报错，transform只能被主线程调用
        Debug.Log(transform.gameObject.name);
    }
}
