using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Utilities
{
    public class Trigger2DCheck : MonoBehaviour
    {
        public LayerMask TargetLayers;

        [SerializeField] private float maxDistance = 100;
        [SerializeField] private float detectTime = 1f;

        private float timer;

        private SimpleRC enterRC = new SimpleRC();

        [SerializeField] private HashSet<Collider2D> colliders = new HashSet<Collider2D>();
        /// <summary>
        /// Get all 2D colliders that are in the current trigger of this object
        /// </summary>
        public HashSet<Collider2D> Colliders => colliders;

        /// <summary>
        /// If there are any collider in the trigger of this object
        /// </summary>
        public bool Triggered
        {
            get { return enterRC.RefCount > 0; }
        }
        public Action<Collider2D> OnEnter = (collider) => { };
        public Action<Collider2D> OnExit = (collider) => { };

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= detectTime)
            {
                List<Collider2D> removedColliders = new List<Collider2D>();

                foreach (Collider2D col in colliders) {
                    if (col && Vector2.Distance(col.gameObject.transform.position, transform.position)
                        >= maxDistance) {

                        enterRC.Release();
                        removedColliders.Add(col);
                        OnExit?.Invoke(col);
                    }
                    else if (col && !col.gameObject.activeInHierarchy) {
                        enterRC.Release();
                        removedColliders.Add(col);
                        OnExit?.Invoke(col);
                    }
                }
              
                foreach (Collider2D removedCollider in removedColliders) {
                    colliders.Remove(removedCollider);
                }
                colliders.RemoveWhere((collider2D1 => collider2D1 == null));

                timer = 0;
            }

        }

        private void OnTriggerStay2D(Collider2D other)
        {

            if (PhysicsUtility.IsInLayerMask(other.gameObject, TargetLayers))
            {
                if (colliders.Contains(other))
                {
                    return;
                }
                enterRC.Retain();
                colliders.Add(other);
                OnEnter?.Invoke(other);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (PhysicsUtility.IsInLayerMask(other.gameObject, TargetLayers))
            {
                enterRC.Retain();
                colliders.Add(other);
                OnEnter?.Invoke(other);
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (PhysicsUtility.IsInLayerMask(other.gameObject, TargetLayers))
            {
                //enterRC.Release();
                if (colliders.Contains(other)) {
                    enterRC.Release();
                    colliders.Remove(other);
                    OnExit?.Invoke(other);
                }
            }

        }

        public void Clear()
        {
            enterRC = new SimpleRC();
            colliders.Clear();

        }
    }
}