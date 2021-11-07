using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework.Playground
{
    public partial class CounterViewController : MonoBehaviour {
        //view
        private Button addButton;
        private Button subtractButton;
        private Text numberText;

        private void Awake() {
            addButton = transform.Find("AddButton").GetComponent<Button>();
            subtractButton = transform.Find("SubtractButton").GetComponent<Button>();
            numberText = transform.Find("Number").GetComponent<Text>();
        }
    }
}
