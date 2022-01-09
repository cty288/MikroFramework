using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public class ShiftGunCommand : AbstractCommand<ShiftGunCommand> {
    protected override void OnExecute() {
        this.GetSystem<IGunSystem>().ShiftGun();
    }
}
