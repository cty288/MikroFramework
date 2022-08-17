using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Utilities
{
    public class TriggerCheck : MonoBehaviour {
        public LayerMask TargetLayers;

        
        private SimpleRC enterRC = new SimpleRC();

        [SerializeField] private float maxDistance = 100;
        [SerializeField] private float detectTime = 1f;

        [SerializeField] private HashSet<Collider> colliders = new HashSet<Collider>();
        /// <summary>
        /// Get all 2D colliders that are in the current trigger of this object
        /// </summary>
        public HashSet<Collider> Colliders => colliders;

        /// <summary>
        /// If there are any collider in the trigger of this object
        /// </summary>
        public bool Triggered
        {
            get { return enterRC.RefCount > 0; }
        }

        private float timer = 0;

        public Action<Collider> OnEnter = (collider) => { };
        public Action<Collider> OnExit = (collider) => { };

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= detectTime)
            {
                List<Collider> removedColliders = new List<Collider>();

                foreach (Collider col in colliders)
                {
                    if (col && Vector2.Distance(col.gameObject.transform.position, transform.position)
                        >= maxDistance) {
                        enterRC.Release();
                        removedColliders.Add(col);
                        OnExit?.Invoke(col);
                    }
                    else if (col && !col.gameObject.activeInHierarchy)
                    {
                        enterRC.Release();
                        removedColliders.Add(col);
                        OnExit?.Invoke(col);
                    }
                }

                foreach (Collider removedCollider in removedColliders)
                {
                    colliders.Remove(removedCollider);
                }
                colliders.RemoveWhere((collider2D1 => collider2D1 == null));

                timer = 0;
            }

        }

        private void OnTriggerStay(Collider other)
        {
            if (PhysicsUtility.IsInLayerMask(other.gameObject, TargetLayers))
            {
                if (colliders.Contains(other)) {
                    return;
                }
                enterRC.Retain();
                colliders.Add(other);
                OnEnter?.Invoke(other);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PhysicsUtility.IsInLayerMask(other.gameObject, TargetLayers))
            {
                enterRC.Retain();
                colliders.Add(other);
                OnEnter?.Invoke(other);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (PhysicsUtility.IsInLayerMask(other.gameObject, TargetLayers))
            {
                //enterRC.Release();
                if (colliders.Contains(other))
                {
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
