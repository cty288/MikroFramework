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
        //�����������ᱨ��transformֻ�ܱ����̵߳���
        Debug.Log(transform.gameObject.name);
    }
}
