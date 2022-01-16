using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class OrthographicCameraScaler : MonoBehaviour
    {
        [SerializeField] private float fixedWidth = 10;

        private Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            camera.orthographicSize = fixedWidth / 2 / camera.aspect;
        }
    }
}
