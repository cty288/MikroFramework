using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using MikroFramework.BindableProperty;
using UnityEngine;
using UnityEngine.UI;

namespace MikroFramework.Examples.CounterApp {

    public class CounterViewController : MonoBehaviour, IController {
        private ICounterModel counterModel;


        private void Start() {
            counterModel = this.GetModel<ICounterModel>();

            counterModel.Count.RegisterOnValueChaned(OnCountChanged);

            transform.Find("AddButton").GetComponent<Button>().onClick.AddListener(() => {
                this.SendCommand<AddCountCommand>();
            });

            transform.Find("SubtractButton").GetComponent<Button>().onClick.AddListener(() => {
                this.SendCommand<SubtractCount>();
            });

            OnCountChanged(counterModel.Count.Value);
        }

        private void OnCountChanged<T>(T newCount) {
            transform.Find("Text").GetComponent<Text>().text = newCount.ToString();
        }

        private void OnDestroy() {
            counterModel.Count.UnRegisterOnValueChanged(OnCountChanged);
        }

        IArchitecture IBelongToArchitecture.GetArchitecture() {
            return CounterApp.Interface;
        }
    }

    public class CounterModel : AbstractModel, ICounterModel {

        public BindableProperty<int> Count { get; } = new BindableProperty<int>() {
            Value = 0
        };


        protected override void OnInit() {
            IStorage storage = this.GetUtility<IStorage>();

            Count.Value = storage.LoadInt("Counter_Count");

            Count.RegisterOnValueChaned(count => { storage.SaveInt("Counter_Count", count); });
        }
    }




    public interface ICounterModel : IModel {
        BindableProperty<int> Count { get; }
    }


}