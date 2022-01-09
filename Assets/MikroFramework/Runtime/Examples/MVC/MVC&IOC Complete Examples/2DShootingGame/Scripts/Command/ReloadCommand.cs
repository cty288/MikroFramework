using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.TimeSystem;
using UnityEngine;

public class ReloadCommand : AbstractCommand<ReloadCommand> {
    protected override void OnExecute() {
        GunInfo currentGun = this.GetSystem<IGunSystem>().CurrentGun;
        GunConfigItem gunConfigItem = this.GetModel<IGunConfigModel>().GetItemByName(
            currentGun.Name.Value);

        int needBulletCount = gunConfigItem.BulletMaxCount - currentGun.BulletCountInGun.Value;

        if (needBulletCount > 0) {
            if (currentGun.BulletCountOutGun.Value > 0) {
                currentGun.State.Value = GunInfo.GunState.Reload;

                this.GetSystem<ITimeSystem>().AddDelayTask(gunConfigItem.ReloadSeconds, () => {
                    if (currentGun.BulletCountOutGun.Value >= needBulletCount) {
                        currentGun.BulletCountOutGun.Value -= needBulletCount;
                        currentGun.BulletCountInGun.Value += needBulletCount;
                    }
                    else {
                        currentGun.BulletCountInGun.Value += currentGun.BulletCountOutGun.Value;
                        currentGun.BulletCountOutGun.Value = 0;
                    }

                    currentGun.State.Value = GunInfo.GunState.Idle;
                });
            }

               
        }

        
    }
}
