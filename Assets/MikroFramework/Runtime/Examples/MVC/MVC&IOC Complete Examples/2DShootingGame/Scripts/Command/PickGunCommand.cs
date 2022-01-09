using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.Pool;
using UnityEngine;

public class PickGunCommand : AbstractCommand<PickGunCommand> {
    private  string name;
    private  int bulletInGun;
    private  int bulletOutGun;

    public static PickGunCommand Allocate(string name, int bulletInGun, int bulletOutGun) {
        PickGunCommand pickGunCommand = SafeObjectPool<PickGunCommand>.Singleton.Allocate();
        pickGunCommand.name = name;
        pickGunCommand.bulletInGun = bulletInGun;
        pickGunCommand.bulletOutGun = bulletOutGun;
        return pickGunCommand;
    }

    
    protected override void OnExecute() {
        this.GetSystem<IGunSystem>().PickGun(name,bulletInGun,bulletOutGun);
    }
}
