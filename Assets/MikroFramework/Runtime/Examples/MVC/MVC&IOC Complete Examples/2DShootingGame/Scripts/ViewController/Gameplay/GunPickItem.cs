using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public class GunPickItem : AbstractMikroController<ShootingEditor2D> {
    public string Name;
    public int BulletCountInGun;
    public int BulletCountOutGun;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            this.SendCommand(PickGunCommand.Allocate(Name, BulletCountInGun,BulletCountOutGun));
        }
    }
}
