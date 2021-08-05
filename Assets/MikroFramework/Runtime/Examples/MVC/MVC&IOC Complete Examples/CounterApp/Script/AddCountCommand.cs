using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using UnityEngine;

namespace MikroFramework.Examples.CounterApp {
    public class AddCountCommand : AbstractCommand
    {

        protected override void OnExecute(params object[] parameters)
        {
            this.GetModel<ICounterModel>().Count.Value++;
        }
    }

}
