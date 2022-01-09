using System.Collections;
using System.Collections.Generic;using MikroFramework.Architecture;
using MikroFramework.BindableProperty;
using UnityEngine;

public interface IStatSystem:ISystem {
    BindableProperty<int> KillCount { get; }
}

public class StatSystem :AbstractSystem, IStatSystem {
    protected override void OnInit() {
        
    }

    public BindableProperty<int> KillCount { get; } = new BindableProperty<int>();
}
