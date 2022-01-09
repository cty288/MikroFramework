using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Architecture;
using MikroFramework.TimeSystem;

public class ShootCommand:AbstractCommand<ShootCommand> {
    protected override bool AutoRecycle { get; } = false;

    protected override void OnExecute() {
        IGunSystem gunSystem = this.GetSystem<IGunSystem>();
        
        gunSystem.CurrentGun.BulletCountInGun.Value--;
        gunSystem.CurrentGun.State.Value = GunInfo.GunState.Shooting;

        GunConfigItem gunconfig = this.GetModel<IGunConfigModel>().GetItemByName(gunSystem.CurrentGun.Name.Value);

        this.GetSystem<ITimeSystem>().AddDelayTask(1/gunconfig.Frequency, () => {
            gunSystem.CurrentGun.State.Value = GunInfo.GunState.Idle;

            if (gunSystem.CurrentGun.BulletCountInGun.Value == 0 && gunSystem.CurrentGun.BulletCountOutGun.Value > 0) {
                 this.SendCommand<ReloadCommand>();
                 RecycleToCache();
            }
        });
    }

  
   
}

