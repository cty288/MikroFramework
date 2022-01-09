using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    public string LevelName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(LevelName);
        }
    }
}
