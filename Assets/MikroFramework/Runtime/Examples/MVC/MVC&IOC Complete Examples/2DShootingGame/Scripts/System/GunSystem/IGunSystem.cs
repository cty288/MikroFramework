using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MikroFramework.Architecture;
using MikroFramework.BindableProperty;
using UnityEngine;

public interface IGunSystem:ISystem {
    GunInfo CurrentGun { get; }

    Queue<GunInfo> GunInfos { get; }

    void PickGun(string name, int bulletCountInGun, int bulletCountOutGun);

    void ShiftGun();
}

public class OnCurrentGunChanged {
    public string Name { get; set; }
}


public class GunSystem : AbstractSystem, IGunSystem {
    private Queue<GunInfo> gunInfos = new Queue<GunInfo>();

    protected override void OnInit() {
        
    }

    public GunInfo CurrentGun { get; } = new GunInfo() {
        BulletCountInGun = new BindableProperty<int>() {Value = 3},

        BulletCountOutGun = new BindableProperty<int>() {
            Value = 5
        },
        Name = new BindableProperty<string>() {
            Value = "Pistol"
        },
        
        State = new BindableProperty<GunInfo.GunState>() {
            Value = GunInfo.GunState.Idle
        }
    };
    public Queue<GunInfo> GunInfos {
        get {
            return gunInfos;
        }
    }

    public void PickGun(string name, int bulletCountInGun, int bulletCountOutGun) {
        if (CurrentGun.Name.Value == name) {
            CurrentGun.BulletCountOutGun.Value += bulletCountOutGun;
            CurrentGun.BulletCountInGun.Value += bulletCountInGun;
        }else if (gunInfos.Any(info => info.Name.Value == name)) {
            GunInfo gunInfo = gunInfos.First(info => info.Name.Value == name);
            gunInfo.BulletCountOutGun.Value += bulletCountOutGun;
            gunInfo.BulletCountInGun.Value += bulletCountInGun;
        }
        else {
            EnqueueCurrentGun(name,bulletCountInGun,bulletCountOutGun);
        }
    }

    private void EnqueueCurrentGun(string nextGunName, int nextGunBulletCountInGun, int nextGunBulletCountOutGun) {
        GunInfo currentGunInfo = new GunInfo()
        {
            Name = new BindableProperty<string>() { Value = CurrentGun.Name.Value },
            BulletCountInGun = new BindableProperty<int>() { Value = CurrentGun.BulletCountInGun.Value },
            BulletCountOutGun = new BindableProperty<int>() { Value = CurrentGun.BulletCountOutGun.Value },
            State = new BindableProperty<GunInfo.GunState>() { Value = CurrentGun.State.Value }
        };

        gunInfos.Enqueue(currentGunInfo);

        CurrentGun.Name.Value = nextGunName;
        CurrentGun.BulletCountOutGun.Value = nextGunBulletCountInGun;
        CurrentGun.BulletCountInGun.Value = nextGunBulletCountOutGun;

        CurrentGun.State.Value = GunInfo.GunState.Idle;

        this.SendEvent(new OnCurrentGunChanged()
        {
            Name = nextGunName
        });
    }

    public void ShiftGun() {
        if (gunInfos.Count > 0) {
            GunInfo nextGunInfo = gunInfos.Dequeue();
            EnqueueCurrentGun(nextGunInfo.Name.Value,nextGunInfo.BulletCountInGun.Value,
                nextGunInfo.BulletCountOutGun.Value);
        }
    }
}
