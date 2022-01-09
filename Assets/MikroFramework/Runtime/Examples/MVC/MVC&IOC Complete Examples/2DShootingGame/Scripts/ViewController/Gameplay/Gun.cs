using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using UnityEngine;

public class Gun : AbstractMikroController<ShootingEditor2D> {

    private Bullet bullet;

    //private IGunSystem gunSystem;
    private GunInfo gunInfo;

    private int maxBulletCount;
    private void Start() {
        bullet = transform.Find("Bullet").GetComponent<Bullet>();
        gunInfo = this.GetSystem<IGunSystem>().CurrentGun;
        maxBulletCount = this.SendQuery(new MaxBulletCountQuery(gunInfo.Name.Value));
        GameObjectPoolManager.Singleton.CreatePool(bullet.gameObject, 10, 50);
    }

    public void Shoot() {
        if (gunInfo.BulletCountInGun.Value > 0 && gunInfo.State.Value == GunInfo.GunState.Idle) {
            Bullet bulletInstance = GameObjectPoolManager.Singleton.Allocate(bullet.gameObject)
                .GetComponent<Bullet>();
            bulletInstance.gameObject.transform.position = bullet.transform.position;
            bulletInstance.gameObject.transform.rotation = bullet.transform.rotation;
            bulletInstance.transform.localScale = bullet.transform.lossyScale; //global scale
            bulletInstance.gameObject.SetActive(true);

            this.SendCommand<ShootCommand>();
        }

        
    }

    public void Reload() {
        if (gunInfo.BulletCountInGun.Value < maxBulletCount &&
            gunInfo.BulletCountOutGun.Value > 0 && gunInfo.State.Value == GunInfo.GunState.Idle) {
            this.SendCommand<ReloadCommand>();
        }
    }

    private void OnDestroy() {
        gunInfo = null;
    }
}
