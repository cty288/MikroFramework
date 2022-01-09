using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public class AttackPlayer : AbstractMikroController<ShootingEditor2D> {
    public int Damage = 1;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            HurtPlayerCommand command = HurtPlayerCommand.Allocate(1);
            this.SendCommand<HurtPlayerCommand>(command);
        }
    }

}
