using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Playground
{
    public partial class CounterViewController : MonoBehaviour {

        private void Start() {
            addButton.onClick.AddListener(() => {
                new ChangeCounterCommand(1).Execute();
            });

            subtractButton.onClick.AddListener(() => {
                new ChangeCounterCommand(-1).Execute();
            });

            CounterExampleModel.Singleton.Count.RegisterOnValueChaned(newValue => {
                numberText.text = newValue.ToString();
            });

            numberText.text = "0";
        }
    }
}
