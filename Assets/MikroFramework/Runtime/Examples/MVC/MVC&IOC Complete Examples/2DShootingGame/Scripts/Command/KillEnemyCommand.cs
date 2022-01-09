using System.Collections;
using System.Collections.Generic;
using MikroFramework.ActionKit;
using MikroFramework.Architecture;
using MikroFramework.Pool;
using UnityEngine;


//use mikroaction as command
public class KillEnemyCommand : MikroAction {
    protected override void OnExecuting() {
        this.GetSystem<IStatSystem>().KillCount.Value++;

        int randomIndex = Random.Range(0, 100);
        if (randomIndex < 80) {
            this.GetSystem<IGunSystem>().CurrentGun.BulletCountInGun.Value += Random.Range(1, 4);
        }

        Finished.Value = true;
    }

    protected override void RecycleBackToPool() {
        SafeObjectPool<KillEnemyCommand>.Singleton.Recycle(this);
    }
}
