using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mikrocosmos
{
    //[ExecuteInEditMode]
    public class UIParticle : MonoBehaviour
    {
        private List<ScaleData> scaleDatas = null;
        [SerializeField]
        private Camera camera;
        
        
        void Awake()
        {
            if (!camera) {
                camera = Camera.main;
            }
            scaleDatas = new List<ScaleData>();
            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>(true)) {
                scaleDatas.Add(new ScaleData() {
                    transform = p.transform, beginScale = p.transform.localScale, GravityScale = p.gravityModifier / camera.orthographicSize,
                    ParticleSystem = p
                });
            }
        }

        void Start()
        {
            float designWidth = 1920;
            float designHeight = 1080;
            float designScale = designWidth / designHeight;
            float scaleRate = (float)Screen.width / (float)Screen.height;

            foreach (ScaleData scale in scaleDatas) {
                if (scale.transform != null) {
                    scale.ParticleSystem.gravityModifier = scale.GravityScale * camera.orthographicSize;
                    if (scaleRate < designScale)
                    {
                        float scaleFactor = scaleRate / designScale;
                        scale.transform.localScale = scale.beginScale * scaleFactor;
                    }
                    else
                    {
                        scale.transform.localScale = scale.beginScale;
                    }
                }
            }
        }

        private float lastwidth = 0f;
        private float lastheight = 0f;
        private float lastCameraSize = 0f;

        void Update () {
            if (Camera.main) {
                if (lastwidth != Screen.width || lastheight != Screen.height || lastCameraSize != camera.orthographicSize)
                {
                    lastwidth = Screen.width;
                    lastheight = Screen.height;
                    lastCameraSize = camera.orthographicSize;
                    Start();
                }
            }
        
           
        }

        class ScaleData
        {
            public Transform transform;
            public Vector3 beginScale = Vector3.one;
            public float GravityScale;
            public ParticleSystem ParticleSystem;
        }
    }
}
