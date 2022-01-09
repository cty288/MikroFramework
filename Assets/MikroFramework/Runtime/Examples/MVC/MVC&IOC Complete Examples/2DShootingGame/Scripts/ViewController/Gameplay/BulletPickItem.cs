using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public class BulletPickItem : AbstractMikroController<ShootingEditor2D> {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            this.SendCommand<AddBulletCommand>();
            Destroy(this.gameObject);
        }
    }
}
