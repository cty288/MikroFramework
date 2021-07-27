using System.Collections;
using System.Collections.Generic;
using MikroFramework.ResKit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MikroFramework
{
    public class Test : MonoBehaviour
    {
        
        void Start() {
            ResLoader resLoader = new ResLoader();

            resLoader.LoadSync<Texture2D>("mftest", "Asteroid_1");

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
