using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.TimeSystem;
using UnityEngine;

public class ShootingEditor2D : Architecture<ShootingEditor2D> {
    protected override void Init() {
        this.RegisterModel<IPlayerModel>(new PlayerModel());
        this.RegisterSystem<IStatSystem>(new StatSystem());
        this.RegisterSystem<IGunSystem>(new GunSystem());
        this.RegisterSystem<ITimeSystem>(new TimeSystem());
        this.RegisterModel<IGunConfigModel>(new GunConfigModel());
    }
}
