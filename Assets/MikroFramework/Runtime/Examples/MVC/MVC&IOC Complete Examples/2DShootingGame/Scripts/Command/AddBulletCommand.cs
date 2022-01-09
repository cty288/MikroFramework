using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public class AddBulletCommand : AbstractCommand<AddBulletCommand>
{
    protected override void OnExecute() {
        IGunSystem gunSystem = this.GetSystem<IGunSystem>();
        IGunConfigModel gunConfigModel = this.GetModel<IGunConfigModel>();

        AddBullet(gunSystem.CurrentGun,gunConfigModel);

        foreach (GunInfo gunInfo in gunSystem.GunInfos) {
            AddBullet(gunInfo, gunConfigModel);
        }
    }

    void AddBullet(GunInfo gunInfo, IGunConfigModel gunConfig) {
        GunConfigItem gunConfigItem = gunConfig.GetItemByName(gunInfo.Name.Value);

        if (!gunConfigItem.NeedBullet) {
            gunInfo.BulletCountInGun.Value = gunConfigItem.BulletMaxCount;
        }
        else {
            gunInfo.BulletCountOutGun.Value += gunConfigItem.BulletMaxCount;
        }
    }
}
