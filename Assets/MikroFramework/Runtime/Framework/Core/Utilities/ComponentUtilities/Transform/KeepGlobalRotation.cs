using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mikrocosmos{
    public class KeepGlobalRotation : MonoBehaviour{
        [SerializeField]
        private Vector3 rotation;

        [SerializeField] private Transform positionRelativeTo;

        [SerializeField]
        private Vector3 positionOffset;
        private void OnEnable() {
            //positionOffset = positionRelativeTo.position - transform.position;
        }

        private void Update() {
            transform.rotation = Quaternion.Euler(rotation);
            if (positionRelativeTo) {
                transform.position = positionRelativeTo.position + positionOffset;
            }
         
        }
    }
}
