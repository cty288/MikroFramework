using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public class FullBulletCommand : AbstractCommand<FullBulletCommand>
{
    protected override void OnExecute() {
        IGunSystem gunSystem = this.GetSystem<IGunSystem>();
        IGunConfigModel gunConfigModel = this.GetModel<IGunConfigModel>();

        gunSystem.CurrentGun.BulletCountInGun.Value = gunConfigModel.GetItemByName(
            gunSystem.CurrentGun.Name.Value).BulletMaxCount;

        foreach (GunInfo gunInfo in gunSystem.GunInfos) {
            gunInfo.BulletCountInGun.Value = gunConfigModel.GetItemByName(
                gunInfo.Name.Value).BulletMaxCount;
        }
    }
}
