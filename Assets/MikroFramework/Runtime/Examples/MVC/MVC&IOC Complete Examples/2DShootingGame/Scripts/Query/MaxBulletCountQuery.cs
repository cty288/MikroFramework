using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using UnityEngine;

public class MaxBulletCountQuery : AbstractQuery<int> {
    private readonly string gunName;

    public MaxBulletCountQuery(string gunName) {
        this.gunName = gunName;
    }

    protected override int OnDo() {
        IGunConfigModel gunConfigModel = this.GetModel<IGunConfigModel>();
        GunConfigItem item = gunConfigModel.GetItemByName(gunName);
        return item.BulletMaxCount;
    }
}
