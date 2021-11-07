using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Playground
{
    public partial class CounterViewController : MonoBehaviour {
        private CounterExampleModel model = new CounterExampleModel();

        private void Start() {
            addButton.onClick.AddListener(() => {
                model.Count++;
            });

            subtractButton.onClick.AddListener(() => {
                model.Count--;
            });

            numberText.text = "0";
        }

        private void Update() {
            numberText.text = model.Count.ToString();
        }
    }
}
