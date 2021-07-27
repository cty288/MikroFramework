using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using UnityEngine;

namespace MikroFramework.Examples.CounterApp{


    public interface IAchievementSystem : ISystem {

    }

    public class AchievementSystem : AbstractSystem, IAchievementSystem {


        protected override void OnInit() {
            var counterModel = this.GetModel<ICounterModel>();

            var prevCount = counterModel.Count.Value;


            counterModel.Count.RegisterOnValueChaned(newCount => {
                if (prevCount < 10 && newCount >= 10) {
                    Debug.Log("Unlock click 10 times achievement");
                }
                else if (prevCount < 20 && newCount >= 20) {
                    Debug.Log("Unlock click 20 times achievement");
                }

                prevCount = newCount;
            });
        }
    }
}