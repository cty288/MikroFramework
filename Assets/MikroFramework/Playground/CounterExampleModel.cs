using System.Collections;
using System.Collections.Generic;
using MikroFramework.BindableProperty;
using MikroFramework.Singletons;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class CounterExampleModel: MikroSingleton<CounterExampleModel> {
     
        private CounterExampleModel() { }
        public BindableProperty<int> Count { get; set; } = new BindableProperty<int>() {Value = 0};
    }
}
